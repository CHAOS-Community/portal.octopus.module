using Chaos.Portal.Core.Data.Model;

namespace Chaos.Octopus.Test.Module.Extension.v6
{
  using System.Linq;
  using Moq;
  using NUnit.Framework;
  using Octopus.Module.Data;
  using Octopus.Module.Extension.v6;
  using Portal.Core;

  [TestFixture]
  public class JobTest : TestBase
  {
    [Test]
    public void Get_GivenStatus_ReturnListFromRepository()
    {
      var repository = new Mock<IOctopusRepository>();
      var extension = Make_Extension(Portal, repository);
      var expected = new[] {new Octopus.Module.Data.Model.Job(), new Octopus.Module.Data.Model.Job()};
      repository.Setup(m => m.Job.Get("Id", "status")).Returns(expected);

      var results = extension.Get("Id", "status");

      Assert.That(results.Count(), Is.EqualTo(2));
    }

    [Test]
    public void GetIncomplete_Default_ReturnListFromRepository()
    {
      var repository = new Mock<IOctopusRepository>();
      var extension = Make_Extension(Portal, repository);
      var expected = new[] {new Octopus.Module.Data.Model.Job(), new Octopus.Module.Data.Model.Job()};
      repository.Setup(m => m.Job.GetIncomplete()).Returns(expected);

      var results = extension.GetIncomplete();

      Assert.That(results.Count(), Is.EqualTo(2));
    }

    [Test]
    public void Set_GivenJob_CallRepository()
    {
      var repository = new Mock<IOctopusRepository>();
      var extension = Make_Extension(Portal, repository);
      var job = Make_JobDto();
      repository.Setup(m => m.Job.Set(job.Id, job.Status, job.Data, job.CreatedByUserId));

      extension.Set(job);

      repository.VerifyAll();
    }

    [Test]
    public void Set_GivenJobWithoutId_StatusShouldBeSetToNewAndIdGenerated()
    {
      var repository = new Mock<IOctopusRepository>();
      var extension = Make_Extension(Portal, repository);
      var job = Make_NewJobDto();
      PortalRequest.Setup(p => p.User).Returns(new UserInfo());
      repository.Setup(m => m.Job.Set(It.IsAny<string>(), "new", job.Data, job.CreatedByUserId));

      extension.Set(job);

      repository.VerifyAll();
    }

    private Job Make_Extension(Mock<IPortalApplication> portal, Mock<IOctopusRepository> repository)
    {
      return (Job) new Job(portal.Object, repository.Object).WithPortalRequest(PortalRequest.Object);
    }

    private static Octopus.Module.Extension.Dto.Job Make_JobDto()
    {
      return new Octopus.Module.Extension.Dto.Job
        {
          Id = "id",
          Status = "status",
          Data = "{'Id':'id', 'Status':'status', 'steps':[]}"
        };
    }

    private static Octopus.Module.Extension.Dto.Job Make_NewJobDto()
    {
      return new Octopus.Module.Extension.Dto.Job
        {
          Data = "{'Id':'id', 'Status':'status', 'steps':[]}"
        };
    }
  }
}