using AccesoDatos;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("")]
    public class LoginController : Controller
    {
        private IJtAuth aut;
        private readonly DataBaseContext _context;
        public LoginController(IJtAuth auts, DataBaseContext ctx)
        {
            aut = auts;
            _context = ctx;
        }

        [HttpPost("Login")]
        public IActionResult Login(Login login)
        {
            LoginReturn user = aut.GetUser(login.username, login.password);
            if (!user.username.Equals(null))
            {
                var token = aut.Autentication(login.username, login.password);
                if(token == null)
                {
                    return Unauthorized();
                }
                user.token = token;
                Usuario us = _context.Usuarios.Where(e => e.Username == login.username).First();
                user.Nombres = us.Nombres;
                user.apellidos = us.Apellidos;
                return Ok(user);
            }
            return Unauthorized();
        }
        
    }
}
