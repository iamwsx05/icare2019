using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    public class clsMaskTypeConverter : ObjectToStringConverter<int>
    {
        protected override void GetStandardValues(IList<int> values, System.ComponentModel.ITypeDescriptorContext context)
        {
            values.Add(0);
            values.Add(1);
            values.Add(2);
        }

        protected override int ConvertObjectFromString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            int ret = 0;

            switch (value)
            {
                case "(无)":
                    ret = 0;
                    break;
                case "数字":
                    ret = 1;
                    break;
                case "日期":
                    ret = 2;
                    break;
                default:
                    ret = 0;
                    break;
            }
            return ret;
        }

        protected override string ConvertToString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, int value)
        {
            string ret = string.Empty;

            switch (value)
            {
                case 0:
                    ret = "(无)";
                    break;
                case 1:
                    ret = "数字";
                    break;
                case 2:
                    ret = "日期";
                    break;
                default:
                    ret = "(无)";
                    break;
            }
            return ret;
        }

        protected override int GetDefaultSourceValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return 0;
        }

        protected override string GetDefaultDestinationValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return "(无)";
        }
    }
}
