using System;

using Aksl.EventBus;

namespace EventBus.ConsoleApp.Events
{
    public class CustomerRegisterCompleteEvent : IntegrationEvent
    {
        public CustomerRegisterCompleteEvent( string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}