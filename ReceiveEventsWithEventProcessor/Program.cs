using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveEventsWithEventProcessor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new EventProcessorHost(
                "localhost",
                "readings",
                "$Default",
                "Endpoint=sb://anzcoders.servicebus.windows.net/;SharedAccessKeyName=receive201502260001;SharedAccessKey=ZzsySuzZ7kvxOlgEQZTqFz61aYtdSRdAWNvGJFY6FOg=",
                "DefaultEndpointsProtocol=https;AccountName=anzcoders;AccountKey=kToCa3fs8IwN+wbawrruY6HSM+T1O3gaRMYg8ki8CmlKzWGs5UVtveo0UsCrO6xQgH2kHzq3dhN/O0C3bEQpUQ=="
                );

            host.RegisterEventProcessorAsync<ConsoleEventProcessor>().Wait();

            Console.WriteLine("Press ENTER to stop.");
            Console.ReadLine();
        }
    }
}
