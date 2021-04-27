using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {

        public void Configure(IApplicationBuilder app)
        {
            

            app.Run(Roteamento);
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