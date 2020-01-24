using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace BlazorMobile
{
    public partial class Chat
    {
        private HubConnection _hubConnection;
        string _connectId;
        private string _lastMsg;

        //public Chat(IHubConnectionBuilder hubConnectionBuilder)
        //{
        //    this.hubConnectionBuilder = hubConnectionBuilder;
        //}
        protected async override Task OnInitializedAsync()
        {
            const string url = "ws://meirkr.com/chat";
            _hubConnection = new HubConnection(url);


            //_hubConnection.Reconnected += (connected =>
            //    {
            //        _connectId = connected;
            //        base.StateHasChanged();
            //        return Task.CompletedTask;
            //    });


            //    On<string>("sendToAll", msg =>
            //{
            //    _lastMsg = msg;
            //    base.StateHasChanged();
            //});
            _hubConnection.Received +=
                (msg =>
                {
                    this._lastMsg = msg;
                });

            await _hubConnection.Start();

            await base.OnInitializedAsync();
        }


    }

}