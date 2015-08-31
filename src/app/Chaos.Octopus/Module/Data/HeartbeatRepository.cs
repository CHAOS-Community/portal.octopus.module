using CHAOS.Data.MySql;
using Chaos.Portal.Core.Exceptions;

namespace Chaos.Octopus.Module.Data
{
  public class HeartbeatRepository : IHeartbeatRepository
  {
    public Gateway Gateway { get; set; }

    public HeartbeatRepository(Gateway gateway)
    {
      Gateway = gateway;
    }

    public void Set()
    {
      var result = Gateway.ExecuteNonQuery("Heartbeat_Set");

      if(result != 1) throw new UnhandledException("Heartbeat wasn't set");
    }
  }
}