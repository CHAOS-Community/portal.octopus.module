namespace Chaos.Octopus.Module.Data
{
    using System;
    using CHAOS.Data;
    using CHAOS.Data.MySql;
    using Mapping;
    using Model;

    public class OctopusRepository
    {
        private readonly Gateway _Gateway;

        public OctopusRepository(string connectionString)
        {
            _Gateway = new Gateway(connectionString);

            Job = new JobRepository(_Gateway);
        }

        static OctopusRepository()
        {
            ReaderExtensions.Mappings.Add(typeof(Job), new JobMapping());
        }

        private JobRepository Job { get; set; }
    }
}
