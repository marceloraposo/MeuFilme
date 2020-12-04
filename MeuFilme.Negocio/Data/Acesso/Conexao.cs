using Microsoft.Extensions.Configuration;
using System.IO;

namespace MeuFilme.Negocio.Data.Acesso
{
    public class Conexao
    {
        #region atributos
        public static string APIKEY_TMDBLIB
        {
            get
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", true)
                  .Build();

                return config["Chave:apiKey"];
            }

        }
        

        #endregion

        #region metodos


        #endregion
    }
}
