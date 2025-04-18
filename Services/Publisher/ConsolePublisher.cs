using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrysImportantProject.Services.Publisher
{
    public class ConsolePublisher : IPublisher
    {
        private readonly ILogger<ConsolePublisher> _logger;
        public ConsolePublisher(ILogger<ConsolePublisher> logger)
        {
            _logger = logger;
        }
        public void Publish(string message)
        {
            _logger.LogInformation("Publishing message to console: " + message);
            Console.WriteLine(message);
        }
    }
}
