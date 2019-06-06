using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;
using Models.Enums.Internal;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleNames.User)]
    public class TestWorkController : ControllerBase
    {
        readonly ITestRepository _testRepository;
        public TestWorkController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        [HttpGet("{assignmentId}")]
        public IActionResult Get(int assignmentId)
        {
            return Ok(_testRepository.GetTestToWork(assignmentId));
        }

        [HttpPost]
        public IActionResult Post(TestAnswersDto dto)
        {
            _testRepository.SaveTestResult(dto);
            return Ok();
        }
    }
}