using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("prescricoes")]
public class ControladorPrescricao : Controller
{
    private ContextoDados contextoDados;
    private IrepossitorioPrescricao repossitorioPrescricao;
    private IRepositorioMedicamento repositorioMedicamento;

    public ControladorPrescricao()
    {
        contextoDados = new ContextoDados(true);
        repossitorioPrescricao = new RepositorioPrescricao(contextoDados);
        repositorioMedicamento = new RepositorioMedicamento(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var medicamentos = repositorioMedicamento.SelecionarRegistros();

        var cadastrarVM = new CadastrarPrescricaoViewModel(medicamentos);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarPrescricaoViewModel cadastarVM)
    {
        var medicamentos = repositorioMedicamento.SelecionarRegistros();

        Prescricao prescricao = cadastarVM.ParaEntidade(medicamentos);

        repossitorioPrescricao.CadastrarRegistro(prescricao);

        var notificacaoVM = new NotificacaoViewModel(
            "Prescrição Cadastrada!",
            $"O registro \"{prescricao.Medicamento.Nome}\" foi cadastrado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult Excluir([FromRoute] int id)
    {
        var prescricao = repossitorioPrescricao.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirPrescricaoViewModel(prescricao.Id, prescricao.Medicamento.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult ExcluirConfirmado([FromRoute] int id)
    {
        repossitorioPrescricao.ExcluirRegistro(id);

        var notificacaoVM = new NotificacaoViewModel(
            "Prescrição Excluída!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        var prescricoes = repossitorioPrescricao.SelecionarRegistros();

        var visualizarVM = new VisualizarPrescricaoViewModel(prescricoes);

        return View(visualizarVM);
    }
}