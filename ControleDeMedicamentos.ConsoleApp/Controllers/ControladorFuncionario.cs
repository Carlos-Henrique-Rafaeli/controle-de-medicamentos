using ControleDeMedicamentos.ConsoleApp.Compartilhado;
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
        return View("Cadastrar");
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(
        [FromForm] string nome, 
        [FromForm] string telefone, 
        [FromForm] string cpf)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario novoFuncionario = new Funcionario(nome, telefone, cpf);

        repositorioFuncionario.CadastrarRegistro(novoFuncionario);

        ViewBag.Mensagem = $"O registro \"{novoFuncionario.Nome}\" foi cadastrado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult ExibirFormularioEdicao([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        ViewBag.Funcionario = funcionarioSelecionado;

        return View("Editar");
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar(
        [FromRoute] int id, 
        [FromForm] string nome,
        [FromForm] string telefone,
        [FromForm] string cpf)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario fornecedorAtualizado = new Funcionario(nome, telefone, cpf);

        repositorioFuncionario.EditarRegistro(id, fornecedorAtualizado);

        ViewBag.Mensagem = $"O registro \"{fornecedorAtualizado.Nome}\" foi editado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult ExibirFormularioExclusao([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        ViewBag.Funcionario = funcionarioSelecionado;

        return View("Excluir");
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        repositorioFuncionario.ExcluirRegistro(id);

        ViewBag.Mensagem = "O registro foi excluído com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();

        ViewBag.Funcionarios = funcionarios;

        return View("Visualizar");
    }
}
