using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class HistoriaClinica
    {
        public decimal IdHistoriaClinica { get; set; }
        public decimal? IdPaciente { get; set; }
        public string? Historia { get; set; }
        public DateTime? FechaIngreso { get; set; }

        public virtual Paciente? IdPacienteNavigation { get; set; }
    }
}
