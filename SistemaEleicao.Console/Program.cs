
using SistemaEleicao.Console;

internal class Program
{
    private static readonly EleicaoManager Eleicao = new();

    private static void Main()
    {
        var opcao = "";

        do
        {
            System.Console.WriteLine();
            System.Console.WriteLine("> SISTEMA DE ELEIÇÃO <");
            System.Console.WriteLine("1 - Cadastrar candidato");
            System.Console.WriteLine("2 - Cadastrar eleitor");
            System.Console.WriteLine("3 - Registrar voto");
            System.Console.WriteLine("4 - Consultar candidatos");
            System.Console.WriteLine("5 - Apurar eleição");
            System.Console.WriteLine("0 - Sair");
            System.Console.Write("Escolha uma opção: ");

            opcao = System.Console.ReadLine()?.Trim() ?? "";

            switch (opcao)
            {
                case "1":
                    CadastrarCandidato();
                    break;

                case "2":
                    CadastrarEleitor();
                    break;

                case "3":
                    RegistrarVoto();
                    break;

                case "4":
                    ConsultarCandidatos();
                    break;

                case "5":
                    System.Console.WriteLine(Eleicao.GerarRelatorio());
                    break;

                case "0":
                    System.Console.WriteLine("Sistema encerrado.");
                    break;

                default:
                    System.Console.WriteLine("Opção inválida.");
                    break;
            }
        }
        while (opcao != "0");
    }

    private static void CadastrarCandidato()
    {
        System.Console.Write("Nome do candidato: ");
        var nome = System.Console.ReadLine() ?? "";

        System.Console.Write("Número do candidato: ");
        var entradaNumero = System.Console.ReadLine();

        if (!int.TryParse(entradaNumero, out var numero))
        {
            System.Console.WriteLine("Número inválido.");
            return;
        }

        try
        {
            var cadastrado = Eleicao.CadastrarCandidato(nome, numero);

            System.Console.WriteLine(
                cadastrado
                    ? "Candidato cadastrado com sucesso."
                    : "Já existe um candidato com esse número.");
        }
        catch (ArgumentException ex)
        {
            System.Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    private static void CadastrarEleitor()
    {
        System.Console.Write("Documento do eleitor: ");
        var documento = System.Console.ReadLine() ?? "";

        try
        {
            var cadastrado = Eleicao.RegistrarEleitor(documento);

            System.Console.WriteLine(
                cadastrado
                    ? "Eleitor cadastrado com sucesso."
                    : "Esse eleitor já está cadastrado.");
        }
        catch (ArgumentException ex)
        {
            System.Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    private static void RegistrarVoto()
    {
        System.Console.Write("Documento do eleitor: ");
        var documento = System.Console.ReadLine() ?? "";

        System.Console.WriteLine("Escolha seu voto:");
        System.Console.WriteLine("- Digite o número do candidato");
        System.Console.WriteLine("- Digite B para voto branco");
        System.Console.WriteLine("- Digite N para voto nulo");
        System.Console.Write("Voto: ");

        var escolha = (System.Console.ReadLine() ?? "")
            .Trim();

        TipoVoto tipo;
        int? numeroCandidato = null;

        if (escolha.Equals("B", StringComparison.OrdinalIgnoreCase))
        {
            tipo = TipoVoto.Branco;
        }
        else if (escolha.Equals("N", StringComparison.OrdinalIgnoreCase))
        {
            tipo = TipoVoto.Nulo;
        }
        else if (int.TryParse(escolha, out var numero))
        {
            tipo = TipoVoto.Candidato;
            numeroCandidato = numero;
        }
        else
        {
            System.Console.WriteLine(
                "Entrada inválida. Informe um número, B ou N.");
            return;
        }

        var registrado = Eleicao.RegistrarVoto(
            documento, tipo, numeroCandidato);

        System.Console.WriteLine(
            registrado
                ? "Voto registrado com sucesso."
                : "Não foi possível registrar o voto. " +
                  "Verifique se o eleitor está cadastrado " +
                  "e se já votou.");
    }

    private static void ConsultarCandidatos()
    {
        var candidatos = Eleicao.Candidatos
            .OrderBy(c => c.Numero)
            .ToList();

        if (candidatos.Count == 0)
        {
            System.Console.WriteLine("Nenhum candidato cadastrado.");
            return;
        }

        System.Console.WriteLine("> CANDIDATOS <");

        foreach (var candidato in candidatos)
        {
            System.Console.WriteLine(
                $"{candidato.Numero} - {candidato.Nome}");
        }
    }
}
