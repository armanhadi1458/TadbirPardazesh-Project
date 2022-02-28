using Infrastructure.NoSql.Redis;
using Infrastructure.Repositories.Person;
using ServiceStack.Redis;
using System;
using System.Data.SqlClient;
using Xunit.Abstractions;

namespace Infrastructure.Test.Utils
{
    public class TestFixture<TObject> : IDisposable where TObject : class, new()
    {
        /// <summary>
        /// Output
        /// </summary>
        public ITestOutputHelper Output { get; set; }

        /// <summary>
        /// Configuration
        /// </summary>
        private TestConfiguration _configuration { get; set; }

        private IPersonRepository _personRepository;
        /// <summary>
        /// Contact repository
        /// </summary>
        public IPersonRepository PersonRepository
        {
            get
            {
                if (_personRepository == null)
                    _personRepository = new PersonRepository(new SqlConnection(_configuration.DapperTesting.ConnectionString));

                return _personRepository;
            }
        }

        private IRedisStore<TObject> _redisStore;

        public IRedisStore<TObject> RedisStore
        {
            get
            {
                if (_redisStore == null)
                    _redisStore = new RedisStore<TObject>(new RedisManagerPool(_configuration.RedisTesting.ConnectionString));

                return _redisStore;
            }
        }

        /// <summary>
        /// Person Id
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// C'tor
        /// </summary>
        public TestFixture()
        {
            ConfigurationHelper helper = new ConfigurationHelper();
            _configuration = helper.GetConfiguration();
        }

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose()
        {
            // Clean up
        }
    }
}
