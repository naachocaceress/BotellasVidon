using System;
using System.Collections.Generic;

namespace VidonBotellasMVC.Models;

public partial class Sucursales
{
    public int IdSucursal { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Botella> Botellas { get; set; } = new List<Botella>();
}
