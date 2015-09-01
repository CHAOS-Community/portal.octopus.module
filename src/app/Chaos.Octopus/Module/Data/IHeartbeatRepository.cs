using Chaos.Octopus.Module.Data.Model;

namespace Chaos.Octopus.Module.Data
{
  public interface IHeartbeatRepository
  {
    void Set(ClusterState state);
  }
}