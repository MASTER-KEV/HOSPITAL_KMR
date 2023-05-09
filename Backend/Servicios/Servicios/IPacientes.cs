using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class Pacientes:IPacientes
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly Errores _error;
        public Pacientes(DataBaseContext ctx)
        {
            _dataBaseContext = ctx;
            _error = new Errores();
        }

        public async Task<IActionResult> CrearPaciente(Paciente paciente)
        {
            try
            {
                SequenceValueGenerator generator = new SequenceValueGenerator("USR", "SEC_PACIENTE");
                decimal id = generator.NextValue(_dataBaseContext);
                paciente.IdPaciente = id;
                await _dataBaseContext.SaveChangesAsync();
                await _dataBaseContext.SaveChangesAsync();
                //Estas 2 lineas de abajo no deberian estar comentadas :v 
                await _dataBaseContext.AddAsync(paciente); // Esta linea lo agrega a la base de datos
                _dataBaseContext.SaveChanges(); //Esta guarda los cambios
                return new ObjectResult( new {estado = 1 }) { StatusCode = 200 };
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("", ex);
            }
        }
        public async Task<IActionResult> GetPaciente(string nombre)
        {
            try
            {
                var pacientes = await _dataBaseContext.Pacientes.Where(e => e.Nombres.ToUpper().Contains(nombre.ToUpper()))
                                        .Include(n => n.Casos).ToListAsync();
                return new ObjectResult(pacientes) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("", ex);
            }
        }
        public async Task<IActionResult> CrearCaso(Caso caso)
        {
            try
            {
                var casosPaciente = await _dataBaseContext.Casos.Where(e => e.IdPaciente == caso.IdPaciente && caso.FechaFin == null).ToArrayAsync();
                if (casosPaciente.Length > 0)
                {
                    return _error.respuestaDeError("El paciente ya cuenta con un caso abierto");
                }
                await _dataBaseContext.Casos.AddAsync(caso);
                _dataBaseContext.SaveChanges();

                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("", ex);
            }
        }
        public async Task<IActionResult> ListarCasosPaciente(int idPaciente)
        {
            try
            {
                return new ObjectResult(1) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("", ex);
            }
        }
        public async Task<IActionResult> CerrarCaso(Caso caso)
        {
            try
            {
                var casos = await _dataBaseContext.Casos.Where(e => e.IdCaso == caso.IdCaso).FirstOrDefaultAsync();
                casos.FechaFin = caso.FechaFin;
                casos.MotivoFinalizacion = caso.MotivoFinalizacion;
                _dataBaseContext.Entry(casos).State = EntityState.Modified;
                _dataBaseContext.SaveChanges();
                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("", ex);
            }
        }

        public async Task<IActionResult> ListarPacientesCasoAbierto(string nombre)
        {
            try
            {
                var clientes = await (from casos in _dataBaseContext.Casos
                                      join paciente in _dataBaseContext.Pacientes on casos.IdPaciente equals paciente.IdPaciente
                                      where
                                      casos.MotivoFinalizacion == null
                                      && casos.FechaFin == null
                                      && paciente.Nombres.ToUpper().Contains(nombre.ToUpper())
                                      select new
                                      {
                                          paciente,
                                          casos.IdCaso
                                      }).ToListAsync();

                return new ObjectResult(clientes) { StatusCode = 200 };


            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("", ex);
            }
        }
    }
}
