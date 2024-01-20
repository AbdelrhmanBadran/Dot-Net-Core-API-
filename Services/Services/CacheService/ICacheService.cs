using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CacheService
{
    public interface ICacheService
    {
        Task SetCacheResponseAsync(string cacheKey , Object response , TimeSpan timeToLive );
        Task<string> GetCacheResponseAsync(string cacheKey );

    }
}
