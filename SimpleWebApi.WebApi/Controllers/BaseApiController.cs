using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SimpleWebApi.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    //[BasicAuthorize]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;

        /// <inheritdoc />
        public BaseApiController(IMapper mapper, ILogger logger)
        {
            Mapper = mapper;
            Logger = logger;
        }
        
        protected IActionResult BadRequestAction(Exception exception, string controllerName)
        {
            //Logger.LogError(exception, controllerName);
            return BadRequest(exception.Message);
        }
    }
}