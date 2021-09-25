using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilaAzure.FolderProducer
{
    public class ProducerService
    {
        private readonly QueueClient _producer;


        public ProducerService()
        {
            _producer = new QueueClient("Endpoint=sb://filasbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=u50TLNQlXqemcVvCOTOgerrqOu0A+TDUZfRuAVInfMs=",
                "fila_teste");
        }

        public async Task<QueueClient> ProducingMessage(string message)
        {
            try
            {
                Console.WriteLine("Sending Message");
                await _producer.SendAsync(new Message(Encoding.UTF8.GetBytes(message)));
                Console.WriteLine("Endind sending message");
                return _producer;
            }
            catch (Exception e)
            {
                Console.WriteLine($"This error {e.Message}");
                throw e;
            }
            finally
            {
                Console.WriteLine("Finish producing message for queue");
            }
        }
    }
}
