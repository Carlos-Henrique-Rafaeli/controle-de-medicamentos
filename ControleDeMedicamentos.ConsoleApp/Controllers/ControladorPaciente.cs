using ControleDeMedicamentos.ConsoleApp.Compartilhado;
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
        return View("Cadastrar");
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(
        [FromForm] string nome, 
        [FromForm] string telefone, 
        [FromForm] string sus)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        Paciente novoPaciente = new Paciente(nome, telefone, sus);

        repositorioPaciente.CadastrarRegistro(novoPaciente);

        ViewBag.Mensagem = $"O registro \"{novoPaciente.Nome}\" foi cadastrado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult ExibirFormularioEdicao([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        ViewBag.Paciente = pacienteSelecionado;

        return View("Editar");
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar(
        [FromRoute] int id, 
        [FromForm] string nome,
        [FromForm] string telefone,
        [FromForm] string sus)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        Paciente fornecedorAtualizado = new Paciente(nome, telefone, sus);

        repositorioPaciente.EditarRegistro(id, fornecedorAtualizado);

        ViewBag.Mensagem = $"O registro \"{fornecedorAtualizado.Nome}\" foi editado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult ExibirFormularioExclusao([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        ViewBag.Paciente = pacienteSelecionado;

        return View("Excluir");
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        repositorioPaciente.ExcluirRegistro(id);

        ViewBag.Mensagem = "O registro foi excluído com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();

        ViewBag.Pacientes = pacientes;

        return View("Visualizar");
    }
}
