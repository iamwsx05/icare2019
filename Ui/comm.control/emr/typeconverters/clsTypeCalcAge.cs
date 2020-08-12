using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    public class clsTypeCalcAge : ObjectToStringConverter<int>
    {
        protected override void GetStandardValues(IList<int> values, System.ComponentModel.ITypeDescriptorContext context)
        {
            values.Add(0);
            values.Add(1);
        }

        protected override int ConvertObjectFromString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            int ret = 0;

            switch (value)
            {
                case "按当前时间计算":
                    ret = 0;
                    break;
                case "按入院时间计算":
                    ret = 1;
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
                    ret = "按当前时间计算";
                    break;
                case 1:
                    ret = "按入院时间计算";
                    break;
                default:
                    ret = "按入院时间计算";
                    break;
            }
            return ret;
        }

        protected override int GetDefaultSourceValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return 1;
        }

        protected override string GetDefaultDestinationValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return "按入院时间计算";
        }
    }
}
