using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebApi.Domain.BreakingBad.Quotes;
using SimpleWebApi.DTO.BreakingBad;

namespace SimpleWebApi.WebApi.Controllers.BreakingBad
{
    public class QuoteController : BaseApiController
    {
        public QuoteController(IMapper mapper, ILogger<QuoteController> logger)
            : base(mapper, logger)
        {
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var quotes = new List<Quote>
                {
                    new Quote { Id = 1, Text = "text1", Author = "author1"},
                    new Quote { Id = 2, Text = "text2", Author = "author2"},
                };
                var response = Mapper.Map<GetQuotesResponseDto>(quotes);

                return Ok(response);
            }
            catch(Exception exception)
            {
                return BadRequestAction(exception, nameof(QuoteController));
            }
            
        }
    }
}