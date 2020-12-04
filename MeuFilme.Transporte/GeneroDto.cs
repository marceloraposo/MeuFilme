using System;
using System.Runtime.Serialization;

namespace MeuFilme.Transporte
{
    [Serializable]
    [DataContract]
    public class GeneroDto : RootDto
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }
    }
}
