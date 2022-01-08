using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NoSql.Redis
{
    public interface IRedisStore<TObject> where TObject : Core.Models.Base.Entity, new()
    {
        Task SetAsync(TObject model);
    }
}
