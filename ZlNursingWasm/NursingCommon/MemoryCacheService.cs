using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NursingCommon
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    public class MemoryCacheService : ICacheService
    {
        public IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public MemoryCacheService() { }

        public bool Add(string key, object value)
        {
            _cache.Set(key, value);
            return true;
        }

        bool ICacheService.Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        bool ICacheService.Add(string key, object value, TimeSpan expiresIn, bool isSliding)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICacheService.AddAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICacheService.AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICacheService.AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 验证缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ICacheService.Exists(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            object cached;
            return _cache.TryGetValue(key, out cached);
        }

        Task<bool> ICacheService.ExistsAsync(string key)
        {
            throw new NotImplementedException();
        }

        T ICacheService.Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        object ICacheService.Get(string key)
        {
            throw new NotImplementedException();
        }

        IDictionary<string, object> ICacheService.GetAll(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        Task<IDictionary<string, object>> ICacheService.GetAllAsync(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        Task<T> ICacheService.GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        Task<object> ICacheService.GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        bool ICacheService.Remove(string key)
        {
            throw new NotImplementedException();
        }

        void ICacheService.RemoveAll(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        Task ICacheService.RemoveAllAsync(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICacheService.RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        bool ICacheService.Replace(string key, object value)
        {
            throw new NotImplementedException();
        }

        bool ICacheService.Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        bool ICacheService.Replace(string key, object value, TimeSpan expiresIn, bool isSliding)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICacheService.ReplaceAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICacheService.ReplaceAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICacheService.ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding)
        {
            throw new NotImplementedException();
        }
    }
}
