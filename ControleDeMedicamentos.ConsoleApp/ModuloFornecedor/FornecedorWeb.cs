using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System.Text;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public static class FornecedorWeb
{
    public static Task ExibirFormularioCadastroFornecedor(HttpContext context)
    {
        string conteudo = File.ReadAllText("ModuloFornecedor/Html/Cadastrar.html");

        return context.Response.WriteAsync(conteudo);
    }

    public static Task CadastrarFornecedor(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cnpj = context.Request.Form["cnpj"].ToString();

        Fornecedor novoFornecedor = new Fornecedor(nome, telefone, cnpj);

        repositorioFornecedor.CadastrarRegistro(novoFornecedor);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro \"{novoFornecedor.Nome}\" foi cadastrado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task ExibirFormularioEdicaoFornecedor(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);

        string conteudo = File.ReadAllText("ModuloFornecedor/Html/Editar.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#nome#", fornecedorSelecionado.Nome);
        sb.Replace("#telefone#", fornecedorSelecionado.Telefone);
        sb.Replace("#cnpj#", fornecedorSelecionado.Cnpj);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task EditarFornecedor(HttpContext context)
    {
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cnpj = context.Request.Form["cnpj"].ToString();

        Fornecedor fornecedorAtualizado = new Fornecedor(nome, telefone, cnpj);

        repositorioFornecedor.EditarRegistro(id, fornecedorAtualizado);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro \"{fornecedorAtualizado.Nome}\" foi editado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task ExibirFormularioExclusaoFornecedor(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);

        string conteudo = File.ReadAllText("ModuloFornecedor/Html/Excluir.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#fornecedor#", fornecedorSelecionado.Nome);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task ExcluirFornecedor(HttpContext context)
    {
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        repositorioFornecedor.ExcluirRegistro(id);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro foi excluído com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    public static Task VisualizarFornecedores(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        string conteudo = File.ReadAllText("ModuloFornecedor/Html/Visualizar.html");

        StringBuilder stringBuilder = new StringBuilder(conteudo);

        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        foreach (var f in fornecedores)
        {
            string itemLista = $"<li>{f.ToString()} / <a href=\"/fornecedores/editar/{f.Id}\">Editar</a> / <a href=\"/fornecedores/excluir/{f.Id}\">Excluir</a> </li> #fornecedor#";

            stringBuilder.Replace("#fornecedor#", itemLista);
        }

        stringBuilder.Replace("#fornecedor#", "");

        string conteudoString = stringBuilder.ToString();

        return context.Response.WriteAsync(conteudoString);
    }
}
