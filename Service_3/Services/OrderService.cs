using Service_1.Models;

namespace Service_1.Services;

public class OrderService : IOrderService
{
    private AppDatabaseContext _context;
    
    public OrderService(AppDatabaseContext context)
    {
        _context = context;
    }

    public async Task Create(OrdersCar order)
    {
        _context.OrdersCars.Add(order);
        await _context.SaveChangesAsync();
    }
}