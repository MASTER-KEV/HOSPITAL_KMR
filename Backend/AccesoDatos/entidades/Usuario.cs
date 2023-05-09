using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Usuario
    {
        public Usuario()
        {
            AplicacionesMedicamentos = new HashSet<AplicacionesMedicamento>();
            Cita = new HashSet<Cita>();
            ExamenesCasos = new HashSet<ExamenesCaso>();
            MedicamentosCasos = new HashSet<MedicamentosCaso>();
            RolesUsuarios = new HashSet<RolesUsuario>();
            Venta = new HashSet<Venta>();
        }

        public decimal IdUsuario { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Estado { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public decimal? IdTipoUsuario { get; set; }

        public virtual TiposUsuario? IdTipoUsuarioNavigation { get; set; }
        public virtual ICollection<AplicacionesMedicamento> AplicacionesMedicamentos { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<ExamenesCaso> ExamenesCasos { get; set; }
        public virtual ICollection<MedicamentosCaso> MedicamentosCasos { get; set; }
        public virtual ICollection<RolesUsuario> RolesUsuarios { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
