using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace OrganizationTree.BusinessLayer
{
    public class RedisCacheRepository<TObject> : ICacheRepository<TObject> where TObject : class
    {
        IDatabase _cache;

        public RedisCacheRepository(IDatabase cache)
        {
            _cache = cache;
        }

        public void InsertOrUpdate(string key, TObject obj)
        {
            string val = JsonConvert.SerializeObject(obj);
            _cache.StringSet(key, val);
        }

        public async Task InsertOrUpdateAsync(string key, TObject obj)
        {
            string val = JsonConvert.SerializeObject(obj);
            await _cache.StringSetAsync(key, val);
        }

        public void InsertOrUpdate(string key, IEnumerable<TObject> objects)
        {
            var items = objects.Select(obj => (RedisValue)JsonConvert.SerializeObject(obj)).ToArray();
            _cache.SetAdd(key, items);
        }

        public async Task InsertOrUpdateAsync(string key, IEnumerable<TObject> objects)
        {
            var items = objects.Select(obj => (RedisValue)JsonConvert.SerializeObject(obj)).ToArray();
            await _cache.SetAddAsync(key, items);
        }

        public TObject Get(string id)
        {
            TObject obj = null;
            string cachedObject = _cache.StringGet(id);
            if (cachedObject != null)
            {
                obj = JsonConvert.DeserializeObject<TObject>(cachedObject);
            }

            return obj;
        }

        public async Task<TObject> GetAsync(string id)
        {
            TObject obj = null;
            string cachedObject = await _cache.StringGetAsync(id);
            if (cachedObject != null)
            {
                obj = JsonConvert.DeserializeObject<TObject>(cachedObject);
            }

            return obj;
        }

        public IEnumerable<TObject> GetList(string id)
        {
            IEnumerable<TObject> objs = new List<TObject>();
            var items = _cache.SetMembers(id);

            if (items.Count() > 0)
            {
                objs = items.Select(item => JsonConvert.DeserializeObject<TObject>(item));
            }

            return objs;
        }

        public async Task<IEnumerable<TObject>> GetListAsync(string id)
        {
            IEnumerable<TObject> objs = new List<TObject>();
            var items = await _cache.SetMembersAsync(id);

            if (items.Count() > 0)
            {
                objs = items.Select(item => JsonConvert.DeserializeObject<TObject>(item));
            }

            return objs;
        }

        public void Invalidate(string id)
        {
            _cache.KeyDelete(id);
        }

        public async Task InvalidateAsync(string id)
        {
            await _cache.KeyDeleteAsync(id);
        }
    }
}
