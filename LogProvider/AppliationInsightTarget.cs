using Microsoft.ApplicationInsights;
using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogProvider
{
    public class AppliationInsightTarget : Target
    {
        public AppliationInsightTarget()
        {

        }

        public TelemetryClient telemetryClient { get; set; }

        //
        // Summary:
        //     Initializes the target. Can be used by inheriting classes to initialize logging.
        protected override void InitializeTarget()
        {
            this.telemetryClient = new TelemetryClient();
            this.telemetryClient.Context.InstrumentationKey = "12345678901234567890-=-098765432";
        }

        //
        // Summary:
        //     Writes logging event to the log target. classes.
        //
        // Parameters:
        //   logEvent:
        //     Logging event to be written out.
        protected override void Write(LogEventInfo logEvent)
        {
            this.telemetryClient.TrackEvent("##### Chinmaya---Test Event #####");
        }
    }
}
