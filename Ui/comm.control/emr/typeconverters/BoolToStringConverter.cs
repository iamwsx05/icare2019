using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Common.Controls.Emr
{
    public class BoolToStringConverter : ObjectToStringConverter<Boolean>
    {
        protected override void GetStandardValues(IList<bool> values, System.ComponentModel.ITypeDescriptorContext context)
        {
            values.Add(true);
            values.Add(false);
        }

        protected override bool ConvertObjectFromString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            switch (value)
            {
                case "是":
                    return true;

                case "否":
                    return false;

                default:
                    return GetDefaultSourceValue(context, culture);
            }
        }

        protected override string ConvertToString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, bool value)
        {
            switch (value)
            {
                case true:
                    return "是";

                case false:
                    return "否";

                default:
                    return GetDefaultDestinationValue(context, culture);
            }
        }

        protected override bool GetDefaultSourceValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return false;
        }

        protected override string GetDefaultDestinationValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return "是";
        }
    }
}
