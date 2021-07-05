using SimpleWebApi.DAL.SqlQueries.Abstract;

namespace SimpleWebApi.DAL.SqlQueries.Concrete.Common
{
    public class GetUsernameByNameAndPasswordSqlQuery : ISqlQuery
    {
        public string Value => @"SELECT name FROM user 
                                 WHERE name = :nameParameter AND password = :passwordParameter";
    }
}