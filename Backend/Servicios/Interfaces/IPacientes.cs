using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IPacientes
    {
        Task<IActionResult> CrearPaciente(Paciente paciente);
        Task<IActionResult> GetPaciente(string nombre);
        Task<IActionResult> CrearCaso(Caso caso);
        Task<IActionResult> ListarCasosPaciente(int idPaciente);
        Task<IActionResult> CerrarCaso(Caso caso);
        Task<IActionResult> ListarPacientesCasoAbierto(string nombre);
    }
}
