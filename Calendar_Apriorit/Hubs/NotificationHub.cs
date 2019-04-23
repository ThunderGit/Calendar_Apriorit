using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;


namespace Calendar_Apriorit.Hubs
{
    public class NotificationHub : Hub
    {
        public override Task OnConnected()
        {
          
            string name = Context.User.Identity.Name;
            Groups.Add(Context.ConnectionId, name);
            return base.OnConnected();
        }

    }
}