using SharedProject;

namespace Service_1.Services;

public interface IRabbitMqSender
{
    Task SendMessage(RabbitTransactionForm form);
}