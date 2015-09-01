using System.Collections.Generic;

namespace Chaos.Octopus.Module.Data.Model
{
  public class ClusterState
  {
    public int JobsInQueue { get; set; }
    public IList<AgentState> Agents { get; set; }
  }

  public class AgentState
  {
    public string Hostname { get; set; }
    public int Port { get; set; }
    public string State { get; set; }
    public bool HasAvailableSlots { get; set; }
  }
}