using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using AccesoDatos;
namespace Api.Controllers
{
    [ApiController]
    [Route("")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarios _users;

        public UsuariosController(IUsuarios usr)
        {
            _users = usr;
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario(Usuario user)
        {
            return await _users.CrearUsuario(user);
        }
    }
}
