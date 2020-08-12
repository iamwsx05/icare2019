using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 日期时间mask转换类
    /// </summary>
    public class DateTimeMaskConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[]{
                "日期",
                "日期时间"
            });
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                if (value.ToString() == "日期")
                {
                    return "yyyy-MM-dd";
                }
                else if (value.ToString() == "日期时间")
                {
                    return "yyyy-MM-dd HH:mm:ss";
                }
                else
                {
                    return base.ConvertFrom(context, culture, value);
                }
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is string)
            {
                if (value.ToString() == "yyyy-MM-dd")
                {
                    return "日期";
                }
                else if (value.ToString() == "yyyy-MM-dd HH:mm:ss")
                {
                    return "日期时间";
                }
                else
                {
                    return base.ConvertTo(context, culture, value, destinationType);
                }
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
