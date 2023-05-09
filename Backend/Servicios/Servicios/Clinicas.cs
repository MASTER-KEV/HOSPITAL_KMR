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

namespace Servicios.Servicios{

    public  class Clinicas:IClinicas
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly Errores _error;

        public Clinicas(DataBaseContext ctx){
            _dataBaseContext = ctx;
            _error = new Errores();
        }

        public async Task<IActionResult> CrearClinica(Clinica clinica)
        {
            try
            {
                SequenceValueGenerator generator = new SequenceValueGenerator("USR", "SEC_CLINICA");
                decimal id = generator.NextValue(_dataBaseContext);
                _dataBaseContext.Clinicas.Add(clinica);
                await _dataBaseContext.SaveChangesAsync();
                await _dataBaseContext.SaveChangesAsync();

                return new ObjectResult(new {estado = 2}){StatusCode = 200};
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("", ex);
            }
        }
        public async Task<IActionResult> GetClinicasSucursal(int idSucursal)
        {
            try
            {
                return new ObjectResult(await _dataBaseContext.Clinicas.Where(e => e.IdSucursal == idSucursal).ToListAsync()){StatusCode = 200};
            }
            catch(Exception ex)
            {
                return _error.respuestaDeError("", ex);
            }
        }
    }
}