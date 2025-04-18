using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrysImportantProject.Services.Publisher
{
    public interface IPublisher
    {
        void Publish(string message);
        public delegate IPublisher PublisherResolver(string key);
    }
}
