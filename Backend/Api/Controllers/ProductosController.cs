using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccesoDatos;
using Servicios.Interfaces;
namespace Api.Controllers
{
    [ApiController]
    [Route("")]
    public class ProductosController : Controller
    {
        private readonly IProductos _prod;
        public ProductosController(IProductos prod)
        {
            _prod = prod;
        }

        [HttpPost("CrearProducto")]

        public async Task<IActionResult> SubirProducto(Producto producto)
        {
            return await _prod.CrearProducto(producto);
        }

        [HttpGet("ListarProductos")]
        public async Task<IActionResult> ListarProductos()
        {
            return await _prod.ListarProductos();
        }

        [HttpGet("BuscarProducto")]
        public async Task<IActionResult> BuscarProducto(string nombre)
        {
            return await _prod.BuscarProducto(nombre);
        }
    }
    
}
