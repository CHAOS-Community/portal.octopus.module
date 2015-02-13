namespace Chaos.Octopus.Module.Extension.Dto
{
  using System;
  using CHAOS.Serialization;
  using Portal.Core.Data.Model;

  public class Job : AResult
  {
    public static Job Create(Data.Model.Job job)
    {
      return new Job
        {
          Id = job.Id,
          Status = job.Status,
          Data = job.Data,
          DateCreated = job.DateCreated
        };
    }

    [Serialize("id")]
    [Newtonsoft.Json.JsonProperty("id")]
    public string Id { get; set; }

    [Serialize("status")]
    [Newtonsoft.Json.JsonProperty("status")]
    public string Status { get; set; }

    [Serialize("data")]
    [Newtonsoft.Json.JsonProperty("data")]
    public string Data { get; set; }

    [Serialize("datecreated")]
    [Newtonsoft.Json.JsonProperty("datecreated")]
    public DateTime DateCreated { get; set; }

    public Guid CreatedByUserId { get; set; }
  }
}