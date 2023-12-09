using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Padre
{
    [Key]
    public int IdPadre { get; set; }

    public string Apellidos { get; set; } = null!;

    public string? Nombres { get; set; }

    public string? Sexo { get; set; }

    public string? Parentesco { get; set; }

    public string? TipoDocumento { get; set; }

    public string? Numero { get; set; }

    public int ValidadoReniec { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Telefono { get; set; }

    public int? IdAlumno { get; set; }

    public int? IdEstado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UserCreacion { get; set; } = null!;

    public string? UserModificacion { get; set; }

    public virtual Alumno? IdAlumnoNavigation { get; set; }

    public virtual ICollection<PadreReunion> PadreReunions { get; set; } = new List<PadreReunion>();
}
