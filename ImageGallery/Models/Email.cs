using System.Net;
using System.Net.Mail;
using RozichMurals.Web.Helpers;

namespace RozichMurals.Web.Models
{
    public class Email
    {
        public void Send(Contact contact)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("Drozdogg@gmail.com", "laughingduck")
            };
            var mail = new MailMessage(
                contact.Email,
                "dirk@dirkrozich.com",
                "DR Murals- New message from " + contact.Name,
                contact.Comment);

            smtp.Send(mail);
        }
    }
}