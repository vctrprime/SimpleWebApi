using SimpleWebApi.DAL.Connection.Abstract;

namespace SimpleWebApi.DAL.Repositories.Concrete
{
    public class BaseRepository
    {
        protected readonly IConnectionCreator ConnectionCreator;

        protected BaseRepository(IConnectionCreator connectionCreator)
        {
            ConnectionCreator = connectionCreator;
        }
    }
}