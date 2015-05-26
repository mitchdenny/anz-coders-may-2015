﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEventsViaDotNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Press ENTER to start devices.");
                Console.ReadLine();
                
                var devices = new List<Device>();

                for (var deviceIndex = 0; deviceIndex < 100; deviceIndex++)
                {
                    var name = string.Format("device{0}", deviceIndex);
                    var @namespace = "anzcoders";
                    var eventHub = "readings";
                    var signature = string.Empty;
    
                var device = new Device(name, @namespace, eventHub, signature);
                    devices.Add(device);

                    Console.WriteLine("Starting {0}...", device.Name);
                    device.Start();
                }

                Console.WriteLine("Press ENTER to stop devices.");
                Console.ReadLine();

                foreach (var device in devices)
                {
                    Console.WriteLine("Stopping {0}...", device.Name);
                    device.Stop();
                }
            }
        }
    }
}
