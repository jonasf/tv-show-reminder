using TvShowReminder.Contracts.Command;

namespace TvShowReminder.Service
{
    public interface ISubscriptionCommandService
    {
        void DeleteSubscription(DeleteSubscriptionCommand command);
    }
}