using Microsoft.Extensions.DependencyInjection;

namespace Center.WebApi.Framework.Configuration
{
    public static class CorsPolicyBuilderConfiguration
    {
        public static void AddCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy",
                    builder =>
                    {
                        //builder.WithOrigins(
                        //    siteSetting.HttpBaseUrls.FinanceWebApi,
                        //    siteSetting.HttpBaseUrls.FinanceWebApiGateway)
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });

            });

        }
    }

}
