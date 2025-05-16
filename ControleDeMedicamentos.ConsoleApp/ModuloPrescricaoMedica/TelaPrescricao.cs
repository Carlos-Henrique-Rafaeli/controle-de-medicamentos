using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class TelaPrescricao : TelaBase<Prescricao>, ITelaCrud
{
    IRepositorioMedicamento repositorioMedicamento;

    public TelaPrescricao(IrepossitorioPrescricao repositorio, IRepositorioMedicamento repositorioMedicamento) : base("Prescricão", repositorio)
    {
       this.repositorioMedicamento = repositorioMedicamento;
    }

    public void VisualizarMedicamentos()
    {
        Console.WriteLine($"Visualizando Medicamentos...");
        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -10} | {1, -21} | {2, -21} | {3, -10} | {4, -15}",
            "Id", "Nome", "Descrição", "Estoque", "Fornecedor"
        );

        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

        foreach (var medicamento in medicamentos)
        {
            if (medicamento == null) continue;

            if (medicamento.Estoque <= 20) Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(
                "{0, -10} | {1, -21} | {2, -21} | {3, -10} | {4, -15}",
                medicamento.Id, medicamento.Nome, medicamento.Descricao, medicamento.Estoque, medicamento.Fornecedor.Nome
            );
            Console.ResetColor();
        }
    }

    public override Prescricao ObterDados(bool validacaoExtra)
    {
        Console.Write("Informe um CRM Válido: ");
        string crm  = Console.ReadLine() ?? string.Empty;

        VisualizarMedicamentos();
        int idMedicamento = int.TryParse(Console.ReadLine(), out int valorMedicamento) ? valorMedicamento : -1;

        Medicamento medicamento = repositorioMedicamento.SelecionarRegistroPorId(idMedicamento);

        if (medicamento == null)
        {
            Notificador.ExibirMensagem($"Não existe Medicamento com o ID {idMedicamento}", ConsoleColor.Red);
            return null!;
        }

        Console.Write("Informe a dosagem do medicamento: ");
        int dosagem = int.TryParse(Console.ReadLine(), out int valorDosagem) ? valorDosagem : 0;
        
        Console.Write("Informe o periodo: ");
        int periodo = int.TryParse(Console.ReadLine(), out int valorPeriodo) ? valorPeriodo : 0;

        Prescricao prescricao = new Prescricao(crm, medicamento, dosagem, periodo);

        return prescricao;
    }
    public override void ExibirTabela()
    {
        Console.WriteLine("{0,-10} | {1,-15} | {2,-20} | {3,-20} | |{4, -10} | {5, -10}",
            "id", "CRM", "Medicamento",   "Dosagem", "Periodo"  ,"DATA" );
    }


    public override void ExibirConteudoTabela(Prescricao registro)
    {
        {
            Prescricao r = registro;
            Console.WriteLine("{0,-10} | {1,-15} | {2,-20} | {3,-20} | |{4, -10} | {5, -10}",       
            r.Id, r.CRM, r.Medicamento.Nome,r.Dosagem,r.Periodo, r.DATA.ToShortDateString()
                );
        }
    }
}
