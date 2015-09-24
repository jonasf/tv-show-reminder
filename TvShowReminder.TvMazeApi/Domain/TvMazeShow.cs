using System;

namespace TvShowReminder.TvMazeApi.Domain
{
    public class TvMazeShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Premiered { get; set; }
        public string Url { get; set; }
        public TvMazeShowImage Image { get; set; }
    }
}
