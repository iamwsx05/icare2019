using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    class ItemTypeConverter : ObjectToStringConverter<string>
    {
        protected override void GetStandardValues(IList<string> values, System.ComponentModel.ITypeDescriptorContext context)
        {
            values.Add("0");
            values.Add("1");
            values.Add("2");
        }

        protected override string ConvertObjectFromString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            string ret = string.Empty;

            switch (value)
            {
                case "":
                    ret = "0";
                    break;
                case "XML节点":
                    ret = "1";
                    break;
                case "XML合计节点":
                    ret = "2";
                    break;
                default:
                    ret = "0";
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
                    ret = "";
                    break;
                case "1":
                    ret = "XML节点";
                    break;
                case "2":
                    ret = "XML合计节点";
                    break;
                default:
                    ret = "";
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
            return "";
        }
    }
}
