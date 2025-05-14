using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores
{
    public class Fornecedor : EntidadeBase<Fornecedor>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Cnpj { get; set; }

        public Fornecedor()
        {
        }

        public Fornecedor(string nome, string telefone, string cnpj) : this()
        {
            Nome = nome;
            Telefone = telefone;
            Cnpj = cnpj;
        }


        public override void AtualizarRegistro(Fornecedor registroEditado)
        {
            Nome = registroEditado.Nome;
            Telefone = registroEditado.Telefone;
            Cnpj = registroEditado.Cnpj;
        }

        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrEmpty(Nome))
                erros += "O campo Nome é obrigatorio !\n";

            else if (Nome.Length < 3)
                erros += "O campo Nome não deve conter menos de 3 caracteres\n";

            if (Nome.Length > 100)
                erros += "O campo Nome não deve conter mais de 100 caracteres\n";

            if (string.IsNullOrEmpty(Telefone))
                erros += "O campo telefone é obrigatorio !\n";

            if (string.IsNullOrEmpty(Cnpj) )
                erros += "O campo Cnpj é obrigatorio !\n";

            if (Cnpj.Length > 14 || Cnpj.Length <14)
                erros += "O campo CNPJ deve conter 14 caracteres\n";

            if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
                erros += "O campo 'Telefone' deve seguir o padrão () 0000-0000 ou (DDD) 00000-0000.\n";


            return erros;
        }
    }
}
