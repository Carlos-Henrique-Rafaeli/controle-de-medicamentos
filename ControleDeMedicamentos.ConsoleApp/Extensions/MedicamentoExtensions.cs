using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class MedicamentoExtensions
{
    public static Medicamento ParaEntidade(
        this FormularioMedicamentoViewModel formularioVM,
        List<Fornecedor> fornecedores
    )
    {
        Fornecedor fornecedorSelecionado = null;

        foreach (var f in fornecedores)
        {
            if (f.Id == formularioVM.FornecedorId)
            {
                fornecedorSelecionado = f;
                break;
            }
        }

        return new Medicamento(
            formularioVM.Nome, 
            formularioVM.Descricao, 
            formularioVM.Estoque, 
            fornecedorSelecionado
        );
    }

    public static DetalhesMedicamentoViewModel ParaDetalhesVM(this Medicamento medicamento)
    {
        return new DetalhesMedicamentoViewModel(
            medicamento.Id, 
            medicamento.Nome, 
            medicamento.Descricao, 
            medicamento.Estoque, 
            medicamento.Fornecedor.Nome
        );
    }
}
