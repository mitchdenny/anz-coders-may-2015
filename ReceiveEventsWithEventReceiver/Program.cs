using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveEventsWithEventReceiver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = EventHubClient.CreateFromConnectionString("Endpoint=sb://anzcoders.servicebus.windows.net/;EntityPath=readings;SharedAccessKeyName=receive201502260001;SharedAccessKey=ZzsySuzZ7kvxOlgEQZTqFz61aYtdSRdAWNvGJFY6FOg=");
            var consumerGroup = client.GetDefaultConsumerGroup();
            var receiver = consumerGroup.CreateReceiver("01");

            while (true)
            {
                var @event = receiver.Receive();
                var reading = Encoding.UTF8.GetString(@event.GetBytes());
                Console.WriteLine(reading);
            }
        }
    }
}
