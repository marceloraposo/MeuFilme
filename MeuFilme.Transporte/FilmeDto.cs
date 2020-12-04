using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MeuFilme.Transporte
{
    [Serializable]
    [DataContract]
    public class FilmeDto : RootDto
    {

        public FilmeDto()
        {
            Elenco = new List<ElencoDto>();
            Equipe = new List<EquipeDto>();
            Genero = new List<GeneroDto>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string NomeOriginal { get; set; }

        [DataMember]
        public string Resumo { get; set; }

        [DataMember]
        public double Popularidade { get; set; }

        [DataMember]
        public double VotosMedia { get; set; }

        [DataMember]
        public int VotosContagem { get; set; }

        [DataMember]
        public string Poster { get; set; }

        [DataMember]
        public string Idioma { get; set; }

        [DataMember]
        public DateTime? DataLancamento { get; set; }

        [DataMember]
        public List<ElencoDto> Elenco { get; set; }

        [DataMember]
        public List<EquipeDto> Equipe { get; set; }

        [DataMember]
        public List<GeneroDto> Genero { get; set; }
    }
}
