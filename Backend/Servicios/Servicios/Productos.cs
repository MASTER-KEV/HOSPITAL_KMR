using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicios.Interfaces;
using Google.Cloud.Storage.V1;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json.Linq;

namespace Servicios.Servicios
{
    public class Productos:IProductos
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly Errores _errores;
        private readonly string bucket = "desa-web-final";
        private readonly string urlBucket = "https://storage.googleapis.com/";
        public Productos(DataBaseContext ctx)
        {
            _dataBaseContext = ctx;
            _errores = new Errores();
        }

        public async Task<IActionResult> CrearProducto(Producto prod)
        {
            try
            {
                if(prod.Imagen != null)
                {
                    prod.Imagen = urlBucket + bucket + "/" + prod.Nombrearchivo;
                    SubirProducto(prod.Archivobase64, prod.Nombrearchivo);

                }
                SequenceValueGenerator generator = new SequenceValueGenerator("USR", "SEQ_PRODUCTO");
                decimal id = generator.NextValue(_dataBaseContext);
                _dataBaseContext.Add(prod);
                prod.Archivobase64 = "";
                await _dataBaseContext.SaveChangesAsync();
                return new ObjectResult(1) { StatusCode = 200 };
            }catch(Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de crear los productos",ex);
            }
        }

        public Google.Apis.Storage.v1.Data.Object SubirProducto(string base64File, string fileName)
        {
            GoogleCredential _credential = GoogleCredential.FromFile("file.json");
            var storage = StorageClient.Create(_credential);
            byte[] buffer = Convert.FromBase64String(base64File);
            MemoryStream stream = new MemoryStream(buffer);
            string fileTipe = "";
            if (fileName.Contains(".png"))
            {
                fileTipe = "image/png";
            } else if (fileName.Contains(".jpeg") || fileName.Contains(".jpg"))
            {
                fileTipe = "image/jpeg";
            }
            var respuesta =  storage.UploadObject(bucket, fileName, fileTipe, stream);
            
            return respuesta;
        }

        public async Task<IActionResult> ListarProductos()
        {
            try
            {
                var x = await _dataBaseContext.Productos.ToListAsync();

                JArray arreglo = new JArray();
                
//                foreach(var item in x)
//                {
                    
//                    string query = @"select sucu.nombre 'key', sum(lote.exitencia) valor from lotes lote
//join bodegas bode on lote.id_bodega = bode.id_bodega
//join sucursales sucu on bode.id_sucursal = sucu.id_sucursal
//where lote.id_producto= " + item.IdProducto+@"
//group by sucu.nombre";
//                    var valores = _dataBaseContext.valores.FromSqlRaw(query);
//                    JArray vals = new JArray();
//                    foreach(var r in valores)
//                    {
//                        vals.Add(new JObject()
//                        {
//                            ["sucursal"] = r.key,
//                            ["cantidad"] = r.valor
//                    });
//                    }
                    
                    
//                    JObject obj = new JObject()
//                    {
//                        ["id"] = item.IdProducto,
//                        ["nombre"] = item.Nombre,
//                        ["imagen"] = item.Imagen,
//                        ["open"] = false,
                        
//                        ["existencias"] = vals
//                    };
//                    arreglo.Add(obj);
//                }

                
                return new ObjectResult(arreglo) { StatusCode = 200 };
            }catch (Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de listar los productos", ex);
            }
        }
        public async Task<IActionResult> BuscarProducto(string nombre)
        {
            try
            {
                var poducto = await _dataBaseContext.Productos.Where(e => e.Nombre.ToUpper().Contains(nombre.ToUpper())).ToListAsync();
                if(poducto.Count == 0)
                {
                    return _errores.respuestaDeError("El producto no fue encontrado");
                }
                return new ObjectResult(poducto) { StatusCode = 200 };
            }catch(Exception ex)
            {
                return _errores.respuestaDeError("Error al momento realizar la busqueda del producto", ex);
            }
        }
    }
}
