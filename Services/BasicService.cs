using HarrysImportantProject.Configuration.Models;
using HarrysImportantProject.Services.Publisher;
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
        public BasicService(BasicServiceConfiguration configuration, IPublisher.PublisherResolver publisherResolver)
        {
            _configuration = configuration;
            _publisherResolver = publisherResolver;
        }
        public void root()
        {
            IPublisher publisher = _publisherResolver(_configuration.Publisher);
            if (publisher == null)
            {
                throw new Exception("Publisher not found");
            }
            else
            {
                publisher.Publish("This is my password " + _configuration.MyPassword);
            }
        }
    }
}
