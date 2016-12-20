using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text.RegularExpressions;
namespace Motormechs.Web.Helper
{
    public class FilterRoles : ActionFilterAttribute
    {
        protected string[] rolestocheck = { };

        /// <summary>
        /// Get Set Route
        /// </summary>
        public string Route
        {
            get;
            set;
        }

        /// <summary>
        /// Get Set Action
        /// </summary>
        public string Action
        {
            get;
            set;
        }

        /// <summary>
        /// Get Set ReturnUrl
        /// </summary>
        public string ReturnUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Set roles to empty string
        /// </summary>
        public FilterRoles()
        {
            Route = "";
            Action = "";
            ReturnUrl = "";
        }

        /// <summary>
        /// Routing
        /// </summary>
        /// <returns></returns>
        private string redirectUrl(string url)
        {
            //if (Action.CompareTo("") != 0)
            //{
            //    if (ReturnUrl.Trim() != "")
            //    {
            //        return "/" + Route + "/" + Action + "?returnUrl=" + ReturnUrl;
            //    }
            //    else {
            //        return "/" + Route + "/" + Action;
            //    }
            //}

            if (url.CompareTo("") != 0)
            {
                if (url.Trim() != "")
                {
                    return "/" + Route + "/" + Action + "?returnUrl=" + url;
                }
                else
                {
                    return "/" + Route + "/" + Action;
                }
            }
            return "/" + Route;
        }

        /// <summary>
        /// Check Roles
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool checkRoles(ActionExecutingContext filterContext)
        {
            bool isAuthorized = false;

            //Checking Roles//
            foreach (string x in rolestocheck)
            {
                if (((System.Web.Security.FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData == x)
                {
                    isAuthorized = true;
                }
            } 
            return isAuthorized;
        }

        /// <summary>
        /// Overriding OnActionExecuting
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Authenticating the identity//
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //filterContext.HttpContext.Response.Redirect(redirectUrl(), true);
                filterContext.Result = new RedirectResult(redirectUrl(filterContext.HttpContext.Request.RawUrl.ToString()));

            }
            else
            {
                bool isAuthorized = checkRoles(filterContext);
                if (!isAuthorized)
                {
                    //filterContext.HttpContext.Response.Redirect(redirectUrl(), true);
                    filterContext.Result = new RedirectResult(redirectUrl(filterContext.HttpContext.Request.RawUrl.ToString()));
                }
            }
        }
    }

    /// <summary>
    /// Role for this site - Admin
    /// </summary>
    public class RequiresAdminRole : FilterRoles
    {
        public RequiresAdminRole()
        {
            rolestocheck = new string[] { "Admin" };
        }
    }

    /// <summary>
    /// Role for this site - Manager
    /// </summary>
    public class RequiresManagerRole : FilterRoles
    {
        public RequiresManagerRole()
        {
            rolestocheck = new string[] { "Admin", "Manager" };
        }
    }

    /// <summary>
    /// Role for this site - User
    /// </summary>
    public class RequiresUserRole : FilterRoles
    {
        public RequiresUserRole()
        {
            rolestocheck = new string[] { "Admin", "Manager", "User" };
        }
    }
       
 

   

   
}
