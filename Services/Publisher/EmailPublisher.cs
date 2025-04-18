using HarrysImportantProject.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HarrysImportantProject.Services.Publisher
{
    public class EmailPublisher : IPublisher
    {
        private readonly EmailPublisherConfiguration _configuration;
        public EmailPublisher(EmailPublisherConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Publish(string message)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                NetworkCredential loginInfo = new NetworkCredential(_configuration.Smtp.Username, _configuration.Smtp.Password); // password for connection smtp if u dont have have then pass blank
                smtp.Credentials = loginInfo;
                smtp.EnableSsl = true;
                smtp.Host = _configuration.Smtp.Host;
                smtp.Port = _configuration.Smtp.Port;
                MailMessage Msg = new MailMessage();
                Msg.From = new MailAddress(_configuration.Smtp.Username);
                Msg.Subject = _configuration.Subject;
                Msg.IsBodyHtml = true;
                List<string> contsplit = message.Split("\n").ToList();
                string contentsHtml = "";
                foreach (string str in contsplit)
                {
                    contentsHtml += str + "<br>";
                }
                Msg.Body = "<br><p>" + contentsHtml + "</p>";
                foreach (string toAddress in _configuration.Recipients)
                {
                    Msg.To.Add(toAddress);
                }
                smtp.Send(Msg);
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException("SmtpException has occured: " + ex.Message + "\n" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
