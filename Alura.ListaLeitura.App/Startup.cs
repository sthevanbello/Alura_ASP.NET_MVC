using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);

            //Mapeando as rotas
            routeBuilder.MapRoute("Livros/ParaLer", LivrosParaLer);
            routeBuilder.MapRoute("Livros/Lendo", LivrosLendo);
            routeBuilder.MapRoute("Livros/Lidos", LivrosLidos);
            routeBuilder.MapRoute("Cadastro/NovoLivro/{Titulo}/{Autor}", NovoLivrosParaLer);
            routeBuilder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes);
            routeBuilder.MapRoute("Cadastro/NovoLivro", ExibeFormulario);
            routeBuilder.MapRoute("Cadastro/Incluir", ProcessaFormulario);

            //Constrói a rota completa através do método Build()
            var rotas = routeBuilder.Build();

            app.UseRouter(rotas);

            //app.Run(Roteamento);
        }

        

        public Task Roteamento(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            var caminhosAtendidos = new Dictionary<string, RequestDelegate>()
            {
                { "/Livros/ParaLer", LivrosParaLer},
                { "/Livros/Lendo", LivrosLendo },
                { "/Livros/Lidos", LivrosLidos }
            };
            var req = context.Request.Path;

            if (caminhosAtendidos.ContainsKey(req))
            {
                var metodo = caminhosAtendidos[req];

                return metodo.Invoke(context);
            }
            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Caminhos inexistente");


        }

        private Task ProcessaFormulario(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.Request.Form["Titulo"].First(), //Com o method="post" no html os dados são passados por .Form e não por .Query
                Autor = context.Request.Form["Autor"].First()
            };

            var repo = new LivroRepositorioCSV();

            repo.Incluir(livro);

            return context.Response.WriteAsync("O livro foi adicionado com sucesso");
        }

        private Task ExibeFormulario(HttpContext context)
        {
            var html = CarregaHTML("Formulario"); 
            return context.Response.WriteAsync(html);
        }

        private string CarregaHTML(string NomeArquivo)
        {
            //D:/Developer/Alura/Asp.NET Core Uma webapp usando o padrão MVC/Alura.ListaLeitura/Alura.ListaLeitura.App/
            //var nomeCompletoArquivo = $"../../../HTML/{NomeArquivo}.html";

            var nomeCompletoArquivo = $"HTML/{NomeArquivo}.html";
            using (var arquivo = File.OpenText(nomeCompletoArquivo))
            {
                return arquivo.ReadToEnd();
            }
        }

        private Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));

            var repo = new LivroRepositorioCSV();

            var livro = repo.Todos.FirstOrDefault(l => l.Id == id);

            if (!(livro == null))
            {
                return context.Response.WriteAsync(livro.Detalhes());
            }

            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Livro não encontrado");
        }
        public Task NovoLivrosParaLer(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.GetRouteValue("Titulo").ToString(),
                Autor = context.GetRouteValue("Autor").ToString()
            };

            var repo = new LivroRepositorioCSV();

            repo.Incluir(livro);

            return context.Response.WriteAsync("O livro foi adicionado com sucesso");
        }

        public async Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            await context.Response.WriteAsync(_repo.ParaLer.ToString());
        }

        public async Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            await context.Response.WriteAsync(_repo.Lidos.ToString());
        }
        public async Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            await context.Response.WriteAsync(_repo.Lendo.ToString());
        }
    }
}
//context.Request.Path 
// Recebe o caminho que está sendo recebido através do browser localhost:5000/Lidos -> retorna /Lidos no corpo do browser