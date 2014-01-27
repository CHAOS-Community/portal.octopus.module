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

        [Serialize]
        public string Id { get; set; }

        [Serialize]
        public string Status { get; set; }

        [Serialize]
        public string Data { get; set; }

        [Serialize]
        public DateTime DateCreated { get; set; }
    }
}