using System;
using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Command;
using TvShowReminder.DataSource;

namespace TvShowReminder.Service.Command
{
    public class AddSubscriptionCommandHandler : ICommandHandler<AddSubscriptionCommand>
    {
        private readonly ISubscriptionCommandDataSource _subscriptionCommandDataSource;

        public AddSubscriptionCommandHandler(ISubscriptionCommandDataSource subscriptionCommandDataSource)
        {
            _subscriptionCommandDataSource = subscriptionCommandDataSource;
        }

        public void Handle(AddSubscriptionCommand command)
        {
            DateTime defaultLastAirDate = DateTime.Now;
            _subscriptionCommandDataSource.Insert(command.ShowId, command.ShowName, defaultLastAirDate);
        }
    }
}
