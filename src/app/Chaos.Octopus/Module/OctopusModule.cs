namespace Chaos.Octopus.Module
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using CHAOS.Serialization;
    using CHAOS.Serialization.Standard;
    using Data;
    using Extension.v6;
    using Portal.Core;
    using Portal.Core.Data.Model;
    using Portal.Core.Exceptions;
    using Portal.Core.Extension;
    using Portal.Core.Module;

    public class OctopusModule : IModule
    {
        public IPortalApplication PortalApplication { get; private set; }
        public IOctopusRepository OctopusRepository { get; private set; }

        public IEnumerable<string> GetExtensionNames(Protocol version)
        {
            yield return "Job";
        }

        public IExtension GetExtension(Protocol version, string name)
        {
            if (version == Protocol.V6)
            {
                if ("Job".Equals(name))
                    return new Job(PortalApplication, OctopusRepository);
            }

            throw new ExtensionMissingException(name);
        }

        public IExtension GetExtension<TExtension>(Protocol version) where TExtension : IExtension
        {
            return GetExtension(version, typeof (TExtension).Name);
        }

        public void Load(IPortalApplication portalApplication)
        {
            PortalApplication = portalApplication;

            var module = PortalApplication.PortalRepository.ModuleGet("Octopus");
            var config = OctopusConfig.Create(module);
            
            OctopusRepository = new OctopusRepository(config.ConnectionString);
        }
    }

    [Serialize]
    public class OctopusConfig
    {
        [Serialize]
        public string ConnectionString { get; set; }

        public static OctopusConfig Create(Module module)
        {
            var xml = XDocument.Parse(module.Configuration);

            return SerializerFactory.XMLSerializer.Deserialize<OctopusConfig>(xml);
        }
    }
}
