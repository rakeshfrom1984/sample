using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cargo.API.Models;
using Cargo.API.Models.ViewModels;
using System.Web.Http.Cors;

namespace Cargo.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        #region Global Variables
        private cargoEntities db = new cargoEntities();
        #endregion

        #region Actions
        /// <summary>
        /// Check the login of the user
        /// </summary>
        /// <param name="login"></param>
        /// <returns> customize response according to request </returns>
        [HttpPost]
        [Route("api/Login/CheckLogin")]
        public IHttpActionResult Login(Login login)
        {
            User user = new User();
            try
            {
                // Login with phone
                if (!string.IsNullOrEmpty( login.PhoneNumber ))
                {
                    user = db.Users.Where(l => l.Phone == login.PhoneNumber && l.Password == login.Password).SingleOrDefault();
                    if (user == null)
                    {
                        return NotFound();
                    }
                    return Ok(new { UserId = user.Id, UserName = user.Name, Phone = user.Phone, LoginWith="Phone" });
                }
                else // if login with email
                {
                    user = db.Users.Where(l => l.Email == login.PhoneNumber && l.Password == login.Password).SingleOrDefault();
                    if (user == null)
                    {
                        return NotFound();
                    }
                    return Ok(new { UserId = user.Id, UserName = user.Name, Email = user.Email, LoginWith = "Email" });
                }
                
            }
            catch (Exception exp)
            {
                return NotFound();
                
            }
            //return Ok(new { UserId=user.Id,UserName=user.Name,Phone=user.Phone});
        }

        #endregion
    }
}
