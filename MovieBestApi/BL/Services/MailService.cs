using AutoMapper.Internal;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MovieBestAPI.Dtos;
using MovieBestAPI.Helper;
using MovieBestAPI.Interfaces;

namespace MovieBestAPI.Services
{
    public class MailService : IMailService
    {

        //private readonly MailSetings _mailSetings;

        //public MailService(IOptions<MailSetings> mailSetings)
        //{
        //    _mailSetings = mailSetings.Value;
        //}

        //public async Task SendEmailAsync(string mailTo, string subject, string body , IList<IFormFile> attachments )
        //{
        //    var email = new MimeMessage
        //    {
        //        Sender = MailboxAddress.Parse(_mailSetings.Email),
        //        Subject = subject
        //    };
        //    email.To.Add(MailboxAddress.Parse(mailTo));
        //    var builder = new BodyBuilder();

        //    if (attachments != null)
        //    {
        //        byte[] fileBytes;
        //        foreach (var file in attachments)
        //        {
        //            if (file.Length > 0)
        //            {
        //                using var ms = new MemoryStream();
        //                file.CopyTo(ms);
        //                fileBytes = ms.ToArray();

        //                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        //            }
        //        }
        //    }

        //    builder.HtmlBody = body;
        //    email.Body = builder.ToMessageBody();
        //    email.From.Add(new MailboxAddress(_mailSetings.DisplayName, _mailSetings.Email));

        //    using var smtp = new SmtpClient();
        //    smtp.Connect(_mailSetings.Host, _mailSetings.Port, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(_mailSetings.Email, _mailSetings.Password);
        //    await smtp.SendAsync(email);

        //    smtp.Disconnect(true);



        //}
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequestDto mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
