using Models.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class PersonalEducativo
{
    [Key]
    public int IdPersonal { get; set; }

    public int? Grado { get; set; }

    public string? Seccion { get; set; }

    public string Apellidos { get; set; } = null!;

    public string? Nombres { get; set; }

    public string Dni { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Imagen { get; set; }

    public string? Direccion { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public int? Rol { get; set; }

    public string? IdUsuario { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UserCreacion { get; set; }

    public string? UserModificacion { get; set; }

    public virtual ApplicationUser? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Informe> Informes { get; set; } = new List<Informe>();
}
