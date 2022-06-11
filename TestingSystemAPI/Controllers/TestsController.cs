using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingSystemAPI.Models;
using TestingSystemAPI.Services;

namespace TestingSystemAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private IModuleService _moduleService;
        public TestsController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet]
        public IEnumerable<TestDTO> Get(Guid moduleId, Guid id)
        {
            return _moduleService.GetTheoryTests(moduleId, id);
        }

        [HttpPost]
        public int Check(Guid moduleId, Guid lessonId, [FromBody] AnswerDTO[] answers)
        {
            return _moduleService.CheckTests(moduleId, lessonId, answers);
        }


    }
}
