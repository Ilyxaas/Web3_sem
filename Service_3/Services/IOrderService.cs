using Service_1.Models;

namespace Service_1.Services;

public interface IOrderService
{
    Task Create(OrdersCar order);
}