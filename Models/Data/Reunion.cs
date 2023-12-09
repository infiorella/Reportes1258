using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Reunion
{
    [Key]
    public int IdReunion { get; set; }

    public string TipoReunion { get; set; } = null!;

    public DateTime FechaReunion { get; set; }

    public string? Agenda { get; set; }

    public string? Acuerdos { get; set; }

    public int? Entrega { get; set; }

    public int? Conformidad { get; set; }

    public int? FirmaronPadres { get; set; }

    public int? IdInforme { get; set; }

    public virtual Informe? IdInformeNavigation { get; set; }

    public virtual ICollection<PadreReunion> PadreReunions { get; set; } = new List<PadreReunion>();
}
