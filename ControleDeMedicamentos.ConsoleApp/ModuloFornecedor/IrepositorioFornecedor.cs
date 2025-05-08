using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public interface IRepositorioFornecedor : IRepositorio<Fornecedor>
{
    public bool VerificarCNPJ(string cnpj, int id = -1);
}
