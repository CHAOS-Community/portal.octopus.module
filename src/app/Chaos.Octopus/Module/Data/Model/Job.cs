namespace Chaos.Octopus.Module.Data.Model
{
    using System;

    public class Job
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public string Data { get; set; }

        public DateTime DateCreated { get; set; }
    }
}