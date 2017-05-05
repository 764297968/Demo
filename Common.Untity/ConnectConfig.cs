using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Untity
{
    public static class ConnectConfig
    {
        public static string ConnString()
        {
             return System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ToString();
        }
        public static string ConnString(string connectAddress, string dataBase, string userName, string passWord)
        {
            string str = string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ToString(), connectAddress, dataBase, userName, passWord);
            return str;

        }
    }
}
