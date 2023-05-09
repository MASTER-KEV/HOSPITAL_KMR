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

    public  class LotesBodegas:IIBodegasLotes
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly Errores _error;

        public LotesBodegas(DataBaseContext ctx){
            _dataBaseContext = ctx;
            _error = new Errores();
        }

        //public async Task<IActionResult> CrearBodega(Bodega bodega){
        //    try
        //    {
        //        _dataBaseContext.Bodegas.Add(bodega);
        //        await _dataBaseContext.SaveChangesAsync();
        //        return new ObjectResult(new {estado = 1}) {StatusCode = 200};

        //    }catch(Exception ex)
        //    {
        //        return _error.respuestaDeError("",ex);
        //    }
        //}
        //public async Task<IActionResult> ListarBodegasSucursal(int idSucursal){
        //    try
        //    {
        //        var bodegas = await _dataBaseContext.Bodegas.Where(e => e.IdSucursal == idSucursal).ToListAsync();
        //        return new ObjectResult(bodegas) {StatusCode = 200};
        //    }catch(Exception ex)
        //    {
        //        return _error.respuestaDeError("",ex);
        //    }
        //}
        //public async Task<IActionResult> CrearLote(Lote lote){
        //    try
        //    {
        //        _dataBaseContext.Lotes.Add(lote);
        //        await _dataBaseContext.SaveChangesAsync();
        //        return new ObjectResult(new {estado = 1}) {StatusCode = 200};
        //    }catch(Exception ex)
        //    {
        //        return _error.respuestaDeError("",ex);
        //    }
        //}
        //public async Task<IActionResult> ListarLotesBodega(int idBodega){
        //    try
        //    {
        //        var lotes = await _dataBaseContext.Lotes.Where(e => e.IdBodega == idBodega).ToListAsync();
        //        return new ObjectResult(lotes) {StatusCode = 200};
        //    }catch(Exception ex)
        //    {
        //        return _error.respuestaDeError("",ex);
        //    }
        //}
        //public async Task<IActionResult> GetLotesBodegaProducto(int idBodega, int idProducto){
        //    try
        //    {
        //        var lotesProducto = await _dataBaseContext.Lotes.Where(e => e.IdBodega == idBodega && e.IdProducto == idProducto).ToListAsync();
        //        return new ObjectResult(lotesProducto) {StatusCode = 200};
        //    }catch(Exception ex)
        //    {
        //        return _error.respuestaDeError("",ex);
        //    }
        //}
        //public async Task<IActionResult> GetProductosSucursal(int idSucursal){
        //    try
        //    {
        //        var productos = await (from producto in _dataBaseContext.Productos
        //                               join lotes in _dataBaseContext.Lotes on producto.IdProducto equals lotes.IdProducto
        //                               join bodegas in _dataBaseContext.Bodegas on lotes.IdBodega equals bodegas.IdBodega
        //                               join sucursal in _dataBaseContext.Sucursales on bodegas.IdSucursal equals sucursal.IdSucursal
        //                               where 
        //                               sucursal.IdSucursal == idSucursal
        //                               && lotes.FechaCaducidad < DateTime.Now
        //                               select
        //                                producto
        //                               ).Include(n => n.Lotes.Where(y => y.IdBodegaNavigation.IdSucursal == idSucursal).ToList()).ToListAsync();
        //        return new ObjectResult(productos) {StatusCode = 200};
        //    }catch(Exception ex)
        //    {
        //        return _error.respuestaDeError("",ex);
        //    }
        //}
        //public async Task<IActionResult> GetLote(int idLote){
        //    try
        //    {

        //        return new ObjectResult(
        //            await _dataBaseContext.Lotes.FirstOrDefaultAsync(e => e.IdLote == idLote)
        //        ) {StatusCode = 200};
        //    }catch(Exception ex)
        //    {
        //        return _error.respuestaDeError("",ex);
        //    }
        //}
        //public async Task<IActionResult> ActualizarLote(Lote lote){
        //    try
        //    {
        //        Lote loteActualizar = await _dataBaseContext.Lotes.FirstAsync(e => e.IdLote == lote.IdLote);

        //        loteActualizar = lote;

        //        _dataBaseContext.Entry(loteActualizar).State = EntityState.Modified;
        //        await _dataBaseContext.SaveChangesAsync();

        //        return new ObjectResult(new {estado = 1}) {StatusCode = 200};
        //    }catch(Exception ex)
        //    {
        //        return _error.respuestaDeError("",ex);
        //    }
        //}
    }
}