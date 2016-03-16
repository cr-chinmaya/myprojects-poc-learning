using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogProvider
{
    public static class NLogProvider
    {
        public static Logger Instance { get; private set; }

        public static void ConfigureWebsiteLogger()
        {
            var applicationInsightTarget = new AppliationInsightTarget();            
            var applicationInsightRule = new LoggingRule("*", LogLevel.Trace, applicationInsightTarget);
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(applicationInsightTarget, LogLevel.Trace);
            LogManager.Configuration.AddTarget("ApplicationInsights", applicationInsightTarget);
            LogManager.Configuration.LoggingRules.Add(applicationInsightRule);
            LogManager.ReconfigExistingLoggers();

            Instance = LogManager.GetCurrentClassLogger();
        }
    }
}
