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

using EventBus.ConsoleApp.Events;
using EventBus.ConsoleApp.EventHandlers;

namespace EventBus.ConsoleApp
{
    public partial class EventBuser
    {
        public Task InitializeSubscribe()
        {

            var eventBus = ServiceProvider.GetRequiredService<IEventBus>();
            try
            {
                eventBus.Subscribe<CustomerRegisterEvent, CustomerRegisterEventHandler>();
                eventBus.Subscribe<CustomerRegisterEvent, CustomerRegisteringEventHandler>();

                eventBus.Subscribe<CustomerRegisterEvent, IIntegrationEventHandler<CustomerRegisterEvent>>();

                eventBus.Subscribe<CustomerUpdateEvent, IIntegrationEventHandler<CustomerUpdateEvent>>();
                eventBus.Subscribe<CustomerRemoveEvent, IIntegrationEventHandler<CustomerRemoveEvent>>();

                eventBus.Publish(new CustomerRegisterEvent("John", "John@outlook.com", DateTime.Now));
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }

            return Task.CompletedTask;
        }
    }
}
