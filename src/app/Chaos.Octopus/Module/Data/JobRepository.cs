namespace Chaos.Octopus.Module.Data
{
    using System.Collections.Generic;
    using CHAOS.Data.MySql;
    using Model;
    using MySql.Data.MySqlClient;
    using Portal.Core.Exceptions;

    public class JobRepository
    {
        public Gateway Gateway { get; set; }

        public JobRepository(Gateway gateway)
        {
            Gateway = gateway;
        }

        public IEnumerable<Job> Get(string status)
        {
            return Gateway.ExecuteQuery<Job>("Job_Get", new[]
                {
                    new MySqlParameter("Status", status)
                });
        }

        public void Set(string id, string status, string data)
        {
            var result = Gateway.ExecuteNonQuery("Job_Set", new[]
                {
                    new MySqlParameter("Id", id),
                    new MySqlParameter("Status", status),
                    new MySqlParameter("Data", data)
                });
            
            if(result == 0)
                throw new UnhandledException("Job wasn't set");
        }
    }
}