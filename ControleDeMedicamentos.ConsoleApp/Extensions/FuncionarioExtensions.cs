using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class FuncionarioExtensions
{
    public static Funcionario ParaEntidade(this FormularioFuncionarioViewModel formularioVM)
    {
        return new Funcionario(
            formularioVM.Nome,
            formularioVM.Telefone,
            formularioVM.CPF
        );
    }

    public static DetalhesFuncionarioViewModel ParaDetalhesVM(this Funcionario funcionario)
    {
        return new DetalhesFuncionarioViewModel(
            funcionario.Id,
            funcionario.Nome,
            funcionario.Telefone,
            funcionario.CPF
        );
    }
}
