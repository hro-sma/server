using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using weather.station.server.Data;

namespace weather.station.server.Actions
{
    public class RateLimitAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Limit the amount of Requests per time periode from each unique IP address
        /// </summary>
        /// <param name="timePeriode">Minimum time periode in seconds between two requests</param>
        /// <returns></returns>
        public RateLimitAttribute(int timePeriode) : base(typeof(RateLimitAttributeImpl))
        {
            Arguments = new object[] { timePeriode };
        }

        private class RateLimitAttributeImpl : IActionFilter
        {
            /// <summary>
            /// MemoryCache service instance
            /// </summary>
            private readonly IMemoryCache _memoryCache;

            private readonly WeatherStationServerContext _context;

            /// <summary>
            /// Minium time periode in seconds between two requests
            /// </summary>
            private int _timePeriode;

            public RateLimitAttributeImpl(IMemoryCache memoryCache, WeatherStationServerContext context, int timePeriode)
            {
                _timePeriode = timePeriode;
                _memoryCache = memoryCache;
                _context = context;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                // Collect header information
                string clientIp = context.HttpContext.Request.Headers["X-Forwarded-For"];
                string deviceId = context.HttpContext.Request.Headers["X-Device-Id"];

                // Try to parse a given deviceId
                if (Guid.TryParse(deviceId, out Guid parsedId))
                {
                    if (_context.Device.Any(d => d.DeviceId == parsedId))
                    {
                        if (!this.AllowRequest(clientIp + ":" + deviceId))
                        {
                            context.Result = new StatusCodeResult(429);
                        }
                        
                        return;
                    }
                }

                if (!this.AllowRequest(clientIp))
                {
                    context.Result = new StatusCodeResult(429);
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                // Collect header information
                string clientIp = context.HttpContext.Request.Headers["X-Forwarded-For"];
                string deviceId = context.HttpContext.Request.Headers["X-Device-Id"];

                this.RegisterRequest(deviceId != null ? clientIp + ":" + deviceId : clientIp);
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
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(_timePeriode));

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
}