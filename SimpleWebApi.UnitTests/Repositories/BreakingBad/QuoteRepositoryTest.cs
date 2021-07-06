using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWebApi.DAL.Repositories.Abstract.BreakingBad;
using SimpleWebApi.Domain.BreakingBad.Quotes;

namespace SimpleWebApi.UnitTests.Repositories.BreakingBad
{
    public class QuoteRepositoryTest : IQuoteRepository
    {
        private readonly List<Quote> quotes;

        public QuoteRepositoryTest()
        {
            quotes = new List<Quote>
            {
                new Quote { Id = 1, Text = "test1", Author = "author1"},
                new Quote { Id = 2, Text = "test2", Author = "author2"},
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