﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace Edument.CQRS
{
    /// <summary>
    /// This implements a basic message dispatcher, driving the overall command handling
    /// and event application/distribution process. It is suitable for a simple, single
    /// node application that can safely build its subscriber list at startup and keep
    /// it in memory. Depends on some kind of event storage mechanism.
    /// </summary>
    public class MessageDispatcher
    {
        private Dictionary<Type, Action<object>> commandHandlers =
            new Dictionary<Type, Action<object>>();      
        private Dictionary<Type, List<Action<object>>> eventSubscribers =
            new Dictionary<Type, List<Action<object>>>();
        private IEventStore eventStore;

        /// <summary>
        /// Initializes a message dispatcher, which will use the specified event store
        /// implementation.
        /// </summary>
        /// <param name="es"></param>
        public MessageDispatcher(IEventStore es)
        {
            eventStore = es;
        }

        /// <summary>
        /// Tries to send the specified command to its handler. Throws an exception
        /// if there is no handler registered for the command.
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="c"></param>
        public void SendCommand<TCommand>(TCommand c)
        {
            if (commandHandlers.ContainsKey(typeof(TCommand)))
                commandHandlers[typeof(TCommand)](c);
            else
                throw new Exception("No command handler registered for " + typeof(TCommand).Name);
        }

        /// <summary>
        /// Publishes the specified event to all of its subscribers.
        /// </summary>
        /// <param name="e"></param>
        private void PublishEvent(object e)
        {
            var eventType = e.GetType();
            if (eventSubscribers.ContainsKey(eventType))
                foreach (var sub in eventSubscribers[eventType])
                    sub(e);
        }

        private void PublishEvent(object e, string readmodel)
        {
            var eventType = e.GetType();

            if (eventSubscribers.ContainsKey(eventType))
                foreach (var sub in eventSubscribers[eventType])
                {
                    dynamic target = sub.Target;
                    if ((target.subscriber.GetType().Name as string).Equals(readmodel, StringComparison.OrdinalIgnoreCase))
                        sub(e);
                }
        }

        /// <summary>
        /// Adds an object that handles the specified command, by virtue of implementing
        /// the IHandleCommand interface.
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TAggregate"></typeparam>
        /// <param name="handler"></param>
        public void AddHandlerFor<TCommand, TAggregate>(IHandleCommand<TCommand, TAggregate> handler)
            where TAggregate : Aggregate, new()
        {
            if (commandHandlers.ContainsKey(typeof(TCommand)))
                throw new Exception("Command handler already registered for " + typeof(TCommand).Name);
            
            commandHandlers.Add(typeof(TCommand), c =>
                {
                    // Create up an empty aggregate.
                    var agg = new TAggregate();

                    // The aggregate loader function, when given an ID, loads and applies
                    // all of the events. It stashes the ID to indicate we loaded up the
                    // aggregate (and thus have to pay attention to version when we save
                    // back the events). Note that in the case where the aggregate's current
                    // state was never looked at, it doesn't matter when we apply the
                    // event; it has no dependency on the past.
                    Func<Guid, TAggregate> al = id =>
                        {
                            agg.Id = id;
                            agg.ApplyEvents(eventStore.LoadEventsFor<TAggregate>(id));
                            return agg;
                        };
                    
                    // With everything set up, we invoke the command handler, collecting the
                    // events that it produces.
                    var resultEvents = new ArrayList();
                    foreach (var e in handler.Handle(al, (TCommand)c))
                        resultEvents.Add(e);
                    
                    // Store the events in the event store.
                    eventStore.SaveEventsFor<TAggregate>(
                        agg.Id,
                        agg.EventsLoaded,
                        resultEvents);

                    // Publish them to all subscribers.
                    foreach (var e in resultEvents)
                        PublishEvent(e);
                });
        }

        /// <summary>
        /// Adds an object that subscribes to the specified event, by virtue of implementing
        /// the ISubscribeTo interface.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="subscriber"></param>
        public void AddSubscriberFor<TEvent>(ISubscribeTo<TEvent> subscriber)
        {
            if (!eventSubscribers.ContainsKey(typeof(TEvent)))
                eventSubscribers.Add(typeof(TEvent), new List<Action<object>>());
            eventSubscribers[typeof(TEvent)].Add(e =>
                subscriber.Handle((TEvent)e));
        }

        /// <summary>
        /// Looks thorugh the specified assembly for all public types that implement
        /// the IHandleCommand or ISubscribeTo generic interfaces. Registers each of
        /// the implementations as a command handler or event subscriber.
        /// </summary>
        /// <param name="ass"></param>
        public void ScanAssembly(Assembly ass)
        {
            // Scan for and register handlers.
            var handlers = 
                from t in ass.GetTypes()
                from i in t.GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(IHandleCommand<,>)
                let args = i.GetGenericArguments()
                select new
                {
                    Type = t,
                    CommandType = args[0],
                    AggregateType = args[1]
                };
            foreach (var h in handlers)
                this.GetType().GetMethod("AddHandlerFor") 
                    .MakeGenericMethod(h.CommandType, h.AggregateType)
                    .Invoke(this, new object[] { CreateInstanceOf(h.Type) });

            // Scan for and register subscribers.
            var subscriber =
                from t in ass.GetTypes()
                from i in t.GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(ISubscribeTo<>)
                select new
                {
                    Type = t,
                    EventType = i.GetGenericArguments()[0]
                };
            foreach (var s in subscriber)
                this.GetType().GetMethod("AddSubscriberFor")
                    .MakeGenericMethod(s.EventType)
                    .Invoke(this, new object[] { CreateInstanceOf(s.Type) });
        }

        /// <summary>
        /// Looks at the specified object instance, examples what commands it handles
        /// or events it subscribes to, and registers it as a receiver/subscriber.
        /// </summary>
        /// <param name="instance"></param>
        public void ScanInstance(object instance)
        {
            // Scan for and register handlers.
            var handlers =
                from i in instance.GetType().GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(IHandleCommand<,>)
                let args = i.GetGenericArguments()
                select new
                {
                    CommandType = args[0],
                    AggregateType = args[1]
                };
            foreach (var h in handlers)
                this.GetType().GetMethod("AddHandlerFor")
                    .MakeGenericMethod(h.CommandType, h.AggregateType)
                    .Invoke(this, new object[] { instance });

            // Scan for and register subscribers.
            var subscriber =
                from i in instance.GetType().GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(ISubscribeTo<>)
                select i.GetGenericArguments()[0];
            foreach (var s in subscriber)
                this.GetType().GetMethod("AddSubscriberFor")
                    .MakeGenericMethod(s)
                    .Invoke(this, new object[] { instance });
        }

        /// <summary>
        /// Creates an instance of the specified type. If you are using some kind
        /// of DI container, and want to use it to create instances of the handler
        /// or subscriber, you can plug it in here.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private object CreateInstanceOf(Type t)
        {
            return Activator.CreateInstance(t);
        }

        public void RepublishEvents()
        {
            foreach (var e in eventStore.LoadAllEvents())
                PublishEvent(e);
        }

        public void RepublishEvents(string readmodel)
        {
            foreach (var e in eventStore.LoadAllEvents())
                PublishEvent(e, readmodel);
        }

        public TAggregate Load<TAggregate>(Guid id)
            where TAggregate : Aggregate, new()
        {
            var agg = new TAggregate();
            agg.Id = id;
            agg.ApplyEvents(eventStore.LoadEventsFor<TAggregate>(id));
            return agg;
        }
    }
}
