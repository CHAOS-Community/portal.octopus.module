namespace Chaos.Octopus.Test.Module
{
    using System;
    using Moq;
    using NUnit.Framework;
    using Octopus.Module;
    using Octopus.Module.Bindings;
    using Portal.Core;
    using Portal.Core.Data.Model;

    [TestFixture]
    public class OctopusModuleTest
    {
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
