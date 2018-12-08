using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using App.Metrics.Health.Extensions.Hosting;

namespace TrabalhandoComAppMetrics.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var metrics = new MetricsBuilder()
                                .Report.ToInfluxDb(options =>
                                {
                                    options.InfluxDb.BaseUri = new Uri("http://127.0.0.1:8086");
                                    options.InfluxDb.Database = "appmetrics";                                    
                                    options.InfluxDb.UserName = "influxUser";
                                    options.InfluxDb.Password = "influxPwd";
                                })
                                .Build();

            services.AddMetrics(metrics);
            services.AddMetricsReportingHostedService();
            services.AddMetricsTrackingMiddleware();

            services.AddMvc().AddMetrics();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHsts();
            }

            app.UseMetricsAllMiddleware();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
