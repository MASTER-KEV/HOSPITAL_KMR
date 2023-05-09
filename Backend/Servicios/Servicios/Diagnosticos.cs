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
    public class Diagnosticos: IDiagnosticos
    {
        DataBaseContext _dataBaseContext;
        Errores _error;
        public Diagnosticos(DataBaseContext ctx)
        {
            _dataBaseContext = ctx;
            _error = new Errores();
        }

        public async Task<IActionResult> GetDiagnosticos(string nombre)
        {
            try
            {
                var diags = await _dataBaseContext.Diagnosticos.Where(e => e.Nombre.ToUpper().Contains(nombre.ToUpper())).ToListAsync();
                return new ObjectResult(diags) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de listar los diagnosticos", ex);
            }
        }

        public async Task<IActionResult> AgregarDiagnosticoCaso(DiagnosticosCaso diag)
        {
            try
            {
                diag.FechaInicio = DateTime.Now;
                _dataBaseContext.DiagnosticosCasos.Add(diag);
                await _dataBaseContext.SaveChangesAsync();
                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de agregar el diagnosticos al paciente", ex);
            }
        }

        public async Task<IActionResult> ListarDiagnosticosCaso(int idCaso)
        {
            try
            {
                
                return new ObjectResult(_dataBaseContext.DiagnosticosCasos.Where(e => e.IdCaso == idCaso).ToListAsync()) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de listar los diagnosticos del paciente", ex);
            }
        }
    }
}
