﻿using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesUsuarioController : Controller
    {
        private readonly IRolesUser _rolesUsuario;
        public RolesUsuarioController(IRolesUser roles)
        {
            _rolesUsuario = roles;
        }
        [HttpGet("GetRolesUsuario")]
        public async Task<IActionResult> GetRolesUsuario(int idUsuario)
        {
            return await _rolesUsuario.ListarRolesUsuario(idUsuario);
        }

        [HttpPost("AgregarRolUsuario")]
        public async Task<IActionResult> AgregarRolUsuario(RolesUsuario rolUsuario)
        {
            return await _rolesUsuario.CrearRolUsuario(rolUsuario);
        }

        [HttpPost("EliminarRolUsuario")]
        public async Task<IActionResult> EliminarRolUsuario(int idRol, int idUsuario)
        {
            return await _rolesUsuario.EliminarRolUsuario(idRol,idUsuario);
        }

    }
}
