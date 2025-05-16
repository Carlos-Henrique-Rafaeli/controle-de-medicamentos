using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("medicamentos")]
public class ControladorMedicamento : Controller
{
    private ContextoDados contextoDados;
    private IRepositorioMedicamento repositorioMedicamento;
    private IRepositorioFornecedor repositorioFornecedor;

    public ControladorMedicamento()
    {
        contextoDados = new ContextoDados(true);
        repositorioMedicamento = new RepositorioMedicamento(contextoDados);
        repositorioFornecedor = new RepositorioFornecedor(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var fornecedores = repositorioFornecedor.SelecionarRegistros();

        var cadastrarVM = new CadastrarMedicamentoViewModel(fornecedores);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarMedicamentoViewModel cadastarVM)
    {
        var fornecedores = repositorioFornecedor.SelecionarRegistros();

        Medicamento medicamento = cadastarVM.ParaEntidade(fornecedores);

        repositorioMedicamento.CadastrarRegistro(medicamento);

        var notificacaoVM = new NotificacaoViewModel(
            "Medicamento Cadastrado!",
            $"O registro \"{medicamento.Nome}\" foi cadastrado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult Editar([FromRoute] int id)
    {
        var medicamento = repositorioMedicamento.SelecionarRegistroPorId(id);

        var fornecedores = repositorioFornecedor.SelecionarRegistros();

        var editarVM = new EditarMedicamentoViewModel(
            id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.Estoque,
            medicamento.Fornecedor.Id,
            fornecedores
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar([FromRoute] int id, EditarMedicamentoViewModel editarVM)
    {
        var fornecedores = repositorioFornecedor.SelecionarRegistros();

        var medicamentoOriginal = repositorioMedicamento.SelecionarRegistroPorId(id);

        var medicamentoEditado = editarVM.ParaEntidade(fornecedores);

        repositorioMedicamento.EditarRegistro(id, medicamentoEditado);

        var notificacaoVM = new NotificacaoViewModel(
            "Medicamento Editado!",
            $"O registro \"{medicamentoEditado.Nome}\" foi editado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult Excluir([FromRoute] int id)
    {
        var medicamento = repositorioMedicamento.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirMedicamentoViewModel(medicamento.Id, medicamento.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult ExcluirConfirmado([FromRoute] int id)
    {
        repositorioMedicamento.ExcluirRegistro(id);

        var notificacaoVM = new NotificacaoViewModel(
            "Medicamento Excluído!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        var medicamentos = repositorioMedicamento.SelecionarRegistros();

        var visualizarVM = new VisualizarMedicamentosViewModel(medicamentos);

        return View(visualizarVM);
    }
}