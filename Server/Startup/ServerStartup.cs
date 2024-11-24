using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using SPSITECH.Module.FORMIO.Repository;
using SPSITECH.Module.FORMIO.Services;

namespace SPSITECH.Module.FORMIO.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFORMIOService, ServerFORMIOService>();
            services.AddDbContextFactory<FORMIOContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
