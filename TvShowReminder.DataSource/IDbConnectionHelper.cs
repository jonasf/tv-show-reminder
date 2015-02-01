using System;
using System.Data;

namespace TvShowReminder.DataSource
{
    public interface IDbConnectionHelper
    {
        void Open(Action<IDbConnection> action);
        T Open<T>(Func<IDbConnection, T> action);
    }
}