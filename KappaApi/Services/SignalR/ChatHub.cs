using Microsoft.AspNetCore.SignalR;
using System.Threading;

namespace KappaApi.Services.SignalR
{
    public class ChatHub : Hub
    {
        private readonly System.Timers.Timer timer;
      
        public async Task LogUser(string userName) 
        {
            
            var cId = Context.ConnectionId;
            await Clients.All.SendAsync("ReceivedUser", userName, cId);
        }
        public async Task SendMessage(string message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
        }

        public async Task SendUpdate(System.Timers.ElapsedEventArgs e)
        {
            await Clients.All.SendAsync("ReceiveUpdate", "update");
        }
      




    }
}
