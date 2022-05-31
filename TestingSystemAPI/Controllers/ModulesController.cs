using Common.Data.Content;
using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestingSystemAPI.Services;

namespace TestingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private IModuleService _moduleService;
        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        // GET: api/modules
        [HttpGet]
        [Authorize]
        public IEnumerable<Module> Get()
        {
            return _moduleService.GetModules();
        }

    }
}
