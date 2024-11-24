using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPSITECH.Module.FORMIO.Repository
{
    public interface IFORMIORepository
    {
        IEnumerable<Models.FORMIO> GetFORMIOs(int ModuleId);
        Models.FORMIO GetFORMIO(int FORMIOId);
        Models.FORMIO GetFORMIO(int FORMIOId, bool tracking);
        Models.FORMIO AddFORMIO(Models.FORMIO FORMIO);
        Models.FORMIO UpdateFORMIO(Models.FORMIO FORMIO);
        void DeleteFORMIO(int FORMIOId);
    }
}
