using System.Threading.Tasks;
using SimpleWebApi.DAL.Repositories.Abstract.Common;

namespace SimpleWebApi.UnitTests.Repositories.Common
{
    public class AuthorizationRepositoryTest : IAuthorizationRepository
    {
        public async Task<bool> IsValidUser(string name, string password)
        {
            return name == BasicCredentials.ValidUsername && password == BasicCredentials.ValidPassword;
        }
    }
}