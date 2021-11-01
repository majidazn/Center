using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;


namespace Center.WebApi.Framework.Configuration.Swagger
{
    public static class SwaggerConfigurationExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //options.DocExpanSion(); 
                //options.EnableAnnotations();
                //options.DescribeAllEnumsAsStrings();
                options.IgnoreObsoleteActions();
                //options.IgnoreObsoleteProperties();

                options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Center APIs V1" });

                #region Filters
                //Add Lockout icon on top of swagger ui page to authenticate
                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "bearer");

                #endregion
            });
        }

    }
}
