using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace Aksl.EventBus
{
    public class InMemoryEventBus : IEventBus
    {
        #region Members
        private List<Subscriber> _subscribers = new List<Subscriber>();
        #endregion

        #region Constructors
        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        #endregion

        public IServiceProvider ServiceProvider { get; private set; }

        #region IEventBus Methods
        public void Publish(IntegrationEvent @event)
        {
            foreach (var subscriber in _subscribers)
            {
                if (subscriber.EventName == @event.GetType().Name)
                {
                    subscriber.TryInvoke(@event);
                }
            }
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var subscriber = _subscribers.FirstOrDefault(s => s.EventName == typeof(T).Name);
            if (subscriber == null)
            {
                subscriber = new Subscriber(typeof(T), ServiceProvider);
                _subscribers.Add(subscriber);
            }
            subscriber.AddSubscription(typeof(TH));
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var subscriber = _subscribers.FirstOrDefault(s => s.EventName == typeof(T).Name);
            if (subscriber != null)
            {
                subscriber.RemoveSubscription(typeof(TH));
                if (subscriber.IsEmpty)
                {
                    _subscribers.Remove(subscriber);
                }
            }
        }
        #endregion

        #region Subscriber
        class Subscriber
        {
            public IServiceProvider _serviceProvider;
            private Dictionary<string, IList<Type>> _subscriptions;

            public Subscriber(Type eventType, IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
                EventName = eventType.Name;
                EventType = eventType;
                _subscriptions = new Dictionary<string, IList<Type>>();
            }

            public string EventName { get; }

            public Type EventType { get;  }

            public bool IsEmpty => _subscriptions.Count() == 0;

            public void AddSubscription(Type handlerType)
            {
                if (!_subscriptions.TryGetValue(EventName, out IList<Type> handlers))
                {
                    _subscriptions.Add(EventName, new List<Type>() { handlerType });
                }
                else
                {
                    if (!handlers.Contains(handlerType))
                    {
                        handlers.Add(handlerType);
                    }
                    else
                    {
                        throw new ArgumentException($"Handler Type {handlerType.Name} already registered for '{ EventName}'", nameof(handlerType));
                    }
                }
            }

            public void RemoveSubscription()
            {
                _subscriptions.Remove(EventName);
            }

            public void RemoveSubscription(Type handlerType)
            {
                if (_subscriptions.TryGetValue(EventName, out IList<Type> handlers))
                {
                    handlers.Remove(handlerType);
                    if (handlers.Count == 0)
                    {
                        _subscriptions.Remove(EventName);
                    }
                }
            }

            public void TryInvoke(IntegrationEvent @event)
            {
                foreach (var handlerTypes in _subscriptions)
                {
                    //foreach (var eh in handlerTypes.Value.Where(r => r.IsAssignableFrom(typeof(IIntegrationEventHandler<>))))
                    foreach (var ht in handlerTypes.Value)
                    {
                        //ActivatorUtilities.CreateInstance(_serviceProvider, ht);
                        var instance = GetServiceOrCreateInstance(ht);
                        ActivatorUtilities.FindMethod(ht, EventType, @event, "HandleAsync", instance, typeof(Task), false);
                    }
                }
            }

            public object GetServiceOrCreateInstance(Type type)
            {
                //var instance = Activator.CreateInstance(eh) as IIntegrationEventHandler;
                return _serviceProvider.GetService(type) ;
            }
        }
        #endregion
    }
}
