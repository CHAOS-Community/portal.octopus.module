namespace Chaos.Octopus.Module.Data
{
  using CHAOS.Data;
  using CHAOS.Data.MySql;
  using Mapping;
  using Model;

  public interface IOctopusRepository
  {
    IJobRepository Job { get; set; }
    IHeartbeatRepository Heartbeat { get; set; }
  }

  public class OctopusRepository : IOctopusRepository
  {
    private readonly Gateway _Gateway;

    public OctopusRepository(string connectionString)
    {
      _Gateway = new Gateway(connectionString);

      Job = new JobRepository(_Gateway);
      Heartbeat = new HeartbeatRepository(_Gateway);
    }

    static OctopusRepository()
    {
      ReaderExtensions.Mappings.Add(typeof (Job), new JobMapping());
    }

    public IJobRepository Job { get; set; }
    public IHeartbeatRepository Heartbeat { get; set; }
  }
}