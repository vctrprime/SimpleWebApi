using System.Collections.Generic;
using AutoMapper;
using SimpleWebApi.Domain.BreakingBad.Quotes;
using SimpleWebApi.DTO.BreakingBad;

namespace SimpleWebApi.Profiles.BreakingBad
{
    public class QuoteProfile : Profile
    {
        public QuoteProfile()
        {
            CreateMap<IEnumerable<Quote>, GetQuotesResponseDto>()
                .ForMember(dto => dto.Result, 
                    act => act.MapFrom(m => m));
        }
    }
}