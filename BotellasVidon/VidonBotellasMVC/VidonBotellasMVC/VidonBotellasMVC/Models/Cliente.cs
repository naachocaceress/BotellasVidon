using System;
using System.Collections.Generic;

namespace VidonBotellasMVC.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int Dni { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string NumeroTelefono { get; set; } = null!;

    public virtual ICollection<Botella> Botellas { get; set; } = new List<Botella>();
}
