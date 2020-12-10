using MeuFilmeWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace MeuFilmeWeb.Pages
{
    public class InformacoesFilmeModel : PageModel
    {

        public FilmeModel filme { get; private set; }
        public List<FilmeModel> filmesRecomendados { get; private set; }

        public string pesquisa { get; set; }

        public InformacoesFilmeModel()
        {

        }

        public void OnGet(int id, string texto)
        {
            CarregarFilme(id);
            if (!string.IsNullOrEmpty(texto))
            {
                pesquisa = texto;
                var recomendados = IndexModel.PesquisarFilme(texto);
                filmesRecomendados = recomendados.Count >= 3 ? recomendados.Where(c => c.Id != id && c.VotosContagem > 0).OrderByDescending(x => x.VotosMedia).Take(3).ToList() : recomendados;
            }
        }

        private void CarregarFilme(int id)
        {
            HttpWebRequest requisicaoWeb = WebRequest.CreateHttp(string.Format("{0}/{1}", "https://meufilmegateway20201204104342.azurewebsites.net/filme/o", id));
            requisicaoWeb.Method = "GET";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                Stream streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                string objResponse = reader.ReadToEnd();

                filme = JsonConvert.DeserializeObject<FilmeModel>(((Newtonsoft.Json.Linq.JObject)(JsonConvert.DeserializeObject<object>(objResponse)))["Value"].ToString());

                streamDados.Close();
                resposta.Close();
            }
        }  

        public RedirectToPageResult OnPostSubmit(string texto)
        {
            return RedirectToPage("Index", new { texto = texto });
        }
    }
}
