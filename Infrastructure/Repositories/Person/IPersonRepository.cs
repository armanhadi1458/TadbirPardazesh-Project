using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Person
{
    public interface IPersonRepository
    {
        Task<Core.Models.Person> InsertPersonAsync(Core.Models.Person person);
        Task<List<Core.Models.Person>> GetAll();
        Task<Core.Models.Person> FindById(Guid personId);
        Task<Core.Models.Person> Update(Core.Models.Person person);
        Task Delete(Guid personId);
    }
}
