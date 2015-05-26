using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SendEventsViaDotNet
{
    public class Device
    {
        public Device(string name)
        {
            this.Name = name;
            this.intervalDuration = Device.random.Next(1000, 10000);
        }

        private static Random random = new Random();

        private string @namespace;
        private string eventHub;
        private string signature;
        private int intervalDuration;
        private Thread tickThread;

        public string Name { get; set; }

        private void Tick()
        {
            while (true)
            {
                Thread.Sleep(this.intervalDuration);

                var epoch = DateTimeOffset.Parse("1970-01-01");
                var timeSinceEpoch = DateTimeOffset.Now - epoch;

                var reading = new Reading()
                {
                    Instant = Math.Floor(timeSinceEpoch.TotalSeconds),
                    Device = this.Name,
                    BatteryLevel = 0,
                    LiquidDepth = 0,
                    WaterDetected = false
                };

                var json = JsonConvert.SerializeObject(reading);
                var payload = Encoding.UTF8.GetBytes(json);
                var @event = new EventData(payload);

                var connectionString = string.Format("Endpoint=sb://anzcoders.servicebus.windows.net/;EntityPath=readings/publishers/{0};SharedAccessKeyName=send201505250001;SharedAccessKey=M6YNE9Y9oTV2vbNXeNOzO0mCTMq4lFh0U+nzVgkFz90=", this.Name);
                var client = EventHubClient.CreateFromConnectionString(connectionString);
                client.Send(@event);
            }
        }

        public void Start()
        {
            if (this.tickThread != null)
            {
                throw new InvalidOperationException();
            }

            this.tickThread = new Thread(this.Tick);
            this.tickThread.IsBackground = true;
            this.tickThread.Start();
        }

        public void Stop()
        {
            if (this.tickThread == null)
            {
                throw new InvalidOperationException();
            }

            this.tickThread.Abort();
            this.tickThread = null;
        }
    }
}
