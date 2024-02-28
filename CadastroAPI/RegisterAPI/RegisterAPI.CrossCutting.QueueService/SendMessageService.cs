using Newtonsoft.Json;
using RabbitMQ.Client;
using RegisterAPI.CrossCutting.QueueService.Interface;
using RegisterAPI.Entity.Entities;
using System.Text;

namespace RegisterAPI.CrossCutting.QueueService
{
    public class SendMessageService : ISendMessageService
    {
        public void SendUserMessage(Client user)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "user_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var jsonMessage = JsonConvert.SerializeObject(user);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                channel.BasicPublish(exchange: "",
                                     routingKey: "user_queue",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
