using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BodegasLotesController : Controller
    {
        private IIBodegasLotes _bodegasLotes;
        public BodegasLotesController(IIBodegasLotes bdLo)
        {
            _bodegasLotes = bdLo;
        }

        //[HttpPost("CrearBodega")]
        //public async Task<IActionResult> CrearBodega(Bodega bodega)
        //{
        //    return await _bodegasLotes.CrearBodega(bodega);
        //}

        //[HttpGet("ListarBodegasSucursal")]
        //public async Task<IActionResult> ListarBodegasSucursal(int idSucursal)
        //{
        //    return await _bodegasLotes.ListarBodegasSucursal(idSucursal);
        //}

        //[HttpPost("CrearLote")]
        //public async Task<IActionResult> CrearLote(Lote lote)
        //{
        //    return await _bodegasLotes.CrearLote(lote);
        //}

        //[HttpGet("ListarLotesBodega")]
        //public async Task<IActionResult> ListarLotesBodega(int idBodega)
        //{
        //    return await _bodegasLotes.ListarLotesBodega(idBodega);
        //}

        //[HttpGet("GetLotesBodegaProdcto")]
        //public async Task<IActionResult> GetLotesBodegaProducto(int idBodega, int idProducto)
        //{
        //    return await _bodegasLotes.GetLotesBodegaProducto(idBodega, idProducto);
        //}

        //[HttpGet("GetProductosSucursal")]
        //public async Task<IActionResult> GetProductosSucursal(int idSucursal)
        //{
        //    return await _bodegasLotes.GetProductosSucursal(idSucursal);
        //}

        //[HttpGet("GetLote")]
        //public async Task<IActionResult> GetLote(int idLote)
        //{
        //    return  await _bodegasLotes.GetLote(idLote);
        //}

        //[HttpPost("ActualizarLote")]
        //public async Task<IActionResult> ActualizarLote(Lote lote)
        //{
        //    return await _bodegasLotes.ActualizarLote(lote);
        //}

        
    }
}
