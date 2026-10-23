# SistemaEleicao

![C#](https://img.shields.io/badge/C%23-.NET%2010-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-Testes%20Unit%C3%A1rios-5E2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![Git](https://img.shields.io/badge/Git-Versionamento-orange?style=for-the-badge&logo=git&logoColor=white)
![GitHub](https://img.shields.io/badge/GitHub-Reposit%C3%B3rio-black?style=for-the-badge&logo=github&logoColor=white)
![License](https://img.shields.io/badge/Licen%C3%A7a-MIT-blue?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Concluída-green?style=for-the-badge)

Projeto desenvolvido para a disciplina de **Garantia da Qualidade de Software**, com foco na aplicação prática de **testes unitários com xUnit**, utilizando uma solução **.NET 10** e ferramentas de versionamento de código.

A atividade consiste no desenvolvimento de um **sistema de eleição simulado**, com funcionalidades para cadastro de candidatos e eleitores, registro de votos e apuração dos resultados.

Além da implementação das funcionalidades, o projeto tem como objetivo compreender a importância dos testes automatizados na validação das regras de negócio e utilizar o Git com o padrão **Conventional Commits** para registrar a evolução do software.

## ▸ Conceito da atividade

A atividade consiste na criação de um repositório chamado `SistemaEleicao` e na implementação de uma solução composta por dois projetos:

- `SistemaEleicao.Console`: aplicação de console que contém as entidades, as regras de negócio e a interface de interação.
- `SistemaEleicao.Tests`: projeto de testes unitários responsável por verificar o comportamento das funcionalidades implementadas.

O projeto de aplicação concentra as regras relacionadas à eleição, enquanto o projeto de testes valida essas regras de forma automatizada.

Essa separação permite testar a lógica do sistema independentemente da interação pelo terminal, facilitando a identificação de erros e a manutenção do código.

## ▸ Camadas da solução

### `SistemaEleicao.Console` — Código de Produção

O projeto `SistemaEleicao.Console` contém a aplicação principal e as regras de negócio do sistema eleitoral.

Neste projeto são implementados:

- A classe `Candidato`, responsável por representar os candidatos.
- A classe `Eleitor`, responsável por representar os eleitores.
- A classe `EleicaoManager`, responsável pelo gerenciamento da eleição.
- O enum `TipoVoto`, que representa as categorias de voto.
- O arquivo `Program.cs`, responsável pelo menu interativo.

Essa camada implementa as funcionalidades de cadastro, validação, votação e apuração dos resultados.

### `SistemaEleicao.Tests` — Testes Unitários

O projeto `SistemaEleicao.Tests` representa a camada de validação automatizada.

Neste projeto são implementados:

- Testes com o atributo `[Fact]`.
- Verificações com `Assert.True`, `Assert.False`, `Assert.Equal`, `Assert.Single`, `Assert.Contains` e `Assert.Throws`.
- Testes para impedir o cadastro de candidatos com números duplicados.
- Testes para impedir o cadastro duplicado de eleitores.
- Testes para verificar a prevenção de votos duplicados.
- Testes para validar votos brancos, nulos e em candidatos inexistentes.
- Testes para conferir a apuração e a identificação do resultado da eleição.

Os testes permitem verificar se o comportamento do sistema corresponde às regras estabelecidas, inclusive após futuras alterações no código.

## ▸ Funcionalidades do sistema eleitoral

A aplicação foi planejada para disponibilizar as seguintes funcionalidades:

| Funcionalidade                | Descrição                                                            |
| ----------------------------- | -------------------------------------------------------------------- |
| Cadastro de candidatos        | Registra candidatos com nome e número.                               |
| Validação de candidatos       | Impede o cadastro de dois candidatos com o mesmo número.             |
| Cadastro de eleitores         | Registra eleitores por meio de um documento simulado.                |
| Validação de eleitores        | Impede o cadastro duplicado de documentos.                           |
| Registro de votos             | Permite votar em candidatos, em branco ou nulo.                      |
| Prevenção de votos duplicados | Impede que um mesmo eleitor vote mais de uma vez.                    |
| Votos inválidos               | Contabiliza como nulo o voto direcionado a um candidato inexistente. |
| Apuração                      | Apresenta os totais de votos e o resultado da eleição.               |

## ▸ Regras de negócio

A classe `EleicaoManager` centraliza as principais regras relacionadas à eleição.

| Método                                                | Retorno  | Regra                                                                               |
| ----------------------------------------------------- | -------- | ----------------------------------------------------------------------------------- |
| `CadastrarCandidato(nome, numero)`                    | `bool`   | Cadastra um candidato quando o número ainda não está registrado.                    |
| `RegistrarEleitor(documento)`                         | `bool`   | Cadastra um eleitor quando o documento ainda não existe.                            |
| `RegistrarVoto(documento, tipoVoto, numeroCandidato)` | `bool`   | Registra o voto quando o eleitor está cadastrado e ainda não votou.                 |
| `ObterVotosCandidato(numeroCandidato)`                | `int`    | Retorna a quantidade de votos válidos atribuídos ao candidato.                      |
| `ObterVencedor()`                                     | `string` | Identifica o candidato com mais votos válidos, empate ou ausência de votos válidos. |
| `GerarRelatorio()`                                    | `string` | Gera o relatório de apuração com os totais da eleição.                              |

### Exemplo de funcionamento

Considere uma eleição com dois candidatos e três eleitores:

```text
Candidato 10 - Ana
Candidato 20 - Bruno

Eleitor 111 vota no candidato 10.
Eleitor 222 vota no candidato 10.
Eleitor 333 vota em branco.
```

O relatório esperado será semelhante a:

```text
> APURAÇÃO DA ELEIÇÃO <
Total de votos registrados: 3

Votos por candidato:
10 - Vargas: 2 voto(s)
20 - Jucelino: 0 voto(s)

Votos brancos: 1
Votos nulos: 0
Resultado: Vargas (número 10)
```

O exemplo é ilustrativo e representa o resultado esperado para os dados apresentados.

## ▸ Testes unitários realizados

Os testes estão organizados em classes de acordo com as funcionalidades verificadas.

| Classe de teste       | Cenários validados                                                                                        | Objetivo                                     |
| --------------------- | --------------------------------------------------------------------------------------------------------- | -------------------------------------------- |
| `CandidatoTests`      | Cadastro válido, número duplicado, nome vazio e número inválido                                           | Validar as regras de cadastro de candidatos. |
| `EleitorTests`        | Cadastro válido, documento duplicado e documento vazio                                                    | Validar as regras de cadastro de eleitores.  |
| `EleicaoManagerTests` | Voto válido, voto duplicado, eleitor não cadastrado, voto nulo, voto branco, vencedor, empate e relatório | Validar as regras de votação e apuração.     |

### Testes de candidatos

Verificam se o cadastro aceita números ainda não utilizados e rejeita números duplicados. Também validam os dados obrigatórios da entidade.

### Testes de eleitores

Verificam se um documento pode ser cadastrado apenas uma vez e se documentos vazios são rejeitados.

### Testes de votação

Verificam se eleitores cadastrados conseguem votar, se um segundo voto é bloqueado e se votos em candidatos inexistentes são contabilizados como nulos.

Também verificam os votos brancos, os votos nulos e a identificação do candidato vencedor ou de um empate.

### Execução dos testes

Para executar todos os testes automatizados, utilize:

```bash
dotnet test
```

O resultado esperado é que todos os testes implementados sejam executados com sucesso, sem falhas.

## ▸ Versionamento e qualidade de software

O projeto utiliza o Git para acompanhar as alterações e o padrão **Conventional Commits** para padronizar as mensagens de commit.

| Conceito                 | Aplicação no projeto                                       |
| ------------------------ | ---------------------------------------------------------- |
| **Solução (`.sln`)**     | Agrupa os projetos de aplicação e de testes.               |
| **Teste unitário**       | Valida individualmente as regras de negócio.               |
| **xUnit**                | Framework utilizado para implementar e executar os testes. |
| **`[Fact]`**             | Identifica um método como teste unitário.                  |
| **`Assert`**             | Compara o comportamento obtido com o esperado.             |
| **Repositório Git**      | Mantém o histórico de alterações do código.                |
| **Conventional Commits** | Padroniza as mensagens de commit.                          |
| **`git add`**            | Seleciona as alterações que serão registradas.             |
| **`git commit`**         | Registra um conjunto de alterações no histórico.           |
| **`git push`**           | Envia os commits para o repositório remoto.                |
| **`.gitignore`**         | Evita versionar arquivos gerados, como `bin/` e `obj/`.    |
| **README.md**            | Documenta o projeto e as instruções de execução.           |

### Padrão de commits

Os commits seguem a estrutura:

```text
tipo(escopo): descrição da alteração
```

Exemplos utilizados no desenvolvimento:

```bash
git commit -m "chore: initial project structure with console and xunit apps"

git commit -m "test(candidato): add unit tests for candidate registration rules"

git commit -m "feat(candidato): implement candidate entity and validation logic"

git commit -m "test(votacao): add unit tests for duplicate voting prevention and invalid votes"

git commit -m "feat(votacao): implement voting logic and voter verification"

git commit -m "feat(console): implement interactive menu for election management"

git commit -m "refactor(core): improve code readability and organize namespaces"

git commit -m "docs: add comprehensive instructions in README.md"
```

Os commits devem ser realizados após as respectivas etapas, com as alterações correspondentes já implementadas e verificadas.

## ▸ Configuração via .NET CLI

Os comandos abaixo demonstram como criar a solução e os projetos utilizando a linha de comando.

```bash
# 1. Cria a solução
dotnet new sln -n SistemaEleicao

# 2. Cria o projeto de console
dotnet new console -n SistemaEleicao.Console -f net10.0

# 3. Cria o projeto de testes com xUnit
dotnet new xunit -n SistemaEleicao.Tests -f net10.0

# 4. Adiciona os projetos à solução
dotnet sln add SistemaEleicao.Console/SistemaEleicao.Console.csproj
dotnet sln add SistemaEleicao.Tests/SistemaEleicao.Tests.csproj

# 5. Adiciona a referência da aplicação no projeto de testes
dotnet add SistemaEleicao.Tests/SistemaEleicao.Tests.csproj reference SistemaEleicao.Console/SistemaEleicao.Console.csproj
```

**Observação:** os comandos de criação são destinados à configuração inicial. Não é necessário executá-los novamente se a solução e os projetos já tiverem sido criados.

## ▸ Como executar

**Pré-requisito:** possuir o [SDK do .NET 10](https://dotnet.microsoft.com/download) instalado na máquina, caso os projetos tenham sido configurados para `net10.0`.

### 1. Clonar o repositório

```bash
git clone https://github.com/miguelalchaar/sistema-eleicao-gqs
```

### 2. Acessar a pasta do projeto

```bash
cd SistemaEleicao
```

### 3. Restaurar as dependências

```bash
dotnet restore
```

### 4. Executar os testes unitários

```bash
dotnet test
```

### 5. Executar a aplicação

```bash
dotnet run --project SistemaEleicao.Console/SistemaEleicao.Console.csproj
```

### 6. Conferir o histórico de commits

```bash
git log --oneline
```

Para verificar se existem alterações locais pendentes:

```bash
git status
```

## ▸ Licença

Este projeto está destinado a fins acadêmicos e educacionais.

Caso seja adotada a **Licença MIT**, inclua um arquivo `LICENSE` na raiz do repositório com o texto oficial da licença. A badge de licença deverá refletir a licença efetivamente incluída no projeto.

## ▸ Autor

**Miguel Alchaar**

Desenvolvido como atividade acadêmica da disciplina de **Garantia da Qualidade de Software**, com foco em testes unitários, regras de negócio e versionamento de código.
