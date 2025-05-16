using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("pacientes")]
public class ControladorPaciente : Controller
{
    [HttpGet("cadastrar")]
    public IActionResult ExibirFormularioCadastro()
    {
        CadastrarPacienteViewModel cadastrarVM = new CadastrarPacienteViewModel();
        
        return View("Cadastrar", cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarPacienteViewModel cadastrarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        Paciente novoPaciente = cadastrarVM.ParaEntidade();

        repositorioPaciente.CadastrarRegistro(novoPaciente);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Paciente Cadastrado!",
            $"O registro \"{novoPaciente.Nome}\" foi cadastrado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult ExibirFormularioEdicao([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        EditarPacienteViewModel editarVM = new EditarPacienteViewModel(
            id,
            pacienteSelecionado.Nome,
            pacienteSelecionado.Telefone
        );

        return View("Editar", editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar([FromRoute] int id, EditarPacienteViewModel editarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        Paciente pacienteAtualizado = editarVM.ParaEntidade();

        repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Paciente Editado!",
            $"O registro \"{pacienteAtualizado.Nome}\" foi editado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult ExibirFormularioExclusao([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        ExcluirPacienteViewModel excluirVM = new ExcluirPacienteViewModel(
            pacienteSelecionado.Id,
            pacienteSelecionado.Nome
        );

        return View("Excluir", excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        repositorioPaciente.ExcluirRegistro(id);

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
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();

        VisualizarPacientesViewModel visualizarVM = new VisualizarPacientesViewModel(pacientes);

        return View("Visualizar", visualizarVM);
    }
}
