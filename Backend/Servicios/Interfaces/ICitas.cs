using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface ICitas
    {
        public Task<IActionResult> CrearCita(Cita cita);
        public Task<IActionResult> GuardarCita(Cita cita);
        public Task<IActionResult> HistorialCitasPaciente(int paciente);
        
    }
}
