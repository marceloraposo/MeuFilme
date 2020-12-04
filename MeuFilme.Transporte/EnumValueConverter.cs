using System;
using System.ComponentModel;
using System.Reflection;

namespace MeuFilme.Transporte
{
    public static class EnumValueConverter
    {
        #region Methods

        /// <summary>
        /// Metodo responsavel por encontrar o item de enum acordo com o grupo de valores e o valor
        /// </summary>
        /// <typeparam name="T">Tipo Generico</typeparam>
        /// <param name="value">Valor correspondente ao item de enum</param>
        /// <returns>Resultado</returns>               
        public static T Parse<T>(string value)
        {
            return (T)Parse(typeof(T), value);
        }

        /// <summary>
        /// Metodo responsavel por converter um item de enum em uma string de acordo com o grupo de valores
        /// informador
        /// </summary>
        /// <typeparam name="T">Tipo Generico</typeparam>
        /// <param name="enumerable">Item de enum a ser convertido</param>
        /// <returns>Resultado</returns>
        public static string ToString<T>(T enumerable)
        {
            return ToString((object)enumerable);
        }

        /// <summary>
        /// Verifica se um Tipo é Nullable
        /// </summary>
        /// <param name="type">Tipo</param>
        /// <returns>True caso seja. False caso não seja</returns>
        private static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        /// <summary>
        /// Metodo responsavel por encontrar o item de enum acordo com o grupo de valores e o valor
        /// </summary>
        /// <param name="enumerable">Tipo que deve ser um enum.</param>
        /// <param name="value">Valor correspondente ao item de enum</param>
        /// <returns>Resultado</returns>
        private static object Parse(Type enumerable, string value)
        {
            object[] attributes;
            Type enumType;

            // se for um enum 'nulavel'
            if (IsNullable(enumerable))
            {
                // Validando se Type é um Enum
                enumType = new NullableConverter(enumerable).UnderlyingType;
                if (!enumType.IsEnum)
                    throw new ArgumentException("The argument should be an enum", enumType.GetType().Name);
            }
            else
            {
                // Validando se Type é um Enum
                if (!enumerable.IsEnum)
                    throw new ArgumentException("The argument should be an enum", enumerable.GetType().Name);
                else
                    enumType = enumerable;
            }

            foreach (FieldInfo info in enumType.GetFields())
            {
                if (!info.IsSpecialName)
                {
                    attributes = info.GetCustomAttributes(typeof(EnumValueAttribute), true);
                    if (attributes != null && attributes.Length > 0)
                    {
                        EnumValueAttribute enumValueAttribute;
                        for (int i = 0; i < attributes.Length; i++)
                        {
                            enumValueAttribute = (EnumValueAttribute)attributes[i];
                            if (enumValueAttribute.Value == value)
                                return info.GetValue(null);
                        }
                    }
                    else
                    {
                        throw new InvalidCastException("The attribute EnumValueAttribute not found on the type " + enumerable.Name);
                    }
                }
            }

            // se nao encontrar o valor do item, e o retorno aceitar nulo, retornar null
            if (string.IsNullOrWhiteSpace(value) && IsNullable(enumerable))
                return null;

            //// nao foi encontrado um enum para o para group/value
            throw new InvalidCastException(string.Format("Unable to convert the value to an enum of type {0}", enumerable.Name));
        }

        /// <summary>
        /// Metodo responsavel por converter um item de enum em uma string de acordo com o grupo de valores
        /// informador
        /// </summary>
        /// <param name="enumerable">Item de enum a ser convertido</param>
        /// <returns>Resultado</returns>
        private static string ToString(object enumerable)
        {
            if ((object)enumerable == null)
                return null;

            Type t = enumerable.GetType();
            object[] attributes;
            foreach (FieldInfo info in t.GetFields())
            {
                // ignorar itens não declarados diretamente no enum
                if (!info.IsSpecialName)
                {
                    if (enumerable.ToString() == info.GetValue(null).ToString())
                    {
                        attributes = info.GetCustomAttributes(typeof(EnumValueAttribute), true);
                        if (attributes != null && attributes.Length > 0)
                        {
                            // procurar o valor default
                            return ((EnumValueAttribute)attributes[0]).Value;
                        }
                        else
                        {
                            throw new InvalidCastException("The attribute EnumMemberValueAttribute not found on the type " + t.Name);
                        }
                    }
                }
            }

            throw new InvalidCastException("Unable to find a value mapped to the enum");
        }

        #endregion Methods
    }
}
