using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Servicios.Interfaces;
using AccesoDatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
namespace Servicios.Servicios{

    public  class Camas:ICamas
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly Errores _error;
        public Camas(DataBaseContext ctx){
            _dataBaseContext = ctx;
            _error = new Errores();
        }

        public async Task<IActionResult> CrearCamma(Cama cama)
        {
            try
            {
                SequenceValueGenerator generador = new SequenceValueGenerator("USR", "SEC_CAMAS");
                decimal id = generador.NextValue(_dataBaseContext);
                cama.IdCama = id;
                await _dataBaseContext.AddAsync(cama);
                await _dataBaseContext.SaveChangesAsync();
                //_dataBaseContext.Camas.Add(cama);
                //await _dataBaseContext.SaveChangesAsync();
                return new ObjectResult(new {estado = 1}){StatusCode = 200};
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("",ex);
            }
        }
        public async Task<IActionResult> GetCamasSucursal(int idSucursal)
        {
            try
            {
                var camas = await(from cama in _dataBaseContext.Camas
                                  join habitacion in _dataBaseContext.Habitaciones on cama.IdHabitacion equals habitacion.IdHabitacion
                                  join sucursal in _dataBaseContext.Sucursales on habitacion.IdSucursal equals sucursal.IdSucursal
                                  where sucursal.IdSucursal == idSucursal
                                  select
                                  cama
                                  ).ToListAsync();
                return new ObjectResult(camas){StatusCode = 200};
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("",ex);
            }
        }
        public async Task<IActionResult> GetHabitacionesSucursal(int idSucursal)
        {
            try
            {
                var habitaciones = await(from habitacion in _dataBaseContext.Habitaciones 
                                        join sucursal in _dataBaseContext.Sucursales on habitacion.IdSucursal equals sucursal.IdSucursal
                                        where sucursal.IdSucursal == idSucursal
                                        select
                                        habitacion
                                        ).Include(n => n.IdTipoHabitacionNavigation)
                                        .ToListAsync();
                return new ObjectResult(habitaciones){StatusCode = 200};
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("",ex);
            }
        }
        public async Task<IActionResult> AsignarCama(AsigancionesCama asignacion)
        {
            try
            {
                _dataBaseContext.AsignacionesCamas.Add(asignacion);
                var camas = await _dataBaseContext.Habitaciones.Where(e => e.IdSucursal == asignacion.IdAsignacionCama)
                    .ToListAsync();
                await _dataBaseContext.SaveChangesAsync();

                return new ObjectResult(new { estado = 1 }){StatusCode = 200};
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("",ex);
            }
        }
        public async Task<IActionResult> DesAsignarCama(int idCama, int idAsignacion, DateTime fechaFin)
        {
            try
            {
                var asignacion = await _dataBaseContext.AsignacionesCamas.FirstOrDefaultAsync(e => e.IdCama == idCama && e.IdAsignacionCama == idAsignacion);

                asignacion.FechaFin = fechaFin;

                _dataBaseContext.Entry(asignacion).State = EntityState.Modified;

                _dataBaseContext.SaveChanges();

                return new ObjectResult(new {estado = 1}){StatusCode = 200};
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("",ex);
            }
        }
        public async Task<IActionResult> CrearHabitacion(Habitacione Habitacion)
        {
            try
            {
                _dataBaseContext.Habitaciones.Add(Habitacion);
                await _dataBaseContext.SaveChangesAsync();
                return new ObjectResult(new {estado = 1}){StatusCode = 200};
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("",ex);
            }
        }
        public async Task<IActionResult> SalasSucursal(int idSucursal)
        {
            try
            {
                
                return new ObjectResult(1){StatusCode = 200};
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("",ex);
            }
        }
        public async Task<IActionResult> GetCamasHabitacion(int idHabitacion)
        {
            try
            {
                var camas = await(from cama in _dataBaseContext.Camas
                                  join habitacion in _dataBaseContext.Habitaciones on cama.IdHabitacion equals habitacion.IdHabitacion
                                  where habitacion.IdHabitacion == idHabitacion
                                  select
                                  cama
                                  ).ToListAsync();
                return new ObjectResult(camas){StatusCode = 200};
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("",ex);
            }
        }
        public async Task<IActionResult> GetAsignacionPaciente(int idPaciente, string tipo)
        {
            try
            {
                if(tipo == "1")
                {
                    var asignacions = await (from cama in _dataBaseContext.Camas
                                            join asigs in _dataBaseContext.AsignacionesCamas on cama.IdCama equals asigs.IdCama
                                            join casos in _dataBaseContext.Casos on asigs.IdCaso  equals casos.IdCaso
                                            join pass in _dataBaseContext.Pacientes on casos.IdPaciente equals pass.IdPaciente
                                            where 
                                            pass.IdPaciente  == idPaciente
                                            select
                                                asigs
                                            ).ToListAsync();
                    return new ObjectResult(asignacions){StatusCode = 200};
                }else if(tipo == "2")
                {
                    var asignacions = await (from cama in _dataBaseContext.Camas
                                            join asigs in _dataBaseContext.AsignacionesCamas on cama.IdCama equals asigs.IdCama
                                            join casos in _dataBaseContext.Casos on asigs.IdCaso  equals casos.IdCaso
                                            join pass in _dataBaseContext.Pacientes on casos.IdPaciente equals pass.IdPaciente
                                            where 
                                            pass.IdPaciente  == idPaciente
                                            select
                                                asigs
                                            ).OrderByDescending(e => e.FechaInicio).ToListAsync();
                    if(asignacions.Count > 0){
                        return new ObjectResult(asignacions.FirstOrDefault()){StatusCode = 200};
                    }
                    return new ObjectResult(new {estado = 1}){StatusCode = 200};

                }else{
                    return _error.respuestaDeError("Valor invalido");
                    
                }
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("",ex);
            }
        }
    }
}