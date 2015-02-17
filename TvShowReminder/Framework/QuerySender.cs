using Autofac;
using TvShowReminder.Contracts;

namespace TvShowReminder.Framework
{
    public class QuerySender : IQuerySender
    {
        private readonly IComponentContext _context;

        public QuerySender(IComponentContext context)
        {
            _context = context;
        }

        public TResult Send<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof (IQueryHandler<,>).MakeGenericType(query.GetType(), typeof (TResult));

            dynamic handler = _context.Resolve(handlerType);
            return handler.Handle((dynamic) query);
        }
    }
}