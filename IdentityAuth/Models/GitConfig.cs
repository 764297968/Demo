using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityAuth.Models
{
    public static class GitConfig
    {
        public static string Git_ClientID
        {
            get {return  GetAppSetting("Git_ClientID"); }
        }
        public static string Git_ClientSecret
        {
            get { return GetAppSetting("Git_ClientSecret"); }
        }
        public static string PageUrl
        {
            get { return GetAppSetting("PageUrl"); }
        }
        public static string Git_AuthUrl
        {
            get { return GetAppSetting("Git_AuthUrl"); }
        }
        public static string Git_ResponseUrl
        {
            get { return GetAppSetting("Git_ResponseUrl"); }
        }
        public static string Git_GetUserInfo
        {
            get { return GetAppSetting("Git_GetUserInfo"); }
        }
        
        private static string GetAppSetting(string key)
        {
            return System.Web.Configuration.WebConfigurationManager.AppSettings[key];
        }
    }
}