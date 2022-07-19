using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security;
//using Microsoft.Build.Framework;
//using Microsoft.Build.Utilities;

namespace QuasarERPBanking.Models
{
    public class MyLogger
    {
        //private static Logger logger = null;

        public static void Init()
        {
            //LogManager.ThrowExceptions = true;
            //logger = LogManager.GetLogger("MyLogger");
        }

        //Variable number of params

        public static void Info(string message, params object[] list)
        {
            //if (logger != null)
            //    logger.Info(message, list);//El list funciona asi???
        }

        public static void Warning(string message, params object[] list)
        {
            //if (logger != null)
            //    logger.Warn(message, list);//El list funciona asi???
        }

        public static void Error(string message, params object[] list)
        {
            //if (logger != null)
            //    logger.Error(message, list);//El list funciona asi???
        }

        public static void Exception(Exception e)
        {
            MyLogger.Error("EXCEPTION: " + e.Message +
                            "\r\nInnerException:\r\n" + e.InnerException +
                            "\r\nStacktrace:\r\n" + e.StackTrace);
        }
    }
}