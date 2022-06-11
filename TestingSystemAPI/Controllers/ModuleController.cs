using Common.Data.Content;
using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingSystemAPI.Services;

namespace TestingSystemAPI.Models
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ModuleController
    {
        private IModuleService _moduleService;
        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet]
        public ModuleInfoDTO Get(Guid id)
        {
            return _moduleService.GetModuleInfo(id);
        }

        [HttpGet]
        public LessonInfoDTO GetLessonInfo(Guid moduleId, Guid id)
        {
            return _moduleService.GetLessonInfo(moduleId, id);
        }
    }
}
