using Microsoft.AspNetCore.Mvc;
using AccesoDatos;
using Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacientesController : Controller
    {
        public IPacientes _paciente;
        private DataBaseContext _dataBaseContext;
        public PacientesController(IPacientes pac, DataBaseContext ctx)
        {
            _paciente = pac;
            _dataBaseContext = ctx;
        }


        [HttpPost("CrearPaciente")]
        public async Task<IActionResult> CrearPaciente(Paciente paciente)
        {
            return await _paciente.CrearPaciente(paciente);
        }

        [HttpGet("GetPaciente")]
        public async Task<IActionResult> GetPaciente(string nombre)
        {
            var x  = await _paciente.GetPaciente(nombre);
            var z = x.GetType().ToString();
            return x;
        }

        [HttpPost("CrearCaso")]
        public async Task<IActionResult> CrearCaso(Caso caso)
        {
            return await _paciente.CrearCaso(caso);      
           
        }

        [HttpGet("CasosPaciente")]
        public async Task<IActionResult> CasosPaciente(int idpaciente)
        {
            return await _paciente.ListarCasosPaciente(idpaciente);
        }

        [HttpPost("CerrarCaso")]
        public async Task<IActionResult> CerrarCaso(Caso caso)
        {
            return await _paciente.CerrarCaso(caso);
        }

        [HttpGet("BuscarPacienteCasoAbierto")]
        public async Task<IActionResult> BuscarPacienteCasoAbierto(string nombre)
        {
            return await _paciente.ListarPacientesCasoAbierto(nombre);
        }

        [HttpGet("GetTodosPacientesCasoHabierto")]
        public async Task<IActionResult> BuscarPacienteCasoAbierto()
        {
            var clientes = await (from casos in _dataBaseContext.Casos
                                  join paciente in _dataBaseContext.Pacientes on casos.IdPaciente equals paciente.IdPaciente
                                  where
                                  casos.MotivoFinalizacion == null
                                  && casos.FechaFin == null
                                  
                                  select new
                                  {
                                      paciente,
                                      casos.IdCaso
                                  }).ToListAsync();

            return Ok(clientes);
        }
    }
}
