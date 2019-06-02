using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Enums.Internal;
using Models.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleNames.Admin)]
    public class QuestionsController : ControllerBase
    {
        readonly IQuestionRepository _questionRepository;
        public QuestionsController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_questionRepository.QuestionListItemDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_questionRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Post(QuestionModel question)
        {
            _questionRepository.Save(question);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(QuestionModel question)
        {
            _questionRepository.Update(question);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Put(int id)
        {
            _questionRepository.Delete(id);
            return Ok();
        }
    }
}