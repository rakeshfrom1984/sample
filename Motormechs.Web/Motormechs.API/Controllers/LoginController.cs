using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Motormechs.Business.Model;
using Motormechs.Business.Repositories;
//using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections;

namespace Motormechs.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        #region Global Variable
        CustomResponse response;
        Motormechs.Business.Repositories.IMotormechsRepository businessLogin;
        #endregion

        #region Login
        [HttpPost]
        [Route("api/Login/LoginCheck")]
        public CustomResponse LoginCheck(Login login)
        {
            // create response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Users bUser = businessLogin.CheckLogin(login);
                    if (bUser.Status == true)// if User Exist
                    {
                        // set Sucess
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bUser.Message;
                        // Set Data
                        response.ResponseData = bUser;
                    }
                    else // if user does not exist
                    {
                        response.IsSuccess = false;
                        response.Message = bUser.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }
        #endregion

        #region Register

        [HttpPost]
        [Route("api/User/Create")]
        public CustomResponse CreateUser(Users user)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Users bUser = businessLogin.CreateUser(user);
                    if (bUser.Status == true)// if User Create
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bUser.Message;
                        response.ResponseData = bUser;
                    }
                    else // if user already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bUser.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }
        #endregion

        #region Create Vehicle
        [HttpPost]
        [Route("api/Vehicle/Create")]
        public CustomResponse CreateVehicle(Vehicles Vehicle)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Vehicles bVehicle = businessLogin.AddVehicle(Vehicle);
                    if (bVehicle.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bVehicle.Message;
                        response.ResponseData = bVehicle;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bVehicle.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }

        [HttpGet]
        [Route("api/Vehicle/get")]
        public CustomResponse GetVehicle(int userId)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Vehicles bVehicle = businessLogin.getVehicleDetail(userId);
                    if (bVehicle.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bVehicle.Message;
                        response.ResponseData = bVehicle;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bVehicle.Message;
                        response.ResponseData = bVehicle;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }

        [HttpGet]
        [Route("api/Vehicle/getByID")]
        public CustomResponse GetVehicleByID(int Id, int userId)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Vehicles bVehicle = businessLogin.getVehicleDetailById(Id, userId);
                    if (bVehicle.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bVehicle.Message;
                        response.ResponseData = bVehicle;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bVehicle.Message;
                        response.ResponseData = bVehicle;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }


        [HttpGet]
        [Route("api/Vehicle/Delete")]
        public CustomResponse DeleteVehicle(int Id, int UserId)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    var bVehicle = businessLogin.DeleteVehicle(Id, UserId);
                    if (bVehicle.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bVehicle.Message;
                        response.ResponseData = bVehicle.List.Count();
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bVehicle.Message;
                        //response.ResponseData = bVehicle;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }
        #endregion

        #region Create Role
        [HttpPost]
        [Route("api/Role/Create")]
        public CustomResponse CreateRole(Role role)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Role bRole = businessLogin.CreateRole(role);
                    if (bRole.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bRole.Message;
                        response.ResponseData = bRole;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bRole.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }
        #endregion

        #region Update Role
        [HttpPost]
        [Route("api/Role/Update")]
        public CustomResponse UpdateRole(Role role)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Role bRole = businessLogin.UpdateRole(role);
                    if (bRole.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bRole.Message;
                        response.ResponseData = bRole;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bRole.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }
        #endregion

        #region Get User Detail
        [HttpGet]
        [Route("api/getuser/detail")]
        public CustomResponse GetUserDetail(int Id)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Users bUsers = businessLogin.GetUserDetail(Id);
                    if (bUsers.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bUsers.Message;
                        response.ResponseData = bUsers;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bUsers.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }

        [HttpGet]
        [Route("api/getuser/ByEmail")]
        public CustomResponse GetUserDetailbyEmail(string Email)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Guid Fpc = Guid.NewGuid();
                    Users bUsers = businessLogin.UpdateMembershipForForgetPassword(Email, Fpc, DateTime.Now, true);
                    if (bUsers.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bUsers.Message;
                        response.ResponseData = bUsers;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bUsers.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }

        [HttpGet]
        [Route("api/ForgetPassword/Status")]
        public CustomResponse GetForgetPasswordStatus(Guid Fpc)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    List<PasswordStatus> bUsers = businessLogin.GetForgetPasswordStatus(Fpc);
                    if (bUsers.Count() > 0)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = "";
                        response.ResponseData = bUsers;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }
        #endregion

        #region Update User Detail
        [HttpPost]
        [Route("api/Updateuser/detail")]
        public CustomResponse UpdateUserDetail(Users user)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Users bUsers = businessLogin.UpdateUserDetail(user);
                    if (bUsers.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bUsers.Message;
                        response.ResponseData = bUsers;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bUsers.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }
   
        [HttpPost]
        [Route("api/User/ChangePassword")]
        public CustomResponse ChangePassword(ChangePasswordModel user)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    ChangePasswordModel bUsers = businessLogin.ChangePassword(user);
                    if (bUsers.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bUsers.Message;
                        response.ResponseData = bUsers;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bUsers.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }

        [HttpPost]
        [Route("api/User/NewPassword")]
        public CustomResponse NewPassword(NewPasswordModel user)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    NewPasswordModel bUsers = businessLogin.NewPassword(user);
                    if (bUsers.Status == true)// if vehicle Created
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bUsers.Message;
                        response.ResponseData = bUsers.List;
                    }
                    else // if vehicle already exist
                    {
                        response.IsSuccess = false;
                        response.Message = bUsers.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }
        #endregion

        #region Service
        [HttpGet]
        [Route("api/Services/Get")]
        public CustomResponse GetServices()
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Services bVehicle = businessLogin.GetServices();
                    if (bVehicle.Status == true)// if Service Exist
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bVehicle.Message;
                        response.ResponseData = bVehicle.ListService;
                    }
                    else // if Service not exist
                    {
                        response.IsSuccess = false;
                        response.Message = bVehicle.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }

        [HttpPost]
        [Route("api/Services/Add")]
        public CustomResponse SaveServices(NewServiceModel NewService)
        {
            // ArrayList array = new ArrayList();
            // create object of response class

            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    Services bService = businessLogin.AddServices(NewService);
                    if (bService.Status == true)// if Service Saved Successfully
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bService.Message;
                        //response.ResponseData = bService.ListService;
                    }
                    else // if error occure during save
                    {
                        response.IsSuccess = false;
                        response.Message = bService.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }

        [HttpGet]
        [Route("api/ServiceList/Get")]
        public CustomResponse GetUserServices(int UserId)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    UserServiceList bService = businessLogin.GetUserServices(UserId);
                    if (bService.Status == true)// if Service Exist
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bService.Message;
                        response.ResponseData = bService.List;
                    }
                    else // if Service not exist
                    {
                        response.IsSuccess = false;
                        response.Message = bService.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }

        [HttpGet]
        [Route("api/ServiceDetailList/Get")]
        public CustomResponse GetUserServiceDetail(int OrderServiceId)
        {
            // create object of response class
            response = new CustomResponse();
            try
            {
                using (businessLogin = new MotormechsRepository())
                {
                    UserServiceDetailList bService = businessLogin.GetUserServiceDetail(OrderServiceId);
                    if (bService.Status == true)// if Service Exist
                    {
                        response.IsSuccess = true;
                        // Set Message
                        response.Message = bService.Message;
                        response.ResponseData = bService.List;
                    }
                    else // if Service not exist
                    {
                        response.IsSuccess = false;
                        response.Message = bService.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }
        #endregion

        //#region Check Token
        //public CustomResponse CheckToken(Users user)
        //{
        //    // create response class
        //    response = new CustomResponse();
        //    try
        //    {
        //        using (businessLogin = new MotormechsRepository())
        //        {
        //            bool status = businessLogin.CheckToken(user.LoginId, user.Token);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.Message = ex.Message.ToString();
        //    }
        //    return response;
        //}
        //#endregion



        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}