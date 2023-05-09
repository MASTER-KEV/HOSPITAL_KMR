using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class DiagnosticosCitum
    {
        public decimal IdDiagnosticoCita { get; set; }
        public decimal? IdCita { get; set; }
        public decimal? IdDiagnostico { get; set; }
        public string? Observacion { get; set; }

        public virtual Cita? IdCitaNavigation { get; set; }
        public virtual Diagnostico? IdDiagnosticoNavigation { get; set; }
    }
}
