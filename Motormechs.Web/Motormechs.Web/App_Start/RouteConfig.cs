using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Motormechs.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "login", url: "home/login", defaults: new { controller = "Account", action = "Login" });
            routes.MapRoute(name: "register", url: "home/register", defaults: new { controller = "Account", action = "Register" });

            routes.MapRoute(name: "ManageVehicle", url: "home/vehicle", defaults: new { controller = "Account", action = "ManageVehicle" });
            routes.MapRoute(name: "addvehicle", url: "home/add-vehicle", defaults: new { controller = "Account", action = "Vehicle" });
            routes.MapRoute(name: "editvehicle", url: "home/edit-vehicle/{Id}", defaults: new { controller = "Account", action = "Vehicle", Id = UrlParameter.Optional });

            routes.MapRoute(name: "Manage", url: "home/account", defaults: new { controller = "Account", action = "Manage" });
            routes.MapRoute(name: "ChangePassword", url: "home/change-password", defaults: new { controller = "Account", action = "ChangePassword" });
            routes.MapRoute(name: "ForgetPassword", url: "home/forget-password", defaults: new { controller = "Account", action = "ForgetPassword" });
            routes.MapRoute(name: "newpassword", url: "home/new-password/{fpc}", defaults: new { controller = "Account", action = "NewPassword", fpc = UrlParameter.Optional });

            routes.MapRoute(name: "services", url: "home/services", defaults: new { controller = "Home", action = "Services" });
            routes.MapRoute(name: "BuyServices", url: "home/new/{services}", defaults: new { controller = "Home", action = "BuyServices", services = UrlParameter.Optional });
            routes.MapRoute(name: "MyServices", url: "home/my-service", defaults: new { controller = "Home", action = "ServicesDetail" });
            //routes.MapRoute(name: "Profile", url: "home/profile/{username}", defaults: new { controller = "Home", action = "ProfilePage", username = UrlParameter.Optional });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
