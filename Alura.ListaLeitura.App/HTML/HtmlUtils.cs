using System.IO;

namespace Alura.ListaLeitura.App.HTML
{
    public class HtmlUtils
    {
        public static string CarregaHTML(string NomeArquivo)
        {
            var nomeCompletoArquivo = $"HTML/{NomeArquivo}.html";
            using (var arquivo = File.OpenText(nomeCompletoArquivo))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}
