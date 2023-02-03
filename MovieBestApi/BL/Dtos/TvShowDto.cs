namespace MovieBestAPI.Dtos
{
    public class TvShowDto
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Kind { get; set; }
        public string Creator { get; set; }
        public string Network { get; set; }
        public string Status { get; set; }
        public IFormFile Poster { get; set; }
    }
}
