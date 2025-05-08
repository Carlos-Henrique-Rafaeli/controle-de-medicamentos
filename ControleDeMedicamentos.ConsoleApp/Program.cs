using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        WebApplication app = builder.Build();

        app.MapGet("/", PaginaInicial);

        // Fornecedores
        app.MapGet("/fornecedores/cadastrar", FornecedorWeb.ExibirFormularioCadastro);
        app.MapPost("/fornecedores/cadastrar", FornecedorWeb.Cadastrar);

        app.MapGet("/fornecedores/editar/{id:int}", FornecedorWeb.ExibirFormularioEdicao);
        app.MapPost("/fornecedores/editar/{id:int}", FornecedorWeb.Editar);

        app.MapGet("/fornecedores/excluir/{id:int}", FornecedorWeb.ExibirFormularioExclusao);
        app.MapPost("/fornecedores/excluir/{id:int}", FornecedorWeb.Excluir);

        app.MapGet("/fornecedores/visualizar", FornecedorWeb.Visualizar);

        // Funcionários
        app.MapGet("/funcionarios/cadastrar", FuncionarioWeb.ExibirFormularioCadastro);
        app.MapPost("/funcionarios/cadastrar", FuncionarioWeb.Cadastrar);

        app.MapGet("/funcionarios/editar/{id:int}", FuncionarioWeb.ExibirFormularioEdicao);
        app.MapPost("/funcionarios/editar/{id:int}", FuncionarioWeb.Editar);

        app.MapGet("/funcionarios/excluir/{id:int}", FuncionarioWeb.ExibirFormularioExclusao);
        app.MapPost("/funcionarios/excluir/{id:int}", FuncionarioWeb.Excluir);

        app.MapGet("/funcionarios/visualizar", FuncionarioWeb.Visualizar);

        // Pacientes
        app.MapGet("/pacientes/cadastrar", PacienteWeb.ExibirFormularioCadastro);
        app.MapPost("/pacientes/cadastrar", PacienteWeb.Cadastrar);

        app.MapGet("/pacientes/editar/{id:int}", PacienteWeb.ExibirFormularioEdicao);
        app.MapPost("/pacientes/editar/{id:int}", PacienteWeb.Editar);

        app.MapGet("/pacientes/excluir/{id:int}", PacienteWeb.ExibirFormularioExclusao);
        app.MapPost("/pacientes/excluir/{id:int}", PacienteWeb.Excluir);

        app.MapGet("/pacientes/visualizar", PacienteWeb.Visualizar);

        app.Run();
    }

    static Task PaginaInicial(HttpContext context)
    {
        string conteudo = File.ReadAllText("Compartilhado/Html/PaginaInicial.html");

        return context.Response.WriteAsync(conteudo);
    }
}
