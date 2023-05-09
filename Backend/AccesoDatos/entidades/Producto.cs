using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoDatos
{
    public partial class Producto
    {
        public Producto()
        {
            MedicamentosCasos = new HashSet<MedicamentosCaso>();
            MedicamentosReceta = new HashSet<MedicamentosRecetum>();
            ProductosFacturas = new HashSet<ProductosFactura>();
        }

        public decimal IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Imagen { get; set; }
        [NotMapped]
        public string? Archivobase64 { get; set; }
        [NotMapped]
        public string? Nombrearchivo { get; set; }

        public virtual ICollection<MedicamentosCaso> MedicamentosCasos { get; set; }
        public virtual ICollection<MedicamentosRecetum> MedicamentosReceta { get; set; }
        public virtual ICollection<ProductosFactura> ProductosFacturas { get; set; }
    }
}
