using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Store
{
    public class SubscriptionBuilder
    {        
        private Subscription _subscription;

        private SubscriptionBuilder(Store.Callback callback)
        {
            _subscription = new Subscription(callback);
        }

        public static SubscriptionBuilder New(Store.Callback callback)
        {
            return new SubscriptionBuilder(callback);
        }

        public SubscriptionBuilder WithEntitySubscription(Guid entityId)
        {
            _subscription.EntityId = entityId;
            return this;
        }

        public SubscriptionBuilder WithPropertySubscription(Guid entityId, string propertyName)
        {
            _subscription.EntityId = entityId;
            _subscription.PropName = propertyName;

            return this;
        }

        public Subscription Build()
        {
            if (_subscription.EntityId != Guid.Empty)
            {
                return _subscription;
            }

            throw new Exception("the subscription does not have a target, please provide an entityId");            
        }
    }
}
