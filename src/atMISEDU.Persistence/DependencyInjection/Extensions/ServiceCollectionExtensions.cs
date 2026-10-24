using atMISEDU.Contract.Constants;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.CATALOG;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Abstractions.Repositories.Sql.HUBS;
using atMISEDU.Persistence.Repositories.Sql.Base;
using atMISEDU.Persistence.Repositories.Sql.CATALOG;
using atMISEDU.Persistence.Repositories.Sql.COLLECTIONS;
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

            #region COLLECTIONS

            services.AddScoped<ICollectionArticlesSqlRepository, CollectionArticlesSqlRepository>();
            services.AddScoped<ICollectionArticlesAwardSqlRepository, CollectionArticlesAwardSqlRepository>();
            services.AddScoped<ICollectionArticlesDiscussionSqlRepository, CollectionArticlesDiscussionSqlRepository>();
            services.AddScoped<ICollectionArticlesDiscussionAwardSqlRepository, CollectionArticlesDiscussionAwardSqlRepository>();
            services.AddScoped<ICollectionArticlesDiscussionInteractionSqlRepository, CollectionArticlesDiscussionInteractionSqlRepository>();
            services.AddScoped<ICollectionArticlesInteractionSqlRepository, CollectionArticlesInteractionSqlRepository>();
            services.AddScoped<ICollectionArticlesTagsSqlRepository, CollectionArticlesTagsSqlRepository>();
            services.AddScoped<ICollectionArticlesVoteSqlRepository, CollectionArticlesVoteSqlRepository>();
            services.AddScoped<ICollectionAwardCatalogSqlRepository, CollectionAwardCatalogSqlRepository>();
            services.AddScoped<ICollectionTagsSqlRepository, CollectionTagsSqlRepository>();

            #endregion

            return services;
        }
    }
}
