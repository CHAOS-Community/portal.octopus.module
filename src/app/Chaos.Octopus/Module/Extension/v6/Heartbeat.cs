﻿using System.Linq;
using Chaos.Octopus.Module.Data;
using Chaos.Octopus.Module.Extension.Dto;
using Chaos.Portal.Core;
using Chaos.Portal.Core.Exceptions;
using Chaos.Portal.Core.Extension;
using Chaos.Portal.v5.Extension.Result;

namespace Chaos.Octopus.Module.Extension.v6
{
  public class Heartbeat : AExtension
  {
    public IHeartbeatRepository HeartbeatRepository { get; set; }

    public Heartbeat(IPortalApplication portalApplication, IHeartbeatRepository heartbeatRepository) : base(portalApplication)
    {
      HeartbeatRepository = heartbeatRepository;
    }

    public EndpointResult Set(ClusterState state = null)
    {
      if(Request.IsAnonymousUser) throw new InsufficientPermissionsException();

      HeartbeatRepository.Set(new Data.Model.ClusterState
        {
          JobsInQueue = state.JobsInQueue,
          Agents = state.Agents.Select(a => new Data.Model.AgentState
            {
              Hostname = a.Hostname,
              Port = a.Port,
              HasAvailableSlots = a.HasAvailableSlots,
              State = a.State
            }).ToList()
        });

      return EndpointResult.Success();
    }
  }
}