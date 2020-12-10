using MeuFilme.Negocio.Data.Acesso;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace MeuFilme.Negocio.Data
{
    public class FilmeData
    {
        /// <summary>
        /// pesquisar filme por nome
        /// </summary>
        /// <param name="pesquisa"></param>
        /// <returns></returns>
        public static SearchContainer<SearchMovie> PesquisarFilme(string pesquisa)
        {
            return new TMDbClient(Conexao.APIKEY_TMDBLIB).SearchMovieAsync(pesquisa, "br").Result;
        }

        /// <summary>
        /// obter equipe do filme
        /// </summary>
        /// <param name="filmeId"></param>
        /// <returns></returns>
        public static Credits ObterEquipePorFilme(int filmeId)
        {
            return new TMDbClient(Conexao.APIKEY_TMDBLIB).GetMovieCreditsAsync(filmeId).Result;
        }

        public static Movie ObterFilme(string filmeId)
        {
            return new TMDbClient(Conexao.APIKEY_TMDBLIB).GetMovieAsync(filmeId,"br").Result;
        }
    }
}
