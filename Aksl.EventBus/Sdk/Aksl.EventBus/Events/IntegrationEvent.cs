using System;

namespace Aksl.EventBus
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreatedOnUtc = DateTime.UtcNow;
        }

        public Guid Id { get; }

        public DateTime CreatedOnUtc { get; }
    }
}
