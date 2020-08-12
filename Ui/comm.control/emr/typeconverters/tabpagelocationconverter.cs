using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Common.Controls.Emr
{
    public class TabPageLocationConverter : System.ComponentModel.EnumConverter
    {
        public TabPageLocationConverter()
            : base(typeof(DevExpress.XtraTab.TabHeaderLocation))
        {

        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            string[] s = new string[]{
                "上",
                "下",
                "左",
                "右"
            };

            return new StandardValuesCollection(s);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            try
            {
                DevExpress.XtraTab.TabHeaderLocation ret = DevExpress.XtraTab.TabHeaderLocation.Top;
                if (value is string)
                {
                    switch (value.ToString())
                    {
                        case "上":
                            ret = DevExpress.XtraTab.TabHeaderLocation.Top;
                            break;

                        case "下":
                            ret = DevExpress.XtraTab.TabHeaderLocation.Bottom;
                            break;

                        case "左":
                            ret = DevExpress.XtraTab.TabHeaderLocation.Left;
                            break;

                        case "右":
                            ret = DevExpress.XtraTab.TabHeaderLocation.Right;
                            break;

                        default:
                            return base.ConvertFrom(context, culture, value);
                    }

                }
                return ret;
                //return base.ConvertFrom(context, culture, value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            try
            {
                if (value is DevExpress.XtraTab.TabHeaderLocation)
                {
                    string ret = "上";

                    switch ((DevExpress.XtraTab.TabHeaderLocation)value)
                    {
                        case DevExpress.XtraTab.TabHeaderLocation.Top:
                            ret = "上";
                            break;

                        case DevExpress.XtraTab.TabHeaderLocation.Left:
                            ret = "左";
                            break;

                        case DevExpress.XtraTab.TabHeaderLocation.Bottom:
                            ret = "下";
                            break;

                        case DevExpress.XtraTab.TabHeaderLocation.Right:
                            ret = "右";
                            break;

                        default:
                            //ret = "(无)";
                            break;
                        //return base.ConvertTo(context, culture, value, destinationType);
                    }

                    return ret;
                }
                return DevExpress.XtraTab.TabHeaderLocation.Top;// base.ConvertTo(context, culture, value, destinationType);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(context, sourceType);
            }
            //return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            else
            {
                return base.CanConvertTo(context, destinationType);
            }
            //return base.CanConvertTo(context, destinationType);
        }
    }
}
