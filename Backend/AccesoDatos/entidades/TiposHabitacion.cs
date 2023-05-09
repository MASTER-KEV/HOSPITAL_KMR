using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class TiposHabitacion
    {
        public TiposHabitacion()
        {
            Habitaciones = new HashSet<Habitacione>();
        }

        public decimal IdTipoHabitacion { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Habitacione> Habitaciones { get; set; }
    }
}
