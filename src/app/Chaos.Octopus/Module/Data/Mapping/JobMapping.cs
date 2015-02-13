namespace Chaos.Octopus.Module.Data.Mapping
{
    using System.Collections.Generic;
    using System.Data;
    using CHAOS.Data;
    using Model;

    public class JobMapping : IReaderMapping<Job>
    {
        public IEnumerable<Job> Map(IDataReader reader)
        {
            while (reader.Read())
            {
                yield return new Job
                    {
                        Id = reader.GetString("Id"),
                        Status = reader.GetString("Status"),
                        Data = reader.GetString("Data"),
                        DateCreated = reader.GetDateTime("DateCreated")
                    };
            }
        }
    }
}