using System.ComponentModel.DataAnnotations;

namespace MovieBestAPI.Dtos
{
    public class MailRequestDto
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IList<IFormFile>? Attachments { get; set; }
    }
}
