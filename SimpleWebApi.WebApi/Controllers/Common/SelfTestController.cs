using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebApi.DAL.Repositories.Abstract.Common;

namespace SimpleWebApi.WebApi.Controllers.Common
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class SelfTestController : BaseApiController
    {
        private readonly ISelfTestRepository _repository;
        
        public SelfTestController(IMapper mapper, ILogger<SelfTestController> logger, ISelfTestRepository repository) : base(mapper, logger)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _repository.Test();
                return Ok("Test successful!");
            }
            catch(Exception exception)
            {
                return BadRequestAction(exception, nameof(SelfTestController));
            }
        }
    }
}