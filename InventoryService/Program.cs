
using InventoryService.Api;
using InventoryService.Application;
using InventoryService.Application.Featuers.stock.Interfaces;
using InventoryService.Application.Featuers.stock.Service;
using InventoryService.Application.Interfaces;
using InventoryService.InfraStrucure.Data;
using InventoryService.InfraStrucure.Data.Interceptor;
using Microsoft.EntityFrameworkCore;

namespace InventoryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationInventory();
            builder.Services.InventoryAddApi();

            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            ArgumentNullException.ThrowIfNull(connectionString);
            builder.Services.AddScoped<AuditableEntityInterceptor>();
            builder.Services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
            });
            builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            builder.Services.AddSingleton(TimeProvider.System);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["Url"]!);
            });



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
