namespace Chaos.Octopus.Module
{
    using System.Collections.Generic;
    using Extension.v6;
    using Portal.Core;
    using Portal.Core.Exceptions;
    using Portal.Core.Extension;
    using Portal.Core.Module;

    public class OctopusModule : IModule
    {
        public IPortalApplication PortalApplication { get; private set; }

        public IEnumerable<string> GetExtensionNames(Protocol version)
        {
            yield return "Job";
        }

        public IExtension GetExtension(Protocol version, string name)
        {
            if (version == Protocol.V6)
            {
                if ("Job".Equals(name))
                    return new Job(PortalApplication);
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
        }
    }
}
