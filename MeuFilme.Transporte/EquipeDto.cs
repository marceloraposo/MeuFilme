using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MeuFilme.Transporte
{
    [Serializable]
    [DataContract]
    public class EquipeDto : CreditoDto
    {

        public EquipeDto()
        {
            Filmes = new List<FilmeDto>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public List<FilmeDto> Filmes { get; set; }
    }
}
