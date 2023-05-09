using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Caso
    {
        public Caso()
        {
            AsigancionesCamas = new HashSet<AsigancionesCama>();
            Cita = new HashSet<Cita>();
            DiagnosticosCasos = new HashSet<DiagnosticosCaso>();
            ExamenesCasos = new HashSet<ExamenesCaso>();
        }

        public decimal IdCaso { get; set; }
        public decimal? IdPaciente { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? MotivoFinalizacion { get; set; }

        public virtual Paciente? IdPacienteNavigation { get; set; }
        public virtual ICollection<AsigancionesCama> AsigancionesCamas { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<DiagnosticosCaso> DiagnosticosCasos { get; set; }
        public virtual ICollection<ExamenesCaso> ExamenesCasos { get; set; }
    }
}
