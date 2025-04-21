using HarrysImportantProject.Configuration.Models;
using HarrysImportantProject.Services.Publisher;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrysImportantProject.Services
{
    public class BasicService
    {
        private readonly BasicServiceConfiguration _configuration;
        private readonly PublisherFactory _publisherFactory;
        private readonly ILogger<BasicService> _logger;
        public BasicService(BasicServiceConfiguration configuration, PublisherFactory publisherFactory, ILogger<BasicService> logger)
        {
            _configuration = configuration;
            _publisherFactory = publisherFactory;
            _logger = logger;
        }
        public void root()
        {
            _logger.LogInformation("BasicService started");
            IPublisher publisher = _publisherFactory.CreatePublisher(_configuration.Publisher);
            if (publisher == null)
            {
                _logger.LogError("Publisher not found");
                throw new Exception("Publisher not found");
            }
            _logger.LogInformation("Publisher found: " + _configuration.Publisher);
            publisher.Publish("This is my password " + _configuration.MyPassword);
            _logger.LogInformation("BasicService finished");
        }
    }
}
