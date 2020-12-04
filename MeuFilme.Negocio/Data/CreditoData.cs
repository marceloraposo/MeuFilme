using MeuFilme.Negocio.Data.Acesso;
using TMDbLib.Client;
using TMDbLib.Objects.Credit;
using TMDbLib.Objects.People;

namespace MeuFilme.Negocio.Data
{
    public class CreditoData
    {
        /// <summary>
        /// obter as informações de credito
        /// </summary>
        /// <param name="creditoId"></param>
        /// <returns></returns>
        public static Credit OtberCredito(string creditoId)
        {
            return new TMDbClient(Conexao.APIKEY_TMDBLIB).GetCreditsAsync(creditoId, "pt").Result;
        }


        public static Person OtberPessoa(int pessoaId)
        {
            return new TMDbClient(Conexao.APIKEY_TMDBLIB).GetPersonAsync(pessoaId, "pt").Result;
        }
    }
}
