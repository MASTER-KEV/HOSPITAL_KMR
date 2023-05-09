using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class RolesUsuario
    {
        public decimal IdRolUsuario { get; set; }
        public decimal? IdRol { get; set; }
        public decimal? IdUsuario { get; set; }
        public string? Estado { get; set; }

        public virtual Role? IdRolNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
