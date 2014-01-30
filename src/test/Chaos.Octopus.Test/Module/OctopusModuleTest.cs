namespace Chaos.Octopus.Test.Module
{
    using System;
    using Moq;
    using NUnit.Framework;
    using Octopus.Module;
    using Octopus.Module.Bindings;
    using Octopus.Module.Extension.v6;
    using Portal.Core;
    using Portal.Core.Data.Model;
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
            var portal = Make_PortalApplication();

            module.Load(portal.Object);

            Assert.That(module.PortalApplication, Is.Not.Null);
        }

        [Test]
        public void Load_Default_OctopusRepositoryIsSet()
        {
            var module = new OctopusModule();
            var portal = Make_PortalApplication();

            module.Load(portal.Object);

            Assert.That(module.OctopusRepository, Is.Not.Null);
        }

        [Test]
        public void Load_Default_JobParameterBindingIsAdded()
        {
            var module = new OctopusModule();
            var portal = Make_PortalApplication();

            module.Load(portal.Object);

            portal.Verify(m => m.Bindings.Add(typeof(Octopus.Module.Extension.Dto.Job), It.IsAny<JobParameterBinding>())); ;
        }

        private Mock<IPortalApplication> Make_PortalApplication()
        {
            var portal = new Mock<IPortalApplication>();
            portal.Setup(m => m.Bindings.Add(It.IsAny<Type>(), It.IsAny<JobParameterBinding>()));
            portal.Setup(m => m.PortalRepository.ModuleGet("Octopus")).Returns(Make_OctopusModuleConfig());

            return portal;
        }

        private Module Make_OctopusModuleConfig()
        {
            return new Module
                {
                    Configuration = "<OctopusConfig><ConnectionString>some connectionstring</ConnectionString></OctopusConfig>"
                };
        }
    }
}
