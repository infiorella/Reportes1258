using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Alumno
{
    [Key]
    public int IdAlumno { get; set; }

    public int? Grado { get; set; }

    public string Seccion { get; set; } = null!;

    public string? TipoDocumento { get; set; }

    public string? NumeroDocumento { get; set; }

    public int? ValidadoReniec { get; set; }

    public string CodigoEstudiante { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string? Sexo { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
    public DateTime? FechaNacimiento { get; set; }

    public string EstadoMatricula { get; set; } = null!;

    public string? SituacionMatricula { get; set; }

    public string? Pais { get; set; }

    public string? PadreVive { get; set; }

    public string? MadreVive { get; set; }

    public string? LenguaMaterna { get; set; }

    public string? SegundaLengua { get; set; }

    public string? TrabajaEstudiante { get; set; }

    public int? HorasLabora { get; set; }

    public string? EscolaridadMadre { get; set; }

    public string? TipoDiscapacidad { get; set; }

    public string? CodigoModular { get; set; }

    public string? NombreRjrd { get; set; }

    public int? IdEstado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UserCreacion { get; set; } = null!;

    public string? UserModificacion { get; set; }

    public virtual ICollection<AlumnoComportamiento> AlumnoComportamientos { get; set; } = new List<AlumnoComportamiento>();

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();

    public virtual ICollection<Padre> Padres { get; set; } = new List<Padre>();
}
