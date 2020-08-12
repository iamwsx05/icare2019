using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Common.Controls.Emr
{
    class LineStyleEnumConverter : ObjectToStringConverter<CtlLineStyle>
    {
        protected override void GetStandardValues(IList<CtlLineStyle> values, System.ComponentModel.ITypeDescriptorContext context)
        {
            values.Add(CtlLineStyle.Dash);
            values.Add(CtlLineStyle.Solid);
        }

        protected override CtlLineStyle ConvertObjectFromString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            if (value == "虚线")
            {
                return CtlLineStyle.Dash;
            }
            else if (value == "实线")
            {
                return CtlLineStyle.Solid;
            }
            else
            {
                return GetDefaultSourceValue(context, culture);
            }
        }

        protected override string ConvertToString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, CtlLineStyle value)
        {
            if (value == CtlLineStyle.Dash)
            {
                return "虚线";
            }
            else if (value == CtlLineStyle.Solid)
            {
                return "实线";
            }
            else
            {
                return GetDefaultDestinationValue(context, culture);
            }
        }

        protected override CtlLineStyle GetDefaultSourceValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return CtlLineStyle.Dash;
        }

        protected override string GetDefaultDestinationValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return "虚线";
        }
    }
}
