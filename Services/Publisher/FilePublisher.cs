using HarrysImportantProject.Configuration.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FilePublisher> _logger;
        public FilePublisher(FilePublisherConfiguration configuration, ILogger<FilePublisher> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void Publish(string message)
        {
            _logger.LogInformation("Publishing message to file: " + message);
            _logger.LogInformation("File location: " + _configuration.FilePath + _configuration.FileName + _configuration.FileExtension);
            using (StreamWriter w = File.AppendText(_configuration.FilePath + _configuration.FileName + _configuration.FileExtension))
            {
                w.WriteLine(message);
            }
        }
    }
}
