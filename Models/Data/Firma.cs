using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Firma
{
    [Key]
    public int IdPadreReunion { get; set; }

    public int? IdPadre { get; set; }

    public string? FirmaLink { get; set; }

    public int? IdReunion { get; set; }

    public int? IdCompromiso { get; set; }

    public virtual Compromiso? IdCompromisoNavigation { get; set; }

    public virtual Padre? IdPadreNavigation { get; set; }

    public virtual Reunion? IdReunionNavigation { get; set; }
}
