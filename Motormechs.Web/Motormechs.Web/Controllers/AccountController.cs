using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Motormechs.Web.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Motormechs.Web.Helper;
using Motormechs.Data.Entity;
using Motormechs.Business.Model;
using System.Web.Security;
using System.Configuration;

namespace Motormechs.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        public string ApiPath = ConfigurationManager.AppSettings["ApiURL"].ToString();

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                string uri = ApiPath + "Login/LoginCheck";
                //string uri = "http://localhost:5014/api/Login/LoginCheck";
                CustomResponse user = new CustomResponse();
                using (HttpClient httpClient = new HttpClient())
                {
                    //user = JsonConvert.DeserializeObject<CustomResponse>(await httpClient.PostAsJsonAsync(uri, model));
                    var xx = await httpClient.PostAsJsonAsync(uri, model);
                    user = xx.Content.ReadAsAsync<CustomResponse>().Result;

                }
                // var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user.IsSuccess == true)
                {
                    Users userss = JsonConvert.DeserializeObject<Users>(user.ResponseData.ToString());
                    FormsAuthenticationTicket(model, userss);
                    //await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", user.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void FormsAuthenticationTicket(LoginViewModel mod, Users list)
        {
            FormsAuthenticationTicket tk = default(FormsAuthenticationTicket);
            if (mod.RememberMe == true)
            {
                tk = new FormsAuthenticationTicket(1, list.Id.ToString() + "|" + list.Name, DateTime.Now, DateTime.Now.AddYears(1), mod.RememberMe, list.RoleType.ToString());
            }
            else
            {
                tk = new FormsAuthenticationTicket(1, list.Id.ToString() + "|" + list.Name, DateTime.Now, DateTime.Now.AddHours(2), mod.RememberMe, list.RoleType.ToString());
            }
            string st = null;
            st = FormsAuthentication.Encrypt(tk);
            HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, st);
            Response.Cookies.Add(ck);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser() { UserName = model.UserName };
                //var result = await UserManager.CreateAsync(user, model.Password);
                string uri = ApiPath + "User/Create";//http://localhost:5014/
                CustomResponse user = new CustomResponse();
                using (HttpClient httpClient = new HttpClient())
                {
                    Users obj = new Users();
                    obj.Name = model.UserName;
                    obj.Email = model.Email;
                    obj.Phone = model.Phone;
                    obj.Password = model.Password;


                    //user = JsonConvert.DeserializeObject<CustomResponse>(await httpClient.PostAsJsonAsync(uri, model));
                    var xx = await httpClient.PostAsJsonAsync(uri, obj);
                    user = xx.Content.ReadAsAsync<CustomResponse>().Result;

                }
                if (user.IsSuccess)
                {
                    LoginViewModel loginmodel = new LoginViewModel();
                    loginmodel.UserName = model.Phone;
                    loginmodel.Password = model.Password;
                    loginmodel.RememberMe = false;
                    Users userss = JsonConvert.DeserializeObject<Users>(user.ResponseData.ToString());
                    FormsAuthenticationTicket(loginmodel, userss);
                    //await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    ModelState.AddModelError("", user.Message);
                    //AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public async Task<ActionResult> Manage()
        {

            int Id = Convert.ToInt32(User.Identity.GetUserName().Split('|')[0]);

            string uri = ApiPath + "getuser/detail?Id=" + Id;//http://localhost:5014/
            CustomResponse user = new CustomResponse();
            using (HttpClient httpClient = new HttpClient())
            {
                var xx = await httpClient.GetAsync(uri);
                user = xx.Content.ReadAsAsync<CustomResponse>().Result;

            }
            Users userss = JsonConvert.DeserializeObject<Users>(user.ResponseData.ToString());
            ManageUserViewModel bUser = AutoMapper.Mapper.Map<ManageUserViewModel>(userss);
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View(bUser);
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {

            ViewBag.ReturnUrl = Url.Action("Manage");

            if (ModelState.IsValid)
            {

                int Id = Convert.ToInt32(User.Identity.GetUserName().Split('|')[0]);

                string uri = ApiPath + "Updateuser/detail";//http://localhost:5014/
                CustomResponse user = new CustomResponse();
                using (HttpClient httpClient = new HttpClient())
                {
                    Users bUser = AutoMapper.Mapper.Map<Users>(model);
                    bUser.Id = Id;
                    //user = JsonConvert.DeserializeObject<CustomResponse>(await httpClient.PostAsJsonAsync(uri, model));
                    var xx = await httpClient.PostAsJsonAsync(uri, bUser);
                    user = xx.Content.ReadAsAsync<CustomResponse>().Result;
                }

            }
            return View(model);
        }

        //
        // GET: /Account/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {

            ViewBag.ReturnUrl = Url.Action("ChangePassword");

            if (ModelState.IsValid)
            {

                int Id = Convert.ToInt32(User.Identity.GetUserName().Split('|')[0]);

                string uri = ApiPath + "User/ChangePassword";//http://localhost:5014/
                CustomResponse user = new CustomResponse();
                using (HttpClient httpClient = new HttpClient())
                {
                    ChangePasswordModel bPass = AutoMapper.Mapper.Map<ChangePasswordModel>(model);
                    bPass.Id = Id;
                    //user = JsonConvert.DeserializeObject<CustomResponse>(await httpClient.PostAsJsonAsync(uri, model));
                    var xx = await httpClient.PostAsJsonAsync(uri, bPass);
                    user = xx.Content.ReadAsAsync<CustomResponse>().Result;

                }

            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {

            return View();
        }

        //
        // POST: /Account/ChangePassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uri = ApiPath + "getuser/ByEmail?Email=" + model.Email;//http://localhost:5014/
                CustomResponse user = new CustomResponse();
                using (HttpClient httpClient = new HttpClient())
                {
                    var xx = await httpClient.GetAsync(uri);
                    user = xx.Content.ReadAsAsync<CustomResponse>().Result;

                }
                if (user.IsSuccess)
                {
                    Users userss = JsonConvert.DeserializeObject<Users>(user.ResponseData.ToString());
                    string address = "http://" + Request.Url.Authority;
                    string link = address + "/home/new-password/" + userss.ForgetPasswordCode;
                    string path = Server.MapPath("~/HtmlTemplate/ForgetPassword.html");
                    string subject = "Reset Password for Motormechs";
                    string body = System.IO.File.ReadAllText(path);
                    body = string.Format(body, userss.Name, link, address);
                    SendEmail(userss.Email, subject, body);
                }
                else
                {
                    ModelState.AddModelError("", user.Message);
                }
            }
            return View();
        }

        private void SendEmail(string email, string subject, string body)
        {
            try
            {
                EmailHandler objEmailHandler = new EmailHandler();
                objEmailHandler.mailTo = email;
                objEmailHandler.mailSubject = subject;
                objEmailHandler.mailBody = body;
                objEmailHandler.SendEmail();
            }
            catch (Exception ex)
            {
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> NewPassword(string fpc)
        {

            string uri = ApiPath + "ForgetPassword/Status?Fpc=" + fpc;//http://localhost:5014/
            CustomResponse Status = new CustomResponse();
            using (HttpClient httpClient = new HttpClient())
            {
                var xx = await httpClient.GetAsync(uri);
                Status = xx.Content.ReadAsAsync<CustomResponse>().Result;
            }
            if (Status.IsSuccess)
            {
                List<PasswordStatus> pStatus = JsonConvert.DeserializeObject<List<PasswordStatus>>(Status.ResponseData.ToString());
                ViewBag.fpc = fpc;
                if (pStatus.Count() > 0)
                {
                    int status = Convert.ToInt32(pStatus.First().status);
                    ViewBag.status = status;
                    if (status == -1)
                    {
                        ViewBag.LastPasswordChangedDate = pStatus.First().LastPasswordChangedDate.Value.ToString("d MMM yyyy");
                    }
                }
                else
                {
                    ViewBag.status = 0;
                }
            }
            else {
                ViewBag.status = 2;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewPassword(NewPasswordViewModel mod)
        {
            if (ModelState.IsValid)
            {
               // string Password = EncryptDecrypt.Encrypt(System.Configuration.ConfigurationManager.AppSettings["EncryptID"], mod.NewPassword.Trim(), true);

                string uri = ApiPath + "User/NewPassword";//http://localhost:5014/
                CustomResponse user = new CustomResponse();
                using (HttpClient httpClient = new HttpClient())
                {
                    NewPasswordModel bmod = AutoMapper.Mapper.Map<NewPasswordModel>(mod);
                    var xx = await httpClient.PostAsJsonAsync(uri, bmod);
                    user = xx.Content.ReadAsAsync<CustomResponse>().Result;

                }



                return RedirectToAction("login", "home");
            }
            else
            {
                ModelState.AddModelError("", "Please fill Password and Confirm Password field.");
                return View();
            }
           
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut();
            FormsAuthentication.SignOut();
            Session.Abandon();
            //return RedirectToAction("Index", "Home");
            return Redirect("/home/index");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }


        //
        // GET: /User/Create
        [AllowAnonymous]
        [RequiresUserRole(Route = "home", Action = "login", ReturnUrl = "/home/vehicle")]
        public async Task<ActionResult> ManageVehicle()
        {
            int Id = Convert.ToInt32(User.Identity.GetUserName().Split('|')[0]);

            string uri = ApiPath + "Vehicle/get?userId=" + Id;//http://localhost:5014/
            CustomResponse user = new CustomResponse();
            using (HttpClient httpClient = new HttpClient())
            {
                var xx = await httpClient.GetAsync(uri);
                user = xx.Content.ReadAsAsync<CustomResponse>().Result;

            }
            Vehicles userss = JsonConvert.DeserializeObject<Vehicles>(user.ResponseData.ToString());
            List<Vehicles> userssVeh = userss.List;
            return View(userssVeh);
        }

        //
        // GET: /User/Create
        [AllowAnonymous]
        [RequiresUserRole(Route = "home", Action = "login", ReturnUrl = "/home/add-vehicle")]
        public async Task<ActionResult> Vehicle(int? Id)
        {
            VehicleViewModel obj = new VehicleViewModel();
            if (Id == null)
            {

                return View(obj);
            }
            else
            {
                int userId = Convert.ToInt32(User.Identity.GetUserName().Split('|')[0]);

                string uri = ApiPath + "Vehicle/getByID?Id=" + Id + "&UserId=" + userId;//http://localhost:5014/
                CustomResponse user = new CustomResponse();
                using (HttpClient httpClient = new HttpClient())
                {
                    var xx = await httpClient.GetAsync(uri);
                    user = xx.Content.ReadAsAsync<CustomResponse>().Result;

                }
                Vehicles userss = JsonConvert.DeserializeObject<Vehicles>(user.ResponseData.ToString());
                if (userss.List.Count() > 0)
                {
                    Vehicles userssVeh = userss.List.FirstOrDefault();
                    obj = AutoMapper.Mapper.Map<VehicleViewModel>(userssVeh);
                    return View(obj);
                }
                else
                {
                    return Redirect("/home/vehicle");
                }
            }
        }

        //
        // POST: /User/Create
        [HttpPost]
        public async Task<ActionResult> Vehicle(VehicleViewModel vehicle)
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/home/login?returnUrl=" + Request.Url.AbsolutePath);


            int id = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            string userType = (((System.Web.Security.FormsIdentity)(User.Identity))).Ticket.UserData;
            if (ModelState.IsValid)
            {

                string uri = ApiPath + "Vehicle/Create";//http://localhost:5014/
                CustomResponse response = new CustomResponse();
                using (HttpClient httpClient = new HttpClient())
                {
                    Vehicles obj = new Vehicles();
                    Vehicles eVehicle = AutoMapper.Mapper.Map<Vehicles>(vehicle);

                    eVehicle.CreatedBy = id;
                    eVehicle.Owner = id;
                    var xx = await httpClient.PostAsJsonAsync(uri, eVehicle);
                    response = xx.Content.ReadAsAsync<CustomResponse>().Result;
                    if (response.IsSuccess)
                    {
                        Vehicles rVehicle = JsonConvert.DeserializeObject<Vehicles>(response.ResponseData.ToString());
                    }
                    else
                    {

                        ModelState.AddModelError("", response.Message);
                        //AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Redirect("/home/vehicle");
            //return View(vehicle);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}