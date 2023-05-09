using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Interfaces;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;

namespace Servicios.Servicios
{
    public  class Usuarios :IUsuarios
    {
        private readonly DataBaseContext _dataBaseContext;
        private Errores _errores;
        public Usuarios(DataBaseContext ctx)
        {
            _dataBaseContext = ctx;
            _errores = new Errores();   
        }

        public async Task<IActionResult> CrearUsuario(Usuario user)
        {
            try
            {
                SequenceValueGenerator generator = new SequenceValueGenerator("USR", "SEC_USUARIO");
                decimal id = generator.NextValue(_dataBaseContext);
                user.IdUsuario = id;
                await _dataBaseContext.AddAsync(user);
                await _dataBaseContext.SaveChangesAsync();
                Encrypt encr = new Encrypt();
                user.Password = Encrypt.GetSHA256(user.Password);
                //_dataBaseContext.Add(user);
                //await _dataBaseContext.SaveChangesAsync();
                
                return new ObjectResult(new {estado = 1 }) { StatusCode = 200 };
            }
            catch (Exception ex) {
                return _errores.respuestaDeError("Error al momento de crear el usuarioooooo", ex);
            }
        }
    }
}
