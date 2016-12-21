using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cargo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {
            return PartialView("login");
        }

        public ActionResult Home()
        {
            return PartialView("Home");
        }
        public ActionResult Slots()
        {
            return PartialView("Slots");
        }
        public ActionResult SlotDetail()
        {
            return PartialView("SlotDetail");
        }
        public ActionResult SlotDetailMessage()
        {
            return PartialView("SlotDetailMessage");
        }
        public ActionResult ReportDelay()
        {
            return PartialView("ReportDelay");
        }
    }
}