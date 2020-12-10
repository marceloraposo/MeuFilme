using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MeuFilmeWeb.Model
{
    public class FilmeModel
    {

        public FilmeModel()
        {
            Elenco = new List<ElencoModel>();
            Equipe = new List<EquipeModel>();
            Genero = new List<GeneroModel>();
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Nome { get; set; }

        [BindProperty]
        public string NomeOriginal { get; set; }

        [BindProperty]
        public string Resumo { get; set; }

        [BindProperty]
        public double Popularidade { get; set; }

        [BindProperty]
        public double VotosMedia { get; set; }

        [BindProperty]
        public int VotosContagem { get; set; }

        [BindProperty]
        public string Poster { get; set; }

        [BindProperty]
        public string Idioma { get; set; }

        [BindProperty]
        public DateTime? DataLancamento { get; set; }

        [BindProperty]
        public List<ElencoModel> Elenco { get; set; }

        [BindProperty]
        public List<EquipeModel> Equipe { get; set; }

        [BindProperty]
        public List<GeneroModel> Genero { get; set; }

        public string NomeIdioma
        {
            get
            {
                return new CultureInfo(Idioma).DisplayName;
            }

        }
    }
}
