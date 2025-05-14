using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public abstract class FormularioFornecedorViewModel
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CNPJ { get; set; }
}

public class CadastrarFornecedorViewModel : FormularioFornecedorViewModel
{
    public CadastrarFornecedorViewModel() { }

    public CadastrarFornecedorViewModel(string nome, string telefone, string cnpj) : this()
    {
        Nome = nome;
        Telefone = telefone;
        CNPJ = cnpj;
    }
}

public class EditarFornecedorViewModel : FormularioFornecedorViewModel
{
    public int Id { get; set; }

    public EditarFornecedorViewModel() { }

    public EditarFornecedorViewModel(int id, string nome, string telefone, string cnpj) : this()
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CNPJ = cnpj;
    }
}

public class ExcluirFornecedorViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirFornecedorViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarFornecedoresViewModel
{
    public List<DetalhesFornecedorViewModel> Registros { get; } = new List<DetalhesFornecedorViewModel>();

    public VisualizarFornecedoresViewModel(List<Fornecedor> fornecedores)
    {
        foreach (Fornecedor f in fornecedores)
        {
            DetalhesFornecedorViewModel detalhesVM = f.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesFornecedorViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CNPJ { get; set; }

    public DetalhesFornecedorViewModel(int id, string nome, string telefone, string cnpj)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CNPJ = cnpj;
    }

    public override string ToString()
    {
        return $"Id: {Id} | Nome: {Nome} | Telefone: {Telefone} | CNPJ: {CNPJ}";
    }
}