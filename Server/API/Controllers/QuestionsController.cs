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
        public IActionResult Get()
        {
            return Ok(_questionRepository.Questions);
        }
    }
}