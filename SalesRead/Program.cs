
using SalesRead.Application.Interfaces;
using SalesRead.InfraStrucure.Data;
using SalesRead.InfraStrucure.Data.Interceptor;
using Microsoft.EntityFrameworkCore;
using Shared.Commen;
using SalesRead.InfraStrucure.Consumers;
using SalesRead.Application;
using SalesRead.Api;


namespace SalesRead
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddApplication();
            builder.Services.AddApi();

            builder.Services.AddControllers();
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
            builder.Services.Configure<RappitMqConfiguration>(builder.Configuration.GetSection("RappitMqConfiguration"));
            builder.Services.AddHostedService<RabbitMqConsumerService>();

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
