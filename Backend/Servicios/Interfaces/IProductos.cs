using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;

namespace Servicios.Interfaces
{
    public interface IProductos
    {
        public Task<IActionResult> CrearProducto(Producto producto);
        public Task<IActionResult> ListarProductos();
        public Task<IActionResult> BuscarProducto(string nombre);
        public Google.Apis.Storage.v1.Data.Object SubirProducto(string base64File, string fileName);
    }
}
