using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Departamento
    {
        public Departamento()
        {
            Municipios = new HashSet<Municipio>();
        }

        public decimal IdDepartamento { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Municipio> Municipios { get; set; }
    }
}
