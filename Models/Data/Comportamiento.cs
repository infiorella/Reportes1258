using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Comportamiento
{
    [Key]
    public int IdComportamiento { get; set; }

    public DateTime FechaIncidencia { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Tipo { get; set; }

    public int? Estado { get; set; }

    public string? Observacion { get; set; }

    public string? MedidasCorrectivas { get; set; }

    public int IdInforme { get; set; }

    public virtual ICollection<AlumnoComportamiento> AlumnoComportamientos { get; set; } = new List<AlumnoComportamiento>();

    public virtual Informe IdInformeNavigation { get; set; } = null!;
}
