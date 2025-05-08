using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System.Text;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public static class FuncionarioWeb
{
    public static Task ExibirFormularioCadastro(HttpContext context)
    {
        string conteudo = File.ReadAllText("ModuloFuncionario/Html/Cadastrar.html");

        return context.Response.WriteAsync(conteudo);
    }

    public static Task Cadastrar(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cpf = context.Request.Form["cpf"].ToString();

        Funcionario novoFuncionario = new Funcionario(nome, telefone, cpf);

        repositorioFuncionario.CadastrarRegistro(novoFuncionario);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro \"{novoFuncionario.Nome}\" foi cadastrado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task ExibirFormularioEdicao(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        string conteudo = File.ReadAllText("ModuloFuncionario/Html/Editar.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#nome#", funcionarioSelecionado.Nome);
        sb.Replace("#telefone#", funcionarioSelecionado.Telefone);
        sb.Replace("#cpf#", funcionarioSelecionado.CPF);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task Editar(HttpContext context)
    {
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cpf = context.Request.Form["cpf"].ToString();

        Funcionario fornecedorAtualizado = new Funcionario(nome, telefone, cpf);

        repositorioFuncionario.EditarRegistro(id, fornecedorAtualizado);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro \"{fornecedorAtualizado.Nome}\" foi editado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task ExibirFormularioExclusao(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Funcionario fornecedorSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        string conteudo = File.ReadAllText("ModuloFuncionario/Html/Excluir.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#funcionario#", fornecedorSelecionado.Nome);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task Excluir(HttpContext context)
    {
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        repositorioFuncionario.ExcluirRegistro(id);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro foi excluído com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task Visualizar(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        string conteudo = File.ReadAllText("ModuloFuncionario/Html/Visualizar.html");

        StringBuilder stringBuilder = new StringBuilder(conteudo);

        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();

        foreach (var f in funcionarios)
        {
            string itemLista = $"<li>{f.ToString()} / <a href=\"/funcionarios/editar/{f.Id}\">Editar</a> / <a href=\"/funcionarios/excluir/{f.Id}\">Excluir</a> </li> #funcionario#";

            stringBuilder.Replace("#funcionario#", itemLista);
        }

        stringBuilder.Replace("#funcionario#", "");

        string conteudoString = stringBuilder.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

}
