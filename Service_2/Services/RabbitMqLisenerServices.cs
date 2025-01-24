using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedProject;

namespace Service_2.Services;

public sealed class RabbitMqListenerService: IHostedService,  IAsyncDisposable
{
    private IConnection? _connection;

    private  IChannel? _channel;
    
    private readonly IServiceProvider _serviceProvider;
    
    private readonly JsonSerializerSettings _settings = new()
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All
    };

    public RabbitMqListenerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private async Task InitializeAsync()
    {
        var host =  "localhost";
        var factory = new ConnectionFactory() { HostName = host, Password = "guest", UserName = "guest"}; // Настройте ваш хост при необходимости
        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(queue: $"OutBox",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        
        await InitializeAsync();
        
        
        var consumer = new AsyncEventingBasicConsumer(_channel!);
        
        consumer.ReceivedAsync += async (model, ea) => 
        {
            
            var body = ea.Body.ToArray();
            
            var message = Encoding.UTF8.GetString(body);
            
            var messageForm = JsonConvert.DeserializeObject<RabbitTransactionForm>(message, _settings);
            
            using (var client = new HttpClient())
            {
                var url = "http://localhost:5002/api/Car/Submit";
                
                var content = new StringContent(messageForm.Id.ToString(), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                
                //Дальнейшая логика
                
                Console.WriteLine(message);
            }
            await _channel!.BasicAckAsync(ea.DeliveryTag, false, cancellationToken);
            
        };
        
        await _channel.BasicConsumeAsync(queue: $"OutBox",
            autoAck: true,
            consumer: consumer, cancellationToken: cancellationToken);
        
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null) await _channel.DisposeAsync();
        if (_connection != null) await _connection.DisposeAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null) await _connection.DisposeAsync();
        if (_channel != null) await _channel.DisposeAsync();
    }
}