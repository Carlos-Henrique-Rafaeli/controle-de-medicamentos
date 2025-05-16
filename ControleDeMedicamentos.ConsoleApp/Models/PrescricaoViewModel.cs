using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public abstract class FormularioPrescricaoViewModel
{
    public string CRM { get; set; }
    public DateTime Data { get; set; }
    public int MedicamentoId { get; set; }
    public List<SelecionarMedicamentoViewModel> MedicamentosDisponiveis { get; set; }
    public int Dosagem { get; set; }
    public int Periodo { get; set; }

    protected FormularioPrescricaoViewModel()
    {
        MedicamentosDisponiveis = new List<SelecionarMedicamentoViewModel>();
    }
}

public class CadastrarPrescricaoViewModel : FormularioPrescricaoViewModel
{
    public CadastrarPrescricaoViewModel() { }

    public CadastrarPrescricaoViewModel(
        List<Medicamento> medicamentos
    ) : this()
    {
        foreach (var medicamento in medicamentos)
        {
            var selecionarVM = new SelecionarMedicamentoViewModel(medicamento.Id, medicamento.Nome);

            MedicamentosDisponiveis.Add(selecionarVM);
        }
    }
}

public class ExcluirPrescricaoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirPrescricaoViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarPrescricaoViewModel
{
    public List<DetalhesPrescricaoViewModel> Registros { get; set; }

    public VisualizarPrescricaoViewModel(List<Prescricao> prescricaos)
    {
        Registros = new List<DetalhesPrescricaoViewModel>();

        foreach (var prescricao in prescricaos)
        {
            var detalhesVM = prescricao.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesPrescricaoViewModel
{
    public int Id { get; set; }
    public string CRM { get; set; }
    public DateTime Data { get; set; }
    public string NomeMedicamento { get; set; }
    public int Dosagem { get; set; }
    public int Periodo { get; set; }
    public DetalhesPrescricaoViewModel(
        int id,
        string crm, 
        DateTime data, 
        string nomeMedicamento, 
        int dosagem,
        int periodo)
    {
        Id = id;
        CRM = crm;
        Data = data;
        NomeMedicamento = nomeMedicamento;
        Dosagem = dosagem;
        Periodo = periodo;
    }


    public override string ToString()
    {
        return $"Id: {Id} | CRM: {CRM} | Data: {Data:d} | Medicamento: {NomeMedicamento} | Dosagem: {Dosagem} | Período: {Periodo}";
    }
}