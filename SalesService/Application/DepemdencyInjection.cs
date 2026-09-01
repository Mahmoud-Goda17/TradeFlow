using FluentValidation;
using SalesService.Application.Behavouir;
using System.Reflection;

namespace SalesService.Application
{
    public static class DepemdencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(ct =>
            {
                ct.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                ct.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            return services;
        }
    }
}
