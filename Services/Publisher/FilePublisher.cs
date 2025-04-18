using HarrysImportantProject.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrysImportantProject.Services.Publisher
{
    public class FilePublisher : IPublisher
    {
        private readonly FilePublisherConfiguration _configuration;
        public FilePublisher(FilePublisherConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Publish(string message)
        {
            using (StreamWriter w = File.AppendText(_configuration.FilePath + _configuration.FileName + _configuration.FileExtension))
            {
                w.WriteLine(message);
            }
        }
    }
}
