using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public static class LivrosLogica
    {

        public static Task Detalhes(HttpContext context)
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
        private static string CarregaLista(IEnumerable<Livro> livros)
        {
            var html = HtmlUtils.CarregaHTML("lista");

            foreach (var item in livros)
            {
                html = html.Replace("#substituir#", $"<li>{item.Titulo} - {item.Autor}</li>#substituir#");
            }
            html = html.Replace("#substituir#", "");
            return html;
        }
        

        public static async Task ParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var html = CarregaLista(_repo.ParaLer.Livros);
            await context.Response.WriteAsync(html);
        }

        
        public static async Task Lidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var html = CarregaLista(_repo.Lidos.Livros);
            await context.Response.WriteAsync(html);
        }
        public static async Task Lendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var html = CarregaLista(_repo.Lendo.Livros);
            await context.Response.WriteAsync(html);
        }

    }
}
