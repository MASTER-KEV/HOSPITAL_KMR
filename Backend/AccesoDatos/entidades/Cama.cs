using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Cama
    {
        public Cama()
        {
            AsigancionesCamas = new HashSet<AsigancionesCama>();
        }

        public decimal IdCama { get; set; }
        public decimal? IdHabitacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Estado { get; set; }

        public virtual Habitacione? IdHabitacionNavigation { get; set; }
        public virtual ICollection<AsigancionesCama> AsigancionesCamas { get; set; }
    }
}
