using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public class Funcionario : EntidadeBase<Funcionario>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CPF { get; set; }

    public Funcionario()
    {
    }

    public Funcionario(string nome, string telefone, string cpf) : this()
    {
        Nome = nome;
        Telefone = telefone;
        CPF = cpf;
    }


    public override void AtualizarRegistro(Funcionario registroEditado)
    {
        Nome = registroEditado.Nome;
        Telefone = registroEditado.Telefone;
        CPF = registroEditado.CPF;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "O campo 'Nome' é obrigatório.\n";

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros += "O campo 'Nome' deve conter entre 3 e 100 caracteres.\n";

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo 'Telefone' é obrigatório.\n";

        else if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
            erros += "O campo 'Telefone' é deve seguir o padrão () 0000-0000 ou (DDD) 00000-0000.\n";

        if (string.IsNullOrWhiteSpace(CPF))
            erros += "O campo 'CPF' é obrigatório.\n";

        else if (!Regex.IsMatch(CPF, @"^\d{11}$"))
            erros += "O campo 'CPF' é precisa conter 11 números.\n";

        return erros;
    }

    public override string ToString()
    {
        return $"Id: {Id} | Nome: {Nome} | Telefone: {Telefone} | CPF: {CPF}";
    }
}
