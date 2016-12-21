using Cargo.API.Models;
using Cargo.API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Cargo.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SlotBookingController : ApiController
    {
        #region Global Variables
        private cargoEntities db = new cargoEntities();
        #endregion

        #region Actions
        /// <summary>
        /// Get Booking List
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> customize response according to request </returns>
        [HttpGet]
        [Route("api/booking/slots")]
        public IHttpActionResult GetBooking(string UserId)
        {
            //List<SlotBooking> list = new List<SlotBooking>();
            Guid id = new Guid(UserId);
            try
            {
               
                if (!string.IsNullOrEmpty(UserId))
                {
                    var list = from booking in db.SlotBookings 
                           where booking.UserId == id 
                           select new 
                           { 
                               BookingId=booking.Id, 
                               UserId=booking.UserId, 
                               BookerName=booking.BookerName, 
                               ReachTime=booking.ReachTime
                           };
                    if (list==null)
                    {
                        return Ok();
                    }
                    return Ok(list);
                }
                else // if login with email
                {
                    return Ok(new { Error = "Some error occured" });
                }

            }
            catch (Exception exp)
            {
                return Ok(new { Error = "Some error occured" });

            }
            //return Ok(new { UserId=user.Id,UserName=user.Name,Phone=user.Phone});
        }

        /// <summary>
        /// to save the status
        /// </summary>
        /// <param name="ArrivalLog"> status :I am on the way =1; Report a delay:2; Enter a message:3</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/booking/SaveStatus")]
        public IHttpActionResult SaveStatus(StatusDetail ArrivalLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ArrivalLog.Id = Guid.NewGuid() ;
                ArrivalLog.CreatedDate = DateTime.Now;
                db.StatusDetails.Add(ArrivalLog);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Ok(new { Error = "Some error occured" });
            }
            return Ok(new { success="true"});

        }
        /// <summary>
        /// Get the List of message with the booker
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="BookingId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/booking/GetMessage")]
        public IHttpActionResult StatusMessage(string UserId, string BookingId, int Status)
        {
            //List<SlotBooking> list = new List<SlotBooking>();
            Guid id = new Guid(UserId);
            Guid bookingid = new Guid(BookingId);
            try
            {

                if (!string.IsNullOrEmpty(UserId))
                {
                    var list = (from sd in db.StatusDetails
                               join u in db.Users on sd.UserId equals u.Id
                               join sb in db.SlotBookings on sd.SlotId equals sb.Id
                               where sd.UserId == id && sd.SlotId==bookingid && sd.Status==Status
                               select new
                               {
                                   BookingId = sd.Id,
                                   UserId = sd.UserId,
                                   BookerName = sb.BookerName,
                                   UserName=u.Name,
                                   Message = sd.Message,
                                   MessageTime=sd.CreatedDate
                               }).ToList();
                    if (list == null)
                    {
                        return Ok();
                    }
                    return Ok(list);
                }
                else // if login with email
                {
                    return Ok(new { Error = "Some error occured" });
                }

            }
            catch (Exception exp)
            {
                return Ok(new { Error = "Some error occured" });

            }
            //return Ok(new { UserId=user.Id,UserName=user.Name,Phone=user.Phone});
        }

        #endregion
    }
}
