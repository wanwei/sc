using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class DataCache<T>
    {
        private List<int> keies = new List<int>();

        private Dictionary<int, T> dicCache = new Dictionary<int, T>();

        private int maxCacheDateCount = 20;

        private Object lockCache = new object();

        public void AddCache(int key, T t)
        {
            lock (lockCache)
            {                
                if (dicCache.ContainsKey(key))
                    return;
                if (keies.Count > maxCacheDateCount)
                {
                    //先进先出策略
                    int removeDate = keies[0];
                    keies.RemoveAt(0);
                    dicCache.Remove(removeDate);
                }
                keies.Add(key);
                dicCache.Add(key, t);
            }
        }

        public void RemoveCache(int key)
        {
            lock (lockCache)
            {
                keies.Remove(key);
                dicCache.Remove(key);
            }
        }

        public T GetCache(int key)
        {
            T t;
            bool hasResult = dicCache.TryGetValue(key, out t);
            if (hasResult)
                return t;
            return default(T);
        }
    }
}
