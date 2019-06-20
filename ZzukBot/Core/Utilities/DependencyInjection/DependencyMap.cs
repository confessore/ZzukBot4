using System;
using System.Collections.Generic;

namespace ZzukBot.Core.Utilities.DependencyInjection
{
    /// <summary>
    ///     A class that helps with basic Dependency Injection (IoC)
    /// </summary>
    public class DependencyMap
    {
        private Dictionary<Type, Func<object>> map;
        /// <summary>
        ///     An empty DependencyMap that can be accessed staticallly
        /// </summary>
        public static DependencyMap Empty => new DependencyMap();

        /// <summary>
        ///     Instantiates a new DependencyMap
        /// </summary>
        public DependencyMap()
        {
            map = new Dictionary<Type, Func<object>>();
        }

        /// <summary>
        ///     Adds a new singleton object to the dependency map dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Add<T>(T obj) where T : class
            => AddFactory(() => obj);

        /// <summary>
        ///     Tries to add a new singleton object to the dependency map dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TryAdd<T>(T obj) where T : class
            => TryAddFactory(() => obj);

        /// <summary>
        ///     Adds a new transient object to the dependency map dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddTransient<T>() where T : class, new()
            => AddFactory(() => new T());

        /// <summary>
        ///     Tries to add a new transient object to the dependency map dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool TryAddTransient<T>() where T : class, new()
            => TryAddFactory(() => new T());

        /// <summary>
        ///     Adds a new transient object to the dependency map dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        public void AddTransient<TKey, TImpl>() where TKey : class
            where TImpl : class, TKey, new()
            => AddFactory<TKey>(() => new TImpl());

        /// <summary>
        ///     Tries to add a new transient object to the dependency map dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns></returns>
        public bool TryAddTransient<TKey, TImpl>() where TKey : class
            where TImpl : class, TKey, new()
            => TryAddFactory<TKey>(() => new TImpl());

        /// <summary>
        ///     Adds a new factory object to the dependency map dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        public void AddFactory<T>(Func<T> factory) where T : class
        {
            var t = typeof(T);
            if (map.ContainsKey(t))
                throw new InvalidOperationException($"The dependency map already contains \"{t.FullName}\"");
            map.Add(t, factory);
        }

        /// <summary>
        ///      Tries to add a new factory object to the dependency map dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        public bool TryAddFactory<T>(Func<T> factory) where T : class
        {
            var t = typeof(T);
            if (map.ContainsKey(t))
                return false;
            map.Add(t, factory);
            return true;
        }

        /// <summary>
        ///     Gets an object from the dependency map dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        /// <summary>
        ///     Gets an object from the dependency map dictionary
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public object Get(Type t)
        {
            if (!TryGet(t, out object result))
                throw new KeyNotFoundException($"The dependency map does not contain \"{t.FullName}\"");
            else
                return result;
        }

        /// <summary>
        ///     Tries to get an object from the dependency map dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGet<T>(out T result)
        {
            if (TryGet(typeof(T), out object untypedResult))
            {
                result = (T)untypedResult;
                return true;
            }
            else
            {
                result = default(T);
                return false;
            }
        }

        /// <summary>
        ///     Tries to get an object from the dependency map dictionary
        /// </summary>
        /// <param name="t"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGet(Type t, out object result)
        {
            if (map.TryGetValue(t, out Func<object> func))
            {
                result = func();
                return true;
            }
            result = null;
            return false;
        }
    }
}
