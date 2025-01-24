using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SharedProject;

namespace Service_1.Services;

public class RabbitMqSender : IRabbitMqSender
{
    public async Task SendMessage(RabbitTransactionForm form)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
        
        IConnection connection = await factory.CreateConnectionAsync();
        IChannel channel = await connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(queue: $"OutBox",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(form));

        await channel.BasicPublishAsync("", $"OutBox",body);

    }
}