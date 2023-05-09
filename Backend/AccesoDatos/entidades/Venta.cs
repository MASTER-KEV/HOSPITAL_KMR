using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Venta
    {
        public decimal IdVenta { get; set; }
        public decimal? IdFactura { get; set; }
        public decimal? IdCliente { get; set; }
        public decimal? Total { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaVenta { get; set; }
        public decimal? UsuarioVenta { get; set; }
        public decimal? IdTipoVenta { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual Factura? IdFacturaNavigation { get; set; }
        public virtual TipoVentum? IdTipoVentaNavigation { get; set; }
        public virtual Usuario? UsuarioVentaNavigation { get; set; }
    }
}
