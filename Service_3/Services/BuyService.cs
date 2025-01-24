using Service_1.Models;
using SharedProject;

namespace Service_1.Services;

public class BuyService : IBuyService
{
    private IOutBoxService _boxService;

    private IOrderService _orderService;

    private IRabbitMqSender _sender;
    
    public BuyService(IOutBoxService outBoxService, IOrderService orderService, IRabbitMqSender mqSender)
    {
        _boxService = outBoxService;
        _orderService = orderService;
        _sender = mqSender;
    }


    public async Task Buy(uint idType)
    {
        var newOrder = new OrdersCar() { Count = 1, CarType = 1};
        
        await _orderService.Create(newOrder);

        var outBoxTransaction = 
            new OutBox() { Id = newOrder.Id, Status = TransactionStatus.Started, Transaction = "BuyCar"};

        await _boxService.Created(outBoxTransaction);

        RabbitTransactionForm form = new() { Id = newOrder.Id, Transaction = "BuyCar" };

        await _sender.SendMessage(form);

    }
}