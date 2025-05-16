using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using System.Security.Principal;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public abstract class FormularioMedicamentoViewModel
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int Estoque { get; set; }
    public int FornecedorId { get; set; }

    public List<SelecionarFornecedorViewModel> FornecedoresDisponiveis { get; set; }

    protected FormularioMedicamentoViewModel()
    {
        FornecedoresDisponiveis = new List<SelecionarFornecedorViewModel>();
    }
}

public class SelecionarFornecedorViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public SelecionarFornecedorViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class CadastrarMedicamentoViewModel : FormularioMedicamentoViewModel
{
    public CadastrarMedicamentoViewModel() { }

    public CadastrarMedicamentoViewModel(List<Fornecedor> fornecedores) : this()
    {
        foreach (var fornecedor in fornecedores)
        {
            var selecionarVM = new SelecionarFornecedorViewModel(fornecedor.Id, fornecedor.Nome);

            FornecedoresDisponiveis.Add(selecionarVM);
        }
    }
}

public class EditarMedicamentoViewModel : FormularioMedicamentoViewModel
{
    public int Id { get; set; }

    public EditarMedicamentoViewModel() { }

    public EditarMedicamentoViewModel(
        int id, 
        string nome, 
        string descricao, 
        int estoque, 
        int fornecedorId, 
        List<Fornecedor> fornecedores
        ) : this()
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Estoque = estoque;
        FornecedorId = fornecedorId;

        foreach (var fornecedor in fornecedores)
        {
            var selecionarVM = new SelecionarFornecedorViewModel(fornecedor.Id, fornecedor.Nome);

            FornecedoresDisponiveis.Add(selecionarVM);
        }
    }
}

public class ExcluirMedicamentoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirMedicamentoViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarMedicamentosViewModel
{
    public List<DetalhesMedicamentoViewModel> Registros { get; set; }

    public VisualizarMedicamentosViewModel(List<Medicamento> medicamentos)
    {
        Registros = new List<DetalhesMedicamentoViewModel>();

        foreach (var medicamento in medicamentos)
        {
            DetalhesMedicamentoViewModel detalhesVM = medicamento.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesMedicamentoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int Estoque { get; set; }
    public string NomeFornecedor { get; set; }

    public DetalhesMedicamentoViewModel(
        int id, 
        string nome, 
        string descricao, 
        int estoque, 
        string nomeFornecedor)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Estoque = estoque;
        NomeFornecedor = nomeFornecedor;
    }

    public override string ToString()
    {
        return $"Id: {Id} | Nome: {Nome} | Descrição: {Descricao} | Estoque: {Estoque} | Fornecedor: {NomeFornecedor}";
    }
}