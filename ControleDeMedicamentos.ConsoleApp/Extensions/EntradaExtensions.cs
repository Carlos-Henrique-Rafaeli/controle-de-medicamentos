using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class EntradaExtensions
{
    public static Entrada ParaEntidade(
        this FormularioEntradaViewModel formularioVM, 
        List<Medicamento> medicamentos, 
        List<Funcionario> funcionarios
    )
    {
        Medicamento medicamentoSelecionado = null;
        Funcionario funcionarioSelecionado = null;

        foreach (var m in medicamentos)
        {
            if (m.Id == formularioVM.MedicamentoId)
            {
                medicamentoSelecionado = m;
                break;
            }
        }

        foreach (var f in funcionarios)
        {
            if (f.Id == formularioVM.FuncionarioId)
            {
                funcionarioSelecionado = f;
                break;
            }
        }

        return new Entrada(
            formularioVM.Data, 
            medicamentoSelecionado, 
            funcionarioSelecionado, 
            formularioVM.Quantidade
        );
    }

    public static DetalhesEntradaViewModel ParaDetalhesVM(this Entrada entrada)
    {
        return new DetalhesEntradaViewModel(
            entrada.Id, 
            entrada.Data, 
            entrada.Medicamento.Nome, 
            entrada.Funcionario.Nome, 
            entrada.Quantidade
        );
    }
}
