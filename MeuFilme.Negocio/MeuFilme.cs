using MeuFilme.Interface;
using MeuFilme.Transporte;
using System;
using System.Reflection;

namespace MeuFilme.Negocio
{
    public partial class MeuFilme : IMeuFilme, IDisposable
    {
        public MeuFilmeResult<string> GetNumeroVersao()
        {
            return MeuFilmeResult<string>.Valid(string.Format("{0}", Assembly.GetExecutingAssembly().GetName().Version));
        }

        public void Dispose()
        {
            this.Dispose();
        }

    }
}
