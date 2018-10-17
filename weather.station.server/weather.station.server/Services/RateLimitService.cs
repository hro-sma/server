using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace weather.station.server.Services
{
    public class RateLimitServiceOptions
    {
        /// <summary>
        /// Maximum amount of requests
        /// </summary>
        /// <value></value>
        public int MaxRequests { get; set; }

        /// <summary>
        /// Time Periode in seconds between the maximum amount of requests
        /// </summary>
        /// <value></value>
        public int TimePeriode { get; set; }
    }

    public class RateLimitService : IRateLimitService
    {
        private readonly RateLimitServiceOptions _options;

        private readonly IMemoryCache _memoryCache;

        public RateLimitService(IOptions<RateLimitServiceOptions> options, IMemoryCache memoryCache)
        {
            _options = options.Value;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Check if the given IP is allowed to perform a request
        /// </summary>
        /// <param name="ip">IPv4 address of the client</param>
        /// <returns>Boolean indicating if a request is allowed or not</returns>
        public bool AllowRequest(string ip)
        {
            if (this.CacheEntryExists(ip))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Register a request from the given IP
        /// </summary>
        /// <param name="ip">IPv4 address of the client</param>
        public void RegisterRequest(string ip)
        {
            this.CreateCacheEntry(ip, DateTime.Now.ToString());
        }
        
        /// <summary>
        /// Add a new Entry to the memory cache
        /// </summary>
        /// <param name="key">Key of the new entry</param>
        /// <param name="value">Value of the new entry</param>
        /// <returns></returns>
        private bool CreateCacheEntry(string key, string value)
        {
            string cacheEntry;

            // Look for cache key.
            if (!_memoryCache.TryGetValue(key, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = value;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(_options.TimePeriode));

                // Save data in cache.
                _memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return true;
        }

        /// <summary>
        /// Check if a key exists in the memory cache
        /// </summary>
        /// <param name="key">Key to find</param>
        /// <returns>Boolean indicating if the key exists or not</returns>
        private bool CacheEntryExists(string key)
        {
            string cacheEntry;

            if (_memoryCache.TryGetValue(key, out cacheEntry))
            {
                return true;
            }

            return false;
        }
    }
}