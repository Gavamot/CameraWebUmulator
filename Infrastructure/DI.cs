using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Config config)
        {
            services.Scan(scan =>
                scan.FromCallingAssembly()
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan =>
                scan.FromCallingAssembly()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Rep")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddConfig(config);

            return services;
        }

      

        static void AddConfig(this IServiceCollection services, Config config)
        {
            var interfaces = config.GetType().GetInterfaces();
            foreach (var face in interfaces)
            {
                services.AddSingleton(face, config);
            }
        }
    }
}
