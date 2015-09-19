using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Lamp.Server.Hubs
{

    public class LampHub : Hub
    {
        public static bool TurnOn { get; set; } 
    }
}