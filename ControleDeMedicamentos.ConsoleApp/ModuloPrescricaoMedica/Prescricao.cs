using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

public class Prescricao : EntidadeBase<Prescricao>
{
    public string CRM { get; set; }
    public DateTime DATA { get; set; }
    public Medicamento Medicamento { get; set; }
    public int Dosagem { get; set; }
    public int Periodo  { get; set; }

    public Prescricao()
    {
    }

    public Prescricao(string crm, Medicamento medicamento , int dosagem , int periodo) : this()
    {
        CRM = crm;
        Medicamento = medicamento;
        DATA = DateTime.Now;
        Dosagem = dosagem;
        Periodo = periodo;
    }

    public override void AtualizarRegistro(Prescricao registroEditado)
    {
        registroEditado.CRM = registroEditado.CRM;
        registroEditado.Medicamento = registroEditado.Medicamento;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrEmpty(CRM))
            erros += "O campo CRM é obrigatorio";

        if (CRM.Length > 6 || CRM.Length <6)
            erros += "O campo CRM deve conter 6 caracteres ";

        if (Medicamento == null)
            erros += "Informe um medicamento";

        return erros;
    }
}



