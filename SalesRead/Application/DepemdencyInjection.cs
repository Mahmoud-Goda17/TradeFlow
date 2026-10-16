using System.Reflection;

namespace SalesRead.Application
{
    public static class DepemdencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(ct =>
            {
                ct.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddMemoryCache(options => options.SizeLimit = 100);

            return services;
        }

    }
}
