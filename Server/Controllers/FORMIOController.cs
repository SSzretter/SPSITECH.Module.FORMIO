using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using SPSITECH.Module.FORMIO.Repository;
using Oqtane.Controllers;
using System.Net;

namespace SPSITECH.Module.FORMIO.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class FORMIOController : ModuleControllerBase
    {
        private readonly IFORMIORepository _FORMIORepository;

        public FORMIOController(IFORMIORepository FORMIORepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _FORMIORepository = FORMIORepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.FORMIO> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _FORMIORepository.GetFORMIOs(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FORMIO Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.FORMIO Get(int id)
        {
            Models.FORMIO FORMIO = _FORMIORepository.GetFORMIO(id);
            if (FORMIO != null && IsAuthorizedEntityId(EntityNames.Module, FORMIO.ModuleId))
            {
                return FORMIO;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FORMIO Get Attempt {FORMIOId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.FORMIO Post([FromBody] Models.FORMIO FORMIO)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, FORMIO.ModuleId))
            {
                FORMIO = _FORMIORepository.AddFORMIO(FORMIO);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "FORMIO Added {FORMIO}", FORMIO);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FORMIO Post Attempt {FORMIO}", FORMIO);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                FORMIO = null;
            }
            return FORMIO;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.FORMIO Put(int id, [FromBody] Models.FORMIO FORMIO)
        {
            if (ModelState.IsValid && FORMIO.FORMIOId == id && IsAuthorizedEntityId(EntityNames.Module, FORMIO.ModuleId) && _FORMIORepository.GetFORMIO(FORMIO.FORMIOId, false) != null)
            {
                FORMIO = _FORMIORepository.UpdateFORMIO(FORMIO);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "FORMIO Updated {FORMIO}", FORMIO);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FORMIO Put Attempt {FORMIO}", FORMIO);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                FORMIO = null;
            }
            return FORMIO;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.FORMIO FORMIO = _FORMIORepository.GetFORMIO(id);
            if (FORMIO != null && IsAuthorizedEntityId(EntityNames.Module, FORMIO.ModuleId))
            {
                _FORMIORepository.DeleteFORMIO(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "FORMIO Deleted {FORMIOId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FORMIO Delete Attempt {FORMIOId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
