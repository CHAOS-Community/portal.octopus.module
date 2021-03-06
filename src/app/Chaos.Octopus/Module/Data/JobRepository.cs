﻿using System;

namespace Chaos.Octopus.Module.Data
{
    using System.Collections.Generic;
    using CHAOS.Data.MySql;
    using Model;
    using MySql.Data.MySqlClient;
    using Portal.Core.Exceptions;

    public interface IJobRepository
    {
        IEnumerable<Job> Get(string id, string status);
        IEnumerable<Job> GetIncomplete();
        void Set(string id, string status, string data, Guid createdByUserId);
    }

    public class JobRepository : IJobRepository
    {
        public Gateway Gateway { get; set; }

        public JobRepository(Gateway gateway)
        {
            Gateway = gateway;
        }

        public IEnumerable<Job> Get(string id, string status)
        {
            return Gateway.ExecuteQuery<Job>("Job_Get", new[]
                {
                    new MySqlParameter("Id", id),
                    new MySqlParameter("Status", status)
                });
        }

        public IEnumerable<Job> GetIncomplete()
        {
            return Gateway.ExecuteQuery<Job>("Job_GetIncomplete");
        }

        public void Set(string id, string status, string data, Guid createdByUserId)
        {
            var result = Gateway.ExecuteNonQuery("Job_Set", new[]
                {
                    new MySqlParameter("Id", id),
                    new MySqlParameter("Status", status),
                    new MySqlParameter("Data", data),
                    new MySqlParameter("CreatedByUserId", createdByUserId.ToByteArray())
                });
            
            if(result == 0)
                throw new UnhandledException("Job wasn't set");
        }
    }
}