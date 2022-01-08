using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Person
{
    public interface IPersonRepository
    {
        Task<int> InsertPersonAsync(Core.Models.Person person);
    }
}
