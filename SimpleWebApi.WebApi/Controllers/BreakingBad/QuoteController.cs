using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebApi.DAL.Repositories.Abstract.BreakingBad;
using SimpleWebApi.Domain.BreakingBad.Quotes;
using SimpleWebApi.DTO.BreakingBad;

namespace SimpleWebApi.WebApi.Controllers.BreakingBad
{
    public class QuoteController : BaseApiController
    {
        private readonly IQuoteRepository _repository;
        
        public QuoteController(IMapper mapper, ILogger<QuoteController> logger, IQuoteRepository repository)
            : base(mapper, logger)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Quote> quotes = await _repository.Get();
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
                Quote quote = await _repository.Get(id);
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