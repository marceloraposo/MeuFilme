using MeuFilme.Transporte;
using System.ServiceModel;

namespace MeuFilme.Interface
{
    [ServiceKnownType(typeof(CreditoDto))]
    public partial interface IMeuFilme
    {
        [OperationContract]
        MeuFilmeResult<CreditoDto> ObterCredito(string creditoId);
    }
}
