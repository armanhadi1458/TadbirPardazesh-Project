namespace Infrastructure.Test.Utils
{
    public class TestConfiguration
    {
        public Connection DapperTesting { get; set; }
        public Connection RedisTesting { get; set; }
    }

    public class Connection
    {
        public string ConnectionString { get; set; }
    }

}
