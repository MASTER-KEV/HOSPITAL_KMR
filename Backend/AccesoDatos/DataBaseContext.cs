using System;
using System.Collections.Generic;
using AccesoDatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AccesoDatos
{
    public partial class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AplicacionesMedicamento> AplicacionesMedicamentos { get; set; } = null!;
        public virtual DbSet<AsigancionesCama> AsignacionesCamas { get; set; } = null!;
        public virtual DbSet<Cama> Camas { get; set; } = null!;
        public virtual DbSet<Caso> Casos { get; set; } = null!;
        public virtual DbSet<Cita> Citas { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Clinica> Clinicas { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Diagnostico> Diagnosticos { get; set; } = null!;
        public virtual DbSet<DiagnosticosCaso> DiagnosticosCasos { get; set; } = null!;
        public virtual DbSet<DiagnosticosCitum> DiagnosticosCita { get; set; } = null!;
        public virtual DbSet<Examene> Examenes { get; set; } = null!;
        public virtual DbSet<ExamenesCaso> ExamenesCasos { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<Habitacione> Habitaciones { get; set; } = null!;
        public virtual DbSet<HistoriaClinica> HistoriaClinicas { get; set; } = null!;
        public virtual DbSet<MedicamentosCaso> MedicamentosCasos { get; set; } = null!;
        public virtual DbSet<MedicamentosRecetum> MedicamentosReceta { get; set; } = null!;
        public virtual DbSet<Municipio> Municipios { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<ProductosFactura> ProductosFacturas { get; set; } = null!;
        public virtual DbSet<Receta> Recetas { get; set; } = null!;
        public virtual DbSet<ResultadoExamene> ResultadoExamenes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RolesUsuario> RolesUsuarios { get; set; } = null!;
        public virtual DbSet<Sucursale> Sucursales { get; set; } = null!;
        public virtual DbSet<TipoVentum> TipoVenta { get; set; } = null!;
        public virtual DbSet<TiposHabitacion> TiposHabitacions { get; set; } = null!;
        public virtual DbSet<TiposUsuario> TiposUsuarios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("User Id= usr;Password=123;Data Source=localhost/xe;", x => x.UseOracleSQLCompatibility("11"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("USR");

            modelBuilder.Entity<AplicacionesMedicamento>(entity =>
            {
                entity.HasKey(e => e.IdAplicacionMedicamento)
                    .HasName("APLICACIONES_MEDICAMENTOS_PK");

                entity.ToTable("APLICACIONES_MEDICAMENTOS");

                entity.Property(e => e.IdAplicacionMedicamento)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_APLICACION_MEDICAMENTO");

                entity.Property(e => e.FechaHoraAplicacion)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_HORA_APLICACION");

                entity.Property(e => e.IdMedicamentoCaso)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_MEDICAMENTO_CASO");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_USUARIO");

                entity.HasOne(d => d.IdMedicamentoCasoNavigation)
                    .WithMany(p => p.AplicacionesMedicamentos)
                    .HasForeignKey(d => d.IdMedicamentoCaso)
                    .HasConstraintName("APLICACIONES_MEDICAMENTOS_FK1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.AplicacionesMedicamentos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("APLICACIONES_MEDICAMENTOS_FK2");
            });

            modelBuilder.Entity<AsigancionesCama>(entity =>
            {
                entity.HasKey(e => e.IdAsignacionCama)
                    .HasName("ASIGANCIONES_CAMA_PK");

                entity.ToTable("ASIGANCIONES_CAMA");

                entity.Property(e => e.IdAsignacionCama)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_ASIGNACION_CAMA");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_FIN");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INICIO");

                entity.Property(e => e.IdCama)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CAMA");

                entity.Property(e => e.IdCaso)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CASO");

                entity.HasOne(d => d.IdCamaNavigation)
                    .WithMany(p => p.AsigancionesCamas)
                    .HasForeignKey(d => d.IdCama)
                    .HasConstraintName("ASIGANCIONES_CAMA_FK1");

                entity.HasOne(d => d.IdCasoNavigation)
                    .WithMany(p => p.AsigancionesCamas)
                    .HasForeignKey(d => d.IdCaso)
                    .HasConstraintName("ASIGANCIONES_CAMA_FK2");
            });

            modelBuilder.Entity<Cama>(entity =>
            {
                entity.HasKey(e => e.IdCama)
                    .HasName("CAMAS_PK");

                entity.ToTable("CAMAS");

                entity.Property(e => e.IdCama)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CAMA");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_FIN");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INICIO");

                entity.Property(e => e.IdHabitacion)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_HABITACION");

                entity.HasOne(d => d.IdHabitacionNavigation)
                    .WithMany(p => p.Camas)
                    .HasForeignKey(d => d.IdHabitacion)
                    .HasConstraintName("CAMAS_FK1");
            });

            modelBuilder.Entity<Caso>(entity =>
            {
                entity.HasKey(e => e.IdCaso)
                    .HasName("CASO_PK");

                entity.ToTable("CASOS");

                entity.Property(e => e.IdCaso)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CASO");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_FIN");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INICIO");

                entity.Property(e => e.IdPaciente)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_PACIENTE");

                entity.Property(e => e.MotivoFinalizacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MOTIVO_FINALIZACION");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Casos)
                    .HasForeignKey(d => d.IdPaciente)
                    .HasConstraintName("CASOS_FK1");
            });

            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(e => e.IdCita)
                    .HasName("CITAS_PK");

                entity.ToTable("CITAS");

                entity.Property(e => e.IdCita)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CITA");

                entity.Property(e => e.IdCaso)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CASO");

                entity.Property(e => e.IdClinica)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CLINICA");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_USUARIO");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACIONES");

                entity.HasOne(d => d.IdCasoNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdCaso)
                    .HasConstraintName("CITAS_FK1");

                entity.HasOne(d => d.IdClinicaNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdClinica)
                    .HasConstraintName("CITAS_FK2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("CITAS_FK3");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("CLIENTES_PK");

                entity.ToTable("CLIENTES");

                entity.Property(e => e.IdCliente)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CLIENTE");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDOS");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.Nit)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NIT");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRES");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");
            });

            modelBuilder.Entity<Clinica>(entity =>
            {
                entity.HasKey(e => e.IdClinica)
                    .HasName("CLINICAS_PK");

                entity.ToTable("CLINICAS");

                entity.Property(e => e.IdClinica)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CLINICA");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.IdSucursal)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_SUCURSAL");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Clinicas)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("DEPARTAMENTOS_PK");

                entity.ToTable("DEPARTAMENTOS");

                entity.Property(e => e.IdDepartamento)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_DEPARTAMENTO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Diagnostico>(entity =>
            {
                entity.HasKey(e => e.IdDiagnostico)
                    .HasName("DIAGNOSTICOS_PK");

                entity.ToTable("DIAGNOSTICOS");

                entity.Property(e => e.IdDiagnostico)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_DIAGNOSTICO");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<DiagnosticosCaso>(entity =>
            {
                entity.HasKey(e => e.IdDiagnosticosCaso)
                    .HasName("DIAGNOSTICOS_CASO_PK");

                entity.ToTable("DIAGNOSTICOS_CASO");

                entity.Property(e => e.IdDiagnosticosCaso)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_DIAGNOSTICOS_CASO");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_FIN");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INICIO");

                entity.Property(e => e.IdCaso)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CASO");

                entity.Property(e => e.IdDiagnostico)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_DIAGNOSTICO");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_USUARIO");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACIONES");

                entity.HasOne(d => d.IdCasoNavigation)
                    .WithMany(p => p.DiagnosticosCasos)
                    .HasForeignKey(d => d.IdCaso)
                    .HasConstraintName("CASO_DIAGNOSTICOS_CASO_FK1");

                entity.HasOne(d => d.IdDiagnosticoNavigation)
                    .WithMany(p => p.DiagnosticosCasos)
                    .HasForeignKey(d => d.IdDiagnostico)
                    .HasConstraintName("DIAGNOSTICOS_CASO_FK1");
            });

            modelBuilder.Entity<DiagnosticosCitum>(entity =>
            {
                entity.HasKey(e => e.IdDiagnosticoCita)
                    .HasName("DIAGNOSTICOS_CITA_PK");

                entity.ToTable("DIAGNOSTICOS_CITA");

                entity.Property(e => e.IdDiagnosticoCita)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_DIAGNOSTICO_CITA");

                entity.Property(e => e.IdCita)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CITA");

                entity.Property(e => e.IdDiagnostico)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_DIAGNOSTICO");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACION");

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.DiagnosticosCita)
                    .HasForeignKey(d => d.IdCita)
                    .HasConstraintName("DIAGNOSTICOS_CITA_FK1");

                entity.HasOne(d => d.IdDiagnosticoNavigation)
                    .WithMany(p => p.DiagnosticosCita)
                    .HasForeignKey(d => d.IdDiagnostico)
                    .HasConstraintName("DIAGNOSTICOS_CITA_FK2");
            });

            modelBuilder.Entity<Examene>(entity =>
            {
                entity.HasKey(e => e.IdExamen)
                    .HasName("EXAMENES_PK");

                entity.ToTable("EXAMENES");

                entity.Property(e => e.IdExamen)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_EXAMEN");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<ExamenesCaso>(entity =>
            {
                entity.HasKey(e => e.IdExamenCaso)
                    .HasName("EXAMENES_CASO_PK");

                entity.ToTable("EXAMENES_CASO");

                entity.Property(e => e.IdExamenCaso)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_EXAMEN_CASO");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.IdCaso)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CASO");

                entity.Property(e => e.IdCita)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CITA");

                entity.Property(e => e.IdExamen)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_EXAMEN");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_USUARIO");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACIONES");

                entity.HasOne(d => d.IdCasoNavigation)
                    .WithMany(p => p.ExamenesCasos)
                    .HasForeignKey(d => d.IdCaso)
                    .HasConstraintName("EXAMENES_CASO_FK1");

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.ExamenesCasos)
                    .HasForeignKey(d => d.IdCita)
                    .HasConstraintName("EXAMENES_CASO_FK2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ExamenesCasos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("EXAMENES_CASO_FK3");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("FACTURA_PK");

                entity.ToTable("FACTURA");

                entity.Property(e => e.IdFactura)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_FACTURA");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.FechaFactura)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_FACTURA");

                entity.Property(e => e.TotalFactura)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOTAL_FACTURA");
            });

            modelBuilder.Entity<Habitacione>(entity =>
            {
                entity.HasKey(e => e.IdHabitacion)
                    .HasName("HABITACIONES_PK");

                entity.ToTable("HABITACIONES");

                entity.Property(e => e.IdHabitacion)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_HABITACION");

                entity.Property(e => e.IdSucursal)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_SUCURSAL");

                entity.Property(e => e.IdTipoHabitacion)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_TIPO_HABITACION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Habitaciones)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("HABITACIONES_FK1");

                entity.HasOne(d => d.IdTipoHabitacionNavigation)
                    .WithMany(p => p.Habitaciones)
                    .HasForeignKey(d => d.IdTipoHabitacion)
                    .HasConstraintName("HABITACIONES_FK2");
            });

            modelBuilder.Entity<HistoriaClinica>(entity =>
            {
                entity.HasKey(e => e.IdHistoriaClinica)
                    .HasName("HISTORIA_CLINICA_PK");

                entity.ToTable("HISTORIA_CLINICA");

                entity.Property(e => e.IdHistoriaClinica)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_HISTORIA_CLINICA");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INGRESO");

                entity.Property(e => e.Historia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("HISTORIA");

                entity.Property(e => e.IdPaciente)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_PACIENTE");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.HistoriaClinicas)
                    .HasForeignKey(d => d.IdPaciente)
                    .HasConstraintName("HISTORIA_CLINICA_FK1");
            });

            modelBuilder.Entity<MedicamentosCaso>(entity =>
            {
                entity.HasKey(e => e.IdMedicamentoCaso)
                    .HasName("MEDICAMENTOS_CASO_PK");

                entity.ToTable("MEDICAMENTOS_CASO");

                entity.Property(e => e.IdMedicamentoCaso)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_MEDICAMENTO_CASO");

                entity.Property(e => e.Dosis)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DOSIS");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_FIN");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INICIO");

                entity.Property(e => e.Frecuencia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FRECUENCIA");

                entity.Property(e => e.IdProducto)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_USUARIO");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("UNIDAD_MEDIDA");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.MedicamentosCasos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("MEDICAMENTOS_CASO_FK2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.MedicamentosCasos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("MEDICAMENTOS_CASO_FK1");
            });

            modelBuilder.Entity<MedicamentosRecetum>(entity =>
            {
                entity.HasKey(e => e.IdMedicamentosReceta)
                    .HasName("MEDICAMENTOS_RECETA_PK");

                entity.ToTable("MEDICAMENTOS_RECETA");

                entity.Property(e => e.IdMedicamentosReceta)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_MEDICAMENTOS_RECETA");

                entity.Property(e => e.Dosis)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DOSIS");

                entity.Property(e => e.Frecuencia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FRECUENCIA");

                entity.Property(e => e.IdProducto)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.IdReceta)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_RECETA");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACIONES");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("UNIDAD_MEDIDA");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.MedicamentosReceta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("MEDICAMENTOS_RECETA_FK2");

                entity.HasOne(d => d.IdRecetaNavigation)
                    .WithMany(p => p.MedicamentosReceta)
                    .HasForeignKey(d => d.IdReceta)
                    .HasConstraintName("MEDICAMENTOS_RECETA_FK1");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("MUNICIPIOS_PK");

                entity.ToTable("MUNICIPIOS");

                entity.Property(e => e.IdMunicipio)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_MUNICIPIO");

                entity.Property(e => e.IdDepartamento)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_DEPARTAMENTO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MUNICIPIOS_FK1");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente)
                    .HasName("PACIENTES_PK");

                entity.ToTable("PACIENTES");

                entity.Property(e => e.IdPaciente)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_PACIENTE");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDOS");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.Dpi)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DPI");

                entity.Property(e => e.FechaFallecido)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_FALLECIDO");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_NACIMIENTO");

                entity.Property(e => e.Genero)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("GENERO");

                entity.Property(e => e.IdMunicipio)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_MUNICIPIO");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRES");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("PACIENTES_FK1");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PRODUCTOS_PK");

                entity.ToTable("PRODUCTOS");

                entity.Property(e => e.IdProducto)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEN");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<ProductosFactura>(entity =>
            {
                entity.HasKey(e => e.IdProductoFactura)
                    .HasName("PRODUCTOS_FACTURA_PK");

                entity.ToTable("PRODUCTOS_FACTURA");

                entity.Property(e => e.IdProductoFactura)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_PRODUCTO_FACTURA");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CANTIDAD");

                entity.Property(e => e.IdFactura)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_FACTURA");

                entity.Property(e => e.IdProducto)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRECIO_UNITARIO");

                entity.Property(e => e.Total)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOTAL");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.ProductosFacturas)
                    .HasForeignKey(d => d.IdFactura)
                    .HasConstraintName("PRODUCTOS_FACTURA_FK1");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProductosFacturas)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("PRODUCTOS_FACTURA_FK2");
            });

            modelBuilder.Entity<Receta>(entity =>
            {
                entity.HasKey(e => e.IdReceta)
                    .HasName("RECETAS_PK");

                entity.ToTable("RECETAS");

                entity.Property(e => e.IdReceta)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_RECETA");

                entity.Property(e => e.IdCita)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CITA");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_USUARIO");

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.Receta)
                    .HasForeignKey(d => d.IdCita)
                    .HasConstraintName("CITA_RECETAS_FK1");
            });

            modelBuilder.Entity<ResultadoExamene>(entity =>
            {
                entity.HasKey(e => e.IdResultadoExamenes)
                    .HasName("RESULTADO_EXAMENES_PK");

                entity.ToTable("RESULTADO_EXAMENES");

                entity.Property(e => e.IdResultadoExamenes)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_RESULTADO_EXAMENES");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.IdExamenCaso)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_EXAMEN_CASO");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACION");

                entity.HasOne(d => d.IdExamenCasoNavigation)
                    .WithMany(p => p.ResultadoExamenes)
                    .HasForeignKey(d => d.IdExamenCaso)
                    .HasConstraintName("RESULTADO_EXAMENES_FK1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("ROLE_PK");

                entity.ToTable("ROLE");

                entity.Property(e => e.IdRol)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_ROL");

                entity.Property(e => e.Column1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("COLUMN1");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<RolesUsuario>(entity =>
            {
                entity.HasKey(e => e.IdRolUsuario)
                    .HasName("ROLES_USUARIO_PK");

                entity.ToTable("ROLES_USUARIO");

                entity.Property(e => e.IdRolUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_ROL_USUARIO");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.IdRol)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_ROL");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_USUARIO");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolesUsuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("ROLES_USUARIO_FK1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.RolesUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("ROLES_USUARIO_FK2");
            });

            modelBuilder.Entity<Sucursale>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("SUCURSALES_PK");

                entity.ToTable("SUCURSALES");

                entity.Property(e => e.IdSucursal)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_SUCURSAL");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.IdMunicipio)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_MUNICIPIO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Sucursales)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("SUCURSALES_FK1");
            });

            modelBuilder.Entity<TipoVentum>(entity =>
            {
                entity.HasKey(e => e.IdTipoVenta)
                    .HasName("TIPO_VENTA_PK");

                entity.ToTable("TIPO_VENTA");

                entity.Property(e => e.IdTipoVenta)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_TIPO_VENTA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");
            });

            modelBuilder.Entity<TiposHabitacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoHabitacion)
                    .HasName("TIPOS_HABITACION_PK");

                entity.ToTable("TIPOS_HABITACION");

                entity.Property(e => e.IdTipoHabitacion)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_TIPO_HABITACION");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");
            });

            modelBuilder.Entity<TiposUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("TIPOS_USUARIO_PK");

                entity.ToTable("TIPOS_USUARIO");

                entity.Property(e => e.IdTipoUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_TIPO_USUARIO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("USUARIOS_PK");

                entity.ToTable("USUARIOS");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_USUARIO");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDOS");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_NACIMIENTO");

                entity.Property(e => e.IdTipoUsuario)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_TIPO_USUARIO");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRES");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("USUARIOS_FK1");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("VENTAS_PK");

                entity.ToTable("VENTAS");

                entity.Property(e => e.IdVenta)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_VENTA");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.FechaVenta)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_VENTA");

                entity.Property(e => e.IdCliente)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdFactura)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_FACTURA");

                entity.Property(e => e.IdTipoVenta)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID_TIPO_VENTA");

                entity.Property(e => e.Total)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOTAL");

                entity.Property(e => e.UsuarioVenta)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USUARIO_VENTA");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("VENTAS_FK2");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdFactura)
                    .HasConstraintName("VENTAS_FK1");

                entity.HasOne(d => d.IdTipoVentaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdTipoVenta)
                    .HasConstraintName("VENTAS_FK3");

                entity.HasOne(d => d.UsuarioVentaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.UsuarioVenta)
                    .HasConstraintName("VENTAS_FK4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
