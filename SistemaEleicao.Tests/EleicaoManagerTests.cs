using SistemaEleicao.Console;
using Xunit;

namespace SistemaEleicao.Tests;

public class EleicaoManagerTests
{
    [Fact]
    public void RegistrarVoto_DeveAceitarVotoEmCandidatoExistente()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Flavio Bolsonaro", 17);
        eleicao.RegistrarEleitor("111");

        var resultado = eleicao.RegistrarVoto(
            "111", TipoVoto.Candidato, 17);

        Assert.True(resultado);
        Assert.Equal(1, eleicao.ObterVotosCandidato(17));
    }

    [Fact]
    public void RegistrarVoto_NaoDevePermitirSegundoVoto()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Flavio Bolsonaro", 17);
        eleicao.RegistrarEleitor("111");

        eleicao.RegistrarVoto("111", TipoVoto.Candidato, 17);

        var resultado = eleicao.RegistrarVoto(
            "111", TipoVoto.Branco);

        Assert.False(resultado);
        Assert.Equal(1, eleicao.TotalVotos);
        Assert.Equal(0, eleicao.VotosBrancos);
    }

    [Fact]
    public void RegistrarVoto_DeveRejeitarEleitorNaoCadastrado()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Flavio Bolsonaro", 17);

        var resultado = eleicao.RegistrarVoto(
            "999", TipoVoto.Candidato, 17);

        Assert.False(resultado);
        Assert.Equal(0, eleicao.TotalVotos);
    }

    [Fact]
    public void RegistrarVoto_DeveContabilizarCandidatoInexistenteComoNulo()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Flavio Bolsonaro", 17);
        eleicao.RegistrarEleitor("111");

        var resultado = eleicao.RegistrarVoto(
            "111", TipoVoto.Candidato, 99);

        Assert.True(resultado);
        Assert.Equal(1, eleicao.VotosNulos);
        Assert.Equal(0, eleicao.ObterVotosCandidato(17));
    }

    [Fact]
    public void RegistrarVoto_DeveContabilizarVotoBranco()
    {
        var eleicao = new EleicaoManager();
        eleicao.RegistrarEleitor("111");

        eleicao.RegistrarVoto("111", TipoVoto.Branco);

        Assert.Equal(1, eleicao.VotosBrancos);
        Assert.Equal(0, eleicao.VotosNulos);
    }

    [Fact]
    public void RegistrarVoto_DeveContabilizarVotoNulo()
    {
        var eleicao = new EleicaoManager();
        eleicao.RegistrarEleitor("111");

        eleicao.RegistrarVoto("111", TipoVoto.Nulo);

        Assert.Equal(1, eleicao.VotosNulos);
    }

    [Fact]
    public void Apuracao_DeveIdentificarCandidatoComMaisVotos()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Flavio Bolsonaro", 17);
        eleicao.CadastrarCandidato("Nicolas Ferreira", 22);

        eleicao.RegistrarEleitor("111");
        eleicao.RegistrarEleitor("222");
        eleicao.RegistrarEleitor("333");

        eleicao.RegistrarVoto("111", TipoVoto.Candidato, 17);
        eleicao.RegistrarVoto("222", TipoVoto.Candidato, 17);
        eleicao.RegistrarVoto("333", TipoVoto.Candidato, 22);

        Assert.Contains("Flavio Bolsonaro", eleicao.ObterVencedor());
        Assert.Equal(2, eleicao.ObterVotosCandidato(17));
        Assert.Equal(1, eleicao.ObterVotosCandidato(22));
    }

    [Fact]
    public void Apuracao_DeveIdentificarEmpate()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Flavio Bolsonaro", 17);
        eleicao.CadastrarCandidato("Nicolas Ferreira", 22);

        eleicao.RegistrarEleitor("111");
        eleicao.RegistrarEleitor("222");

        eleicao.RegistrarVoto("111", TipoVoto.Candidato, 17);
        eleicao.RegistrarVoto("222", TipoVoto.Candidato, 22);

        Assert.Equal("Empate entre candidatos.", eleicao.ObterVencedor());
    }

    [Fact]
    public void GerarRelatorio_DeveApresentarResultadoDaEleicao()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Flavio Bolsonaro", 17);
        eleicao.RegistrarEleitor("111");
        eleicao.RegistrarVoto("111", TipoVoto.Branco);

        var relatorio = eleicao.GerarRelatorio();

        Assert.Contains("Flavio Bolsonaro", relatorio);
        Assert.Contains("Votos brancos: 1", relatorio);
        Assert.Contains("Total de votos registrados: 1", relatorio);
    }
}