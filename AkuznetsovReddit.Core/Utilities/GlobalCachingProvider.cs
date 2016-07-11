using System;

namespace AkuznetsovReddit.Core.Utilities
{
    /// <summary>
    /// Global caching provider
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Core.Utilities.BaseCachingProvider" />
    public class GlobalCachingProvider : BaseCachingProvider

    {
        public static GlobalCachingProvider Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        public virtual new void AddItem(string key, object value, DateTimeOffset? expiration = null)
        {
            base.AddItem(key, value, expiration);
        }

        public virtual new object GetItem(string key)
        {
            return base.GetItem(key);
        }

        public virtual new void RemoveItem(string key)
        {
            base.RemoveItem(key);
        }

        private class Nested
        {
            internal static readonly GlobalCachingProvider instance = new GlobalCachingProvider();

            static Nested()
            {
            }
        }
    }
}
