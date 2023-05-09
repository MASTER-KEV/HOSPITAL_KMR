using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class DiagnosticosCaso
    {
        public decimal IdDiagnosticosCaso { get; set; }
        public decimal? IdCaso { get; set; }
        public decimal? IdDiagnostico { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? IdUsuario { get; set; }
        public string? Estado { get; set; }

        public virtual Caso? IdCasoNavigation { get; set; }
        public virtual Diagnostico? IdDiagnosticoNavigation { get; set; }
    }
}
