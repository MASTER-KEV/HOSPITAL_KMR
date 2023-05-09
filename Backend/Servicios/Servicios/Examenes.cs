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
    public class Examenes:IExamenes
    {
        DataBaseContext _dataBaseContext;
        Errores _error;
        public Examenes(DataBaseContext ctx)
        {
            _dataBaseContext = ctx;
            _error = new Errores();
        }
        public async Task<IActionResult> AgregarExamenesCaso(ExamenesCaso examenCaso)
        {
            try
            {
                _dataBaseContext.ExamenesCasos.Add(examenCaso);
                await _dataBaseContext.SaveChangesAsync();
                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de agregar el examen al paciente", ex);
            }
        }
        public async Task<IActionResult> GetExamenesCaso(int idCaso)
        {
            try
            {
                var x = await (from examen in _dataBaseContext.ExamenesCasos
                               join cita in _dataBaseContext.Citas on examen.IdCita equals cita.IdCita
                               join caso in _dataBaseContext.Casos on cita.IdCaso equals caso.IdCaso
                               where
                               caso.IdCaso == idCaso
                               select
                                examen
                                )
                                .Include(m => m.IdExamenCaso)
                                .ToListAsync();
                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de lisar los examenes del paciente", ex);
            }
        }

        public async Task<IActionResult> GetExamenes(string nombre)
        {
            try
            {

                var x = await _dataBaseContext.Examenes.Where(e => e.Nombre.ToUpper().Contains(nombre.ToUpper())).ToListAsync();
                return new ObjectResult(x) { StatusCode = 200 };
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("Error al momento de lisar los examenes", ex);
            }
        }

    }
}
