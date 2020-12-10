using Microsoft.AspNetCore.Mvc;

namespace MeuFilmeWeb.Model
{
    public class GeneroModel
    {
        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Nome { get; set; }
    }
}
