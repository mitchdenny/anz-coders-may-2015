using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Reading
    {
        [JsonProperty("instant")]
        public double Instant { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("liquidDepth")]
        public int LiquidDepth { get; set; }

        [JsonProperty("batteryLevel")]
        public int BatteryLevel { get; set; }

        [JsonProperty("waterDetected")]
        public bool WaterDetected { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
