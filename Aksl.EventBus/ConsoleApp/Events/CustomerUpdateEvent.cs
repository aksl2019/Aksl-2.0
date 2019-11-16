using System;

using Aksl.EventBus;

namespace EventBus.ConsoleApp.Events
{
    public class CustomerUpdateEvent : IntegrationEvent
    {
        public CustomerUpdateEvent(Guid customerId, string name, string email, DateTime birthDate)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }
        public Guid  CustomerId { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}