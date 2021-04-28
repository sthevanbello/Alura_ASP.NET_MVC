using Alura.ListaLeitura.App.Logica;
using Alura.ListaLeitura.App.MVC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

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
            var builder = new RouteBuilder(app);

            //Mapeando as rotas

            //builder.MapRoute("{classe}/{metodo}", RoteamentoPadrao.TratamentoPadrao);
            builder.MapRoute("Livros/ParaLer", LivrosLogica.ParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLogica.Lendo);
            builder.MapRoute("Livros/Lidos", LivrosLogica.Lidos);
            builder.MapRoute("Livros/Detalhes/{id:int}", LivrosLogica.Detalhes);
            builder.MapRoute("Cadastro/NovoLivro/{Titulo}/{Autor}", CadastroLogica.NovoLivro);
            builder.MapRoute("Cadastro/ExibeFormulario", CadastroLogica.ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", CadastroLogica.Incluir);

            //Constrói a rota completa através do método Build()
            var rotas = builder.Build();

            app.UseRouter(rotas);
        }

    }
}
//context.Request.Path 
// Recebe o caminho que está sendo recebido através do browser localhost:5000/Lidos -> retorna /Lidos no corpo do browser