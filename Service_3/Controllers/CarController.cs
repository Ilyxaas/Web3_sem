using System.Text;
using Microsoft.AspNetCore.Mvc;
using Service_1.Models;
using Service_1.Services;

namespace Web_3Sem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private IBuyService _service;

        private IOutBoxService _Outservice;
        
        public CarController(IBuyService buyService, IOutBoxService boxService)
        {
            _service = buyService;
            _Outservice = boxService;
        }

        [HttpPost("Buy")]
        public async Task<IActionResult> Buy()
        {
            // Работа с 1 сервисом
            await _service.Buy(1);
            
            
            // Работа с другим сервисом
            
            using (var client = new HttpClient())
            {
                var url = "http://localhost:5009/api/AnyControllers/Notification";
                
                var content = new StringContent("5", Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                
            }
             
            return Ok();
            
        }
        
        [HttpPost("Submit")]
        public async Task<IActionResult> SubmitTransaction([FromBody] uint id)
        {
            Console.WriteLine($"Транзакция с номером {id} доставлена");
            
            await _Outservice.Update(id, TransactionStatus.Completed);
            return Ok();
        }
    }
}