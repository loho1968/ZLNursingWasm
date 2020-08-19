using CloneExtensions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace NursingCommon
{
    /// <summary>
    /// 缓存对象
    /// </summary>
    public class DIMemoryCache
    {
        public static IMemoryCache GetMemory()
        {
            return DIServicesCollection.Instance.GetService(typeof(IMemoryCache)) as IMemoryCache;
        }

    }

    /// <summary>
    /// 缓存帮助类（提供缓存操作）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MemeryCacheHelper<T>
    {

        /// <summary>
        /// 根据指定key获取缓存
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetCacheByName(string name)
        {
            IMemoryCache memoryCache = DIMemoryCache.GetMemory();
            return memoryCache.Get<T>(name).GetClone();
        }
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="t"></param>
        /// <param name="name"></param>
        public static void Update(T t, string name)
        {
            IMemoryCache memoryCache = DIMemoryCache.GetMemory();
            memoryCache.Set<T>(name, t);
        }
    }


}
