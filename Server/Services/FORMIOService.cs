using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using SPSITECH.Module.FORMIO.Repository;

namespace SPSITECH.Module.FORMIO.Services
{
    public class ServerFORMIOService : IFORMIOService
    {
        private readonly IFORMIORepository _FORMIORepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerFORMIOService(IFORMIORepository FORMIORepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _FORMIORepository = FORMIORepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.FORMIO>> GetFORMIOsAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_FORMIORepository.GetFORMIOs(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FORMIO Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.FORMIO> GetFORMIOAsync(int FORMIOId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_FORMIORepository.GetFORMIO(FORMIOId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FORMIO Get Attempt {FORMIOId} {ModuleId}", FORMIOId, ModuleId);
                return null;
            }
        }

        public Task<Models.FORMIO> AddFORMIOAsync(Models.FORMIO FORMIO)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, FORMIO.ModuleId, PermissionNames.Edit))
            {
                FORMIO = _FORMIORepository.AddFORMIO(FORMIO);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "FORMIO Added {FORMIO}", FORMIO);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FORMIO Add Attempt {FORMIO}", FORMIO);
                FORMIO = null;
            }
            return Task.FromResult(FORMIO);
        }

        public Task<Models.FORMIO> UpdateFORMIOAsync(Models.FORMIO FORMIO)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, FORMIO.ModuleId, PermissionNames.Edit))
            {
                FORMIO = _FORMIORepository.UpdateFORMIO(FORMIO);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "FORMIO Updated {FORMIO}", FORMIO);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FORMIO Update Attempt {FORMIO}", FORMIO);
                FORMIO = null;
            }
            return Task.FromResult(FORMIO);
        }

        public Task DeleteFORMIOAsync(int FORMIOId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _FORMIORepository.DeleteFORMIO(FORMIOId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "FORMIO Deleted {FORMIOId}", FORMIOId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FORMIO Delete Attempt {FORMIOId} {ModuleId}", FORMIOId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
