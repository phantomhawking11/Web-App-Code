using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lamp.Server.Hubs;
using Microsoft.AspNet.SignalR;

namespace Lamp.Server.Controllers
{
    public class LampController : ApiController
    {
        public void Get(bool turnOn)
        {
            var lampContex = GlobalHost.ConnectionManager.GetHubContext<LampHub>();
            LampHub.TurnOn = turnOn;

            lampContex.Clients.All.OnSwitch(turnOn);
        }
    }
}
