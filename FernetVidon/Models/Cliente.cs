using System;
using System.Collections.Generic;

namespace FernetVidon.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int dni { get; set; }

    public string Nombre { get; set; } 

    public string Apellido { get; set; } 

    public DateTime FechaNacimiento { get; set; }

    public string NumeroTelefono { get; set; } 

    public virtual ICollection<Botellas> Botellas { get; set; } = new List<Botellas>();
}
