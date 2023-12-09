using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Nota
{
    [Key]
    public int IdNota { get; set; }

    public int? IdAlumno { get; set; }

    public int Grado { get; set; }

    public string Seccion { get; set; } = null!;

    public string AreaCurricular { get; set; } = null!;

    public string? Competencia { get; set; }

    public string? Cal1 { get; set; }

    public string? Cal2 { get; set; }

    public string? Cal3 { get; set; }

    public string? CalAnual { get; set; }

    public string? Comentario { get; set; }

    public int? IdEstado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UserCreacion { get; set; } = null!;

    public virtual Alumno? IdAlumnoNavigation { get; set; }
}
