using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<Quote> quotes;
        
        public QuoteController(IMapper mapper, ILogger<QuoteController> logger)
            : base(mapper, logger)
        {
            quotes = new List<Quote>
            {
                new Quote { Id = 1, Text = "text1", Author = "author1"},
                new Quote { Id = 2, Text = "text2", Author = "author2"},
            };
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = Mapper.Map<GetQuotesResponseDto>(quotes);

                return Ok(response);
            }
            catch(Exception exception)
            {
                return BadRequestAction(exception, nameof(QuoteController));
            }
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var quote = quotes.FirstOrDefault(q => q.Id == id);
                if (quote is null) return NotFound();
                
                return Ok(quote);
            }
            catch(Exception exception)
            {
                return BadRequestAction(exception, nameof(QuoteController));
            }
        }
    }
}