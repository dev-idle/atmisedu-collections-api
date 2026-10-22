using Asp.Versioning.ApiExplorer;
using atMISEDU.Contract.Constants;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace atMISEDUatMISEDUAPI.DependencyInjection.Options
{
    /// <summary>
    /// Options for configure swagger
    /// </summary>
    public class SwaggerConfigureOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly ApiConfig apiConfig;
        private readonly IApiVersionDescriptionProvider provider;

        public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider, ApiConfig apiConfig)
        {
            this.provider = provider;
            this.apiConfig = apiConfig;
        }

        /// <summary>
        /// Configure swagger gen
        /// </summary>
        /// <param name="options">Options to configure</param>
        public void Configure(SwaggerGenOptions options)
        {
            // Iterate through API version descriptions
            foreach (var description in provider.ApiVersionDescriptions)
            {
                // Create OpenAPI info
                var apiInfo = new OpenApiInfo
                {
                    Title = apiConfig.Name,
                    Version = description.ApiVersion.ToString(),
                    Description = description.IsDeprecated ? "This version was deprecated." : null
                };
                // Add Swagger document
                options.SwaggerDoc(description.GroupName, apiInfo);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Nhập mã thông báo JWT vào đây theo định dạng 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                });
            }
        }

        /// <summary>
        /// Configure swagger gen
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}