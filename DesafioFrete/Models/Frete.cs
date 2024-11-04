using System;
using System.Collections.Generic;

namespace DesafioFrete.Models;

public partial class Frete
{
    public long FreteId { get; set; }

    public int PesoVeiculo { get; set; }

    public int Distancia { get; set; }

    public string CidadeOrigem { get; set; } = null!;

    public string CidadeDestino { get; set; } = null!;

    public decimal ValorFrete { get; set; }

    public decimal ValorTaxa { get; set; }

    public long UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;

    public Frete()
    {
    }

    public Frete(int pesoVeiculo, int distancia, string cidadeOrigem, string cidadeDestino)
    {
        PesoVeiculo = pesoVeiculo;
        Distancia = distancia;
        CidadeOrigem = cidadeOrigem;
        CidadeDestino = cidadeDestino;

        // Calcular os valores iniciais
        ValorFrete = CalcularValorFrete(Distancia, PesoVeiculo);
        ValorTaxa = CalcularValorTaxa(ValorFrete, Distancia);
    }

    private decimal CalcularValorFrete(int distancia, int pesoVeiculo)
    {
        return distancia * pesoVeiculo;
    }

    private decimal CalcularValorTaxa(decimal valor, int distancia)
    {
        decimal porcentagem;
        if (distancia <= 100)
        {
            porcentagem = 20;
        } else if(distancia > 100 && distancia <= 200)
        {
            porcentagem = 15;
        } else if(distancia > 200 && distancia <= 500)
        {
            porcentagem = 10;
        } else
        {
            porcentagem = 7.5M;
        }

        var taxa = (porcentagem / 100) * valor;
        return taxa;
    }
}
