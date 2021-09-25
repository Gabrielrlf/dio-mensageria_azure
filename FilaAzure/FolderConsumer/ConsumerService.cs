using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FilaAzure.FolderConsumer
{
    class ConsumerService : BackgroundService
    {
        private readonly QueueClient _client;
        public ConsumerService()
        {
            _client = new QueueClient("Endpoint=sb://filasbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=u50TLNQlXqemcVvCOTOgerrqOu0A+TDUZfRuAVInfMs=",
                "fila_teste", ReceiveMode.PeekLock);
            Console.WriteLine("Starting reading be Queue");
        
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await _client.CloseAsync();
            Console.WriteLine("Finishing reading queue and connection with queue");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() =>
                {
                    _client.RegisterMessageHandler(ProcessMessage,
                        new MessageHandlerOptions(ProcessError)
                        {
                            MaxConcurrentCalls = 1,
                            AutoComplete = false
                        }
                    );
                });
            }
        }

        private async Task ProcessMessage(Message message, CancellationToken cancellationToken)
        {
            var body = Encoding.UTF8.GetString(message.Body);

            Console.WriteLine($"New Message receiver: {body}");
            await _client.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ProcessError(ExceptionReceivedEventArgs e)
        {
            Console.WriteLine($"Error \n" +
                $"{e.Exception.GetType().FullName}\n" +
                $"{e.Exception.Message}");
            
            return Task.CompletedTask;
        }
    }
}
