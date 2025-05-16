using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public abstract class FormularioPacienteViewModel
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CartaoSUS { get; set; }
}

public class CadastrarPacienteViewModel : FormularioPacienteViewModel
{
    public CadastrarPacienteViewModel() { }

    public CadastrarPacienteViewModel(string nome, string telefone, string cartaoSus)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaoSus;
    }
}

public class EditarPacienteViewModel : FormularioPacienteViewModel
{
    public int Id { get; set; }

    public EditarPacienteViewModel() { }

    public EditarPacienteViewModel(int id, string nome, string telefone) : this()
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
    }
}

public class ExcluirPacienteViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirPacienteViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarPacientesViewModel
{
    public List<DetalhesPacienteViewModel> Registros { get; } = new List<DetalhesPacienteViewModel>();

    public VisualizarPacientesViewModel(List<Paciente> pacientes)
    {
        foreach (Paciente p in pacientes)
        {
            DetalhesPacienteViewModel detalhesVM = p.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesPacienteViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CartaoSUS { get; set; }

    public DetalhesPacienteViewModel(int id, string nome, string telefone, string cartaoSus)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaoSus;
    }
    public override string ToString()
    {
        return $"Id: {Id} | Nome: {Nome} | Telefone: {Telefone} | Cartão SUS: {CartaoSUS}";
    }
}