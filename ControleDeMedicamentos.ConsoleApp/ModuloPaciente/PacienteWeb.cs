using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System.Text;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public static class PacienteWeb
{
    public static Task ExibirFormularioCadastro(HttpContext context)
    {
        string conteudo = File.ReadAllText("ModuloPaciente/Html/Cadastrar.html");

        return context.Response.WriteAsync(conteudo);
    }

    public static Task Cadastrar(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string sus = context.Request.Form["sus"].ToString();

        Paciente novoFornecedor = new Paciente(nome, telefone, sus);

        repositorioPaciente.CadastrarRegistro(novoFornecedor);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro \"{novoFornecedor.Nome}\" foi cadastrado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task ExibirFormularioEdicao(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Paciente fornecedorSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        string conteudo = File.ReadAllText("ModuloPaciente/Html/Editar.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#nome#", fornecedorSelecionado.Nome);
        sb.Replace("#telefone#", fornecedorSelecionado.Telefone);
        sb.Replace("#sus#", fornecedorSelecionado.CartaoSUS);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task Editar(HttpContext context)
    {
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string sus = context.Request.Form["sus"].ToString();

        Paciente fornecedorAtualizado = new Paciente(nome, telefone, sus);

        repositorioPaciente.EditarRegistro(id, fornecedorAtualizado);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro \"{fornecedorAtualizado.Nome}\" foi editado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task ExibirFormularioExclusao(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Paciente fornecedorSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        string conteudo = File.ReadAllText("ModuloPaciente/Html/Excluir.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#paciente#", fornecedorSelecionado.Nome);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task Excluir(HttpContext context)
    {
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        repositorioPaciente.ExcluirRegistro(id);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro foi excluído com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task Visualizar(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        string conteudo = File.ReadAllText("ModuloPaciente/Html/Visualizar.html");

        StringBuilder stringBuilder = new StringBuilder(conteudo);

        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();

        foreach (var p in pacientes)
        {
            string itemLista = $"<li>{p.ToString()} / <a href=\"/pacientes/editar/{p.Id}\">Editar</a> / <a href=\"/pacientes/excluir/{p.Id}\">Excluir</a> </li> #paciente#";

            stringBuilder.Replace("#paciente#", itemLista);
        }

        stringBuilder.Replace("#paciente#", "");

        string conteudoString = stringBuilder.ToString();

        return context.Response.WriteAsync(conteudoString);
    }
}
