using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using HarrysImportantProject.Services;
using HarrysImportantProject.Configuration.Models;
using HarrysImportantProject.Services.Publisher;

namespace HarrysImportantProject
{
    public class Startup
    {
        private static ServiceCollection? _serviceCollection;
        private static ServiceProvider? _serviceProvider;
        private Microsoft.Extensions.Logging.ILogger logger;
        public void Start()
        {
            //create new logger
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .WriteTo.File("logs//" + DateTime.Today.ToString("yyyy") + "//" + DateTime.Today.ToString("MM") + "//" + DateTime.Today.ToString("dd") + "//HarrysImportantProject-" + DateTime.Now.ToString("HHmmss") + ".txt")
                .MinimumLevel.Debug()
                .CreateLogger();

            RegisterServices();//register services for DI function

            logger = _serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Start");
            runMyStuff();//run my code
        }
        public void Stop()
        {
            logger.LogInformation("End");
            DisposeServices();
            System.Environment.Exit(0);
        }

        public void runMyStuff()
        {
            var bs = _serviceProvider.GetRequiredService<BasicService>();//get the basic decryption class
            bs.root();//run the basic decryption class
            Stop();
        }

        private static void RegisterServices()
        {
            //read json file
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _serviceCollection = new ServiceCollection();
            //get objects from json and register them
            var BasicSettings = new BasicServiceConfiguration();
            configuration.Bind("BasicSettings", BasicSettings);
            _serviceCollection.AddSingleton(BasicSettings);
            var EmailSettings = new EmailPublisherConfiguration();
            configuration.Bind("EmailSettings", EmailSettings);
            _serviceCollection.AddSingleton(EmailSettings);
            var FileSettings = new FilePublisherConfiguration();
            configuration.Bind("FileSettings", FileSettings);
            _serviceCollection.AddSingleton(FileSettings);

            //register all the classes
            _serviceCollection.AddSingleton<Program>();
            _serviceCollection.AddLogging(cfg => cfg.AddSerilog()).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug);
            _serviceCollection.AddSingleton<BasicService>();

            _serviceCollection.AddTransient<EmailPublisher>();
            _serviceCollection.AddTransient<ConsolePublisher>();
            _serviceCollection.AddTransient<FilePublisher>();
            _serviceCollection.AddTransient<IPublisher.PublisherResolver>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "Email":
                        return serviceProvider.GetService<EmailPublisher>();
                    case "Console":
                        return serviceProvider.GetService<ConsolePublisher>();
                    case "File":
                        return serviceProvider.GetService<FilePublisher>();
                    default:
                        throw new KeyNotFoundException();
                }
            });

            _serviceProvider = _serviceCollection.BuildServiceProvider(true);
        }
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
