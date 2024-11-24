using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Shared;
using System.Collections.Generic;

namespace SPSITECH.Module.FORMIO
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "FORMIO",
            Description = "testing formio",
            Version = "1.0.0",
            ServerManagerType = "SPSITECH.Module.FORMIO.Manager.FORMIOManager, SPSITECH.Module.FORMIO.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "SPSITECH.Module.FORMIO.Shared.Oqtane",
            PackageName = "SPSITECH.Module.FORMIO",
            Resources = new List<Resource>() // load these resources globally so they cache
            {
                new Resource { ResourceType = ResourceType.Stylesheet, Url =  "~/Module.css" },
                new Resource { ResourceType = ResourceType.Script, Url =  "~/Module.js" },
                new Resource { ResourceType = ResourceType.Stylesheet, Url = "https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" },
                new Resource { ResourceType = ResourceType.Stylesheet, Url = "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" },
                
            }
        };
    }
}
