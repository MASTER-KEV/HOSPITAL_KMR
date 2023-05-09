using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AccesoDatos;
using Microsoft.EntityFrameworkCore;

namespace Servicios.Servicios
{
    public class RolesUser: IRolesUser
    {
        private readonly DataBaseContext _context;
        private Errores _error;
        //Constructor
        public RolesUser(DataBaseContext ctx)
        {
            _context = ctx;
            _error = new Errores();
        }
        public async Task<IActionResult> CrearRolUsuario(AccesoDatos.RolesUsuario rolUsuario)
        {
            try
            {
                SequenceValueGenerator generator = new SequenceValueGenerator("USR", "SEC_CREARROLUSUARIO");
                decimal id = generator.NextValue(_context);
                rolUsuario.IdRolUsuario = id;
                rolUsuario.Estado = "A";
                await _context.AddAsync(rolUsuario);
                await _context.SaveChangesAsync();
                //_context.RolesUsuarios.Add(rolUsuario);
                //await _context.SaveChangesAsync();
                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de asignar el rol al usuario", ex);
            }
        }
        //Eliminar Rol usuario
        public async Task<IActionResult> EliminarRolUsuario(int idRol, int idUsuario)
        {
            try
            {
                var rolUsuario = await _context.RolesUsuarios.Where(e => e.IdUsuario == idUsuario && e.IdRol == idRol && e.Estado == "A").FirstOrDefaultAsync();
                if (rolUsuario == null)
                {
                    return _error.respuestaDeError("El usuario no cuenta con el rol solicitado");
                }
                _context.Entry(rolUsuario).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de eliminar el rol del usuario", ex);
            }
        }
        //Listar Roles Usuario
        public async Task<IActionResult> ListarRolesUsuario(int idUsuario)
        {
            try
            {
                var Roles = await _context.RolesUsuarios.Where(n => n.IdUsuario == idUsuario && n.Estado == "A")
                            .Include(e => e.IdRolNavigation)
                            .Include(e => e.IdUsuarioNavigation)
                            .ToListAsync();

                return new ObjectResult(Roles) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de obtener los roles del usuario.", ex);
            }
        }
    }
}
