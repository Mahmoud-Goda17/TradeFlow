using FluentValidation;
using InventoryService.Application.Behavouir;
using System.Reflection;

namespace InventoryService.Application
{
    public static class DepemdencyInjection
    {
        public static IServiceCollection AddApplicationInventory(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(ct =>
            {
                ct.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                ct.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddMemoryCache(options => options.SizeLimit = 100);

            return services;
        }

    }
}
