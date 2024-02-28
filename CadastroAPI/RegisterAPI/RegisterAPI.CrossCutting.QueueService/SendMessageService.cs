using Newtonsoft.Json;
using RabbitMQ.Client;
using RegisterAPI.CrossCutting.ExternalService.Models;
using RegisterAPI.CrossCutting.QueueService.Interface;
using RegisterAPI.Entity.Entities;
using System.Text;

namespace RegisterAPI.CrossCutting.QueueService
{
    public class SendMessageService : ISendMessageService
    {
        public void SendClientMessage(Client client)
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

                var jsonMessage = JsonConvert.SerializeObject(client);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                channel.BasicPublish(exchange: "",
                                     routingKey: "client_queue",
                                     basicProperties: null,
                                     body: body);
            }
        }
        public void SendClientSync(ClientIntegration client)
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

                var jsonMessage = JsonConvert.SerializeObject(client);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                channel.BasicPublish(exchange: "",
                                     routingKey: "client_integration_queue",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
