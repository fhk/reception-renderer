using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ReceptionRenderer.PubSub
{
    public class Hub
    {
        public class Handler
        {
            public MonoBehaviour Receiver { get; set; }
            public Delegate Action { get; set; }

            public Handler(MonoBehaviour receiver, Delegate action)
            {
                Receiver = receiver;
                Action = action;
            }
        }

        private Dictionary<Type, List<Handler>> _handlers;

        public Hub()
        {
            _handlers = new Dictionary<Type, List<Handler>>(50);
        }

        public void Publish<T>(T data)
        {
            if (!_handlers.ContainsKey(typeof(T)))
                return;

            List<Handler> handlers = _handlers[typeof(T)];

            for(int i = 0; i < handlers.Count; i++)
                ((Action<T>)handlers[i].Action)(data);
        }

        public void Subscribe<T>(MonoBehaviour receiver, Action<T> handler)
        {
            if (!_handlers.ContainsKey(typeof(T)))
                _handlers.Add(typeof(T), new List<Handler>(100));

            _handlers[typeof(T)].Add(new Handler(receiver, handler));
        }

        public void Unsubscribe<T>(MonoBehaviour receiver)
        {
            if (!_handlers.ContainsKey(typeof(T)))
                return;

            _handlers[typeof(T)].RemoveAll(x => x.Receiver == receiver);
        }
    }
}
