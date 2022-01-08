using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Person
{
    public class PersonRepository: IPersonRepository
    {
        protected IDbConnection _dbConnection { get; }
        public PersonRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> InsertPersonAsync(Core.Models.Person person)
        {
            var query = "INSERT INTO People (Id, Age, FirstName, lastName) " +
                        "VALUES (@Id, @Age, @FirstName, @LastName)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", person.Id);
            parameters.Add("Age", person.Age);
            parameters.Add("FirstName", person.FirstName);
            parameters.Add("LastName", person.LastName);

            using (var connection = _dbConnection)
            {
                return (await connection.ExecuteAsync(query, parameters));
            }
        }
    }
}
