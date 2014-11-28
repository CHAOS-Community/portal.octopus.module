namespace Chaos.Octopus.Module.Extension.v6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;
    using Portal.Core;
    using Portal.Core.Extension;

    public class Job : AExtension
    {
        public IOctopusRepository Repository { get; set; }

        public Job(IPortalApplication portalApplication, IOctopusRepository repository) : base(portalApplication)
        {
            Repository = repository;
        }

        // TODO: Requires Authentication
        public IEnumerable<Dto.Job> Get(string id, string status)
        {
            var results = Repository.Job.Get(id, status);

            return results.Select(Dto.Job.Create);
        }

        public IEnumerable<Dto.Job> GetIncomplete()
        {
            var results = Repository.Job.GetIncomplete();

            return results.Select(Dto.Job.Create);
        }

        public Dto.Job Set(Dto.Job job)
        {
            if (string.IsNullOrEmpty(job.Id))
            {
                job.Id = Guid.NewGuid().ToString();
                job.Status = "new";
                job.DateCreated = DateTime.UtcNow;
            }
            else if (string.IsNullOrEmpty(job.Status)) // TOTO: Improve message
                throw new MissingFieldException("Status missing");

            Repository.Job.Set(job.Id, job.Status, job.Data);

            return job;
        }
    }
}
