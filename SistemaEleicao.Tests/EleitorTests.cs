
using SistemaEleicao.Console;
using Xunit;

namespace SistemaEleicao.Tests;

public class EleitorTests
{
    [Fact]
    public void RegistrarEleitor_DeveCadastrarDocumentoValido()
    {
        var eleicao = new EleicaoManager();

        var resultado = eleicao.RegistrarEleitor("123456");

        Assert.True(resultado);
        Assert.Single(eleicao.Eleitores);
    }

    [Fact]
    public void RegistrarEleitor_NaoDevePermitirDocumentoDuplicado()
    {
        var eleicao = new EleicaoManager();

        eleicao.RegistrarEleitor("123456");

        var resultado = eleicao.RegistrarEleitor("123456");

        Assert.False(resultado);
        Assert.Single(eleicao.Eleitores);
    }

    [Fact]
    public void Eleitor_DeveRejeitarDocumentoVazio()
    {
        Assert.Throws<ArgumentException>(
            () => new Eleitor(""));
    }
}
