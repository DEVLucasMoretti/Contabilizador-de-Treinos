using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Utils.Interface;

namespace Utils
{
    public class MemoryCacheService :ICacheService
    {
        private readonly ObjectCache cache;
        public MemoryCacheService() 
        {
            cache = MemoryCache.Default;//vai atribuir à variável Cache um espaço na memória, onde vai ter as nossas chaves de cache, criando uma instancia só do cache
        }

        public T Get<T>(string key)
        {
            return (T)cache.Get(key);
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public void Set<T>(string key, T objeto, int cacheSeconds)
        {
            cache.Set(key, objeto, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(cacheSeconds)});
        }

        public void Set<T>(string key, T objeto)
        {
            Set(key, objeto, 15);
        }
    }
}
