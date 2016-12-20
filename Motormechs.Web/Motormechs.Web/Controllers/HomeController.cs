using Motormechs.Business.Model;
using Motormechs.Web.Helper;
using Motormechs.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace Motormechs.Web.Controllers
{
    public class HomeController : Controller
    {
        public string ApiPath = ConfigurationManager.AppSettings["ApiURL"].ToString();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.Message = "Your AboutUs page.";

            return View();
        }

        public ActionResult ContactUs()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Services()
        {
            ViewBag.Message = "Your services page.";

            return View();
        }

        //
        // GET: /User/Create
        [AllowAnonymous]
        [RequiresUserRole(Route = "home", Action = "login", ReturnUrl = "/home/new/spa")]
        public async Task<ActionResult> BuyServices(string services)
        {
            string uri = ApiPath + "Services/Get";//http://localhost:5014/
            CustomResponse response = new CustomResponse();
            List<Services> List = new List<Business.Model.Services>();
            using (HttpClient httpClient = new HttpClient())
            {
                var xx = await httpClient.GetAsync(uri);
                response = xx.Content.ReadAsAsync<CustomResponse>().Result;
                if (response.IsSuccess)
                {
                    List = JsonConvert.DeserializeObject<List<Services>>(response.ResponseData.ToString());
                }
            }
           
            return View(List);
        }

        [HttpPost]
        [AllowAnonymous]
        [RequiresUserRole(Route = "home", Action = "login", ReturnUrl = "/home/new/spa")]

        public async Task<ActionResult> BuyServices(List<ServiceIds> Services, Vehicles VehicleViewModel, string PickNDrop)
        //public async Task<ActionResult> BuyServices(NewServiceModel service)
        {
            string returnMessage = "false";
            ArrayList paramList = new ArrayList();
            string uri = ApiPath + "Services/Add";//http://localhost:5014/
            CustomResponse response = new CustomResponse();
            List<Services> List = new List<Business.Model.Services>();
            using (HttpClient httpClient = new HttpClient())
            {
                
                NewServiceModel sModel = new NewServiceModel();
                sModel.Services = Services;
                sModel.VehicleViewModel = VehicleViewModel;
                sModel.PickNDrop = PickNDrop;
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                sModel.UserId = id;
                //ArrayList paramList = new ArrayList();
               // paramList.Add(Services);
                //paramList.Add(vehicle);
                //paramList.Add(PickAndDrop);
               // var x = JsonConvert.SerializeObject(new { Services = Services, vehicle = vehicle, PickAndDrop=PickAndDrop });
                var xx = await httpClient.PostAsJsonAsync(uri, sModel); 
                response = xx.Content.ReadAsAsync<CustomResponse>().Result;
                if (response.IsSuccess)
                {
                    returnMessage = "True";
                }
            }
            //return View();
           return Json(returnMessage, JsonRequestBehavior.AllowGet);
        }

        // POST: /User/Create
        [HttpPost]
        public async Task<ActionResult> services(VehicleViewModel vehicle)
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
                    }
                }

            }

            // If we got this far, something failed, redisplay form
            return View(vehicle);
        }

        
        public async Task<ActionResult> ServicesDetail()
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/home/login?returnUrl=" + Request.Url.AbsolutePath);

            List<UserServiceList> serviceList = new List<UserServiceList>();
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            string userType = (((System.Web.Security.FormsIdentity)(User.Identity))).Ticket.UserData;
            if (ModelState.IsValid)
            {
                string uri = ApiPath + "ServiceList/Get?UserId=" + id;//http://localhost:5014/
                CustomResponse response = new CustomResponse();
                using (HttpClient httpClient = new HttpClient())
                {
                    var xx = await httpClient.GetAsync(uri);
                    response = xx.Content.ReadAsAsync<CustomResponse>().Result;
                    if (response.IsSuccess)
                    {
                        serviceList = JsonConvert.DeserializeObject<List<UserServiceList>>(response.ResponseData.ToString());
                    }
                    else
                    {
                        ModelState.AddModelError("", response.Message);
                    }
                }

            }

            // If we got this far, something failed, redisplay form
            return View(serviceList);
        }

        //
        //// POST: /User/Create
        //[HttpPost]
        //public async Task<ActionResult> services(VehicleViewModel vehicle)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //        return Redirect("/home/login?returnUrl=" + Request.Url.AbsolutePath);


        //    int id = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
        //    string userType = (((System.Web.Security.FormsIdentity)(User.Identity))).Ticket.UserData;
        //    if (ModelState.IsValid)
        //    {
        //        string uri = ApiPath + "Vehicle/Create";//http://localhost:5014/
        //        CustomResponse response = new CustomResponse();
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            Vehicles obj = new Vehicles();
        //            Vehicles eVehicle = AutoMapper.Mapper.Map<Vehicles>(vehicle);

        //            eVehicle.CreatedBy = id;
        //            eVehicle.Owner = id;
        //            var xx = await httpClient.PostAsJsonAsync(uri, eVehicle);
        //            response = xx.Content.ReadAsAsync<CustomResponse>().Result;
        //            if (response.IsSuccess)
        //            {
        //                Vehicles rVehicle = JsonConvert.DeserializeObject<Vehicles>(response.ResponseData.ToString());
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", response.Message);
        //            }
        //        }

        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(vehicle);
        //}

    }
}