using CloudNautical.DataAccess.ADO.Repository;
using CloudNautical.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudNautical.Infrastructure.Common.DependancyContainer
{
    public static class DependancyContainer
    {
        public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionString").Get<string>();
            services.AddScoped(provider => new OrderRepository(connectionString));
            services.AddScoped<DeliveryService>();
        }
    }
}
