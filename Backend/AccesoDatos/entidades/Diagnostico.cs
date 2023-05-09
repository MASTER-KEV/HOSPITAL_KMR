using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Diagnostico
    {
        public Diagnostico()
        {
            DiagnosticosCasos = new HashSet<DiagnosticosCaso>();
            DiagnosticosCita = new HashSet<DiagnosticosCitum>();
        }

        public decimal IdDiagnostico { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<DiagnosticosCaso> DiagnosticosCasos { get; set; }
        public virtual ICollection<DiagnosticosCitum> DiagnosticosCita { get; set; }
    }
}
