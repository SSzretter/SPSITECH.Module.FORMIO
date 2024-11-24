using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using SPSITECH.Module.FORMIO.Services;

namespace SPSITECH.Module.FORMIO.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFORMIOService, FORMIOService>();
        }
    }
}
