using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DevExpress.Utils.Design;

namespace Common.Controls.Emr.Misc
{
    public class FontSerializationService
    {
        private static string SPLITER_ITEM
        {
            get
            {
                return "#";
            }
        }

        private static string SPLITER_NAMEVALUE
        {
            get
            {
                return ";";
            }
        }

        public static string Serialize(Font font)
        {
            string ret = string.Empty;

            if (font != null)
            {
                //文本格式
                string fontname = font.FontFamily.Name;

                //字体大小
                float fontsize = font.Size;

                //粗体
                bool isBold = font.Bold;

                //斜体
                bool isItalic = font.Italic;

                //下画线
                bool isUnderline = font.Underline;

                //删除线
                bool isStrikeout = font.Strikeout;


                ret = "FontFamily" + SPLITER_NAMEVALUE + fontname + SPLITER_ITEM;

                ret += "Size" + SPLITER_NAMEVALUE + fontsize.ToString() + SPLITER_ITEM;

                ret += "Bold" + SPLITER_NAMEVALUE + isBold.ToString() + SPLITER_ITEM;

                ret += "Italic" + SPLITER_NAMEVALUE + isItalic.ToString() + SPLITER_ITEM;

                ret += "Underline" + SPLITER_NAMEVALUE + isUnderline.ToString() + SPLITER_ITEM;

                ret += "Strikeout" + SPLITER_NAMEVALUE + isStrikeout.ToString();// +SPLITER;

            }
            return ret;
        }

        public static Font Deserialize(string fonttext)
        {
            Font ret = null;
            if (!string.IsNullOrEmpty(fonttext))
            {
                string[] namevalues = fonttext.Split(new string[] { SPLITER_ITEM }, StringSplitOptions.None);

                //文本格式
                string fontname = string.Empty;

                //字体大小
                float fontsize = 8;

                //粗体
                bool isBold = false;

                //斜体
                bool isItalic = false;

                //下画线
                bool isUnderline = false;

                //删除线
                bool isStrikeout = false;

                FontStyle style = FontStyle.Regular;
                foreach (string namevalue in namevalues)
                {
                    string name = namevalue.Split(new string[] { SPLITER_NAMEVALUE }, StringSplitOptions.None)[0];
                    string value = namevalue.Split(new string[] { SPLITER_NAMEVALUE }, StringSplitOptions.None)[1];

                    if (name.ToLower() == "fontfamily")
                    {
                        fontname = value;
                    }
                    else if (name.ToLower() == "size")
                    {
                        float.TryParse(value, out fontsize);
                    }
                    else if (name.ToLower() == "bold")
                    {
                        if (value.ToLower() == "true")
                        {
                            isBold = true;
                            style = style | FontStyle.Bold;
                        }
                    }
                    else if (name.ToLower() == "italic")
                    {
                        if (value.ToLower() == "true")
                        {
                            isItalic = true;
                            style = style | FontStyle.Italic;
                        }
                    }
                    else if (name.ToLower() == "underline")
                    {
                        if (value.ToLower() == "true")
                        {
                            isUnderline = true;
                            style = style | FontStyle.Underline;
                        }
                    }
                    else if (name.ToLower() == "strikeout")
                    {
                        if (value.ToLower() == "true")
                        {
                            isStrikeout = true;
                            style = style | FontStyle.Strikeout;
                        }
                    }
                }
                ret = new Font(fontname, fontsize, style);
                
            }
            return ret;
        }
    }
}
