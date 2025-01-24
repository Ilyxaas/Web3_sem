using Microsoft.EntityFrameworkCore;
using Service_1.Models;
using Service_1.Services;

namespace Web_3Sem
{
    public class Program
    {
        public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<AppDatabaseContext>(options => options.UseNpgsql
            (builder.Configuration.GetConnectionString("ConnectionStrings")));

        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IOutBoxService, OutBoxService>();
        builder.Services.AddScoped<IRabbitMqSender, RabbitMqSender>();
        builder.Services.AddScoped<IBuyService, BuyService>();

        // Add services to the container.
        builder.Services.AddAuthorization();
        

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
    }
}