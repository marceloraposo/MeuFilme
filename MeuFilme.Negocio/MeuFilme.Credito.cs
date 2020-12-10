using MeuFilme.Interface;
using MeuFilme.Negocio.Data;
using MeuFilme.Transporte;
using System;
using TMDbLib.Objects.Credit;

namespace MeuFilme.Negocio
{
    public partial class MeuFilme : IMeuFilme
    {
        public MeuFilmeResult<CreditoDto> ObterCredito(string creditoId)
        {
            CreditoDto creditoDto = new CreditoDto();

            if (string.IsNullOrWhiteSpace(creditoId))
            {
                return MeuFilmeResult<CreditoDto>.Invalid(new CreditoDto(), "Parâmetro vazio");
            }
            try
            {
                Credit resultado = CreditoData.OtberCredito(creditoId);

                if (resultado != null)
                {
                    creditoDto = CarregarCredito(resultado);
                }
                else
                {
                    return MeuFilmeResult<CreditoDto>.Invalid(creditoDto, "Crédito não encontrado");
                }
            }
            catch (Exception e)
            {
                return MeuFilmeResult<CreditoDto>.Invalid(creditoDto, e.Message);
            }

            return MeuFilmeResult<CreditoDto>.Valid(creditoDto);
        }

        private EquipeDto CarregarCredito(Credit item)
        {
            EquipeDto equipeDto = null;

            if (item != null)
                equipeDto = new EquipeDto { CreditoId = item.Id, Nome = item.Person.Name, Cargo = item.Job, Departamento = item.Department };

            return equipeDto;
        }
    }
}
