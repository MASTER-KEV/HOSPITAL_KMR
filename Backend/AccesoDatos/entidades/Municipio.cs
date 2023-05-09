using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Municipio
    {
        public Municipio()
        {
            Pacientes = new HashSet<Paciente>();
            Sucursales = new HashSet<Sucursale>();
        }

        public decimal IdMunicipio { get; set; }
        public string? Nombre { get; set; }
        public decimal IdDepartamento { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
        public virtual ICollection<Paciente> Pacientes { get; set; }
        public virtual ICollection<Sucursale> Sucursales { get; set; }
    }
}
