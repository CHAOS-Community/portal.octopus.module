namespace Chaos.Octopus.Module.Extension.v6
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Portal.Core;
    using Portal.Core.Extension;

    public class Job : AExtension
    {
        public IOctopusRepository Repository { get; set; }

        public Job(IPortalApplication portalApplication, IOctopusRepository repository) : base(portalApplication)
        {
            Repository = repository;
        }

        public IEnumerable<Dto.Job> Get(string status)
        {
            var results = Repository.Job.Get(status);

            return results.Select(Dto.Job.Create);
        }
    }
}
