using CHAOS.Serialization;
using Chaos.Portal.Core.Data.Model;

namespace Chaos.Octopus.Module.Extension.Dto
{
  public class ClusterState : AResult
  {
    [Serialize]
    public int ConnectedAgents { get; set; }
    
    [Serialize]
    public int JobsInQueue { get; set; }
  }
}