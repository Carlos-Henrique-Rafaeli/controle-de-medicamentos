using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("funcionarios")]
public class ControladorFuncionario : Controller
{
    [HttpGet("cadastrar")]
    public IActionResult ExibirFormularioCadastro()
    {
        CadastrarFuncionarioViewModel cadastrarVM = new CadastrarFuncionarioViewModel();

        return View("Cadastrar", cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVm)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario novoFuncionario = cadastrarVm.ParaEntidade();

        repositorioFuncionario.CadastrarRegistro(novoFuncionario);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Funcionário Cadastrado!",
            $"O registro \"{novoFuncionario.Nome}\" foi cadastrado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult ExibirFormularioEdicao([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        EditarFuncionarioViewModel editarVM = new EditarFuncionarioViewModel(
            id,
            funcionarioSelecionado.Nome,
            funcionarioSelecionado.Telefone,
            funcionarioSelecionado.CPF
        );

        return View("Editar", editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar([FromRoute] int id, EditarFuncionarioViewModel editarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario funcionarioAtualizado = editarVM.ParaEntidade();

        repositorioFuncionario.EditarRegistro(id, funcionarioAtualizado);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Funcionário Editado!",
            $"O registro \"{funcionarioAtualizado.Nome}\" foi editado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult ExibirFormularioExclusao([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        ExcluirFuncionarioViewModel excluirVM = new ExcluirFuncionarioViewModel(
            funcionarioSelecionado.Id,
            funcionarioSelecionado.Nome
        );

        return View("Excluir", excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        repositorioFuncionario.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Funcionário Excluído!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();

        VisualizarFuncionariosViewModel visualizarVM = new VisualizarFuncionariosViewModel(funcionarios);

        return View("Visualizar", visualizarVM);
    }
}
