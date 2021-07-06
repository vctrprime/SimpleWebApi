using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleWebApi.Domain.BreakingBad.Quotes;

namespace SimpleWebApi.DAL.Repositories.Abstract.BreakingBad
{
    public interface IQuoteRepository
    {
        Task<IEnumerable<Quote>> Get();

        Task<Quote> Get(int id);
    }
}