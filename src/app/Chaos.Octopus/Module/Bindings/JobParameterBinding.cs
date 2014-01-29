namespace Chaos.Octopus.Module.Bindings
{
    using System.Collections.Generic;
    using System.Reflection;
    using Extension.Dto;
    using Portal.Core.Bindings;

    public class JobParameterBinding : IParameterBinding
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            var json = parameters["data"];

            var job = Newtonsoft.Json.JsonConvert.DeserializeObject<Job>(json);
            job.Data = json;

            return job;
        }
    }
}