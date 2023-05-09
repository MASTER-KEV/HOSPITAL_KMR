using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClinicaController{
        private readonly IClinicas _clinica;
        public ClinicaController(IClinicas clini)
        {
            _clinica = clini;   
        }

        [HttpPost("CrearClinica")]
        public async Task<IActionResult>  CrearCllinica(Clinica clinica)
        {
            return await _clinica.CrearClinica(clinica);
        }

        [HttpGet("GetClinicasSucursal")]
        public async Task<IActionResult> GetClinicasSucursal(int idSucursal)
        {
            return await _clinica.GetClinicasSucursal(idSucursal);
        }
        

    }
}
