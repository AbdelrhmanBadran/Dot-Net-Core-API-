using Microsoft.OpenApi.Models;
namespace ECommerce.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiDemo", Version = "v1" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Jwt Authorization header using the bearer scheme. Example: Authorization : Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                };

                c.AddSecurityDefinition("bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securityScheme , new[] { "bearer"} }
                };

                c.AddSecurityRequirement(securityRequirement);
            });


            return services;
        }
    }
}
