namespace MovieBestAPI.Dtos
{
    public class PeopleDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string KnownCredits { get; set; }
        public string BirthDay { get; set; }
        public string PlaceOfBirth { get; set; }
        public string About { get; set; }
        public IFormFile Poster { get; set; }

    }
}
