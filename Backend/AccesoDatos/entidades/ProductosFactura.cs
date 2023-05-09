using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class ProductosFactura
    {
        public decimal IdProductoFactura { get; set; }
        public decimal? IdProducto { get; set; }
        public decimal? IdFactura { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Total { get; set; }
        public decimal? PrecioUnitario { get; set; }

        public virtual Factura? IdFacturaNavigation { get; set; }
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
