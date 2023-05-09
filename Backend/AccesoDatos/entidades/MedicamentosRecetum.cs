using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class MedicamentosRecetum
    {
        public decimal IdMedicamentosReceta { get; set; }
        public decimal? IdReceta { get; set; }
        public decimal? IdProducto { get; set; }
        public string? Observaciones { get; set; }
        public string? Frecuencia { get; set; }
        public string? Dosis { get; set; }
        public string? UnidadMedida { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
        public virtual Receta? IdRecetaNavigation { get; set; }
    }
}
