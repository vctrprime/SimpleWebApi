using System.Threading.Tasks;

namespace SimpleWebApi.DAL.Repositories.Abstract.Common
{
    public interface IAuthorizationRepository
    {
        Task<bool> IsValidUser(string name, string password);
    }
}