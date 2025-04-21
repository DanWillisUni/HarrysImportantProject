using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrysImportantProject.Services.Publisher
{
    public class PublisherFactory
    {
        private readonly IPublisher.PublisherResolver _publisherResolver;
        public PublisherFactory(IPublisher.PublisherResolver publisherResolver)
        {
            _publisherResolver = publisherResolver;
        }
        public IPublisher CreatePublisher(string publisherType)
        {
            return _publisherResolver(publisherType);
        }
    }
}
