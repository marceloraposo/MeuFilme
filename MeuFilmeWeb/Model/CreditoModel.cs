using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuFilmeWeb.Model
{
    public class CreditoModel
    {
        [BindProperty]
        public string CreditoId { get; set; }

        [BindProperty]
        public string Foto { get; set; }

        [BindProperty]
        public string Nome { get; set; }

        [BindProperty]
        public string Cargo { get; set; }

        [BindProperty]
        public string Personagem { get; set; }

        [BindProperty]
        public string Departamento { get; set; }
    }
}
