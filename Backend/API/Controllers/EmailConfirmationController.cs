using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using System.Web.Http.Cors;
using BOL.Dto;
using BLL;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmailConfirmationController : ApiController
    {

        [HttpPost]
        [Route("Api/SendEmail")]
        public IHttpActionResult SendEmail(EmailDto email)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(email.UserEmail, email.Name));
            message.From = new MailAddress("buddyhub@fahimfaisal.net", "BuddyHub");
            //message.Bcc.Add(new MailAddress("Amit Mohanty <amitmohanty@email.com>"));
            message.Subject = "Please Confirm Your Account";
            string BodyText = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/Assets/Html/EmailTemplate.html"));
            // Generate Token
            string Token = EmailLogService.EntryEmailByUsername(email.Username);

            BodyText = BodyText.Replace("bh-confirm-link", "https://localhost:44349/Api/ConfirmEmail/" + Token);
            BodyText = BodyText.Replace("bh-username", email.Name);
            message.Body = BodyText;
            message.IsBodyHtml = true;
            var smtp = new SmtpClient("fahimfaisal.net");
            //smtp.Port = 465;  ///---------**Note**--Time Out when using the port
            smtp.Credentials = new NetworkCredential("buddyhub@fahimfaisal.net", "faisal@123");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(message);
                return Ok("Successful");
            } catch (Exception ex)
            {
                return Ok(ex);
            };
        }
        [HttpGet]
        [Route("Api/ConfirmEmail/{token}")]
        public IHttpActionResult VerifyEmail(string token)
        {
            if (EmailLogService.ConfirmEmailStatus(token))
            {
                return Ok("Successful");
            }
            else
            {
                return BadRequest("Token Not Valid");
            }
        }
    }
}
