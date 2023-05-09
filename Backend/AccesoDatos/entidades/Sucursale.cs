using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Sucursale
    {
        public Sucursale()
        {
            Habitaciones = new HashSet<Habitacione>();
            Clinicas = new HashSet<Clinica>();
        }

        public decimal IdSucursal { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Estado { get; set; }
        public decimal? IdMunicipio { get; set; }

        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual ICollection<Habitacione> Habitaciones { get; set; }
        public virtual ICollection<Clinica> Clinicas { get; set; }
    }
}
