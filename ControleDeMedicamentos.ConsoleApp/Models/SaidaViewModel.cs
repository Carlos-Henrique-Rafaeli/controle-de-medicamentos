using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoDeSaida;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public abstract class FormularioSaidaViewModel
{
    public DateTime Data { get; set; }
    public int PacienteId { get; set; }
    public List<SelecionarPacienteViewModel> PacientesDisponiveis { get; set; }
    public int PrescricaoId { get; set; }
    public List<SelecionarPrescricaoViewModel> PrescricoesDisponiveis { get; set; }
    public int MedicamentoId { get; set; }
    public List<SelecionarMedicamentoViewModel> MedicamentosDisponiveis { get; set; }

    protected FormularioSaidaViewModel()
    {
        PacientesDisponiveis = new List<SelecionarPacienteViewModel>();
        PrescricoesDisponiveis = new List<SelecionarPrescricaoViewModel>();
        MedicamentosDisponiveis = new List<SelecionarMedicamentoViewModel>();
    }
}

public class SelecionarPacienteViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public SelecionarPacienteViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class SelecionarPrescricaoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public SelecionarPrescricaoViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}


public class CadastrarSaidaViewModel : FormularioSaidaViewModel
{
    public CadastrarSaidaViewModel() { }

    public CadastrarSaidaViewModel(
        List<Paciente> pacientes,
        List<Prescricao> prescricaos,
        List<Medicamento> medicamentos
    ) : this()
    {
        foreach (var paciente in pacientes)
        {
            var selecionarVM = new SelecionarPacienteViewModel(paciente.Id, paciente.Nome);

            PacientesDisponiveis.Add(selecionarVM);
        }

        foreach (var prescricao in prescricaos)
        {
            var selecionarVM = new SelecionarPrescricaoViewModel(prescricao.Id, prescricao.CRM);

            PrescricoesDisponiveis.Add(selecionarVM);
        }

        foreach (var medicamento in medicamentos)
        {
            var selecionarVM = new SelecionarMedicamentoViewModel(medicamento.Id, medicamento.Nome);

            MedicamentosDisponiveis.Add(selecionarVM);
        }
    }
}

public class ExcluirSaidaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirSaidaViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarSaidasViewModel
{
    public List<DetalhesSaidaViewModel> Registros { get; set; }

    public VisualizarSaidasViewModel(List<RequisicaoDeSaida> saidas)
    {
        Registros = new List<DetalhesSaidaViewModel>();

        foreach (var saida in saidas)
        {
            var detalhesVM = saida.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesSaidaViewModel
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string NomePaciente { get; set; }
    public string NomePrescricao { get; set; }
    public string NomeMedicamento { get; set; }
    
    public DetalhesSaidaViewModel(
        int id, 
        DateTime data, 
        string nomePaciente, 
        string nomePrescricao, 
        string nomeMedicamento
    )
    {
        Id = id;
        Data = data;
        NomePaciente = nomePaciente;
        NomePrescricao = nomePrescricao;
        NomeMedicamento = nomeMedicamento;
    }

    public override string ToString()
    {
        return $"Id: {Id} | Data: {Data:d} | Paciente: {NomePaciente} | CRM Prescrição: {NomePrescricao} | Medicamento: {NomeMedicamento}";
    }
}