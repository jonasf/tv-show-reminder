﻿using System;

namespace TvShowReminder.TvRageApi
{
    public class SpecialEpisode
    {
        public int Season { get; set; }
        public DateTime AirDate { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
    }
}