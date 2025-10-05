using Company.DAL.Models.Shared;
using System.Net;
using System.Net.Mail;

namespace Company.BLL.Services.EmailSettings
{
    public class EmailSetting : IEmailSetting
    {
        public void sendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ahmed.moh.elmetwally@gmail.com", "tezvxtodlhcauozv");
            client.Send("ahmed.moh.elmetwally@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}




