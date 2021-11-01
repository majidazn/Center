using System;
using System.Net.Http;
using Autofac;
using Center.ApplicationServices.Activity.Serices;
using Center.ApplicationServices.Center.Services;
using Center.ApplicationServices.CenterVariable.Services;
using Center.ApplicationServices.CommonServices.AccountService;
using Center.ApplicationServices.CommonServices.GeneralVariableServices;
using Center.Common.Api;
using Center.DataAccess.Context;
using Center.DataAccess.Infrastrutures.Strategies;
using Center.WebApi.Framework.Configuration;
using Center.WebApi.Framework.Infrastrutures;
using Core.Common.Enums;
using DotNetCore.CAP;
using Framework.Auditing;
using Framework.Configurations;
using Framework.SecurityHeaders;
using Framework.TenantConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;

namespace Center.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

        }
        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            ConfigureClientFactoryClients(services);

            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("Authorization");
            });

            // services.AddNamedHttpClientServices(_siteSetting);
            services.AddDynamicPermissionsPolicy();
            services.AddDbContext(Configuration);
            services.IdentitySetup();
            services.SwaggerSetup();
            services.CorsSetup("AllowedHosts");
            services.IpLimitingSetup(Configuration);
            services.AddControllersWithViews((options) =>
            {
                options.EnableEndpointRouting = false;
            }).AddJsonOptions(jsonOptions =>
               {
                   jsonOptions.JsonSerializerOptions.IgnoreNullValues = true;

               }).AddNewtonsoftJson(jsonOptions =>
               {
                   jsonOptions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
               });

            //services.AddLocalntegrationEvent();
            services.AddAuditing();
            services.AddInMemoryCachingSingleton();
            services.AddScoped<ICenterService, CenterService>();
            services.AddScoped<ICenterVariableService, CenterVariableService>();
            services.AddScoped<IInternalUsageRules, InternalUsageRules>();
            services.AddScoped<IActivityService, ActivityService>();

            services.AddScoped<IAccountServices, AccountServices>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IHostNameProvider, HostNameProvider>();

       //     services.AddHostedService<StartupService>();

            //services.AddMassTransit(x =>
            //{
            //    x.AddConsumers(Assembly.GetExecutingAssembly());
            //    x.SetKebabCaseEndpointNameFormatter();
            //    x.UsingRabbitMq((context, cfg) =>
            //    {
            //        cfg.ConfigureEndpoints(context);
            //        cfg.Host(_siteSetting.MassTransitConfig.Host, _siteSetting.MassTransitConfig.Port, _siteSetting.MassTransitConfig.VirtualHost,
            //            h =>
            //            {
            //                h.Username(_siteSetting.MassTransitConfig.Username);
            //                h.Password(_siteSetting.MassTransitConfig.Password);
            //            }
            //        );
            //    });
            //});

            //services.AddMassTransitHostedService();

            services.AddCap(x =>
            {
                x.FailedRetryCount = _siteSetting.CapConfig.FailedRetryCount;
                x.FailedRetryInterval = _siteSetting.CapConfig.FailedRetryInterval;
                x.SucceedMessageExpiredAfter = _siteSetting.CapConfig.SucceedMessageExpiredAfter;
                x.UseEntityFramework<CenterBoundedContextCommand>();
                x.UseRabbitMQ(o =>
                {
                    o.HostName = _siteSetting.CapConfig.RabbitMQ.Host;
                    o.Port = _siteSetting.CapConfig.RabbitMQ.Port;
                    o.UserName = _siteSetting.CapConfig.RabbitMQ.Username;
                    o.Password = _siteSetting.CapConfig.RabbitMQ.Password;
                });
                x.UseDashboard();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostNameProvider hostProvider, IGeneralVariableServices gvServices)
        {
            app.UseHeaderPropagation();

            if (hostProvider.IsSSL() && env.IsProduction())
                Hsts.Configurations(app);

            else if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerCustom();
            }

            app.UseCustomExceptionHandler();  //dont displace
            app.IntializeDatabase();
            app.UseHttpsRedirection();

            //app.IntializeLocalIntegrationEventDatabase();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("AllowedHosts");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerCustom();
            //app.UseBanBlockedIps();
            app.UseIpRateLimitingCustom();
            //app.UseAntiforgeryToken();
            app.UseCapDashboard();

            NoCacheHttpHeaders.Configurations(app);
            RedirectValidation.Configurations(app);
            Xframe.Configurations(app);
            Xxss.Configurations(app);
            XContentType.Configurations(app);
            XDownloadOptions.Configurations(app);
            XRobotTag.Configurations(app);
            Csp.Configurations(app, hostProvider.IsSSL());


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureClientFactoryClients(IServiceCollection services)
        {
            services.AddHttpClient(HttpClientNameType.SecurityWebApi.ToString(), c =>
            {
                c.BaseAddress = new Uri(_siteSetting.HttpBaseUrls.SecurityWebApi);
            })
             //.AddPolicyHandler(RetryPolicy)
             //.AddPolicyHandler(TimeoutPolicy)
             //.AddPolicyHandler(CircuitBreakerPolicy)
             .AddHeaderPropagation();

            services.AddHttpClient(HttpClientNameType.VariableWebApi.ToString(), c =>
            {
                c.BaseAddress = new Uri(_siteSetting.HttpBaseUrls.GeneralVariableWebApi);
            });
            //.AddPolicyHandler(RetryPolicy)
            //.AddPolicyHandler(TimeoutPolicy)
            //.AddPolicyHandler(CircuitBreakerPolicy)
            //.AddHeaderPropagation();

        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            builder.RegisterModule(new WebSetupDependency());
        }
        private IAsyncPolicy<HttpResponseMessage> TimeoutPolicy
        {
            get
            {
                return Policy.TimeoutAsync<HttpResponseMessage>(100);
            }
        }
        private IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy
        {
            get
            {
                return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
            }
        }

        private AsyncRetryPolicy<HttpResponseMessage> RetryPolicy
        {
            get
            {
                return HttpPolicyExtensions
               .HandleTransientHttpError()
               .Or<TimeoutRejectedException>() // thrown by Polly's TimeoutPolicy if the inner call times out
               .WaitAndRetryAsync(1, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            }
        }
    }
}
