using System;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class Paciente
    {
        public Paciente()
        {
            Casos = new HashSet<Caso>();
            HistoriaClinicas = new HashSet<HistoriaClinica>();
        }

        public decimal IdPaciente { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Dpi { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Genero { get; set; }
        public DateTime? FechaFallecido { get; set; }
        public string? Direccion { get; set; }
        public decimal? IdMunicipio { get; set; }

        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual ICollection<Caso> Casos { get; set; }
        public virtual ICollection<HistoriaClinica> HistoriaClinicas { get; set; }
    }
}
