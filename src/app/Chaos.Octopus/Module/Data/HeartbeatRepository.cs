using CHAOS.Data.MySql;
using Chaos.Octopus.Module.Data.Model;
using Chaos.Portal.Core.Exceptions;
using MySql.Data.MySqlClient;

namespace Chaos.Octopus.Module.Data
{
  public class HeartbeatRepository : IHeartbeatRepository
  {
    public Gateway Gateway { get; set; }

    public HeartbeatRepository(Gateway gateway)
    {
      Gateway = gateway;
    }

    public void Set(ClusterState state)
    {
      var result = Gateway.ExecuteNonQuery("Heartbeat_Set",
        new MySqlParameter("ClusterState", Newtonsoft.Json.JsonConvert.SerializeObject(state)));

      if(result != 1) throw new UnhandledException("Heartbeat wasn't set");
    }
  }
}