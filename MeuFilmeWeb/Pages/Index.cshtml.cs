using MeuFilmeWeb.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MeuFilmeWeb.Pages
{
    public class IndexModel : PageModel
    {
        public List<FilmeModel> filmes { get; set; }
        public string pesquisa { get; set; }


        public IndexModel()
        {
            filmes = new List<FilmeModel>();
        }

        public void OnGet(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                pesquisa = texto;
                filmes = PesquisarFilme(texto);
            }
        }

        public void OnPostSubmit(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                pesquisa = texto;
                filmes = PesquisarFilme(texto);
            }
        }

        public static List<FilmeModel> PesquisarFilme(string texto)
        {
            List<FilmeModel> listaFilme = new List<FilmeModel>();
            HttpWebRequest requisicaoWeb = WebRequest.CreateHttp(string.Format("{0}/{1}", "https://meufilmegateway20201204104342.azurewebsites.net/filme/p", texto));
            requisicaoWeb.Method = "GET";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                Stream streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                string objResponse = reader.ReadToEnd();
                foreach (Newtonsoft.Json.Linq.JToken item in ((Newtonsoft.Json.Linq.JObject)(JsonConvert.DeserializeObject<object>(objResponse)))["collection"])
                {
                    listaFilme.Add(JsonConvert.DeserializeObject<FilmeModel>(item["Value"].ToString()));
                }

                streamDados.Close();
                resposta.Close();
            }
            return listaFilme;
        }
    }
}
