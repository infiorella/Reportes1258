using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models;

public partial class Colegio1258Context : DbContext
{
    public Colegio1258Context()
    {
    }

    public Colegio1258Context(DbContextOptions<Colegio1258Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<AlumnoComportamiento> AlumnoComportamientos { get; set; }

    public virtual DbSet<Asistencia> Asistencia { get; set; }

    public virtual DbSet<Comportamiento> Comportamientos { get; set; }

    public virtual DbSet<Compromiso> Compromisos { get; set; }

    public virtual DbSet<Concepto> Conceptos { get; set; }

    public virtual DbSet<Informe> Informes { get; set; }

    public virtual DbSet<Nota> Nota { get; set; }

    public virtual DbSet<Padre> Padres { get; set; }

    public virtual DbSet<PadreReunion> PadreReunions { get; set; }

    public virtual DbSet<PersonalEducativo> PersonalEducativos { get; set; }

    public virtual DbSet<Reunion> Reunions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno);

            entity.ToTable("Alumno");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CodigoEstudiante)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.CodigoModular)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.EscolaridadMadre)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.EstadoMatricula)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.IdEstado).HasDefaultValueSql("((1))");
            entity.Property(e => e.LenguaMaterna)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MadreVive)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NombreRjrd)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("NombreRJRD");
            entity.Property(e => e.Nombres)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PadreVive)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Pais)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Seccion)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SegundaLengua)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SituacionMatricula)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TipoDiscapacidad)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.TrabajaEstudiante)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserCreacion)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UserModificacion)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AlumnoComportamiento>(entity =>
        {
            entity.HasKey(e => e.IdAlumnoComportamiento).HasName("PK_AlumnoComportamiento");

            entity.ToTable("AlumnoComportamiento");

            entity.Property(e => e.IdAlumnoComportamiento).HasColumnName("idAlumnoComportamiento");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.AlumnoComportamientos)
                .HasForeignKey(d => d.IdAlumno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Alumno_AlumnoComportamiento");

            entity.HasOne(d => d.IdComportamientoNavigation).WithMany(p => p.AlumnoComportamientos)
                .HasForeignKey(d => d.IdComportamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Comportamiento_AlumnoComportamiento");
        });

        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia);

            entity.Property(e => e.IdAsistencia).HasColumnName("idAsistencia");
            entity.Property(e => e.Estado)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FechaAsistencia).HasColumnType("date");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.UserCreacion)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("FK_Alumno_Asistencia");
        });

        modelBuilder.Entity<Comportamiento>(entity =>
        {
            entity.HasKey(e => e.IdComportamiento).HasName("PK_Comportamiento");

            entity.ToTable("Comportamiento");

            entity.Property(e => e.IdComportamiento).HasColumnName("idComportamiento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.FechaIncidencia).HasColumnType("datetime");
            entity.Property(e => e.MedidasCorrectivas)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Observacion)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdInformeNavigation).WithMany(p => p.Comportamientos)
                .HasForeignKey(d => d.IdInforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Comportamiento_Informe");
        });

        modelBuilder.Entity<Compromiso>(entity =>
        {
            entity.HasKey(e => e.IdCompromiso).HasName("PK_Compromiso");

            entity.ToTable("Compromiso");

            entity.Property(e => e.IdCompromiso).HasColumnName("idCompromiso");
            entity.Property(e => e.Compromisos)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.FechaCompromiso).HasColumnType("datetime");

            entity.HasOne(d => d.IdInformeNavigation).WithMany(p => p.Compromisos)
                .HasForeignKey(d => d.IdInforme)
                .HasConstraintName("fk_Compromiso_Informe");
        });

        modelBuilder.Entity<Concepto>(entity =>
        {
            entity.HasKey(e => e.IdConcepto).HasName("PK_Concepto");

            entity.Property(e => e.IdConcepto)
                .ValueGeneratedNever()
                .HasColumnName("idConcepto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Informe>(entity =>
        {
            entity.HasKey(e => e.IdInforme).HasName("PK_Informe");

            entity.ToTable("Informe");

            entity.Property(e => e.IdInforme).HasColumnName("idInforme");
            entity.Property(e => e.ClasificacionAi)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ClasificacionAI");
            entity.Property(e => e.Comentario)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ComentarioDirector)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.EstadoInforme)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("('P')")
                .IsFixedLength();
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaEnvio).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IdEstado).HasDefaultValueSql("((1))");
            entity.Property(e => e.Link)
                .HasMaxLength(90)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Personal_Informe");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.IdNota);

            entity.Property(e => e.IdNota).HasColumnName("idNota");
            entity.Property(e => e.AreaCurricular)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Cal1)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Cal2)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Cal3)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CalAnual)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Comentario)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Competencia)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdEstado).HasDefaultValueSql("((1))");
            entity.Property(e => e.Seccion)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserCreacion)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("FK_Alumno_Nota");
        });

        modelBuilder.Entity<Padre>(entity =>
        {
            entity.HasKey(e => e.IdPadre);

            entity.ToTable("Padre");

            entity.Property(e => e.IdPadre).HasColumnName("idPadre");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(90)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombres)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Numero)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Parentesco)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.UserCreacion)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UserModificacion)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Padres)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("FK_Alumno_Padre");
        });

        modelBuilder.Entity<PadreReunion>(entity =>
        {
            entity.HasKey(e => e.IdPadreReunion).HasName("PK_PadreReunion");

            entity.ToTable("PadreReunion");

            entity.Property(e => e.IdPadreReunion).HasColumnName("idPadreReunion");
            entity.Property(e => e.FirmaLink)
                .HasMaxLength(90)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCompromisoNavigation).WithMany(p => p.PadreReunions)
                .HasForeignKey(d => d.IdCompromiso)
                .HasConstraintName("fk_Compromiso_Firma");

            entity.HasOne(d => d.IdPadreNavigation).WithMany(p => p.PadreReunions)
                .HasForeignKey(d => d.IdPadre)
                .HasConstraintName("fk_Padre_Firma");

            entity.HasOne(d => d.IdReunionNavigation).WithMany(p => p.PadreReunions)
                .HasForeignKey(d => d.IdReunion)
                .HasConstraintName("fk_Reunion_Firma");
        });

        modelBuilder.Entity<PersonalEducativo>(entity =>
        {
            entity.HasKey(e => e.IdPersonal);

            entity.ToTable("PersonalEducativo");

            entity.Property(e => e.IdPersonal).HasColumnName("idPersonal");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Dni)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Imagen).HasColumnType("text");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Seccion)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserCreacion)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UserModificacion)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PersonalEducativos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Usuario_Personal");
        });

        modelBuilder.Entity<Reunion>(entity =>
        {
            entity.HasKey(e => e.IdReunion).HasName("PK_Reunion");

            entity.ToTable("Reunion");

            entity.Property(e => e.IdReunion).HasColumnName("idReunion");
            entity.Property(e => e.Acuerdos)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Agenda)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.FechaReunion).HasColumnType("datetime");
            entity.Property(e => e.TipoReunion)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.IdInformeNavigation).WithMany(p => p.Reunions)
                .HasForeignKey(d => d.IdInforme)
                .HasConstraintName("fk_Reunion_Informe");
        });

       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
