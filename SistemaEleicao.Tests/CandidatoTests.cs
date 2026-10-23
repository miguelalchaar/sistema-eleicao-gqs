
using SistemaEleicao.Console;
using Xunit;

namespace SistemaEleicao.Tests;

public class CandidatoTests
{
    [Fact]
    public void CadastrarCandidato_DeveCadastrarQuandoNumeroForInedito()
    {
        var eleicao = new EleicaoManager();

        var resultado = eleicao.CadastrarCandidato("Flavio Bolsonaro", 17);

        Assert.True(resultado);
        Assert.Single(eleicao.Candidatos);
    }

    [Fact]
    public void CadastrarCandidato_NaoDevePermitirNumeroDuplicado()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("Nicolas Ferreira", 22);

        var resultado = eleicao.CadastrarCandidato("Lucas Pavanato", 22);

        Assert.False(resultado);
        Assert.Single(eleicao.Candidatos);
    }

    [Fact]
    public void Candidato_DeveRejeitarNomeVazio()
    {
        Assert.Throws<ArgumentException>(
            () => new Candidato("", 10));
    }

    [Fact]
    public void Candidato_DeveRejeitarNumeroNaoPositivo()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Candidato("Flavio Bolsonaro", 0));
    }
}
