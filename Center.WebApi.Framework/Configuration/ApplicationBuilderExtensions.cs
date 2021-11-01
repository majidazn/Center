using Center.DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Center.WebApi.Framework.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static void IntializeDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CenterBoundedContextCommand>(); //Service locator

                //Applies any pending migrations for the context to the database like (Update-Database)
                dbContext.Database.Migrate();

            }
        }
    }
}
