using System;
using System.Runtime.Caching;

namespace AkuznetsovReddit.Core.Utilities
{
    /// <summary>
    /// Base cashing Provider
    /// </summary>
    public class BaseCachingProvider
    {
        protected MemoryCache cache = new MemoryCache("CachingProvider");
        private static readonly object _padlock = new object();

        public BaseCachingProvider()
        {
        }

        /// <summary>
        /// Adds the item to server side chaching
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiration">The expiration.</param>
        protected virtual void AddItem(string key, object value, DateTimeOffset? expiration = null)
        {
            lock (_padlock)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                if (expiration != null)
                {
                    policy.AbsoluteExpiration = expiration.Value;
                }
                else
                {
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(1);
                }
                cache.Add(key, value, policy);
            }
        }

        /// <summary>
        /// Gets the item from cache by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected virtual object GetItem(string key)
        {
            lock (_padlock)
            {
                return cache[key];
            }
        }

        /// <summary>
        /// Removes the item from cache by key.
        /// </summary>
        /// <param name="key">The key.</param>
        protected virtual void RemoveItem(string key)
        {
            lock (_padlock)
            {
                cache.Remove(key);
            }
        }
    }
}
