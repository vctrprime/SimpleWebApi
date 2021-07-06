using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SimpleWebApi.DAL.Connection.Abstract;
using SimpleWebApi.DAL.Repositories.Abstract.BreakingBad;
using SimpleWebApi.DAL.SqlQueries.ForRepositories.BreakingBad;
using SimpleWebApi.Domain.BreakingBad.Quotes;

namespace SimpleWebApi.DAL.Repositories.Concrete.BreakingBad
{
    public class QuoteRepository : BaseRepository, IQuoteRepository
    {
        private readonly QuoteRepositorySqlQueries _sqlQueries;
        
        public QuoteRepository(IConnectionCreator connectionCreator, QuoteRepositorySqlQueries sqlQueries) : base(connectionCreator)
        {
            _sqlQueries = sqlQueries;
        }

        public async Task<IEnumerable<Quote>> Get()
        {
            var quotes = await ConnectionCreator.Connection.QueryAsync<Quote>(_sqlQueries.GetAllQuotesSqlQuery.Value);
            return quotes;
        }

        public async Task<Quote> Get(int id)
        {
            var quote = await ConnectionCreator.Connection.QueryFirstOrDefaultAsync<Quote>(_sqlQueries.GetQuoteSqlQuery.Value,
                new
                {
                    idParameter = id
                });
            return quote;
        }
    }
}