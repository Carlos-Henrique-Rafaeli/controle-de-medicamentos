using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class TelaFornecedor : TelaBase<Fornecedor> , ITelaCrud
{
    IRepositorioFornecedor repositorioFornecedor;

    public TelaFornecedor(IRepositorioFornecedor repositorio) : base("fornecedor", repositorio)
    {
        repositorioFornecedor = repositorio;
    }

    public override bool ValidarInserirEditar(Fornecedor registroEditado, int idRegistro = -1)
    {
        bool validacao = repositorioFornecedor.VerificarCNPJ( registroEditado.Cnpj, idRegistro);

        if (!validacao)
        {
            Notificador.ExibirMensagem("Já existe um Fornecedor com este CNPJ", ConsoleColor.Red);
            return false;
        }

        return true;
    }

    public override Fornecedor ObterDados(bool validacaoExtra)
    {
        Console.Write("Informe o Nome : ");
        string nome = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        Console.Write("Informe o Telefone : ");
        string telefone = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        Console.Write("Informe o Cnpj : ");
        string cnpj = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        Fornecedor fornecedor = new Fornecedor(nome, telefone, cnpj);

        return fornecedor;
    }
    public override void ExibirTabela()
    {
        Console.WriteLine("{0,-10} | {1,-30} | {2,-20} | {3,-20} |" ,
            "id","nome","telefone" ,"Cnpj"
            );
    }

    public override void ExibirConteudoTabela(Fornecedor fornecedor)
    {
        Console.WriteLine("{0,-10} | {1,-30} | {2,-20} | {3,-20} |"
          , fornecedor.Id, fornecedor.Nome ,fornecedor.Telefone ,fornecedor.Cnpj );
    }
}
