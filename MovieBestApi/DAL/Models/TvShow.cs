namespace MovieBestAPI.Models
{
    public class TvShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Kind { get; set; }
        public string Creator { get; set; }
        public string Network { get; set; }
        public string Status { get; set; }
        public byte[] Poster { get; set; }
    }
}
