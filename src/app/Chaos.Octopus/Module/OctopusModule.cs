using Chaos.Octopus.Module.Extension.Dto;

namespace Chaos.Octopus.Module
{
  using System.Xml.Linq;
  using Bindings;
  using CHAOS.Serialization;
  using CHAOS.Serialization.Standard;
  using Data;
  using Extension.v6;
  using Portal.Core;
  using Portal.Core.Data.Model;
  using Portal.Core.Module;

  public class OctopusModule : IModuleConfig
  {
    public IPortalApplication PortalApplication { get; private set; }
    public IOctopusRepository OctopusRepository { get; private set; }

    public void Load(IPortalApplication portalApplication)
    {
      PortalApplication = portalApplication;
      PortalApplication.Bindings.Add(typeof (Extension.Dto.Job), new JobParameterBinding());

      var module = PortalApplication.PortalRepository.ModuleGet("Octopus");
      var config = OctopusConfig.Create(module);

      OctopusRepository = new OctopusRepository(config.ConnectionString);

			portalApplication.MapRoute("/v6/Job", () => new Job(portalApplication, OctopusRepository));
      portalApplication.MapRoute("/v6/Heartbeat", () => new Heartbeat(portalApplication, OctopusRepository.Heartbeat));
      portalApplication.AddBinding(typeof(ClusterState), new JsonParameterBinding<ClusterState>());
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