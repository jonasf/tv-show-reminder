using System;
using NSubstitute;
using TvShowReminder.Contracts.Command;
using TvShowReminder.DataSource;
using TvShowReminder.Service.Command;
using Xunit;

namespace TvShowReminder.Unittests
{
    public class AddSubscriptionCommandHandlerTests
    {
        private readonly AddSubscriptionCommandHandler _handler;
        private readonly ISubscriptionCommandDataSource _subscriptionCommandDataSource;

        private const int ShowId = 1;
        private const string ShowName = "That show";
        private readonly AddSubscriptionCommand _command;

        public AddSubscriptionCommandHandlerTests()
        {
            _command = new AddSubscriptionCommand
            {
                ShowId = ShowId,
                ShowName = ShowName
            };

            _subscriptionCommandDataSource = Substitute.For<ISubscriptionCommandDataSource>();
            _handler = new AddSubscriptionCommandHandler(_subscriptionCommandDataSource);    
        }

        [Fact]
        public void Should_add_subscription()
        {
            _handler.Handle(_command);

            _subscriptionCommandDataSource.Received(1).Insert(ShowId, ShowName, Arg.Any<DateTime>());
        }

        [Fact]
        public void Should_add_show_with_today_as_last_airdate()
        {
            _handler.Handle(_command);

            _subscriptionCommandDataSource.Received(1).Insert(ShowId, ShowName,
                        Arg.Is<DateTime>(date => date > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0) && date < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, 59)));
        }
    }
}
