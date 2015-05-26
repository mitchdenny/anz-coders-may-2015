using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEventsViaDotNet
{
    public class Reading
    {
        [JsonProperty("instant")]
        public int Instant { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("liquidDepth")]
        public int LiquidDepth { get; set; }

        [JsonProperty("batteryLevel")]
        public int BatteryLevel { get; set; }

        [JsonProperty("waterDetected")]
        public bool WaterDetected { get; set; }
    }
}
