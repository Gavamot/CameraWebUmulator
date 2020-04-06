using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public static class DI
    {
        public static IServiceCollection AddApp(this IServiceCollection services)
        {
            services.Scan(scan =>
                scan.FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());

            services.Scan(scan =>
                scan.FromCallingAssembly()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Rep")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
