using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cargo.API.Models.ViewModels
{
    public class ArrivalStatus
    {
        public Guid UserId { get; set; }
        public Guid BookingId { get; set; }
        public Guid StatusId { get; set; }
        public string  Message { get; set; }
        public DateTime DelayTime { get; set; }
        public string Reason { get; set; }
        public bool OnTheWay { get; set; }
        public int Status { get; set; }


    }
}