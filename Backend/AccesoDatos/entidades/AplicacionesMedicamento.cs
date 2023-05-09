using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class AplicacionesMedicamento
    {
        public decimal IdAplicacionMedicamento { get; set; }
        public decimal? IdMedicamentoCaso { get; set; }
        public DateTime? FechaHoraAplicacion { get; set; }
        public decimal? IdUsuario { get; set; }

        public virtual MedicamentosCaso? IdMedicamentoCasoNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
