namespace Chaos.Octopus.Test.Module.Extension.Dto
{
    using System;
    using NUnit.Framework;
    using Octopus.Module.Data.Model;

    [TestFixture]
    public class JobTest
    {
         [Test]
         public void Create_GivenModel_ReturnDto()
         {
             var job = new Job {Id = "id", Status = "status", Data = "data", DateCreated = new DateTime(2000, 01, 01)};

             var result = Octopus.Module.Extension.Dto.Job.Create(job);

             Assert.That(result.Id, Is.EqualTo(job.Id));
             Assert.That(result.Status, Is.EqualTo(job.Status));
             Assert.That(result.Data, Is.EqualTo(job.Data));
             Assert.That(result.DateCreated, Is.EqualTo(job.DateCreated));
         }
    }
}