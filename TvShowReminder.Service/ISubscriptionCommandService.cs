using TvShowReminder.Model.Command;

namespace TvShowReminder.Service
{
    public interface ISubscriptionCommandService
    {
        void AddSubscription(AddSubscriptionCommand command);
        void DeleteSubscription(DeleteSubscriptionCommand command);
    }
}