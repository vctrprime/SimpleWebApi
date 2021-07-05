using System;
using Microsoft.Data.Sqlite;

namespace SimpleWebApi.DAL.Connection.Abstract
{
    public interface IConnectionCreator : IDisposable
    {
        SqliteConnection Connection { get; }
    }
}