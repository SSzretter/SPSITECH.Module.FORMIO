using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPSITECH.Module.FORMIO.Services
{
    public interface IFORMIOService 
    {
        Task<List<Models.FORMIO>> GetFORMIOsAsync(int ModuleId);

        Task<Models.FORMIO> GetFORMIOAsync(int FORMIOId, int ModuleId);

        Task<Models.FORMIO> AddFORMIOAsync(Models.FORMIO FORMIO);

        Task<Models.FORMIO> UpdateFORMIOAsync(Models.FORMIO FORMIO);

        Task DeleteFORMIOAsync(int FORMIOId, int ModuleId);
    }
}
