using System.Collections.Generic;
using TvShowReminder.Contracts;
using TvShowReminder.Contracts.Dto;
using TvShowReminder.Contracts.Query;
using TvShowReminder.Contracts.Response;
using TvShowReminder.DataSource;

namespace TvShowReminder.Service.Query
{
    public class AllSubscriptionsWithNextEpisodeQueryHandler : IQueryHandler<AllSubscriptionsWithNextEpisodeQuery, AllSubscriptionsWithNextEpisodeResult>
    {
        private readonly ISubscriptionQueryDataSource _subscriptionQueryDataSource;
        private readonly IEpisodesQueryDataSource _episodesQueryDataSource;

        public AllSubscriptionsWithNextEpisodeQueryHandler(ISubscriptionQueryDataSource subscriptionQueryDataSource, 
                                                            IEpisodesQueryDataSource episodesQueryDataSource)
        {
            _subscriptionQueryDataSource = subscriptionQueryDataSource;
            _episodesQueryDataSource = episodesQueryDataSource;
        }

        public AllSubscriptionsWithNextEpisodeResult Handle(AllSubscriptionsWithNextEpisodeQuery query)
        {
            var subscriptions = _subscriptionQueryDataSource.GetAllSubscriptions();

            var result = new List<SubscriptionWithNextEpisodeDto>();
            foreach (var subscription in subscriptions)
            {
                result.Add(new SubscriptionWithNextEpisodeDto
                {
                    Subscription = subscription,
                    NextEpisode = _episodesQueryDataSource.GetNextEpisode(subscription.Id)
                });
            }

            return new AllSubscriptionsWithNextEpisodeResult
            {
                Subscriptions = result
            };
        }
    }
}
