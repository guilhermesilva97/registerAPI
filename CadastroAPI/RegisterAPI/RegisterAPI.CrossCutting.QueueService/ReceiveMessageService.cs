using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RegisterAPI.CrossCutting.ExternalService.Interface;
using RegisterAPI.CrossCutting.ExternalService.Models;
using RegisterAPI.CrossCutting.QueueService.Interface;
using RegisterAPI.Entity.Entities;
using System.Text;

namespace RegisterAPI.CrossCutting.QueueService
{
    public class ReceiveMessageService : IReceiveMessageService
    {
        private readonly IIntegrationService _integrationService;
        public ReceiveMessageService(IIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }
        public async Task ReceiveMessages()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "client_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    var receivedObj = JsonConvert.DeserializeObject<Client>(jsonMessage);

                    if (receivedObj != null)
                    {
                        await _integrationService.SendClientToIntegrations(receivedObj);
                    }
                };

                channel.BasicConsume(queue: "client_queue",
                                     autoAck: true,
                                     consumer: consumer);

                while (true)
                {
                    await Task.Delay(1000);
                }
            }
        }
        public async Task ReceiveClientSyncMessages()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "client_integration_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    var receivedObj = JsonConvert.DeserializeObject<ClientIntegration>(jsonMessage);

                    if (receivedObj != null)
                    {
                        await _integrationService.SyncClientToIntegration(receivedObj);
                    }
                };

                channel.BasicConsume(queue: "client_integration_queue",
                                     autoAck: true,
                                     consumer: consumer);

                while (true)
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
