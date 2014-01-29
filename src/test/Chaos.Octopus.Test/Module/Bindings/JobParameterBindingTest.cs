namespace Chaos.Octopus.Test.Module.Bindings
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Octopus.Module.Bindings;
    using Octopus.Module.Extension.Dto;

    [TestFixture]
    public class JobParameterBindingTest
    {
         [Test]
         public void Bing_WithJobJson_ReturnJobDto()
         {
             var json = Make_JobJson();
             var binding = new JobParameterBinding();
             var parameters = new Dictionary<string, string> {{"data", json}};
             var parameter = typeof (MyClass).GetMethod("Test").GetParameters().First();
                
             var result = (Job) binding.Bind(parameters, parameter);

             Assert.That(result.Id, Is.EqualTo("0123456789"));
             Assert.That(result.Data, Is.EqualTo(json));
         }

         private string Make_JobJson()
         {
             return "{\"id\":\"0123456789\", \"steps\":[{\"tasks\":[{\"pluginId\":\"com.chaos.octopus.agent.unit.TestPlugin, 1.0.0\",\"properties\":{\"sleep\":\"3000\",\"number\":\"2\"}}]}]}";
         }

        class MyClass
        {
            public void Test(Job job)
            {
                
            }
        }
    }
}