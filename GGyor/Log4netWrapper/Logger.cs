using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace Log4netWrapper
{
    public class ApplicationLogger
    {
        private static bool isConfigured = false;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static bool _canNotConfigure = false;

        public static bool CanNotConfigure
        {
            get { return _canNotConfigure; }
        }

        private static ILog Logger
        {
            get
            {
                if (!isConfigured && !_canNotConfigure)
                {
                    try
                    {
                        XmlConfigurator.Configure();
                        isConfigured = true;
                    }
                    catch (Exception ex)
                    {
                        _canNotConfigure = true;
                    }
                }

                return log;

            }
        }

        // DEBUG
        public static void Debug(string message)
        {
            if (Logger.IsDebugEnabled)
                Logger.Debug(message);
        }
        public static void Debug(string message, Exception ex)
        {
            if (Logger.IsDebugEnabled)
                Logger.Debug(message, ex);
        }
        public static void DebugFormat(string message, params object[] args)
        {
            if (Logger.IsDebugEnabled)
                Logger.DebugFormat(message, args);
        }
        public static void DebugFormat(string message, Exception ex, params object[] args)
        {
            if (Logger.IsDebugEnabled)
                Logger.Debug(String.Format(message, args), ex);
        }

        // INFO
        public static void Info(string message)
        {
            if (Logger.IsInfoEnabled)
                Logger.Info(message);
        }
        public static void Info(string message, Exception ex)
        {
            if (Logger.IsInfoEnabled)
                Logger.Info(message, ex);
        }
        public static void InfoFormat(string message, params object[] args)
        {
            if (Logger.IsInfoEnabled)
                Logger.InfoFormat(message, args);
        }
        public static void InfoFormat(string message, Exception ex, params object[] args)
        {
            if (Logger.IsInfoEnabled)
                Logger.Info(String.Format(message, args), ex);
        }

        // WARNING
        public static void Warn(string message)
        {
            if (Logger.IsWarnEnabled)
                Logger.Warn(message);
        }
        public static void Warn(string message, Exception ex)
        {
            if (Logger.IsWarnEnabled)
                Logger.Warn(message, ex);
        }
        public static void WarnFormat(string message, params object[] args)
        {
            if (Logger.IsWarnEnabled)
                Logger.WarnFormat(message, args);
        }
        public static void WarnFormat(string message, Exception ex, params object[] args)
        {
            if (Logger.IsWarnEnabled)
                Logger.Warn(String.Format(message, args), ex);
        }


        // ERROR
        public static void Error(string message)
        {
            try
            {
                if (Logger.IsErrorEnabled)
                    Logger.Error(message);
            }
            catch (Exception)
            {

            }
        }
        public static void Error(string message, Exception ex)
        {
            try
            {
                if (Logger.IsErrorEnabled)
                    Logger.Error(message, ex);
            }
            catch (Exception)
            {

            }
        }
        public static void ErrorFormat(string message, params object[] args)
        {
            try
            {
                if (Logger.IsErrorEnabled)
                    Logger.ErrorFormat(message, args);
            }
            catch (Exception)
            {

            }
        }
        public static void ErrorFormat(string message, Exception ex, params object[] args)
        {
            try
            {
                if (Logger.IsErrorEnabled)
                    Logger.Error(String.Format(message, args), ex);
            }
            catch (Exception)
            {

            }
        }

        // FATAL
        public static void Fatal(string message)
        {
            if (Logger.IsFatalEnabled)
                Logger.Fatal(message);
        }
        public static void Fatal(string message, Exception ex)
        {
            if (Logger.IsFatalEnabled)
                Logger.Fatal(message, ex);
        }
        public static void FatalFormat(string message, params object[] args)
        {
            if (Logger.IsFatalEnabled)
                Logger.FatalFormat(message, args);
        }
        public static void FatalFormat(string message, Exception ex, params object[] args)
        {
            if (Logger.IsFatalEnabled)
                Logger.Fatal(String.Format(message, args), ex);
        }

    }
}
