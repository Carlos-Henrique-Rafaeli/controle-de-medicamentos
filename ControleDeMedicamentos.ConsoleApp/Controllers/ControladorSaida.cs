using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoDeSaida;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("saidas")]
public class ControladorSaida : Controller
{
    private ContextoDados contextoDados;
    private IRepositorioRequisicaoDeSaida repositorioSaida;
    private IRepositorioPaciente repositorioPaciente;
    private IrepossitorioPrescricao repositorioPrescricao;
    private IRepositorioMedicamento repositorioMedicamento;

    public ControladorSaida()
    {
        contextoDados = new ContextoDados(true);
        repositorioSaida = new RepositorioRequisicaoDeSaida(contextoDados);
        repositorioPaciente = new RepositorioPaciente(contextoDados);
        repositorioPrescricao = new RepositorioPrescricao(contextoDados);
        repositorioMedicamento = new RepositorioMedicamento(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var pacientes = repositorioPaciente.SelecionarRegistros();
        var prescricoes = repositorioPrescricao.SelecionarRegistros();
        var medicamentos = repositorioMedicamento.SelecionarRegistros();

        var cadastrarVM = new CadastrarSaidaViewModel(pacientes, prescricoes, medicamentos);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarSaidaViewModel cadastarVM)
    {
        var pacientes = repositorioPaciente.SelecionarRegistros();
        var prescricoes = repositorioPrescricao.SelecionarRegistros();
        var medicamentos = repositorioMedicamento.SelecionarRegistros();

        RequisicaoDeSaida saida = cadastarVM.ParaEntidade(pacientes, prescricoes, medicamentos);

        int quantidade = saida.PrescricaoMedica.Dosagem * saida.PrescricaoMedica.Periodo;

        saida.MedicamentoRequisitado.AtualizarEstoque(quantidade, false);
        
        repositorioSaida.CadastrarRegistro(saida);

        var notificacaoVM = new NotificacaoViewModel(
            "Requisição Cadastrada!",
            $"Foram removidas \"{quantidade}\" do registro \"{saida.MedicamentoRequisitado.Nome}\"!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult Excluir([FromRoute] int id)
    {
        var saida = repositorioSaida.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirSaidaViewModel(saida.Id, saida.MedicamentoRequisitado.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult ExcluirConfirmado([FromRoute] int id)
    {
        repositorioSaida.ExcluirRegistro(id);

        var notificacaoVM = new NotificacaoViewModel(
            "Requisição Excluída!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        var saidas = repositorioSaida.SelecionarRegistros();

        var visualizarVM = new VisualizarSaidasViewModel(saidas);

        return View(visualizarVM);
    }
}
