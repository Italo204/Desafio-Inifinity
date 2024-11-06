using System;
using System.Collections.Generic;

namespace DesafioFrete.Models;

public partial class Usuario
{
    public long UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string? Telefone { get; set; }

    public string Role { get; set; }

    public virtual ICollection<Frete> Fretes { get; set; } = new List<Frete>();
}
