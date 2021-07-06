using SimpleWebApi.DAL.SqlQueries.Concrete.BreakingBad;

namespace SimpleWebApi.DAL.SqlQueries.ForRepositories.BreakingBad
{
    public class QuoteRepositorySqlQueries
    {
        public GetAllQuotesSqlQuery GetAllQuotesSqlQuery { get; set; }

        public GetQuoteSqlQuery GetQuoteSqlQuery { get; set; }
        
    }
}