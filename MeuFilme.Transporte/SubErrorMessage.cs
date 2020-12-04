using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace MeuFilme.Transporte
{
    [DataContract]
    [Serializable]
    public class SubErrorMessage
    {
        #region Constructors

        public SubErrorMessage(string itemCaption, List<ErrorMessage> errors)
            : this(itemCaption, errors, new List<SubErrorMessage>())
        {
        }

        public SubErrorMessage(string itemCaption, List<ErrorMessage> errors, List<SubErrorMessage> subErrors)
        {
            this.ItemCaption = itemCaption;
            this.Errors = errors;
            this.SubErrors = new List<SubErrorMessage>();
        }

        public SubErrorMessage(IItemCaption item, List<ErrorMessage> errors)
            : this(item, errors, new List<SubErrorMessage>())
        {
        }

        public SubErrorMessage(IItemCaption item, List<ErrorMessage> errors, List<SubErrorMessage> subErrors)
        {
            this.ItemCaption = item.ItemCaption;
            this.Errors = errors;
            this.SubErrors = subErrors;
        }

        #endregion Constructors

        #region Properties

        [DataMember]
        public List<ErrorMessage> Errors
        {
            get;
            private set;
        }

        [DataMember]
        public string ItemCaption
        {
            get; set;
        }

        [DataMember]
        public List<SubErrorMessage> SubErrors
        {
            get;
            private set;
        }

        #endregion Properties
    }
}