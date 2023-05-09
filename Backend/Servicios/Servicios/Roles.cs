using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class Roles:IRoles
    {
        private readonly DataBaseContext _context;
        private Errores _error;
        public Roles(DataBaseContext ctx)
        {
            _context = ctx;
            _error = new Errores();
        }

        public async Task<IActionResult> CrearRol(Role rol)
        {
            try
            {
                var roleBuscado = _context.Roles.Where(e => e.Nombre == rol.Nombre.Trim() && e.Column1== "A").FirstOrDefault();
                if (roleBuscado != null)
                {
                    return _error.respuestaDeError("El rol '"+rol.Nombre+"' ya existe");
                }
                Role rolNuevo = new Role();
                rolNuevo.Nombre = rol.Nombre;   
                rolNuevo.Column1 = "A";

                SequenceValueGenerator generator = new SequenceValueGenerator("USR", "SEC_ROL");
                decimal id = generator.NextValue(_context);
                rol.IdRol = id;
                await _context.AddAsync(rolNuevo);
                await _context.SaveChangesAsync();

                //_context.Roles.Add(rolNuevo);
                //await _context.SaveChangesAsync();
                JObject respuesta = new JObject
                {
                    ["message"] = 1
                };
                OkObjectResult ok = new OkObjectResult(respuesta) { StatusCode = 200};
                return ok;

            }catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de crear el rol", ex);
            }
        }

        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var Roles = await _context.Roles.ToListAsync();
                return new ObjectResult(Roles) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de obtener los roles", ex);
            }
        }
        public async Task<IActionResult> DesactivarRol(int idRol)
        {
            try
            {
                var Rol = _context.Roles.FirstOrDefault(e => e.IdRol == idRol);
                if (Rol == null)
                {
                    return _error.respuestaDeError("No se encontro el rol con id: " + idRol);
                }
                Rol.Column1 = "N";
                _context.Entry(Rol).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ObjectResult(new { message = 1 }) { StatusCode = 200 };
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("Error al momento de desactivar el rol", ex);
            }
        }

    }
}
