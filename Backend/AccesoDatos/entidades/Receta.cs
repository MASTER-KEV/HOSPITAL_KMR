using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Receta
    {
        public Receta()
        {
            MedicamentosReceta = new HashSet<MedicamentosRecetum>();
        }

        public decimal IdReceta { get; set; }
        public decimal? IdCita { get; set; }
        public decimal? IdUsuario { get; set; }

        public virtual Cita? IdCitaNavigation { get; set; }
        public virtual ICollection<MedicamentosRecetum> MedicamentosReceta { get; set; }
    }
}
