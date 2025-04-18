using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace HarrysImportantProject.Configuration
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {
            try
            {
                HostFactory.Run(configure =>
                {
                    configure.Service<Startup>(service =>
                    {
                        service.ConstructUsing(s => new Startup());
                        service.WhenStarted(s => s.Start());
                        service.WhenStopped(s => s.Stop());
                    });
                    configure.RunAsLocalSystem();
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
