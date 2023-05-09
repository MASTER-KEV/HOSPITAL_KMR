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
    public class Citas:ICitas
    {
        DataBaseContext _databaseContext;
        Errores _error;
        public Citas(DataBaseContext ctx)
        {
            _databaseContext = ctx;
            _error = new Errores();
        }

        public async Task<IActionResult> CrearCita(Cita cita)
        {
            try
            {
                SequenceValueGenerator generator = new SequenceValueGenerator("USR", "SEQ_CITA");
                decimal id = generator.NextValue(_databaseContext);
                cita.IdCita = id;
                await _databaseContext.AddAsync(cita);
                await _databaseContext.SaveChangesAsync();
                //_databaseContext.Citas.Add(cita);
                //await _databaseContext.SaveChangesAsync();
                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("Error al momento de crear la cita",ex);
            }
        }
        public async Task<IActionResult> GuardarCita(Cita cita)
        {
            try
            {
                using var transaciont = _databaseContext.Database.BeginTransaction();
                var recetas = await _databaseContext.Recetas.ToListAsync();
                var citaExis = await _databaseContext.Citas.Where(e => e.IdCita == cita.IdCita).FirstOrDefaultAsync();
                decimal maxReceta = 0;
                if(recetas.Count > 0)
                {
                    maxReceta = recetas.Max(e => e.IdReceta);
                }
                foreach(var rec in cita.Receta)
                {
                    
                    _databaseContext.Recetas.Add(rec);
                    _databaseContext.SaveChanges();
                    
                }
                foreach (var rec in cita.Receta)
                {
                    
                    foreach (var med in rec.MedicamentosReceta)
                    {
                        MedicamentosRecetum medica = new MedicamentosRecetum();
                        medica.IdReceta = rec.IdReceta;
                        medica.IdProducto = med.IdProducto;
                        _databaseContext.MedicamentosReceta.Add(medica);

                    }
                }
                citaExis.Observaciones = cita.Observaciones;
                
                _databaseContext.Entry(citaExis).State = EntityState.Modified;
                _databaseContext.SaveChanges();
                foreach(var diag in cita.DiagnosticosCita)
                {
                    _databaseContext.DiagnosticosCita.Add(diag);
                    _databaseContext.SaveChanges();
                }
                foreach(var ex in cita.ExamenesCasos)
                {
                    _databaseContext.ExamenesCasos.Add(ex);
                    _databaseContext.SaveChanges();
                }

                await _databaseContext.SaveChangesAsync();

                transaciont.Commit();

                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de crear la cita", ex);
            }
        }
        public async Task<IActionResult> HistorialCitasPaciente(int paciente)
        {
            try
            {
                var historialCitas = await (from pacientes in _databaseContext.Pacientes
                                            join caso in _databaseContext.Casos on pacientes.IdPaciente equals caso.IdPaciente
                                            join cita in _databaseContext.Citas on caso.IdCaso equals cita.IdCaso
                                            select
                                            cita)
                                            .Include(n => n.IdClinicaNavigation)
                                            .ThenInclude(m => m.IdSucursalNavigation)
                                            .Include(n => n.IdCasoNavigation)
                                            .ThenInclude(m => m.IdPacienteNavigation).ToListAsync();
                return new ObjectResult(historialCitas) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de crear la cita", ex);
            }
        }
    }
}
