using MeuFilme.Transporte;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MeuFilme.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("MeuFilme")]
    public class FilmeController : ControllerBase
    {
        private MeuFilme.Negocio.MeuFilme meuFilme;
        public FilmeController()
        {
            meuFilme = new MeuFilme.Negocio.MeuFilme();
        }

        [HttpGet("ObterFilme/{filmeId}",Name = "ObterFilme")]
        public MeuFilmeResult<FilmeDto> ObterFilme(string filmeId)
        {
            return meuFilme.ObterFilme(filmeId);
        }

        [HttpGet("PesquisarFilme/{nomeDoFilme}", Name = "PesquisarFilme")]
        public MeuFilmeResultCollection<FilmeDto> PesquisarFilme(string nomeDoFilme)
        {
            return meuFilme.PesquisarFilme(nomeDoFilme);
        }
    }
}