using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Treinos_API_Backend.Configurations
{
    public class Config
    {
        public Config(){}

        public static string GetLogPath()
        {
            return GetLogPath("logPath");
        }
        public static string GetLogPath(string key)
        {
            string logPath = System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            logPath = Path.Combine(logPath , $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
            return logPath;
        }

        public static string GetConnectionString()
        {
            return GetConnectionString("BDFit");
        }
        public static string GetConnectionString(string name)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public static int GetCacheExpiration(string key)
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[key].ToString());
        }
    }
}