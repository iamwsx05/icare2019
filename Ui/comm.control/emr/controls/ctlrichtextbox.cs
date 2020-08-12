using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data;
using DevExpress.XtraTreeList.Columns;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using DevExpress.XtraTreeList.Nodes;
using Common.Entity;
using Common.Utils;
using weCare.Core.Utils;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 病历书写控件
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.RichTextBox))]
    public partial class ctlRichTextBox : System.Windows.Forms.RichTextBox, IRtfEditor, IRuntimeDesignControl, IFormCtrl
    {
        #region 外部API
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;

        #endregion

        #region 构造函数
        /// <summary>
        /// 门诊.处理意见外部调用
        /// </summary>
        private const string OP_Call = "opcall";
        /// <summary>
        /// 初始化中
        /// </summary>
        public bool m_blnIniting = false;
        private IContainer components;
        /// <summary>
        /// 表格标志
        /// </summary>
        private bool _blnTableFlag = false;
        /// <summary>
        /// 表格标志
        /// </summary>
        public bool m_blnTableFlag
        {
            get { return _blnTableFlag; }
            set
            {
                _blnTableFlag = value;
                if (value)
                {
                    this.Font = new System.Drawing.Font("宋体", 9.5F);
                }
            }
        }

        /// <summary>
        /// 元素字体
        /// </summary>
        private System.Drawing.Font FntElement
        {
            get
            {
                return new System.Drawing.Font("黑体", this.Font.Size);
            }
        }

        /// <summary>
        /// 父表格
        /// </summary>
        internal ctlTableCase ParentTable { get; set; }

        /// <summary>
        /// 是否允许元素自由编辑
        /// </summary>
        private bool IsAllowElementFreeEdit
        {
            get
            {
                if (this.FormTypeName == c_strFormClass)
                {
                    return false;
                }
                else
                {
                    int val = 0;
                    if (GlobalParm.dicSysParameter.ContainsKey(28))
                    {
                        int.TryParse(GlobalParm.dicSysParameter[28], out val);
                    }
                    if (val == 1)
                        return false;
                    else
                        return true;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ctlRichTextBox()
            : base()
        {
            Init();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_blnInit"></param>
        public ctlRichTextBox(bool p_blnInit)
            : base()
        {
            this.m_blnIniting = true;
            Init();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_blnInit"></param>
        public ctlRichTextBox(bool p_blnInit, bool p_blnUseRowSpace)
            : base()
        {
            this.m_blnIniting = true;
            this.UseRowSpacing = p_blnUseRowSpace;
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            if (DesignMode)
            {
                return;
            }

            this.m_lstDoubleStrikeThrough = new List<EntityDstInfo>();
            this.m_lstModifyUsers = new List<EntityModifyUserInfo>();
            this.m_lstTextContentInfos = new List<EntityUserContentInfo>();
            this.m_lstMedicalTerm = new List<EntityMedicalTerm>();
            this.m_lstImageInfo = new List<EntityImageInfo>();

            this.m_objCurrentModifyUser = new EntityModifyUserInfo();
            this.m_objCurrentModifyUser.ColorText = this.m_clrOldPartInsertText;
            this.m_objCurrentModifyUser.UserSequence = -1;

            this.m_clrOldPartInsertText = this.m_clrDefaultViewText;
            this.m_pntEndVisible = new Point(this.Width - 5, this.Height - 5);
            this.m_intFontHeight = this.Font.Height;

            this.m_sbdTemp = new StringBuilder();
            this.ImeMode = ImeMode.OnHalf;
            this.AllowDrop = true;

            using (Graphics _graphics = this.CreateGraphics())
            {
                // = _graphics.DpiX;
                // = _graphics.DpiY;
            }

            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prevBorderStyle = this.BorderStyle;
            this.prevBackColor = this.BackColor;

            this.EnableContextMenuStrip = false;
            this.m_objTipCtrl = new DevExpress.Utils.ToolTipController();
            this.m_objTipCtrl.AutoPopDelay = 2000;
            this.m_objTipCtrl.CloseOnClick = DevExpress.Utils.DefaultBoolean.True;
            this.m_objTipCtrl.Rounded = true;
            this.m_objTipCtrl.ShowBeak = true;
            this.m_objTipCtrl.ShowShadow = true;

            this.m_objTipArgs = this.m_objTipCtrl.CreateShowArgs();
            this.m_objTipArgs.IconType = DevExpress.Utils.ToolTipIconType.Information;
            this.m_objTipArgs.IconSize = DevExpress.Utils.ToolTipIconSize.Large;
            this.m_objTipArgs.ImageIndex = -1;
        }

        #endregion

        #region 变量
        /// <summary>
        /// 表单模板类名
        /// </summary>
        private const string c_strFormClass = "com.HopeBridge.ehr.client.frmEhrCasetemplate";

        /// <summary>
        /// 是否使用行间距
        /// </summary>
        private bool _blnUseRowSpacing = true;
        /// <summary>
        /// 是否使用行间距
        /// </summary>
        public bool UseRowSpacing
        {
            get { return _blnUseRowSpacing; }
            set { _blnUseRowSpacing = value; }
        }

        /// <summary>
        /// 当前选中的图片
        /// </summary>
        public EntityImageInfo m_objCurrentSelectedImage { get; set; }

        /// <summary>
        /// 双划线前缀
        /// </summary>
        private string m_strDSTPrefix
        {
            get
            {
                return Function.AsciiToStr(26); //(3);//(16);
            }
        }
        /// <summary>
        /// 术语前缀
        /// </summary>
        private string m_strTermPrefix
        {
            get
            {
                return string.Empty;
                //return clsFunction.s_strAsciiToStr(94);// (14);
            }
        }
        /// <summary>
        /// 图片前缀
        /// </summary>
        private string m_strImagePrefix
        {
            get
            {
                return string.Empty; // clsFunction.s_strAsciiToStr(14);
            }
        }

        /// <summary>
        /// 在文本内容替换时使用的工具
        /// </summary>
        private static System.Windows.Forms.RichTextBox s_rtbRTFReplace = new System.Windows.Forms.RichTextBox();

        /// <summary>
        /// 标记医学术语区间
        /// </summary>
        private List<EntityMedicalTerm> m_lstMedicalTerm = null;

        public List<EntityMedicalTerm> LstMedicalTerm
        {
            get { return m_lstMedicalTerm; }
        }

        /// <summary>
        /// 标记有双删除线的区间
        /// </summary>
        private List<EntityDstInfo> m_lstDoubleStrikeThrough = null;
        private List<EntityView> m_lstDDLView = new List<EntityView>();

        #region only for dst
        /// <summary>
        /// 最后可见的坐标,始终保持为控件的右下角偏移(-5,-5)
        /// </summary>
        private Point m_pntEndVisible;

        /// <summary>
        /// 字体高度,始终为控件所采用的字体的高度
        /// </summary>
        private int m_intFontHeight = 0;

        #endregion

        /// <summary>
        /// 用户交互时是否已经选择了文本
        /// </summary>
        private bool m_blnIsSelectedChanged = false;

        /// <summary>
        /// 是否处理选择改变的事件
        /// </summary>
        private bool m_blnCanSelectedChanged = true;

        /// <summary>
        /// 用户改变文本内容之前的长度

        /// </summary>
        private int m_intPreviouslyLen = 0;

        /// <summary>
        /// 当前光标索引
        /// </summary>
        private int m_intCurrentCursorIndex = 0;

        /// <summary>
        /// 记录用户选择文本的开始索引

        /// </summary>
        private int m_intSelectedTextStartIndex = 0;

        /// <summary>
        /// 记录用户选择文本的长度

        /// </summary>
        private int m_intSelectedTextLength = 0;

        /// <summary>
        /// 标记是后退键还是删除键
        /// </summary>
        private bool m_blnIsBackspace = false;

        /// <summary>
        /// 标记是否处理TextChanged事件,通常在内部设置控件文本内容时用
        /// </summary>
        private bool m_blnCanTextChanged = true;

        /// <summary>
        /// 标记是否可以修改选择的内容

        /// </summary>
        private bool m_blnCanModifySelection = true;

        /// <summary>
        /// 文本长度
        /// </summary>
        private int m_intTextLenght = 0;

        /// <summary>
        /// 前一次的文本内容(RTF)
        /// </summary>
        private string m_strPrevioslyText = string.Empty;

        /// <summary>
        /// 是否是在输入法模式下，如果是不重画。

        /// </summary>
        private bool m_blnIMEInput = false;

        /// <summary>
        /// 拼接字符串的临时缓冲
        /// </summary>
        private StringBuilder m_sbdTemp = null;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        private EntityModifyUserInfo m_objCurrentModifyUser = null;

        /// <summary>
        /// 所有修改用户的信息
        /// </summary>
        private List<EntityModifyUserInfo> m_lstModifyUsers = null;

        /// <summary>
        /// 存放clsUserContentInfo对象
        /// </summary>
        private List<EntityUserContentInfo> m_lstTextContentInfos = null;
        private List<EntityView> m_lstTextView = new List<EntityView>();
        private List<EntityView> m_lstElementView = new List<EntityView>();

        /// <summary>
        /// 多选
        /// </summary>
        private bool m_blnMuliSelectedFlag = false;
        /// <summary>
        /// 图片
        /// </summary>
        private List<EntityImageInfo> m_lstImageInfo = null;
        /// <summary>
        /// 术语颜色
        /// </summary>
        private Color m_clrTerm = Color.Blue;    // Color.Black; // Color.FromArgb(50, 60, 250);

        /// <summary>
        /// 元素.词汇编辑颜色
        /// </summary>
        private Color ElementEditColor = Color.OrangeRed;
        /// <summary>
        /// [键
        /// </summary>
        private bool m_blnKey219 = false;
        /// <summary>
        /// ]键
        /// </summary>
        private bool m_blnKey221 = false;
        /// <summary>
        /// 调用术语状态
        /// </summary>
        private bool m_blnLoadTermStatus = false;

        /// <summary>
        /// 临时RichTextBox对象
        /// </summary>
        private ctlRichTextBox m_objRtb = null;

        /// <summary>
        /// 系统分割符
        /// </summary>
        private string m_strSysSplit = "||";

        /// <summary>
        /// 替换RTF
        /// </summary>
        private bool IsReplaceRtf = false;

        /// <summary>
        /// 父EMR控件
        /// </summary>
        public ucEmr ParentEmrControl { get; set; }


        #endregion

        #region 属性

        #region IEfCtrl

        /// <summary>
        /// 项目代码(名称)
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// 项目标题(描述)
        /// </summary>
        public string ItemCaption { get; set; }

        /// <summary>
        /// 父节点名
        /// </summary>
        public string ParentNode { get; set; }

        /// <summary>
        /// 计算类型
        /// </summary>
        [Browsable(false)]
        public string CalProperty { get; set; }

        /// <summary>
        /// 行缩进字符个数
        /// </summary>
        [Browsable(false)]
        public int RowShrinkDigit { get; set; }

        #endregion

        [Category("IRuntimeDesignControl属性")]
        [Description("必填组号")]
        public string EssentialGroupNo { get; set; }

        /// <summary>
        /// 门诊传入的输入法名称
        /// </summary>
        [Browsable(false)]
        public string OpInputLanguageName { get; set; }

        #region 固定高度
        /// <summary>
        /// 固定高度
        /// </summary>
        public bool FixedHeight { get; set; }
        #endregion

        #region 书写显示类型
        /// <summary>
        /// 书写显示类型 0 不限制 1 无病人 2 年龄、性别受限
        /// </summary>
        private int _intConfineType = 0;
        /// <summary>
        /// 书写显示类型  0 不限制 1 无病人 2 年龄、性别受限
        /// </summary>
        public int intConfineType
        {
            get { return _intConfineType; }
            set { _intConfineType = value; }
        }
        #endregion

        #region 模板维护标志
        /// <summary>
        /// 模板维护标志
        /// </summary>
        [Browsable(false)]
        public bool TemplateFlag { get; set; }
        #endregion

        #region 图片标志
        /// <summary>
        /// 图片标志 true 是；false 是。
        /// </summary>
        [Category("附加属性"), Description("图片标志 true 是；false 是。")]
        public bool ImageFlag { get; set; }
        #endregion

        #region 值变化标志
        /// <summary>
        /// 值变化标志
        /// </summary>
        private bool _blnValueChangedFlag = false;
        /// <summary>
        /// 值变化标志
        /// </summary>
        public bool ValueChangedFlag
        {
            get
            {
                if (GlobalPatient.currPatient == null || string.IsNullOrEmpty(GlobalPatient.currPatient.RegisterID)) return false;
                return _blnValueChangedFlag;
            }
            set { _blnValueChangedFlag = value; }
        }
        #endregion

        #region 缺省的视图(字体)颜色
        /// <summary>
        /// 缺省的视图(字体)颜色
        /// </summary>
        private Color m_clrDefaultViewText = Color.Black;
        #endregion

        #region 指定控件内容是否只读 ,默认false
        /// <summary>
        /// 指定控件内容是否只读
        /// </summary>
        private bool m_blnIsReadOnly = false;

        /// <summary>
        /// 指定控件内容是否只读
        /// </summary>
        public bool _blnReadOnly
        {
            get
            {
                return ReadOnly;
            }
            set
            {
                m_blnIsReadOnly = value;
                ReadOnly = value;
            }
        }

        #endregion

        #region 当前用户在旧的部分输入文字的颜色,默认黑色
        /// <summary>
        /// 当前用户在旧的部分输入文字的颜色,默认黑色
        /// </summary>
        private Color m_clrOldPartInsertText = Color.Black;
        #endregion

        #region 行间距
        /// <summary>
        /// 行间距
        /// </summary>
        private int m_intRowSpacing = 20;
        /// <summary>
        /// 行间距
        /// </summary>
        public int _intRowSpacing
        {
            get
            {
                if (FixedHeight) return 12;
                return m_intRowSpacing;
            }
            set
            {
                m_intRowSpacing = value;
            }
        }

        /// <summary>
        /// 设置行间距
        /// </summary>
        private void m_mthSetRowSpacing(int p_intRowSpacing)
        {
            if (p_intRowSpacing <= 0) return;
            this.stopReDraw();
            this._intRowSpacing = p_intRowSpacing;
            RichTextBoxPlus.PARAFORMAT2 fmt = new RichTextBoxPlus.PARAFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.bLineSpacingRule = 3;
            fmt.dyLineSpacing = 20 * p_intRowSpacing;
            fmt.dwMask = RichTextBoxPlus.PFM_LINESPACING;
            RichTextBoxPlus.SendMessage(new HandleRef(this, this.Handle), RichTextBoxPlus.EM_SETPARAFORMAT, 0, ref fmt);
            this.reDraw();
        }

        /// <summary>
        /// 设置行间距
        /// </summary>
        private void m_mthSetRowSpacing()
        {
            if (this.IsHandleCreated && this.UseRowSpacing && !this.m_blnIniting && !this.m_blnTableFlag)
                this.m_mthSetRowSpacing(_intRowSpacing);
        }
        #endregion

        #region 是否允许拖动
        //public bool _blnAllowDrop
        //{
        //    get
        //    {
        //        return m_blnAllowDrop;
        //    }
        //    set
        //    {
        //        m_blnAllowDrop = value;
        //    }
        //}
        #endregion

        #region 表字段属性

        ///// <summary>
        ///// 数据库字段名
        ///// </summary>
        //[Category("字段属性")]
        //[Description("数据库字段名")]
        //public string DBColName
        //{
        //    get;
        //    set;
        //}
        ///// <summary>
        ///// 数据库字段描述
        ///// </summary>
        //[Category("字段属性")]
        //[Description("数据库字段描述")]
        //public string DBColDesc
        //{
        //    get;
        //    set;
        //}
        ///// <summary>
        ///// 数据库字段类型
        ///// </summary>
        //[Category("字段属性")]
        //[Description("数据库字段类型")]
        //public string DBColType
        //{
        //    get;
        //    set;
        //}

        //[Category("字段属性")]
        //[Description("数据库字段精度")]
        //public string DBColPrecision
        //{
        //    get;
        //    set;
        //}

        //[Category("字段属性")]
        //[Description("运行时只读")]
        //public bool DBColReadOnly
        //{
        //    get;
        //    set;
        //}
        #endregion

        #region (首)行属性
        int intRowShrinkdigit = 4;
        /// <summary>
        /// 行缩进字符个数
        /// </summary>
        [Category("字段属性"), Description("行缩进字符个数")]
        public int RowShrinkdigit
        {
            get { return intRowShrinkdigit; }
            set { intRowShrinkdigit = value; }
        }

        private bool isCheckCaption = false;
        /// <summary>
        /// 首行标题字符
        /// </summary>
        private string _strFirstlineCaption = string.Empty;
        /// <summary>
        /// 首行标题字符
        /// </summary>
        [Category("字段属性"), Description("首行标题字符")]
        public string FirstlineCaption
        {
            get
            {
                if (string.IsNullOrEmpty(_strFirstlineCaption))
                {
                    return null;
                }
                else
                {
                    if (isCompleteSetXml)
                    {
                        if (!isCheckCaption && this.Text != string.Empty)
                        {
                            if (this.Text.StartsWith(_strFirstlineCaption))
                            {
                                isCheckCaption = true;
                            }
                            else
                            {
                                int pos = this.Text.IndexOf(":");
                                if (pos < 0)
                                {
                                    pos = this.Text.IndexOf("：");
                                }
                                if (pos > 0 && pos < 10)
                                {
                                    _strFirstlineCaption = this.Text.Substring(0, pos + 1);
                                    isCheckCaption = true;
                                }
                            }
                        }
                    }
                }
                return _strFirstlineCaption.Trim();
            }
            set { _strFirstlineCaption = value; }
        }
        //public int IsAutoSignature { get; set; }
        //public int IsAllowSignNull { get; set; }

        /// <summary>
        /// 默认行数
        /// </summary>
        private int _intDefaultRows = 1;
        /// <summary>
        /// 默认行数
        /// </summary>
        [Category("字段属性"), Description("默认行数")]
        public int DefaultRows
        {
            get
            {
                if (_intDefaultRows <= 0) _intDefaultRows = 1;
                return _intDefaultRows;
            }
            set { _intDefaultRows = value; }
        }
        #endregion

        /// <summary>
        /// 外部特殊修改标志(独立调数据界面调用)
        /// </summary>
        private bool _blnExtSpecModifyFlag = false;
        /// <summary>
        /// 外部特殊修改标志
        /// </summary>
        public bool ExtSpecModifyFlag
        {
            get { return _blnExtSpecModifyFlag; }
            set { _blnExtSpecModifyFlag = value; }
        }

        /// <summary>
        /// 页绑定
        /// </summary>
        private bool _blnBandingPage = false;
        /// <summary>
        /// 页绑定
        /// </summary>
        public bool BandingPage
        {
            get { return _blnBandingPage; }
            set { _blnBandingPage = value; }
        }
        #endregion

        #region 事件

        #region m_evtTextChange 事件
        /// <summary>
        /// 文本内容发生变化,包括上下标,双划线等,替代原来的TextChange事件.
        /// </summary>
        public event EventHandler m_evtTextChange;
        /// <summary>
        /// 先前的文本内容,未记录格式,(TextChange记录文本内容,上下标,双划线即可)
        /// </summary>
        private string m_txtTextBoxOldContent = "";
        /// <summary>
        /// 触发m_evtTextChange事件
        /// </summary>
        private void m_mthFireEvtTextChange(bool blnCheckContent)
        {
            if (this.m_blnIniting) return;

            //检查内容
            if (blnCheckContent)
            {
                if (m_txtTextBoxOldContent == this.Text) return; //无变化
            }

            //触发事件
            if (m_evtTextChange != null) m_evtTextChange(this, new EventArgs());

            m_txtTextBoxOldContent = this.Text;
        }
        #endregion

        #region 鼠标事件

        /// <summary>
        /// 鼠标进入事件
        /// </summary>
        public event EventHandler m_evtMouseEnter;

        /// <summary>
        /// 进入事件
        /// </summary>
        public event EventHandler m_evtOnEnter;

        /// <summary>
        /// 提示框
        /// </summary>
        private DevExpress.Utils.ToolTipController m_objTipCtrl = null;
        /// <summary>
        /// 提示框事件
        /// </summary>
        private DevExpress.Utils.ToolTipControllerShowEventArgs m_objTipArgs = null;
        #endregion

        #region 图片选中事件
        public event EventHandler m_evtSelectedImage;
        #endregion

        private void CloseIcdList()
        {
            if (this.ParentEmrControl != null && this.ParentEmrControl.EfCtrls != null)
            {
                // 暂时屏蔽0627
                //try
                //{
                //    foreach (var item in ((frmBaseMdiCase)this.FindForm()).lstDBCol)
                //    {
                //        if (item is ctlICD && ((ctlICD)item).frmTemp != null && ((ctlICD)item).frmTemp.blnExtCall)
                //        {
                //            ((ctlICD)item).CloseListItem();
                //        }
                //    }
                //}
                //catch { };
            }
        }

        public bool m_blnInitEnter = false;
        protected override void OnEnter(EventArgs e)
        {
            //this.SelectionLength = 0;
            //if (m_evtOnEnter != null)
            //{
            //    clsEvtRichTextBox evt = new clsEvtRichTextBox();
            //    evt.m_objRichTextBox = this;
            //    m_evtOnEnter(this, evt);
            //}

            this.m_intCurrentCursorIndex = 0;
            this.m_mthSetCaptionCursor();
            this.m_blnInitEnter = true;

            if (this.Parent is ShowPanelForm)
            {
                ((ShowPanelForm)this.Parent).CurrentSelectedRichTextBox = this;
            }
            else if (this.Parent.Parent is ShowPanelForm)
            {
                ((ShowPanelForm)this.Parent.Parent).CurrentSelectedRichTextBox = this;
            }
            else if (this.Parent.Parent.Parent is ShowPanelForm)
            {
                ((ShowPanelForm)this.Parent.Parent.Parent).CurrentSelectedRichTextBox = this;
            }

            base.OnEnter(e);
        }

        #endregion

        #region 重绘/暂停重绘
        /// <summary>
        /// 暂停重绘
        /// </summary>
        void stopReDraw()
        {
            SendMessage(this.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 重绘
        /// </summary>
        void reDraw()
        {
            SendMessage(this.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            this.Invalidate();
            //this.Refresh();
        }
        #endregion

        #region 受限提示
        /// <summary>
        /// 受限提示
        /// </summary>
        private void m_mthHintConfineInfo()
        {
            if (this.intConfineType > 0)
            {
                string strInfo = string.Empty;
                if (this.intConfineType == 1)
                {
                    strInfo = "书写受限\r\n\r\n请选择病人。";
                }
                else if (this.intConfineType == 2)
                {
                    strInfo = "书写受限\r\n\r\n病人的年龄、性别受限。";
                }

                DialogBox.Msg(strInfo, MessageBoxIcon.Information, uiHelper.frmCurr);
            }
        }
        #endregion

        #region 双划线处理

        #region 处理paint消息,实现双划线的重画
        /// <summary>
        ///  处理paint消息,实现双划线的重画
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (this.m_blnIniting) return;
            if (m.Msg == 0x0204)
            {
                if (this.m_blnCursorPositionToCaption())
                    DialogBox.Msg("字段标题无右键", MessageBoxIcon.Information, uiHelper.frmCurr);
            }
            else if (m.Msg == 0x000F)//Repaint事件
            {
                Graphics objGrp = this.CreateGraphics();
                if (!m_blnIMEInput)
                {
                    this.m_mthDrawDST(objGrp);
                    this.m_mthDrawVirtualLine(objGrp);
                    this.m_mthDrawPoint(objGrp);
                }
                objGrp.Dispose();
            }
        }
        #endregion

        #region 画虚线
        /// <summary>
        /// 画虚线
        /// </summary>
        private void m_mthDrawVirtualLine(Graphics objGrp)
        {
            if (this._presentationMode == 1 || this._presentationMode == 3)
            {
                SizeF sf = SizeF.Empty;
                if (!string.IsNullOrEmpty(this.FirstlineCaption))
                    sf = objGrp.MeasureString(this.FirstlineCaption, this.Font);
                Pen p = new Pen(Brushes.Black);
                if (this._presentationMode == 1)
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                    p.DashPattern = new float[] { 2.0F, 2.0F };
                }
                else if (this._presentationMode == 3)
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                }
                bool blnMuti = false;
                if (this.Text.Length > 0)
                {
                    int intRow = 1;
                    int intRowChr = 0;
                    int intY = 0;
                    do
                    {
                        intRowChr = this.GetFirstCharIndexFromLine(intRow);
                        Point point = this.GetPositionFromCharIndex(intRowChr);
                        if (intY == point.Y)
                            break;
                        else if (point.Y > 15)
                        {
                            if (intRow == 1)
                                objGrp.DrawLine(p, new Point((Int32)sf.Width - 2, point.Y), new Point(this.Width, point.Y));
                            else
                                objGrp.DrawLine(p, new Point(0, point.Y), new Point(this.Width, point.Y));
                            blnMuti = true;
                        }
                        intY = point.Y;
                        intRow++;
                    } while (true);
                }
                if (blnMuti)
                    objGrp.DrawLine(p, new Point(0, this.Height - 1), new Point(this.Width, this.Height - 1));
                else
                    objGrp.DrawLine(p, new Point((Int32)sf.Width - 2, this.Height - 1), new Point(this.Width, this.Height - 1));
            }
            else if (this._presentationMode == 4)
            {
                this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            }
        }
        #endregion

        #region 画双删除线
        /// <summary>
        /// 添加预览对象信息
        /// </summary>
        /// <param name="p_objPoint"></param>
        /// <param name="p_intWidth"></param>
        /// <param name="p_intHeight"></param>
        /// <param name="p_intIndex"></param>
        /// <param name="p_intType">1. 文本  2. 双划线 3. 元素(词汇)</param>
        private void m_mthAddViewInfo(Point p_objPoint, int p_intWidth, int p_intHeight, int p_intIndex, int p_intType)
        {
            EntityView objView = new EntityView();
            objView.X1 = p_objPoint.X;
            objView.X2 = p_objPoint.X + p_intWidth;
            objView.Y1 = p_objPoint.Y - p_intHeight;
            objView.Y2 = p_objPoint.Y + p_intHeight + 10;
            objView.Index = p_intIndex;
            if (p_intType == 1)
            {
                this.m_lstTextView.Add(objView);
            }
            else if (p_intType == 2)
            {
                this.m_lstDDLView.Add(objView);
            }
            else if (p_intType == 3)
            {
                this.m_lstElementView.Add(objView);
            }
        }

        /// <summary>
        /// 画双删除线
        /// </summary>
        /// <param name="p_objGrp"></param>
        private void m_mthDrawDST(Graphics p_objGrp)
        {
            try
            {
                int intStartVisibleCharIndex = this.GetCharIndexFromPosition(Point.Empty);
                int intEndVisibleCharIndex = this.GetCharIndexFromPosition(m_pntEndVisible);

                if ((intEndVisibleCharIndex == 0) && (this.Text.Length != 1)) return;

                #region 内容位置
                this.m_lstTextView.Clear();
                this.m_lstTextContentInfos.Sort();

                int intIndex = 0;
                foreach (EntityUserContentInfo objContentInfo in this.m_lstTextContentInfos)
                {
                    //if (objContentInfo.m_clrText != Color.Red) continue;
                    //起止位置
                    int intStartIndex = objContentInfo.StartIndex;
                    int intEndIndex = objContentInfo.EndIndex;
                    //在可视区域之前,不处理
                    if (intEndIndex < intStartVisibleCharIndex) continue;
                    //在可视区域之后,退出
                    if ((intEndVisibleCharIndex > 0) && (intStartIndex > intEndVisibleCharIndex)) break;
                    //数据合法性
                    if ((intStartIndex < 0) || (intEndIndex < 0) || (intStartIndex >= this.TextLength) || (intEndIndex >= this.TextLength)) continue;

                    while (intStartIndex <= intEndIndex)
                    {
                        //边界值处理
                        if (intStartIndex < intStartVisibleCharIndex)
                            intStartIndex = intStartVisibleCharIndex;

                        if ((intEndVisibleCharIndex > 0) && (intEndIndex > intEndVisibleCharIndex))
                            intEndIndex = intEndVisibleCharIndex;

                        //起止行
                        int intUpLine = this.GetLineFromCharIndex(intStartIndex);
                        int intDownLine = this.GetLineFromCharIndex(intEndIndex);

                        if (intUpLine == intDownLine)
                        {
                            #region 头尾同一行

                            Point pntStart = this.GetPositionFromCharIndex(intStartIndex);
                            Point pntEnd = this.m_ptGetCharPositionRightDown(intEndIndex, p_objGrp);

                            this.m_mthAddViewInfo(pntStart, pntEnd.X - pntStart.X, m_intFontHeight, intIndex, 1);

                            #endregion
                        }
                        else
                        {
                            #region 不在同一行,即多行

                            Point pntTemp = new Point(this.Width, 0);
                            Point pntStart = this.GetPositionFromCharIndex(intStartIndex);

                            if (pntStart.Y < 0)
                            {
                                intStartIndex++;
                                continue;
                            }

                            pntTemp.Y = pntStart.Y;

                            int intEndCharIndex = this.GetCharIndexFromPosition(pntTemp) - 1;
                            int intLineWidth = this.GetPositionFromCharIndex(intEndCharIndex).X;
                            intLineWidth += (int)p_objGrp.MeasureString(this.Text[intEndCharIndex].ToString(), this.Font).Width - 5 - pntStart.X;
                            this.m_mthAddViewInfo(pntStart, intLineWidth, m_intFontHeight, intIndex, 1);

                            intUpLine++;
                            while (intUpLine < intDownLine)
                            {
                                int intStartCharIndex = intEndCharIndex + 1;
                                pntStart = this.GetPositionFromCharIndex(intEndCharIndex + 1);

                                int intCharAdd = 2;
                                while (pntStart.Y == pntTemp.Y)
                                {
                                    intStartCharIndex = intEndCharIndex + intCharAdd;
                                    pntStart = this.GetPositionFromCharIndex(intStartCharIndex);
                                    intCharAdd++;
                                }

                                pntTemp.Y = pntStart.Y;

                                intEndCharIndex = this.GetCharIndexFromPosition(pntTemp) - 1;

                                if (intStartCharIndex < this.Text.Length)
                                {
                                    if (intEndCharIndex - intStartCharIndex <= 0 || this.Text.Substring(intStartCharIndex, intEndCharIndex - intStartCharIndex - 1).Trim() == "")
                                    {
                                        intUpLine++;
                                        continue;
                                    }
                                }

                                intLineWidth = this.GetPositionFromCharIndex(intEndCharIndex).X;
                                intLineWidth += (int)p_objGrp.MeasureString(this.Text[intEndCharIndex].ToString(), this.Font).Width - 5 - pntStart.X;

                                this.m_mthAddViewInfo(pntStart, intLineWidth, m_intFontHeight, intIndex, 1);
                                intUpLine++;
                            }

                            pntStart = this.GetPositionFromCharIndex(intEndCharIndex + 1);

                            int intCharAdd1 = 2;
                            while (pntStart.Y == pntTemp.Y)
                            {
                                int intStartCharIndex = intEndCharIndex + intCharAdd1;
                                pntStart = this.GetPositionFromCharIndex(intStartCharIndex);
                                intCharAdd1++;
                            }

                            if (intEndIndex + 1 < this.Text.Length)
                            {
                                Point pntEnd = this.GetPositionFromCharIndex(intEndIndex + 1);

                                if (pntEnd.Y == pntStart.Y)
                                {
                                    intLineWidth = pntEnd.X - pntStart.X;
                                }
                                else
                                    intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X + (int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(), this.Font).Width - 5 - pntStart.X;
                            }
                            else
                                intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X + (int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(), this.Font).Width - 5 - pntStart.X;

                            this.m_mthAddViewInfo(pntStart, intLineWidth, m_intFontHeight, intIndex, 1);
                            #endregion
                        }
                        break;
                    }
                    intIndex++;
                }
                #endregion

                #region 元素.词汇
                this.m_lstElementView.Clear();
                this.m_lstMedicalTerm.Sort();

                intIndex = 0;
                foreach (EntityMedicalTerm objElem in this.m_lstMedicalTerm)
                {
                    //起止位置
                    int intStartIndex = objElem.StartIndex;
                    int intEndIndex = objElem.EndIndex;
                    //在可视区域之前,不处理
                    if (intEndIndex < intStartVisibleCharIndex) continue;
                    //在可视区域之后,退出
                    if ((intEndVisibleCharIndex > 0) && (intStartIndex > intEndVisibleCharIndex)) break;
                    //数据合法性
                    if ((intStartIndex < 0) || (intEndIndex < 0) || (intStartIndex >= this.TextLength) || (intEndIndex >= this.TextLength)) continue;

                    while (intStartIndex <= intEndIndex)
                    {
                        //边界值处理
                        if (intStartIndex < intStartVisibleCharIndex)
                            intStartIndex = intStartVisibleCharIndex;

                        if ((intEndVisibleCharIndex > 0) && (intEndIndex > intEndVisibleCharIndex))
                            intEndIndex = intEndVisibleCharIndex;

                        //起止行
                        int intUpLine = this.GetLineFromCharIndex(intStartIndex);
                        int intDownLine = this.GetLineFromCharIndex(intEndIndex);

                        if (intUpLine == intDownLine)
                        {
                            #region 头尾同一行

                            Point pntStart = this.GetPositionFromCharIndex(intStartIndex);
                            Point pntEnd = this.m_ptGetCharPositionRightDown(intEndIndex, p_objGrp);

                            this.m_mthAddViewInfo(pntStart, pntEnd.X - pntStart.X, m_intFontHeight, intIndex, 3);

                            #endregion
                        }
                        else
                        {
                            #region 不在同一行,即多行

                            Point pntTemp = new Point(this.Width, 0);
                            Point pntStart = this.GetPositionFromCharIndex(intStartIndex);

                            if (pntStart.Y < 0)
                            {
                                intStartIndex++;
                                continue;
                            }

                            pntTemp.Y = pntStart.Y;

                            int intEndCharIndex = this.GetCharIndexFromPosition(pntTemp) - 1;
                            int intLineWidth = this.GetPositionFromCharIndex(intEndCharIndex).X;
                            intLineWidth += (int)p_objGrp.MeasureString(this.Text[intEndCharIndex].ToString(), this.Font).Width - 5 - pntStart.X;
                            this.m_mthAddViewInfo(pntStart, intLineWidth, m_intFontHeight, intIndex, 3);

                            intUpLine++;
                            while (intUpLine < intDownLine)
                            {
                                int intStartCharIndex = intEndCharIndex + 1;
                                pntStart = this.GetPositionFromCharIndex(intEndCharIndex + 1);

                                int intCharAdd = 2;
                                while (pntStart.Y == pntTemp.Y)
                                {
                                    intStartCharIndex = intEndCharIndex + intCharAdd;
                                    pntStart = this.GetPositionFromCharIndex(intStartCharIndex);
                                    intCharAdd++;
                                }

                                pntTemp.Y = pntStart.Y;

                                intEndCharIndex = this.GetCharIndexFromPosition(pntTemp) - 1;

                                if (intStartCharIndex < this.Text.Length)
                                {
                                    if (intEndCharIndex - intStartCharIndex <= 0 || this.Text.Substring(intStartCharIndex, intEndCharIndex - intStartCharIndex - 1).Trim() == "")
                                    {
                                        intUpLine++;
                                        continue;
                                    }
                                }

                                intLineWidth = this.GetPositionFromCharIndex(intEndCharIndex).X;
                                intLineWidth += (int)p_objGrp.MeasureString(this.Text[intEndCharIndex].ToString(), this.Font).Width - 5 - pntStart.X;

                                this.m_mthAddViewInfo(pntStart, intLineWidth, m_intFontHeight, intIndex, 3);
                                intUpLine++;
                            }

                            pntStart = this.GetPositionFromCharIndex(intEndCharIndex + 1);

                            int intCharAdd1 = 2;
                            while (pntStart.Y == pntTemp.Y)
                            {
                                int intStartCharIndex = intEndCharIndex + intCharAdd1;
                                pntStart = this.GetPositionFromCharIndex(intStartCharIndex);
                                intCharAdd1++;
                            }

                            if (intEndIndex + 1 < this.Text.Length)
                            {
                                Point pntEnd = this.GetPositionFromCharIndex(intEndIndex + 1);

                                if (pntEnd.Y == pntStart.Y)
                                {
                                    intLineWidth = pntEnd.X - pntStart.X;
                                }
                                else
                                    intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X + (int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(), this.Font).Width - 5 - pntStart.X;
                            }
                            else
                                intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X + (int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(), this.Font).Width - 5 - pntStart.X;

                            this.m_mthAddViewInfo(pntStart, intLineWidth, m_intFontHeight, intIndex, 3);
                            #endregion
                        }
                        break;
                    }
                    intIndex++;
                }
                #endregion

                if (this.m_lstDoubleStrikeThrough.Count == 0) return;
                Pen penStrike = new Pen(Color.Red);

                #region 画双划线
                int intOffset = 16;// 8;
                if (!this.UseRowSpacing) intOffset = 8;
                if (this.m_blnTableFlag) intOffset = 8;
                //intOffset += Convert.ToInt32(Math.Ceiling(decimal.Parse(Convert.ToString((this._intRowSpacing - 300) * 8 / 300)))) * 2 + 2;

                this.m_lstDDLView.Clear();
                this.m_lstDoubleStrikeThrough.Sort();

                intIndex = 0;
                foreach (EntityDstInfo objDST in this.m_lstDoubleStrikeThrough)
                {
                    int intStartIndex = objDST.StartIndex;
                    int intEndIndex = objDST.EndIndex;

                    if (intEndIndex < intStartVisibleCharIndex) continue;

                    if ((intEndVisibleCharIndex > 0) && (intStartIndex > intEndVisibleCharIndex)) return;

                    penStrike.Color = objDST.ColorDst;

                    if ((intStartIndex < 0) || (intEndIndex < 0) || (intStartIndex >= m_intPreviouslyLen) || (intEndIndex >= m_intPreviouslyLen)) continue;

                    while (intStartIndex <= intEndIndex)
                    {
                        if (intStartIndex < intStartVisibleCharIndex)
                            intStartIndex = intStartVisibleCharIndex;

                        if ((intEndVisibleCharIndex > 0) && (intEndIndex > intEndVisibleCharIndex))
                            intEndIndex = intEndVisibleCharIndex;

                        int intUpLine = this.GetLineFromCharIndex(intStartIndex);
                        int intDownLine = this.GetLineFromCharIndex(intEndIndex);

                        if (intUpLine == intDownLine)
                        {
                            #region 头尾同一行

                            Point pntStart = this.GetPositionFromCharIndex(intStartIndex);

                            int intLineWidth = 0;

                            if (intEndIndex + 1 < this.Text.Length)
                            {
                                Point pntEnd = this.GetPositionFromCharIndex(intEndIndex + 1);

                                if (pntEnd.Y == pntStart.Y)
                                    intLineWidth = pntEnd.X - pntStart.X;
                                else
                                    intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X + (int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(), this.Font).Width - 5 - pntStart.X;
                            }
                            else
                                intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X + (int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(), this.Font).Width - 5 - pntStart.X;

                            this.m_mthAddViewInfo(pntStart, intLineWidth, m_intFontHeight, intIndex, 2);
                            pntStart.Offset(0, intOffset);
                            p_objGrp.DrawLine(penStrike, pntStart.X, pntStart.Y, pntStart.X + intLineWidth, pntStart.Y);
                            pntStart.Offset(0, 3);
                            p_objGrp.DrawLine(penStrike, pntStart.X, pntStart.Y, pntStart.X + intLineWidth, pntStart.Y);

                            #endregion
                        }
                        else
                        {
                            #region 不在同一行,即多行

                            Point pntTemp = new Point(this.Width, 0);

                            Point pntStart = this.GetPositionFromCharIndex(intStartIndex);

                            if (pntStart.Y < 0)
                            {
                                intStartIndex++;
                                continue;
                            }
                            pntTemp.Y = pntStart.Y;

                            int intEndCharIndex = this.GetCharIndexFromPosition(pntTemp) - 1;
                            int intLineWidth = this.GetPositionFromCharIndex(intEndCharIndex).X;
                            intLineWidth += (int)p_objGrp.MeasureString(this.Text[intEndCharIndex].ToString(), this.Font).Width - 5 - pntStart.X;

                            this.m_mthAddViewInfo(pntStart, intLineWidth, m_intFontHeight, intIndex, 2);
                            pntStart.Offset(0, intOffset);
                            p_objGrp.DrawLine(penStrike, pntStart.X, pntStart.Y, pntStart.X + intLineWidth, pntStart.Y);
                            pntStart.Offset(0, 3);
                            p_objGrp.DrawLine(penStrike, pntStart.X, pntStart.Y, pntStart.X + intLineWidth, pntStart.Y);

                            intUpLine++;

                            while (intUpLine < intDownLine)
                            {
                                int intStartCharIndex = intEndCharIndex + 1;

                                pntStart = this.GetPositionFromCharIndex(intEndCharIndex + 1);

                                int intCharAdd = 2;
                                while (pntStart.Y == pntTemp.Y)
                                {
                                    intStartCharIndex = intEndCharIndex + intCharAdd;
                                    pntStart = this.GetPositionFromCharIndex(intStartCharIndex);
                                    intCharAdd++;
                                }

                                pntTemp.Y = pntStart.Y;

                                intEndCharIndex = this.GetCharIndexFromPosition(pntTemp) - 1;

                                if (intStartCharIndex < this.Text.Length)
                                {
                                    if (intEndCharIndex - intStartCharIndex == 0 || intEndCharIndex - intStartCharIndex <= 0 || this.Text.Substring(intStartCharIndex, intEndCharIndex - intStartCharIndex - 1).Trim() == "")
                                    {
                                        intUpLine++;
                                        continue;
                                    }
                                }

                                intLineWidth = this.GetPositionFromCharIndex(intEndCharIndex).X;
                                intLineWidth += (int)p_objGrp.MeasureString(this.Text[intEndCharIndex].ToString(), this.Font).Width - 5 - pntStart.X;

                                this.m_mthAddViewInfo(pntStart, intLineWidth, m_intFontHeight, intIndex, 2);
                                pntStart.Offset(0, intOffset);
                                p_objGrp.DrawLine(penStrike, pntStart.X, pntStart.Y, pntStart.X + intLineWidth, pntStart.Y);
                                pntStart.Offset(0, 3);
                                p_objGrp.DrawLine(penStrike, pntStart.X, pntStart.Y, pntStart.X + intLineWidth, pntStart.Y);

                                intUpLine++;
                            }

                            pntStart = this.GetPositionFromCharIndex(intEndCharIndex + 1);

                            int intCharAdd1 = 2;
                            while (pntStart.Y == pntTemp.Y)
                            {
                                int intStartCharIndex = intEndCharIndex + intCharAdd1;
                                pntStart = this.GetPositionFromCharIndex(intStartCharIndex);
                                intCharAdd1++;
                            }

                            if (intEndIndex + 1 < this.Text.Length)
                            {
                                Point pntEnd = this.GetPositionFromCharIndex(intEndIndex + 1);

                                if (pntEnd.Y == pntStart.Y)
                                {
                                    intLineWidth = pntEnd.X - pntStart.X;
                                }
                                else
                                    intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X + (int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(), this.Font).Width - 5 - pntStart.X;
                            }
                            else
                                intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X + (int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(), this.Font).Width - 5 - pntStart.X;

                            this.m_mthAddViewInfo(pntStart, intLineWidth, m_intFontHeight, intIndex, 2);
                            pntStart.Offset(0, intOffset);
                            p_objGrp.DrawLine(penStrike, pntStart.X, pntStart.Y, pntStart.X + intLineWidth, pntStart.Y);
                            pntStart.Offset(0, 3);
                            p_objGrp.DrawLine(penStrike, pntStart.X, pntStart.Y, pntStart.X + intLineWidth, pntStart.Y);

                            #endregion
                        }

                        break;
                    }
                    intIndex++;
                }
                #endregion

                penStrike.Dispose();
            }
            catch (Exception err)
            {
                string strErr = err.Message;
            }
        }
        #endregion

        #region 画点
        /// <summary>
        /// 画点
        /// </summary>
        /// <param name="p_objGrp"></param>
        private void m_mthDrawPoint(Graphics p_objGrp)
        {
            try
            {
                Form frmParent = this.FindForm();
                if (frmParent is IRichTextStyle)
                {
                    if (((IRichTextStyle)frmParent).blnShowElementRedPoint)
                    {
                        Pen penPoint = new Pen(Color.Red);
                        SolidBrush brushPoint = new SolidBrush(Color.Red);
                        foreach (EntityMedicalTerm objElement in this.m_lstMedicalTerm)
                        {
                            if (string.IsNullOrEmpty(objElement.UserID) || objElement.UserID == "8888")
                            {
                                Point pntEnd = this.m_ptGetCharPositionRightDown(objElement.EndIndex, p_objGrp);
                                p_objGrp.DrawEllipse(penPoint, pntEnd.X - 10, pntEnd.Y + 7, 3, 3);
                                p_objGrp.FillEllipse(brushPoint, pntEnd.X - 10, pntEnd.Y + 7, 3, 3);
                            }
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        #region 校验元素值
        /// <summary>
        /// 校验元素值
        /// </summary>
        private void m_mthCheckElement()
        {
            EntityMedicalTerm objElement = null;
            string strContent = this.Text;
            for (int i = this.m_lstMedicalTerm.Count - 1; i >= 0; i--)
            {
                objElement = this.m_lstMedicalTerm[i];
                if (objElement.StartIndex > strContent.Length || objElement.EndIndex > strContent.Length)
                {
                    this.m_lstMedicalTerm.Remove(objElement);
                    continue;
                }

                if (objElement.StartIndex + objElement.Value.Trim().Length <= strContent.Length)
                {
                    if (objElement.Value.Trim() != strContent.Substring(objElement.StartIndex, objElement.Value.Trim().Length))
                    {
                        this.m_lstMedicalTerm.Remove(objElement);
                    }
                }
            }
        }
        #endregion

        #endregion

        #region 处理用户交互文本
        /// <summary>
        /// 处理用户交互文本
        /// </summary>
        /// <param name="p_chrOle"></param>
        /// <param name="p_chrNew"></param>
        private void m_mthReplaceChar(char p_chrOle, char p_chrNew)
        {
            lock (s_rtbRTFReplace)
            {
                s_rtbRTFReplace.Rtf = this.Rtf;
                s_rtbRTFReplace.Text = s_rtbRTFReplace.Text.Replace(p_chrOle, p_chrNew);

                IsReplaceRtf = true;
                this.Rtf = s_rtbRTFReplace.Rtf;
                intTextLength = this.Text.Length;
                IsReplaceRtf = false;
            }
        }

        /// <summary>
        /// 用户选择了文本后再交互 , Handle
        /// </summary>
        private void m_mthHandleSelectedChanged()
        {
            /*
             * 用户在选择了文本后进行交互。
             * 存在两种情况：
             * 把所有选择的文本全部删除；
             * 用新的文本（长度不定）代替选择的文本。
             */
            if (this.m_blnInitSetXml) return;
            int intDiffLen = this.TextLength - m_intPreviouslyLen;
            if (intDiffLen == 0)
            {
                if (this.m_blnSetFontPropertyFlag) return;
            }
            else
            {
                this.m_blnSetFontPropertyFlag = false;
            }

            m_blnCanSelectedChanged = false;

            this.m_intCurrentCursorIndex = m_intSelectedTextStartIndex;
            if (-1 * intDiffLen == m_intSelectedTextLength)
            {
                //删除选择                
                m_blnIsBackspace = false;
                m_mthHandleDelete(m_intSelectedTextLength);
            }
            else if (-1 * intDiffLen < m_intSelectedTextLength)
            {
                //替换
                this.m_mthAdjustContentPosition_Delete(m_intSelectedTextStartIndex, m_intSelectedTextLength, intDiffLen);
                this.m_mthAdjustElementPosition_Delete(m_intSelectedTextStartIndex, m_intSelectedTextLength, intDiffLen);
                //if (this.m_blnIsBackspace)
                //    this.AdjustElementPosition_DeleteBath(this.m_intSelectedTextStartIndex + 1, Math.Abs(intDiffLen));
                //else
                //    this.AdjustElementPosition_DeleteBath(this.m_intSelectedTextStartIndex, Math.Abs(intDiffLen));

                if (intDiffLen != 0)
                {
                    int intTempStartIndex = SelectionStart;
                    this.SelectionStart = m_intCurrentCursorIndex;
                    this.SelectionLength = m_intSelectedTextLength + intDiffLen;
                    this.SelectionColor = m_clrOldPartInsertText;
                    this.SelectionFont = this.Font;
                    this.SelectionCharOffset = 0;
                    this.SelectionStart += this.SelectionLength;
                    this.SelectionLength = 0;
                }
            }

            m_blnIsSelectedChanged = false;
            m_blnCanSelectedChanged = true;
        }

        /// <summary>
        /// 处理用户没有选择文本而进行的交互
        /// </summary>
        private void m_mthHandleNotSelectedChanged()
        {
            /*
             * 用户没有选择文本的情况下的交互。
             * 存在两种情况：
             * 文本变长：光标后添加；
             * 文本变短：从光标开始向前或向后删除。
             */
            if (this.m_blnInitSetXml || this.m_blnInsertEmpty) return;
            int intDiffLen = this.TextLength - m_intPreviouslyLen;
            if (intDiffLen > 0)
            {
                //插入
                m_mthHandleInsert(intDiffLen);
            }
            else
            {
                //删除
                m_mthHandleDelete(-1 * intDiffLen);
            }
        }

        /// <summary>
        /// 分割元素
        /// </summary>
        /// <param name="p_strContent"></param>
        /// <returns></returns>
        private string[] SplitElement(string p_strContent)
        {
            char[] chrArr = new char[] { ',', ';', '.', '，', '；', '。' };

            return p_strContent.Split(chrArr);
        }

        private bool IsDealIdeaCol
        {
            get
            {
                return (!string.IsNullOrEmpty(ItemName) && ItemName.ToLower() == "dealidea" ? true : false);
            }
        }

        /// <summary>
        /// 检查门诊元素
        /// </summary>
        /// <returns></returns>
        private bool CheckOpCallDealIdeaElement()
        {
            if (!IsDealIdeaCol)
            {
                return false;
            }

            if (this.SelectionLength == 0)
            {
                return m_blnCursorPositionToTermOpCall(this.m_intCurrentCursorIndex);
            }

            bool blnRet = false;
            int intStartIndex = this.m_intCurrentCursorIndex;
            int intEndIndex = intStartIndex + this.SelectionLength;
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if ((obj.StartIndex < intStartIndex && intStartIndex <= obj.EndIndex) ||
                    (obj.StartIndex > intStartIndex && obj.StartIndex < intEndIndex) ||
                    (obj.EndIndex > intStartIndex && obj.EndIndex <= intEndIndex) ||
                    (obj.StartIndex <= intStartIndex && intEndIndex <= obj.EndIndex) ||
                    (obj.EndIndex >= intStartIndex && obj.EndIndex < intEndIndex) ||
                    (obj.StartIndex <= intStartIndex && obj.EndIndex <= intEndIndex && obj.EndIndex >= intStartIndex))
                {
                    if (obj.TID.ToLower() == OP_Call)
                        blnRet = true;
                    break;
                }
                else
                {
                    if (this.m_blnMuliSelectedFlag)
                    {
                        if (obj.StartIndex == intStartIndex)
                        {
                            if (obj.TID.ToLower() == OP_Call)
                                blnRet = true;
                            break;
                        }
                    }
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            return blnRet;
        }

        /// <summary>
        /// 检查门诊处理元素
        /// </summary>
        /// <param name="p_intIndex"></param>
        /// <returns></returns>
        private bool CheckOpCallDealIdeaElement(int p_intIndex)
        {
            if (IsDealIdeaCol)
            {
                foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
                {
                    if (obj.TID.ToLower() == OP_Call && obj.EndIndex + 1 == p_intIndex)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #region m_mthHandleInsert
        /// <summary>
        /// 处理添加
        /// </summary>
        /// <param name="p_intNewLen">新添加的长度</param>
        private void m_mthHandleInsert(int p_intNewLen)
        {
            //int intCurrIndex = this.m_intCurrentCursorIndex;
            //if (p_intNewLen != 0)
            //{
            //    this.m_mthAdjustDSTPosition();
            //    this.m_mthAdjustTermPosition();
            //}

            //设置新插入部分的颜色
            bool blnTempCanSel = m_blnCanSelectedChanged;
            m_blnCanSelectedChanged = false;

            int intTempStartIndex = this.SelectionStart;
            int intElementInsertPostion = intTempStartIndex - p_intNewLen;
            this.SelectionLength = 0;

            if (this.IsInsertPatElement)
            {
                this.IsInsertPatElement = false;
            }
            else
            {
                if (!this.IsInsertNorElement && this.CursorPositionToTerm_Midd(intElementInsertPostion) && IsAllowElementFreeEdit)
                {
                    this.SelectionStart = intElementInsertPostion;
                    this.SelectionLength = p_intNewLen;
                    this.SelectionColor = ElementEditColor;
                    this.SelectionFont = FntElement;
                    this.SelectionCharOffset = 0;
                    this.AdjustElementPosition_Insert(intElementInsertPostion, p_intNewLen);
                }
                else if (!this.IsInsertNorElement && this.CursorPositionToTerm_End(intElementInsertPostion) && !CheckOpCallDealIdeaElement(intElementInsertPostion) && IsAllowElementFreeEdit)
                {
                    string strContent = this.Text.Substring(intElementInsertPostion, p_intNewLen);
                    List<string> lstArr = SplitElement(strContent).ToList();
                    if (lstArr.Count == 1 && lstArr[0] != string.Empty && IsAllowElementFreeEdit)
                    {
                        this.SelectionStart = intElementInsertPostion;
                        this.SelectionLength = p_intNewLen;
                        this.SelectionColor = ElementEditColor;
                        this.SelectionFont = FntElement;
                        this.SelectionCharOffset = 0;
                        this.AdjustElementPosition_Insert(intElementInsertPostion, p_intNewLen);
                    }
                    else if (lstArr.Count > 1)
                    {
                        if (lstArr[0] == string.Empty)
                        {
                            this.SelectionStart = intElementInsertPostion;
                            this.SelectionLength = p_intNewLen;
                            this.SelectionColor = m_clrOldPartInsertText;
                            this.SelectionFont = this.Font;
                            this.SelectionCharOffset = 0;
                        }
                        else
                        {
                            if (IsAllowElementFreeEdit)
                            {
                                this.SelectionStart = intElementInsertPostion;
                                this.SelectionLength = lstArr[0].Length;
                                this.SelectionColor = ElementEditColor;
                                this.SelectionFont = FntElement;
                                this.SelectionCharOffset = 0;
                                this.AdjustElementPosition_Insert(intElementInsertPostion, lstArr[0].Length);
                            }

                            this.SelectionStart = intElementInsertPostion + lstArr[0].Length;
                            this.SelectionLength = p_intNewLen - lstArr[0].Length;
                            this.SelectionColor = m_clrOldPartInsertText;
                            this.SelectionFont = this.Font;
                            this.SelectionCharOffset = 0;
                        }
                    }
                }
                else
                {
                    if (intElementInsertPostion < 0) intElementInsertPostion = this.SelectionStart;
                    this.SelectionStart = intElementInsertPostion; // m_intCurrentCursorIndex;
                    this.SelectionLength = p_intNewLen;
                    this.SelectionColor = m_clrOldPartInsertText;
                    this.SelectionFont = this.Font;
                    this.SelectionCharOffset = 0;
                }
            }
            this.SelectionLength = 0;
            this.SelectionStart = intTempStartIndex;

            this.m_blnCanSelectedChanged = blnTempCanSel;
            this.m_mthAdjustContentPosition_Insert(p_intNewLen);
            if (p_intNewLen != 0)
            {
                this.m_mthAdjustDSTPosition();
                this.m_mthAdjustTermPosition();
            }

            this.m_intCurrentCursorIndex = this.SelectionStart;
            this.Invalidate();
        }
        #endregion

        #region m_mthHandleDelete
        /// <summary>
        /// 处理删除
        /// </summary>
        private void m_mthHandleDelete(int p_intDelLen)
        {
            int intStartIndex = this.m_intCurrentCursorIndex;
            if (this.m_blnIsBackspace)
            {
                if (Math.Abs(p_intDelLen) < 2)
                {
                    intStartIndex = m_intCurrentCursorIndex - p_intDelLen;
                }
            }
            this.m_mthAdjustContentPosition_Delete(intStartIndex, p_intDelLen, -1 * p_intDelLen);
            this.m_mthAdjustElementPosition_Delete(this.SelectionStart, p_intDelLen, -1 * p_intDelLen);
            //if (this.m_blnIsBackspace)
            //    this.AdjustElementPosition_DeleteBath(this.SelectionStart + 1, Math.Abs(p_intDelLen));
            //else
            //    this.AdjustElementPosition_DeleteBath(this.SelectionStart, Math.Abs(p_intDelLen));

            if (p_intDelLen != 0)
            {
                this.m_mthAdjustDSTPosition();
                this.m_mthAdjustTermPosition();
            }

            this.m_intCurrentCursorIndex = this.SelectionStart;
        }
        #endregion

        #endregion

        #region 记录当前光标的位置和用户按键信息，判断是否需要控制

        /// <summary>
        /// 记录当前光标的位置和用户按键信息，判断是否需要控制

        /// </summary>
        private void m_mthUpdateCurrentStatus()
        {
            m_intCurrentCursorIndex = this.SelectionStart;

            if (!m_blnIsReadOnly)
            {
                ReadOnly = false;

                foreach (EntityDstInfo objDST in this.m_lstDoubleStrikeThrough)
                {
                    if (m_intCurrentCursorIndex > objDST.StartIndex
                        && m_intCurrentCursorIndex <= objDST.EndIndex)
                    {
                        ReadOnly = true;
                        break;
                    }
                }
            }
        }
        #endregion

        #region (外部调用)设置元素对象
        /// <summary>
        /// (外部调用)设置元素对象
        /// </summary>
        /// <param name="p_lstMedicalTerm"></param>
        public void m_mthSetMedicalTerm(List<EntityMedicalTerm> p_lstMedicalTerm)
        {
            if (p_lstMedicalTerm == null || p_lstMedicalTerm.Count == 0)
                return;

            try
            {
                this.stopReDraw();
                if (this.m_lstMedicalTerm == null)
                    this.m_lstMedicalTerm = new List<EntityMedicalTerm>();
                this.m_lstMedicalTerm.AddRange(p_lstMedicalTerm);
                this.m_mthAdjustTermPosition();
            }
            finally
            {
                this.reDraw();
            }
        }
        #endregion

        #region 设置系统登录人
        /// <summary>
        /// 设置系统登录人
        /// </summary>
        /// <param name="p_strLoginUserID">ID</param>
        /// <param name="p_strLoginUserName">名称</param>
        public void SetLoginUser(string p_strLoginUserID, string p_strLoginUserName)
        {
            if (this.m_objCurrentModifyUser == null)
                this.m_objCurrentModifyUser = new EntityModifyUserInfo();
            this.m_objCurrentModifyUser.UserID = p_strLoginUserID;
            this.m_objCurrentModifyUser.UserName = p_strLoginUserName;
            this.m_objCurrentModifyUser.ModifyDate = Common.Utils.Utils.ServerTime();
        }
        #endregion

        #region 设置XML文本
        /// <summary>
        /// 通过XML获取元素列表
        /// </summary>
        /// <param name="p_strXML"></param>
        /// <returns></returns>
        public List<EntityMedicalTerm> m_objGetElementListByXML(string p_strXML)
        {
            List<EntityMedicalTerm> lstElement = new List<EntityMedicalTerm>();
            XmlNodeList nodeList = Function.ReadXML(p_strXML, "MedicalTerm");
            if (nodeList != null)
            {
                EntityMedicalTerm objTerm = null;
                foreach (XmlNode node in nodeList)
                {
                    objTerm = new EntityMedicalTerm();
                    objTerm.StartIndex = int.Parse(node.Attributes["S"].Value);
                    objTerm.EndIndex = int.Parse(node.Attributes["E"].Value);
                    objTerm.TID = node.Attributes["T"].Value;
                    objTerm.Value = node.Attributes["V"].Value;
                    objTerm.UserID = node.Attributes["I"].Value;
                    objTerm.UserName = node.Attributes["N"].Value;
                    objTerm.CreateTime = DateTime.Parse(node.Attributes["D"].Value);
                    lstElement.Add(objTerm);
                }
            }
            return lstElement;
        }

        /// <summary>
        /// 调模板
        /// </summary>
        private bool m_blnLoadTemplate = false;

        public void SetXmlText(byte[] p_bytRtfArr, string p_strXML, bool p_blnLoadCaseFlag, bool p_blnTemplate)
        {
            this.m_blnLoadTemplate = true;
            this.SetXmlText(p_bytRtfArr, p_strXML, p_blnLoadCaseFlag);
            this.m_blnLoadTemplate = false;
        }
        /// <summary>
        /// 是否完成赋值
        /// </summary>
        private bool isCompleteSetXml = false;
        /// <summary>
        /// 设置XML文本状态
        /// </summary>
        private bool m_blnInitSetXml = false;
        /// <summary>
        /// 设置XML文本
        /// </summary>
        /// <param name="p_bytRtfArr"></param>
        /// <param name="p_strXML"></param>
        /// <param name="p_blnLoadCaseFlag">调用历史病历标志 true 是 false 否</param>
        public void SetXmlText(byte[] p_bytRtfArr, string p_strXML, bool p_blnLoadCaseFlag)
        {
            this.m_lstDoubleStrikeThrough = null;
            this.m_lstMedicalTerm = null;
            this.m_lstImageInfo = null;
            this.m_lstTextContentInfos = new List<EntityUserContentInfo>();
            this.m_lstDoubleStrikeThrough = new List<EntityDstInfo>();
            this.m_lstMedicalTerm = new List<EntityMedicalTerm>();
            this.m_lstImageInfo = new List<EntityImageInfo>();
            this.m_blnInitSetXml = true;
            this.ValueChangedFlag = true;
            try
            {
                if (p_bytRtfArr == null) return;
                if (GlobalCase.caseInfo != null)
                {
                    if (GlobalPatient.currPatient.Sex == "1" && this.m_strGetFirstlineCaption().Contains("月经"))
                    {
                        this.ClearText();
                        return;
                    }
                }
                this.stopReDraw();
                this.m_mthSetRtf(p_bytRtfArr);
                this.m_clrOldPartInsertText = this.m_clrDefaultViewText;
                this.m_intPreviouslyLen = this.TextLength;

                XmlNodeList nodeList = null;
                if (p_blnLoadCaseFlag)
                {
                    this.ValueChangedFlag = false;
                    nodeList = Function.ReadXML(p_strXML, "Content");
                    if (nodeList != null)
                    {
                        EntityUserContentInfo objContent = null;
                        List<EntityUserContentInfo> lstCheckContent = new List<EntityUserContentInfo>();
                        foreach (XmlNode node in nodeList)
                        {
                            objContent = new EntityUserContentInfo();
                            try
                            {
                                objContent.StartIndex = int.Parse(node.Attributes["S"].Value);
                                objContent.EndIndex = int.Parse(node.Attributes["E"].Value);
                                objContent.UserID = node.Attributes["I"].Value;
                                objContent.UserName = node.Attributes["N"].Value;
                                //objContent.m_intUserSequence = int.Parse(node.Attributes["Q"].Value);
                                objContent.ColorText = Color.FromArgb(int.Parse(node.Attributes["R"].Value));
                                objContent.ModifyDate = DateTime.Parse(node.Attributes["D"].Value);
                            }
                            catch
                            {
                                objContent.StartIndex = 0;
                                objContent.EndIndex = this.Text.Length - 1;
                                objContent.UserID = this.m_objCurrentModifyUser.UserID;
                                objContent.UserName = this.m_objCurrentModifyUser.UserName;
                                //objContent.m_intUserSequence = 1;
                                objContent.ColorText = Color.Black;
                                objContent.ModifyDate = Common.Utils.Utils.ServerTime();
                            }

                            objContent.UserInfo = new EntityModifyUserInfo();
                            objContent.UserInfo.ColorText = objContent.ColorText;
                            objContent.UserInfo.ModifyDate = objContent.ModifyDate;
                            objContent.UserInfo.UserSequence = objContent.UserSequence;
                            objContent.UserInfo.UserID = objContent.UserID;
                            objContent.UserInfo.UserName = objContent.UserName;
                            if (objContent.EndIndex > this.Text.Length - 1) continue;
                            if (lstCheckContent.Any(t => t.StartIndex == objContent.StartIndex && t.EndIndex == objContent.EndIndex && t.UserID == objContent.UserID))
                                continue;
                            else
                                lstCheckContent.Add(objContent);
                            this.m_lstTextContentInfos.Add(objContent);
                        }
                        if (!this.m_blnCompareModifier(false))
                        {
                            this.m_clrOldPartInsertText = Color.Red;
                        }
                        else
                        {
                            if (GlobalCase.caseInfo != null && GlobalCase.caseInfo.CreateDate != null)
                            {
                                int intHour = 0;
                                if (GlobalParm.dicSysParameter.ContainsKey(26))
                                {
                                    int.TryParse(GlobalParm.dicSysParameter[26], out intHour);
                                }

                                DateTime dtmCreate = GlobalCase.caseInfo.CreateDate.Value;
                                if (dtmCreate.AddHours(double.Parse(intHour.ToString())) < Common.Utils.Utils.ServerTime())
                                {
                                    bool bln41 = false;
                                    List<string> lst41 = GlobalParm.dicSysParameter[27].ToLower().Split(';').ToList();
                                    if (lst41.Count > 0)
                                    {
                                        foreach (string strP41 in lst41)
                                        {
                                            if (strP41.Split('-').Length == 2)
                                            {
                                                if (GlobalCase.caseInfo.CaseCode.ToLower().Trim() == strP41.Split('-')[0].Trim() && this.ItemName.ToLower().Trim() == strP41.Split('-')[1].Trim())
                                                {
                                                    bln41 = true;
                                                }
                                            }
                                        }
                                    }
                                    if (!bln41)
                                    {
                                        this.m_clrOldPartInsertText = Color.Red;
                                    }
                                }
                            }
                        }
                    }

                    nodeList = Function.ReadXML(p_strXML, "DDL");
                    if (nodeList != null)
                    {
                        EntityDstInfo objDST = null;
                        foreach (XmlNode node in nodeList)
                        {
                            objDST = new EntityDstInfo();
                            objDST.StartIndex = int.Parse(node.Attributes["S"].Value);
                            objDST.EndIndex = int.Parse(node.Attributes["E"].Value);
                            objDST.Value = this.m_strDSTPrefix + node.Attributes["V"].Value;
                            objDST.UserID = node.Attributes["I"].Value;
                            objDST.UserName = node.Attributes["N"].Value;
                            objDST.DeleteTime = DateTime.Parse(node.Attributes["D"].Value);

                            this.m_lstDoubleStrikeThrough.Add(objDST);
                        }
                    }
                }

                nodeList = Function.ReadXML(p_strXML, "MedicalTerm");
                if (nodeList != null)
                {
                    bool blnInterllRefFlag = false;
                    EntityIntellectionReference objRef = null;
                    List<EntityIntellectionReference> lstReference = new List<EntityIntellectionReference>();

                    List<EntityMedicalTerm> lstPatElement = new List<EntityMedicalTerm>();
                    EntityMedicalTerm objTerm = null;
                    foreach (XmlNode node in nodeList)
                    {
                        objTerm = new EntityMedicalTerm();
                        objTerm.StartIndex = int.Parse(node.Attributes["S"].Value);
                        objTerm.EndIndex = int.Parse(node.Attributes["E"].Value);
                        if (node.Attributes["A"] != null) objTerm.CaseCode = node.Attributes["A"].Value;
                        objTerm.TID = node.Attributes["T"].Value;
                        objTerm.Value = node.Attributes["V"].Value;
                        objTerm.UserID = "8888"; // node.Attributes["I"].Value;
                        //objTerm.m_strUserName = node.Attributes["N"].Value;
                        //objTerm.m_dtmCreateTime = DateTime.Parse(node.Attributes["D"].Value);

                        if (objTerm.TID == "PatInfo" && this.FormTypeName != c_strFormClass)
                        {
                            objRef = new EntityIntellectionReference();
                            objRef.ClassID = 1;
                            objRef.StartIndex = objTerm.StartIndex;
                            objRef.Len = objTerm.Value.Trim().Length;
                            objRef.CaseColCode = string.Empty;
                            objRef.Text = objTerm.Value.Trim();//objTerm.m_strValue.Substring(1, objTerm.m_strValue.Length - 2);

                            lstReference.Add(objRef);
                        }
                        else if (objTerm.TID.StartsWith("Intellection") && this.FormTypeName != c_strFormClass)
                        {
                            objRef = new EntityIntellectionReference();
                            objRef.ClassID = 2;
                            objRef.StartIndex = objTerm.StartIndex;
                            objRef.Len = objTerm.Value.Trim().Length;
                            objRef.CaseColCode = objTerm.TID.Replace("Intellection;", "");
                            objRef.Text = "{" + objTerm.Value.Trim() + "}";//"{" + objTerm.m_strValue.Substring(1, objTerm.m_strValue.Length - 2) + "}";
                            objRef.OrgText = objRef.Text;

                            lstReference.Add(objRef);
                            blnInterllRefFlag = true;
                        }
                        else
                        {
                            this.m_lstMedicalTerm.Add(objTerm);
                        }
                    }
                    if (lstReference.Count > 0)
                    {
                        // 20150627
                        //if (blnInterllRefFlag)
                        //{
                        //    clsProxyUniversal proxy = new clsProxyUniversal();
                        //    proxy.proxyUniversal.m_intIntellectionRefCheck(string.Empty, clsGlobalPatient.objCurrentPatient.intRegisterID, ref lstReference);
                        //    proxy = null;
                        //}
                        lstReference.Sort();

                        int pos = -1;
                        string strText = string.Empty;
                        string strCaseColCode = string.Empty;
                        string strCaseCode = string.Empty;
                        string strColCode = string.Empty;
                        for (int i = lstReference.Count - 1; i >= 0; i--)
                        {
                            this.SelectionStart = lstReference[i].StartIndex;
                            this.SelectionLength = lstReference[i].Len;
                            this.SelectionColor = m_clrOldPartInsertText;
                            this.SelectionFont = this.Font;
                            this.SelectionCharOffset = 0;

                            strText = lstReference[i].Text;
                            if (strText == lstReference[i].OrgText)
                            {
                                RichTextBoxPlus.InsertText(this, string.Empty);
                            }
                            else
                            {
                                if (lstReference[i].ClassID == 1)
                                {
                                    RichTextBoxPlus.InsertText(this, this.m_strGetPatElementInfo(strText));
                                }
                                else if (lstReference[i].ClassID == 2)
                                {
                                    strCaseColCode = lstReference[i].CaseColCode;
                                    strCaseCode = strCaseColCode.Split(';')[0];
                                    strColCode = strCaseColCode.Split(';')[1];
                                    strText = uiHelper.CaseColumnContent(strCaseCode, strColCode, strText);
                                    RichTextBoxPlus.InsertText(this, strText);
                                }
                            }
                        }
                        this.SelectionLength = 0;
                    }
                    this.m_mthSetMedicalTermColor(0);
                }

                //简校验.历史数据.临时
                this.m_mthCheckElement();
                //临时处理.针对格式反复的模板
                if (!p_blnLoadCaseFlag && this.Text.Trim().Length > 0 && !string.IsNullOrEmpty(this.FirstlineCaption))
                {
                    if (!this.Text.StartsWith(this.FirstlineCaption))
                    {
                        int pos = -1;
                        string strSign = string.Empty;
                        for (int s = 0; s < 2; s++)
                        {
                            if (s == 0) strSign = ":";
                            else if (s == 1) strSign = "：";

                            pos = this.Text.IndexOf(strSign);
                            if (pos >= 0 && pos < 12)
                            {
                                if (this.Text.Substring(0, pos + 1) != this.FirstlineCaption)
                                {
                                    this.m_mthAdjustFirstCaption(this.FirstlineCaption, pos + 1);
                                }
                                break;
                            }
                        }
                    }
                }
                this.m_mthSetCaptionBlod();
                this.AdjustOldElementStyle();

                this.Invalidate();
            }
            catch (Exception objEx)
            {
                DialogBox.Msg(objEx.Message, MessageBoxIcon.Information, uiHelper.frmCurr);
                //throw objEx;
            }
            finally
            {
                this.m_blnInitSetXml = false;
                isCompleteSetXml = true;
                this.reDraw();
            }
        }

        private void SetTextLength()
        {
            if (GlobalParm.dicSysParameter != null && GlobalCase.caseInfo != null && GlobalParm.dicSysParameter.ContainsKey(29))
            {
                List<string> lstColLimit = GlobalParm.dicSysParameter[29].ToLower().Split(';').ToList();
                if (lstColLimit.Count > 0)
                {
                    string[] sarr = null;
                    foreach (string str in lstColLimit)
                    {
                        sarr = str.Split('-');
                        if (sarr.Length == 3)
                        {
                            if (sarr[0] == GlobalCase.caseInfo.CaseCode.ToLower() && sarr[1] == this.ItemName.ToLower())
                            {
                                int intFontNums = 0;
                                int.TryParse(sarr[2], out intFontNums);
                                if (intFontNums > 0)
                                {
                                    this.MaxLength = intFontNums;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 调整旧元素样式
        /// </summary>
        private void AdjustOldElementStyle()
        {
            if (this.m_lstMedicalTerm.Count > 0)
            {
                bool blnStatus = false;
                using (ctlRichTextBox rich = new ctlRichTextBox())
                {
                    rich.Text = this.Text;
                    rich.Rtf = this.Rtf;

                    EntityMedicalTerm obj = null;
                    this.m_lstMedicalTerm.Sort();
                    for (int i = m_lstMedicalTerm.Count - 1; i >= 0; i--)
                    {
                        obj = m_lstMedicalTerm[i];
                        if (obj.Value.StartsWith("[") && obj.Value.EndsWith("]"))
                        {
                            rich.Select(obj.EndIndex, 1);
                            RichTextBoxPlus.InsertText(rich, "");

                            rich.Select(obj.StartIndex, 1);
                            RichTextBoxPlus.InsertText(rich, "");

                            obj.Value = obj.Value.Substring(1);
                            obj.Value = obj.Value.Substring(0, obj.Value.Length - 1);
                            blnStatus = true;
                        }
                    }
                    this.Rtf = rich.Rtf;
                }

                if (blnStatus)
                {
                    this.m_mthAdjustTermPosition();
                }
            }
        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="p_strElementName"></param>
        /// <returns></returns>
        private string m_strGetPatElementInfo(string p_strElementName)
        {
            string strInfo = string.Empty;
            foreach (EnumPatientInfoType enPatInfo in Enum.GetValues(typeof(EnumPatientInfoType)))
            {
                if (enPatInfo.ToString() == p_strElementName) strInfo = PatientInfoHelper.GetTypePatientInfo(enPatInfo);
            }
            return strInfo;
        }
        #endregion

        #region 获取XML文本
        /// <summary>
        /// 获取XML文本
        /// </summary>
        /// <returns>XML文本</returns>
        public string GetXmlText()
        {
            this.m_mthFinishEdit();
            return this.m_strGetXmlFromInfo(this.m_lstDoubleStrikeThrough, this.m_lstModifyUsers, this.m_lstTextContentInfos, this.m_lstMedicalTerm, null);
        }

        /// <summary>
        /// 获取XML文本
        /// </summary>
        /// <param name="p_dtmCaseWriteDate"></param>
        /// <returns></returns>
        public string GetXmlText(DateTime? p_dtmCaseWriteDate)
        {
            this.m_mthFinishEdit();
            return this.m_strGetXmlFromInfo(this.m_lstDoubleStrikeThrough, this.m_lstModifyUsers, this.m_lstTextContentInfos, this.m_lstMedicalTerm, p_dtmCaseWriteDate);
        }

        #endregion

        #region 获取XML文本
        /// <summary>
        /// 获取XML文本
        /// </summary>
        /// <returns>XML文本</returns>
        public string m_strGetTraceXmlText()
        {
            this.m_mthFinishEdit();
            List<EntitySuperSubScript> lstScript = new List<EntitySuperSubScript>();
            List<EntityFontColor> lstFontColor = new List<EntityFontColor>();
            List<EntityFontBold> lstFontBold = new List<EntityFontBold>();
            List<EntityFontItalic> lstFontItalic = new List<EntityFontItalic>();
            List<EntityFontUnderLine> lstFontUnderLine = new List<EntityFontUnderLine>();

            lock (s_rtbRTFReplace)
            {
                System.Windows.Forms.RichTextBox txtTemp = s_rtbRTFReplace;

                int i = 0;
                txtTemp.Rtf = this.Rtf;

                #region 上下标
                txtTemp.SelectionStart = 0;
                txtTemp.SelectionLength = 1;

                i = 0;
                while (i < txtTemp.Text.Length)
                {
                    txtTemp.SelectionStart = i;
                    int intStart = i;
                    int intOffset = txtTemp.SelectionCharOffset;
                    while (i + 1 < txtTemp.Text.Length)
                    {
                        txtTemp.SelectionStart = i + 1;
                        if (txtTemp.SelectionCharOffset == intOffset)
                            i++;
                        else
                            break;
                    }
                    int intEnd = i;

                    if (intOffset == c_intSuperScriptCharOffSet || intOffset == c_intSubScriptCharOffSet)
                    {
                        EntitySuperSubScript objScript = new EntitySuperSubScript();
                        objScript.CharOffset = intOffset;
                        objScript.Index = intStart;
                        objScript.Value = txtTemp.Text.Substring(intStart, intEnd - intStart + 1);
                        lstScript.Add(objScript);
                    }

                    i++;
                }
                #endregion

                #region 字体颜色
                txtTemp.SelectionStart = 0;
                txtTemp.SelectionLength = 1;

                i = 0;
                Color clrTmp;
                while (i < txtTemp.Text.Length)
                {
                    txtTemp.SelectionStart = i;
                    int intStart = i;
                    clrTmp = txtTemp.SelectionColor;
                    while (i + 1 < txtTemp.Text.Length)
                    {
                        txtTemp.SelectionStart = i + 1;
                        if (txtTemp.SelectionColor == clrTmp)
                            i++;
                        else
                            break;
                    }
                    int intEnd = i;

                    if (clrTmp != Color.Black)
                    {
                        EntityFontColor objFontColor = new EntityFontColor();
                        objFontColor.Index = intStart;
                        objFontColor.ColorValue = clrTmp;
                        objFontColor.TxtValue = txtTemp.Text.Substring(intStart, intEnd - intStart + 1);
                        lstFontColor.Add(objFontColor);
                    }

                    i++;
                }
                #endregion

                #region 字体粗体
                txtTemp.SelectionStart = 0;
                txtTemp.SelectionLength = 1;

                i = 0;
                bool blnBold = false;
                while (i < txtTemp.Text.Length)
                {
                    txtTemp.SelectionStart = i;
                    int intStart = i;
                    blnBold = txtTemp.SelectionFont.Bold;
                    while (i + 1 < txtTemp.Text.Length)
                    {
                        txtTemp.SelectionStart = i + 1;
                        if (txtTemp.SelectionFont.Bold == blnBold)
                            i++;
                        else
                            break;
                    }
                    int intEnd = i;

                    if (blnBold)
                    {
                        EntityFontBold objFontBold = new EntityFontBold();
                        objFontBold.Index = intStart;
                        objFontBold.Value = txtTemp.Text.Substring(intStart, intEnd - intStart + 1);
                        lstFontBold.Add(objFontBold);
                    }

                    i++;
                }
                #endregion

                #region 字体斜体
                txtTemp.SelectionStart = 0;
                txtTemp.SelectionLength = 1;

                i = 0;
                bool blnItalic = false;
                while (i < txtTemp.Text.Length)
                {
                    txtTemp.SelectionStart = i;
                    int intStart = i;
                    blnItalic = txtTemp.SelectionFont.Italic;
                    while (i + 1 < txtTemp.Text.Length)
                    {
                        txtTemp.SelectionStart = i + 1;
                        if (txtTemp.SelectionFont.Italic == blnItalic)
                            i++;
                        else
                            break;
                    }
                    int intEnd = i;

                    if (blnItalic)
                    {
                        EntityFontItalic objFontItalic = new EntityFontItalic();
                        objFontItalic.Index = intStart;
                        objFontItalic.Value = txtTemp.Text.Substring(intStart, intEnd - intStart + 1);
                        lstFontItalic.Add(objFontItalic);
                    }

                    i++;
                }
                #endregion

                #region 字体下划线
                txtTemp.SelectionStart = 0;
                txtTemp.SelectionLength = 1;

                i = 0;
                bool blnUnderLine = false;
                while (i < txtTemp.Text.Length)
                {
                    txtTemp.SelectionStart = i;
                    int intStart = i;
                    blnUnderLine = txtTemp.SelectionFont.Underline;
                    while (i + 1 < txtTemp.Text.Length)
                    {
                        txtTemp.SelectionStart = i + 1;
                        if (txtTemp.SelectionFont.Underline == blnUnderLine)
                            i++;
                        else
                            break;
                    }
                    int intEnd = i;

                    if (blnUnderLine)
                    {
                        EntityFontUnderLine objFontUnderLine = new EntityFontUnderLine();
                        objFontUnderLine.Index = intStart;
                        objFontUnderLine.Value = txtTemp.Text.Substring(intStart, intEnd - intStart + 1);
                        lstFontUnderLine.Add(objFontUnderLine);
                    }

                    i++;
                }
                #endregion
            }

            return this.m_strGetTraceXmlFromInfo(this.m_lstDoubleStrikeThrough, this.m_lstModifyUsers, this.m_lstTextContentInfos, lstScript, lstFontColor, lstFontBold, lstFontItalic, lstFontUnderLine, this.m_lstMedicalTerm);
        }
        #endregion

        #region 重写
        /// <summary>
        /// 复制
        /// </summary>
        public new void Copy()
        {
            if (this.SelectedText == "")
            {
                return;
            }

            string strSysFlag = string.Empty;
            if (GlobalPatient.currPatient != null && this.ParentEmrControl != null)
            {
                strSysFlag = GlobalPatient.currPatient.PatientID + this.m_strSysSplit;
            }

            Clipboard.SetText(strSysFlag + this.SelectedText);
        }

        /// <summary>
        /// 控件大小改变时:仅记录左下角位置
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(System.EventArgs e)
        {
            m_pntEndVisible.X = this.Width - 5;
            m_pntEndVisible.Y = this.Height - 5;

            base.OnSizeChanged(e);

            this.m_mthSetRowSpacing();
        }

        /// <summary>
        /// 字体改变时:仅更新字体高度变量
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(System.EventArgs e)
        {
            m_intFontHeight = this.Font.Height;

            base.OnFontChanged(e);
        }

        private int intTextLength = 0;

        /// <summary>
        /// 文本内容改变时:调用各类操作
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            if (this.m_blnIniting) return;
            if (this.m_blnMouseMoving) return;
            if (this.IsReplaceRtf) return;
            //this.m_mthAutoSetTerm();

            if (this.Text.Length >= this.MaxLength && evtReachMaxLength != null)
            {
                evtReachMaxLength(this);
            }

            m_mthFireEvtTextChange(true);		//检查m_evtTextChange事件

            //不处理则退出
            if (!m_blnCanTextChanged) return;
            if (!this.m_blnInitSetXml) this.ValueChangedFlag = true;

            //是否选择文本
            if (m_blnIsSelectedChanged)
            {
                //是否可以修改选中文本
                if (m_blnCanModifySelection)
                {
                    //选择文本后进行的交互操作
                    m_mthHandleSelectedChanged();
                }
                else
                {
                    //不能修改,还原为先前的内容
                    m_blnCanSelectedChanged = false;

                    m_blnCanTextChanged = false;
                    IsReplaceRtf = true;
                    this.Rtf = m_strPrevioslyText;  //还原先前内容
                    this.intTextLength = this.Text.Length;
                    IsReplaceRtf = false;
                    m_blnCanTextChanged = true;

                    this.SelectionStart = m_intCurrentCursorIndex;	//还原光标位置

                    m_blnCanSelectedChanged = true;
                    return;
                }
            }
            else
            {
                //没有选择文本的交互操作
                m_mthHandleNotSelectedChanged();
            }

            //不允许\t
            if (this.Text.IndexOf('\t') >= 0)
            {
                m_blnCanSelectedChanged = false;
                m_blnCanTextChanged = false;
                m_mthReplaceChar('\t', ' ');
                this.SelectionStart = m_intCurrentCursorIndex;
                m_blnCanTextChanged = true;
                m_blnCanSelectedChanged = true;
            }

            //记录当前信息
            m_intPreviouslyLen = this.TextLength;
            m_intTextLenght = m_intPreviouslyLen;
            m_strPrevioslyText = this.Rtf;	//	this.Text;
            m_blnIMEInput = false;

            intTextLength = this.Text.Length;
            base.OnTextChanged(e);

            //若内容为空,则所有操作复位
            if (this.Text == string.Empty) this.ClearText();
        }

        /// <summary>
        /// 更新当前状态
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (this.SelectedText != "")
                {
                    Clipboard.SetText(this.SelectedText);
                }
            }
            if (!m_blnIMEInput) this.m_mthUpdateCurrentStatus();

            if (this.m_intAddSpaceLen > 0 && e.KeyCode == Keys.Enter)
            {
                this.SelectionStart += this.m_intAddSpaceLen;
                this.m_intAddSpaceLen = 0;
            }

            if (e.KeyCode == Keys.Enter && !this.ReadOnly)
            {
                if (IsDealIdeaCol)
                {
                    if (this.CursorPositionToTerm_Midd(this.SelectionStart))
                    {
                        base.OnKeyUp(e);
                        return;
                    }

                    int intPos = this.SelectionStart;
                    if (intPos > 2)
                    {
                        string strSub = this.Text.Substring(0, intPos);
                        if (strSub != string.Empty)
                        {
                            int ret = GetSerNo(strSub);
                            if (ret > 0)
                            {
                                this.SelectionLength = 0;
                                string s = Convert.ToString(ret + 1);
                                this.m_mthInsertText(s + "、");
                                //this.SelectionStart += s.Length + 1;
                            }
                        }
                    }
                }
                return;
            }

            base.OnKeyUp(e);
        }

        private bool m_blnCheckPositionDel()
        {
            bool blnRet = false;
            if (this.m_blnMuliSelectedFlag)
            {
                int idx1 = this.SelectionStart;
                int idx2 = this.SelectionStart + this.SelectionLength - 1;

                if (idx1 <= this.m_strGetFirstlineCaption().Length && idx2 == this.Text.Length - 1)
                {
                    blnRet = true;
                }
            }

            return blnRet;
        }

        /// <summary>
        /// Ctrl+C
        /// </summary>
        public bool KeyCtrlC { get; set; }

        /// <summary>
        /// 处理各类功能键操作
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                this.stopReDraw();

                #region 按键
                KeyCtrlC = false;
                if (!CheckValidCaptionCursor())
                {
                    ImeMode = ImeMode.Off;
                    e.Handled = true;
                    return;
                }
                this.m_mthHintConfineInfo();
                int intCurrPos = this.m_intCurrentCursorIndex;
                if (e.KeyCode == Keys.Down)
                {
                    if (!this.m_blnTableFlag && this.GetLineFromCharIndex(intCurrPos) == this.GetLineFromCharIndex(this.Text.Length - 1))
                    {
                        SendKeys.SendWait("{TAB}");
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    if (!this.m_blnTableFlag && this.GetLineFromCharIndex(intCurrPos) == this.GetLineFromCharIndex(0))
                    {
                        SendKeys.SendWait("+{TAB}");
                    }
                }
                else if (e.Control && e.KeyCode == Keys.C)
                {
                    e.Handled = true;
                    Clipboard.Clear();
                    this.Copy();
                    if (m_blnIsSelectedChanged)
                    {
                        m_blnIsSelectedChanged = false;
                    }
                    KeyCtrlC = true;
                }
                else if (e.Control && e.KeyCode == Keys.X)
                {
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.Z)
                {
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.V)
                {
                    e.Handled = true;
                    this.m_mthPaste();
                }
                else if (e.Shift && e.KeyCode == Keys.Delete)
                {
                    e.Handled = true;
                    return;
                }
                else if (e.Shift && e.KeyCode == Keys.Back)
                {
                    e.Handled = true;
                    return;
                }
                else if (e.Control && e.KeyCode == Keys.R)
                {
                    e.Handled = true;
                    return;
                }
                else if (e.KeyCode == (Keys)Enum.Parse(typeof(Keys), "219") && !this.m_blnLoadTermStatus)
                {
                    this.m_blnKey219 = true;
                }
                else if (e.KeyCode == (Keys)Enum.Parse(typeof(Keys), "221") && !this.m_blnLoadTermStatus)
                {
                    this.m_blnKey221 = true;
                }
                else if (e.Alt && e.KeyCode == Keys.V)
                {
                    this.m_mthInsertText("√");
                }

                if (e.KeyCode == Keys.ProcessKey)
                {
                    KeyChar = false;
                    this.m_blnIMEInput = true;
                }
                #endregion

                #region 位置
                bool blnDelPosition = false;
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    blnDelPosition = this.m_blnCheckPositionDel();
                }

                bool blnDST = false;
                bool blnCaption = this.m_blnCursorPositionToCaption();
                if (this.m_blnMuliSelectedFlag)
                {
                    if (IsDealIdeaCol)
                    {
                        EntityMedicalTerm objElement = CursorPositionToTerm(this.SelectionStart);
                        if (objElement != null && objElement.TID.ToLower() == OP_Call)
                        {
                            e.Handled = true;
                            return;
                        }

                    }
                    if (!blnDelPosition)
                    {
                        blnDST = this.m_blnCursorPositionToDST();
                        if (blnDST || blnCaption)
                        {
                            this.SelectionLength = 0;
                            if (e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                                e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown)
                            {
                            }
                            else
                            {
                                if (!this.ExtSpecModifyFlag)
                                {
                                    e.Handled = true;
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (this.m_blnCursorPositionToTermOpCall(intCurrPos))
                    {
                        e.Handled = true;
                        return;
                    }

                    if (this.m_blnCursorPositionToTermOpCall(intCurrPos, e.KeyCode))
                    {
                        e.Handled = true;
                        return;
                    }
                    //if (this.m_blnCursorPositionToDST(intCurrPos, e.KeyCode) || this.m_blnCursorPositionToTerm(intCurrPos, e.KeyCode))
                    blnDST = this.m_blnCursorPositionToDST(intCurrPos, e.KeyCode);
                    if (blnDST || blnCaption)
                    {
                        if (e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                            e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown)
                        {
                        }
                        else
                        {
                            if (!this.ExtSpecModifyFlag)
                            {
                                e.Handled = true;
                                return;
                            }
                        }
                    }
                }
                #endregion

                #region 删除控制

                this.m_blnIsBackspace = false;
                if (e.KeyCode == Keys.Delete || (m_blnMuliSelectedFlag && e.KeyCode == Keys.Enter))
                {
                    if (!blnDelPosition)
                    {
                        if (this.Text.Replace("\n", "").Trim() == this.SelectedText.Replace("\n", "").Trim())
                        {
                            if (m_lstDoubleStrikeThrough == null || m_lstDoubleStrikeThrough.Count == 0)
                            {
                                this.ClearText();
                            }
                            else
                            {
                                e.Handled = true;
                            }
                            return;
                        }
                        if (!this.m_blnCheckCursorValidPostion2() && !this.ExtSpecModifyFlag)
                        {
                            e.Handled = true;
                            return;
                        }
                    }

                    int intCurrIndex = intCurrPos;
                    foreach (EntityUserContentInfo objContentInfo in this.m_lstTextContentInfos)
                    {
                        if (objContentInfo.StartIndex <= m_intCurrentCursorIndex && m_intCurrentCursorIndex <= objContentInfo.EndIndex)
                        {
                            if (!m_blnCompareModifier(m_objCurrentModifyUser, objContentInfo.UserInfo) && !this.ExtSpecModifyFlag)
                            {
                                e.Handled = true;
                                return;
                            }
                            break;
                        }
                        else if (objContentInfo.EndIndex < m_intCurrentCursorIndex)
                        {
                            continue;
                        }
                        else
                        {
                            if ((m_intCurrentCursorIndex != 0) && (!m_blnCompareModifier(m_objCurrentModifyUser, objContentInfo.UserInfo)) && !this.ExtSpecModifyFlag)
                            {
                                e.Handled = true;
                                return;
                            }
                            break;
                        }
                    }

                    if (this.m_blnMuliSelectedFlag)
                    {

                    }
                    else
                    {
                        foreach (EntityDstInfo obj in this.m_lstDoubleStrikeThrough)
                        {
                            if ((intCurrIndex + 1 > obj.StartIndex && intCurrIndex + 1 < obj.EndIndex) && !this.ExtSpecModifyFlag)
                            {
                                e.Handled = true;
                                return;
                            }
                        }

                        if (!IsAllowElementFreeEdit)
                        {
                            for (int i = 0; i < this.m_lstMedicalTerm.Count; i++)
                            {
                                //if (intCurrIndex + 1 >= this.m_lstMedicalTerm[i].m_intStartIndex && intCurrIndex <= this.m_lstMedicalTerm[i].m_intEndIndex)
                                if ((intCurrIndex >= this.m_lstMedicalTerm[i].StartIndex && intCurrIndex <= this.m_lstMedicalTerm[i].EndIndex) && !this.ExtSpecModifyFlag)
                                {
                                    this.m_mthDelTerm(i);
                                    e.Handled = true;
                                    return;
                                }
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Back)
                {
                    if (!blnDelPosition)
                    {
                        if (this.Text.Replace("\n", "").Trim() == this.SelectedText.Replace("\n", "").Trim())
                        {
                            if (m_lstDoubleStrikeThrough == null || m_lstDoubleStrikeThrough.Count == 0)
                            {
                                this.ClearText();
                            }
                            else
                            {
                                e.Handled = true;
                            }
                            return;
                        }

                        if (!this.m_blnCheckCursorValidPostion2() && !this.ExtSpecModifyFlag)
                        {
                            e.Handled = true;
                            return;
                        }
                    }

                    int intCurrIndex = intCurrPos;
                    m_blnIsBackspace = true;
                    foreach (EntityUserContentInfo objContentInfo in this.m_lstTextContentInfos)
                    {
                        if (objContentInfo.StartIndex - 1 <= m_intCurrentCursorIndex && m_intCurrentCursorIndex <= objContentInfo.EndIndex + 1)
                        {
                            if (!m_blnCompareModifier(m_objCurrentModifyUser, objContentInfo.UserInfo) && !this.ExtSpecModifyFlag)
                            {
                                e.Handled = true;
                                return;
                            }
                            break;
                        }
                        else if (objContentInfo.EndIndex < m_intCurrentCursorIndex - 1)		//删除光标的前一个字符
                        {
                            continue;
                        }
                        else
                        {
                            if (((m_intCurrentCursorIndex != 0) && (!m_blnCompareModifier(m_objCurrentModifyUser, objContentInfo.UserInfo))) && !this.ExtSpecModifyFlag)
                            {
                                e.Handled = true;
                                return;
                            }
                            break;
                        }
                    }

                    if (this.m_blnMuliSelectedFlag)
                    {
                    }
                    else
                    {
                        foreach (EntityDstInfo obj in this.m_lstDoubleStrikeThrough)
                        {
                            if ((intCurrIndex - 1 > obj.StartIndex && intCurrIndex - 1 <= obj.EndIndex) && !this.ExtSpecModifyFlag)
                            {
                                e.Handled = true;
                                return;
                            }
                        }

                        if (!IsAllowElementFreeEdit)
                        {
                            for (int i = 0; i < this.m_lstMedicalTerm.Count; i++)
                            {
                                if ((intCurrIndex - 1 >= this.m_lstMedicalTerm[i].StartIndex && intCurrIndex - 1 <= this.m_lstMedicalTerm[i].EndIndex) && !this.ExtSpecModifyFlag)
                                {
                                    this.m_mthDelTerm(i);
                                    e.Handled = true;
                                    return;
                                }
                            }
                        }
                    }

                    if ((!string.IsNullOrEmpty(this.FirstlineCaption)) && !this.ExtSpecModifyFlag)
                    {
                        //if (this.RowShrinkdigit + this.FirstlineCaption.Length == intCurrIndex)
                        if (this.FirstlineCaption.Length == intCurrIndex)
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
                else if (e.KeyCode == Keys.Insert)
                {
                    e.Handled = true;
                    return;
                }
                if (blnDelPosition)
                {
                    e.Handled = true;
                    this.ClearText();
                    return;
                }
                #endregion

                #region 元素
                if (e.KeyCode == Keys.F5)
                {
                    if (this.m_blnCursorPositionToTerm2(intCurrPos))
                        this.m_mthUpdateElement();
                    else
                        this.m_mthAddMedicalTerm();
                }
                bool blnElement = ((!IsAllowElementFreeEdit && this.m_blnCursorPositionToTerm2(intCurrPos)) ? true : false);
                if (blnElement)
                {
                    this.SelectionLength = 0;

                    if (e.KeyCode == Keys.ShiftKey)
                    {
                        this.ImeMode = ImeMode.Off;
                        e.Handled = true;
                        DialogBox.Msg("元素不可以编辑。");
                        return;
                    }

                    if (e.KeyCode == Keys.F5)// Keys.Space)
                    {
                        this.m_mthUpdateElement();
                    }
                    else if (e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                            e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown)
                    { }
                    else
                    {
                        e.Handled = true;
                        return;
                    }
                }
                else
                {
                    //if (e.KeyCode == Keys.F5)
                    //{
                    //    this.m_mthAddMedicalTerm();
                    //}
                }
                if (e.Control && (blnDST || blnElement || blnCaption))
                {
                    e.Handled = true;
                    return;
                }
                //ReadOnly = false;

                #endregion

                if (e.KeyCode == Keys.Enter)
                {
                    if (!Multiline)
                    {
                        SendKeys.SendWait("{TAB}");
                        return;
                    }

                    if (!this.ReadOnly)
                    {
                        this.m_mthAddSpace(false);
                        return;
                    }
                }

                if (e.KeyCode == Keys.Left)
                {
                    if (!string.IsNullOrEmpty(this.FirstlineCaption))
                    {
                        if (this.SelectionStart <= this.FirstlineCaption.Length)
                        {
                            this.SelectionStart = this.FirstlineCaption.Length;
                            e.Handled = true;
                            return;
                        }
                    }
                }
                base.OnKeyDown(e);
            }
            finally
            {
                this.reDraw();
            }
        }

        private bool IsInt(string val)
        {
            try
            {
                int.Parse(val);
                return true;
            }
            catch
            {
                return false;
            }
            //return Regex.IsMatch(val, @"^[+-]?\d*$");
        }

        private int GetSerNo(string val)
        {
            string str = string.Empty;
            string strNum = string.Empty;

            int pos = val.LastIndexOf("、");
            for (int i = pos - 1; i >= 0; i--)
            {
                str = val.Substring(i, 1);
                if (IsInt(str))
                {
                    for (int k = i; k >= 0; k--)
                    {
                        str = val.Substring(k, 1);
                        if (IsInt(str))
                        {
                            strNum = str + strNum;
                            if (k == 0)
                            {
                                int ret = 0;
                                int.TryParse(strNum, out ret);
                                return ret;
                            }
                        }
                        else
                        {
                            int ret = 0;
                            int.TryParse(strNum, out ret);
                            return ret;
                        }
                    }
                }
                else
                {
                    i = val.LastIndexOf("、", i);
                }
            }
            return 0;
        }

        #region 修改者比较
        /// <summary>
        /// 修改者比较
        /// </summary>
        /// <param name="p_objModifierA"></param>
        /// <param name="p_objModifierB"></param>
        /// <returns></returns>
        private bool m_blnCompareModifier(EntityModifyUserInfo p_objModifierCur, EntityModifyUserInfo p_objModifierOrg)
        {
            bool blnReturn = true;
            try
            {
                if (p_objModifierCur == null || p_objModifierOrg == null)
                    return false;

                bool blnAllowModifyOther = false;
                if (GlobalParm.dicSysParameter.ContainsKey(73))
                {
                    blnAllowModifyOther = GlobalParm.dicSysParameter[74].ToString().Trim() == "1" ? true : false;
                }

                int intHour = 0;
                if (GlobalParm.dicSysParameter.ContainsKey(26))
                {
                    int.TryParse(GlobalParm.dicSysParameter[26], out intHour);
                }

                if (p_objModifierOrg.ModifyDate.AddHours(double.Parse(intHour.ToString())) <= Common.Utils.Utils.ServerTime())
                {
                    if (blnAllowModifyOther)
                        return true;
                    else
                        return false;
                }

                if (p_objModifierCur.UserID != p_objModifierOrg.UserID)
                {
                    return false;
                }

                if (p_objModifierOrg.ModifyDate <= GlobalCase.caseInfo.CreateDate)
                {
                    if (!this.m_blnCompareModifier(true)) return false;
                }

                if (p_objModifierCur.UserID == p_objModifierOrg.UserID && p_objModifierCur.ModifyDate == p_objModifierOrg.ModifyDate)
                    return true;
                //else
                //    return this.m_blnCompareModifier(true);
            }
            catch
            {
                blnReturn = false;
            }
            return blnReturn;
        }
        /// <summary>
        /// 修改者比较
        /// </summary>
        private bool m_blnCompareModifier(bool p_blnFlag)
        {
            bool blnReturn = true;
            bool bln41 = false;
            List<string> lst41 = GlobalParm.dicSysParameter[27].ToLower().Split(';').ToList();
            if (lst41.Count > 0)
            {
                foreach (string strP41 in lst41)
                {
                    if (strP41.Split('-').Length == 2)
                    {
                        if (GlobalCase.caseInfo.CaseCode.ToLower().Trim() == strP41.Split('-')[0].Trim() && this.ItemName.ToLower().Trim() == strP41.Split('-')[1].Trim())
                        {
                            bln41 = true;
                        }
                    }
                }
            }
            if (bln41)
            {
                return true;
            }

            this.m_lstTextContentInfos.Sort();
            if (this.m_lstTextContentInfos.Count > 0)
            {
                string strCreatorID = string.Empty;
                if (this.m_blnTableFlag)
                {
                    strCreatorID = this.m_lstTextContentInfos[0].UserID;
                }
                else
                {
                    strCreatorID = GlobalCase.caseInfo.CreatorID.ToString();
                }

                if (p_blnFlag)
                {
                    if (strCreatorID == this.m_objCurrentModifyUser.UserID)
                    {
                        blnReturn = m_blnCheckUser(strCreatorID);
                    }
                }
                else
                {
                    if (strCreatorID != this.m_objCurrentModifyUser.UserID)
                    {
                        return false;
                    }
                    blnReturn = m_blnCheckUser(strCreatorID);
                }
            }
            return blnReturn;
        }
        /// <summary>
        /// 检查用户
        /// </summary>
        /// <param name="p_strEmpID"></param>
        /// <returns></returns>
        private bool m_blnCheckUser(string p_strEmpID)
        {
            bool blnReturn = true;

            if (GlobalCase.caseInfo.Signature == null || GlobalCase.caseInfo.Signature.Count == 0)
            {
                return blnReturn;
            }

            var user = from textuser in GlobalCase.caseInfo.Signature /* this.m_lstTextContentInfos */
                       where (textuser.EmpNo != p_strEmpID)
                       select textuser;
            if (user.ToArray().Length > 0)
            {
                blnReturn = false;
            }

            return blnReturn;

            //var user = from textuser in this.m_lstTextContentInfos
            //           where ((textuser.m_strUserID != strCreatorID) && 
            //                  (strCreatorID == this.m_objCurrentModifyUser.m_strUserID))
            //           select textuser;
        }

        #endregion

        /// <summary>
        /// 设置当前元素.词汇颜色
        /// </summary>
        private void CurrentElementColr()
        {
            EntityMedicalTerm objElement = CursorPositionToTerm(this.SelectionStart);
            if (objElement != null)
            {
                SetElementCurrColor(objElement);
            }
            else
            {
                ResetElementCurrColor();
            }
        }

        private bool KeyChar = false;

        /// <summary>
        /// OnKeyPress
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!CheckValidCaptionCursor())
            {
                ImeMode = ImeMode.Off;
                e.Handled = true;
                return;
            }
            if (this.m_blnMuliSelectedFlag)
            {
                bool blnElement = (!IsAllowElementFreeEdit && (this.m_blnCursorPositionToTerm() || this.m_blnCursorPositionToTerm3(this.SelectionStart, this.SelectionStart + this.SelectionLength)) ? true : false);
                if (this.m_blnCursorPositionToDST() || CheckOpCallDealIdeaElement() || this.m_blnCursorPositionToImage() ||
                    this.m_blnCursorPositionToCaption() || blnElement)
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    if (m_blnIsSelectedChanged)
                    {
                        if (m_blnCanModifySelection)
                        {
                            m_blnCanSelectedChanged = false;
                        }
                        else
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
            }
            else
            {
                if (!this.m_blnCheckCursorValidPostion())
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    if (m_blnIsSelectedChanged)
                    {
                        if (m_blnCanModifySelection)
                        {
                            m_blnCanSelectedChanged = false;
                        }
                        else
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
            }

            if ((e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
            }
            else if (/*(e.KeyChar >= '0' && e.KeyChar <= '9') ||*/ (e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z'))
            {
                KeyChar = true;
            }
            else
            {
                KeyChar = false;
            }

            base.OnKeyPress(e);

            if (intTextLength >= this.MaxLength && evtReachMaxLength != null)
            {
                evtReachMaxLength(this);
            }
        }
        /// <summary>
        /// 选择状态
        /// </summary>
        private bool m_blnSelectionStatus = false;
        /// <summary>
        /// 确定选中区域是否可以更改
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectionChanged(System.EventArgs e)
        {
            if (this.SelectionLength > 0 && this.m_blnInitEnter)
            {
                int pos = this.SelectionStart + this.SelectionLength;
                this.SelectionLength = 0;
                this.m_blnInitEnter = false;
                //this.SelectionStart = pos;
                return;
            }
            this.m_blnMuliSelectedFlag = false;
            if (this.m_blnInitFontStyle) return;
            if (this.IsInsertNorElement) return;

            if (this.SelectionLength == 0)
            {
                //this.SelectionFont = this.Font;
                this.SelectionCharOffset = 0;
                if (!KeyChar && !CursorPositionToTerm_Midd(this.SelectionStart))
                {
                    this.ImeMode = ImeMode.OnHalf;
                }
                else
                {
                    this.ImeMode = ImeMode.Off;
                }
            }
            else
            {
                if (this.m_blnCursorPositionToCaption(this.SelectionStart))
                {
                    this.SelectionLength = 0;
                    this.SelectionStart = this.m_strGetFirstlineCaption().Length;
                    this.m_intCurrentCursorIndex = this.SelectionStart;
                    //this.SelectionFont = this.Font;
                    this.SelectionCharOffset = 0;
                }
                else
                {
                    this.m_blnSelectionStatus = true;
                    this.m_blnMuliSelectedFlag = true;
                    this.m_intCurrentCursorIndex = this.SelectionStart;
                }
            }

            this.m_blnCanModifySelection = true;
            if (m_blnCanSelectedChanged)
            {
                m_intSelectedTextStartIndex = this.SelectionStart;
                m_intSelectedTextLength = this.SelectionLength;

                //指定是否选择了文本
                m_blnIsSelectedChanged = (this.SelectionLength > 0);
                if (this.m_blnUpdateElementFlag) this.m_blnIsSelectedChanged = false;

                #region 用户选中了文本时,设置是否可以修改选择文本的标志
                if (m_blnIsSelectedChanged)
                {
                    int intEndSeletedIndex = m_intSelectedTextStartIndex + m_intSelectedTextLength - 1;
                    for (int i = 0; i < this.m_lstTextContentInfos.Count; i++)
                    {
                        EntityUserContentInfo objContentInfo = this.m_lstTextContentInfos[i];

                        //选择文本与当前文本段有交叉
                        if ((m_intSelectedTextStartIndex >= objContentInfo.StartIndex) && (m_intSelectedTextStartIndex <= objContentInfo.EndIndex))
                        {
                            //用户不同,不能修改
                            if (objContentInfo.UserInfo != m_objCurrentModifyUser)
                            {
                                m_blnCanModifySelection = false;
                                break;
                            }

                            //选中文本属于当前文本段,允许修改
                            if ((intEndSeletedIndex >= objContentInfo.StartIndex) && (intEndSeletedIndex <= objContentInfo.EndIndex))
                            {
                                break;
                            }
                        }
                    }
                }
                #endregion
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (this.m_blnIniting) return;
            if (this.m_evtMouseEnter != null)
            {
                clsEvtRichTextBox evt = new clsEvtRichTextBox();
                evt.m_objRichTextBox = this;
                this.m_evtMouseEnter(this, evt);
            }
            base.OnMouseEnter(e);
        }

        private int m_intTmpStartIndex = 0;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (this.m_lstMedicalTerm.Count > 0)
            {
                for (int i = 0; i < this.m_lstMedicalTerm.Count; i++)
                {
                    this.m_lstMedicalTerm[i].IsChangeColor = false;
                }
                EntityMedicalTerm ele = null;
                if (this.m_blnCursorPositionToTerm2(this.SelectionStart, ref ele))
                {
                    ele.IsChangeColor = true;

                    this.m_mthSetMedicalTermColor(0);
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.m_blnIniting) return;

            if (!string.IsNullOrEmpty(OpInputLanguageName))
            {
                foreach (InputLanguage lan in InputLanguage.InstalledInputLanguages)
                {
                    if (lan.LayoutName.Contains(OpInputLanguageName))
                    {
                        InputLanguage.CurrentInputLanguage = lan;
                        OpInputLanguageName = string.Empty;
                        break;
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {
            }
            else
            {
                this.m_intCurrentCursorIndex = this.SelectionStart;
                if (!this.m_blnCheckCursorValidPostion())
                {
                    this.ImeMode = ImeMode.Off;
                    //this.ReadOnly = true;
                }
                else
                {
                    if (!KeyChar && !CursorPositionToTerm_Midd(this.SelectionStart))
                    {
                        this.ImeMode = ImeMode.OnHalf;
                    }
                    else
                    {
                        this.ImeMode = ImeMode.Off;
                    }
                }

                bool b = false;
                if (this.SelectionLength <= 1)
                {
                    b = this.m_blnCursorPositionToImage4(this.m_intCurrentCursorIndex);
                }
                if (!b && this.Text == this.m_strGetFirstlineCaption())
                {
                    this.SelectionStart = this.m_strGetFirstlineCaption().Length;
                }

                this.m_intCurrentCursorIndex = this.SelectionStart;

                CloseIcdList();
            }
            this.m_intTmpStartIndex = this.SelectionStart;
            this.m_mthSetCaptionCursor();

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (this.m_blnIniting) return;
            if (this.m_blnInitEnter && !this.m_blnMuliSelectedFlag)
            {
                this.m_blnInitEnter = false;
                if (this.SelectionLength > 0)
                {
                    int pos = this.SelectionStart + this.SelectionLength;
                    //this.SelectionLength = 0;
                    this.SelectionStart = pos;
                }
            }
            if (this.m_blnMuliSelectedFlag)
            {
                int intTmpEndIndex = this.SelectionStart + this.SelectionLength - 1;
                if (intTmpEndIndex > this.m_intTmpStartIndex)
                {
                    if (intTmpEndIndex - this.m_intTmpStartIndex + 1 > 0)
                    {
                        this.Select(this.m_intTmpStartIndex, intTmpEndIndex - this.m_intTmpStartIndex + 1);
                    }
                }
                string strText = this.Text.Substring(this.SelectionStart, this.SelectionLength);
                if (strText.Length > 2)
                {
                    if (strText.Substring(strText.Length - 2) == "][")
                    {
                        this.Select(this.SelectionStart, this.SelectionLength - 1);
                    }
                }
            }

            this.m_blnSelectionStatus = false;
            //Cursor = Cursors.Default;     // 20150703 00:10
            base.OnMouseUp(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            this.ResetElementCurrColor();

            if (this.Parent is ShowPanelForm)
            {
                ((ShowPanelForm)this.Parent).CurrentSelectedRichTextBox = null;
            }
            else if (this.Parent.Parent is ShowPanelForm)
            {
                ((ShowPanelForm)this.Parent.Parent).CurrentSelectedRichTextBox = null;
            }
            else if (this.Parent.Parent.Parent is ShowPanelForm)
            {
                ((ShowPanelForm)this.Parent.Parent.Parent).CurrentSelectedRichTextBox = null;
            }

            base.OnLeave(e);
        }

        private void SetElementCurrColor(EntityMedicalTerm p_objElement)
        {
            try
            {
                if (this.m_lstMedicalTerm.Count > 0)
                {
                    int intCurrIndex = this.SelectionStart;
                    int intLen = this.SelectionLength;

                    if (p_objElement.IsChangeColor) return;

                    this.m_blnMouseMoving = true;
                    p_objElement.IsChangeColor = true;
                    this.SelectionStart = p_objElement.StartIndex;
                    this.SelectionLength = p_objElement.Value.Length;
                    this.SelectionColor = ElementEditColor;
                    this.SelectionLength = 0;

                    EntityMedicalTerm obj = null;
                    for (int i = 0; i < this.m_lstMedicalTerm.Count; i++)
                    {
                        obj = this.m_lstMedicalTerm[i];
                        if (p_objElement != obj && obj.IsChangeColor)
                        {
                            obj.IsChangeColor = false;
                            this.SelectionStart = obj.StartIndex;
                            this.SelectionLength = obj.Value.Length;
                            this.SelectionColor = m_clrTerm;
                            this.SelectionLength = 0;
                        }
                    }

                    this.SelectionStart = intCurrIndex;
                    this.SelectionLength = 0;
                }
            }
            finally
            {
                this.m_blnMouseMoving = false;
            }
        }

        private void ResetElementCurrColor()
        {
            if (this.m_lstMedicalTerm.Count > 0)
            {
                int intItemIndex = -1;
                this.m_lstMedicalTerm.Sort();

                try
                {
                    for (int k = 0; k < this.m_lstMedicalTerm.Count; k++)
                    {
                        if (k != intItemIndex && this.m_lstMedicalTerm[k].IsChangeColor)
                        {
                            this.m_blnMouseMoving = true;
                            this.m_lstMedicalTerm[k].IsChangeColor = false;
                            this.SelectionStart = this.m_lstMedicalTerm[k].StartIndex;
                            this.SelectionLength = this.m_lstMedicalTerm[k].Value.Length;
                            if (this.m_lstMedicalTerm[k].TID == OP_Call)
                                this.SelectionColor = Color.Black;
                            else
                                this.SelectionColor = m_clrTerm;
                            this.SelectionFont = FntElement;
                            this.SelectionLength = 0;
                        }
                    }
                }
                finally
                {
                    this.m_blnMouseMoving = false;
                }
            }
        }

        private bool m_blnMouseMoving = false;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.m_blnIniting) return;

            //try
            //{
            //    if (this.m_lstMedicalTerm.Count > 0)
            //    {
            //        bool blnDo = false;
            //        this.m_lstMedicalTerm.Sort();

            //        using (RichTextBox rtx = new RichTextBox())
            //        {
            //            int intItemIndex = -1;
            //            rtx.Rtf = this.Rtf;

            //            foreach (clsView obj in this.m_lstElementView)
            //            {
            //                if (e.X >= obj.X1 && e.X <= obj.X2 &&
            //                    e.Y >= obj.Y1 && e.Y <= obj.Y2)
            //                {
            //                    intItemIndex = obj.Index;
            //                    if (this.m_lstMedicalTerm[obj.Index].IsChangeColor)
            //                    {
            //                        break;
            //                    }
            //                    this.m_blnMouseMoving = true;
            //                    this.m_lstMedicalTerm[obj.Index].IsChangeColor = true;
            //                    rtx.SelectionStart = this.m_lstMedicalTerm[obj.Index].m_intStartIndex;
            //                    rtx.SelectionLength = this.m_lstMedicalTerm[obj.Index].m_strValue.Length;
            //                    rtx.SelectionColor = Color.OrangeRed;
            //                    rtx.SelectionLength = 0;

            //                    for (int k = 0; k < this.m_lstMedicalTerm.Count; k++)
            //                    {
            //                        if (k != intItemIndex && this.m_lstMedicalTerm[k].IsChangeColor)
            //                        {
            //                            this.m_blnMouseMoving = true;
            //                            this.m_lstMedicalTerm[k].IsChangeColor = false;
            //                            rtx.SelectionStart = this.m_lstMedicalTerm[k].m_intStartIndex;
            //                            rtx.SelectionLength = this.m_lstMedicalTerm[k].m_strValue.Length;
            //                            rtx.SelectionColor = m_clrTerm;
            //                            rtx.SelectionLength = 0;
            //                        }
            //                    }
            //                    blnDo = true;
            //                    break;
            //                }                            
            //            }
            //            if (blnDo)
            //            {
            //                this.Rtf = rtx.Rtf;
            //            }
            //        }

            //        //bool blnDo = false;
            //        //int intCurrIndex = this.SelectionStart;
            //        //int intLen = this.SelectionLength;
            //        //RichTextBox rtx = new RichTextBox();
            //        //foreach (clsView obj in this.m_lstElementView)
            //        //{
            //        //    if (e.X >= obj.X1 && e.X <= obj.X2 &&
            //        //        e.Y >= obj.Y1 && e.Y <= obj.Y2)
            //        //    {
            //        //        intItemIndex = obj.Index;
            //        //        if (this.m_lstMedicalTerm[obj.Index].IsChangeColor)
            //        //        {
            //        //            break;
            //        //        }
            //        //        this.m_blnMouseMoving = true;
            //        //        this.m_lstMedicalTerm[obj.Index].IsChangeColor = true;
            //        //        this.SelectionStart = this.m_lstMedicalTerm[obj.Index].m_intStartIndex;
            //        //        this.SelectionLength = this.m_lstMedicalTerm[obj.Index].m_strValue.Length;
            //        //        this.SelectionColor = Color.OrangeRed;
            //        //        this.SelectionLength = 0;

            //        //        for (int k = 0; k < this.m_lstMedicalTerm.Count; k++)
            //        //        {
            //        //            if (k != intItemIndex && this.m_lstMedicalTerm[k].IsChangeColor)
            //        //            {
            //        //                this.m_blnMouseMoving = true;
            //        //                this.m_lstMedicalTerm[k].IsChangeColor = false;
            //        //                this.SelectionStart = this.m_lstMedicalTerm[k].m_intStartIndex;
            //        //                this.SelectionLength = this.m_lstMedicalTerm[k].m_strValue.Length;
            //        //                this.SelectionColor = m_clrTerm;
            //        //                this.SelectionLength = 0;
            //        //            }
            //        //        }
            //        //        blnDo = true;
            //        //        break;
            //        //    }
            //        //}

            //        //if (blnDo)
            //        //{
            //        //    this.SelectionStart = intCurrIndex;
            //        //    this.SelectionLength = 0;
            //        //}
            //    }
            //}
            //finally
            //{
            //    this.m_blnMouseMoving = false;
            //}

            if (GlobalCase.caseInfo == null || GlobalCase.caseInfo.CreatorID == null)
            {
            }
            else
            {
                try
                {
                    string strUserName = string.Empty;
                    DateTime dtmOperDate = DateTime.Now;
                    bool blnStatus = false;

                    if (this.m_lstTextContentInfos.Count > 0)
                    {
                        this.m_lstTextContentInfos.Sort();
                        foreach (EntityView obj in this.m_lstTextView)
                        {
                            if (e.X >= obj.X1 && e.X <= obj.X2 &&
                                e.Y >= obj.Y1 && e.Y <= obj.Y2)
                            {
                                //if (this.m_lstTextContentInfos[obj.Index].m_strUserID == clsGlobalCase.objCaseInfo.intCreatorID.ToString() && this.m_lstTextContentInfos[obj.Index].m_clrText.ToArgb() == Color.Black.ToArgb())
                                if (this.m_lstTextContentInfos[obj.Index].ColorText.ToArgb() == Color.Black.ToArgb())
                                {
                                    break;
                                }

                                strUserName = this.m_lstTextContentInfos[obj.Index].UserName;
                                dtmOperDate = this.m_lstTextContentInfos[obj.Index].ModifyDate;
                                blnStatus = true;
                                break;
                            }
                        }
                    }

                    if (this.m_lstDoubleStrikeThrough.Count > 0)
                    {
                        this.m_lstDoubleStrikeThrough.Sort();
                        foreach (EntityView obj in this.m_lstDDLView)
                        {
                            if (e.X >= obj.X1 && e.X <= obj.X2 &&
                                e.Y >= obj.Y1 && e.Y <= obj.Y2)
                            {
                                strUserName = this.m_lstDoubleStrikeThrough[obj.Index].UserName;
                                dtmOperDate = this.m_lstDoubleStrikeThrough[obj.Index].DeleteTime;
                                blnStatus = true;
                                break;
                            }
                        }
                    }

                    if (blnStatus)
                    {
                        this.m_objTipArgs.ToolTip = strUserName + "\r\n" + dtmOperDate.ToString("yyyy-MM-dd HH:mm:ss");
                        this.m_objTipCtrl.ShowHint(this.m_objTipArgs);
                    }
                    else
                    {
                        this.m_objTipCtrl.HideHint();
                    }
                }
                catch { }
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// OnMouseDoubleClick
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (GlobalPatient.currPatient == null)
            {
                //DialogBox.Msg("请先选择病人。", MessageBoxIcon.Information, uiHelper.frmCurr);
                return;
            }
            if (_blnReadOnly) return;
            if (this.m_blnCursorPositionToImage4(this.m_intCurrentCursorIndex))
            {
                this.m_mthEditImage();
            }
            else
            {
                this.m_mthUpdateElement();
            }
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            try
            {
                if (this.ParentEmrControl != null)
                {
                    if (e.Delta > 0)
                    {
                        this.ParentEmrControl.xtraScrollableControl.VerticalScroll.Value -= 10;
                    }
                    else if (e.Delta < 0)
                    {
                        this.ParentEmrControl.xtraScrollableControl.VerticalScroll.Value += 10;
                    }
                }
            }
            catch { }
            finally
            { }
            base.OnMouseWheel(e);
        }

        /// <summary>
        /// 拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlRichTextBox_DragDrop(Object sender, System.Windows.Forms.DragEventArgs e)
        {
            this.m_intCurrentCursorIndex = this.SelectionStart;
            if (!this.m_blnCheckCursorValidPostion()) return;

            if (e.Data.GetDataPresent(typeof(System.String)))
            {
                this.m_mthInsertText(Convert.ToString(e.Data.GetData(typeof(System.String))));
            }
        }

        #endregion

        #region 结束修改
        /// <summary>
        /// 标记结束修改，记录修改时间
        /// </summary>
        public void m_mthFinishEdit()
        {
            //简校验
            this.m_mthCheckElement();

            //结束时间
            DateTime dtmFinishEdit = Common.Utils.Utils.ServerTime();

            //用户信息
            int intUserSequence = m_objCurrentModifyUser.UserSequence;

            if (intUserSequence == -1)
            {
                intUserSequence = this.m_lstModifyUsers.Count + 1;
            }

            //更新双画线区域的时间及用户信息
            foreach (EntityDstInfo objDSTInfo in this.m_lstDoubleStrikeThrough)
            {
                if (objDSTInfo.DeleteTime.Year == 1900)
                {
                    objDSTInfo.DeleteTime = dtmFinishEdit;
                    objDSTInfo.UserSequence = intUserSequence;
                }
            }

            //更新当前用户信息
            if (m_objCurrentModifyUser.UserSequence == -1)
            {
                m_objCurrentModifyUser.UserSequence = intUserSequence;
                m_objCurrentModifyUser.ModifyDate = dtmFinishEdit;
                m_objCurrentModifyUser.ColorText = m_clrOldPartInsertText;

                this.m_lstModifyUsers.Add(m_objCurrentModifyUser);
            }

            if (this.Text.Length > 0 && this.m_lstTextContentInfos.Count == 0)
            {
                EntityUserContentInfo objContentInfo = new EntityUserContentInfo();

                objContentInfo.UserInfo = m_objCurrentModifyUser;
                objContentInfo.StartIndex = 0;
                objContentInfo.EndIndex = this.Text.Length - 1;
                objContentInfo.UserID = m_objCurrentModifyUser.UserID;
                objContentInfo.UserName = m_objCurrentModifyUser.UserName;
                objContentInfo.ModifyDate = dtmFinishEdit;
                objContentInfo.ColorText = this.m_clrDefaultViewText;

                this.m_lstTextContentInfos.Add(objContentInfo);
            }
            this.m_mthResetPoint();

            if (!this.ValueChangedFlag) return;
            this.ValueChangedFlag = false;
        }

        private void m_mthResetPoint()
        {
            bool blnCheck = false;
            foreach (EntityMedicalTerm objElement in this.m_lstMedicalTerm)
            {
                if (string.IsNullOrEmpty(objElement.UserID))
                {
                    objElement.UserID = this.m_objCurrentModifyUser.UserID;
                    objElement.UserName = this.m_objCurrentModifyUser.UserName;
                    blnCheck = true;
                }
            }
            if (blnCheck)
            {
                this.Invalidate();
            }
        }
        #endregion

        #region 清空文本内容
        /// <summary>
        /// 清空文本内容
        /// </summary>
        public void ClearText()
        {
            this.m_blnCanTextChanged = false;
            //if (this.Text != string.Empty) this.ValueChangedFlag = true;
            this.Text = string.Empty;

            this.m_lstDoubleStrikeThrough.Clear();
            this.m_lstModifyUsers.Clear();
            this.m_lstTextContentInfos.Clear();
            this.m_lstMedicalTerm.Clear();
            this.m_lstImageInfo.Clear();

            this.m_intPreviouslyLen = 0;
            this.m_intTextLenght = m_intPreviouslyLen;
            this.m_blnIsSelectedChanged = false;
            this.m_blnCanSelectedChanged = false;
            this.SelectionStart = 0;
            this.SelectionLength = 0;

            this.m_clrOldPartInsertText = this.m_clrDefaultViewText;
            this.SelectionColor = this.m_clrDefaultViewText;
            this.m_blnCanSelectedChanged = true;
            this.m_intCurrentCursorIndex = 0;
            this.m_blnCanTextChanged = true;

            try
            {
                this.SetFirstlineCaption();
            }
            catch { };
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        private void m_mthDel()
        {
            //this.m_mthStopRedraw();
            RichTextBoxPlus.InsertText(this, "");
            //this.SelectionLength = 0;
            //this.m_mthRedraw();
            //this.Focus();
        }
        #endregion

        #region 获取正确的文本

        /// <summary>
        /// 获取正确的文本
        /// </summary>
        /// <returns></returns>
        public string GetRightText()
        {
            //this.m_mthFinishEdit();
            m_sbdTemp.Length = 0;

            int intStartIndex = 0;
            this.m_lstDoubleStrikeThrough.Sort();
            foreach (EntityDstInfo objDST in this.m_lstDoubleStrikeThrough)
            {
                if (objDST.StartIndex > intStartIndex && intStartIndex < this.Text.Length)
                    m_sbdTemp.Append(this.Text.Substring(intStartIndex, objDST.StartIndex - intStartIndex));

                intStartIndex = objDST.EndIndex + 1;
            }

            if (intStartIndex < this.Text.Length)
            {
                m_sbdTemp.Append(this.Text.Substring(intStartIndex, this.Text.Length - intStartIndex));
            }
            return m_sbdTemp.ToString();

            //string strText = m_sbdTemp.ToString();
            //foreach (clsImageInfo objImage in this.m_lstImageInfo)
            //{
            //    strText = strText.Replace(objImage.m_strImageNO, "");
            //}

            //return strText;
        }
        /// <summary>
        /// 重载,当判断com.digitalwave.Controls.ctlRichTextBox内容是否改变时用
        /// </summary>
        /// <param name="p_blnCheckStatus"></param>
        /// <returns></returns>
        public string GetRightText(bool p_blnCheckStatus)
        {
            if (!p_blnCheckStatus)
                m_mthFinishEdit();

            return GetRightText();
        }
        #endregion

        #region 处理上下标,仅在显示上作控制
        /// <summary>
        /// 上标or下标字体大小
        /// </summary>
        public const int c_intSuperSubScriptSize = 8;
        /// <summary>
        /// 上标
        /// </summary>
        public const int c_intSuperScriptCharOffSet = 7;
        /// <summary>
        /// 下标
        /// </summary>
        public const int c_intSubScriptCharOffSet = -3;

        private const int c_intNormalScriptCharOffset = 0;

        /// <summary>
        /// 设置所选文字为上标或下标

        /// </summary>
        /// <param name="p_intType">0：上标；1：下标</param>
        public void m_mthSetSelectionScript(int p_intType)
        {
            bool blnCanTextChanged = m_blnCanSelectedChanged;
            m_blnCanTextChanged = false;

            if (!this.m_blnCheckCursorValidPostion())
                return;

            this.ValueChangedFlag = true;
            if (p_intType == 0)
            {
                this.SelectionFont = new Font(this.Font.FontFamily, c_intSuperSubScriptSize);
                this.SelectionCharOffset = c_intSuperScriptCharOffSet;
            }
            else if (p_intType == 1)
            {
                this.SelectionFont = new Font(this.Font.FontFamily, c_intSuperSubScriptSize);
                this.SelectionCharOffset = c_intSubScriptCharOffSet;
            }

            m_blnCanTextChanged = blnCanTextChanged;

            m_mthFireEvtTextChange(false);

        }

        /// <summary>
        /// 撤消上下标

        /// </summary>
        public void m_mthUndoSuperSubScript()
        {
            bool blnCanTextChanged = m_blnCanSelectedChanged;
            m_blnCanTextChanged = false;

            this.ValueChangedFlag = true;
            this.SelectionFont = new Font(this.Font.FontFamily, this.Font.Size);
            this.SelectionCharOffset = c_intNormalScriptCharOffset;

            m_blnCanTextChanged = blnCanTextChanged;

            m_mthFireEvtTextChange(false);
        }
        #endregion

        #region 获取指定位置字符右下角的坐标
        /// <summary>
        ///  获取指定位置字符右下角的坐标.for Dst
        /// </summary>
        /// <param name="intCharIndex"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        private Point m_ptGetCharPositionRightDown(int intCharIndex, Graphics g)
        {
            if (intCharIndex < 0) intCharIndex = 0;
            if (intCharIndex >= this.TextLength) intCharIndex = this.TextLength - 1;

            Point ptLeftUp = this.GetPositionFromCharIndex(intCharIndex);

            Point ptRightUp = this.GetPositionFromCharIndex(intCharIndex + 1);

            Point ptRightDown;
            if (ptLeftUp.Y == ptRightUp.Y)
            {
                ptRightDown = new Point(ptRightUp.X, ptRightUp.Y + m_intFontHeight);
            }
            else
            {
                int intCharWidth = (int)g.MeasureString(this.Text[intCharIndex].ToString(), this.Font).Width - 5;
                ptRightDown = new Point(ptLeftUp.X + intCharWidth, ptLeftUp.Y + m_intFontHeight);
            }

            return ptRightDown;
        }
        #endregion

        #region 获取XML信息
        #region 获取双划线信息

        /// <summary>
        /// 获取双划线信息

        /// </summary>
        /// <returns></returns>
        public string m_strGetXmlOfDST()
        {
            this.m_mthFinishEdit();

            if (this.m_lstDoubleStrikeThrough.Count == 0)
                return string.Empty;

            MemoryStream objXmlStream = new MemoryStream();
            XmlTextWriter objXmlWriter = new XmlTextWriter(objXmlStream, System.Text.Encoding.Unicode);

            this.m_lstDoubleStrikeThrough.Sort();
            objXmlWriter.WriteStartDocument();
            objXmlWriter.WriteStartElement("Del");
            foreach (EntityDstInfo objDST in this.m_lstDoubleStrikeThrough)
            {
                objXmlWriter.WriteStartElement("L");
                objXmlWriter.WriteAttributeString("S", objDST.StartIndex.ToString());
                objXmlWriter.WriteAttributeString("E", objDST.EndIndex.ToString());
                objXmlWriter.WriteAttributeString("V", objDST.Value);
                //objXmlWriter.WriteAttributeString("C", objDST.m_clrDST.ToArgb().ToString());
                objXmlWriter.WriteAttributeString("I", objDST.UserID);
                objXmlWriter.WriteAttributeString("N", objDST.UserName);
                //objXmlWriter.WriteAttributeString("I", objDST.m_intUserSequence.ToString());
                objXmlWriter.WriteAttributeString("D", objDST.DeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                objXmlWriter.WriteEndElement();
            }
            objXmlWriter.WriteEndElement();
            objXmlWriter.WriteEndDocument();
            objXmlWriter.Flush();

            string strXml = System.Text.Encoding.Unicode.GetString(objXmlStream.ToArray(), 0, (int)objXmlStream.Length);

            int intRootBegin = strXml.IndexOf("<Del");

            return strXml.Substring(intRootBegin, strXml.Length - intRootBegin);
        }
        #endregion

        #region 获取术语信息
        /// <summary>
        /// 获取术语信息
        /// </summary>
        /// <returns></returns>
        public string m_strGetXmlOfTerm()
        {
            this.m_mthFinishEdit();

            if (this.m_lstMedicalTerm.Count == 0)
                return string.Empty;

            this.m_lstMedicalTerm.Sort();
            MemoryStream objXmlStream = new MemoryStream();
            XmlTextWriter objXmlWriter = new XmlTextWriter(objXmlStream, System.Text.Encoding.Unicode);

            objXmlWriter.WriteStartDocument();
            objXmlWriter.WriteStartElement("MedicalTerm");
            foreach (EntityMedicalTerm objMedicalTerm in this.m_lstMedicalTerm)
            {
                objXmlWriter.WriteStartElement("C");
                objXmlWriter.WriteAttributeString("S", objMedicalTerm.StartIndex.ToString());
                objXmlWriter.WriteAttributeString("E", objMedicalTerm.EndIndex.ToString());
                objXmlWriter.WriteAttributeString("I", objMedicalTerm.UserID);
                objXmlWriter.WriteAttributeString("N", objMedicalTerm.UserName);
                objXmlWriter.WriteAttributeString("D", objMedicalTerm.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                objXmlWriter.WriteAttributeString("T", objMedicalTerm.TID);
                objXmlWriter.WriteAttributeString("V", objMedicalTerm.Value);
                objXmlWriter.WriteEndElement();
            }
            objXmlWriter.WriteEndElement();
            objXmlWriter.WriteEndDocument();
            objXmlWriter.Flush();

            string strXml = System.Text.Encoding.Unicode.GetString(objXmlStream.ToArray(), 0, (int)objXmlStream.Length);

            int intRootBegin = strXml.IndexOf("<MedicalTerm");

            return strXml.Substring(intRootBegin, strXml.Length - intRootBegin);
        }
        #endregion

        #region 从XMLInfo对象获取xml文本
        /// <summary>
        /// 从XMLInfo对象获取xml文本
        /// </summary>
        private string m_strGetXmlFromInfo(List<EntityDstInfo> p_lstDSTIndex, List<EntityModifyUserInfo> p_lstUserInfo, List<EntityUserContentInfo> p_lstTextContentInfos, List<EntityMedicalTerm> p_lstMedicalTerm, DateTime? p_dtmCaseWriteDate)
        {
            if (string.IsNullOrEmpty(this.Text.Trim())) return string.Empty;
            MemoryStream objXmlStream = new MemoryStream();
            XmlTextWriter objXmlWriter = new XmlTextWriter(objXmlStream, System.Text.Encoding.Unicode);

            objXmlWriter.WriteStartDocument();
            objXmlWriter.WriteStartElement("Main");
            DateTime dtmCurrent = Common.Utils.Utils.ServerTime();

            #region 内容信息
            if (p_lstUserInfo.Count > 0 || p_lstTextContentInfos.Count > 0)
            {
                objXmlWriter.WriteStartElement("Content");
                /*
                int intIndex = 1;
                foreach (clsModifyUserInfo objUserInfo in p_lstUserInfo)
                {
                    objUserInfo.m_intUserSequence = intIndex++;

                    objXmlWriter.WriteStartElement("UI");
                    objXmlWriter.WriteAttributeString("D", objUserInfo.m_strUserID);
                    objXmlWriter.WriteAttributeString("N", objUserInfo.m_strUserName);
                    objXmlWriter.WriteAttributeString("S", objUserInfo.m_intUserSequence.ToString());
                    objXmlWriter.WriteAttributeString("M", objUserInfo.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objXmlWriter.WriteAttributeString("C", objUserInfo.m_clrText.ToArgb().ToString());
                    objXmlWriter.WriteEndElement();
                }
                 * */

                int intIndex = 1;
                List<EntityUserContentInfo> lstContent = new List<EntityUserContentInfo>();
                EntityUserContentInfo objContentInfo = null;
                for (int i = 0; i < p_lstTextContentInfos.Count; i++)
                {
                    objContentInfo = p_lstTextContentInfos[i];

                    if (string.IsNullOrEmpty(objContentInfo.UserID)) objContentInfo.UserID = this.m_objCurrentModifyUser.UserID;
                    if (string.IsNullOrEmpty(objContentInfo.UserName)) objContentInfo.UserName = this.m_objCurrentModifyUser.UserName;
                    if (objContentInfo.EndIndex > this.Text.Length - 1) continue;
                    if (this.ExtSpecModifyFlag && objContentInfo.UserID == GlobalLogin.objLogin.EmpNo) continue;
                    if (lstContent.Any(t => t.StartIndex == objContentInfo.StartIndex && t.EndIndex == objContentInfo.EndIndex && t.UserID == objContentInfo.UserID))
                        continue;
                    else
                        lstContent.Add(objContentInfo);

                    objXmlWriter.WriteStartElement("C");
                    objXmlWriter.WriteAttributeString("S", objContentInfo.StartIndex.ToString());
                    objXmlWriter.WriteAttributeString("E", objContentInfo.EndIndex.ToString());
                    objXmlWriter.WriteAttributeString("I", objContentInfo.UserID);
                    objXmlWriter.WriteAttributeString("N", objContentInfo.UserName);
                    objXmlWriter.WriteAttributeString("R", objContentInfo.ColorText.ToArgb().ToString());
                    if (p_dtmCaseWriteDate == null)
                    {
                        if (objContentInfo.ModifyDate.ToString("yyyy") == "0001")
                        {
                            objXmlWriter.WriteAttributeString("D", dtmCurrent.ToString("yyyy-MM-dd HH:mm:ss"));
                            objContentInfo.ModifyDate = dtmCurrent;
                        }
                        else
                        {
                            objXmlWriter.WriteAttributeString("D", objContentInfo.ModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                    else
                    {
                        objXmlWriter.WriteAttributeString("D", p_dtmCaseWriteDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        objContentInfo.ModifyDate = p_dtmCaseWriteDate.Value;
                    }
                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }
            #endregion

            #region 术语
            if (p_lstMedicalTerm.Count > 0)
            {
                p_lstMedicalTerm.Sort();
                objXmlWriter.WriteStartElement("MedicalTerm");

                EntityMedicalTerm objMedicalTerm = null;
                for (int i = 0; i < p_lstMedicalTerm.Count; i++)
                {
                    objMedicalTerm = p_lstMedicalTerm[i];

                    if ((objMedicalTerm.TID == "PatInfo" && this.FormTypeName != c_strFormClass) ||
                        (objMedicalTerm.TID.StartsWith("Intellection") && this.FormTypeName != c_strFormClass))
                    {
                        continue;
                    }
                    if (this.FormTypeName == c_strFormClass)
                    {
                        objMedicalTerm.UserID = string.Empty;
                        objMedicalTerm.UserName = string.Empty;
                    }
                    objXmlWriter.WriteStartElement("C");
                    objXmlWriter.WriteAttributeString("S", objMedicalTerm.StartIndex.ToString());
                    objXmlWriter.WriteAttributeString("E", objMedicalTerm.EndIndex.ToString());
                    //objXmlWriter.WriteAttributeString("I", objMedicalTerm.m_strUserID);
                    //objXmlWriter.WriteAttributeString("N", objMedicalTerm.m_strUserName);
                    //objXmlWriter.WriteAttributeString("D", (p_dtmCaseWriteDate == null ? objMedicalTerm.m_dtmCreateTime.ToString("yyyy-MM-dd HH:mm:ss") : p_dtmCaseWriteDate.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                    if (string.IsNullOrEmpty(objMedicalTerm.CaseCode))
                        objXmlWriter.WriteAttributeString("A", GlobalCase.caseInfo.CaseCode);
                    else
                        objXmlWriter.WriteAttributeString("A", objMedicalTerm.CaseCode);
                    objXmlWriter.WriteAttributeString("T", objMedicalTerm.TID);
                    objXmlWriter.WriteAttributeString("V", objMedicalTerm.Value);
                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }
            #endregion

            #region 双划线
            if (p_lstDSTIndex.Count > 0)
            {
                p_lstDSTIndex.Sort();
                objXmlWriter.WriteStartElement("DDL");
                EntityDstInfo objDST = null;

                for (int i = 0; i < p_lstDSTIndex.Count; i++)
                {
                    objDST = p_lstDSTIndex[i];

                    objXmlWriter.WriteStartElement("C ");
                    objXmlWriter.WriteAttributeString("S", objDST.StartIndex.ToString());
                    objXmlWriter.WriteAttributeString("E", objDST.EndIndex.ToString());
                    objXmlWriter.WriteAttributeString("V", objDST.Value.Substring(1));
                    //objXmlWriter.WriteAttributeString("C", objDST.m_clrDST.ToArgb().ToString());
                    objXmlWriter.WriteAttributeString("I", objDST.UserID);
                    objXmlWriter.WriteAttributeString("N", objDST.UserName);
                    //objXmlWriter.WriteAttributeString("I", objDST.m_intUserSequence.ToString());
                    //objXmlWriter.WriteAttributeString("D", (p_dtmCaseWriteDate == null ? objDST.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss") : p_dtmCaseWriteDate.Value.ToString("yyyy-MM-dd HH:mm:ss")));

                    if (p_dtmCaseWriteDate == null)
                    {
                        if (objDST.DeleteTime.ToString("yyyy") == "0001")
                        {
                            objXmlWriter.WriteAttributeString("D", dtmCurrent.ToString("yyyy-MM-dd HH:mm:ss"));
                            objDST.DeleteTime = dtmCurrent;
                        }
                        else
                        {
                            objXmlWriter.WriteAttributeString("D", objDST.DeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                    else
                    {
                        objXmlWriter.WriteAttributeString("D", p_dtmCaseWriteDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        objDST.DeleteTime = p_dtmCaseWriteDate.Value;
                    }

                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }
            #endregion

            objXmlWriter.WriteEndElement();
            objXmlWriter.WriteEndDocument();
            objXmlWriter.Flush();

            string strXml = System.Text.Encoding.Unicode.GetString(objXmlStream.ToArray(), 0, (int)objXmlStream.Length);

            int intRootBegin = strXml.IndexOf("<Main");

            return strXml.Substring(intRootBegin, strXml.Length - intRootBegin);
        }

        #endregion

        #region 从XMLInfo对象获取TraceXml文本

        private string m_strGetTraceXmlFromInfo(List<EntityDstInfo> p_lstDSTIndex, List<EntityModifyUserInfo> p_lstUserInfo, List<EntityUserContentInfo> p_lstTextContentInfos, List<EntitySuperSubScript> p_lstSuperSubScriptInfo,
                                                 List<EntityFontColor> p_lstFontColor, List<EntityFontBold> p_lstFontBold, List<EntityFontItalic> p_lstFontItalic, List<EntityFontUnderLine> p_lstFontUnderLine, List<EntityMedicalTerm> p_lstMedicalTerm)
        {
            MemoryStream objXmlStream = new MemoryStream();
            XmlTextWriter objXmlWriter = new XmlTextWriter(objXmlStream, System.Text.Encoding.Unicode);

            objXmlWriter.WriteStartDocument();
            objXmlWriter.WriteStartElement("Main");

            #region 内容信息
            objXmlWriter.WriteStartElement("OrgContent");
            objXmlWriter.WriteStartElement("Description");
            objXmlWriter.WriteAttributeString("Value", this.Text);
            objXmlWriter.WriteEndElement();
            objXmlWriter.WriteEndElement();

            if (p_lstUserInfo.Count > 0 || p_lstTextContentInfos.Count > 0)
            {
                objXmlWriter.WriteStartElement("Content");
                /*
                int intIndex = 1;
                foreach (clsModifyUserInfo objUserInfo in p_lstUserInfo)
                {
                    objUserInfo.m_intUserSequence = intIndex++;

                    objXmlWriter.WriteStartElement("UI");
                    objXmlWriter.WriteAttributeString("D", objUserInfo.m_strUserID);
                    objXmlWriter.WriteAttributeString("N", objUserInfo.m_strUserName);
                    objXmlWriter.WriteAttributeString("S", objUserInfo.m_intUserSequence.ToString());
                    objXmlWriter.WriteAttributeString("M", objUserInfo.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objXmlWriter.WriteAttributeString("C", objUserInfo.m_clrText.ToArgb().ToString());
                    objXmlWriter.WriteEndElement();
                }
                 * */

                foreach (EntityUserContentInfo objContentInfo in p_lstTextContentInfos)
                {
                    objXmlWriter.WriteStartElement("C");
                    objXmlWriter.WriteAttributeString("S", objContentInfo.StartIndex.ToString());
                    objXmlWriter.WriteAttributeString("E", objContentInfo.EndIndex.ToString());
                    //objXmlWriter.WriteAttributeString("Q", objContentInfo.objUserInfo.m_intUserSequence.ToString());
                    objXmlWriter.WriteAttributeString("I", objContentInfo.UserID);
                    objXmlWriter.WriteAttributeString("N", objContentInfo.UserName);
                    objXmlWriter.WriteAttributeString("D", objContentInfo.ModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }
            #endregion

            #region 术语
            if (p_lstMedicalTerm.Count > 0)
            {
                p_lstMedicalTerm.Sort();
                objXmlWriter.WriteStartElement("MedicalTerm");
                foreach (EntityMedicalTerm objMedicalTerm in p_lstMedicalTerm)
                {
                    objXmlWriter.WriteStartElement("C");
                    objXmlWriter.WriteAttributeString("S", objMedicalTerm.StartIndex.ToString());
                    objXmlWriter.WriteAttributeString("E", objMedicalTerm.EndIndex.ToString());
                    objXmlWriter.WriteAttributeString("I", objMedicalTerm.UserID);
                    objXmlWriter.WriteAttributeString("N", objMedicalTerm.UserName);
                    objXmlWriter.WriteAttributeString("D", objMedicalTerm.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    objXmlWriter.WriteAttributeString("T", objMedicalTerm.TID);
                    objXmlWriter.WriteAttributeString("V", objMedicalTerm.Value);
                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }
            #endregion

            #region 上下标
            if (p_lstSuperSubScriptInfo.Count > 0)
            {
                objXmlWriter.WriteStartElement("SuperSubScript");
                foreach (EntitySuperSubScript objScript in p_lstSuperSubScriptInfo)
                {
                    objXmlWriter.WriteStartElement("C");
                    objXmlWriter.WriteAttributeString("S", objScript.Index.ToString());
                    objXmlWriter.WriteAttributeString("O", objScript.CharOffset.ToString());
                    objXmlWriter.WriteAttributeString("V", objScript.Value);
                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }
            #endregion

            #region 字体其他属性
            if (p_lstFontColor.Count > 0)
            {
                objXmlWriter.WriteStartElement("FontColor");
                foreach (EntityFontColor objFontColor in p_lstFontColor)
                {
                    objXmlWriter.WriteStartElement("C");
                    objXmlWriter.WriteAttributeString("S", objFontColor.Index.ToString());
                    objXmlWriter.WriteAttributeString("R", objFontColor.ColorValue.ToArgb().ToString());
                    objXmlWriter.WriteAttributeString("V", objFontColor.TxtValue);
                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }

            if (p_lstFontBold.Count > 0)
            {
                objXmlWriter.WriteStartElement("FontBold");
                foreach (EntityFontBold objFontBold in p_lstFontBold)
                {
                    objXmlWriter.WriteStartElement("C");
                    objXmlWriter.WriteAttributeString("S", objFontBold.Index.ToString());
                    objXmlWriter.WriteAttributeString("V", objFontBold.Value);
                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }

            if (p_lstFontItalic.Count > 0)
            {
                objXmlWriter.WriteStartElement("FontItalic");
                foreach (EntityFontItalic objFontItalic in p_lstFontItalic)
                {
                    objXmlWriter.WriteStartElement("C");
                    objXmlWriter.WriteAttributeString("S", objFontItalic.Index.ToString());
                    objXmlWriter.WriteAttributeString("V", objFontItalic.Value);
                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }

            if (p_lstFontUnderLine.Count > 0)
            {
                objXmlWriter.WriteStartElement("FontUnderLine");
                foreach (EntityFontUnderLine objFontUnderLine in p_lstFontUnderLine)
                {
                    objXmlWriter.WriteStartElement("C");
                    objXmlWriter.WriteAttributeString("S", objFontUnderLine.Index.ToString());
                    objXmlWriter.WriteAttributeString("V", objFontUnderLine.Value);
                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }
            #endregion

            #region 双划线
            if (p_lstDSTIndex.Count > 0)
            {
                p_lstDSTIndex.Sort();
                objXmlWriter.WriteStartElement("DST");
                foreach (EntityDstInfo objDST in p_lstDSTIndex)
                {
                    objXmlWriter.WriteStartElement("C ");
                    objXmlWriter.WriteAttributeString("S", objDST.StartIndex.ToString());
                    objXmlWriter.WriteAttributeString("E", objDST.EndIndex.ToString());
                    objXmlWriter.WriteAttributeString("V", objDST.Value);
                    //objXmlWriter.WriteAttributeString("C", objDST.m_clrDST.ToArgb().ToString());
                    objXmlWriter.WriteAttributeString("I", objDST.UserID);
                    objXmlWriter.WriteAttributeString("N", objDST.UserName);
                    //objXmlWriter.WriteAttributeString("I", objDST.m_intUserSequence.ToString());
                    objXmlWriter.WriteAttributeString("D", objDST.DeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    objXmlWriter.WriteEndElement();
                }
                objXmlWriter.WriteEndElement();
            }
            #endregion

            objXmlWriter.WriteEndElement();
            objXmlWriter.WriteEndDocument();

            objXmlWriter.Flush();

            string strXml = System.Text.Encoding.Unicode.GetString(objXmlStream.ToArray(), 0, (int)objXmlStream.Length);

            int intRootBegin = strXml.IndexOf("<Main");

            return strXml.Substring(intRootBegin, strXml.Length - intRootBegin);
        }

        #endregion
        #endregion

        #region 设置双划线
        /// <summary>
        /// 设置双划线
        /// </summary>
        public void m_mthSetDoubleStrikeThrough()
        {
            if (this.SelectionLength == 0) return;
            if (this.m_blnCursorPositionToDST() || this.m_blnCursorPositionToTerm3(this.SelectionStart, this.SelectionStart + this.SelectionLength) || this.m_blnCursorPositionToCaption())
            {
                return;
            }

            this.stopReDraw();
            int intStartIndex = this.SelectionStart;
            int intEndIndex = intStartIndex + this.SelectionLength;
            this.m_intPreviouslyLen = this.TextLength;
            this.SelectionLength = 0;
            this.SelectionStart = intStartIndex;
            this.m_mthInsertText(this.m_strDSTPrefix);
            intEndIndex += 1;

            EntityDstInfo objDST = new EntityDstInfo();
            objDST.StartIndex = intStartIndex;
            objDST.EndIndex = intEndIndex - 1;
            objDST.ColorDst = Color.Red;
            objDST.UserID = m_objCurrentModifyUser.UserID;
            objDST.UserName = m_objCurrentModifyUser.UserName;
            objDST.Value = this.Text.Substring(intStartIndex, intEndIndex - intStartIndex);
            objDST.DeleteTime = DateTime.Now;
            this.m_lstDoubleStrikeThrough.Add(objDST);

            this.SelectionStart = intStartIndex;
            this.SelectionLength = 0;
            this.reDraw();
            this.m_mthFireEvtTextChange(false);
        }
        #endregion

        #region 撤销指定双划线
        /// <summary>
        /// 撤销指定双划线
        /// </summary>
        public void m_mthCancelDST()
        {
            if (this.SelectionLength > 0) this.SelectionLength = 0;
            int intCurrIndex = this.SelectionStart;
            int intEmpID = 0;
            foreach (EntityDstInfo obj in this.m_lstDoubleStrikeThrough)
            {
                if (obj.StartIndex <= intCurrIndex && intCurrIndex <= obj.EndIndex)
                {
                    if (obj.UserID != m_objCurrentModifyUser.UserID) return;
                    if (GlobalCase.caseInfo.Signature.Any(t => t.SignatureDate >= obj.DeleteTime && t.EmpNo == obj.UserID))
                    {
                        return;
                    }

                    EntityDstInfo objTmp = new EntityDstInfo();
                    objTmp.StartIndex = obj.StartIndex;
                    objTmp.EndIndex = obj.EndIndex;
                    this.m_lstDoubleStrikeThrough.Remove(obj);

                    this.stopReDraw();
                    //this.SelectionStart = objTmp.m_intStartIndex;
                    //this.SelectionLength = 1;
                    //clsRichTextBoxPlus.InsertText(this, " ");
                    //SendKeys.SendWait("{BS}");
                    this.Select(objTmp.StartIndex, 1);
                    RichTextBoxPlus.InsertText(this, string.Empty);
                    this.reDraw();
                    break;
                }
            }
        }
        #endregion

        #region 检查当前光标位置是否在双划线内
        /// <summary>
        /// 检查当前光标位置是否在双划线内
        /// </summary>
        /// <returns></returns>
        public bool m_blnCursorPositionToDST(int p_intIndex, Keys p_objKey)
        {
            bool blnRet = false;
            if (p_objKey == Keys.Delete)
            {
                foreach (EntityDstInfo obj in this.m_lstDoubleStrikeThrough)
                {
                    if (obj.StartIndex <= p_intIndex && p_intIndex <= obj.EndIndex)
                    {
                        blnRet = true;
                        break;
                    }
                }
            }
            else if (p_objKey == Keys.Back)
            {
                foreach (EntityDstInfo obj in this.m_lstDoubleStrikeThrough)
                {
                    if (obj.StartIndex < p_intIndex && p_intIndex <= obj.EndIndex + 1)
                    {
                        blnRet = true;
                        break;
                    }
                }
            }
            else
            {
                int intStartIndex = p_intIndex;
                int intEndIndex = intStartIndex;
                foreach (EntityDstInfo obj in this.m_lstDoubleStrikeThrough)
                {
                    if ((obj.StartIndex < intStartIndex && intStartIndex <= obj.EndIndex) ||
                        (obj.StartIndex > intStartIndex && obj.StartIndex < intEndIndex) ||
                        (obj.EndIndex > intStartIndex && obj.EndIndex <= intEndIndex))
                    {
                        blnRet = true;
                        break;
                    }
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            //this.ReadOnly = blnRet;
            return blnRet;
        }
        /// <summary>
        /// 检查当前光标位置是否在双划线内
        /// </summary>
        /// <returns></returns>
        public bool m_blnCursorPositionToDST()
        {
            if (this.SelectionLength == 0)
            {
                return this.m_blnCursorPositionToDST2(this.m_intCurrentCursorIndex);
            }

            bool blnRet = false;
            int intStartIndex = this.m_intCurrentCursorIndex;
            int intEndIndex = intStartIndex + this.SelectionLength;
            foreach (EntityDstInfo obj in this.m_lstDoubleStrikeThrough)
            {
                if ((obj.StartIndex < intStartIndex && intStartIndex <= obj.EndIndex) ||
                    (obj.StartIndex > intStartIndex && obj.StartIndex < intEndIndex) ||
                    (obj.EndIndex > intStartIndex && obj.EndIndex <= intEndIndex) ||
                    (obj.StartIndex <= intStartIndex && intEndIndex <= obj.EndIndex))
                {
                    blnRet = true;
                    break;
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            //this.ReadOnly = blnRet;
            return blnRet;
        }
        /// <summary>
        /// 检查当前光标位置是否在双划线内
        /// </summary>
        /// <returns></returns>
        public bool m_blnCursorPositionToDST2(int p_intIndex)
        {
            bool blnRet = false;
            foreach (EntityDstInfo obj in this.m_lstDoubleStrikeThrough)
            {
                if (obj.StartIndex < p_intIndex && p_intIndex <= obj.EndIndex)
                {
                    blnRet = true;
                    break;
                }
            }
            return blnRet;
        }
        #endregion

        #region 调整双划线位置

        /// <summary>
        /// 调整双划线位置
        /// </summary>
        private void m_mthAdjustDSTPosition()
        {
            if (this.m_lstDoubleStrikeThrough.Count == 0) return;

            int intStart = 0;
            int intPos = 0;
            string strText = this.Text;
            this.m_lstDoubleStrikeThrough.Sort();
            foreach (EntityDstInfo objDST in this.m_lstDoubleStrikeThrough)
            {
                intPos = strText.IndexOf(objDST.Value, intStart);
                if (intPos != objDST.StartIndex)
                {
                    objDST.StartIndex = intPos;
                    objDST.EndIndex = intPos + objDST.Value.Length - 1;
                }
                intStart = objDST.EndIndex + 1;
            }
            this.SelectionLength = 0;
            this.Invalidate();
        }
        #endregion

        #region 调整元素.词汇位置
        /// <summary>
        /// 调整元素.词汇位置---Insert
        /// </summary>
        /// <param name="p_intNewLen"></param>
        private void AdjustElementPosition_Insert(int p_intIndex, int p_intNewLen)
        {
            EntityMedicalTerm objElement = CursorPositionToTerm_Insert(p_intIndex);
            if (objElement != null && objElement.EndIndex + p_intNewLen + 1 <= this.Text.Length)
            {
                objElement.EndIndex += p_intNewLen;
                objElement.Value = this.Text.Substring(objElement.StartIndex, objElement.EndIndex - objElement.StartIndex + 1);
            }
        }

        private string GetElementName(EntityMedicalTerm objElement)
        {
            try
            {
                //if (objElement.m_intEndIndex + 1 <= this.Text.Length)
                return this.Text.Substring(objElement.StartIndex, objElement.EndIndex - objElement.StartIndex + 1);
                //else
                //{
                //    objElement.m_intEndIndex = this.Text.Length;
                //    return this.Text.Substring(objElement.m_intStartIndex, this.Text.Length - objElement.m_intStartIndex + 1);
                //}
            }
            catch
            {
                //MessageBox.Show("text len: " + this.Text.Length + "  Start:" + objElement.m_intStartIndex.ToString() + "   End:" + objElement.m_intEndIndex.ToString() + "  diff len:" + Convert.ToString(this.Text.Length - objElement.m_intStartIndex + 1));
            }
            return string.Empty;
        }

        private void AdjustElementPosition_DeleteBath(int p_intIndex, int p_intDiffLength)
        {
            if (!IsAllowElementFreeEdit) return;
            if (this.m_lstMedicalTerm.Count == 0) return;
            if (p_intDiffLength == 0) return;

            int intStartIndex = p_intIndex;
            int intEndIndex = intStartIndex + p_intDiffLength - 1;

            EntityMedicalTerm objElement = null;
            this.m_lstMedicalTerm.Sort();
            for (int i = this.m_lstMedicalTerm.Count - 1; i >= 0; i--)
            {
                objElement = this.m_lstMedicalTerm[i];

                if (objElement.StartIndex >= intStartIndex && objElement.EndIndex <= intEndIndex)
                {
                    this.m_lstMedicalTerm.Remove(objElement);
                    continue;
                }

                if (objElement.StartIndex >= intStartIndex && objElement.EndIndex > intEndIndex)
                {
                    if (objElement.EndIndex - p_intDiffLength >= this.Text.Length)
                        continue;
                    objElement.StartIndex = intStartIndex;
                    objElement.EndIndex -= p_intDiffLength;
                    if (!RemoveElement(objElement))
                        objElement.Value = GetElementName(objElement); //this.Text.Substring(objElement.m_intStartIndex, objElement.m_intEndIndex - objElement.m_intStartIndex + 1);
                    continue;
                }

                if (objElement.StartIndex < intStartIndex && objElement.EndIndex <= intEndIndex && objElement.EndIndex >= intStartIndex)
                {
                    if (objElement.EndIndex - p_intDiffLength >= this.Text.Length)
                        continue;
                    objElement.EndIndex -= p_intDiffLength;
                    if (!RemoveElement(objElement))
                        objElement.Value = GetElementName(objElement); //this.Text.Substring(objElement.m_intStartIndex, objElement.m_intEndIndex - objElement.m_intStartIndex + 1);
                    continue;
                }

                if (objElement.StartIndex < intStartIndex && objElement.EndIndex > intEndIndex)
                {
                    if (objElement.EndIndex - p_intDiffLength >= this.Text.Length)
                        continue;
                    objElement.EndIndex -= p_intDiffLength;
                    if (!RemoveElement(objElement))
                        objElement.Value = GetElementName(objElement); //this.Text.Substring(objElement.m_intStartIndex, objElement.m_intEndIndex - objElement.m_intStartIndex + 1);
                    continue;
                }
            }
        }

        /// <summary>
        /// 调整元素.词汇位置---Delete
        /// </summary>
        /// <param name="p_intIndex"></param>
        /// <param name="p_intDiffLength"></param>
        private void AdjustElementPosition_Delete(int p_intIndex, int p_intDiffLength)
        {
            if (!IsAllowElementFreeEdit) return;

            EntityMedicalTerm objElement = CursorPositionToTerm_Delete(p_intIndex);
            if (objElement != null)
            {
                objElement.EndIndex -= p_intDiffLength;

                if (!RemoveElement(objElement))
                    objElement.Value = this.Text.Substring(objElement.StartIndex, objElement.EndIndex - objElement.StartIndex + 1);
            }
        }

        private bool RemoveElement(EntityMedicalTerm objElement)
        {
            if (objElement.EndIndex < 0 || objElement.StartIndex > objElement.EndIndex || (objElement.StartIndex == objElement.EndIndex && objElement.Value == string.Empty))
            {
                this.m_lstMedicalTerm.Remove(objElement);
                return true;
            }
            return false;
        }
        #endregion

        #region 调整内容段位置
        /// <summary>
        /// 调整内容段位置.插入
        /// </summary>
        /// <param name="p_intNewLen"></param>
        private void m_mthAdjustContentPosition_Insert(int p_intNewLen)
        {
            bool blnStatus = false;	//标识新增文本是否已被处理,若是则后续文本段只需位置向后偏移即可
            EntityUserContentInfo objContentInfo = null;
            int intIndex = 0;
            while (intIndex < this.m_lstTextContentInfos.Count)
            {
                objContentInfo = this.m_lstTextContentInfos[intIndex];

                //文本段位于新增文字之前时
                if (objContentInfo.EndIndex < m_intCurrentCursorIndex)
                {
                    //如果是最后一个文本段,则延长改文本段(同一用户)或添加一个新的文本段(不同用户)
                    if (intIndex == this.m_lstTextContentInfos.Count - 1)
                    {
                        if (objContentInfo.UserInfo == m_objCurrentModifyUser)
                        {
                            objContentInfo.EndIndex += p_intNewLen;				//延长
                        }
                        else
                        {
                            EntityUserContentInfo objInfo = new EntityUserContentInfo();		//新增
                            objInfo.UserInfo = m_objCurrentModifyUser;
                            objInfo.StartIndex = m_intCurrentCursorIndex;
                            objInfo.EndIndex = m_intCurrentCursorIndex + p_intNewLen - 1;
                            objInfo.UserID = m_objCurrentModifyUser.UserID;
                            objInfo.UserName = m_objCurrentModifyUser.UserName;
                            objInfo.ModifyDate = Common.Utils.Utils.ServerTime();
                            objInfo.ColorText = this.m_clrOldPartInsertText;
                            objInfo.UserInfo.ModifyDate = objInfo.ModifyDate;

                            this.m_lstTextContentInfos.Add(objInfo);
                        }

                        break;
                    }
                    //如果新增文字紧接当前文本段,则考虑是否可以衔接
                    else if (objContentInfo.EndIndex + 1 == m_intCurrentCursorIndex)
                    {
                        if (objContentInfo.UserInfo == m_objCurrentModifyUser)
                        {
                            objContentInfo.EndIndex += p_intNewLen;
                            blnStatus = true;		//衔接后,后续文本段只需后移
                        }
                    }

                    intIndex++;
                }
                else //文本段位于新增文字之后
                {
                    //新增文本已被某个文本段所衔接,后续的文本段只需后移
                    if (blnStatus)
                    {
                        objContentInfo.StartIndex += p_intNewLen;
                        objContentInfo.EndIndex += p_intNewLen;

                        intIndex++;
                    }
                    else
                    {
                        #region 新增文本与文本段交叉
                        //相同用户,直接衔接						
                        if (objContentInfo.UserInfo == m_objCurrentModifyUser)
                        {
                            objContentInfo.EndIndex += p_intNewLen;
                            intIndex++;
                            blnStatus = true;
                        }
                        else //不同用户
                        {
                            //新增文本处于文本段中间,将原有文本段分成两端,再插入一个新文本段
                            if (objContentInfo.StartIndex < m_intCurrentCursorIndex && m_intCurrentCursorIndex < objContentInfo.EndIndex)
                            {
                                int intEndIndex = objContentInfo.EndIndex;

                                //原有段->段1
                                objContentInfo.EndIndex = m_intCurrentCursorIndex - 1;

                                intIndex++;

                                //新段->段2 , 插入到段1之后
                                EntityUserContentInfo objNewInfo = new EntityUserContentInfo();
                                objNewInfo.UserInfo = m_objCurrentModifyUser;
                                objNewInfo.StartIndex = m_intCurrentCursorIndex;
                                objNewInfo.EndIndex = m_intCurrentCursorIndex + p_intNewLen - 1;
                                objNewInfo.UserID = m_objCurrentModifyUser.UserID;
                                objNewInfo.UserName = m_objCurrentModifyUser.UserName;
                                objNewInfo.ModifyDate = Common.Utils.Utils.ServerTime();
                                objNewInfo.ColorText = this.m_clrOldPartInsertText;
                                objNewInfo.UserInfo.ModifyDate = objNewInfo.ModifyDate;

                                this.m_lstTextContentInfos.Insert(intIndex, objNewInfo);

                                intIndex++;

                                //原有段的后部分->段3 
                                EntityUserContentInfo objOldInfo = new EntityUserContentInfo();
                                objOldInfo.UserInfo = objContentInfo.UserInfo;
                                objOldInfo.StartIndex = m_intCurrentCursorIndex + p_intNewLen;
                                objOldInfo.EndIndex = intEndIndex + p_intNewLen;
                                objOldInfo.UserID = objContentInfo.UserID;
                                objOldInfo.UserName = objContentInfo.UserName;
                                objOldInfo.ModifyDate = objContentInfo.ModifyDate;
                                objOldInfo.ColorText = objContentInfo.ColorText;
                                objOldInfo.UserInfo.ModifyDate = objOldInfo.ModifyDate;

                                this.m_lstTextContentInfos.Insert(intIndex, objOldInfo);

                                intIndex++;

                                blnStatus = true;
                            }
                            else if (objContentInfo.StartIndex >= m_intCurrentCursorIndex) //新增文本紧靠文本段之前
                            {
                                //插入一个新文本段

                                EntityUserContentInfo objNewInfo = new EntityUserContentInfo();
                                objNewInfo.UserInfo = m_objCurrentModifyUser;
                                objNewInfo.StartIndex = m_intCurrentCursorIndex;
                                objNewInfo.EndIndex = m_intCurrentCursorIndex + p_intNewLen - 1;
                                objNewInfo.UserID = m_objCurrentModifyUser.UserID;
                                objNewInfo.UserName = m_objCurrentModifyUser.UserName;
                                objNewInfo.ModifyDate = Common.Utils.Utils.ServerTime();
                                objNewInfo.ColorText = this.m_clrOldPartInsertText;
                                objNewInfo.UserInfo.ModifyDate = objNewInfo.ModifyDate;

                                this.m_lstTextContentInfos.Insert(intIndex, objNewInfo);

                                intIndex++;

                                blnStatus = true;
                            }
                            else
                            {
                                intIndex++;
                            }
                        }
                        #endregion
                    }
                }
            }
        }
        /// <summary>
        /// 删除视图信息
        /// </summary>
        /// <param name="p_intStartIndex"></param>
        /// <param name="p_intEndIndex"></param>
        /// <param name="p_strUserID"></param>
        private void m_mthDeleteTextView(int p_intStartIndex, int p_intEndIndex, string p_strUserID)
        {
            for (int i = this.m_lstTextContentInfos.Count - 1; i >= 0; i--)
            {
                if (this.m_lstTextContentInfos[i].StartIndex == p_intStartIndex &&
                    this.m_lstTextContentInfos[i].EndIndex == p_intEndIndex &&
                    this.m_lstTextContentInfos[i].UserID == p_strUserID)
                {
                    this.m_lstTextContentInfos.RemoveAt(i);
                    break;
                }
            }
        }

        private void m_mthAdjustElementPosition_Delete(int p_intStartIndex, int p_intOldLength, int p_intDiffLength)
        {
            //只能在自己添加的区域替换，并由此决定此区域必定连续区域。(即被替换的部分处于同一个文本段,一个文本段要么包含该区域,要么不包含该区域)
            //即添加只在一个区域内替换。
            //但需要把替换的区域后的区域更新
            int intTmpValue = 0;
            EntityMedicalTerm objContentInfo = null;
            for (int i = this.m_lstMedicalTerm.Count - 1; i >= 0; i--)
            {
                objContentInfo = this.m_lstMedicalTerm[i];

                if (this.m_blnIsBackspace)
                    intTmpValue = p_intStartIndex + 1;
                else
                    intTmpValue = p_intStartIndex;
                if (Math.Abs(p_intDiffLength) == 1 && (intTmpValue == objContentInfo.StartIndex) && (objContentInfo.StartIndex == objContentInfo.EndIndex))
                {
                    this.m_lstMedicalTerm.RemoveAt(i);
                    break;
                }

                //替换区域属于当前文本段
                if ((objContentInfo.StartIndex <= p_intStartIndex) && (objContentInfo.EndIndex >= p_intStartIndex))
                {
                    //此区域是替换区域，更新结束坐标
                    if (objContentInfo.EndIndex - p_intStartIndex >= Math.Abs(p_intDiffLength))
                        objContentInfo.EndIndex += p_intDiffLength;
                    else
                        objContentInfo.EndIndex = p_intStartIndex - 1;
                    SetElementName(objContentInfo);

                    if (objContentInfo.EndIndex < objContentInfo.StartIndex)
                    {
                        //此区域被删除
                        this.m_lstMedicalTerm.RemoveAt(i);
                    }
                }
                else if (objContentInfo.StartIndex > p_intStartIndex)
                {
                    int intTmpEndIndex = p_intStartIndex + Math.Abs(p_intDiffLength);
                    if (intTmpEndIndex > objContentInfo.EndIndex)
                    {
                        this.m_lstMedicalTerm.RemoveAt(i);
                    }
                    else if (intTmpEndIndex <= objContentInfo.StartIndex)
                    {
                        objContentInfo.StartIndex += p_intDiffLength;
                        objContentInfo.EndIndex += p_intDiffLength;
                    }
                    else if (intTmpEndIndex > objContentInfo.StartIndex && intTmpEndIndex <= objContentInfo.EndIndex)
                    {
                        objContentInfo.StartIndex = p_intStartIndex;
                        objContentInfo.EndIndex += p_intDiffLength;
                        SetElementName(objContentInfo);
                    }
                }
            }
            this.m_mthSetMedicalTermColor(0);
        }

        private void SetElementName(EntityMedicalTerm objElement)
        {
            try
            {
                objElement.Value = this.Text.Substring(objElement.StartIndex, objElement.EndIndex - objElement.StartIndex + 1);
            }
            catch
            {
                //MessageBox.Show("text len: " + this.Text.Length + "  Start:" + objElement.m_intStartIndex.ToString() + "   End:" + objElement.m_intEndIndex.ToString() + "  diff len:" + Convert.ToString(this.Text.Length - objElement.m_intStartIndex + 1));
            }
        }

        /// <summary>
        /// 调整内容段位置.删除
        /// </summary>
        /// <param name="p_intStartIndex"></param>
        /// <param name="p_intOldLength"></param>
        /// <param name="p_intDiffLength"></param>
        private void m_mthAdjustContentPosition_Delete(int p_intStartIndex, int p_intOldLength, int p_intDiffLength)
        {
            //只能在自己添加的区域替换，并由此决定此区域必定连续区域。(即被替换的部分处于同一个文本段,一个文本段要么包含该区域,要么不包含该区域)
            //即添加只在一个区域内替换。
            //但需要把替换的区域后的区域更新
            int intTmpValue = 0;
            EntityUserContentInfo objContentInfo = null;
            for (int i = this.m_lstTextContentInfos.Count - 1; i >= 0; i--)
            {
                objContentInfo = this.m_lstTextContentInfos[i];

                if (this.m_blnIsBackspace)
                    intTmpValue = p_intStartIndex + 1;
                else
                    intTmpValue = p_intStartIndex;
                if (Math.Abs(p_intDiffLength) == 1 && (intTmpValue == objContentInfo.StartIndex) && (objContentInfo.StartIndex == objContentInfo.EndIndex))
                {
                    this.m_lstTextContentInfos.RemoveAt(i);
                    this.m_mthDeleteTextView(objContentInfo.StartIndex, objContentInfo.EndIndex, objContentInfo.UserID);
                    break;
                }

                //替换区域属于当前文本段
                if ((objContentInfo.StartIndex <= p_intStartIndex) && (objContentInfo.EndIndex >= p_intStartIndex))
                {
                    //此区域是替换区域，更新结束坐标
                    objContentInfo.EndIndex += p_intDiffLength;

                    if (objContentInfo.EndIndex < objContentInfo.StartIndex)
                    {
                        //此区域被删除
                        this.m_lstTextContentInfos.RemoveAt(i);
                        this.m_mthDeleteTextView(objContentInfo.StartIndex, objContentInfo.EndIndex, objContentInfo.UserID);
                    }
                }
                //当前文本段在替换区域之后,位置向后偏移
                else if (objContentInfo.StartIndex > p_intStartIndex)
                {
                    //此区域在替换区域后，更新开始和结束坐标
                    objContentInfo.StartIndex += p_intDiffLength;
                    objContentInfo.EndIndex += p_intDiffLength;
                }
            }
            //将用户相同的相邻的文本段衔接起来作为一个文本段
            EntityUserContentInfo objPreContentInfo = null;
            for (int i = 0; i < this.m_lstTextContentInfos.Count; i++)
            {
                objContentInfo = this.m_lstTextContentInfos[i];

                if (objPreContentInfo != null && objPreContentInfo.EndIndex + 1 == objContentInfo.StartIndex && objPreContentInfo.UserInfo == objContentInfo.UserInfo)
                {
                    objPreContentInfo.EndIndex = objContentInfo.EndIndex;
                    this.m_lstTextContentInfos.RemoveAt(i);
                    this.m_mthDeleteTextView(objContentInfo.StartIndex, objContentInfo.EndIndex, objContentInfo.UserID);
                    i--;
                }
                else
                {
                    objPreContentInfo = objContentInfo;
                }
            }

            if (Math.Abs(p_intDiffLength) > 50)
            {
                this.Invalidate();
            }
        }
        #endregion

        #region 字体其他属性
        /// <summary>
        /// 设置字体属性标志
        /// </summary>
        private bool m_blnSetFontPropertyFlag = false;
        /// <summary>
        /// 字体其他属性
        /// </summary>
        /// <param name="p_intFlag"></param>
        public void m_mthSetSelectionFontProperty(int p_intFlag)
        {
            if (!(this.SelectionFont == null))
            {
                if (this.SelectionLength == 0) return;
                if (this.m_blnCursorPositionToDST())
                {
                    DialogBox.Msg("删除内容不能设置字体属性。", MessageBoxIcon.Information, uiHelper.frmCurr);
                    return;
                }
                else
                {
                    if (CheckOpCallDealIdeaElement()) // this.m_blnCursorPositionToTerm())
                    {
                        DialogBox.Msg("医学术语不能设置字体属性。", MessageBoxIcon.Information, uiHelper.frmCurr);
                        return;
                    }

                    if (this.m_blnCursorPositionToImage() || this.m_blnCursorPositionToCaption())
                    {
                        return;
                    }
                }
                Font currentFont = this.SelectionFont;
                FontStyle newFontStyle = this.SelectionFont.Style;

                using (RichTextBox rtx = new RichTextBox())
                {
                    rtx.Rtf = this.SelectedRtf;
                    rtx.Select(0, rtx.Text.Length);

                    switch (p_intFlag)
                    {
                        case 0:     // 颜色
                            using (ColorDialog dlg = new ColorDialog())
                            {
                                dlg.Color = rtx.SelectionColor;
                                if (dlg.ShowDialog(this.FindForm()) == DialogResult.OK)
                                {
                                    this.m_blnSetFontPropertyFlag = true;
                                    rtx.SelectionColor = dlg.Color;
                                }
                            }
                            break;
                        case 1:     // 粗体
                            newFontStyle = rtx.SelectionFont.Style ^ FontStyle.Bold;
                            break;
                        case 2:     // 斜体
                            newFontStyle = rtx.SelectionFont.Style ^ FontStyle.Italic;
                            break;
                        case 3:     // 下划线
                            newFontStyle = rtx.SelectionFont.Style ^ FontStyle.Underline;
                            break;
                        case 4:
                            break;
                        default:
                            break;
                    }
                    rtx.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                    this.SelectedRtf = rtx.SelectedRtf;
                }
            }
        }
        #endregion

        #region 自动设置术语
        /// <summary>
        /// 自动设置术语
        /// </summary>
        //private void m_mthAutoSetTerm()
        //{
        //    int intStartIndex = 0;
        //    int intEndIndex = 0;
        //    int intTmpIndex = 0;
        //    if (this.m_blnKey219)
        //    {
        //        this.m_blnKey219 = false;
        //        intStartIndex = this.SelectionStart - 1;
        //        if (intStartIndex >= this.Text.Length) return;
        //        intEndIndex = this.Text.IndexOf("]", intStartIndex);
        //        intTmpIndex = this.Text.IndexOf("[", intStartIndex + 1);
        //        if (intTmpIndex > intStartIndex && intTmpIndex < intEndIndex) return;
        //    }
        //    else if (this.m_blnKey221)
        //    {
        //        this.m_blnKey221 = false;
        //        intEndIndex = this.SelectionStart - 1;
        //        if (intEndIndex < 0) return;
        //        intTmpIndex = this.Text.LastIndexOf("]", intEndIndex - 1);
        //        intStartIndex = this.Text.LastIndexOf("[", intEndIndex);
        //        if (intTmpIndex < intEndIndex && intTmpIndex > intStartIndex) return;
        //    }
        //    else
        //    {
        //        return;
        //    }

        //    if (intStartIndex >= 0 && intEndIndex > 0)
        //    {
        //        clsMedicalTerm objMedicalTerm = new clsMedicalTerm();
        //        objMedicalTerm.m_intStartIndex = intStartIndex;
        //        objMedicalTerm.m_intEndIndex = intEndIndex;
        //        objMedicalTerm.m_clrTerm = this.m_clrTerm;
        //        objMedicalTerm.m_strUserID = m_objCurrentModifyUser.m_strUserID;
        //        objMedicalTerm.m_strUserName = m_objCurrentModifyUser.m_strUserName;
        //        objMedicalTerm.m_dtmCreateTime = DateTime.Now;
        //        objMedicalTerm.m_intType = 1;
        //        objMedicalTerm.m_strValue = this.Text.Substring(intStartIndex, intEndIndex - intStartIndex + 1);
        //        this.m_lstMedicalTerm.Add(objMedicalTerm);
        //        this.m_mthSetMedicalTermColor(1);
        //    }
        //}
        #endregion

        #region 调用常用医学术语

        private bool IsInsertNorElement = false;

        private bool IsInsertPatElement = false;

        /// <summary>
        /// 当前控件所在窗体类型
        /// </summary>
        private string FormTypeName
        {
            get
            {
                if (this.FindForm() == null)
                    return string.Empty;

                return this.FindForm().GetType().ToString();
            }
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="p_strElementID"></param>
        /// <param name="p_strElmentName"></param>
        private void m_mthAddElement(string p_strCaseCode, string p_strElementID, string p_strElmentName, EntityDragRichItem dragRichItem, bool p_blnReplaceFlag)
        {
            this.stopReDraw();
            this.m_blnLoadTermStatus = true;
            if (!p_blnReplaceFlag)
            {
                this.SelectionLength = 0;
            }

            if (p_strElementID.ToLower() == "patinfo")
            {
                this.IsInsertPatElement = true;
            }

            this.IsInsertNorElement = true;
            int intStartIndex = this.SelectionStart;
            //if (p_strElmentName.IndexOf("[") < 0)
            //    p_strElmentName = "[" + p_strElmentName;
            //if (p_strElmentName.IndexOf("]") < 0)
            //    p_strElmentName += "]";
            //p_strElmentName = this.m_strTermPrefix + p_strElmentName;
            this.m_mthInsertText(p_strElmentName);
            //this.m_mthAdjustTermPosition();
            this.IsInsertNorElement = false;

            string strUserID = m_objCurrentModifyUser.UserID;
            string struserName = m_objCurrentModifyUser.UserName;
            if (this.FormTypeName == c_strFormClass)
            {
                strUserID = "TempUser";
                struserName = "TempUser";
            }

            EntityMedicalTerm objMedicalTerm = new EntityMedicalTerm();
            objMedicalTerm.StartIndex = intStartIndex;
            objMedicalTerm.EndIndex = intStartIndex + p_strElmentName.Length - 1;
            objMedicalTerm.ColorTerm = this.m_clrTerm;
            objMedicalTerm.UserID = strUserID;
            objMedicalTerm.UserName = struserName;
            objMedicalTerm.CreateTime = DateTime.Now;
            objMedicalTerm.Type = 0;
            if (string.IsNullOrEmpty(p_strCaseCode))
                objMedicalTerm.CaseCode = GlobalCase.caseInfo.CaseCode;
            else
                objMedicalTerm.CaseCode = p_strCaseCode;
            objMedicalTerm.TID = p_strElementID;
            objMedicalTerm.Value = p_strElmentName;
            this.m_lstMedicalTerm.Add(objMedicalTerm);
            this.m_mthSetMedicalTermColor(0);
            this.m_mthFireEvtTextChange(false);
            this.m_blnLoadTermStatus = false;
            this.SelectionStart = objMedicalTerm.EndIndex + 1;
            this.SelectionLength = 0;
            this.m_mthAdjustTermPosition();
            this.reDraw();

            if (dragRichItem != null)
            {
                this.m_mthAddItemTemple(dragRichItem.DragString, dragRichItem.DragMedicalTerm);
            }
        }
        /// <summary>
        /// 缺省元素模板
        /// </summary>
        public void m_mthDefaultElementTemplate()
        {
            if (this._blnReadOnly) return;
            if (intConfineType == 2) return;
            if (!this.m_blnCheckCursorValidPostion()) return;
            string strElementID = string.Empty;
            string strElementName = string.Empty;
            EntityDragRichItem dragRichItem = null;
            frmLoadElement frm = new frmLoadElement();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.TemplateFlag = true;
            if (frm.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                strElementID = frm.ElementID;
                strElementName = frm.ElementName;
                dragRichItem = frm.DragRichItem;
            }
            else
                return;

            this.m_mthAddElement(string.Empty, strElementID, strElementName, dragRichItem, false);
        }

        /// <summary>
        /// 调用常用医学术语
        /// </summary>
        public void m_mthAddMedicalTerm()
        {
            if (this._blnReadOnly) return;
            if (intConfineType == 2) return;
            if (!this.m_blnCheckCursorValidPostion()) return;
            string strElementID = string.Empty;
            string strElementName = string.Empty;
            EntityDragRichItem dragRichItem = null;
            frmLoadElement frm = new frmLoadElement();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.TemplateFlag = this.TemplateFlag;
            if (frm.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                strElementID = frm.ElementID;
                strElementName = frm.ElementName;
                dragRichItem = frm.DragRichItem;
                this.m_mthAddElement(string.Empty, strElementID, strElementName, dragRichItem, false);
            }
        }

        /// <summary>
        /// 调用常用医学术语
        /// </summary>
        /// <param name="p_strElementID"></param>
        /// <param name="p_strElementName"></param>
        private void m_mthInsertMedicalTerm(string p_strCaseCode, string p_strElementID, string p_strElementName, EntityDragRichItem dragRichItem, bool p_blnReplaceFlag)
        {
            this.m_mthAddElement(p_strCaseCode, p_strElementID, p_strElementName, dragRichItem, p_blnReplaceFlag);
        }

        /// <summary>
        /// 添加元素(智能引用)
        /// </summary>
        /// <param name="p_objRefItem"></param>
        private void m_mthAddElement(EntityIntellectiveRefItem p_objRefItem)
        {
            if (p_objRefItem == null) return;

            string strElementID = "Intellection;" + p_objRefItem.CaseCode + ";" + p_objRefItem.ColCode;
            string strElementName = uiHelper.CaseColumnContent(p_objRefItem.CaseCode, p_objRefItem.ColCode, p_objRefItem.ColName);

            this.m_mthInsertElement(strElementID, strElementName);
        }

        /// <summary>
        /// 加元素(外部调用)
        /// </summary>
        /// <param name="p_intType">1 门诊外部调用</param>
        /// <param name="p_strContent"></param>
        public void m_mthAddElement(int p_intType, string p_strContent)
        {
            if (string.IsNullOrEmpty(p_strContent)) return;

            if (p_intType == 1)
            {
                this.m_mthInsertElement(OP_Call, p_strContent);
            }
        }

        /// <summary>
        /// 插入元素
        /// </summary>
        /// <param name="strElementID"></param>
        /// <param name="strElementName"></param>
        public void m_mthInsertElement(string strElementID, string strElementName)
        {
            this.stopReDraw();
            this.m_blnLoadTermStatus = true;

            this.IsInsertNorElement = true;
            int intStartIndex = this.SelectionStart;

            if (strElementID.ToLower().StartsWith("intellection"))
            {
                this.IsInsertPatElement = true;
            }

            //if (m_blnExtCall() && DBColName.ToLower() == "dealidea")
            //{
            //    strElementName = "" + strElementName.Trim() + " ";
            //}
            //else
            //{
            //    if (strElementName.IndexOf("[") < 0)
            //        strElementName = "[" + strElementName;
            //    if (strElementName.IndexOf("]") < 0)
            //        strElementName += "]";
            //}
            strElementName = this.m_strTermPrefix + strElementName.Trim();
            this.m_mthInsertText(strElementName);
            //this.SelectionStart = intStartIndex;
            //this.SelectionLength = strElementName.Length;
            //this.SelectionFont = FntElement;
            //this.SelectionColor = m_clrTerm;//Color.Red;
            this.IsInsertNorElement = false;
            this.SelectionLength = 0;

            EntityMedicalTerm objMedicalTerm = new EntityMedicalTerm();
            objMedicalTerm.StartIndex = intStartIndex;
            objMedicalTerm.EndIndex = intStartIndex + strElementName.Length - 1;
            objMedicalTerm.ColorTerm = this.m_clrTerm;
            objMedicalTerm.UserID = m_objCurrentModifyUser.UserID;
            objMedicalTerm.UserName = m_objCurrentModifyUser.UserName;
            objMedicalTerm.CreateTime = DateTime.Now;
            objMedicalTerm.Type = 0;
            objMedicalTerm.TID = strElementID;
            objMedicalTerm.Value = strElementName;
            this.m_lstMedicalTerm.Add(objMedicalTerm);
            this.m_mthSetMedicalTermColor(0);
            this.m_mthFireEvtTextChange(false);
            this.m_blnLoadTermStatus = false;
            this.SelectionStart = objMedicalTerm.EndIndex + 1;
            this.SelectionLength = 0;
            this.reDraw();
        }
        #endregion

        #region 检查当前光标位置是否在医学术语内

        /// <summary>
        /// 检查当前光标位置是否在医学术语内
        /// </summary>
        /// <returns></returns>
        private bool m_blnCursorPositionToTerm()
        {
            if (this.SelectionLength == 0)
            {
                return this.m_blnCursorPositionToTerm2(this.m_intCurrentCursorIndex);
            }
            bool blnRet = false;
            int intStartIndex = this.m_intCurrentCursorIndex;
            int intEndIndex = intStartIndex + this.SelectionLength;
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if ((obj.StartIndex < intStartIndex && intStartIndex <= obj.EndIndex) ||
                    (obj.StartIndex > intStartIndex && obj.StartIndex < intEndIndex) ||
                    (obj.EndIndex > intStartIndex && obj.EndIndex <= intEndIndex) ||
                    (obj.StartIndex <= intStartIndex && intEndIndex <= obj.EndIndex) ||
                    (obj.EndIndex >= intStartIndex && obj.EndIndex < intEndIndex) ||
                    (obj.StartIndex <= intStartIndex && obj.EndIndex <= intEndIndex && obj.EndIndex >= intStartIndex))
                {
                    this.m_objEditElement = obj;
                    blnRet = true;
                    break;
                }
                else
                {
                    //if (this.SelectionLength > 0)
                    if (this.m_blnMuliSelectedFlag)
                    {
                        if (obj.StartIndex == intStartIndex)
                        {
                            this.m_objEditElement = obj;
                            blnRet = true;
                            break;
                        }
                    }
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            //this.ReadOnly = blnRet;
            return blnRet;
        }
        /// <summary>
        /// 检查当前光标位置是否在医学术语内
        /// </summary>
        /// <param name="p_intIndex"></param>
        /// <param name="p_objKey"></param>
        /// <returns></returns>
        private bool m_blnCursorPositionToTerm(int p_intIndex, Keys p_objKey)
        {
            bool blnRet = false;
            if (p_objKey == Keys.Delete)
            {
                foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
                {
                    if (obj.StartIndex <= p_intIndex && p_intIndex <= obj.EndIndex)
                    {
                        blnRet = true;
                        break;
                    }
                }
            }
            else if (p_objKey == Keys.Back)
            {
                foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
                {
                    if (obj.StartIndex < p_intIndex && p_intIndex <= obj.EndIndex + 1)
                    {
                        blnRet = true;
                        break;
                    }
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            //this.ReadOnly = blnRet;
            return blnRet;
        }
        /// <summary>
        /// 检查当前光标位置是否在医学术语内
        /// </summary>
        /// <param name="p_intIndex"></param>
        /// <param name="p_objKey"></param>
        /// <returns></returns>
        private bool m_blnCursorPositionToTermOpCall(int p_intIndex, Keys p_objKey)
        {
            bool blnRet = false;

            if (!IsDealIdeaCol)
            {
                return blnRet;
            }
            if (p_objKey == Keys.Delete)
            {
                foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
                {
                    if (obj.StartIndex <= p_intIndex && p_intIndex <= obj.EndIndex && obj.TID.ToLower() == OP_Call)
                    {
                        blnRet = true;
                        break;
                    }
                }
            }
            else if (p_objKey == Keys.Back)
            {
                foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
                {
                    if (obj.StartIndex < p_intIndex && p_intIndex <= obj.EndIndex + 1 && obj.TID.ToLower() == OP_Call)
                    {
                        blnRet = true;
                        break;
                    }
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            //this.ReadOnly = blnRet;
            return blnRet;
        }
        /// <summary>
        /// 检查当前光标位置是否在医学术语内
        /// </summary>
        /// <param name="p_intIndex"></param>
        /// <returns></returns>
        private bool m_blnCursorPositionToTerm2(int p_intIndex, ref EntityMedicalTerm p_objTerm)
        {
            bool blnRet = false;
            p_objTerm = null;
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.StartIndex < p_intIndex && p_intIndex <= obj.EndIndex)
                {
                    blnRet = true;
                    p_objTerm = obj;
                    break;
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            //this.ReadOnly = blnRet;
            return blnRet;
        }

        private EntityMedicalTerm CursorPositionToTerm(int index)
        {
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.StartIndex < index && index <= obj.EndIndex)
                {
                    return obj;
                }
            }
            return null;
        }

        private EntityMedicalTerm CursorPositionToTerm_Insert(int index)
        {
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.StartIndex < index && index <= obj.EndIndex + 1)
                {
                    return obj;
                }
            }
            return null;
        }

        private EntityMedicalTerm CursorPositionToTerm_Delete(int index)
        {
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.StartIndex <= index && index <= obj.EndIndex)
                {
                    return obj;
                }
            }
            return null;
        }

        private bool CursorPositionToTerm_Midd(int index)
        {
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.StartIndex < index && index <= obj.EndIndex)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CursorPositionToTerm_End(int index)
        {
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.EndIndex + 1 == index)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckEditElementPosition()
        {
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.StartIndex <= this.SelectionStart && this.SelectionStart <= obj.EndIndex)
                {
                    this.m_objEditElement = obj;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查当前光标位置是否在医学术语内
        /// </summary>
        /// <param name="p_intIndex"></param>
        /// <returns></returns>
        private bool m_blnCursorPositionToTerm2(int p_intIndex)
        {
            bool blnRet = false;
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.StartIndex < p_intIndex && p_intIndex <= obj.EndIndex)
                {
                    this.m_objEditElement = obj;
                    blnRet = true;
                    break;
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            //this.ReadOnly = blnRet;
            return blnRet;
        }

        private bool m_blnCursorPositionToTermOpCall(int p_intIndex)
        {
            bool blnRet = false;
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.StartIndex < p_intIndex && p_intIndex <= obj.EndIndex && obj.TID.ToLower() == OP_Call)
                {
                    blnRet = true;
                    break;
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            //this.ReadOnly = blnRet;
            return blnRet;
        }

        /// <summary>
        /// 检查当前光标位置是否在医学术语内
        /// </summary>
        /// <param name="p_intStart"></param>
        /// <param name="p_intEnd"></param>
        /// <returns></returns>
        private bool m_blnCursorPositionToTerm3(int p_intStart, int p_intEnd)
        {
            bool blnRet = false;
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if ((obj.StartIndex < p_intStart && p_intStart <= obj.EndIndex) ||
                    (obj.StartIndex < p_intEnd && p_intEnd <= obj.EndIndex))
                {
                    blnRet = true;
                    break;
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            //this.ReadOnly = blnRet;
            return blnRet;
        }
        #endregion

        #region 调整医学术语位置

        private bool IsElement(RichTextBox rtx, int start)
        {
            rtx.Select(start, 1);
            if (rtx.SelectionFont.Name == "黑体")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 调整医学术语位置
        /// </summary>
        private void m_mthAdjustTermPosition()
        {
            if (this.m_lstMedicalTerm.Count == 0) return;
            this.m_lstMedicalTerm.Sort();

            int intType = 0;
            int intIndex = -1;
            using (RichTextBox rich = new RichTextBox())
            {
                rich.Rtf = this.Rtf;
                for (int i = 0; i < rich.Text.Length; i++)
                {
                    if (IsElement(rich, i))
                    {
                        intIndex++;
                        if (intIndex < this.m_lstMedicalTerm.Count)
                        {
                            this.m_lstMedicalTerm[intIndex].StartIndex = i;
                            this.m_lstMedicalTerm[intIndex].EndIndex = i + this.m_lstMedicalTerm[intIndex].Value.Length - 1;
                            //this.m_lstMedicalTerm[intIndex].IsChangeColor = false;

                            i = this.m_lstMedicalTerm[intIndex].EndIndex;
                        }
                    }
                }
            }

            if (intIndex >= 0)
            {
                if (CursorPositionToTerm(this.SelectionStart) == null)
                    this.m_mthSetMedicalTermColor(intType);
            }
            else
            {
                this.m_lstMedicalTerm.Clear();
            }
        }
        #endregion

        #region 设置医学术语
        /// <summary>
        /// 设置医学术语
        /// </summary>
        public void m_mthSetMedicalTerm()
        {
            if (!this.m_blnCheckCursorValidPostion()) return;
            if (this.SelectionLength == 0) return;

            this.stopReDraw();
            this.m_blnLoadTermStatus = true;
            int intStartIndex = this.SelectionStart;
            int intEndIndex = intStartIndex + this.SelectionLength;
            //this.SelectionLength = 0;
            //this.SelectionStart = intEndIndex;
            //this.m_mthInsertText("]");
            //this.SelectionStart = intStartIndex;
            //this.m_mthInsertText(this.m_strTermPrefix + "[");
            //intEndIndex += Convert.ToString(this.m_strTermPrefix + "[]").Length;

            EntityMedicalTerm objMedicalTerm = new EntityMedicalTerm();
            objMedicalTerm.StartIndex = intStartIndex;
            objMedicalTerm.EndIndex = intEndIndex - 1;
            objMedicalTerm.UserID = m_objCurrentModifyUser.UserID;
            objMedicalTerm.UserName = m_objCurrentModifyUser.UserName;
            objMedicalTerm.CreateTime = DateTime.Now;
            objMedicalTerm.ColorTerm = this.m_clrTerm;
            objMedicalTerm.Value = this.Text.Substring(intStartIndex, intEndIndex - intStartIndex);
            this.m_lstMedicalTerm.Add(objMedicalTerm);

            this.m_mthSetMedicalTermColor(0);
            this.m_mthFireEvtTextChange(false);
            this.m_blnLoadTermStatus = false;
            this.SelectionStart = intStartIndex;
            this.SelectionLength = 0;
        }
        #endregion

        #region 设置术语颜色
        /// <summary>
        /// 设置术语颜色
        /// </summary>
        private void m_mthSetMedicalTermColor(int p_intType)
        {
            if (this.m_lstMedicalTerm.Count == 0) return;

            using (RichTextBox rtx = new RichTextBox())
            {
                rtx.Rtf = this.Rtf;

                int intCurrIndex = this.SelectionStart;
                rtx.SelectionLength = 0;

                for (int i = 0; i < this.m_lstMedicalTerm.Count; i++)
                {
                    if (this.m_strTermPrefix == string.Empty)
                    {
                        if (p_intType == 1)
                        {
                            rtx.SelectionStart = this.m_lstMedicalTerm[i].StartIndex + 1;
                            rtx.SelectionLength = this.m_lstMedicalTerm[i].Value.Length - 1;
                        }
                        else
                        {
                            rtx.SelectionStart = this.m_lstMedicalTerm[i].StartIndex;
                            rtx.SelectionLength = this.m_lstMedicalTerm[i].Value.Length;
                        }
                        if (this.m_lstMedicalTerm[i].TID == OP_Call)
                            rtx.SelectionColor = Color.Black;
                        else if (this.m_lstMedicalTerm[i].IsChangeColor)
                            rtx.SelectionColor = ElementEditColor;
                        else
                            rtx.SelectionColor = this.m_clrTerm;
                        rtx.SelectionFont = FntElement;
                    }
                    else
                    {
                        rtx.SelectionStart = this.m_lstMedicalTerm[i].StartIndex;
                        rtx.SelectionLength = 1;
                        rtx.SelectionColor = this.BackColor;
                        rtx.SelectionStart = this.m_lstMedicalTerm[i].StartIndex + 1;
                        rtx.SelectionLength = this.m_lstMedicalTerm[i].Value.Length - 1;
                    }
                }

                IsReplaceRtf = true;
                this.Rtf = rtx.Rtf;
                this.intTextLength = this.Text.Length;
                IsReplaceRtf = false;
                this.SelectionLength = 0;
                this.SelectionStart = intCurrIndex;
            }
        }
        #endregion

        #region 撤销医学术语
        /// <summary>
        /// 撤销医学术语
        /// </summary>
        public void m_mthCancelMedicalTerm()
        {
            if (this.SelectionLength > 0) this.SelectionLength = 0;
            int intCurrIndex = this.SelectionStart;
            foreach (EntityMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.StartIndex < intCurrIndex && intCurrIndex <= obj.EndIndex)
                {
                    EntityMedicalTerm objTmp = new EntityMedicalTerm();
                    objTmp.StartIndex = obj.StartIndex;
                    objTmp.EndIndex = obj.EndIndex;
                    this.m_lstMedicalTerm.Remove(obj);

                    this.SelectionStart = objTmp.StartIndex;
                    this.SelectionLength = objTmp.EndIndex - objTmp.StartIndex + 1;
                    this.SelectionColor = Color.Black;

                    this.SelectionStart = objTmp.EndIndex;
                    this.SelectionLength = 1;
                    this.m_mthDel();

                    this.SelectionStart = objTmp.StartIndex;
                    if (this.m_strTermPrefix == string.Empty)
                    {
                        this.SelectionLength = 1;
                    }
                    else
                    {
                        this.SelectionLength = 2;
                    }
                    this.m_mthDel();

                    break;
                }
            }
        }
        #endregion

        #region 插入图片
        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="p_objImageInfo"></param>
        /// <param name="p_objImage"></param>
        public void m_mthEditImage(EntityImageInfo p_objImageInfo, Image p_objImage)
        {
            this.stopReDraw();
            this.SelectionStart = p_objImageInfo.StartIndex;
            this.SelectionLength = 1;
            RichTextBoxPlus.InsertImage(this, p_objImage);
            this.reDraw();
            this.ValueChangedFlag = true;
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="p_objImage"></param>
        public void m_mthInsertImage(Image p_objImage)
        {
            byte[] bytArr = Function.ConvertImageToByte(p_objImage, 0);
            this.m_mthInsertImage("noknow", p_objImage, bytArr);
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        public void m_mthInsertImage(string p_strFile)
        {
            Image img = Image.FromFile(p_strFile);
            byte[] bytArr = Function.ConvertImageToByte(img, 0);
            this.m_mthInsertImage("noknow", img, bytArr);
        }
        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="p_strImageID"></param>
        /// <param name="p_btyObjectArr"></param>
        public void m_mthInsertImage(string p_strImageID, byte[] p_bytObjectArr)
        {
            Image img = Function.ConvertByteToImage(p_bytObjectArr);
            this.m_mthInsertImage(p_strImageID, img, p_bytObjectArr);
        }
        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="p_strImageID"></param>
        /// <param name="p_objImage"></param>
        /// <param name="p_bytObjectArr"></param>
        public void m_mthInsertImage(string p_strImageID, Image p_objImage, byte[] p_bytObjectArr)
        {
            try
            {
                if (!this.m_blnCheckCursorValidPostion()) return;
                this.stopReDraw();
                RichTextBoxPlus.InsertImage(this, p_objImage);
                this.reDraw();
                this.ValueChangedFlag = true;
            }
            catch (Exception objEx)
            {
                DialogBox.Msg("插入图片异常:\r\n" + objEx.Message, MessageBoxIcon.Information, uiHelper.frmCurr);
            }
        }

        #endregion

        #region 插入文本
        /// <summary>
        /// 插入文本
        /// </summary>
        /// <param name="p_strText"></param>
        public void m_mthInsertText(string p_strText)
        {
            if (string.IsNullOrEmpty(p_strText)) return;

            //Clipboard.Clear();
            //Clipboard.SetText(p_strText);
            //this.Paste();
            //Clipboard.Clear();

            RichTextBoxPlus.InsertText(this, p_strText);
            if (this.Focused && this.FindForm() != null)
            {
                SendKeys.Send("{LEFT}");
                SendKeys.Send("{RIGHT}");
            }
        }

        /// <summary>
        /// 字段引用
        /// </summary>
        /// <param name="p_strText"></param>
        /// <param name="p_lstElement"></param>
        public void ColumnReference(string p_strText, List<EntityMedicalTerm> p_lstElement)
        {
            this.Focus();
            try
            {
                this.stopReDraw();
                if (this.Text.Trim() != string.Empty)
                {
                    this.ClearText();
                }
                this.m_intCurrentCursorIndex = 0;

                string strFirstlineCaption = this.m_strGetFirstlineCaption();
                if (!string.IsNullOrEmpty(strFirstlineCaption) && p_strText.StartsWith(strFirstlineCaption))
                {
                    p_strText = p_strText.Replace(strFirstlineCaption, "");
                }

                this.m_mthInsertText(p_strText);
                this.m_lstMedicalTerm.AddRange(p_lstElement);
                this.m_mthSetMedicalTermColor(0);
            }
            finally
            {
                this.reDraw();
                this.Parent.Invalidate();
            }
        }

        /// <summary>
        /// 引用插入
        /// </summary>
        /// <param name="p_strText"></param>
        /// <param name="p_lstElement"></param>
        public void m_mthReferenceInsert(string p_strText, List<EntityMedicalTerm> p_lstElement)
        {
            this.Focus();
            this.stopReDraw();
            this.SelectionStart = this.Text.Length;
            this.SelectionLength = 0;
            this.m_intCurrentCursorIndex = this.SelectionStart;

            string strFirstlineCaption = this.m_strGetFirstlineCaption();
            if (!string.IsNullOrEmpty(strFirstlineCaption) && p_strText.StartsWith(strFirstlineCaption))
            {
                p_strText = p_strText.Replace(strFirstlineCaption, "");
            }
            this.m_mthInsertText(p_strText);

            DateTime dtmNow = Common.Utils.Utils.ServerTime();
            foreach (EntityMedicalTerm obj in p_lstElement)
            {
                if (string.IsNullOrEmpty(obj.UserID))
                {
                    obj.UserID = m_objCurrentModifyUser.UserID;
                    obj.UserName = m_objCurrentModifyUser.UserName;
                    obj.CreateTime = dtmNow;
                }
            }
            this.m_mthSetMedicalTerm(p_lstElement);
            this.reDraw();
        }

        /// <summary>
        /// 插空值标志
        /// </summary>
        private bool m_blnInsertEmpty = false;
        /// <summary>
        /// 插空值
        /// </summary>
        /// <param name="p_strChr"></param>
        private void m_mthInsertEmpty(string p_strChr)
        {
            int idx = 0;
            if (this.Text.Length > 0)
            {
                idx = this.Text.Length - 1;
            }
            this.SelectionStart = idx;
            RichTextBoxPlus.InsertText(this, p_strChr);
            this.Select(idx, p_strChr.Length);
            this.SelectionFont = new Font("宋体", 18, FontStyle.Regular);
            this.Select(idx, p_strChr.Length);
            RichTextBoxPlus.InsertText(this, "");
        }

        /// <summary>
        /// 插空值
        /// </summary>
        public void m_mthInsertEmpty()
        {
            if (this.Multiline)
            {
                this.m_blnInsertEmpty = true;
                string strChr = "|";
                int idx = this.SelectionStart;
                Graphics objGrp = this.CreateGraphics();
                if ((this.Width < (objGrp.MeasureString(this.Text, this.Font)).Width))
                {
                    this.m_mthInsertEmpty(strChr);
                }
                else
                {
                    if (this.Height > this.m_intRowSpacing * 2)// (objGrp.MeasureString(this.Text, this.Font)).Height)
                    {
                        this.m_mthInsertEmpty(strChr);
                    }
                    else
                    {
                        List<int> lstImageIdx = new List<int>();
                        RichTextBoxPlus.GetImageIndex(this, 0, this.Text.Length, ref lstImageIdx);
                        if (lstImageIdx != null && lstImageIdx.Count > 0)
                        {
                            this.m_mthInsertEmpty(strChr);
                        }
                    }
                }
                this.SelectionStart = idx;
                this.SelectionLength = 0;
                this.m_blnInsertEmpty = false;
            }
            else
            {
                this.UseRowSpacing = false;
            }
        }
        #endregion

        #region 复制文本
        /// <summary>
        /// 复制文本
        /// </summary>
        public void m_mthPaste()
        {
            if (!this.m_blnCheckCursorValidPostion()) return;
            if (this.intConfineType == 2) return;
            if (this.SelectionLength > 0) this.SelectionLength = 0;
            string strContent = Clipboard.GetText(TextDataFormat.UnicodeText);
            if (string.IsNullOrEmpty(strContent)) return;
            int pos = strContent.IndexOf(this.m_strSysSplit);
            if (pos > 0)
            {
                string strPatId = string.Empty;
                if (GlobalPatient.currPatient != null)
                {
                    strPatId = GlobalPatient.currPatient.PatientID;
                }
                if (strPatId != strContent.Substring(0, pos))
                {
                    DialogBox.Msg("非本人病历资料，禁止复制。", MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    strContent = strContent.Substring(pos + 2);
                }
            }

            strContent = strContent.Replace("[", "").Replace("]", "");
            Clipboard.SetText(strContent);

            if (MaxLength > 0)// && evtReachMaxLength != null)
            {
                if (this.Text.Length + strContent.Length > this.MaxLength)
                {
                    DialogBox.Msg("文本长度大于允许的最大的长度。", MessageBoxIcon.Information, uiHelper.frmCurr);
                    return;
                }
            }

            this.m_mthInsertText(strContent);
            this.m_mthSetMedicalTermColor(0);
        }
        #endregion

        #region 清除指定内容(简单)
        /// <summary>
        /// 清除指定内容(简单)
        /// </summary>
        /// <param name="p_strText"></param>
        public void m_mthCleartext(string p_strText)
        {
            if (this.Text.Trim().Length == 0 || string.IsNullOrEmpty(p_strText)) return;

            int pos = this.Text.IndexOf(p_strText);
            if (pos < 0) return;

            int intStartIndex = pos;
            int intEndIndex = pos + p_strText.Length;

            if (!this.m_blnCursorPositionToTerm3(intStartIndex, intEndIndex))
            {
                this.Select(pos, p_strText.Length);
                this.m_mthInsertText(" ");
            }
        }
        #endregion

        #region 检查当前光标位置是否在片图内
        /// <summary>
        /// 检查当前光标位置是否在片图内
        /// </summary>
        /// <returns></returns>
        private bool m_blnCursorPositionToImage()
        {
            return false;
            bool blnRet = false;
            int intStartIndex = this.m_intCurrentCursorIndex;
            int intEndIndex = intStartIndex + this.SelectionLength;

            List<int> lstImageIdx = new List<int>();
            RichTextBoxPlus.GetImageIndex(this, intStartIndex, intEndIndex, ref lstImageIdx);
            foreach (int idx in lstImageIdx)
            {
                if ((idx < intStartIndex && intStartIndex <= idx) ||
                    (idx > intStartIndex && idx < intEndIndex) ||
                    (idx > intStartIndex && idx <= intEndIndex))
                {
                    blnRet = true;
                    break;
                }
                else
                {
                    if (this.SelectionLength > 0)
                    {
                        if (idx == intStartIndex)
                        {
                            blnRet = true;
                            break;
                        }
                    }
                }
            }
            if (m_evtSelectedImage != null)
            {
                clsEvtSelectedImage evtArg = new clsEvtSelectedImage();
                evtArg.blnSelected = blnRet;
                m_evtSelectedImage(this, evtArg);
            }
            return blnRet;
        }

        /// <summary>
        /// 检查当前光标位置是否在片图内
        /// </summary>
        /// <param name="p_intIndex"></param>
        /// <returns></returns>
        //private bool m_blnCursorPositionToImage3(int p_intIndex)
        //{
        //    bool blnRet = false;
        //p_objImage = null;

        //Image imgPos = null;
        //clsRichTextBoxPlus.GetImage(this, p_intIndex, ref imgPos);
        //if (imgPos != null)
        //{
        //    blnRet = true;
        //    p_objImage = new clsImageInfo();
        //    p_objImage.m_intStartIndex = p_intIndex;
        //    p_objImage.m_bytImageArr = clsFunction.s_bytConvertImageToByte(imgPos, 0);
        //}
        //    return blnRet;
        //}
        /// <summary>
        /// 检查当前光标位置是否在片图内
        /// </summary>
        /// <param name="p_intIndex"></param>
        /// <returns></returns>
        private bool m_blnCursorPositionToImage4(int p_intIndex)
        {
            this.m_objCurrentSelectedImage = null;
            bool blnRet = RichTextBoxPlus.GetImageStatus(this, p_intIndex);
            if (blnRet)
            {
                Image imgPos = null;
                RichTextBoxPlus.GetImage(this, p_intIndex, ref imgPos);
                if (imgPos != null)
                {
                    this.m_objCurrentSelectedImage = new EntityImageInfo();
                    this.m_objCurrentSelectedImage.StartIndex = p_intIndex;
                    this.m_objCurrentSelectedImage.ImageArr = Function.ConvertImageToByte(imgPos, 0);
                }
                if (m_evtSelectedImage != null)
                {
                    clsEvtSelectedImage evtArg = new clsEvtSelectedImage();
                    evtArg.blnSelected = blnRet;
                    m_evtSelectedImage(this, evtArg);
                }
            }
            return blnRet;
        }

        #endregion

        #region 删除元素
        /// <summary>
        /// 删除元素
        /// </summary>
        /// <param name="p_intIndex"></param>
        public void m_mthDelTerm(int p_intIndex)
        {
            if (this.m_lstMedicalTerm.Count == 0)
                return;
            if (p_intIndex < 0 || p_intIndex > this.m_lstMedicalTerm.Count - 1)
                return;

            EntityMedicalTerm objTerm = this.m_lstMedicalTerm[p_intIndex];
            if (objTerm == null) return;
            this.m_lstMedicalTerm.RemoveAt(p_intIndex);
            EntityMedicalTerm[] objTermArr = new EntityMedicalTerm[this.m_lstMedicalTerm.Count];
            this.m_lstMedicalTerm.CopyTo(objTermArr);
            this.m_lstMedicalTerm.Clear();

            this.SelectionStart = objTerm.StartIndex;
            this.SelectionLength = objTerm.Value.Length;
            this.m_mthDel();

            this.m_lstMedicalTerm.AddRange(objTermArr);
            this.m_mthAdjustTermPosition();
        }
        /// <summary>
        /// 删除元素
        /// </summary>
        /// <param name="p_objElement"></param>
        public void m_mthDelTerm(EntityMedicalTerm p_objElement)
        {
            if (this.m_lstMedicalTerm.Count == 0)
                return;

            if (p_objElement == null) return;

            // 旧模板数据问题，临时处理(存在多个同元素)
            for (int i = this.m_lstMedicalTerm.Count - 1; i >= 0; i--)
            {
                if (this.m_lstMedicalTerm[i].TID == p_objElement.TID && this.m_lstMedicalTerm[i].StartIndex == p_objElement.StartIndex && this.m_lstMedicalTerm[i].EndIndex == p_objElement.EndIndex)
                {
                    this.m_lstMedicalTerm.RemoveAt(i);
                    //break;
                }
            }

            //this.m_lstMedicalTerm.Remove(p_objElement);

            //clsMedicalTerm[] objTermArr = new clsMedicalTerm[this.m_lstMedicalTerm.Count];
            //this.m_lstMedicalTerm.CopyTo(objTermArr);
            //this.m_lstMedicalTerm.Clear();

            //this.m_lstMedicalTerm.AddRange(objTermArr);            
        }
        #endregion

        #region 删除图片
        /// <summary>
        /// 删除图片状态
        /// </summary>
        public bool m_blnDelImageStatus = false;
        /// <summary>
        /// 删除图片
        /// </summary>
        public void m_mthDelImage()
        {
            this.m_blnDelImageStatus = true;
            SendKeys.SendWait("{DEL}");
            this.m_blnDelImageStatus = false;
        }
        #endregion

        #region 编辑图片
        /// <summary>
        /// 编辑图片
        /// </summary>
        /// <param name="p_objImage"></param>
        private void m_mthEditImage()
        {
            Image img = Function.ConvertByteToImage(this.m_objCurrentSelectedImage.ImageArr);
            frmImageEdit frmEdit = new frmImageEdit(img);
            if (frmEdit.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                this.m_mthEditImage(this.m_objCurrentSelectedImage, frmEdit.CurrentEditImage);
            }

        }
        #endregion

        #region 选择图片
        /// <summary>
        /// 选择图片
        /// </summary>
        public void m_mthSelectImage()
        {
            OpenFileDialog ofDiag = new OpenFileDialog();
            ofDiag.Filter = "Bmp文件(*.Bmp)|*.Bmp|Png文件(*.Png)|*.Png|Jpg文件(*.Jpg)|*.Jpg";
            ofDiag.Title = "选择文件";
            ofDiag.InitialDirectory = "D:\\";
            if (ofDiag.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                this.m_mthInsertImage(ofDiag.FileName);
            }
        }
        #endregion

        #region (外部)获取图片信息
        /// <summary>
        /// (外部)获取图片信息
        /// </summary>
        /// <returns></returns>
        public List<EntityCaseRtbImage> GetRtbImage()
        {
            List<EntityCaseRtbImage> lstImage = new List<EntityCaseRtbImage>();

            //if (this.m_lstImageInfo.Count > 0)
            //{
            //    int intRootBegin = 0;
            //    string strXml = string.Empty;
            //    MemoryStream objXmlStream = null;
            //    XmlTextWriter objXmlWriter = null;
            //    clsDCUniversalCaseRtbImage objRtbImage = null;
            //    foreach (clsImageInfo objImg in this.m_lstImageInfo)
            //    {
            //        objRtbImage = new clsDCUniversalCaseRtbImage();
            //        objXmlStream = new MemoryStream();
            //        objXmlWriter = new XmlTextWriter(objXmlStream, System.Text.Encoding.Unicode);

            //        objXmlWriter.WriteStartDocument();
            //        objXmlWriter.WriteStartElement("Main");
            //        objXmlWriter.WriteStartElement("Image");
            //        objXmlWriter.WriteStartElement("C");
            //        objXmlWriter.WriteAttributeString("S", objImg.m_intStartIndex.ToString());
            //        objXmlWriter.WriteAttributeString("E", objImg.m_intEndIndex.ToString());
            //        objXmlWriter.WriteAttributeString("ID", objImg.m_strImageID);
            //        objXmlWriter.WriteAttributeString("NO", objImg.m_strImageNO);
            //        objXmlWriter.WriteAttributeString("R", objImg.m_strImageRtf);
            //        objXmlWriter.WriteAttributeString("I", objImg.m_strUserID);
            //        objXmlWriter.WriteAttributeString("N", objImg.m_strUserName);
            //        objXmlWriter.WriteAttributeString("D", objImg.m_dtmCreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            //        objXmlWriter.WriteEndElement();

            //        objXmlWriter.WriteEndElement();
            //        objXmlWriter.WriteEndElement();
            //        objXmlWriter.WriteEndDocument();

            //        objXmlWriter.Flush();

            //        strXml = System.Text.Encoding.Unicode.GetString(objXmlStream.ToArray(), 0, (int)objXmlStream.Length);
            //        intRootBegin = strXml.IndexOf("<Main");
            //        objRtbImage.strImageIdXml = strXml.Substring(intRootBegin, strXml.Length - intRootBegin);
            //        objRtbImage.bytImageData = objImg.m_bytImageArr;
            //        lstImage.Add(objRtbImage);
            //    }
            //}

            return lstImage;
        }
        #endregion

        #region (外部)设置图片信息
        /// <summary>
        /// (外部)设置图片信息
        /// </summary>
        /// <returns></returns>
        public void SetRtbImage(List<EntityCaseRtbImage> p_lstRtbImage)
        {
            //clsImageInfo objImageInfo = null;
            //XmlNodeList nodeList = null;

            //this.m_lstImageInfo = null;
            //this.m_lstImageInfo = new List<clsImageInfo>();
            //foreach (clsDCUniversalCaseRtbImage objImage in p_lstRtbImage)
            //{
            //    nodeList = clsFunction.s_objReadXML(objImage.strImageIdXml, "Image");
            //    if (nodeList != null)
            //    {
            //        foreach (XmlNode node in nodeList)
            //        {
            //            objImageInfo = new clsImageInfo();
            //            objImageInfo.m_intStartIndex = int.Parse(node.Attributes["S"].Value);
            //            objImageInfo.m_intEndIndex = int.Parse(node.Attributes["E"].Value);
            //            objImageInfo.m_strImageID = node.Attributes["ID"].Value;
            //            objImageInfo.m_strImageNO = node.Attributes["N"].Value;
            //            objImageInfo.m_strImageRtf = node.Attributes["R"].Value;
            //            objImageInfo.m_bytImageArr = objImage.bytImageData;
            //            objImageInfo.m_strUserID = node.Attributes["I"].Value;
            //            objImageInfo.m_strUserName = node.Attributes["N"].Value;
            //            objImageInfo.m_dtmCreateTime = DateTime.Parse(node.Attributes["D"].Value);

            //            this.m_lstImageInfo.Add(objImageInfo);
            //        }
            //    }
            //}
        }
        #endregion

        #region 修改元素

        private EntityMedicalTerm m_objEditElement = null;
        /// <summary>
        /// 修改元素标志
        /// </summary>
        private bool m_blnUpdateElementFlag = false;
        /// <summary>
        /// 修改元素
        /// </summary>
        private void m_mthUpdateElement()
        {
            if (this._blnReadOnly) return;
            if (ItemName == null) return;
            if (IsDealIdeaCol) return;
            if (intConfineType == 2) return;
            if (this.SelectionLength > 0) this.SelectionLength = 0;
            this.m_intCurrentCursorIndex = this.SelectionStart;
            if (this.m_blnCursorPositionToDST2(this.m_intCurrentCursorIndex)) return;
            this.m_blnUpdateElementFlag = true;
            try
            {
                //clsMedicalTerm objTerm = null;
                //if (this.m_blnCursorPositionToTerm())
                if (CheckEditElementPosition())
                {
                    this.ImeMode = ImeMode.Off;
                    if (this.m_objEditElement.TID == "PatInfo")
                    {
                        DialogBox.Msg("病人基本信息元素，不能编辑。", MessageBoxIcon.Information, uiHelper.frmCurr);
                        return;
                    }
                    else if (this.m_objEditElement.TID.StartsWith("Intellection"))
                    {
                        DialogBox.Msg("智能引用字段，不能编辑。", MessageBoxIcon.Information, uiHelper.frmCurr);
                        return;
                    }
                    frmLoadElement frm = new frmLoadElement(this.m_objEditElement.TID, this.m_objEditElement.Value, this.m_objEditElement.CaseCode);
                    //frm.Owner = this.ParentEmrControl;// this.FindForm();
                    //    if (frm.Owner != null)
                    //    {
                    //        Point pntLoc = frm.Owner.PointToScreen(frm.Owner.Location) + new Size(50, 50);
                    //        frm.Location = frm.Owner.PointToClient(pntLoc);
                    //    }

                    frm.TemplateFlag = this.TemplateFlag;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        this.m_mthDelTerm(this.m_objEditElement);
                        this.SelectionStart = this.m_objEditElement.StartIndex;
                        this.SelectionLength = this.m_objEditElement.Value.Length;
                        this.m_intPreviouslyLen = this.TextLength - this.m_objEditElement.Value.Length;
                        this.m_mthInsertMedicalTerm(frm.CaseCode, frm.ElementID, frm.ElementName, frm.DragRichItem, true);
                        this.m_mthAdjustTermPosition();

                        //SendKeys.SendWait("{DEL}");
                        //if (!this.m_blnCursorPositionToTerm())
                        //{
                        //    this.SelectionStart = objTerm.m_intStartIndex;
                        //    this.m_mthInsertMedicalTerm(frm.ElementID, frm.ElementName, frm.DragRichItem);
                        //}
                    }
                    frm = null;
                }
            }
            finally
            {
                this.m_blnUpdateElementFlag = false;
            }
        }
        #endregion

        #region RTF
        #region 获取RTF
        #region 获取全部RTF
        /// <summary>
        /// 获取全部RTF
        /// </summary>
        /// <returns></returns>
        public byte[] GetAllRtf()
        {
            this.m_mthResetPoint();
            return Function.ConvertObjectToByte(this.Rtf);
        }
        #endregion

        #region 获取打印RTF

        /// <summary>
        /// 计算字符
        /// </summary>
        /// <param name="p_lstTempData"></param>
        private void m_mthComputeChar(ref List<EntityTempData> p_lstTempData)
        {
            EntityTempData objTempData = null;
            p_lstTempData = new List<EntityTempData>();
            if (this.m_lstDoubleStrikeThrough.Count > 0)
            {
                this.m_lstDoubleStrikeThrough.Sort();
                foreach (EntityDstInfo objDST in this.m_lstDoubleStrikeThrough)
                {
                    if (objDST.StartIndex < 0) continue;
                    objTempData = new EntityTempData();
                    objTempData.StartIndex = objDST.StartIndex;
                    objTempData.EndIndex = objDST.EndIndex;
                    objTempData.Len = objDST.Value.Length;
                    p_lstTempData.Add(objTempData);
                }
            }

            if (this.m_lstMedicalTerm.Count > 0)
            {
                this.m_lstMedicalTerm.Sort();
                if (p_lstTempData.Count > 0)
                {
                    int intStart = 0;
                    int intEnd = 0;
                    bool blnStatus1 = false;
                    bool blnStatus2 = false;
                    List<EntityTempData> lstTmp = new List<EntityTempData>();
                    foreach (EntityMedicalTerm objTerm in this.m_lstMedicalTerm)
                    {
                        blnStatus1 = false;
                        blnStatus2 = false;
                        intStart = objTerm.StartIndex;
                        intEnd = objTerm.EndIndex;
                        foreach (EntityTempData objTemp in p_lstTempData)
                        {
                            if (objTemp.StartIndex <= intStart && intStart <= objTemp.EndIndex)
                                blnStatus1 = true;
                            if (objTemp.StartIndex <= intEnd && intEnd <= objTemp.EndIndex)
                                blnStatus2 = true;
                            if (blnStatus1 && blnStatus2)
                                break;
                        }
                        if (!blnStatus1)
                        {
                            objTempData = new EntityTempData();
                            objTempData.StartIndex = intStart;
                            objTempData.EndIndex = objTempData.StartIndex;
                            objTempData.Len = 1;
                            if (this.Text.Substring(objTempData.StartIndex, 1) == "[")
                            {
                                lstTmp.Add(objTempData);
                            }
                        }
                        if (!blnStatus2)
                        {
                            objTempData = new EntityTempData();
                            objTempData.StartIndex = intEnd;
                            objTempData.EndIndex = objTempData.StartIndex;
                            objTempData.Len = 1;
                            if (this.Text.Substring(objTempData.StartIndex, 1) == "]")
                            {
                                lstTmp.Add(objTempData);
                            }
                        }
                    }
                    p_lstTempData.AddRange(lstTmp);
                }
                else
                {
                    foreach (EntityMedicalTerm objTerm in this.m_lstMedicalTerm)
                    {
                        objTempData = new EntityTempData();
                        objTempData.StartIndex = objTerm.StartIndex;
                        objTempData.EndIndex = objTempData.StartIndex;
                        objTempData.Len = 1;
                        if (this.Text.Substring(objTempData.StartIndex, 1) == "[")
                        {
                            p_lstTempData.Add(objTempData);
                        }

                        objTempData = new EntityTempData();
                        objTempData.StartIndex = objTerm.EndIndex;
                        objTempData.EndIndex = objTempData.StartIndex;
                        objTempData.Len = 1;
                        if (this.Text.Substring(objTempData.StartIndex, 1) == "]")
                        {
                            p_lstTempData.Add(objTempData);
                        }
                    }
                }
            }
            p_lstTempData.Sort();
        }

        /// <summary>
        /// 获取打印RTF
        /// </summary>
        /// <returns></returns>
        public byte[] GetPrintRtf()
        {
            this.m_mthResetPoint();

            string strRtf = string.Empty;
            List<EntityTempData> lstTempData = new List<EntityTempData>();
            this.m_mthComputeChar(ref lstTempData);
            if (lstTempData.Count > 0)
            {
                this.m_objRtb = new ctlRichTextBox();
                this.m_objRtb.Text = this.Text;
                this.m_objRtb.Rtf = this.Rtf;
                for (int i = lstTempData.Count - 1; i >= 0; i--)
                {
                    this.m_objRtb.SelectionStart = lstTempData[i].StartIndex;
                    this.m_objRtb.SelectionLength = lstTempData[i].Len;
                    RichTextBoxPlus.InsertText(this.m_objRtb, "");
                }
                strRtf = this.m_objRtb.Rtf;
                this.m_objRtb = null;
            }
            else
            {
                strRtf = this.Rtf;
            }
            return Function.ConvertObjectToByte(strRtf);
        }
        #endregion
        #endregion

        #region 设置RTF
        /// <summary>
        /// 设置RTF
        /// </summary>
        /// <param name="p_bytValue"></param>
        private void m_mthSetRtf(byte[] p_bytRtfArr)
        {
            string strRtf = Function.GetRtf(p_bytRtfArr);

            if (this.m_blnLoadTemplate)
            {
                using (RichTextBox rtx = new RichTextBox())
                {
                    rtx.Rtf = strRtf;
                    //if (strRtf.IndexOf("pict") == -1) //引发行间距小
                    //{
                    //    rtx.Text = rtx.Text.Trim();
                    //}
                    rtx.Select(0, rtx.Text.Length);
                    //有模板需要颜色标示，如中山，暂取消屏蔽。
                    //rtx.SelectionColor = Color.Black;
                    Font fntSelect = this.Font;
                    if (rtx.SelectionFont != null)
                    {
                        fntSelect = rtx.SelectionFont;
                    }

                    rtx.SelectionFont = new Font(fntSelect.FontFamily, fntSelect.Size, FontStyle.Regular);

                    strRtf = rtx.Rtf;
                }
            }

            IsReplaceRtf = true;
            this.Rtf = strRtf;
            this.intTextLength = this.Text.Length;
            IsReplaceRtf = false;
        }
        #endregion

        #region 调整首字符
        /// <summary>
        /// 调整首字符
        /// </summary>
        /// <param name="p_strCaption"></param>
        /// <param name="p_intLen"></param>
        private void m_mthAdjustFirstCaption(string p_strCaption, int p_intLen)
        {
            using (RichTextBox rtx = new RichTextBox())
            {
                rtx.Rtf = this.Rtf;
                rtx.Select(0, p_intLen);
                RichTextBoxPlus.InsertText(rtx, p_strCaption);
                IsReplaceRtf = true;
                this.Rtf = rtx.Rtf;
                this.intTextLength = this.Text.Length;
                IsReplaceRtf = false;
            }
        }
        #endregion
        #endregion

        #region IRuntimeDesignControl 成员

        bool referencetype = true;
        [Category("IRuntimeDesignControl属性")]
        [Description("是否资料调用")]
        public bool Referencetype
        {
            get
            {
                return referencetype;
            }
            set
            {
                referencetype = value;
            }
        }

        bool essential = false;
        [Category("IRuntimeDesignControl属性")]
        [Description("是否必填")]
        public bool Essential
        {
            get
            {
                return essential;
            }
            set
            {
                essential = value;
            }
        }

        int masktype = 0;
        [Category("IRuntimeDesignControl属性")]
        [Description("掩码类型")]
        public int MaskType
        {
            get { return masktype; }
            set { masktype = value; }
        }

        public bool RunTimeReadOnly
        {
            get;
            set;
            //get
            //{
            //    return this.DBColReadOnly;
            //}
            //set
            //{
            //    this.DBColReadOnly = value;
            //}
        }

        [Browsable(false)]
        [Category("IRuntimeDesignControl属性")]
        [Description("编辑值")]
        public object EditObject
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }

        [Browsable(false)]
        public Font TextFont
        {
            get
            {
                return new System.Drawing.Font("宋体", 9F);
                //return null;
            }
            set
            {
                this.Font = new System.Drawing.Font("宋体", 9F);
                //this.Font = value;
            }
        }

        [Browsable(false)]
        public decimal ZIndex { get; set; }

        private bool visible4design = true;

        [Browsable(false)]
        public bool Visible4Design
        {
            get
            {
                return visible4design;
            }
            set
            {
                visible4design = value;
            }
        }

        public bool EnableContextMenuStrip { get; set; }

        private int _presentationMode = 1;
        Color prevBackColor;
        BorderStyle prevBorderStyle;

        /// <summary>
        /// 展现方式
        /// </summary>
        //[TypeConverter(typeof(PresentationModeConverter))]
        public int PresentationMode
        {
            get
            {
                return _presentationMode;
            }
            set
            {
                if (_presentationMode != value)
                {
                    _presentationMode = value;

                    if (value == 0)
                    {
                        this.BorderStyle = this.prevBorderStyle;
                        this.BackColor = this.prevBackColor;
                    }
                    else if (value == 1 || value == 2 || value == 3)
                    {
                        this.prevBorderStyle = this.BorderStyle;
                        this.prevBackColor = this.BackColor;

                        this.BorderStyle = BorderStyle.None;
                        this.BackColor = Color.White;

                        //Pen p = new Pen(Brushes.Black);
                        //p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        //this.CreateGraphics().DrawLine(p, new Point(5, this.Height - 2), new Point(this.Width - 2, this.Height - 2));
                    }
                }
            }
        }

        [Browsable(false)]
        public bool ShowUnderLine { get; set; }
        #endregion

        #region IDBColProperty 成员

        [Browsable(false)]
        public string DBValue { get; set; }
        //{
        //    get
        //    {
        //        //return null;
        //        return this.Text;
        //    }
        //    set
        //    {
        //        this.Text = value;
        //        //throw new NotImplementedException();
        //    }
        //}

        #endregion

        #region 首行
        /// <summary>
        /// 获取首行标题
        /// </summary>
        /// <returns></returns>
        public string m_strGetFirstlineCaption()
        {
            string strSpace = string.Empty;
            //for (int i = 0; i < this.RowShrinkdigit; i++)
            //{
            //    strSpace += " ";
            //}
            return strSpace + this.FirstlineCaption;
        }
        /// <summary>
        /// 设置首行标题
        /// </summary>
        public void SetFirstlineCaption()
        {
            if (string.IsNullOrEmpty(this.FirstlineCaption))
            {
                this.m_mthAddSpace(false);
            }
            else
            {
                //this.m_mthInsertText(this.m_strGetFirstlineCaption());
                //if (string.IsNullOrEmpty(this.FirstlineCaption)) return;
                this.Text = this.m_strGetFirstlineCaption();
                this.m_mthSetCaptionBlod();
                this.SelectionStart = this.Text.Length;
                this.SelectionLength = 0;
                this.m_intCurrentCursorIndex = this.SelectionStart;
            }
            this.m_mthNewLine(this.DefaultRows);
            if (!this.Multiline)
            {
                this.Location = new Point(this.Location.X, this.Location.Y + this.Height - 22);
                this.Height = 22;
            }

            this.SetTextLength();
        }

        /// <summary>
        /// 初始化粗体
        /// </summary>
        private bool m_blnInitFontStyle = false;

        private void m_mthSetCaptionBlod()
        {
            if (!string.IsNullOrEmpty(this.FirstlineCaption))
            {
                try
                {
                    this.m_blnInitFontStyle = true;

                    if (!this.m_blnTableFlag)
                    {
                        // 临时处理
                        if (this.Text.Length >= 5 && GlobalCase.caseInfo != null)
                        {
                            if (this.Text.Substring(0, 5) == "月经生育史")
                            {
                                this.Select(0, 5);
                                this.m_mthInsertText("月经史");
                            }
                        }

                        this.Select(0, this.FirstlineCaption.Length);
                        using (RichTextBox rtx = new RichTextBox())
                        {
                            rtx.Rtf = this.SelectedRtf;
                            Font fntCurrent = this.Font;// .SelectionFont;
                            rtx.Select(0, rtx.Text.Length);
                            // 强行置为普通文本
                            //rtx.SelectionFont = new Font(fntCurrent.FontFamily, fntCurrent.Size, FontStyle.Regular);
                            // 强行置为粗体字体
                            rtx.SelectionFont = new Font(fntCurrent.FontFamily, fntCurrent.Size, FontStyle.Bold);
                            this.SelectedRtf = rtx.SelectedRtf;
                            this.SelectionLength = 0;
                        }
                    }
                }
                finally
                {
                    this.m_blnInitFontStyle = false;
                }
            }
        }
        /// <summary>
        /// 行缩进位数(调整用)
        /// </summary>
        private int m_intAddSpaceLen = 0;
        /// <summary>
        /// 行缩进
        /// </summary>
        private void m_mthAddSpace(bool p_blnInit)
        {
            if (string.IsNullOrEmpty(ItemName))    //(DBColName))
                return;
            if (p_blnInit && !string.IsNullOrEmpty(this.FirstlineCaption))
                return;
            if (!this.Multiline || this.m_blnTableFlag)
                return;
            if (this.RowShrinkdigit == 0)
                return;
            string strSpace = string.Empty;
            for (int i = 0; i < this.RowShrinkdigit; i++)
            {
                strSpace += " ";
            }
            if (strSpace.Length > 0)
            {
                this.m_mthInsertText(strSpace);
                if (p_blnInit)
                {
                    this.SelectionStart = strSpace.Length;
                }
                else
                {
                    this.SelectionStart -= strSpace.Length;
                    this.m_intAddSpaceLen = strSpace.Length;
                }
                this.Focus();
            }
        }
        /// <summary>
        /// 检查首行标题位置
        /// </summary>
        /// <returns></returns>
        private bool m_blnCursorPositionToCaption()
        {
            if (!string.IsNullOrEmpty(this.FirstlineCaption))
            {
                int intStart = 0;
                int intEnd = this.FirstlineCaption.Length;
                if (intEnd <= intStart) return false;

                int intPos = this.m_intCurrentCursorIndex;
                if (this.m_blnMuliSelectedFlag)
                {
                    intPos = this.SelectionStart;
                }
                if (intStart <= intPos && intPos < intEnd)
                {
                    ImeMode = ImeMode.Off;
                    //ReadOnly = true;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查首行标题位置
        /// </summary>
        /// <returns></returns>
        private bool m_blnCursorPositionToCaption(int p_intIdx)
        {
            if (!string.IsNullOrEmpty(this.FirstlineCaption))
            {
                int intStart = 0;
                int intEnd = this.FirstlineCaption.Length;
                if (intEnd <= intStart) return false;

                if (intStart <= p_intIdx && p_intIdx < intEnd)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 标题光标控制
        /// <summary>
        /// 标题光标控制
        /// </summary>
        private void m_mthSetCaptionCursor()
        {
            if (this.m_blnCursorPositionToCaption())
            {
                this.SelectionStart = this.FirstlineCaption.Length;
                this.SelectionLength = 0;
                this.m_intCurrentCursorIndex = this.SelectionStart;
            }
        }

        /// <summary>
        /// 检查标题有效光标
        /// </summary>
        /// <returns></returns>
        private bool CheckValidCaptionCursor()
        {
            if (!string.IsNullOrEmpty(this.FirstlineCaption) && this.SelectionStart < this.FirstlineCaption.Length)
                return false;
            return true;
        }

        #endregion

        #region 检查光标位置是否有效
        /// <summary>
        /// true 位置有效 false 位置无效
        /// </summary>
        /// <returns></returns>
        public bool m_blnCheckCursorValidPostion()
        {
            bool blnElement = (!IsAllowElementFreeEdit && this.m_blnCursorPositionToTerm()) ? true : false;
            if (blnElement || CheckOpCallDealIdeaElement() || this.m_blnCursorPositionToDST() || this.m_blnCursorPositionToImage() || this.m_blnCursorPositionToCaption())
                return false;
            return true;
        }
        /// <summary>
        /// true 位置有效 false 位置无效
        /// </summary>
        /// <returns></returns>
        public bool m_blnCheckCursorValidPostion2()
        {
            bool blnElement = (!IsAllowElementFreeEdit && this.m_blnCursorPositionToTerm()) ? true : false;
            if (blnElement || CheckOpCallDealIdeaElement() || this.m_blnCursorPositionToDST() || this.m_blnCursorPositionToCaption())
                return false;
            return true;
        }
        #endregion

        #region 添加字段模板内容

        /// <summary>
        /// 插入外部元素模板
        /// </summary>
        /// <param name="p_strItemName"></param>
        /// <param name="p_lstMedTerm"></param>
        public void InsertExtItemTemple(string p_strItemName, List<EntityMedicalTerm> p_lstMedTerm)
        {
            if (p_lstMedTerm != null && p_lstMedTerm.Count > 0)
            {
                p_lstMedTerm.Sort();
                int intStartIndex = SelectionStart;
                foreach (EntityMedicalTerm obj in p_lstMedTerm)
                {
                    obj.StartIndex += intStartIndex;
                    obj.EndIndex = obj.StartIndex + obj.Value.Length - 1;
                }
            }

            this.m_mthAddItemTemple(p_strItemName, p_lstMedTerm);
        }

        /// <summary>
        /// 添加字段模板内容
        /// </summary>
        /// <param name="p_strItemName"></param>
        /// <param name="p_lstMedTerm"></param>
        private void m_mthAddItemTemple(string p_strItemName, List<EntityMedicalTerm> p_lstMedTerm)
        {
            if (!this.m_blnCheckCursorValidPostion()) return;
            this.stopReDraw();
            this.m_blnLoadTermStatus = true;
            this.SelectionStart = this.m_intCurrentCursorIndex;
            this.SelectionLength = 0;
            this.m_mthInsertText(p_strItemName);
            if (p_lstMedTerm != null && p_lstMedTerm.Count > 0)
            {
                this.m_lstMedicalTerm.AddRange(p_lstMedTerm);
                this.m_mthSetMedicalTermColor(0);
                this.m_mthAdjustTermPosition();
            }
            this.m_mthFireEvtTextChange(false);
            this.m_blnLoadTermStatus = false;
            this.SelectionLength = 0;
            this.reDraw();
        }

        #endregion

        #region 接收从右侧资料引用面板拖拉的文本和元素
        /// <summary>
        /// 接收从右侧资料引用面板拖拉的文本和元素
        /// </summary>
        /// <param name="drgevent"></param>
        protected override void OnDragDrop(DragEventArgs e)
        {
            this.m_intCurrentCursorIndex = this.SelectionStart;
            if (!this.m_blnCheckCursorValidPostion()) return;
            Type objTypeRichItem = Type.GetType("weCare.Core.Entity.EntityDragRichItem");
            Type objTypeIntellectiveRefItem = Type.GetType("weCare.Core.Entity.EntityIntellectiveRefItem");
            Type objTypePatInfo = typeof(EnumPatientInfoType);
            if (e.Data.GetDataPresent(objTypeRichItem))
            {
                //从右侧资料引用面板拖入数据 
                EntityDragRichItem dragRichItem = e.Data.GetData(objTypeRichItem) as EntityDragRichItem;
                string strContent = uiHelper.CaseColumnContent(dragRichItem.CaseCode, dragRichItem.ColCode, dragRichItem.DragString);
                this.m_mthAddItemTemple(strContent, dragRichItem.DragMedicalTerm);
            }
            else if (e.Data.GetDataPresent(objTypeIntellectiveRefItem))
            {
                Form frm = this.FindForm();
                if (frm.GetType().ToString() == c_strFormClass)
                {
                    this.m_mthAddElement((EntityIntellectiveRefItem)e.Data.GetData(objTypeIntellectiveRefItem));
                }
            }
            else if (e.Data.GetDataPresent(objTypePatInfo))
            {
                Form frm = this.FindForm();
                if (frm.GetType().ToString() == c_strFormClass)
                {
                    this.m_mthAddElement(string.Empty, "PatInfo", Convert.ToString((EnumPatientInfoType)e.Data.GetData(objTypePatInfo)), null, false);
                }
                else
                {
                    string strInfo = PatientInfoHelper.GetTypePatientInfo((EnumPatientInfoType)e.Data.GetData(objTypePatInfo));
                    if (string.IsNullOrEmpty(strInfo))
                    {
                        DialogBox.Msg("没有资料", MessageBoxIcon.Information, uiHelper.frmCurr);
                    }
                    else
                    {
                        this.m_mthInsertText(strInfo.Trim());
                    }
                }
            }
            else if (e.Data.GetDataPresent(typeof(System.String)))
            {
                this.m_mthInsertText(Convert.ToString(e.Data.GetData(typeof(System.String))).Trim());
            }
            else
            {
                //原有默认方法
                base.OnDragDrop(e);
            }
        }
        #endregion

        #region 新行
        /// <summary>
        /// 新行
        /// </summary>
        public void m_mthNewLine()
        {
            this.m_mthNewLine(1);
        }
        /// <summary>
        /// 新行
        /// </summary>
        /// <param name="p_intRowCount"></param>
        public void m_mthNewLine(int p_intRowCount)
        {
            RichTextBoxPlus.InsertNewLine(this, p_intRowCount, this.m_strGetFirstlineCaption());
        }
        #endregion

        #region 表格.插入行
        /// <summary>
        /// 表格.插入行
        /// </summary>
        internal void m_mthInsertRow()
        {
            if (ParentTable != null)
            {
                ParentTable.m_mthInsertRow();
            }
        }
        #endregion

        #region 表格.删除行
        /// <summary>
        /// 表格.删除行
        /// </summary>
        internal void m_mthDeleteRow()
        {
            if (ParentTable != null)
            {
                ParentTable.m_mthDelRow();
            }
        }
        #endregion

        #region 表格.删除单元格
        /// <summary>
        /// 表格.删除单元格
        /// </summary>
        internal void m_mthDeleteCell()
        {
            if (ParentTable != null)
            {
                bool blnStatus = false;
                if (GlobalParm.dicSysParameter.ContainsKey(25) && !string.IsNullOrEmpty(GlobalParm.dicSysParameter[25]))
                {
                    string strRoleID = GlobalParm.dicSysParameter[25];
                    if (GlobalLogin.objLogin.lstRoleID.IndexOf(strRoleID) >= 0)
                    {
                        blnStatus = true;
                    }
                }
                if (blnStatus)
                    this.ClearText();
                else
                    DialogBox.Msg("对不起，无权限删除单元格。", MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        public event HandleReachMaxLength evtReachMaxLength;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }

    #region 事件参数类
    /// <summary>
    /// 病例书写控件事件
    /// </summary>
    public class clsEvtRichTextBox : EventArgs
    {
        /// <summary>
        /// 病例书写控件
        /// </summary>
        public ctlRichTextBox m_objRichTextBox { get; set; }
    }

    /// <summary>
    /// 选择图片事件
    /// </summary>
    public class clsEvtSelectedImage : EventArgs
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool blnSelected { get; set; }
    }
    #endregion

    #region Reach.MAXLENGTH
    public delegate void HandleReachMaxLength(object sender);
    #endregion
}


