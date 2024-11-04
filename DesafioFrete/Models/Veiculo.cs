using System;
using System.Collections.Generic;

namespace DesafioFrete.Models;

public partial class Veiculo
{
    public long VeiculoId { get; set; }

    public int PesoVeiculo { get; set; }

    public string Placa { get; set; } = null!;

    public string Renavam { get; set; } = null!;
}
