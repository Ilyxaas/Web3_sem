using Service_1.Models;

namespace Service_1.Services;

public interface IOutBoxService
{
    Task Created(OutBox item);
    Task Update(uint id, TransactionStatus status);
}