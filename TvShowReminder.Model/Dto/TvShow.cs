namespace TvShowReminder.Model.Dto
{
    public class TvShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int StartedYear { get; set; }
        public int EndedYear { get; set; }
        public bool IsSubscribed { get; set; }
    }
}