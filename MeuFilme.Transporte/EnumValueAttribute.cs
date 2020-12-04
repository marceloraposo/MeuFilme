using System;
using System.Runtime.Serialization;

namespace MeuFilme.Transporte
{
    [Serializable]
    [DataContract]
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumValueAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Contrutor Default
        /// </summary>
        /// <param name="value"></param>
        public EnumValueAttribute(string value)
        {
            this.Value = value;
        }

        public EnumValueAttribute(string value, string text)
        {
            this.Value = value;
            this.Text = text;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Valor do Item do Enum
        /// </summary>
        [DataMember]
        public string Value
        {
            get; private set;
        }

        [DataMember]
        public string Text
        {
            get; set;
        }

        #endregion Properties
    }
}
