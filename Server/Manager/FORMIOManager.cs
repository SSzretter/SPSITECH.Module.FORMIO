using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using SPSITECH.Module.FORMIO.Repository;
using System.Threading.Tasks;

namespace SPSITECH.Module.FORMIO.Manager
{
    public class FORMIOManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IFORMIORepository _FORMIORepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public FORMIOManager(IFORMIORepository FORMIORepository, IDBContextDependencies DBContextDependencies)
        {
            _FORMIORepository = FORMIORepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new FORMIOContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new FORMIOContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.FORMIO> FORMIOs = _FORMIORepository.GetFORMIOs(module.ModuleId).ToList();
            if (FORMIOs != null)
            {
                content = JsonSerializer.Serialize(FORMIOs);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.FORMIO> FORMIOs = null;
            if (!string.IsNullOrEmpty(content))
            {
                FORMIOs = JsonSerializer.Deserialize<List<Models.FORMIO>>(content);
            }
            if (FORMIOs != null)
            {
                foreach(var FORMIO in FORMIOs)
                {
                    _FORMIORepository.AddFORMIO(new Models.FORMIO { ModuleId = module.ModuleId, Name = FORMIO.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var FORMIO in _FORMIORepository.GetFORMIOs(pageModule.ModuleId))
           {
               if (FORMIO.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "SPSITECHFORMIO",
                       EntityId = FORMIO.FORMIOId.ToString(),
                       Title = FORMIO.Name,
                       Body = FORMIO.Name,
                       ContentModifiedBy = FORMIO.ModifiedBy,
                       ContentModifiedOn = FORMIO.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
