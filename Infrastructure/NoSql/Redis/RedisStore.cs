using Core.Utils;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.NoSql.Redis
{
    public class RedisStore<TObject> : IRedisStore<TObject> where TObject : class, new()
    {
        protected IRedisClientsManager _redisClienManager { get; }
        protected IRedisClient _redisClient { get; }
        public RedisStore(IRedisClientsManager redisClienManager)
        {
            _redisClienManager = redisClienManager;
            _redisClient = _redisClienManager.GetClient();
        }

        public async Task Save(string key, TObject model) => await Task.Run(() => _redisClient.Set<TObject>(key, model, DateTime.Now.AddMinutes(10)));

        public async Task<TObject> GetValue(string key) => await Task.Run(() => _redisClient.Get<TObject>(key));

        public async Task Remove(string key) => await Task.Run(() => _redisClient.Remove(key));

    }
}
