using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Clinica
    {
        public Clinica()
        {
            Cita = new HashSet<Cita>();
        }

        public decimal IdClinica { get; set; }
        public decimal? IdSucursal { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public virtual Sucursale IdSucursalNavigation { get; set; }

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
