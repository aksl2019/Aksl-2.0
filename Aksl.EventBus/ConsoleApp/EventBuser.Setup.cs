using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Aksl.EventBus;
using EventBus.ConsoleApp.EventHandlers;
using EventBus.ConsoleApp.Events;

namespace EventBus.ConsoleApp
{
    public partial class EventBuser
    {
        #region Members
        private const string hostUri = "http://localhost:50001";
        private string postRequestUri = $"{hostUri}/api/todo";

        // private IWebApiService _webApiService;
        protected Action _onWebApiSender;

        protected bool _isInitialize;


        protected ILoggerFactory _loggerFactory;
        protected ILogger _logger;


        protected CancellationTokenSource _cancellationTokenSource;

        private static int _totalCount = 0;

        public static EventBuser Instance { get; private set; }
        #endregion

        #region Constructors
        static EventBuser()
        {
            Instance = new EventBuser();
        }

        public EventBuser()
        {
        }

        public Task InitializeTask()
        {
            try
            {
                _isInitialize = true;

                //1.
                Services = new ServiceCollection();
                this.Services.AddOptions();

                //2.Configuration
                //string basePath = Directory.GetCurrentDirectory() + @"\..\..\..\..";
                string basePath = Directory.GetCurrentDirectory();
                this.ConfigurationBuilder = new ConfigurationBuilder()
                                               .SetBasePath(basePath)
                                               .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: false);

                this.Configuration = ConfigurationBuilder.Build();

                //3.Logging
                Services.AddLogging(builder =>
                {
                    var loggingSection = this.Configuration.GetSection("Logging");
                    var includeScopes = loggingSection.GetValue<bool>("IncludeScopes");

                    builder.AddConfiguration(loggingSection);

                    //加入一个ConsoleLoggerProvider
                    builder.AddConsole(consoleLoggerOptions =>
                    {
                        consoleLoggerOptions.IncludeScopes = includeScopes;
                    });

                    //加入一个DebugLoggerProvider
                    builder.AddDebug();
                });


                //注入
                Services.AddSingleton<IEventBus, InMemoryEventBus>();

                Services.AddScoped<CustomerRegisterEventHandler>();
                Services.AddScoped<CustomerRegisteringEventHandler>();

                Services.AddScoped<IIntegrationEventHandler<CustomerRegisterEvent>, CustomerEventHandler>();
                Services.AddScoped<IIntegrationEventHandler<CustomerUpdateEvent>, CustomerEventHandler>();
                Services.AddScoped<IIntegrationEventHandler<CustomerRemoveEvent>, CustomerEventHandler>();

                Services.AddScoped<IIntegrationEventHandler<CustomerRegisterCompleteEvent>, CustomerCompleteEventHandler>();
                Services.AddScoped<IIntegrationEventHandler<CustomerUpdateCompleteEvent>, CustomerCompleteEventHandler>();
                Services.AddScoped<IIntegrationEventHandler<CustomerRemoveCompleteEvent>, CustomerCompleteEventHandler>();

                //4.
                this.ServiceProvider = this.Services.BuildServiceProvider();

                //5.
                _loggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();
                _logger = _loggerFactory.CreateLogger<EventBuser>();

                var eventBus = ServiceProvider.GetRequiredService<IEventBus>();

                var customerRegisterEvent_01 = ServiceProvider.GetService(typeof(CustomerRegisterEventHandler));
                var customerRegisterEvent_02 = ServiceProvider.GetService(typeof(CustomerRegisteringEventHandler));

                var customerRegisterEvent_1 = ServiceProvider.GetService(typeof(IIntegrationEventHandler<CustomerRegisterEvent>));
                var customerUpdateEvent_1 = ServiceProvider.GetService(typeof(IIntegrationEventHandler<CustomerUpdateEvent>));
                var customerRemoveEvent_1 = ServiceProvider.GetService(typeof(IIntegrationEventHandler<CustomerRemoveEvent>));

                var customerRegisterEvent_2 = ServiceProvider.GetRequiredService<IIntegrationEventHandler<CustomerRegisterEvent>>();
                var customerUpdatedEvent_2 = ServiceProvider.GetRequiredService<IIntegrationEventHandler<CustomerUpdateEvent>>();
                var customerRemovedEvent_2 = ServiceProvider.GetRequiredService<IIntegrationEventHandler<CustomerRemoveEvent>>();

                var customerRegisterCompleteEvent = ServiceProvider.GetRequiredService<IIntegrationEventHandler<CustomerRegisterCompleteEvent>>();
                var customerUpdateCompleteEvent = ServiceProvider.GetRequiredService<IIntegrationEventHandler<CustomerUpdateCompleteEvent>>();
                var customerRemoveCompleteEvent = ServiceProvider.GetRequiredService<IIntegrationEventHandler<CustomerRemoveCompleteEvent>>();

                eventBus.Subscribe<CustomerRegisterCompleteEvent, IIntegrationEventHandler<CustomerRegisterCompleteEvent>>();

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                //  Console.WriteLine(ex.Message);

                return Task.FromException(ex);
            }
        }
        #endregion

        #region Properties
        public ServiceCollection Services { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        public IConfigurationBuilder ConfigurationBuilder { get; private set; }

        public IConfigurationRoot Configuration { get; private set; }

        public ILoggerFactory LoggerFactory => _loggerFactory;

        #endregion
    }
}
