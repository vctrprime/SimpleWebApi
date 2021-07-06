using System.Threading.Tasks;
using Dapper;
using SimpleWebApi.DAL.Connection.Abstract;
using SimpleWebApi.DAL.Repositories.Abstract.Common;

namespace SimpleWebApi.DAL.Repositories.Concrete.Common
{
    public class SelfTestRepository : BaseRepository, ISelfTestRepository
    {
        public SelfTestRepository(IConnectionCreator connectionCreator) : base(connectionCreator)
        {
        }

        public async Task Test()
        {
            await ConnectionCreator.Connection.ExecuteAsync("SELECT 1");
        }
    }
}