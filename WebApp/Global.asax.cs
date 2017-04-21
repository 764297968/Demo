using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
          //  Context.RewritePath();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void FormsAuthentication_OnAuthenticate(Object sender,
      FormsAuthenticationEventArgs e)
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] == null)
            {
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" +
                    HttpUtility.UrlEncode(e.Context.Request.Url.AbsoluteUri));
            }
        }
    }
}
