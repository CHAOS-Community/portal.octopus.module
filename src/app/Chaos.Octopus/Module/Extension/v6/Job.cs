namespace Chaos.Octopus.Module.Extension.v6
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Data;
  using Portal.Core;
  using Portal.Core.Exceptions;
  using Portal.Core.Extension;

  public class Job : AExtension
  {
    public IOctopusRepository Repository { get; set; }

    public Job(IPortalApplication portalApplication, IOctopusRepository repository) : base(portalApplication)
    {
      Repository = repository;
    }

    public IEnumerable<Dto.Job> Get(string id, string status)
    {
      EnsureUserIsAuthenticated("Job/GetIncomplete");

      var results = Repository.Job.Get(id, status);

      return results.Select(Dto.Job.Create);
    }

    private void EnsureUserIsAuthenticated(string jobGet)
    {
      if (Request.IsAnonymousUser)
      {
        throw new InsufficientPermissionsException(string.Format("{0} not available for anonymous users", jobGet));
      }
    }

    public IEnumerable<Dto.Job> GetIncomplete()
    {
      EnsureUserIsAuthenticated("Job/Get");

      var results = Repository.Job.GetIncomplete();

      return results.Select(Dto.Job.Create);
    }

    public Dto.Job Set(Dto.Job job)
    {
      EnsureUserIsAuthenticated("Job/Set");

      if (string.IsNullOrEmpty(job.Id))
      {
        job.Id = Guid.NewGuid().ToString();
        job.Status = "new";
        job.DateCreated = DateTime.UtcNow;
        job.CreatedByUserId = Request.User.Guid;
      }
      else if (string.IsNullOrEmpty(job.Status))
        throw new MissingFieldException("Status missing");

      Repository.Job.Set(job.Id, job.Status, job.Data, job.CreatedByUserId);

      return job;
    }
  }
}