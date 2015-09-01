using System.Collections.Generic;
using CHAOS.Serialization;
using Chaos.Portal.Core.Data.Model;

namespace Chaos.Octopus.Module.Extension.Dto
{
  public class ClusterState : AResult
  {
    [Serialize("jobsInQueue")]
    public int JobsInQueue { get; set; }

    [Serialize("agents")]
    public IList<AgentState> Agents { get; set; }

    public ClusterState()
    {
      Agents = new List<AgentState>();
    }
  }

  public class AgentState
  {
    [Serialize("hostname")]
    public string Hostname { get; set; }

    [Serialize("port")]
    public int Port { get; set; }

    [Serialize("state")]
    public string State { get; set; }

    [Serialize("hasAvailableSlots")]
    public bool HasAvailableSlots { get; set; }
  }
}