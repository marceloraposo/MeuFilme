using MeuFilme.Interface;
using MeuFilme.Negocio.Data;
using MeuFilme.Transporte;
using System;
using System.Collections.Generic;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace MeuFilme.Negocio
{
    public partial class MeuFilme : IMeuFilme
    {
        public MeuFilmeResultCollection<FilmeDto> PesquisarFilme(string pesquisa)
        {
            FilmeDto filmeDto = new FilmeDto();
            var list = new MeuFilmeResultCollection<FilmeDto>();

            if (string.IsNullOrWhiteSpace(pesquisa))
            {
                list.Collection.Add(MeuFilmeResult<FilmeDto>.Invalid(filmeDto, "Parâmetro vazio"));
                return list;
            }
            try
            {

                SearchContainer<SearchMovie> resultado = FilmeData.PesquisarFilme(pesquisa);

                if (resultado != null)
                {
                    foreach (SearchMovie result in resultado.Results)
                    {

                        filmeDto = CarregarFilme(result);
                        Credits resultadoCredito = FilmeData.ObterEquipePorFilme(result.Id);
                        filmeDto.Elenco = CarregarListaElenco(resultadoCredito.Cast);
                        filmeDto.Equipe = CarregarListaEquipe(resultadoCredito.Crew);

                        list.Collection.Add(MeuFilmeResult<FilmeDto>.Valid(filmeDto));

                    }
                }
                else
                {
                    list.Collection.Add(MeuFilmeResult<FilmeDto>.Invalid(filmeDto, "Filme não encontrado"));
                }
            }
            catch (Exception e)
            {
                list.Collection.Add(MeuFilmeResult<FilmeDto>.Invalid(filmeDto, e.Message));
            }
            return list;
        }

        private FilmeDto CarregarFilme(SearchMovie result)
        {
            FilmeDto filmeDto = new FilmeDto();

            filmeDto.Id = result.Id;
            filmeDto.Nome = result.Title;
            filmeDto.NomeOriginal = result.OriginalTitle;
            filmeDto.DataLancamento = result.ReleaseDate;
            filmeDto.Resumo = result.Overview;
            filmeDto.Idioma = result.OriginalLanguage;
            filmeDto.Poster = result.PosterPath;
            filmeDto.Popularidade = result.Popularity;
            filmeDto.VotosMedia = result.VoteAverage;
            filmeDto.VotosContagem = result.VoteCount;

            return filmeDto;
        }

        private List<ElencoDto> CarregarListaElenco(List<Cast> result)
        {
            List<ElencoDto> list = new List<ElencoDto>();

            foreach (var item in result)
            {
                list.Add(CarregarElenco(item));
            }

            return list;
        }

        private ElencoDto CarregarElenco(Cast item)
        {
            ElencoDto elencoDto = null;

            if (item != null)
                elencoDto = new ElencoDto { CreditoId = item.CreditId, Nome = item.Name, Personagem = item.Character, Foto = item.ProfilePath };

            return elencoDto;
        }

        private List<EquipeDto> CarregarListaEquipe(List<Crew> result)
        {
            List<EquipeDto> list = new List<EquipeDto>();

            foreach (var item in result)
            {
                list.Add(CarregarEquipe(item));
            }

            return list;
        }

        private EquipeDto CarregarEquipe(Crew item)
        {
            EquipeDto equipeDto = null;

            if (item != null)
                equipeDto = new EquipeDto { CreditoId = item.CreditId, Nome = item.Name, Cargo = item.Job, Departamento = item.Department, Foto = item.ProfilePath };

            return equipeDto;
        }

        private List<GeneroDto> CarregarListaGenero(List<Genre> result)
        {
            List<GeneroDto> list = new List<GeneroDto>();

            foreach (var item in result)
            {
                list.Add(CarregarGenero(item));
            }

            return list;
        }

        private GeneroDto CarregarGenero(Genre item)
        {
            GeneroDto generoDto = null;

            if (item != null)
                generoDto = new GeneroDto { Id = item.Id, Nome = item.Name};

            return generoDto;
        }

        public MeuFilmeResult<FilmeDto> ObterFilme(string filmeId)
        {
            FilmeDto filmeDto = new FilmeDto();
            try
            {
                Movie result = FilmeData.ObterFilme(filmeId);

                filmeDto = CarregarFilme(result);
                Credits credits = FilmeData.ObterEquipePorFilme(Convert.ToInt32(filmeId));

                if (credits != null)
                    filmeDto.Elenco = credits == null ? new List<ElencoDto>() : CarregarListaElenco(credits.Cast);
                
                if (credits != null)
                    filmeDto.Equipe = credits == null ? new List<EquipeDto>() : CarregarListaEquipe(credits.Crew);

                if(result.Genres != null)
                    filmeDto.Genero = CarregarListaGenero(result.Genres);

                return MeuFilmeResult<FilmeDto>.Valid(filmeDto);
            }
            catch (Exception e)
            {
                return MeuFilmeResult<FilmeDto>.Invalid(filmeDto, e.Message);
            }
        }

        private FilmeDto CarregarFilme(Movie result)
        {
            FilmeDto filmeDto = new FilmeDto();

            filmeDto.Id = result.Id;
            filmeDto.Nome = result.Title;
            filmeDto.NomeOriginal = result.OriginalTitle;
            filmeDto.DataLancamento = result.ReleaseDate;
            filmeDto.Resumo = result.Overview;
            filmeDto.Idioma = result.OriginalLanguage;
            filmeDto.Poster = result.PosterPath;
            filmeDto.Popularidade = result.Popularity;
            filmeDto.VotosMedia = result.VoteAverage;
            filmeDto.VotosContagem = result.VoteCount;

            return filmeDto;
        }
    }
}
