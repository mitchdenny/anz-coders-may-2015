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
        public Device(string name, string @namespace, string eventHub, string signature)
        {
            this.Name = name;
            this.@namespace = @namespace;
            this.eventHub = eventHub;
            this.signature = signature;
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
