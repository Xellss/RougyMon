using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MiniEngine
{
    public class GameObject
    {
        public string Tag;

        private Hashtable components = new Hashtable();

        public GameObject()
        {
        }

        public bool HasComponent<T>()
        {
            return components.ContainsKey(typeof(T));
        }

        public T AddComponent<T>() where T : Component, new()
        {
            if (!components.ContainsKey(typeof(T)))
            {
                T component = new T();
                component.SetGameObject(this);
                components.Add(typeof(T), component);
                InvokeStart(component, typeof(T));
            }
            return (T)components[typeof(T)];
        }

        public void RemoveComponent<T>()
        {
            components.Remove(typeof(T));
        }

        public T GetComponent<T>()
        {
            return (T)components[typeof(T)];
        }

        private void InvokeStart(object component, Type type)
        {
            MethodInfo method = type.GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
            if (method != null)
                method.Invoke(component, null);
        }

        public virtual void Destroy()
        {
            Component[] temp = new Component[components.Count];
            components.Values.CopyTo(temp, 0);

            foreach (var tempComponent in temp)
            {
                tempComponent.Destroy();
            }
        }
    }
}