﻿using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BackEnd.Entities
{
    public partial class EmpleosContext : DbContext
    {
        public EmpleosContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmpleosContext>();
            optionsBuilder.UseSqlServer(Utilities.Util.ConnectionString);
        }

        public EmpleosContext(DbContextOptions<EmpleosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aplicacione> Aplicaciones { get; set; }
        public virtual DbSet<Bitacora> Bitacoras { get; set; }
        public virtual DbSet<Empleo> Empleos { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Filtro> Filtros { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Utilities.Util.ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging(true);
            base.OnConfiguring(optionsBuilder);
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("Server=.;Database=Empleos;Integrated Security=True;Trusted_Connection=True;");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Aplicacione>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.EmpleoId).HasColumnName("EmpleoID");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.HasOne(d => d.Empleo)
                    .WithMany()
                    .HasForeignKey(d => d.EmpleoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aplicaciones_Empleo");

                entity.HasOne(d => d.Usuario)
                    .WithMany()
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aplicaciones_Usuario");
            });

            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.HasKey(e => e.UsuarioId)
                    .HasName("PK__Bitacora__2AFD262188B434FB");

                entity.ToTable("Bitacora");

                entity.Property(e => e.UsuarioId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UsuarioID");

                entity.Property(e => e.Error).IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.Usuario)
                    .WithOne(p => p.Bitacora)
                    .HasForeignKey<Bitacora>(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bitacora_Usuario");
            });

            modelBuilder.Entity<Empleo>(entity =>
            {
                entity.Property(e => e.EmpleoId).HasColumnName("EmpleoID");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

                entity.Property(e => e.Especializacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("especializacion");

                entity.Property(e => e.NombreEmpleo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreEmpleo");

                entity.Property(e => e.Salario)
                    .HasColumnType("decimal(11, 2)")
                    .HasColumnName("salario");

                entity.Property(e => e.Ubicacion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ubicacion");

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.Empleos)
                    .HasForeignKey(d => d.EmpresaId)
                    .HasConstraintName("R_6");

                entity.HasOne(d => d.EspecializacionNavigation)
                    .WithMany(p => p.Empleos)
                    .HasForeignKey(d => d.Especializacion)
                    .HasConstraintName("FK_Empleos_Spec");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresa");

                entity.Property(e => e.EmpresaId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmpresaID");

                entity.Property(e => e.CedulaJuridica)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cedulaJuridica");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreEmpresa");

                entity.HasOne(d => d.CodigoNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.Codigo)
                    .HasConstraintName("R_7");
            });

            modelBuilder.Entity<Filtro>(entity =>
            {
                entity.HasKey(e => e.Especializacion);

                entity.Property(e => e.Especializacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("especializacion");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("XPKRoles");

                entity.Property(e => e.Codigo)
                    .ValueGeneratedNever()
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Cv).HasColumnName("CV");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}