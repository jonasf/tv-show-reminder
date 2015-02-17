using Autofac;
using TvShowReminder.Contracts;

namespace TvShowReminder.Framework
{
    public class CommandSender : ICommandSender
    {
        private readonly IComponentContext _context;

        public CommandSender(IComponentContext context)
        {
            _context = context;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _context.Resolve<ICommandHandler<TCommand>>();
            handler.Handle(command);
        }
    }
}