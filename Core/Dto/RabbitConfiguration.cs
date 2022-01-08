using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class RabbitConfiguration
    {
        public readonly static string Name = "Rabbit";
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PersonSqlQueueName { get; set; }
        public string PerosnRedisQueueName { get; set; }
    }
}
