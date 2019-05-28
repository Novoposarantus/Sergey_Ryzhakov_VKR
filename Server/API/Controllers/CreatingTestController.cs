using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Enums.Internal;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleNames.Admin)]
    public class CreatingTestController : ControllerBase
    {
        readonly ITestRepository _testRepository;
        public CreatingTestController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public IActionResult Get()
        {
            return Ok(_testRepository.TestListItemDtos);
        }
    }
}