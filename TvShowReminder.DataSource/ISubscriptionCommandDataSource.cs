﻿using System;

namespace TvShowReminder.DataSource
{
    public interface ISubscriptionCommandDataSource
    {
        void Insert(int showId, string showName, DateTime lastAirDate);
    }
}