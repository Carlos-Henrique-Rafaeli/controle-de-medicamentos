using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoDeSaida;

namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class SaidaExtensions
{
    public static RequisicaoDeSaida ParaEntidade(
        this FormularioSaidaViewModel formularioVM, 
        List<Paciente> pacientes, 
        List<Prescricao> prescricaos, 
        List<Medicamento> medicamentos)
    {
        Paciente pacienteSelecionado = null;
        Prescricao prescricaoSelecionada = null;
        Medicamento medicamentoSelecionado = null;

        foreach (var p in pacientes)
        {
            if (p.Id == formularioVM.PacienteId)
            {
                pacienteSelecionado = p;
                break;
            }
        }

        foreach (var p in prescricaos)
        {
            if (p.Id == formularioVM.PrescricaoId)
            {
                prescricaoSelecionada = p;
                break;
            }
        }

        foreach (var m in medicamentos)
        {
            if (m.Id == formularioVM.MedicamentoId)
            {
                medicamentoSelecionado = m;
                break;
            }
        }

        return new RequisicaoDeSaida(
            formularioVM.Data, 
            pacienteSelecionado, 
            prescricaoSelecionada, 
            medicamentoSelecionado
        );
    }

    public static DetalhesSaidaViewModel ParaDetalhesVM(this RequisicaoDeSaida saida)
    {
        return new DetalhesSaidaViewModel(
            saida.Id, 
            saida.Data, 
            saida.Paciente.Nome, 
            saida.PrescricaoMedica.CRM, 
            saida.MedicamentoRequisitado.Nome
        );
    }
}
