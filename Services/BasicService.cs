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
        private readonly IPublisher.PublisherResolver _publisherResolver;
        private readonly ILogger<BasicService> _logger;
        public BasicService(BasicServiceConfiguration configuration, IPublisher.PublisherResolver publisherResolver, ILogger<BasicService> logger)
        {
            _configuration = configuration;
            _publisherResolver = publisherResolver;
            _logger = logger;
        }
        public void root()
        {
            _logger.LogInformation("BasicService started");
            IPublisher publisher = _publisherResolver(_configuration.Publisher);
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
