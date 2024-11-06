using System;
using System.Threading;
using System.Threading.Tasks;
using DataContext;
using DesafioFrete.Controllers;
using DesafioFrete.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace FreteDesafioTeste;

public class TestesControllers
{
    private readonly Mock<FreteDatabaseContext> _mockContext;

    private readonly FreteController _controller;

    private readonly ITestOutputHelper _output;

    public TestesControllers(ITestOutputHelper outputHelper)
    {
        _output = outputHelper;
        _mockContext = new Mock<FreteDatabaseContext>();
        _controller = new FreteController(_mockContext.Object);
    }

    [Fact]
    public async Task CreateFreteTest()
    {
        Frete frete = new Frete(5, 10, "cidadeOrigemCerto!", "cidadeDestinoCerto!");

        _mockContext.Setup(c => c.Fretes.AddAsync(frete, default)).ReturnsAsync((Frete f, CancellationToken ct) =>
        {
            return Mock.Of<EntityEntry<Frete>>(e => e.Entity == f);
        }
        );
        _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.CreateFrete(frete);
        if (result.Result is CreatedAtActionResult createdAtResult)
        {
            // Verifique o conteúdo retornado
            var returnFrete = Assert.IsType<Frete>(createdAtResult.Value);

            Console.WriteLine($"Esperado FreteId: {frete.FreteId}, Retornado FreteId: {returnFrete.FreteId}");
            Console.WriteLine($"Esperado ValorFrete: {frete.ValorFrete}, Retornado ValorFrete: {returnFrete.ValorFrete}");
            Console.WriteLine($"Esperado ValorTaxa: {frete.ValorTaxa}, Retornado ValorTaxa: {returnFrete.ValorTaxa}");
            Console.WriteLine($"Esperado ActionName: 'GetFrete', Retornado ActionName: {createdAtResult.ActionName}");

            Assert.Equal(frete.FreteId, returnFrete.FreteId);
            Assert.Equal(frete.ValorFrete, returnFrete.ValorFrete);
            Assert.Equal(frete.ValorTaxa, returnFrete.ValorTaxa);
            Assert.Equal("GetFrete", createdAtResult.ActionName);
        }
        else
        {
            // Exibe um erro personalizado se o tipo não for o esperado
            Assert.True(false, "O resultado não é do tipo CreatedAtActionResult.");
        }
    }
}