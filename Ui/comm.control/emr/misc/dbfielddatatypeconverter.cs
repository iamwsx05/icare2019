using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common.Controls.Emr
{
    public class DBFieldDataTypeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            string[] s = new string[]{
                "(无)",
                "字符串",
                "整数",
                "小数",
                "日期时间",
                "是非"
            };

            return new StandardValuesCollection(s);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value != null)
            {
                string ret = string.Empty;

                switch (value.ToString())
                {
                    case "字符串":
                        ret = typeof(string).FullName;
                        break;

                    case "整数":
                        ret = typeof(int).FullName;
                        break;

                    case "小数":
                        ret = typeof(decimal).FullName;
                        break;

                    case "日期时间":
                        ret = typeof(DateTime).FullName;
                        break;

                    case "是非":
                        ret = typeof(bool).FullName;
                        break;
                }

                return ret;
            }
            else
            {
                return string.Empty;
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value != null)
            {
                string ret = string.Empty;

                switch (value.ToString())
                {
                    case "System.String":
                        ret = "字符串";
                        break;

                    case "System.Int32":
                        ret = "整数";
                        break;

                    case "System.Decimal":
                        ret = "小数";
                        break;

                    case "System.DateTime":
                        ret = "日期";
                        break;

                    case "System.Boolean":
                        ret = "是非";
                        break;

                    case "":
                        ret = "(无)";
                        break;

                    default:
                        //ret = "(无)";
                        //break;
                        return base.ConvertTo(context, culture, value, destinationType);
                }

                return ret;
            }
            else
            {
                //return base.ConvertTo(context, culture, value, destinationType);
                return "(无)";
            }
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }
    }
}
