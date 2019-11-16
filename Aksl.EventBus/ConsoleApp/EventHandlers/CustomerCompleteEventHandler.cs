using System;
using System.Threading;
using System.Threading.Tasks;

using Aksl.EventBus;

using EventBus.ConsoleApp.Events;

namespace EventBus.ConsoleApp.EventHandlers
{
    public class CustomerCompleteEventHandler : IIntegrationEventHandler<CustomerRegisterCompleteEvent>,
                                                IIntegrationEventHandler<CustomerUpdateCompleteEvent>,
                                                IIntegrationEventHandler<CustomerRemoveCompleteEvent>
    {
        #region Constructors
        public CustomerCompleteEventHandler()
        {
        }
        #endregion

        public Task HandleAsync(CustomerRegisterCompleteEvent @event)
        {
            Console.WriteLine("Handle Customer Register Complete Event");

            return Task.CompletedTask;
        }

        public Task HandleAsync(CustomerUpdateCompleteEvent @event)
        {
            Console.WriteLine("Handle Customer Update Complete Event");

            return Task.CompletedTask;
        }

        public Task HandleAsync(CustomerRemoveCompleteEvent @event)
        {
            Console.WriteLine("Handle Customer Remove Complete Event");

            return Task.CompletedTask;
        }
    }
}