using atMISEDU.Contract.Constants;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.CATALOG;
using atMISEDU.Domain.Abstractions.Repositories.Sql.HUBS;
using atMISEDU.Persistence.Repositories.Sql.Base;
using atMISEDU.Persistence.Repositories.Sql.CATALOG;
using atMISEDU.Persistence.Repositories.Sql.HUBS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace atMISEDU.Persistence.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register infrastructure EF services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Application configuration</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString(Const.CONN_CONFIG_SQL);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.RegisterServices();
            return services;
        }

        /// <summary>
        /// Registering infrastructure ef services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Service collection</returns>
        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericSqlRepository<,>), typeof(GenericSqlRepository<,>));
            services.AddScoped<ISqlUnitOfWork, SqlUnitOfWork>();

            #region CATALOG

            services.AddScoped<ICatalogUnitSqlRepository, CatalogUnitSqlRepository>();

            #endregion

            #region HUBS

            services.AddScoped<IHubsChannelSqlRepository, HubsChannelSqlRepository>();

            #endregion

            return services;
        }
    }
}
