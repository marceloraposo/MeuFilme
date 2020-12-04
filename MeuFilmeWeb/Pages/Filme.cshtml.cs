using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MeuFilmeWeb.Pages
{
    public class FilmeModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        private readonly ILogger<FilmeModel> _logger;

        public FilmeModel(ILogger<FilmeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int id)
        {
            Id = id;
        }
    }
}
