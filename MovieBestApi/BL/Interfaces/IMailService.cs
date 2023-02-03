using MovieBestAPI.Dtos;

namespace MovieBestAPI.Interfaces
{
    public interface IMailService
    {
        //Task SendEmailAsync(string mailTo, string subject, string body , IList<IFormFile> attachments );
        Task SendEmailAsync(MailRequestDto mailRequest);

    }
}
