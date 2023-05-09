using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class AsigancionesCama
    {
        public decimal IdAsignacionCama { get; set; }
        public decimal? IdCaso { get; set; }
        public decimal? IdCama { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Estado { get; set; }

        public virtual Cama? IdCamaNavigation { get; set; }
        public virtual Caso? IdCasoNavigation { get; set; }
    }
}
