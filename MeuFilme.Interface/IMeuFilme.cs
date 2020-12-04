using MeuFilme.Transporte;
using System.ServiceModel;

namespace MeuFilme.Interface
{
    [MeuFilmeContractBehaviorAttribute]
    [ServiceContract(SessionMode = SessionMode.Required)]
    [ServiceKnownType(typeof(MeuFilme.Transporte.EnumValueAttribute))]
    public partial interface IMeuFilme
    {
        [OperationContract]
        MeuFilmeResult<string> GetNumeroVersao();
    }
}
