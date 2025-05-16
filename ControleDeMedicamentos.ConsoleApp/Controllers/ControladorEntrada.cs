using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("entradas")]
public class ControladorEntrada : Controller
{
    private ContextoDados contextoDados;
    private IRepositorioEntrada repositorioEntrada;
    private IRepositorioMedicamento repositorioMedicamento;
    private IRepositorioFuncionario repositorioFuncionario;

    public ControladorEntrada()
    {
        contextoDados = new ContextoDados(true);
        repositorioEntrada = new RepositorioEntrada(contextoDados);
        repositorioMedicamento = new RepositorioMedicamento(contextoDados);
        repositorioFuncionario = new RepositorioFuncionario(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var funcionarios = repositorioFuncionario.SelecionarRegistros();
        var medicamentos = repositorioMedicamento.SelecionarRegistros();

        var cadastrarVM = new CadastrarEntradaViewModel(medicamentos, funcionarios);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarEntradaViewModel cadastarVM)
    {
        var funcionarios = repositorioFuncionario.SelecionarRegistros();
        var medicamentos = repositorioMedicamento.SelecionarRegistros();

        Entrada entrada = cadastarVM.ParaEntidade(medicamentos, funcionarios);
        
        entrada.Medicamento.AtualizarEstoque(entrada.Quantidade);
        
        repositorioEntrada.CadastrarRegistro(entrada);

        var notificacaoVM = new NotificacaoViewModel(
            "Entrada Cadastrada!",
            $"Foram adicionadas \"{entrada.Quantidade}\" ao registro \"{entrada.Medicamento.Nome}\"!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult Excluir([FromRoute] int id)
    {
        var entrada = repositorioEntrada.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirEntradaViewModel(entrada.Id, entrada.Medicamento.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult ExcluirConfirmado([FromRoute] int id)
    {
        repositorioEntrada.ExcluirRegistro(id);

        var notificacaoVM = new NotificacaoViewModel(
            "Entrada Excluída!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        var entradas = repositorioEntrada.SelecionarRegistros();

        var visualizarVM = new VisualizarEntradasViewModel(entradas);

        return View(visualizarVM);
    }
}
