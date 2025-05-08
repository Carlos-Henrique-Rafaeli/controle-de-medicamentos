using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class RepositorioFornecedor : RepositorioBase<Fornecedor>, IRepositorioFornecedor
{
    public RepositorioFornecedor(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Fornecedor> ObterRegistros()
    {
        return contexto.Fornecedor;
    }

    public bool VerificarCNPJ(string cnpj, int id = -1)
    {
        List<Fornecedor> fornecedor = SelecionarRegistros();

        foreach (var funcionario in fornecedor)
        {
            if (funcionario == null) continue;

            if (funcionario.Id == id) continue;

            if (funcionario.Cnpj == cnpj) return false;
        }
        return true;
    }
}
