using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IRolesUser
    {
        public Task<IActionResult> CrearRolUsuario(RolesUsuario rolUsuario);
        //Eliminar Rol usuario
        public Task<IActionResult> EliminarRolUsuario(int idRol, int idUsuario);
        //Listar Roles Usuario
        public Task<IActionResult> ListarRolesUsuario(int idUsuario);
    }
}
