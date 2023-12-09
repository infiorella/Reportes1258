using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Compromiso
{
    [Key]
    public int IdCompromiso { get; set; }

    public DateTime FechaCompromiso { get; set; }

    public string Compromisos { get; set; } = null!;

    public int? IdInforme { get; set; }

    public virtual Informe? IdInformeNavigation { get; set; }

    public virtual ICollection<PadreReunion> PadreReunions { get; set; } = new List<PadreReunion>();
}
