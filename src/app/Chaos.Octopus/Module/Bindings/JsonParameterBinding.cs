namespace Chaos.Octopus.Module.Bindings
{
  using System.Collections.Generic;
  using System.Reflection;
  using Portal.Core.Bindings;

  public class JsonParameterBinding<T> : IParameterBinding
  {
    public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
    {
      var json = parameters[parameterInfo.Name];

      return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    }
  }
}