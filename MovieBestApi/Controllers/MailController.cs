using MailKit;
using MovieBestAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBestAPI.Dtos;
using IMailService = MovieBestAPI.Interfaces.IMailService;
using AutoMapper.Internal;

namespace MovieBestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        //    private readonly IMailService _mailingService;

        //    public MailController(IMailService mailService)
        //    {
        //        _mailingService = mailService;
        //    }


        //    [HttpPost("send")]
        //    public async Task<IActionResult> SendMail([FromForm] MailRequestDto dto)
        //    {
        //        await _mailingService.SendEmailAsync(dto.ToEmail, dto.Subject, dto.Body, dto.Attachments);
        //        return Ok();
        //    }

        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequestDto request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok(request);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }


}
