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
    }
}