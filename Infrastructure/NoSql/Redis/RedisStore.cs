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
    public class RedisStore<TObject> : IRedisStore<TObject> where TObject : Core.Models.Base.Entity, new()
    {
        protected IRedisClientsManager _redisClienManager { get; }
        protected IRedisClient _redisClient { get; }
        public RedisStore(IRedisClientsManager redisClienManager)
        {
            _redisClienManager = redisClienManager;
            _redisClient = _redisClienManager.GetClient();
        }

        public async Task SetAsync(TObject model)
        {
            await Task.Run(() =>
            {
                string serilize = JsonSerializer.Serialize(model, JsonSerializerSetting.JsonSerializerOptions);
                _redisClient.Set($"peron:{model.Id}", serilize, DateTime.Now.AddMinutes(10));
            });
        }

    }
}
