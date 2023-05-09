using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class MedicamentosCaso
    {
        public MedicamentosCaso()
        {
            AplicacionesMedicamentos = new HashSet<AplicacionesMedicamento>();
        }

        public decimal IdMedicamentoCaso { get; set; }
        public decimal? IdUsuario { get; set; }
        public decimal? IdProducto { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Frecuencia { get; set; }
        public string? Dosis { get; set; }
        public string? UnidadMedida { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<AplicacionesMedicamento> AplicacionesMedicamentos { get; set; }
    }
}
