using System;
using System.Collections.Generic;

namespace FernetVidon.Models;

public partial class Botellas
{
    public int IdBotella { get; set; }

    public string? QuienGuardoMozo { get; set; }

    public DateTime FechaGuardado { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public string Estado { get; set; } = null!;

    public int? IdCliente { get; set; }

    public int? IdSucursal { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Sucursales? IdSucursalNavigation { get; set; }
}
