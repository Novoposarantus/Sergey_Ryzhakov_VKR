using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Enums.Internal;
using Models.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleNames.Admin)]
    public class QuestionTypeController : ControllerBase
    {
        readonly IQuestionRepository _questionRepository;
        public QuestionTypeController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_questionRepository.Types);
        }

        [HttpPost]
        public IActionResult Post(QuestionTypeModel model)
        {
            return Ok(_questionRepository.SaveQuestionType(model.Name));    
        }
    }
}