using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Cita
    {
        public Cita()
        {
            DiagnosticosCita = new HashSet<DiagnosticosCitum>();
            ExamenesCasos = new HashSet<ExamenesCaso>();
            Receta = new HashSet<Receta>();
        }

        public decimal IdCita { get; set; }
        public decimal? IdCaso { get; set; }
        public decimal? IdClinica { get; set; }
        public decimal? IdUsuario { get; set; }
        public string? Observaciones { get; set; }

        public virtual Caso? IdCasoNavigation { get; set; }
        public virtual Clinica? IdClinicaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DiagnosticosCitum> DiagnosticosCita { get; set; }
        public virtual ICollection<ExamenesCaso> ExamenesCasos { get; set; }
        public virtual ICollection<Receta> Receta { get; set; }
    }
}
