using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrysImportantProject.Services.Publisher
{
    public class ConsolePublisher : IPublisher
    {
        public void Publish(string message)
        {
            Console.WriteLine(message);
        }
    }
}
