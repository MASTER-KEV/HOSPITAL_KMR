using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Interfaces;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Servicios.Servicios
{
    public class Sucursales : ISucursal
    {
        private readonly DataBaseContext _context;
        private readonly Errores _error;

        public Sucursales(DataBaseContext ctx)
        {
            _context = ctx;
            _error = new Errores();
        }

        public async Task<IActionResult> CrearSucursal(Sucursale sucursal)
        {
            try
            {
                SequenceValueGenerator generador = new SequenceValueGenerator("USR", "SEQ_SUCURSAL");// Cree el objeto para genera sequencias con el usuario duenio y el nombre de la sequencia
                decimal id = generador.NextValue(_context);// Generamos el siguiente valor
                sucursal.IdSucursal =  id; //asignamos el valor al objeto
                await _context.AddAsync(sucursal);//Agregamos 
                await _context.SaveChangesAsync();//Guardamos cambios   

                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de crear la sucursal", ex);
            }
        }
        public async Task<IActionResult> GetSucursales()
        {
            try
            {
                List<Sucursale> sucursales = await _context.Sucursales
                                                    .Include(n => n.Clinicas)
                                                    .ToListAsync();
                return new ObjectResult(sucursales) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de listar las sucursales", ex);
            }
        }
    }
}
