
using Microsoft.EntityFrameworkCore;
using QueueMessage.Consumer.DataBase;
using WebApi.Infrastructure.Contracts;
using WebApi.Infrastructure.Contracts.MessageQueue;
using WebApi.Infrastructure.Contracts.Repository;
using WebApi.Infrastructure.Contracts.Service;
using WebApi.Infrastructure.Repository;
using WebApi.Service;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<MySqlDbContext>(options =>
             options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            ConfigurationInterface(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ConfigurationInterface(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddSingleton<IMessageQueueService, MessageQueueService>();
            builder.Services.AddSingleton<IMessageQueueRepository, MessageQueueRepository>();
        }
    }
}
