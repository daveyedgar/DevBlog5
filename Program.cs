using DevBlog5.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace DevBlog5
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();
            var dataService = host.Services.CreateScope().ServiceProvider.GetRequiredService<DataService>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // DataService service runs migrations and seeds database
            await dataService.ManageDataAsync();



            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
