using System;
using System.Data.Common;

namespace SimpleWebApi.DAL.Connection.Abstract
{
    public interface IConnectionCreator : IDisposable
    {
        DbConnection Connection { get; }
    }
}