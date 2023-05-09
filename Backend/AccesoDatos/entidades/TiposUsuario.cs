using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class TiposUsuario
    {
        public TiposUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public decimal IdTipoUsuario { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
