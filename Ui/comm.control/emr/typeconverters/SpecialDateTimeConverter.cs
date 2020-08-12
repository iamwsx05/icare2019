using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    class SpecialDateTimeConverter : ObjectToStringConverter<string>
    {
        protected override void GetStandardValues(IList<string> values, System.ComponentModel.ITypeDescriptorContext context)
        {
            values.Add("");
            values.Add("DateTime.Now");
        }

        protected override string ConvertObjectFromString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            if (value == "无")
            {
                return string.Empty;
            }
            else if (value == "今天")
            {
                return "DateTime.Now";
            }
            else
            {
                return GetDefaultSourceValue(context, culture);
            }
        }

        protected override string ConvertToString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            if (value == "")
            {
                return "无";
            }
            else if (value == "DateTime.Now")
            {
                return "今天";
            }
            else
            {
                return GetDefaultDestinationValue(context, culture);
            }
        }

        protected override string GetDefaultSourceValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }

        protected override string GetDefaultDestinationValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return "无";
        }
    }
}
