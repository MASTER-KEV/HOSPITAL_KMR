using Microsoft.AspNetCore.Mvc;
using AccesoDatos;

namespace Servicios.Interfaces
{
    public interface IClinicas
    {
        public Task<IActionResult> CrearClinica(Clinica clinica);
        public Task<IActionResult> GetClinicasSucursal(int idSucursal);
    }
}