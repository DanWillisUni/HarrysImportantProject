using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrysImportantProject.Configuration.Models
{
    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class EmailPublisherConfiguration
    {
        public List<string> Recipients { get; set; } = new List<string>();
        public SmtpConfiguration Smtp { get; set; }
        public string Subject { get; set; }
    }
}
