using System;

namespace Aksl.EventBus
{
    public class SubscriptionInfo
    {
        private SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(handlerType);
        }

        public Type HandlerType { get; }
    }
}
