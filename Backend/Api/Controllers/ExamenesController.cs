using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamenesController : Controller
    {
        private readonly IExamenes _examenes ;
        public ExamenesController(IExamenes ex)
        {
            _examenes = ex;
        }

        [HttpGet("GetExamenes")]
        public async Task<IActionResult> GetExamenes(string nombre)
        {
            return await _examenes.GetExamenes(nombre);
        }
    }
}
