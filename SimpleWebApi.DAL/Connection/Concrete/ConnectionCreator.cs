using System.Data;
using Microsoft.Data.Sqlite;
using SimpleWebApi.DAL.Config;
using SimpleWebApi.DAL.Connection.Abstract;

namespace SimpleWebApi.DAL.Connection.Concrete
{
    public class ConnectionCreator : IConnectionCreator
    {
        private readonly DbConfig _dbConfig;
        private SqliteConnection _connection;

        public ConnectionCreator(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public SqliteConnection Connection
        {
            get
            {
                _connection ??= CreateConnection();

                return _connection;
            }
        }

        private SqliteConnection CreateConnection()
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