using Cache.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Cache
{
    /// <summary>
    /// Represents a helper class to work with cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class CacheHelper
    {
        /// <summary>
        /// Gets cached item
        /// </summary>
        /// <param name="key">The name of cached item.</param>        
        /// <returns>Cached item or default if not found</returns>
        public static T Get<T>(string key)
            
        {
            if (IsCached(key))
                return (T)HttpContext.Current.Cache[key];
            else
                return default(T);
        }

        /// <summary>
        /// Add object to cache.
        /// </summary>
        /// <typeparam name="T">The type of cached item</typeparam>
        /// <param name="item">The item to be cached.</param>
        /// <param name="key">The key that will be used for that item.</param>
        public static void Add<T>(T item, string key)
        {
            HttpContext.Current.Cache.Insert(key, item, null, DateTime.UtcNow.AddMinutes(Settings.Default.CacheExpirationTimeMins), TimeSpan.Zero,
                                           CacheItemPriority.Normal, null);    
        }

        /// <summary>
        /// Determine if the specified key is already cached.
        /// </summary>
        /// <param name="key">The name of cached item.</param>
        public static bool IsCached(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }

        /// <summary>
        /// Purge (remove) item from cache by given key.
        /// </summary>
        /// <param name="key">The name of cached item.</param>
        public static void Purge(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public static IEnumerable<string> GetCacheDetails()
        {
            var result = new List<string>();

            result.Add("EffectivePrivateBytesLimit = " + HttpContext.Current.Cache.EffectivePrivateBytesLimit.ToString());
            result.Add("EffectivePercentagePhysicalMemoryLimit = " + HttpContext.Current.Cache.EffectivePercentagePhysicalMemoryLimit.ToString());

            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Key.ToString());

            }

            return result;
        }
    }
}
