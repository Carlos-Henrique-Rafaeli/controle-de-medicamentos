using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public abstract class FormularioEntradaViewModel
{
    public DateTime Data { get; set; }
    public int MedicamentoId { get; set; }
    public List<SelecionarMedicamentoViewModel> MedicamentosDisponiveis { get; set; }
    public int FuncionarioId { get; set; }
    public List<SelecionarFuncionarioViewModel> FuncionariosDisponiveis { get; set; }
    public int Quantidade { get; set; }

    protected FormularioEntradaViewModel()
    {
        MedicamentosDisponiveis = new List<SelecionarMedicamentoViewModel>();
        FuncionariosDisponiveis = new List<SelecionarFuncionarioViewModel>();
    }
}

public class SelecionarMedicamentoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public SelecionarMedicamentoViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class SelecionarFuncionarioViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public SelecionarFuncionarioViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class CadastrarEntradaViewModel : FormularioEntradaViewModel
{
    public CadastrarEntradaViewModel() { }

    public CadastrarEntradaViewModel(
        List<Medicamento> medicamentos, 
        List<Funcionario> funcionarios
    ) : this()
    {
        foreach (var medicamento in medicamentos)
        {
            var selecionarVM = new SelecionarMedicamentoViewModel(medicamento.Id, medicamento.Nome);

            MedicamentosDisponiveis.Add(selecionarVM);
        }
        foreach (var funcionario in funcionarios)
        {
            var selecionarVM = new SelecionarFuncionarioViewModel(funcionario.Id, funcionario.Nome);

            FuncionariosDisponiveis.Add(selecionarVM);
        }
    }
}

public class ExcluirEntradaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirEntradaViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarEntradasViewModel
{
    public List<DetalhesEntradaViewModel> Registros { get; set; }

    public VisualizarEntradasViewModel(List<Entrada> entradas)
    {
        Registros = new List<DetalhesEntradaViewModel>();

        foreach (var entrada in entradas)
        {
            var detalhesVM = entrada.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesEntradaViewModel
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string NomeMedicamento { get; set; }
    public string NomeFuncionario { get; set; }
    public int Quantidade { get; set; }

    public DetalhesEntradaViewModel(
        int id, 
        DateTime data, 
        string nomeMedicamento, 
        string nomeFuncionario, 
        int quantidade
    )
    {
        Id = id;
        Data = data;
        NomeMedicamento = nomeMedicamento;
        NomeFuncionario = nomeFuncionario;
        Quantidade = quantidade;
    }

    public override string ToString()
    {
        return $"Id: {Id} | Data: {Data:d} | Funcionário: {NomeFuncionario} | Medicamento: {NomeFuncionario} Quantidade: {Quantidade}";
    }
}