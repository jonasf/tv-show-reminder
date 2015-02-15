using TvShowReminder.Contracts.Command;

namespace TvShowReminder.Service
{
    public interface ISubscriptionCommandService
    {
        void AddSubscription(AddSubscriptionCommand command);
        void DeleteSubscription(DeleteSubscriptionCommand command);
    }
}