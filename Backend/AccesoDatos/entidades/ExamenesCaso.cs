using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class ExamenesCaso
    {
        public ExamenesCaso()
        {
            ResultadoExamenes = new HashSet<ResultadoExamene>();
        }

        public decimal IdExamenCaso { get; set; }
        public decimal? IdCaso { get; set; }
        public decimal? IdExamen { get; set; }
        public decimal? IdCita { get; set; }
        public string? Observaciones { get; set; }
        public string? Estado { get; set; }
        public decimal? IdUsuario { get; set; }

        public virtual Caso? IdCasoNavigation { get; set; }
        public virtual Cita? IdCitaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<ResultadoExamene> ResultadoExamenes { get; set; }
    }
}
