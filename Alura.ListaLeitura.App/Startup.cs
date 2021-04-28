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

            builder.MapRoute("{classe}/{metodo}", RoteamentoPadrao.TratamentoPadrao);
            
            //Constrói a rota completa através do método Build()
            var rotas = builder.Build();

            app.UseRouter(rotas);
        }

    }
}
//context.Request.Path 
// Recebe o caminho que está sendo recebido através do browser localhost:5000/Lidos -> retorna /Lidos no corpo do browser