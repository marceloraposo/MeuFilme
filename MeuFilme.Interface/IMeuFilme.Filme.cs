using MeuFilme.Transporte;
using System.ServiceModel;

namespace MeuFilme.Interface
{
    [ServiceKnownType(typeof(FilmeDto))]
    [ServiceKnownType(typeof(EquipeDto))]
    [ServiceKnownType(typeof(ElencoDto))]
    public partial interface IMeuFilme
    {
        [OperationContract]
        MeuFilmeResultCollection<FilmeDto> PesquisarFilme(string pesquisa);

        [OperationContract]
        MeuFilmeResult<FilmeDto> ObterFilme(string filmeId);
    }
}
