using Chaos.Octopus.Module.Data;
using Chaos.Portal.Core;
using Chaos.Portal.Core.Exceptions;
using Chaos.Portal.Core.Extension;
using Chaos.Portal.v5.Extension.Result;

namespace Chaos.Octopus.Module.Extension.v6
{
  public class Heartbeat : AExtension
  {
    public IHeartbeatRepository HeartbeatRepository { get; set; }

    public Heartbeat(IPortalApplication portalApplication, IHeartbeatRepository heartbeatRepository) : base(portalApplication)
    {
      HeartbeatRepository = heartbeatRepository;
    }

    public EndpointResult Set()
    {
      if(Request.IsAnonymousUser) throw new InsufficientPermissionsException();

      HeartbeatRepository.Set();

      return EndpointResult.Success();
    }
  }
}