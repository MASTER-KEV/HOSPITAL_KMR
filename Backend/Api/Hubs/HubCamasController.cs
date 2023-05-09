using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.Json.Serialization;
using AccesoDatos;
using Microsoft.EntityFrameworkCore;

namespace Api.Hubs
{
    public class HubCamasController : Hub
    {
        public Task UnirseAlGrupo(string idSucursal)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, idSucursal);
           
        }
        public Task AbandonarGrupo(string idSucursal)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, idSucursal);

        }

    }
}
