using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Factura
    {
        public Factura()
        {
            ProductosFacturas = new HashSet<ProductosFactura>();
            Venta = new HashSet<Venta>();
        }

        public decimal IdFactura { get; set; }
        public decimal? TotalFactura { get; set; }
        public DateTime? FechaFactura { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<ProductosFactura> ProductosFacturas { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
