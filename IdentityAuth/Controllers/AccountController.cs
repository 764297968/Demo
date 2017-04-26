using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using IdentityAuth.Models;
using System.IO;

namespace IdentityAuth.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl)) {
                    return Redirect(returnUrl);
                }
                else{
                    return Redirect(WebConfigurationManager.AppSettings["loginUrl"]);
                }
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            string loginName = model.Email;
            if (string.IsNullOrEmpty(loginName))
                return View();

            FormsAuthentication.SetAuthCookie(loginName, true);
            return Redirect(returnUrl);
        }
        public ActionResult GitHubLogin()
        {
            string url=GitConfig.Git_AuthUrl;
            string str = "";
            HttpWebRequest req =(HttpWebRequest) HttpWebRequest.Create(url);
            req.MediaType = "POST";
            WebResponse res= req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream());
            str = reader.ReadToEnd();
            reader.Close();
            res.Close();
            return Content(str);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}