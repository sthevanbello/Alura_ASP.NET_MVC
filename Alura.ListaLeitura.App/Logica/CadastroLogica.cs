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
    public static class CadastroLogica
    {
        
        public static Task Incluir(HttpContext context)
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



        public static Task NovoLivro(HttpContext context)
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

        public static async Task ExibeFormulario(HttpContext context)
        {
            var html = HtmlUtils.CarregaHTML("Formulario");
            await context.Response.WriteAsync(html);
        }
    }
}
