using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Venta>();
        }

        public decimal IdCliente { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Nit { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}
