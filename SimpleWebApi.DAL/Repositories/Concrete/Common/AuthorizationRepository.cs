using System.Threading.Tasks;
using Dapper;
using SimpleWebApi.DAL.Connection.Abstract;
using SimpleWebApi.DAL.Repositories.Abstract.Common;
using SimpleWebApi.DAL.SqlQueries.ForRepositories.Common;

namespace SimpleWebApi.DAL.Repositories.Concrete.Common
{
    public class AuthorizationRepository : BaseRepository, IAuthorizationRepository
    {
        private readonly AuthorizationRepositorySqlQueries _sqlQueries;

        public AuthorizationRepository(IConnectionCreator connectionCreator, AuthorizationRepositorySqlQueries sqlQueries) 
            : base(connectionCreator)
        {
            _sqlQueries = sqlQueries;
        }
        
        public async Task<bool> IsValidUser(string name, string password)
        {
            try
            {
                var user = await ConnectionCreator.Connection.QueryFirstOrDefaultAsync<string>(
                    _sqlQueries.GetUsernameByNameAndPasswordSqlQuery.Value, new
                    {
                        nameParameter = name,
                        passwordParameter = password
                    });

                return !string.IsNullOrEmpty(user);
            }
            catch
            {
                return false;
            }
            
        }
    }
}