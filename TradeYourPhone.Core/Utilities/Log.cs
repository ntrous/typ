using log4net;
using log4net.Config;
using System;

namespace TradeYourPhone.Core.Utilities
{
    public static class Log
    {
        private static readonly ILog logger =
           LogManager.GetLogger(typeof(Log));

        static Log()
        {
            DOMConfigurator.Configure();
        }

        public static void LogError(string msg)
        {
            logger.Error(msg);
        }

        public static void LogError(string msg, Exception ex)
        {
            logger.Error(msg, ex);
        }

        public static void LogInfo(string msg)
        {
            logger.Info(msg);
        }
    }
}
