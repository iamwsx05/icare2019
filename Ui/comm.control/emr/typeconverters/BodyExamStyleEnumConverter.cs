using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Common.Controls.Emr
{
    class BodyExamStyleEnumConverter : ObjectToStringConverter<enumBodyExamStyle>
    {
        protected override void GetStandardValues(IList<enumBodyExamStyle> values, System.ComponentModel.ITypeDescriptorContext context)
        {
            values.Add(enumBodyExamStyle.BC);
            values.Add(enumBodyExamStyle.CT);
            values.Add(enumBodyExamStyle.X);            
        }

        protected override enumBodyExamStyle ConvertObjectFromString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            if (value == "B超")
            {
                return enumBodyExamStyle.BC;
            }
            else if (value == "CT")
            {
                return enumBodyExamStyle.CT;
            }
            else if (value == "X光")
            {
                return enumBodyExamStyle.X;
            }
            else
            {
                return GetDefaultSourceValue(context, culture);
            }
        }

        protected override string ConvertToString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, enumBodyExamStyle value)
        {
            if (value == enumBodyExamStyle.BC)
            {
                return "B超";
            }
            else if (value == enumBodyExamStyle.CT)
            {
                return "CT";
            }
            else if (value == enumBodyExamStyle.X)
            {
                return "X光";
            }
            else
            {
                return GetDefaultDestinationValue(context, culture);
            }
        }

        protected override enumBodyExamStyle GetDefaultSourceValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return enumBodyExamStyle.X;
        }

        protected override string GetDefaultDestinationValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return "X光";
        }
    }
}
