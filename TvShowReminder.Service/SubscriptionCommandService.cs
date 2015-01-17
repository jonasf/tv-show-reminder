using System;
using TvShowReminder.DataSource;
using TvShowReminder.Model.Command;

namespace TvShowReminder.Service
{
    public class SubscriptionCommandService : ISubscriptionCommandService
    {
        private readonly ISubscriptionCommandDataSource _dataSource;

        public SubscriptionCommandService(ISubscriptionCommandDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public void AddSubscription(AddSubscriptionCommand command)
        {
            DateTime defaultLastAirDate = DateTime.Now;
            _dataSource.Insert(command.ShowId, command.ShowName, defaultLastAirDate);
        }
    }
}
