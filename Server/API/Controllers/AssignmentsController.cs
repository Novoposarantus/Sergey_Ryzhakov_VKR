using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Models.DtoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        readonly ITestRepository _testRepository;
        readonly IAssignmentsRepository _assignmentsRepository;
        readonly IUserRepository _userRepository;
        public AssignmentsController(
            ITestRepository testRepository, 
            IAssignmentsRepository assignmentsRepository,
            IUserRepository userRepository)
        {
            _testRepository = testRepository;
            _assignmentsRepository = assignmentsRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new AssignmentsPageDto(
                _testRepository.Tests,
                _assignmentsRepository.Assignments,
                _userRepository.Users));
        }

        [HttpPost]
        public IActionResult Post(SaveAssignmentsDto dto)
        {
            return Ok(_assignmentsRepository.Save(dto));
        }
    }
}