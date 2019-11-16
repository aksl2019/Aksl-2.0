using System;
using System.Threading;
using System.Threading.Tasks;

using Aksl.EventBus;

using EventBus.ConsoleApp.Events;

namespace EventBus.ConsoleApp.EventHandlers
{
    public class CustomerRegisterEventHandler : IIntegrationEventHandler<CustomerRegisterEvent>
    {
        protected IEventBus _eventBus;

        #region Constructors
        public CustomerRegisterEventHandler(IEventBus eventBus)
                                             => _eventBus = eventBus;
        #endregion

        public Task HandleAsync(CustomerRegisterEvent @event)
        {
            Console.WriteLine("Handle Customer Register Event");

            _eventBus.Publish(new CustomerRegisterCompleteEvent(@event.Name, @event.Email, @event.BirthDate));

            return Task.CompletedTask;
        }
    }

    public class CustomerRegisteringEventHandler : IIntegrationEventHandler<CustomerRegisterEvent>
    {
        protected IEventBus _eventBus;

        #region Constructors
        public CustomerRegisteringEventHandler(IEventBus eventBus)
                                             => _eventBus = eventBus;
        #endregion

        public Task HandleAsync(CustomerRegisterEvent @event)
        {
            Console.WriteLine("Handle Customer Registering Event");

            _eventBus.Publish(new CustomerRegisterCompleteEvent(@event.Name, @event.Email, @event.BirthDate));

            return Task.CompletedTask;
        }
    }
}