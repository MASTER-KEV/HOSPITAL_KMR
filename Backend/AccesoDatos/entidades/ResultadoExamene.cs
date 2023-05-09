using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class ResultadoExamene
    {
        public decimal IdResultadoExamenes { get; set; }
        public decimal? IdExamenCaso { get; set; }
        public string? Observacion { get; set; }
        public string? Estado { get; set; }

        public virtual ExamenesCaso? IdExamenCasoNavigation { get; set; }
    }
}
