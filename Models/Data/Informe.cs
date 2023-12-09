
using System;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Informe
{
    [Key]
    public int IdInforme { get; set; }

    public int IdPersonal { get; set; }

    public string? ClasificacionAi { get; set; }

    public int Tipo { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? Mes { get; set; }

    public int? Trimestre { get; set; }

    public string Link { get; set; } = null!;

    public string? Comentario { get; set; }

    public string? EstadoInforme { get; set; }

    public int IdEstado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public string? ComentarioDirector { get; set; }

    public virtual ICollection<Comportamiento> Comportamientos { get; set; } = new List<Comportamiento>();

    public virtual ICollection<Compromiso> Compromisos { get; set; } = new List<Compromiso>();

    public virtual PersonalEducativo IdPersonalNavigation { get; set; } = null!;

    public virtual ICollection<Reunion> Reunions { get; set; } = new List<Reunion>();
}
