using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    class PresentationModeConverter : ObjectToStringConverter<int>
    {
        protected override void GetStandardValues(IList<int> values, System.ComponentModel.ITypeDescriptorContext context)
        {
            values.Add(0);
            values.Add(1);
            values.Add(2);
            values.Add(3);
            values.Add(4);
        }

        protected override int ConvertObjectFromString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string value)
        {
            int intRet = 0;

            switch (value)
            {
                case "3D":
                    intRet = 0;
                    break;
                case "虚线":
                    intRet = 1;
                    break;
                case "(无)":
                    intRet = 2;
                    break;
                case "实线":
                    intRet = 3;
                    break;
                case "边框":
                    intRet = 4;
                    break;
                default:
                    intRet = GetDefaultSourceValue(context, culture);
                    break;
            }

            return intRet;
        }

        protected override string ConvertToString(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, int value)
        {
            string strRet = string.Empty;
            switch (value)
            {
                case 0:
                    strRet = "3D";
                    break;
                case 1:
                    strRet = "虚线";
                    break;
                case 2:
                    strRet = "(无)";
                    break;
                case 3:
                    strRet = "实线";
                    break;
                case 4:
                    strRet = "边框";
                    break;
                default:
                    strRet = GetDefaultDestinationValue(context, culture);
                    break;
            }

            return strRet;
        }

        protected override int GetDefaultSourceValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return 1;
        }

        protected override string GetDefaultDestinationValue(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture)
        {
            return "虚线";
        }
    }
}
