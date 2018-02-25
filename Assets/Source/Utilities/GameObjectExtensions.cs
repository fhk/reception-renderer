using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Utilities
{
    public static class GameObjectExtensions
    {
        public static T GetComponentInAncestor<T>(this GameObject obj)
            where T : Component
        {
            if (obj.transform.parent == null)
                return null;

            T component = obj.transform.parent.GetComponent<T>();

            if (component != null)
                return component;
            else
                return GetComponentInAncestor<T>(obj.transform.parent.gameObject);
        }
    }
}
