using TvShowReminder.Contracts;

namespace TvShowReminder.Framework
{
    public interface IQuerySender
    {
        TResult Send<TResult>(IQuery<TResult> query);
    }
}
