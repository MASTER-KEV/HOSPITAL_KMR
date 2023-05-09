using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace Servicios.Interfaces
{
    public interface IExamenes
    {
        public Task<IActionResult> AgregarExamenesCaso(ExamenesCaso examenCaso);
        public Task<IActionResult> GetExamenesCaso(int idCaso);
        public Task<IActionResult> GetExamenes(string nombre);
    }
}
