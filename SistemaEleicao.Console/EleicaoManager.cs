
namespace SistemaEleicao.Console;

public class EleicaoManager
{
    private readonly Dictionary<int, Candidato> _candidatos = new();

    public IReadOnlyCollection<Candidato> Candidatos =>
        _candidatos.Values.ToList().AsReadOnly();

    public bool CadastrarCandidato(string nome, int numero)
    {
        if (_candidatos.ContainsKey(numero))
            return false;

        var candidato = new Candidato(nome, numero);
        _candidatos.Add(numero, candidato);

        return true;
    }
}
