using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace SPSITECH.Module.FORMIO.Services
{
    public class FORMIOService : ServiceBase, IFORMIOService
    {
        public FORMIOService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("FORMIO");

        public async Task<List<Models.FORMIO>> GetFORMIOsAsync(int ModuleId)
        {
            List<Models.FORMIO> FORMIOs = await GetJsonAsync<List<Models.FORMIO>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.FORMIO>().ToList());
            return FORMIOs.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.FORMIO> GetFORMIOAsync(int FORMIOId, int ModuleId)
        {
            return await GetJsonAsync<Models.FORMIO>(CreateAuthorizationPolicyUrl($"{Apiurl}/{FORMIOId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.FORMIO> AddFORMIOAsync(Models.FORMIO FORMIO)
        {
            return await PostJsonAsync<Models.FORMIO>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, FORMIO.ModuleId), FORMIO);
        }

        public async Task<Models.FORMIO> UpdateFORMIOAsync(Models.FORMIO FORMIO)
        {
            return await PutJsonAsync<Models.FORMIO>(CreateAuthorizationPolicyUrl($"{Apiurl}/{FORMIO.FORMIOId}", EntityNames.Module, FORMIO.ModuleId), FORMIO);
        }

        public async Task DeleteFORMIOAsync(int FORMIOId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{FORMIOId}", EntityNames.Module, ModuleId));
        }
    }
}
