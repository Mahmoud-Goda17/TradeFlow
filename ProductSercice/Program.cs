
using System.Text.Json.Serialization;
using ProductSercice.Application.Interfaces;
using ProductSercice.InfraStrucure.Data;
using ProductSercice.InfraStrucure.Data.Interceptor;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application;
using ProductSercice.Api;

namespace ProductSercice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationProducts();
            builder.Services.ProductAddApi();

            builder.Services.AddControllers()
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

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


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
