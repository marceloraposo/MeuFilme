using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MeuFilme.Transporte
{
    [Serializable]
    [DataContract]
    public class CreditoDto : RootDto
    {

        [DataMember]
        public string CreditoId { get; set; }

        [DataMember]
        public string Foto { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Cargo { get; set; }

        [DataMember]
        public string Personagem { get; set; }

        [DataMember]
        public string Departamento { get; set; }

    }
}
