using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("fornecedores")]
public class ControladorFornecedor : Controller
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
        [FromForm] string cnpj
    )
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        Fornecedor novoFornecedor = new Fornecedor(nome, telefone, cnpj);

        repositorioFornecedor.CadastrarRegistro(novoFornecedor);

        ViewBag.Mensagem = $"O registro \"{novoFornecedor.Nome}\" foi cadastrado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult ExibirFormularioEdicao([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);

        ViewBag.Fornecedor = fornecedorSelecionado;

        return View("Editar");
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar(
        [FromRoute] int id,
        [FromForm] string nome,
        [FromForm] string telefone,
        [FromForm] string cnpj)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        Fornecedor fornecedorAtualizado = new Fornecedor(nome, telefone, cnpj);

        repositorioFornecedor.EditarRegistro(id, fornecedorAtualizado);

        ViewBag.Mensagem = $"O registro \"{fornecedorAtualizado.Nome}\" foi editado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult ExibirFormularioExclusao([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);

        ViewBag.Fornecedor = fornecedorSelecionado;

        return View("Excluir");
    }

    [HttpPost("excluir/{id:int}")]
    public  IActionResult Excluir([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        repositorioFornecedor.ExcluirRegistro(id);

        ViewBag.Mensagem = "O registro foi excluído com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        ViewBag.Fornecedores = fornecedores;

        return View("Visualizar");
    }
}
