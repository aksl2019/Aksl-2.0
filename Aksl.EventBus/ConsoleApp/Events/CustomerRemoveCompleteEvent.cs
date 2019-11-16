using System;

using Aksl.EventBus;

namespace EventBus.ConsoleApp.Events
{
    public class CustomerRemoveCompleteEvent : IntegrationEvent
    {
        public CustomerRemoveCompleteEvent(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; private set; }
    }
}