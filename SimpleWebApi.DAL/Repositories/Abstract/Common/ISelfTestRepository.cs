using System.Threading.Tasks;

namespace SimpleWebApi.DAL.Repositories.Abstract.Common
{
    public interface ISelfTestRepository
    {
        Task Test();
    }
}