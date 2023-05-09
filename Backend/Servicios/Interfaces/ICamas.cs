using Microsoft.AspNetCore.Mvc;
using AccesoDatos;

namespace Servicios.Interfaces
{
    public interface ICamas
    {
        public Task<IActionResult> CrearCamma(Cama cama);
        public Task<IActionResult> GetCamasSucursal(int idSucursal);
        public Task<IActionResult> GetHabitacionesSucursal(int idSucursal);
        public Task<IActionResult> AsignarCama(AsigancionesCama asignacion);
        public Task<IActionResult> DesAsignarCama(int idCama, int idAsignacion, DateTime fechaFin);
        public Task<IActionResult> CrearHabitacion(Habitacione Habitacion);
        public Task<IActionResult> SalasSucursal(int idSucursal);
        public Task<IActionResult> GetCamasHabitacion(int idHabitacion);
        public Task<IActionResult> GetAsignacionPaciente(int idPaciente, string tipo);
        
    }
}