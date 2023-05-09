using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class TipoVentum
    {
        public TipoVentum()
        {
            Venta = new HashSet<Venta>();
        }

        public decimal IdTipoVenta { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}
