using System.Data;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using SimpleWebApi.DAL.Config;
using SimpleWebApi.DAL.Connection.Abstract;

namespace SimpleWebApi.DAL.Connection.Concrete
{
    public class ConnectionCreator : IConnectionCreator
    {
        private readonly DbConfig _dbConfig;
        private DbConnection _connection;

        public ConnectionCreator(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public DbConnection Connection
        {
            get
            {
                _connection ??= CreateConnection();

                return _connection;
            }
        }

        private DbConnection CreateConnection()
        {
            _connection = new SqliteConnection(_dbConfig.ConnectionString);

            _connection.Open();

            return _connection;
        }
        
        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
                _connection.Dispose();

                _connection = null;
            }
        }
    }
}