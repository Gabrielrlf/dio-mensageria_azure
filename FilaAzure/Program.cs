using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using FilaAzure.FolderConsumer;
using FilaAzure.FolderProducer;

namespace FilaAzure
{
    class Program
    {
        static async Task Main(string[] args)
        {
             await Start();
        }

        private static async Task Start()
        {
            Console.WriteLine("Initializing...");
            Console.WriteLine("Whhat option? 1 - Producer Message | 2 - Consumer Message");
            int valueOption = int.Parse(Console.ReadLine());

            switch (valueOption)
            {
                case 1:
                    await new ProducerService().ProducingMessage("Message here");
                    await Start();
                    break;
          
                default:
                    CreateHostBuilder().Build().Run();
                    break;
            }

        }
        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ConsumerService>();
                services.AddHostedService<ConsumerService>();
            });
    }
}
