using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class Clientes: IClientes
    {
        private readonly Errores _error;
        private readonly DataBaseContext _context;
        public Clientes(DataBaseContext ctx)
        {
            _error = new Errores();
            _context = ctx;
        }

        public async Task<IActionResult> CrearCliente(Cliente cliente)
        {
            try
            {
                SequenceValueGenerator generator = new SequenceValueGenerator("USR", "SEC_CLIENTE");
                decimal id = generator.NextValue(_context);
                cliente.IdCliente = id;
                await _context.AddAsync(cliente);
                await _context.SaveChangesAsync();
                //_context.Add(cliente);
                //await _context.SaveChangesAsync();
                return new ObjectResult(1) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error el momento de crear el cliente", ex);
            }
        }

        public async Task<IActionResult> GetClientes()
        {
            try
            {
                return new ObjectResult(await _context.Clientes.ToListAsync());
            }catch(Exception ex)
            {
                return _error.respuestaDeError("Error al obtener el listado de clientes", ex);
            }
        }

        public async Task<IActionResult> GetComprasCliente(int idCliente)
        {
            try
            {
                
                return new ObjectResult(1) { StatusCode = 200 };
            }catch(Exception ex)
            {
                return _error.respuestaDeError("Error al momento de obtener el historial de compras del cliente", ex);
            }
        }


    }
}
