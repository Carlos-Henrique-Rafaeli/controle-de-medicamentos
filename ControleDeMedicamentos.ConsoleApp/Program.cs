using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

namespace ControleDeMedicamentos.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        WebApplication app = builder.Build();

        app.MapGet("/", PaginaInicial);

        app.MapGet("/fornecedores/cadastrar", FornecedorWeb.ExibirFormularioCadastroFornecedor);
        app.MapPost("/fornecedores/cadastrar", FornecedorWeb.CadastrarFornecedor);

        app.MapGet("/fornecedores/editar/{id:int}", FornecedorWeb.ExibirFormularioEdicaoFornecedor);
        app.MapPost("/fornecedores/editar/{id:int}", FornecedorWeb.EditarFornecedor);

        app.MapGet("/fornecedores/excluir/{id:int}", FornecedorWeb.ExibirFormularioExclusaoFornecedor);
        app.MapPost("/fornecedores/excluir/{id:int}", FornecedorWeb.ExcluirFornecedor);

        app.MapGet("/fornecedores/visualizar", FornecedorWeb.VisualizarFornecedores);

        app.Run();
    }

    static Task PaginaInicial(HttpContext context)
    {
        string conteudo = File.ReadAllText("Compartilhado/Html/PaginaInicial.html");

        return context.Response.WriteAsync(conteudo);
    }
}
