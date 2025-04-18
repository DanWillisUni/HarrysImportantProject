using HarrysImportantProject.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HarrysImportantProject.Services.Publisher
{
    public class EmailPublisher : IPublisher
    {
        private readonly EmailPublisherConfiguration _configuration;
        private readonly ILogger<EmailPublisher> _logger;
        public EmailPublisher(EmailPublisherConfiguration configuration, ILogger<EmailPublisher> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void Publish(string message)
        {
            _logger.LogInformation("Publishing message to email: " + message);
            try
            {
                _logger.LogDebug("Creating SMTP client");
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
                _logger.LogInformation("Sending email to: " + string.Join(", ", _configuration.Recipients));
                foreach (string toAddress in _configuration.Recipients)
                {
                    Msg.To.Add(toAddress);
                }
                smtp.Send(Msg);
            }
            catch (SmtpException ex)
            {
                _logger.LogError("SmtpException: " + ex.Message);
                throw new ApplicationException("SmtpException has occured: " + ex.Message + "\n" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.Message);
                throw ex;
            }
        }
    }
}
