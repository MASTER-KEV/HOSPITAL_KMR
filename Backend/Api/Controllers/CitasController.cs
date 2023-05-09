using Microsoft.AspNetCore.Mvc;
using AccesoDatos;
using Servicios.Interfaces;
namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitasController : Controller
    {
        private readonly ICitas _cita;
        
        public CitasController(ICitas c)
        {
            _cita = c;
        }

        [HttpPost("CrearCita")]
        public async Task<IActionResult> CrearCita(Cita cita)
        {
            return await _cita.CrearCita(cita);
        }

        [HttpPost("GuardarCita")]
        public async Task<IActionResult> GuardarCita(Cita cita)
        {
            return await _cita.GuardarCita(cita);
        }

        [HttpPost("HistorialCitasPaciente")]
        public async Task<IActionResult> HistorialCitasPaciente(int idPaciente)
        {
            return await _cita.HistorialCitasPaciente(idPaciente);
        }
    }
}
