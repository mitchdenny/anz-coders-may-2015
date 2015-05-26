using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveEventsWithEventProcessor
{
    public class ConsoleEventProcessor : IEventProcessor
    {
        public async Task CloseAsync(PartitionContext context, CloseReason reason)
        {
        }

        public async Task OpenAsync(PartitionContext context)
        {
        }

        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var message in messages)
            {
                var json = Encoding.UTF8.GetString(message.GetBytes());
                Console.WriteLine(json);
            }

            await context.CheckpointAsync();
        }
    }
}
