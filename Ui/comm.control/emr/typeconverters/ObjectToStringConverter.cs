using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;

namespace Common.Controls.Emr
{
    public abstract class ObjectToStringConverter<T> : System.ComponentModel.TypeConverter
    {
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<T> list = new List<T>();
            GetStandardValues(list, context);
            return new StandardValuesCollection(list.ToArray());
        }

        protected abstract void GetStandardValues(IList<T> values, ITypeDescriptorContext context);

        protected abstract T ConvertObjectFromString(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value);

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            Debug.WriteLine(value);
            if (value.GetType() == typeof(string))
            {
                return ConvertObjectFromString(context, culture, value.ToString());
            }
            else
            {
                return GetDefaultSourceValue(context, culture);
            }
        }

        protected abstract string ConvertToString(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, T value);

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value != null && value != DBNull.Value && value is T)
            {
                return ConvertToString(context, culture, (T)value);
            }
            else
            {
                return GetDefaultDestinationValue(context, culture);
            }
        }

        protected abstract T GetDefaultSourceValue(ITypeDescriptorContext context, System.Globalization.CultureInfo culture);
        protected abstract string GetDefaultDestinationValue(ITypeDescriptorContext context, System.Globalization.CultureInfo culture);

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
