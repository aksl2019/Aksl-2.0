using System;
using System.Threading;
using System.Threading.Tasks;

using Aksl.EventBus;

using EventBus.ConsoleApp.Events;

namespace EventBus.ConsoleApp.EventHandlers
{
    public class CustomerEventHandler : IIntegrationEventHandler<CustomerRegisterEvent>,
                                        IIntegrationEventHandler<CustomerUpdateEvent>,
                                        IIntegrationEventHandler<CustomerRemoveEvent>
    {
        protected IEventBus _eventBus;

        #region Constructors
        public CustomerEventHandler(IEventBus eventBus)
                                       => _eventBus = eventBus;
        #endregion

        public Task HandleAsync(CustomerRegisterEvent @event)
        {
            Console.WriteLine("Handle Customer Register Event");

            _eventBus.Publish(new CustomerRegisterCompleteEvent(@event.Name, @event.Email, @event.BirthDate));

            return Task.FromResult(true);
        }

        public Task HandleAsync(CustomerUpdateEvent @event)
        {
            Console.WriteLine("Handle CustomerUpdated Event");

            return Task.FromResult(true);
        }

        public Task HandleAsync(CustomerRemoveEvent @event)
        {
            Console.WriteLine("Handle CustomerRemoved Event");

            return Task.FromResult(true);
        }
    }
}