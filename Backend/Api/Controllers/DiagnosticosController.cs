using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiagnosticosController : Controller
    {
        private readonly IDiagnosticos _diags;
        public DiagnosticosController(IDiagnosticos dx)
        {
            _diags = dx;
        }
        [HttpPost("GetDiagnosticos")]
        public async Task<IActionResult> GetDiagnosticos(string nombre)
        {
            return await _diags.GetDiagnosticos(nombre);
        }

        [HttpPost("AgregarDiagnostico")]
        public async Task<IActionResult> AgregarDiagnostico(DiagnosticosCaso diag)
        {
            return await _diags.AgregarDiagnosticoCaso(diag);
        }

        [HttpPost("ListarDiagnosticosCaso")]
        public async Task<IActionResult> ListarDiagnosticosCaso(int idCaso)
        {
            return await _diags.ListarDiagnosticosCaso(idCaso);
        }
    }
}
