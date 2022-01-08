using Infrastructure.NoSql.Redis;
using Infrastructure.Repositories.Person;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));

            //add repositories
            services.AddTransient<IPersonRepository, PersonRepository>();


            //add redis
            services.AddTransient<IRedisClientsManager>(c =>
                new RedisManagerPool(configuration.GetSection("RedisConnectionString").Value));

            services.AddTransient(typeof(IRedisStore<>), typeof(RedisStore<>));

            return services;
        }
    }
}
