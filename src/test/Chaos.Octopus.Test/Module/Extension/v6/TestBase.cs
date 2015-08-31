using Chaos.Portal.Core;
using Chaos.Portal.Core.Request;
using Moq;
using NUnit.Framework;

namespace Chaos.Octopus.Test.Module.Extension.v6
{
  public class TestBase
  {
    protected Mock<IPortalRequest> PortalRequest;
    protected Mock<IPortalApplication> Portal;

    [SetUp]
    public void SetUp()
    {
      PortalRequest = new Mock<IPortalRequest>();
      Portal = new Mock<IPortalApplication>();
    }
  }
}