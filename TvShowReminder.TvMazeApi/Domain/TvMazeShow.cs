using System;
using Newtonsoft.Json;

namespace TvShowReminder.TvMazeApi.Domain
{
    public class TvMazeShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Premiered { get; set; }
        public string Url { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TvMazeShowImage Image { get; set; }
    }
}
