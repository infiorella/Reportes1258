using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class AlumnoComportamiento
{
    [Key]
    public int IdAlumnoComportamiento { get; set; }

    public int IdComportamiento { get; set; }

    public int IdAlumno { get; set; }

    public virtual Alumno IdAlumnoNavigation { get; set; } = null!;

    public virtual Comportamiento IdComportamientoNavigation { get; set; } = null!;
}
