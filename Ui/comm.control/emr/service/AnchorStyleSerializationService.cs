using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using Common.Entity;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    class AnchorStyleSerializationService
    {
        public static string Serialize(AnchorStyles style)
        {
            string text = string.Empty;

            if (style == AnchorStyles.None)
            {

            }
            else
            {
                if ((style & AnchorStyles.Top) == AnchorStyles.Top)
                {
                    text += ",Top";
                }

                if ((style & AnchorStyles.Right) == AnchorStyles.Right)
                {
                    text += ",Right";
                }

                if ((style & AnchorStyles.Left) == AnchorStyles.Left)
                {
                    text += ",Left";
                }

                if ((style & AnchorStyles.Bottom) == AnchorStyles.Bottom)
                {
                    text += ",Bottom";
                }

                if (text.Length > 0)
                {
                    text = text.Remove(0, 1);
                }
            }
            return text;
        }

        public static AnchorStyles Deserialize(string text)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 自定义表单界面控件布局
    /// </summary>
    [Serializable]
    [DataContract]
    [EntityAttribute(TableName = "t_ehr_formcontrolarrange", DisplayName = "控件布局")]
    public class clsCustomFormControlArrangement : BaseDataContract
    {
        /// <summary>
        /// 数据库记录标识
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "serno_int", DbType = DbType.Decimal, DisplayName = "serno", IsGenerator = true, IsPK = true)]
        public decimal Serno { get; set; }

        /// <summary>
        /// 单据ID
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "formid_int", DbType = DbType.Decimal, DisplayName = "自定义表单ID")]
        public decimal FormID { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "controltype_vchr", DbType = DbType.String, DisplayName = "控件类型")]
        public string ControlType { get; set; }

        /// <summary>
        /// 控件实例名称
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "controlname_vchr", DbType = DbType.String, DisplayName = "控件实例名称")]
        public string ControlName { get; set; }

        /// <summary>
        /// 父控件实例名称
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "parent_vchr", DbType = DbType.String, DisplayName = "父控件实例名称")]
        public string Parent { get; set; }


        /// <summary>
        /// 运行时只读
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "readonly_bit", DbType = DbType.Decimal, DisplayName = "运行时只读")]
        public decimal RunTimeReadOnly { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "width_int", DbType = DbType.Decimal, DisplayName = "宽度")]
        public decimal Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "height_int", DbType = DbType.Decimal, DisplayName = "高度")]
        public decimal Height { get; set; }

        /// <summary>
        /// 距离顶端距离
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "top_int", DbType = DbType.Decimal, DisplayName = "距离顶端距离")]
        public decimal Top { get; set; }

        /// <summary>
        /// 距离左侧距离
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "left_int", DbType = DbType.Decimal, DisplayName = "距离左侧距离")]
        public decimal Left { get; set; }

        /// <summary>
        /// 文本字体
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "textfont_vchr", DbType = DbType.String, DisplayName = "文本字体")]
        public string TextFont { get; set; }

        /// <summary>
        /// 文本值
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "text_vchr", DbType = DbType.String, DisplayName = "文本值")]
        public string DesignTimeText { get; set; }

        /// <summary>
        /// Tab键顺序
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "taborder_int", DbType = DbType.Decimal, DisplayName = "Tab键顺序")]
        public decimal TabOrder { get; set; }

        #region 颜色
        /// <summary>
        /// 前景色
        /// </summary>
        [DataMember]
        public Color ForeColor
        {
            get
            {
                try
                {
                    ColorConverter cc = new ColorConverter();
                    Color c = (Color)cc.ConvertFromString(this.ForeColorText);
                    return c;
                }
                catch
                {
                    return Color.Transparent;
                }
            }
            set
            {
                ColorConverter cc = new ColorConverter();
                this.ForeColorText = cc.ConvertToString(value);
            }
        }

        /// <summary>
        /// 前景色存储值
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "forecolor_vchr", DbType = DbType.String, DisplayName = "前景色存储值")]
        public string ForeColorText
        {
            get;
            set;
        }

        /// <summary>
        /// 背景色
        /// </summary>
        [DataMember]
        public Color BackColor
        {
            get
            {
                try
                {
                    ColorConverter cc = new ColorConverter();
                    Color c = (Color)cc.ConvertFromString(this.BackColorText);
                    return c;
                }
                catch
                {
                    return Color.Transparent;
                }
            }
            set
            {
                ColorConverter cc = new ColorConverter();
                this.BackColorText = cc.ConvertToString(value);
            }
        }

        /// <summary>
        /// 背景色存储值
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "backcolor_vchr", DbType = DbType.String, DisplayName = "背景色存储值")]
        public string BackColorText { get; set; }


        #endregion

        /// <summary>
        /// 子项目
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "items_vchr", DbType = DbType.String, DisplayName = "子项目")]
        public string Items { get; set; }

        #region 绑定属性

        /// <summary>
        /// 绑定列名
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bindingfield_name_vchr", DbType = DbType.String, DisplayName = "绑定列名")]
        public string BindingFieldName { get; set; }

        /// <summary>
        /// 绑定列的数据类型
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bindingfield_type_vchr", DbType = DbType.String, DisplayName = "绑定列的数据类型")]
        public string BindingFieldDataType { get; set; }

        /// <summary>
        /// 绑定列备注
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bindingfield_desc_vchr", DbType = DbType.String, DisplayName = "绑定列备注")]
        public string BindingFieldDesc { get; set; }

        /// <summary>
        /// 绑定列数据精度
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bindingfield_precision_vchr", DbType = DbType.String, DisplayName = "绑定列数据精度")]
        public string BindingFieldPrecision { get; set; }
        #endregion

        /// <summary>
        /// 点击事件调用的dll和类名
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "event_click_vchr", DbType = DbType.String, DisplayName = "点击事件调用的dll和类名")]
        public string EventClick { get; set; }

        ///// <summary>
        ///// Z轴顺序
        ///// </summary>
        //public decimal ZIndex { get; set; }

        /// <summary>
        /// 保留字段
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "reservefield_vchr", DbType = DbType.String, DisplayName = "保留字段")]
        public string ReserveField { get; set; }


        /// <summary>
        /// 关联计算属性
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "calproperty_vchr", DbType = DbType.String, DisplayName = "关联计算属性")]
        public string CalProperty { get; set; }

        /// <summary>
        /// 行缩进字符个数
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "rowshrinkdigit_int", DbType = DbType.Decimal, DisplayName = "行缩进字符个数")]
        public decimal RowShrinkdigit { get; set; }

        /// <summary>
        /// 首行标题字符
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "firstlinecaption_vchr", DbType = DbType.String, DisplayName = "首行标题字符")]
        public string FirstLineCaption { get; set; }

        /// <summary>
        /// 控件是否隐藏(数据库存储)
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "hidden_int", DbType = DbType.String, DisplayName = "控件是否隐藏(数据库存储)")]
        public decimal Hiddden_int { get; set; }


        //private decimal _showunderline_int = 1;

        /// <summary>
        /// 是否显示下划线
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "showunderline_int", DbType = DbType.String, DisplayName = "是否显示下划线")]
        public decimal Showunderline_int { get; set; }

        /// <summary>
        /// 自动签名
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "isautosignature_int", DbType = DbType.Decimal, DisplayName = "自动签名")]
        public decimal IsAutoSignature { get; set; }
        /// <summary>
        /// 空签名
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "isallowsignnull_int", DbType = DbType.Decimal, DisplayName = "空签名")]
        public decimal IsAllowSignNull { get; set; }
        //{
        //    get
        //    {
        //        return _showunderline_int;
        //    }
        //    set
        //    {
        //        _showunderline_int = value;
        //    }
        //}

        private decimal referencetype_int;
        [DataMember]
        [EntityAttribute(FieldName = "referencetype_int", DbType = DbType.Decimal, IsPK = false, IsGenerator = false, DisplayName = "是否资料调用")]
        public decimal Referencetype_int
        {
            set { this.referencetype_int = value; }
            get { return this.referencetype_int; }
        }


        #region ctlLayoutControl相关属性
        private string itemparent_vchr;
        private string itemcontrol_vchr;
        private decimal drawItemborders_int;

        [DataMember]
        [EntityAttribute(FieldName = "itemparent_vchr", DbType = DbType.String, IsPK = false, IsGenerator = false, DisplayName = "LayoutItem使用_父控件名称")]
        public string Itemparent_vchr
        {
            set { this.itemparent_vchr = value; }
            get { return this.itemparent_vchr; }
        }

        [DataMember]
        [EntityAttribute(FieldName = "Itemcontrol_vchr", DbType = DbType.String, IsPK = false, IsGenerator = false, DisplayName = "LayoutItem使用_对应控件")]
        public string Itemcontrol_vchr
        {
            set { this.itemcontrol_vchr = value; }
            get { return this.itemcontrol_vchr; }
        }

        [DataMember]
        [EntityAttribute(FieldName = "drawItemborders_int", DbType = DbType.Decimal, IsPK = false, IsGenerator = false, DisplayName = "LayoutItem使用_是否画边框")]
        public decimal DrawItemborders_int
        {
            set { this.drawItemborders_int = value; }
            get { return this.drawItemborders_int; }
        }

        private string signcaptain_vchr;

        [DataMember]
        [EntityAttribute(FieldName = "signcaptain_vchr", DbType = DbType.String, IsPK = false, IsGenerator = false, DisplayName = "签名标题")]
        public string Signcaptain_vchr
        {
            set { this.signcaptain_vchr = value; }
            get { return this.signcaptain_vchr; }
        }
        #endregion

        private decimal essential_int;
        [DataMember]
        [EntityAttribute(FieldName = "essential_int", DbType = DbType.Decimal, IsPK = false, IsGenerator = false, DisplayName = "是否必填")]
        public decimal Essential_int
        {
            set { this.essential_int = value; }
            get { return this.essential_int; }
        }

        private string elementitems_vchr;
        [DataMember]
        [EntityAttribute(FieldName = "elementitems_vchr", DbType = DbType.String, IsPK = false, IsGenerator = false, DisplayName = "必填元素项")]
        public string Elementitems_vchr
        {
            set { this.elementitems_vchr = value; }
            get { return this.elementitems_vchr; }
        }

        private decimal defaultRows_int = 1;
        [DataMember]
        [EntityAttribute(FieldName = "defaultrows_int", DbType = DbType.Decimal, IsPK = false, IsGenerator = false, DisplayName = "默认行数")]
        public decimal DefaultRows_int
        {
            set { this.defaultRows_int = value; }
            get { return this.defaultRows_int; }
        }

        private decimal presentationmode = 2;
        [DataMember]
        [EntityAttribute(FieldName = "presentationmode_int", DbType = DbType.Decimal, IsPK = false, IsGenerator = false, DisplayName = "样式")]
        public decimal PresentationMode
        {
            set { this.presentationmode = value; }
            get { return this.presentationmode; }
        }

        private decimal fixedheight = 0;
        [DataMember]
        [EntityAttribute(FieldName = "fixedheight_int", DbType = DbType.Decimal, IsPK = false, IsGenerator = false, DisplayName = "固定行高")]
        public decimal FixedHeight
        {
            set { this.fixedheight = value; }
            get { return this.fixedheight; }
        }

        private decimal masktype = 0;
        [DataMember]
        [EntityAttribute(FieldName = "masktype_int", DbType = DbType.Decimal, IsPK = false, IsGenerator = false, DisplayName = "掩码类型")]
        public decimal MaskType
        {
            set { this.masktype = value; }
            get { return this.masktype; }
        }

        [DataMember]
        [EntityAttribute(FieldName = "formversion_int", DbType = DbType.Decimal, IsPK = false, IsGenerator = false, DisplayName = "自定表单版本")]
        public decimal decFormVersion { get; set; }

        [DataMember]
        public bool _ShowUnderLine
        {
            get
            {
                if (Showunderline_int == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                Showunderline_int = (value == true ? 1 : 0);
            }
        }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible
        {
            get
            {
                if (Hiddden_int == 0)
                {
                    return true;
                }
                else if (Hiddden_int == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                if (value == true)
                {
                    this.Hiddden_int = 0;
                }
                else
                {
                    this.Hiddden_int = 1;
                }
            }
        }

        public clsCustomFormControlArrangement()
        {
            this.Parent = string.Empty;
            this.DesignTimeText = string.Empty;

            this.BindingFieldName = string.Empty;
            this.BindingFieldDataType = string.Empty;
            this.BindingFieldDesc = string.Empty;
            this.BindingFieldPrecision = string.Empty;

            this.EventClick = string.Empty;

            this.ForeColorText = string.Empty;
            this.BackColorText = string.Empty;

            this.RunTimeReadOnly = 0;

            this.ReserveField = string.Empty;

            this.Hiddden_int = 0;
        }

        public override string ToString()
        {
            return string.Format("Name={0} Parent={1} Text={2}", this.ControlName, this.Parent, this.DesignTimeText);
        }
    }
}
