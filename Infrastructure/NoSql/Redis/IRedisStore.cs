using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NoSql.Redis
{
    public interface IRedisStore<TObject> where TObject : class, new()
    {
        Task Save(string key, TObject model);
        Task<TObject> GetValue(string key);
        Task Remove(string key);
    }
}
