using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.PubSub
{
    public static class PubSubExtensions
    {
        static private readonly Hub _hub = new Hub();

        public static void Publish<T>(this MonoBehaviour publisher, T data)
        {
            _hub.Publish(data);
        }

        public static void Subscribe<T>(this MonoBehaviour receiver, Action<T> handler)
        {
            _hub.Subscribe(receiver, handler);
        }

        public static void Unsubscribe<T>(this MonoBehaviour receiver)
        {
            _hub.Unsubscribe<T>(receiver);
        }
    }
}