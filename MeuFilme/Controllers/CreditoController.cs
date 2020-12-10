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
    public class CreditoController : ControllerBase
    {
        private MeuFilme.Negocio.MeuFilme meuFilme;
        public CreditoController()
        {
            meuFilme = new MeuFilme.Negocio.MeuFilme();
        }

        [HttpGet("ObterCredito/{creditoId}", Name = "ObterCredito")]
        public MeuFilmeResult<CreditoDto> ObterCredito(string creditoId)
        {
            return meuFilme.ObterCredito(creditoId);
        }
    }
}