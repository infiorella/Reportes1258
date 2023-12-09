using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Asistencia
{
    [Key]
    public int IdAsistencia { get; set; }

    public DateTime? FechaAsistencia { get; set; }

    public int? IdAlumno { get; set; }

    public string? Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UserCreacion { get; set; } = null!;

    public virtual Alumno? IdAlumnoNavigation { get; set; }
}
