using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace MeuFilme.Transporte
{
    public interface IValidable
    {
        #region Properties

        bool AutoRollback
        {
            get; set;
        }

        bool IsValid
        {
            get;
        }

        #endregion Properties
    }

    [Serializable]
    [DataContract]
    public class MeuFilmeResult<T> : IValidable
    {
        #region Constructors

        public MeuFilmeResult(T entity, List<ErrorMessage> propertyErrors)
            : this()
        {
            this.Value = entity;
            this.Errors = propertyErrors;
        }

        private MeuFilmeResult()
        {
            this.Value = default(T);
            this.Errors = new List<ErrorMessage>();
            this.SubErrors = new List<SubErrorMessage>();
            this.AutoRollback = true;
        }

        #endregion Constructors

        #region Properties

        public bool AutoRollback
        {
            get; set;
        }

        [DataMember(Name = "Errors")]
        public List<ErrorMessage> Errors
        {
            get;
            private set;
        }

        public bool IsValid
        {
            get
            {
                return this.Errors != null && !this.Errors.Any() && !this.SubErrors.Any();
            }
        }

        [DataMember(Name = "SubErrors")]
        public List<SubErrorMessage> SubErrors
        {
            get;
            private set;
        }

        [DataMember(Name="Value")]
        public T Value
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public static ErrorMessage ErrorMessageOracle(string errorMessage, int errorNumber)
        {
            string oracleException = string.Format("ORA-{0:00000}", Math.Abs(errorNumber));
            string propertyResourceKey = string.Empty;
            string oracleExceptionDescription = string.Empty;

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                if (errorMessage.Contains("UK_COD"))
                    propertyResourceKey = "UI.Codigo";
                else if (errorMessage.Contains("UK_NOME"))
                    propertyResourceKey = "UI.Nome";
                else if (errorMessage.Contains("UK_DESC"))
                    propertyResourceKey = "UI.Descricao";
                else if (errorMessage.Contains("UK_SIGLA"))
                    propertyResourceKey = "UI.Simbolo";
                else if (errorMessage.Contains("UK_EMAIL"))
                    propertyResourceKey = "UI.Email";
                else if (errorMessage.Contains("FK_IDPR"))
                    propertyResourceKey = "UI.IndiceProjecao";
                else if (errorMessage.Contains("FK_GRIN"))
                    propertyResourceKey = "UI.Agrupamento";
                else if (errorMessage.Contains("FK_GRPR"))
                    propertyResourceKey = "UI.GrupoPrograma";
                else if (errorMessage.Contains("FK_UMED"))
                    propertyResourceKey = "UI.UnidadeMedida";
                else if (errorMessage.Contains("FK_GRIN"))
                    propertyResourceKey = "UI.Agrupamento";
                else if (errorMessage.Contains("FK_COLA"))
                    propertyResourceKey = "UI.Colaborador";
                else if (errorMessage.Contains("UK_UNAC"))
                    propertyResourceKey = "UI.UA";

                else
                {
                    oracleExceptionDescription = errorMessage;
                }
            }

            return new ErrorMessage(propertyResourceKey, oracleException, oracleExceptionDescription);
        }

        public static MeuFilmeResult<T> Invalid(T entity, string messageResourceKey)
        {
            MeuFilmeResult<T> result = new MeuFilmeResult<T>
            {
                Value = entity
            };
            result.Errors.Add(new ErrorMessage(messageResourceKey));

            return result;
        }

        public static MeuFilmeResult<T> Invalid(T entity, string propertyResourceKey, string messageResourceKey)
        {
            MeuFilmeResult<T> result = new MeuFilmeResult<T>
            {
                Value = entity
            };
            result.Errors.Add(new ErrorMessage(propertyResourceKey, messageResourceKey));

            return result;
        }

        public static MeuFilmeResult<T> Invalid(T entity, string propertyResourceKey, string messageResourceKey, params object[] messageParams)
        {
            MeuFilmeResult<T> result = new MeuFilmeResult<T>
            {
                Value = entity
            };
            result.Errors.Add(new ErrorMessage(propertyResourceKey, messageResourceKey, messageParams));

            return result;
        }

        public static MeuFilmeResult<T> Invalid(T entity, List<ErrorMessage> propertyErrors)
        {
            MeuFilmeResult<T> result = new MeuFilmeResult<T>
            {
                Value = entity
            };
            result.Errors = propertyErrors;

            return result;
        }

        public static MeuFilmeResult<T> Invalid(T entity, List<SubErrorMessage> subErrors)
        {
            MeuFilmeResult<T> result = new MeuFilmeResult<T>
            {
                Value = entity
            };
            result.SubErrors = subErrors;

            return result;
        }

        public static MeuFilmeResult<T> InvalidOracle(T entity, string errorMessage, int errorNumber)
        {
            var error = ErrorMessageOracle(errorMessage, errorNumber);
            return MeuFilmeResult<T>.Invalid(entity, new List<ErrorMessage>() { error });
        }

        public static MeuFilmeResult<T> InvalidOracle(T entity, int errorNumber)
        {
            var error = ErrorMessageOracle(string.Empty, errorNumber);
            return MeuFilmeResult<T>.Invalid(entity, new List<ErrorMessage>() { error });
        }

        public static MeuFilmeResult<T> Valid(T validResult)
        {
            MeuFilmeResult<T> result = new MeuFilmeResult<T>
            {
                Value = validResult
            };

            return result;
        }

        public void AddError(string messageResourceKey)
        {
            if (Errors == null)
                Errors = new List<ErrorMessage>();

            this.Errors.Add(new ErrorMessage(messageResourceKey));
        }

        public void AddError(string propertyResourceKey, string messageResourceKey)
        {
            this.Errors.Add(new ErrorMessage(propertyResourceKey, messageResourceKey));
        }

        public void AddError(string propertyResourceKey, string messageResourceKey, params object[] messageParams)
        {
            this.Errors.Add(new ErrorMessage(propertyResourceKey, messageResourceKey, messageParams));
        }

        public void InsertError(int index, string messageResourceKey)
        {
            this.Errors.Insert(index, new ErrorMessage(messageResourceKey));
        }

        public void InsertError(int index, string propertyResourceKey, string messageResourceKey)
        {
            this.Errors.Insert(index, new ErrorMessage(propertyResourceKey, messageResourceKey));
        }

        public void InsertError(int index, string propertyResourceKey, string messageResourceKey, params object[] messageParams)
        {
            this.Errors.Insert(index, new ErrorMessage(propertyResourceKey, messageResourceKey, messageParams));
        }

        #endregion Methods
    }

    [Serializable]
    [DataContract]
    public class MeuFilmeResultCollection<T> : IValidable
        where T : RootDto
    {
        #region Constructors

        public MeuFilmeResultCollection()
        {
            this.Collection = new List<MeuFilmeResult<T>>();
            this.AutoRollback = true;
        }

        #endregion Constructors

        #region Properties

        public bool AutoRollback
        {
            get; set;
        }

        [DataMember]
        public List<MeuFilmeResult<T>> Collection
        {
            get; private set;
        }

        public bool IsValid
        {
            get
            {
                return !this.Collection.Exists(x => !x.IsValid);
            }
        }

        #endregion Properties

        #region Methods

        public MeuFilmeResultCollection<RootDto> ToBaseDto()
        {
            var result = new MeuFilmeResultCollection<RootDto>();

            foreach (var item in this.Collection)
                result.Collection.Add(MeuFilmeResult<RootDto>.Invalid(item.Value, item.Errors));

            return result;
        }

        public List<RootDto> ToDtoList()
        {
            return this.Collection.Select(x => x.Value).OfType<RootDto>().ToList();
        }

        public List<T> ToDtoList<T>()
        {
            return this.Collection.Select(x => x.Value).OfType<T>().ToList();
        }

        #endregion Methods
    }
}