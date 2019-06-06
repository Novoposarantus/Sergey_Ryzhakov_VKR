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
    public class CreatingTestController : ControllerBase
    {
        readonly ITestRepository _testRepository;
        readonly IQuestionRepository _questionRepository;
        public CreatingTestController(ITestRepository testRepository, IQuestionRepository questionRepository)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
        }
        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult Get()
        {
            return Ok(_testRepository.TestListItemDtos);
        }

        [HttpGet("AllQuestions")]
        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult AllQuestions()
        {
            return Ok(_questionRepository.Questions);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult Get(int id)
        {
            return Ok(_testRepository.Get(id));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult Post(SaveTestDto question)
        {
            _testRepository.Save(question);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult Put(SaveTestDto question)
        {
            _testRepository.Update(question);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult Delete(int id)
        {
            _testRepository.Delete(id);
            return Ok();
        }
    }
}