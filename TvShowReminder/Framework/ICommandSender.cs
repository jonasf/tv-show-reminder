using TvShowReminder.Contracts;

namespace TvShowReminder.Framework
{
    public interface ICommandSender
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}