using System;

namespace Aksl.EventBus
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<T, TH>() where T : IntegrationEvent
                                where TH : IIntegrationEventHandler<T>;

        //void Subscribe<TSender, TEvent, TEventHandler>(object target) where TSender : class 
        //                                                              where TEvent : IntegrationEvent
        //                                                              where TEventHandler : IIntegrationEventHandler<TEvent>;

        void Unsubscribe<T, TH>() where TH : IIntegrationEventHandler<T>
                                  where T : IntegrationEvent;


    }
}
