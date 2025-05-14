using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public abstract class FormularioFuncionarioViewModel
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CPF { get; set; }
}

public class CadastrarFuncionarioViewModel : FormularioFuncionarioViewModel
{
    public CadastrarFuncionarioViewModel() { }

    public CadastrarFuncionarioViewModel(string nome, string telefone, string cpf) : this()
    {
        Nome = nome;
        Telefone = telefone;
        CPF = cpf;
    }
}

public class EditarFuncionarioViewModel : FormularioFuncionarioViewModel
{
    public int Id { get; set; }

    public EditarFuncionarioViewModel() { }

    public EditarFuncionarioViewModel(int id, string nome, string telefone, string cpf)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CPF = cpf;
    }
}

public class ExcluirFuncionarioViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirFuncionarioViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarFuncionariosViewModel
{
    public List<DetalhesFuncionarioViewModel> Registros { get; } = new List<DetalhesFuncionarioViewModel>();

    public VisualizarFuncionariosViewModel(List<Funcionario> funcionarios)
    {
        foreach (Funcionario f in funcionarios)
        {
            DetalhesFuncionarioViewModel detalhesVM = f.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesFuncionarioViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CPF { get; set; }

    public DetalhesFuncionarioViewModel(int id, string nome, string telefone, string cpf)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CPF = cpf;
    }
    public override string ToString()
    {
        return $"Id: {Id} | Nome: {Nome} | Telefone: {Telefone} | CPF: {CPF}";
    }
}