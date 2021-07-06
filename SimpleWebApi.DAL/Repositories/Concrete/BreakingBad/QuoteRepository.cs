using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWebApi.DAL.Connection.Abstract;
using SimpleWebApi.DAL.Repositories.Abstract.BreakingBad;
using SimpleWebApi.Domain.BreakingBad.Quotes;

namespace SimpleWebApi.DAL.Repositories.Concrete.BreakingBad
{
    public class QuoteRepository : BaseRepository, IQuoteRepository
    {
        private readonly List<Quote> quotes;
        
        public QuoteRepository(IConnectionCreator connectionCreator) : base(connectionCreator)
        {
            quotes = new List<Quote>
            {
                new Quote { Id = 1, Text = "text1", Author = "author1"},
                new Quote { Id = 2, Text = "text2", Author = "author2"},
            };
        }

        public async Task<IEnumerable<Quote>> Get()
        {
            return quotes;
        }

        public async Task<Quote> Get(int id)
        {
            return quotes.FirstOrDefault(q => q.Id == id);
        }
    }
}