using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MeuFilmeWeb.Model
{
    public class EquipeModel : CreditoModel
    {
        public EquipeModel()
        {
            Filmes = new List<FilmeModel>();
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public List<FilmeModel> Filmes { get; set; }
    }
}
