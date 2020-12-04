using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MeuFilme.Transporte
{
    [DataContract]
    [Serializable]
    public class ErrorMessage
    {
        #region Constructors

        public ErrorMessage(string messageResourceKey)
        {
            this.MessageResourceKey = messageResourceKey;
        }

        public ErrorMessage(string propertyResourceKey, string messageResourceKey)
        {
            this.PropertyResourceKey = propertyResourceKey;
            this.MessageResourceKey = messageResourceKey;
        }

        public ErrorMessage(string propertyResourceKey, string messageResourceKey, params object[] messageParams)
        {
            this.PropertyResourceKey = propertyResourceKey;
            this.MessageResourceKey = messageResourceKey;
            this.MessageParams = messageParams.ToList();
        }

        #endregion Constructors

        #region Properties

        [DataMember]
        public List<object> MessageParams
        {
            get; set;
        }

        [DataMember]
        public string MessageResourceKey
        {
            get; set;
        }

        [DataMember]
        public string PropertyResourceKey
        {
            get; set;
        }

        #endregion Properties
    }
}