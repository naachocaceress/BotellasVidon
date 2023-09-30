using System;
using System.Collections.Generic;

namespace FernetVidon.Models;

public partial class Sucursales
{
    public int IdSucursal { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Botellas> Botellas { get; set; } = new List<Botellas>();

}
