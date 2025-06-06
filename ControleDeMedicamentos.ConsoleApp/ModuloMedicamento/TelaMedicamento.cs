﻿using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoDeSaida;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
{
    IRepositorioMedicamento repositorioMedicamento;
    IRepositorioFornecedor repositorioFornecedor;
    IRepositorioEntrada repositorioEntrada;
    IRepositorioRequisicaoDeSaida repositorioSaida;

    public TelaMedicamento(IRepositorioMedicamento repositorioMedicamento, 
        IRepositorioFornecedor repositorioFornecedor,
        IRepositorioEntrada repositorioEntrada,
        IRepositorioRequisicaoDeSaida repositorioSaida) : base("Medicamento", repositorioMedicamento)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioEntrada = repositorioEntrada;
        this.repositorioSaida = repositorioSaida;
    }

    public override bool ValidarExlcuir(Medicamento registro, int idRegistro)
    {
        List<Entrada> entradas = repositorioEntrada.SelecionarRegistros();
        List<RequisicaoDeSaida> saidas = repositorioSaida.SelecionarRegistros();

        foreach (var entrada in entradas)
        {
            if (entrada == null) continue;

            if (entrada.Medicamento == registro)
            {
                Notificador.ExibirMensagem("Não foi possível excluir o medicamento o mesmo contém 'Requisição de Entrada'", ConsoleColor.Red);
                return false;
            }
        }

        foreach (var saida in saidas)
        {
            if (saida == null) continue;

            if (saida.MedicamentoRequisitado == registro)
            {
                Notificador.ExibirMensagem("Não foi possível excluir o medicamento o mesmo contém 'Requisição de Saída'", ConsoleColor.Red);
                return false;
            }
        }

        return true;
    }

    public override void ExibirConteudoTabela(Medicamento registro)
    {
        if (registro.Estoque <= 20) Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(
            "{0, -10} | {1, -21} | {2, -21} | {3, -10} | {4, -15}",
            registro.Id, registro.Nome, registro.Descricao, registro.Estoque, registro.Fornecedor.Nome
        );
        Console.ResetColor();
    }

    public override void ExibirTabela()
    {
        Console.WriteLine(
            "{0, -10} | {1, -21} | {2, -21} | {3, -10} | {4, -15}",
            "Id", "Nome", "Descrição", "Estoque", "Fornecedor"
        );
    }
    public void VisualizarFornecedores()
    {
        Console.WriteLine($"Visualizando Fornecedores...");
        Console.WriteLine("---------------------------------");

        Console.WriteLine("{0,-10} | {1,-30} | {2,-20} | {3,-20} |",
        "id", "nome", "telefone", "Cnpj"
        );

        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        foreach (var fornecedor in fornecedores)
        {
            if (fornecedor == null) continue;

            Console.WriteLine("{0,-10} | {1,-30} | {2,-20} | {3,-20} |", 
            fornecedor.Id, fornecedor.Nome, fornecedor.Telefone, fornecedor.Cnpj);
        }
    }

    public override Medicamento ObterDados(bool validacaoExtra)
    {
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o descrição: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o estoque: ");
        int estoque = int.TryParse(Console.ReadLine(), out int valorEstoque) ? valorEstoque : -1;

        VisualizarFornecedores();
        int idFornecedor = int.TryParse(Console.ReadLine(), out int valorFornecedor) ? valorFornecedor : -1;

        Fornecedor fornecedor = repositorioFornecedor.SelecionarRegistroPorId(idFornecedor);

        if (fornecedor == null)
        {
            Notificador.ExibirMensagem($"Não existe Fornecedor com o ID {idFornecedor}", ConsoleColor.Red);
            return null!;
        }

        Medicamento novoMedicamento = new Medicamento(nome, descricao, estoque, fornecedor);

        return novoMedicamento;
    }
}
