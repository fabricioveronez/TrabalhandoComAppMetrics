using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TrabalhandoComAppMetrics.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureMetricsWithDefaults(
                builder =>
                {
                    builder.Report.ToInfluxDb(options =>
                    {
                        options.InfluxDb.BaseUri = new System.Uri("http://localhost:8086");
                        options.InfluxDb.Database = "appmetrics";
                        options.InfluxDb.CreateDataBaseIfNotExists = true;
                        options.InfluxDb.UserName = "influxUser";
                        options.InfluxDb.Password = "influxPwd";
                    });                 
                })
            .UseMetrics()
            .UseStartup<Startup>();   
    }
}
