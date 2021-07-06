using SimpleWebApi.DAL.SqlQueries.Abstract;

namespace SimpleWebApi.DAL.SqlQueries.Concrete.BreakingBad
{
    public class GetAllQuotesSqlQuery : ISqlQuery
    {
        public string Value => @"SELECT id, text, author FROM quote";
    }
}