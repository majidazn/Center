using System;
using System.Net.Http;
using Center.Common.Api;
using Center.Common.Enums;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Center.WebApi.Framework.Configuration
{
    public static class HttpClientBuilderConfiguration
    {
        public static void AddNamedHttpClientServices(this IServiceCollection services, SiteSettings siteSettings)
        {
            //https://github.com/App-vNext/Polly/wiki/Polly-and-HttpClientFactory

            var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>() // thrown by Polly's TimeoutPolicy if the inner call times out
            .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(20),
                    TimeSpan.FromSeconds(30)
                }, (exception, timeSpan, retryCount, context) =>
                {
                    // do something   
                });

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(100); // Timeout for an individual try

            services.AddHttpClient(HttpBaseApiUrlsType.SecurityWebApi.ToString(), c =>
            {
                c.BaseAddress = new Uri(siteSettings.HttpBaseUrls.SecurityWebApi);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            //.AddPolicyHandler(retryPolicy)
            //.AddPolicyHandler(timeoutPolicy)
            //.AddPolicyHandler(GetCircuitBreakerPolicy())
             .AddHeaderPropagation();

        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));
        }
    }

}
