using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace testuser.Models.SysModel
{
    public partial class dbModel : DbContext
    {
        public dbModel()
           : base("name=DefaultConnection")
        {
        }
        public virtual DbSet<tblAfavorde> tblAfavorde { get; set; }
        public virtual DbSet<tblArchivos> tblArchivos { get; set; }
        public virtual DbSet<tblCasos> tblCasos { get; set; }
        public virtual DbSet<tblCategoriaPersonal> tblCategoriaPersonal { get; set; }
        public virtual DbSet<tblCategorias> tblCategorias { get; set; }
        public virtual DbSet<tblClientes> tblClientes { get; set; }
        public virtual DbSet<tblDepartamentos> tblDepartamentos { get; set; }
        public virtual DbSet<tblEstadosCasos> tblEstadosCasos { get; set; }
        public virtual DbSet<tblEstadosRegistros> tblEstadosRegistros { get; set; }
        public virtual DbSet<tblEventCalendar> tblEventCalendar { get; set; }
        public virtual DbSet<tblHistorial> tblHistorial { get; set; }
        public virtual DbSet<tblJuzgados> tblJuzgados { get; set; }
        public virtual DbSet<tblLibros> tblLibros { get; set; }
        public virtual DbSet<tblMunicipios> tblMunicipios { get; set; }
        public virtual DbSet<tblOtorgante> tblOtorgante { get; set; }
        public virtual DbSet<tblPersonal> tblPersonal { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblAfavorde>()
                .Property(e => e.nombres)
                .IsUnicode(false);

            modelBuilder.Entity<tblAfavorde>()
                .Property(e => e.apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<tblAfavorde>()
                .Property(e => e.redsocial)
                .IsUnicode(false);

            modelBuilder.Entity<tblAfavorde>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tblAfavorde>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<tblAfavorde>()
                .Property(e => e.lugarnacimiento)
                .IsUnicode(false);

            modelBuilder.Entity<tblAfavorde>()
                .Property(e => e.municipio)
                .IsUnicode(false);

            modelBuilder.Entity<tblAfavorde>()
                .Property(e => e.departamento)
                .IsUnicode(false);

            modelBuilder.Entity<tblAfavorde>()
                .HasMany(e => e.tblLibros)
                .WithRequired(e => e.tblAfavorde)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblArchivos>()
                .Property(e => e.Archivo)
                .IsUnicode(false);

            modelBuilder.Entity<tblArchivos>()
                .Property(e => e.Descripción)
                .IsUnicode(false);

            modelBuilder.Entity<tblCasos>()
                .Property(e => e.Numero_Caso)
                .IsFixedLength();

            modelBuilder.Entity<tblCasos>()
                .HasMany(e => e.tblArchivos)
                .WithRequired(e => e.tblCasos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblCasos>()
                .HasMany(e => e.tblHistorial)
                .WithRequired(e => e.tblCasos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblCategoriaPersonal>()
                .Property(e => e.CategoriaPersonal)
                .IsUnicode(false);

            modelBuilder.Entity<tblCategoriaPersonal>()
                .HasMany(e => e.tblPersonal)
                .WithRequired(e => e.tblCategoriaPersonal)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblCategorias>()
                .Property(e => e.Nombre_Categoria)
                .IsUnicode(false);

            modelBuilder.Entity<tblCategorias>()
                .HasMany(e => e.tblCasos)
                .WithRequired(e => e.tblCategorias)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblClientes>()
                .Property(e => e.Nombres)
                .IsUnicode(false);

            modelBuilder.Entity<tblClientes>()
                .Property(e => e.Apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<tblClientes>()
                .Property(e => e.Genero)
                .IsUnicode(false);

            modelBuilder.Entity<tblClientes>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tblClientes>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<tblClientes>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<tblClientes>()
                .HasMany(e => e.tblCasos)
                .WithRequired(e => e.tblClientes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDepartamentos>()
                .Property(e => e.Departamento)
                .IsUnicode(false);

            modelBuilder.Entity<tblDepartamentos>()
                .HasMany(e => e.tblJuzgados)
                .WithRequired(e => e.tblDepartamentos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDepartamentos>()
                .HasMany(e => e.tblMunicipios)
                .WithRequired(e => e.tblDepartamentos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblEstadosCasos>()
                .HasMany(e => e.tblCasos)
                .WithRequired(e => e.tblEstadosCasos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblEstadosRegistros>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<tblEstadosRegistros>()
                .HasMany(e => e.tblClientes)
                .WithRequired(e => e.tblEstadosRegistros)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblEventCalendar>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<tblEventCalendar>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<tblEventCalendar>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<tblEventCalendar>()
                .Property(e => e.textColor)
                .IsUnicode(false);

            modelBuilder.Entity<tblHistorial>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<tblHistorial>()
                .Property(e => e.Archivo)
                .IsUnicode(false);

            modelBuilder.Entity<tblJuzgados>()
                .Property(e => e.Juzgado)
                .IsUnicode(false);

            modelBuilder.Entity<tblJuzgados>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tblJuzgados>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<tblJuzgados>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<tblLibros>()
                .Property(e => e.instrumento)
                .IsUnicode(false);

            modelBuilder.Entity<tblLibros>()
                .Property(e => e.folios)
                .IsUnicode(false);

            modelBuilder.Entity<tblLibros>()
                .Property(e => e.libro)
                .IsUnicode(false);

            modelBuilder.Entity<tblLibros>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<tblMunicipios>()
                .Property(e => e.Municipio)
                .IsUnicode(false);

            modelBuilder.Entity<tblMunicipios>()
                .HasMany(e => e.tblJuzgados)
                .WithRequired(e => e.tblMunicipios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblOtorgante>()
                .Property(e => e.nombres)
                .IsUnicode(false);

            modelBuilder.Entity<tblOtorgante>()
                .Property(e => e.apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<tblOtorgante>()
                .Property(e => e.redsocial)
                .IsUnicode(false);

            modelBuilder.Entity<tblOtorgante>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tblOtorgante>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<tblOtorgante>()
                .Property(e => e.lugarnacimiento)
                .IsUnicode(false);

            modelBuilder.Entity<tblOtorgante>()
                .Property(e => e.municipio)
                .IsUnicode(false);

            modelBuilder.Entity<tblOtorgante>()
                .Property(e => e.departamento)
                .IsUnicode(false);

            modelBuilder.Entity<tblOtorgante>()
                .HasMany(e => e.tblLibros)
                .WithRequired(e => e.tblOtorgante)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblPersonal>()
                .Property(e => e.Nombres)
                .IsUnicode(false);

            modelBuilder.Entity<tblPersonal>()
                .Property(e => e.Apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<tblPersonal>()
                .Property(e => e.redsocial)
                .IsUnicode(false);

            modelBuilder.Entity<tblPersonal>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tblPersonal>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<tblPersonal>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<tblPersonal>()
                .HasMany(e => e.tblLibros)
                .WithRequired(e => e.tblPersonal)
                .WillCascadeOnDelete(false);

            //Database.SetInitializer<dbModel>(null);
            //base.OnModelCreating(modelBuilder);
        }
    }
}