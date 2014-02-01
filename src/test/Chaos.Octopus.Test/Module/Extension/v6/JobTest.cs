namespace Chaos.Octopus.Test.Module.Extension.v6
{
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using Octopus.Module.Data;
    using Octopus.Module.Extension.v6;
    using Portal.Core;

    [TestFixture]
    public class JobTest
    {
        [Test]
        public void Get_GivenStatus_ReturnListFromRepository()
        {
            var repository = new Mock<IOctopusRepository>();
            var portal = new Mock<IPortalApplication>();
            var extension = new Job(portal.Object, repository.Object);
            var expected = new[] { new Octopus.Module.Data.Model.Job(), new Octopus.Module.Data.Model.Job() };
            repository.Setup(m => m.Job.Get("status")).Returns(expected);

            var results = extension.Get("status");

            Assert.That(results.Count(), Is.EqualTo(2));
        }
        
        [Test]
        public void GetIncomplete_Default_ReturnListFromRepository()
        {
            var repository = new Mock<IOctopusRepository>();
            var portal = new Mock<IPortalApplication>();
            var extension = new Job(portal.Object, repository.Object);
            var expected = new[] { new Octopus.Module.Data.Model.Job(), new Octopus.Module.Data.Model.Job() };
            repository.Setup(m => m.Job.GetIncomplete("status")).Returns(expected);

            var results = extension.GetIncomplete(;

            Assert.That(results.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Set_GivenJob_CallRepository()
        {
            var repository = new Mock<IOctopusRepository>();
            var portal = new Mock<IPortalApplication>();
            var extension = new Job(portal.Object, repository.Object);
            var job = Make_JobDto();
            repository.Setup(m => m.Job.Set(job.Id, job.Status, job.Data));

            extension.Set(job);

            repository.VerifyAll();
        }

        [Test]
        public void Set_GivenJobWithoutId_StatusShouldBeSetToNewAndIdGenerated()
        {
            var repository = new Mock<IOctopusRepository>();
            var portal = new Mock<IPortalApplication>();
            var extension = new Job(portal.Object, repository.Object);
            var job = Make_NewJobDto();
            repository.Setup(m => m.Job.Set(It.IsAny<string>(), "new", job.Data));

            extension.Set(job);

            repository.VerifyAll();
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