using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    class CpNodeTypeConverter : ObjectToStringConverter<string>
    {
        protected override void GetStandardValues(IList<string> values, System.ComponentModel.ITypeDescriptorContext context)
        {
            values.Add("0");
            values.Add("1");
            values.Add("2");
            values.Add("3");
        }

        protected override string ConvertObjectFromString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            string ret = string.Empty;

            switch (value)
            {
                case "入院日":
                    ret = "0";
                    break;
                case "平常日":
                    ret = "1";
                    break;
                case "手术日":
                    ret = "2";
                    break;
                case "出院日":
                    ret = "3";
                    break;
                default:
                    ret = "1";
                    break;

            }
            return ret;
        }

        protected override string ConvertToString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            string ret = string.Empty;

            switch (value.ToString())
            {
                case "0":
                    ret = "入院日";
                    break;
                case "1":
                    ret = "平常日";
                    break;
                case "2":
                    ret = "手术日";
                    break;
                case "3":
                    ret = "出院日";
                    break;
                default:
                    ret = "平常日";
                    break;
            }

            return ret;
        }

        protected override string GetDefaultSourceValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }

        protected override string GetDefaultDestinationValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return "平常日";
        }
    }
}
