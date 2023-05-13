using DocumentFormat.OpenXml.Math;
using hospital.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace hospital.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MailController : Controller
    {
        #region MailGet
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region MailPost
        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("HSM Hospital", "reyhanhsynova@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("Patient", mailRequest.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = mailRequest.Subject;
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("reyhanhsynova@gmail.com", "wivpmtnhpntebros");
            client.Send(mimeMessage);
            client.Disconnect(true);
             return View();



        }
        #endregion
    }
}
