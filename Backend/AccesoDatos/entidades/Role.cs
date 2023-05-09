using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Role
    {
        public Role()
        {
            RolesUsuarios = new HashSet<RolesUsuario>();
        }

        public decimal IdRol { get; set; }
        public string? Nombre { get; set; }
        public string? Column1 { get; set; }

        public virtual ICollection<RolesUsuario> RolesUsuarios { get; set; }
    }
}
