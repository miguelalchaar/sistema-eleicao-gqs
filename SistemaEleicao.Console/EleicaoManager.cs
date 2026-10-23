
namespace SistemaEleicao.Console;

public class EleicaoManager
{
    private readonly Dictionary<int, Candidato> _candidatos = new();
    private readonly Dictionary<string, Eleitor> _eleitores = new(
        StringComparer.OrdinalIgnoreCase);

    private readonly Dictionary<string, (TipoVoto Tipo, int? NumeroCandidato)>
        _votos = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyCollection<Candidato> Candidatos =>
        _candidatos.Values.ToList().AsReadOnly();

    public IReadOnlyCollection<Eleitor> Eleitores =>
        _eleitores.Values.ToList().AsReadOnly();

    public int TotalVotos => _votos.Count;

    public int VotosBrancos =>
        _votos.Values.Count(v => v.Tipo == TipoVoto.Branco);

    public int VotosNulos =>
        _votos.Values.Count(v =>
            v.Tipo == TipoVoto.Nulo ||
            (v.Tipo == TipoVoto.Candidato &&
             (!v.NumeroCandidato.HasValue ||
              !_candidatos.ContainsKey(v.NumeroCandidato.Value))));

    public bool CadastrarCandidato(string nome, int numero)
    {
        if (_candidatos.ContainsKey(numero))
            return false;

        var candidato = new Candidato(nome, numero);
        _candidatos.Add(numero, candidato);

        return true;
    }

    public bool RegistrarEleitor(string documento)
    {
        var eleitor = new Eleitor(documento);

        if (_eleitores.ContainsKey(eleitor.Documento))
            return false;

        _eleitores.Add(eleitor.Documento, eleitor);
        return true;
    }

    public bool RegistrarVoto(
        string documento,
        TipoVoto tipoVoto,
        int? numeroCandidato = null)
    {
        if (string.IsNullOrWhiteSpace(documento))
            return false;

        documento = documento.Trim();

        if (!_eleitores.ContainsKey(documento))
            return false;

        if (_votos.ContainsKey(documento))
            return false;

        if (tipoVoto == TipoVoto.Branco)
        {
            _votos.Add(documento, (TipoVoto.Branco, null));
            return true;
        }

        if (tipoVoto == TipoVoto.Nulo)
        {
            _votos.Add(documento, (TipoVoto.Nulo, null));
            return true;
        }

        if (tipoVoto == TipoVoto.Candidato)
        {
            // um número que não existe gera um voto nulo.
            _votos.Add(documento, (TipoVoto.Candidato, numeroCandidato));
            return true;
        }

        return false;
    }

    public int ObterVotosCandidato(int numeroCandidato)
    {
        if (!_candidatos.ContainsKey(numeroCandidato))
            return 0;

        return _votos.Values.Count(v =>
            v.Tipo == TipoVoto.Candidato &&
            v.NumeroCandidato == numeroCandidato &&
            _candidatos.ContainsKey(numeroCandidato));
    }

    public string ObterVencedor()
    {
        if (_candidatos.Count == 0)
            return "Não há candidatos cadastrados.";

        var resultados = _candidatos.Values
            .Select(c => new
            {
                Candidato = c,
                Votos = ObterVotosCandidato(c.Numero)
            })
            .OrderByDescending(r => r.Votos)
            .ToList();

        if (resultados[0].Votos == 0)
            return "Não houve votos válidos para candidatos.";

        if (resultados.Count > 1 &&
            resultados[0].Votos == resultados[1].Votos)
            return "Empate entre candidatos.";

        return $"{resultados[0].Candidato.Nome} " +
               $"(número {resultados[0].Candidato.Numero})";
    }

    public string GerarRelatorio()
    {
        var linhas = new List<string>
        {
            "> APURAÇÃO DA ELEIÇÃO <",
            $"Total de votos registrados: {TotalVotos}",
            "",
            "Votos por candidato:"
        };

        foreach (var candidato in _candidatos.Values.OrderBy(c => c.Numero))
        {
            linhas.Add(
                $"{candidato.Numero} - {candidato.Nome}: " +
                $"{ObterVotosCandidato(candidato.Numero)} voto(s)");
        }

        linhas.Add("");
        linhas.Add($"Votos brancos: {VotosBrancos}");
        linhas.Add($"Votos nulos: {VotosNulos}");
        linhas.Add($"Resultado: {ObterVencedor()}");

        return string.Join(Environment.NewLine, linhas);
    }
}
