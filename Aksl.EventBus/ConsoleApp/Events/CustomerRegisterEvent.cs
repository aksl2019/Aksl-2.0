using System;

using Aksl.EventBus;

namespace EventBus.ConsoleApp.Events
{
    public class CustomerRegisterEvent : IntegrationEvent
    {
        public CustomerRegisterEvent(string name, string email, DateTime birthDate)
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