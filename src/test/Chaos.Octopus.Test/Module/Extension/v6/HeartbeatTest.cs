using Chaos.Octopus.Module.Data;
using Chaos.Octopus.Module.Extension.v6;
using Chaos.Portal.Core.Exceptions;
using Moq;
using NUnit.Framework;

namespace Chaos.Octopus.Test.Module.Extension.v6
{
  [TestFixture]
  public class HeartbeatTest : TestBase
  {
    private Mock<IHeartbeatRepository> HeartbeatRepository { get; set; }

    [SetUp]
    public new void SetUp()
    {
      base.SetUp();

      HeartbeatRepository = new Mock<IHeartbeatRepository>();
    }

    [Test, ExpectedException(typeof (InsufficientPermissionsException))]
    public void Set_GivenAnonymousUser_Throw()
    {
      var ext = Make_HeartbeatExtension();
      PortalRequest.Setup(p => p.IsAnonymousUser).Returns(true);

      ext.Set();
    }
    
    [Test]
    public void Set_GivenAuthenticatedUser_SetOnRepository()
    {
      var ext = Make_HeartbeatExtension();

      ext.Set();

      HeartbeatRepository.Verify(m => m.Set());
    }

    private Heartbeat Make_HeartbeatExtension()
    {
      return (Heartbeat)new Heartbeat(Portal.Object, HeartbeatRepository.Object).WithPortalRequest(PortalRequest.Object);
    }
  }
}