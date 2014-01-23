namespace Chaos.Octopus.Test.Module
{
    using Moq;
    using NUnit.Framework;
    using Octopus.Module;
    using Octopus.Module.Extension.v6;
    using Portal.Core;
    using Portal.Core.Exceptions;

    [TestFixture]
    public class OctopusModuleTest
    {
        [Test]
        public void GetExtensionNames_LatestProtocol_ShouldContainJob()
        {
            var module = new OctopusModule();

            var results = module.GetExtensionNames(Protocol.Latest);

            Assert.That(results, Contains.Item("Job"));
        }

        [Test]
        public void GetExtension_GivenJob_ReturnJobExtension()
        {
            var module = new OctopusModule();

            var result = module.GetExtension(Protocol.Latest, "Job");

            Assert.That(result, Is.AssignableFrom<Job>());
        }

        [Test]
        public void GetExtension_GivenJobType_ReturnJobExtensionInstance()
        {
            var module = new OctopusModule();

            var result = module.GetExtension<Job>(Protocol.Latest);

            Assert.That(result, Is.AssignableFrom<Job>());
        }

        [Test, ExpectedException(typeof(ExtensionMissingException))]
        public void GetExtension_GivenUnsupportedExtensionName_ThrowException()
        {
            var module = new OctopusModule();

            module.GetExtension(Protocol.Latest, "missing");
        }

        [Test]
        public void Load_Default_PortalApplicationIsSet()
        {
            var module = new OctopusModule();
            var portal = new Mock<IPortalApplication>();

            module.Load(portal.Object);

            Assert.That(module.PortalApplication, Is.Not.Null);
        }
    }
}
