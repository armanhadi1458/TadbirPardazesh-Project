using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Person
{
    public class PersonRepository : IPersonRepository
    {
        protected string _dbConnection { get; }
        public PersonRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection.ConnectionString;
        }

        public async Task<Core.Models.Person> InsertPersonAsync(Core.Models.Person person)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", person.Id);
            parameters.Add("@Age", person.Age);
            parameters.Add("@FirstName", person.FirstName);
            parameters.Add("@LastName", person.LastName);

            using (var connection = new SqlConnection(_dbConnection))
            {
                return await connection.QueryFirstOrDefaultAsync<Core.Models.Person>(sql: "dbo.Person_Create", param: parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Core.Models.Person>> GetAll()
        {
            using (var connection = new SqlConnection(_dbConnection))
            {
                var people = await connection.QueryAsync<Core.Models.Person>(sql: "dbo.Person_GetAll", commandType: CommandType.StoredProcedure);
                return people.ToList();
            }
        }

        public async Task<Core.Models.Person> FindById(Guid personId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", personId);

            using (var connection = new SqlConnection(_dbConnection))
            {
                return await connection.QueryFirstOrDefaultAsync<Core.Models.Person>(sql: "dbo.Person_FindById", param: parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Core.Models.Person> Update(Core.Models.Person person)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", person.Id);
            parameters.Add("@FirstName", person.FirstName);
            parameters.Add("@LastName", person.LastName);
            parameters.Add("@Age", person.Age);

            using (var connection = new SqlConnection(_dbConnection))
            {
                return await connection.QueryFirstOrDefaultAsync<Core.Models.Person>(sql: "dbo.Person_Update", param: parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Delete(Guid personId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", personId);

            using (var connection = new SqlConnection(_dbConnection))
            {
                await connection.ExecuteAsync(sql: "dbo.Person_Delete", param: parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
