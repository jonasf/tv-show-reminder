using System;
using Newtonsoft.Json;

namespace TvShowReminder.TvMazeApi.Domain
{
    public class TvMazeEpisode
    {
        public string Name { get; set; }
        public int Season { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Number { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime AirDate { get; set; }
    }
}
