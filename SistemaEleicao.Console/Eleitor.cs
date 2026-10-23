
namespace SistemaEleicao.Console;

public class Eleitor
{
    public string Documento { get; }

    public Eleitor(string documento)
    {
        if (string.IsNullOrWhiteSpace(documento))
            throw new ArgumentException(
                "O documento do eleitor é obrigatório.",
                nameof(documento));

        Documento = documento.Trim();
    }
}
