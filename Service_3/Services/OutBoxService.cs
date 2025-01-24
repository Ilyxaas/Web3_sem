using Microsoft.AspNetCore.Http.HttpResults;
using Service_1.Models;

namespace Service_1.Services;

public class OutBoxService : IOutBoxService
{
    private  AppDatabaseContext _dbContext;
        
    public OutBoxService(AppDatabaseContext context)
    {
        _dbContext = context;
    }

    public async Task Created(OutBox item)
    {
        _dbContext.OutBoxes.Add(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(uint id, TransactionStatus status)
    {
        var temp = await _dbContext.OutBoxes.FindAsync(id);
        
        if (temp == null)
            throw new Exception();

        temp.Status = status;

        _dbContext.OutBoxes.Update(temp);
        
        await _dbContext.SaveChangesAsync();
    }
}