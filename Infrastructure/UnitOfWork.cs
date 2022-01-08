using Infrastructure.Repositories.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPersonRepository Person { get; set; }
        public UnitOfWork(IPersonRepository personRepository)
        {
            Person = personRepository;
        }
    }
}
