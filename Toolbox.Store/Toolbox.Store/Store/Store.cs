using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Store.Extensions;
using System.Security.Cryptography.X509Certificates;
using Toolbox.Store.Components;

namespace Toolbox.Store
{
    public class Store
    {
        private readonly Navigator _navigator;
        private readonly Comparer _comaprer;
        private Entity _root;        
        private List<Subscription> _subscriptions;

        ///// <summary>
        ///// selects the data that the subscrition will monitor changes on.
        ///// </summary>
        ///// <param name="store">the data managed by the store</param>
        ///// <returns></returns>
        //public delegate object Selector(object store);

        /// <summary>
        /// called when data changes
        /// </summary>
        /// <param name="preUpdate">the data before change</param>
        /// <param name="postUpdate">the data after change</param>
        public delegate void Callback(object preUpdate, object postUpdate);
        
        public Store(Entity root)
        {
            _root = root;
            _subscriptions = new List<Subscription>();            
            _comaprer = new Comparer();

        }

        /// <summary>
        /// provides a higher order function that gives access to the root data of the store. what is returned in this function is the new state of the store.
        /// </summary>
        /// <param name="func"></param>
        public void Update(Func<Entity, Entity> func)
        {
            var preUpdate = _root;
            _root = func(_root);

            FindSubscriptions(preUpdate)
                .ForEach(s => s.Invoke(preUpdate, _root));

        }
        /// <summary>
        /// subscribes to an entity
        /// </summary>
        /// <param name="subject">the id of the entity which is subscribed to</param>
        /// <param name="callback">the function that is called when the entity changes.</param>
        public void Subscribe(Guid entityId, Callback callback)
        {
            var subscription = SubscriptionBuilder
                .New(callback)
                .WithEntitySubscription(entityId)
                .Build();
            
            _subscriptions.Add(subscription);
        }

        /// <summary>
        /// Subscripbes to the entire store
        /// </summary>
        /// <param name="callback"></param>
        public void Subscribe(Callback callback)
        {
            var subscription = SubscriptionBuilder
                .New(callback)
                .WithEntitySubscription(_root.Id)
                .Build();

            _subscriptions.Add(subscription);
        }

        public void SubscribeToProperty(Guid entityId, string propName, Callback callback)
        {
            var subscription = SubscriptionBuilder
                .New(callback)
                .WithPropertySubscription(entityId, propName)
                .Build();

            _subscriptions.Add(subscription);
        }

        /// <summary>
        /// Use if property is in root object
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="callback"></param>
        public void SubscribeToProperty(string propName, Callback callback)
        {
            var subscription = SubscriptionBuilder
                .New(callback)
                .WithPropertySubscription(_root.Id, propName)
                .Build();

            _subscriptions.Add(subscription);
        }

        /// <summary>
        /// locates subscriptions that target entities that has been modified
        /// </summary>
        /// <param name="preUpdate">the state of the store before a change</param>
        /// <param name="rootpostUpdate">the state of the store after a change</param>
        /// <returns></returns>
        private IEnumerable<Subscription> FindSubscriptions(Entity preUpdate)
        {
            var preNavigator = new Navigator(preUpdate);
            var postNavigator = new Navigator(_root);
            var comparer = new Comparer();

            return _subscriptions.Where(s => comparer.EqualTo(
                preNavigator.FindEntity(s.EntityId), postNavigator.FindEntity(s.EntityId)));
        }

        public override string ToString()
        {
            return _root.HumanReadable();
        }
    }
}
