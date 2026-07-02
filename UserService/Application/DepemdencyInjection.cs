using System.Reflection;
using FluentValidation;
using UserService.Application.Behavouir;

namespace UserService.Application
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
            services.AddMemoryCache(options => options.SizeLimit = 100);

            return services;
        }
    }
}
