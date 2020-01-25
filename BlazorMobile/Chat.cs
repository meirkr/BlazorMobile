using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorMobile
{
    public partial class Chat
    {
        private HubConnection _hubConnection;
        private string _lastMsg;

        //public Chat(IHubConnectionBuilder hubConnectionBuilder)
        //{
        //    this.hubConnectionBuilder = hubConnectionBuilder;
        //}
        protected async override Task OnInitializedAsync()
        {
            const string url = "http://meirkr.com/chat";
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();


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

            _hubConnection.On<string, string>("sendToAll", (user, msg)
                =>
            {
                //_user = user;
                _lastMsg = msg;

                base.StateHasChanged();
            });



            await base.OnInitializedAsync();
        }

        private void OnClick()
        {
            _ = StartSignalr();

        }

        private async Task StartSignalr()
        {
            try
            {
                await _hubConnection.StartAsync();
                System.Diagnostics.Trace.WriteLine("---------- connected!");
            }
            catch(Exception e)
            {
                System.Diagnostics.Trace.WriteLine("---------- exception:\n!" + e);

            }
        }
    }

}