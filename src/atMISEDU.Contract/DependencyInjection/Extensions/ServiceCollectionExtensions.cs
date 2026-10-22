using atMISEDU.Contract.Abstractions;
using atMISEDU.Contract.Constants;
using atMISEDU.Contract.DTOs;
using atMISEDU.Contract.Helpers;
using atMISEDU.Contract.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace atMISEDU.Contract.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add <see cref="EnvironmentHelper"/>, have easily way to get <see cref="IWebHostEnvironment"/>
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IWebHostEnvironment AddEnvironmentHelper(this IWebHostEnvironment env)
        {
            EnvironmentHelper.Environment = env;
            return env;
        }

        public static IServiceCollection AddConfigSetting(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UploadedSettingsDTOs>(configuration.GetSection(Const.UPLOADED_SETTINGS));
            services.Configure<DomainHostsDTOs>(configuration.GetSection(Const.DOMAIN_HOSTS));

            services.AddPasswordHasher();

            return services;
        }

        /// <summary>
        /// Password hasher using <see cref="BCrypt"/> to encrypt and validate password.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPasswordHasher(this IServiceCollection services)
        {
            return services.AddScoped<IPasswordHasher, PasswordHasher>();
        }
    }
}