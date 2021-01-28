using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Weather.Api.Configuration;

namespace Weather.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

           Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(configuration:Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseApiConfiguration();
        }
    }
}
