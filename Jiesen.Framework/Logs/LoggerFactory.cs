using System.IO;
using Jiesen.Core.ConfigManager;
using log4net;

namespace Jiesen.Framework.Logs
{
    /// <summary>
    /// Log Factory
    /// </summary>
    public static class LoggerFactory
    {
        public static ILogger Instance { get; }
        static LoggerFactory()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppConfig.GetAbsolutePath("log4net")));
            var log = LogManager.GetLogger("Default");
            Instance = new Log4Net(log);
        }

        public static ILogger GetLogger(string loggerName)
        {
            var log = LogManager.Exists(loggerName) ?? LogManager.GetLogger(loggerName);
            return new Log4Net(log);
        }

    }
}
