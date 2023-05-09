using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IDiagnosticos
    {
        public Task<IActionResult> GetDiagnosticos(string nombre);
        public Task<IActionResult> AgregarDiagnosticoCaso(DiagnosticosCaso diag);
        public Task<IActionResult> ListarDiagnosticosCaso(int idCaso);
    }
}
