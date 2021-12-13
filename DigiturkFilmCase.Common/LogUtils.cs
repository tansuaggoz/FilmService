using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigiturkFilmCase.Common
{
    public class LogUtils
    {
        private static readonly log4net.ILog log= log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public LogUtils()
        {
            //log4net.Config.XmlConfigurator.Configure();
        }

        internal static void LogDebug(string message)
        {
            log.Debug(message);
        }

        public static void LogError(string message)
        {
            log.Error(message);
        }

        public static void LogError(Exception ex, string message)
        {
            log.Error(message);
            log.Error(ex.ToString());
        }
        
        public static void LogInfo(string e)
        {
            log.Info(e);
        }

        public static void LogDebug(Exception e)
        {
            log.Debug(e);
        }

        public static void LogFatal(Exception e, string message)
        {
            log.Fatal(message, e);
        }

        public static void LogFatal(Exception e)
        {
            log.Fatal(e);
        }

        public static void LogWarn(Exception e)
        {
            log.Warn(e);
        }
    }
}
