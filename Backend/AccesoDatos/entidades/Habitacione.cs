using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Habitacione
    {
        public Habitacione()
        {
            Camas = new HashSet<Cama>();
        }

        public decimal IdHabitacion { get; set; }
        public decimal? IdSucursal { get; set; }
        public decimal? IdTipoHabitacion { get; set; }
        public string? Nombre { get; set; }

        public virtual Sucursale? IdSucursalNavigation { get; set; }
        public virtual TiposHabitacion? IdTipoHabitacionNavigation { get; set; }
        public virtual ICollection<Cama> Camas { get; set; }
    }
}
