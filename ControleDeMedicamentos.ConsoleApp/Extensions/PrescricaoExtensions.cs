using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class PrescricaoExtensions
{
    public static Prescricao ParaEntidade(
        this FormularioPrescricaoViewModel formularioVM, 
        List<Medicamento> medicamentos
    )
    {
        Medicamento medicamentoSelecionado = null;

        foreach (var m in medicamentos)
        {
            if (m.Id == formularioVM.MedicamentoId)
            {
                medicamentoSelecionado = m;
                break;
            }
        }

        return new Prescricao(
            formularioVM.CRM, 
            medicamentoSelecionado, 
            formularioVM.Dosagem, 
            formularioVM.Periodo
        );
    }

    public static DetalhesPrescricaoViewModel ParaDetalhesVM(this Prescricao prescricao)
    {
        return new DetalhesPrescricaoViewModel(
            prescricao.Id, 
            prescricao.CRM, 
            prescricao.DATA, 
            prescricao.Medicamento.Nome, 
            prescricao.Dosagem, 
            prescricao.Periodo
        );
    }
}
