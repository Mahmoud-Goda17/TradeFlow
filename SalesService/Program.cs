
using System.Text.Json.Serialization;
using SalesService.InfraStrucure.Data;
using SalesService.InfraStrucure.Data.Interceptor;
using Microsoft.EntityFrameworkCore;
using SalesService.Application.Interfaces;
using SalesService.Application.Featuers.SalesOrdes.Interfaces;
using SalesService.Application.Featuers.SalesOrdes.services;
using SalesService.Application;
using SalesService.Api;
using Shared.Commen;
using SalesService.InfraStrucure.Services;


namespace SalesService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplication();
            builder.Services.SalesAddApi();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            ArgumentNullException.ThrowIfNull(connectionString);
            builder.Services.AddScoped<AuditableEntityInterceptor>();
            builder.Services.AddDbContext<AppDpContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
            });
            builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDpContext>());

            builder.Services.AddSingleton(TimeProvider.System);

            builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ProductUrl"]!);
            });
            builder.Services.AddHttpClient<ICustomerServiceClient, CustomerServiceClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["UserUrl"]!);
            });
            builder.Services.Configure<RappitMqConfiguration>(builder.Configuration.GetSection("RappitMqConfiguration"));
            builder.Services.AddScoped<IRabbitmqservice, Rabbitmqservice>();

            var app = builder.Build();

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
