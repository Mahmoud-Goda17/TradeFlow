using FluentValidation;
using ProductSercice.Application.Behavouir;
using System.Reflection;

namespace ProductSercice.Application
{
    public static class DepemdencyInjection
    {
        public static IServiceCollection AddApplicationProducts(this IServiceCollection services)
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
