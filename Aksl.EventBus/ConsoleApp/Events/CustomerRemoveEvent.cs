using System;

using Aksl.EventBus;

namespace EventBus.ConsoleApp.Events
{
    public class CustomerRemoveEvent : IntegrationEvent
    {
        public CustomerRemoveEvent(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; private set; }
    }
}