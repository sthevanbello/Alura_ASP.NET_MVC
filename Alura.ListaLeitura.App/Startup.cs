using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {

        public void Configure(IApplicationBuilder app)
        {
            app.Run(LivrosParaLer);
        }

        public Task LivrosParaLer(HttpContext context)
        {
                        

            var _repo = new LivroRepositorioCSV();
            
            var repoString = _repo.ParaLer.ToString();

            return context.Response.WriteAsync(repoString);

        }
    }
}