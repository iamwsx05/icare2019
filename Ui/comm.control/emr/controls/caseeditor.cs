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
using iCare.Core.Entity;
using iCare.Core.Utils;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 病历书写控件
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.RichTextBox))]
    public partial class ctlRichTextBox : System.Windows.Forms.RichTextBox, IDBColProperty, IRtfEditor, IRuntimeDesignControl
    {

        #region 构造函数
        /// <summary>
        /// 初始化中
        /// </summary>
        public bool m_blnIniting = false;
        /// <summary>
        /// 双划线(事件)
        /// </summary>
        public bool m_blnDoubleLine = false;
        /// <summary>
        /// 表格标志
        /// </summary>
        private bool _blnTableFlag = false;
        private AutocompleteMenu autocompleteAssistant = new com.HopeBridge.ehr.control.AutocompleteMenu();
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

        public bool blnDiagFlgas
        {
            get
            {
                return (MaskType == 3 || MaskType == 4 || MaskType == 5 || MaskType == 6);
            }
        }

        /// <summary>
        /// 允许剪切的标志
        /// </summary>
        public bool blnCutFlags = false;

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
        /// ElementFreeEdit
        /// </summary>
        /// <returns></returns>
        private bool ElementFreeEdit()
        {
            if (FormTypeName == clsRichTextBoxTool.TemplateFormClass)
            {
                return false;
            }
            else
            {
                int val = 0;
                int.TryParse(clsGlobalSysParameter.dicSysParameter[65], out val);

                if (val == 1)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// 是否允许元素自由编辑
        /// </summary>
        private bool IsAllowElementFreeEdit
        {
            get;
            set;
            //get
            //{
            //    if (FormTypeName == clsRichTextBoxTool.TemplateFormClass)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        int val = 0;
            //        int.TryParse(clsGlobalSysParameter.dicSysParameter[65], out val);

            //        if (val == 1)
            //            return false;
            //        else
            //            return true;
            //    }
            //}
        }

        /// <summary>
        /// 修改表单的时限
        /// </summary>
        public int? intModifyLimit { get; set; }

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

            this.m_lstDoubleStrikeThrough = new List<clsDSTInfo>();
            this.m_lstModifyUsers = new List<clsModifyUserInfo>();
            this.m_lstTextContentInfos = new List<clsUserContentInfo>();
            this.m_lstMedicalTerm = new List<clsMedicalTerm>();
            this.m_lstImageInfo = new List<clsImageInfo>();
            this.m_lstICDInfo = new List<clsICDInfo>();

            this.m_objCurrentModifyUser = new clsModifyUserInfo();
            this.m_objCurrentModifyUser.m_clrText = this.m_clrOldPartInsertText;
            this.m_objCurrentModifyUser.m_intUserSequence = -1;

            this.m_clrOldPartInsertText = this.m_clrDefaultViewText;
            this.m_pntEndVisible = new Point(this.Width - 5, this.Height - 5);
            this.m_intFontHeight = this.Font.Height;

            this.m_sbdTemp = new StringBuilder();
            if (this is ctlAllergy) { }
            else
            {
                this.ImeMode = ImeMode.On;
            }
            this.AllowDrop = true;

            using (Graphics _graphics = this.CreateGraphics())
            {
                // = _graphics.DpiX;
                // = _graphics.DpiY;
            }

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

            //if (FirstlineCaption == null)//门诊在此调用，因为门诊FirstlineCaption == null。
            //{
            //    this.m_mthSetRowSpacing();
            //}
        }

        #endregion

        #region 变量
        public new bool DesignMode
        {
            get { return System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower() == "devenv"; }
        }

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
        public clsImageInfo m_objCurrentSelectedImage { get; set; }

        /// <summary>
        /// 在文本内容替换时使用的工具
        /// </summary>
        private static System.Windows.Forms.RichTextBox s_rtbRTFReplace = new System.Windows.Forms.RichTextBox();

        /// <summary>
        /// 标记医学术语区间
        /// </summary>
        private List<clsMedicalTerm> m_lstMedicalTerm = null;

        public List<clsMedicalTerm> LstMedicalTerm
        {
            get { return m_lstMedicalTerm; }
        }

        /// <summary>
        /// 标记有双删除线的区间
        /// </summary>
        internal List<clsDSTInfo> m_lstDoubleStrikeThrough = null;
        private List<clsView> m_lstDDLView = new List<clsView>();
        private List<clsICDInfo> m_lstICDInfo = null;

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
        //字节长度
        internal int intTextLength { get; set; }
        private int intSelectedTextLength { get; set; }
        public int intMaxLength { get; set; }//PreProcessMessage方法中不可以对控件的属性访问，所以增加一个变量来记录

        //改写之是为了赋值intMaxLength
        public override int MaxLength
        {
            get
            {
                return base.MaxLength;
            }
            set
            {
                base.MaxLength = value;
                intMaxLength = value;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!DesignMode)
            {
                if (MaskType == 3)
                {
                    InitializeComponent();

                    System.Data.DataTable dtSource = CacheContext.Get("clsEntityIcd");

                    foreach (System.Data.DataRow drCurrent in dtSource.Rows)
                    {
                        autocompleteAssistant.AddItem(new DictAutocompleteItem(drCurrent["icdcode_vchr"].ToString(),
                            drCurrent["icdcnname_vchr"].ToString(),
                            this.IsAllowSignNull == 0 ? drCurrent["icdcnname_vchr"].ToString() : drCurrent["icdcode_vchr"].ToString(),
                            false)
                            {
                                KeyWidth = 100,
                                ValueWidth = 320,
                                FilterColumns = new string[] { drCurrent["icdpycode_vchr"].ToString(),
                            drCurrent["icdwbcode_vchr"].ToString() }
                            });
                    }
                }
                else if (MaskType == 4)
                {
                    InitializeComponent();

                    System.Data.DataTable dtSource = CacheContext.Get("clsEntityClinicalIcd", "status_int = 1 and type_int = 1");

                    foreach (System.Data.DataRow drCurrent in dtSource.Rows)
                    {
                        autocompleteAssistant.AddItem(new DictAutocompleteItem(drCurrent["icdcode_vchr"].ToString(),
                            drCurrent["clinicalname_vchr"].ToString(),
                           this.IsAllowSignNull == 0 ? drCurrent["clinicalname_vchr"].ToString() : drCurrent["icdcode_vchr"].ToString(),
                            true)
                            {
                                KeyWidth = 100,
                                ValueWidth = 320,
                                FilterColumns = new string[] { drCurrent["icdname_vchr"].ToString(), drCurrent["pycode_vchr"].ToString(), drCurrent["wbcode_vchr"].ToString() }
                            });
                    }
                }
                else if (MaskType == 5)
                {
                    InitializeComponent();

                    if (clsGlobalHospitalCode.Code == "0001")
                    {
                        System.Data.DataTable dtSource = CacheContext.Get("clsEntityOperation", "status_int = 1");

                        foreach (System.Data.DataRow drCurrent in dtSource.Rows)
                        {
                            autocompleteAssistant.AddItem(new DictAutocompleteItem(drCurrent["opcode_vchr"].ToString(),
                                drCurrent["opname_vchr"].ToString(),
                                this.IsAllowSignNull == 0 ? drCurrent["opname_vchr"].ToString() : drCurrent["opcode_vchr"].ToString(),
                                false)
                            {
                                KeyWidth = 100,
                                ValueWidth = 320,
                                FilterColumns = new string[] { drCurrent["pycode_vchr"].ToString(),
                            drCurrent["wbcode_vchr"].ToString() }
                            });
                        }
                    }
                    else
                    {
                        System.Data.DataTable dtSource = CacheContext.Get("clsEntityOperationICD");

                        foreach (System.Data.DataRow drCurrent in dtSource.Rows)
                        {
                            autocompleteAssistant.AddItem(new DictAutocompleteItem(drCurrent["icdcode_vchr"].ToString(),
                                drCurrent["icdname_vchr"].ToString(),
                                this.IsAllowSignNull == 0 ? drCurrent["icdname_vchr"].ToString() : drCurrent["icdcode_vchr"].ToString(),
                                true)
                            {
                                KeyWidth = 100,
                                ValueWidth = 320,
                                FilterColumns = new string[] { drCurrent["icdpycode_vchr"].ToString(),
                            drCurrent["icdwbcode_vchr"].ToString() }
                            });
                        }
                    }
                }
                else if (MaskType == 6)
                {
                    InitializeComponent();

                    System.Data.DataTable dtSource = CacheContext.Get("clsEntityClinicalIcd", "status_int = 1 and type_int = 3");

                    foreach (System.Data.DataRow drCurrent in dtSource.Rows)
                    {
                        autocompleteAssistant.AddItem(new DictAutocompleteItem(drCurrent["icdcode_vchr"].ToString(),
                            drCurrent["clinicalname_vchr"].ToString(),
                           this.IsAllowSignNull == 0 ? drCurrent["clinicalname_vchr"].ToString() : drCurrent["icdcode_vchr"].ToString(),
                            true)
                            {
                                KeyWidth = 100,
                                ValueWidth = 320,
                                FilterColumns = new string[] { drCurrent["icdname_vchr"].ToString(), drCurrent["pycode_vchr"].ToString(), drCurrent["wbcode_vchr"].ToString() }
                            });
                    }
                }
            }
        }

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
        private clsModifyUserInfo m_objCurrentModifyUser = null;

        /// <summary>
        /// 所有修改用户的信息
        /// </summary>
        private List<clsModifyUserInfo> m_lstModifyUsers = null;

        /// <summary>
        /// 存放clsUserContentInfo对象
        /// </summary>
        private List<clsUserContentInfo> m_lstTextContentInfos = null;
        private List<clsView> m_lstTextView = new List<clsView>();
        private List<clsView> m_lstElementView = new List<clsView>();

        /// <summary>
        /// 多选
        /// </summary>
        private bool m_blnMuliSelectedFlag = false;
        /// <summary>
        /// 图片
        /// </summary>
        private List<clsImageInfo> m_lstImageInfo = null;

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
        #endregion

        #region 属性

        [Category("IRuntimeDesignControl属性")]
        [Description("必填组号")]
        public string EssentialGroupNo { get; set; }

        /// <summary>
        /// 首行标题字符长度
        /// </summary>
        public int FirstlineCaptionLen
        {
            get
            {
                return this.FirstlineCaption == null ? 0 : this.FirstlineCaption.Length;
            }
        }

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
                if (clsGlobalPatient.objCurrentPatient == null || clsGlobalPatient.objCurrentPatient.intRegisterID <= 0) return false;
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
        private int m_intRowSpacing = 20;// 14;
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
        private void m_mthSetRowSpacing()
        {
            if (this.IsHandleCreated && this.UseRowSpacing && !this.m_blnIniting && !this.m_blnTableFlag)
            {
                if (_intRowSpacing > 0)
                {
                    try
                    {
                        clsRichTextBoxTool.SetRowSpacing(_intRowSpacing, this);
                    }
                    catch (System.TypeInitializationException e)
                    {
                        clsFunction.s_blnOuputLog(e.Message + "|" + e.StackTrace);
                    }
                }
            }
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

        /// <summary>
        /// 数据库字段名
        /// </summary>
        [Category("字段属性")]
        [Description("数据库字段名")]
        public string DBColName
        {
            get;
            set;
        }
        /// <summary>
        /// 数据库字段描述
        /// </summary>
        [Category("字段属性")]
        [Description("数据库字段描述")]
        public string DBColDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 数据库字段类型
        /// </summary>
        [Category("字段属性")]
        [Description("数据库字段类型")]
        public string DBColType
        {
            get;
            set;
        }

        [Category("字段属性")]
        [Description("数据库字段精度")]
        public string DBColPrecision
        {
            get;
            set;
        }

        [Category("字段属性")]
        [Description("运行时只读")]
        public bool DBColReadOnly
        {
            get;
            set;
        }
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
        public int IsAutoSignature { get; set; }
        public int IsAllowSignNull { get; set; }

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
            if (this.FindForm() is frmBaseMdiCase)
            {
                if (((frmBaseMdiCase)this.FindForm()).lstDBCol != null)
                {
                    try
                    {
                        foreach (var item in ((frmBaseMdiCase)this.FindForm()).lstDBCol)
                        {
                            if (item is ctlICD && ((ctlICD)item).frmTemp != null && ((ctlICD)item).frmTemp.blnExtCall)
                            {
                                ((ctlICD)item).CloseListItem();
                            }
                        }
                    }
                    catch { };
                }
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

            if (clsHelper.OpCaseCall)
            {
                try
                {
                    if (DesignMode) return;
                    if (this.FindForm().MdiParent == null) return;
                    if (this.FindForm() is frmBaseMdiCase)
                    {
                        ((frmBaseMdiCase)this.FindForm()).SetItemTemplateCaption(this.DBColDesc);

                        CloseIcdList();
                    }

                    Form frmMain = this.FindForm().MdiParent;
                    string strMethod = "m_mthShowFieldTemplate";

                    MethodInfo objMth = frmMain.GetType().GetMethod(strMethod);
                    object[] param = new object[1];
                    param[0] = this.DBColName;
                    objMth.Invoke(frmMain, param);
                }
                catch { }
            }

            //this.m_intCurrentCursorIndex = 0;
            this.m_mthSetCaptionCursor();
            this.m_blnInitEnter = true;

            base.OnEnter(e);
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

                clsDialog.Msg(strInfo, MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
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
            if (m.Msg == 0x010F && !m_blnCanModifySelection)//中文输入WM_IME_COMPOSITION = 0x010F
            {
                return;
            }

            base.WndProc(ref m);
            if (this.m_blnIniting) return;
            if (m.Msg == 0x0204)
            {
                if (this.m_blnCursorPositionToCaption())
                    clsDialog.Msg("字段标题无右键", MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
            }
            else if (m.Msg == 0x000F)//Repaint事件
            {
                Graphics objGrp = this.CreateGraphics();
                if (!m_blnIMEInput)
                {
                    clsRichTextBoxTool.DrawVirtualLine(objGrp, _presentationMode, this.FirstlineCaption, this);
                    clsRichTextBoxTool.DrawDST(objGrp, this.m_pntEndVisible, this.m_intPreviouslyLen, this.UseRowSpacing, this.m_blnTableFlag, this,
                                               this.m_lstTextContentInfos, this.m_lstDoubleStrikeThrough, this.m_lstMedicalTerm,
                                               ref this.m_lstTextView, ref this.m_lstDDLView, ref this.m_lstElementView);
                    clsRichTextBoxTool.DrawPoint(objGrp, this.m_lstMedicalTerm, this);
                }
                objGrp.Dispose();
            }
        }
        #endregion

        #region 校验元素值
        /// <summary>
        /// 校验元素值
        /// </summary>
        private void m_mthCheckElement()
        {
            clsMedicalTerm objElement = null;
            string strContent = this.Text;
            for (int i = this.m_lstMedicalTerm.Count - 1; i >= 0; i--)
            {
                objElement = this.m_lstMedicalTerm[i];
                if (objElement.m_intStartIndex > strContent.Length || objElement.m_intEndIndex > strContent.Length)
                {
                    this.m_lstMedicalTerm.Remove(objElement);
                    continue;
                }

                if (objElement.m_intStartIndex + objElement.m_strValue.Trim().Length <= strContent.Length)
                {
                    if (objElement.m_strValue.Trim() != strContent.Substring(objElement.m_intStartIndex, objElement.m_strValue.Trim().Length))
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
                intTextLength = System.Text.Encoding.Default.GetBytes(this.m_strGetRightText()).Length;
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

                clsRichTextBoxTool.HandleDelete(this, m_intSelectedTextLength, ref this.m_intCurrentCursorIndex, m_blnIsBackspace, ref this.m_lstTextContentInfos,
                                                ref this.m_lstMedicalTerm, ref this.m_lstDoubleStrikeThrough, this.FntElement);

            }
            else if (-1 * intDiffLen < m_intSelectedTextLength)
            {
                //替换
                clsRichTextBoxTool.AdjustContentPosition_Delete(m_intSelectedTextStartIndex, m_intSelectedTextLength, intDiffLen, ref this.m_lstTextContentInfos, this.m_blnIsBackspace, this);
                clsRichTextBoxTool.AdjustElementPosition_Delete(m_intSelectedTextStartIndex, m_intSelectedTextLength, intDiffLen, this.m_blnIsBackspace, this.FntElement, ref this.m_lstMedicalTerm, this);

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
                clsRichTextBoxTool.HandleInsert(this, ref this.m_lstMedicalTerm, ref this.m_lstTextContentInfos, ref m_lstDoubleStrikeThrough, intDiffLen, this.FntElement, ref this.m_blnCanSelectedChanged,
                                                ref this.IsInsertPatElement, ref this.m_intCurrentCursorIndex, this.IsDealIdeaCol, this.IsInsertNorElement, this.IsAllowElementFreeEdit,
                                                this.m_objCurrentModifyUser, this.m_clrOldPartInsertText, DateTime.Now);
            }
            else
            {
                //删除
                clsRichTextBoxTool.HandleDelete(this, -1 * intDiffLen, ref this.m_intCurrentCursorIndex, m_blnIsBackspace, ref this.m_lstTextContentInfos, ref this.m_lstMedicalTerm,
                                                ref this.m_lstDoubleStrikeThrough, this.FntElement);
            }
        }

        private bool IsDealIdeaCol
        {
            get
            {
                return (!string.IsNullOrEmpty(DBColName) && clsHelper.OpCaseCall && DBColName.ToLower() == "dealidea" ? true : false);
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
            foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if ((obj.m_intStartIndex < intStartIndex && intStartIndex <= obj.m_intEndIndex) ||
                    (obj.m_intStartIndex > intStartIndex && obj.m_intStartIndex < intEndIndex) ||
                    (obj.m_intEndIndex > intStartIndex && obj.m_intEndIndex <= intEndIndex) ||
                    (obj.m_intStartIndex <= intStartIndex && intEndIndex <= obj.m_intEndIndex) ||
                    (obj.m_intEndIndex >= intStartIndex && obj.m_intEndIndex < intEndIndex) ||
                    (obj.m_intStartIndex <= intStartIndex && obj.m_intEndIndex <= intEndIndex && obj.m_intEndIndex >= intStartIndex))
                {
                    if (obj.m_strTID.ToLower() == clsRichTextBoxTool.OpCall)
                        blnRet = true;
                    break;
                }
                else
                {
                    if (this.m_blnMuliSelectedFlag)
                    {
                        if (obj.m_intStartIndex == intStartIndex)
                        {
                            if (obj.m_strTID.ToLower() == clsRichTextBoxTool.OpCall)
                                blnRet = true;
                            break;
                        }
                    }
                }
            }
            if (blnRet) ImeMode = ImeMode.Off;
            return blnRet;
        }

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

                foreach (clsDSTInfo objDST in this.m_lstDoubleStrikeThrough)
                {
                    if (m_intCurrentCursorIndex > objDST.m_intStartIndex
                        && m_intCurrentCursorIndex <= objDST.m_intEndIndex)
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
        public void m_mthSetMedicalTerm(List<clsMedicalTerm> p_lstMedicalTerm)
        {
            if (p_lstMedicalTerm == null || p_lstMedicalTerm.Count == 0)
                return;

            try
            {
                clsRichTextBoxTool.StopRedraw(this.Handle);
                if (this.m_lstMedicalTerm == null)
                    this.m_lstMedicalTerm = new List<clsMedicalTerm>();
                this.m_lstMedicalTerm.AddRange(p_lstMedicalTerm);
                clsRichTextBoxTool.AdjustTermPosition(ref this.m_lstMedicalTerm, this.FntElement, this);
            }
            finally
            {
                clsRichTextBoxTool.Redraw(this.Handle);
            }
        }
        #endregion

        #region 设置系统登录人
        /// <summary>
        /// 设置系统登录人
        /// </summary>
        /// <param name="p_strLoginUserID">ID</param>
        /// <param name="p_strLoginUserName">名称</param>
        public void m_mthSetLoginUser(string p_strLoginUserID, string p_strLoginUserName)
        {
            if (this.m_objCurrentModifyUser == null)
                this.m_objCurrentModifyUser = new clsModifyUserInfo();
            this.m_objCurrentModifyUser.m_strUserID = p_strLoginUserID;
            this.m_objCurrentModifyUser.m_strUserName = p_strLoginUserName;
            this.m_objCurrentModifyUser.m_dtmModifyDate = clsHelper.s_dtmMidderTime();
        }
        #endregion

        #region 设置XML文本
        /// <summary>
        /// 通过XML获取元素列表
        /// </summary>
        /// <param name="p_strXML"></param>
        /// <returns></returns>
        public List<clsMedicalTerm> m_objGetElementListByXML(string p_strXML)
        {
            return clsRichTextBoxTool.GetElementListByXML(p_strXML);
        }

        /// <summary>
        /// 调模板
        /// </summary>
        private bool m_blnLoadTemplate = false;

        public void m_mthSetXmlText(byte[] p_bytRtfArr, string p_strXML, bool p_blnLoadCaseFlag, bool p_blnTemplate)
        {
            this.m_blnLoadTemplate = true;
            this.m_mthSetXmlText(p_bytRtfArr, p_strXML, p_blnLoadCaseFlag);
            this.m_blnLoadTemplate = false;
            this.Invalidate();
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
        public void m_mthSetXmlText(byte[] p_bytRtfArr, string p_strXML, bool p_blnLoadCaseFlag)
        {
            this.m_lstTextContentInfos = new List<clsUserContentInfo>();
            this.m_lstDoubleStrikeThrough = new List<clsDSTInfo>();
            this.m_lstMedicalTerm = new List<clsMedicalTerm>();
            this.m_lstImageInfo = new List<clsImageInfo>();
            this.m_lstICDInfo = new List<clsICDInfo>();
            this.m_blnInitSetXml = true;
            this.ValueChangedFlag = true;
            try
            {
                if (p_bytRtfArr == null) return;
                if (clsGlobalCase.objCaseInfo != null && clsGlobalCase.objCaseInfo.intCaseStatus == 0)
                {
                    if (clsGlobalPatient.objCurrentPatient.strSex == "1" && this.m_strGetFirstlineCaption().Contains("月经"))
                    {
                        this.m_mthClearText();
                        return;
                    }
                }
                clsRichTextBoxTool.StopRedraw(this.Handle);
                this.m_mthSetRtf(p_bytRtfArr);
                this.m_clrOldPartInsertText = this.m_clrDefaultViewText;
                this.m_intPreviouslyLen = this.TextLength;

                XmlNodeList nodeList = null;
                if (p_blnLoadCaseFlag)
                {
                    this.ValueChangedFlag = false;
                    nodeList = clsFunction.s_objReadXML(p_strXML, "Content");
                    if (nodeList != null)
                    {
                        clsUserContentInfo objContent = null;
                        List<clsUserContentInfo> lstCheckContent = new List<clsUserContentInfo>();
                        foreach (XmlNode node in nodeList)
                        {
                            objContent = new clsUserContentInfo();
                            try
                            {
                                objContent.m_intStartIndex = int.Parse(node.Attributes["S"].Value);
                                objContent.m_intEndIndex = int.Parse(node.Attributes["E"].Value);
                                objContent.m_strUserID = node.Attributes["I"].Value;
                                objContent.m_strUserName = node.Attributes["N"].Value;
                                //objContent.m_intUserSequence = int.Parse(node.Attributes["Q"].Value);
                                objContent.m_clrText = Color.FromArgb(int.Parse(node.Attributes["R"].Value));
                                objContent.m_dtmModifyDate = DateTime.Parse(node.Attributes["D"].Value);
                            }
                            catch
                            {
                                objContent.m_intStartIndex = 0;
                                objContent.m_intEndIndex = this.Text.Length - 1;
                                objContent.m_strUserID = this.m_objCurrentModifyUser.m_strUserID;
                                objContent.m_strUserName = this.m_objCurrentModifyUser.m_strUserName;
                                //objContent.m_intUserSequence = 1;
                                objContent.m_clrText = Color.Black;
                                objContent.m_dtmModifyDate = clsMidderTime.s_dtmMidderTime();
                            }

                            objContent.objUserInfo = new clsModifyUserInfo();
                            objContent.objUserInfo.m_clrText = objContent.m_clrText;
                            objContent.objUserInfo.m_dtmModifyDate = objContent.m_dtmModifyDate;
                            objContent.objUserInfo.m_intUserSequence = objContent.m_intUserSequence;
                            objContent.objUserInfo.m_strUserID = objContent.m_strUserID;
                            objContent.objUserInfo.m_strUserName = objContent.m_strUserName;
                            if (objContent.m_intEndIndex > this.Text.Length - 1) continue;
                            if (lstCheckContent.Any(t => t.m_intStartIndex == objContent.m_intStartIndex && t.m_intEndIndex == objContent.m_intEndIndex && t.m_strUserID == objContent.m_strUserID))
                                continue;
                            else
                                lstCheckContent.Add(objContent);
                            this.m_lstTextContentInfos.Add(objContent);
                        }

                        if (this.IsAutoSignature == 0)//需要痕迹控制
                        {
                            if (!clsRichTextBoxTool.CompareModifier(false, this.m_blnTableFlag, this.DBColName, this.m_lstTextContentInfos, this.m_objCurrentModifyUser) && clsGlobalCase.objCaseInfo.intCaseStatus != 1)
                            {
                                this.m_clrOldPartInsertText = Color.Red;
                            }
                            else
                            {
                                if (clsGlobalCase.objCaseInfo != null && clsGlobalCase.objCaseInfo.dtmCreateDate != null && clsGlobalCase.objCaseInfo.intCaseStatus == 2)
                                {
                                    int intHour = 0;
                                    if (intModifyLimit != null)
                                    {
                                        intHour = intModifyLimit.Value;
                                    }
                                    else if (clsGlobalSysParameter.dicSysParameter.ContainsKey(7))
                                    {
                                        int.TryParse(clsGlobalSysParameter.dicSysParameter[7], out intHour);
                                    }

                                    DateTime dtmCreate = clsGlobalCase.objCaseInfo.dtmCreateDate.Value;
                                    if (dtmCreate.AddHours(double.Parse(intHour.ToString())) < clsMidderTime.s_dtmMidderTime())
                                    {
                                        bool bln41 = false;
                                        List<string> lst41 = clsGlobalSysParameter.dicSysParameter[41].ToLower().Split(';').ToList();
                                        if (lst41.Count > 0)
                                        {
                                            foreach (string strP41 in lst41)
                                            {
                                                if (strP41.Split('-').Length == 2)
                                                {
                                                    if (clsGlobalCase.objCaseInfo.strCaseCode.ToLower().Trim() == strP41.Split('-')[0].Trim() && this.DBColName.ToLower().Trim() == strP41.Split('-')[1].Trim())
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
                    }

                    nodeList = clsFunction.s_objReadXML(p_strXML, "DDL");
                    if (nodeList != null)
                    {
                        clsDSTInfo objDST = null;
                        foreach (XmlNode node in nodeList)
                        {
                            objDST = new clsDSTInfo();
                            objDST.m_intStartIndex = int.Parse(node.Attributes["S"].Value);
                            objDST.m_intEndIndex = int.Parse(node.Attributes["E"].Value);
                            objDST.m_strValue = clsRichTextBoxTool.DSTPrefix + node.Attributes["V"].Value;
                            objDST.m_strUserID = node.Attributes["I"].Value;
                            objDST.m_strUserName = node.Attributes["N"].Value;
                            objDST.m_dtmDeleteTime = DateTime.Parse(node.Attributes["D"].Value);

                            this.m_lstDoubleStrikeThrough.Add(objDST);
                        }
                    }
                }

                nodeList = clsFunction.s_objReadXML(p_strXML, "MedicalTerm");
                if (nodeList != null)
                {
                    bool blnInterllRefFlag = false;
                    clsDCIntellectionRef objRef = null;
                    List<clsDCIntellectionRef> lstReference = new List<clsDCIntellectionRef>();

                    List<clsMedicalTerm> lstPatElement = new List<clsMedicalTerm>();
                    clsMedicalTerm objTerm = null;
                    foreach (XmlNode node in nodeList)
                    {
                        objTerm = new clsMedicalTerm();
                        objTerm.m_intStartIndex = int.Parse(node.Attributes["S"].Value);
                        objTerm.m_intEndIndex = int.Parse(node.Attributes["E"].Value);
                        if (node.Attributes["A"] != null) objTerm.m_strCaseCode = node.Attributes["A"].Value;
                        objTerm.m_strTID = node.Attributes["T"].Value;
                        objTerm.m_strValue = node.Attributes["V"].Value;
                        objTerm.m_strUserID = "8888"; // node.Attributes["I"].Value;
                        //objTerm.m_strUserName = node.Attributes["N"].Value;
                        //objTerm.m_dtmCreateTime = DateTime.Parse(node.Attributes["D"].Value);

                        if (objTerm.m_strTID == "PatInfo" && this.FormTypeName != clsRichTextBoxTool.TemplateFormClass)
                        {
                            objRef = new clsDCIntellectionRef();
                            objRef.intClassID = 1;
                            objRef.intStartIndex = objTerm.m_intStartIndex;
                            objRef.intLen = objTerm.m_strValue.Trim().Length;
                            objRef.strCaseColCode = string.Empty;
                            objRef.strText = objTerm.m_strValue.Trim();//objTerm.m_strValue.Substring(1, objTerm.m_strValue.Length - 2);

                            lstReference.Add(objRef);
                        }
                        else if (objTerm.m_strTID.StartsWith("Intellection") && this.FormTypeName != clsRichTextBoxTool.TemplateFormClass)
                        {
                            objRef = new clsDCIntellectionRef();
                            objRef.intClassID = 2;
                            objRef.intStartIndex = objTerm.m_intStartIndex;
                            objRef.intLen = objTerm.m_strValue.Trim().Length;
                            objRef.strCaseColCode = objTerm.m_strTID.Replace("Intellection;", "");
                            objRef.strText = "{" + objTerm.m_strValue.Trim() + "}";//"{" + objTerm.m_strValue.Substring(1, objTerm.m_strValue.Length - 2) + "}";
                            objRef.strOrgText = objRef.strText;

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
                        if (blnInterllRefFlag)
                        {
                            clsProxyUniversal proxy = new clsProxyUniversal();
                            proxy.proxyUniversal.m_intIntellectionRefCheck(string.Empty, clsGlobalPatient.objCurrentPatient.intRegisterID, ref lstReference);
                            proxy = null;
                        }
                        lstReference.Sort();

                        int pos = -1;
                        string strText = string.Empty;
                        string strCaseColCode = string.Empty;
                        string strCaseCode = string.Empty;
                        string strColCode = string.Empty;
                        for (int i = lstReference.Count - 1; i >= 0; i--)
                        {
                            this.SelectionStart = lstReference[i].intStartIndex;
                            this.SelectionLength = lstReference[i].intLen;
                            this.SelectionColor = m_clrOldPartInsertText;
                            this.SelectionFont = this.Font;
                            this.SelectionCharOffset = 0;

                            strText = lstReference[i].strText;
                            if (strText == lstReference[i].strOrgText)
                            {
                                clsRichTextBoxPlus.InsertText(this, string.Empty);
                                List<clsDCIntellectionRef> lstTempRef = lstReference.FindAll(t => t.intStartIndex > lstReference[i].intStartIndex);
                                foreach (clsDCIntellectionRef objIntellectionRef in lstTempRef)
                                {
                                    objIntellectionRef.intStartIndex -= lstReference[i].intLen;
                                }
                                foreach (clsMedicalTerm objMedicalTerm in m_lstMedicalTerm)
                                {
                                    if (objMedicalTerm.m_intStartIndex > lstReference[i].intStartIndex)
                                    {
                                        objMedicalTerm.m_intStartIndex -= lstReference[i].intLen;
                                        objMedicalTerm.m_intEndIndex -= lstReference[i].intLen;
                                    }
                                }
                            }
                            else
                            {
                                if (lstReference[i].intClassID == 1)
                                {
                                    string strPatElementInfo = this.m_strGetPatElementInfo(strText) == null ? "" : this.m_strGetPatElementInfo(strText);
                                    int intElementLen = strPatElementInfo.Length;
                                    clsRichTextBoxPlus.InsertText(this, strPatElementInfo);
                                    List<clsDCIntellectionRef> lstTempRef = lstReference.FindAll(t => t.intStartIndex > lstReference[i].intStartIndex);
                                    foreach (clsDCIntellectionRef objIntellectionRef in lstTempRef)
                                    {
                                        objIntellectionRef.intStartIndex += intElementLen - lstReference[i].intLen;
                                    }
                                    foreach (clsMedicalTerm objMedicalTerm in m_lstMedicalTerm)
                                    {
                                        if (objMedicalTerm.m_intStartIndex > lstReference[i].intStartIndex)
                                        {
                                            objMedicalTerm.m_intStartIndex += intElementLen - lstReference[i].intLen;
                                            objMedicalTerm.m_intEndIndex += intElementLen - lstReference[i].intLen;
                                        }
                                    }
                                }
                                else if (lstReference[i].intClassID == 2)
                                {
                                    strCaseColCode = lstReference[i].strCaseColCode;
                                    strCaseCode = strCaseColCode.Split(';')[0];
                                    strColCode = strCaseColCode.Split(';')[1];
                                    strText = clsHelper.s_strCaseColumnContent(strCaseCode, strColCode, strText);
                                    if (strText.StartsWith("初步诊断:"))
                                    {
                                        strText = strText.Substring(5);
                                    }
                                    int intTextLen = strText.Length;
                                    clsRichTextBoxPlus.InsertText(this, strText);
                                    List<clsDCIntellectionRef> lstTempRef = lstReference.FindAll(t => t.intStartIndex > lstReference[i].intStartIndex);
                                    foreach (clsDCIntellectionRef objIntellectionRef in lstTempRef)
                                    {
                                        objIntellectionRef.intStartIndex += intTextLen - lstReference[i].intLen;
                                    }
                                    foreach (clsMedicalTerm objMedicalTerm in m_lstMedicalTerm)
                                    {
                                        if (objMedicalTerm.m_intStartIndex > lstReference[i].intStartIndex)
                                        {
                                            objMedicalTerm.m_intStartIndex += intTextLen - lstReference[i].intLen;
                                            objMedicalTerm.m_intEndIndex += intTextLen - lstReference[i].intLen;
                                        }
                                    }

                                    if (this.MaskType == 3 || this.MaskType == 4 || this.MaskType == 5 || this.MaskType == 6)
                                    {
                                        nodeList = clsFunction.s_objReadXML(lstReference[i].strXmlContent, "ICDInfo");
                                        if (nodeList != null)
                                        {
                                            clsICDInfo objICD = null;
                                            foreach (XmlNode node in nodeList)
                                            {
                                                int intFlags = int.Parse(node.Attributes["F"].Value);
                                                if (intFlags < 0) continue;

                                                objICD = new clsICDInfo();
                                                objICD.m_intStartIndex = int.Parse(node.Attributes["S"].Value) + this.SelectionStart;
                                                objICD.m_intEndIndex = int.Parse(node.Attributes["E"].Value) + this.SelectionStart;
                                                objICD.m_intFlags = intFlags;
                                                objICD.m_strICDCode = node.Attributes["I"].Value;
                                                objICD.m_strICDName = node.Attributes["N"].Value;

                                                if (!m_lstICDInfo.Exists(t => t.m_strICDCode == objICD.m_strICDCode))
                                                {
                                                    this.m_lstICDInfo.Add(objICD);
                                                }
                                            }
                                        }

                                        m_lstICDInfo.Sort((a, b) => { return a.m_intStartIndex.CompareTo(b.m_intStartIndex); });
                                    }
                                }
                            }
                        }
                        this.SelectionLength = 0;
                    }
                    this.m_mthSetMedicalTermColor(0);
                }

                nodeList = clsFunction.s_objReadXML(p_strXML, "ICDInfo");
                if (nodeList != null)
                {
                    clsICDInfo objICD = null;
                    foreach (XmlNode node in nodeList)
                    {
                        objICD = new clsICDInfo();
                        objICD.m_intStartIndex = int.Parse(node.Attributes["S"].Value);
                        objICD.m_intEndIndex = int.Parse(node.Attributes["E"].Value);
                        objICD.m_intFlags = int.Parse(node.Attributes["F"].Value);
                        objICD.m_strICDCode = node.Attributes["I"].Value;
                        objICD.m_strICDName = node.Attributes["N"].Value;

                        this.m_lstICDInfo.Add(objICD);
                    }
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


            }
            catch (Exception objEx)
            {
                clsDialog.Msg(objEx.Message, MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
                clsFunction.s_blnOuputException(objEx);
            }
            finally
            {
                this.m_blnInitSetXml = false;
                isCompleteSetXml = true;
                clsRichTextBoxTool.Redraw(this.Handle);
                this.Invalidate();
            }
        }

        private void SetTextLength()
        {
            if (clsGlobalSysParameter.dicSysParameter != null && clsGlobalCase.objCaseInfo != null && clsGlobalCase.objCaseInfo.strCaseCode != null && this.DBColName != null)
            {
                List<string> lstColLimit = clsGlobalSysParameter.dicSysParameter[66].ToLower().Split(';').ToList();
                if (lstColLimit.Count > 0)
                {
                    string[] sarr = null;
                    foreach (string str in lstColLimit)
                    {
                        sarr = str.Split('-');
                        if (sarr.Length == 3)
                        {
                            if (sarr[0] == clsGlobalCase.objCaseInfo.strCaseCode.ToLower() && sarr[1] == this.DBColName.ToLower())
                            {
                                int intFontNums = 0;
                                int.TryParse(sarr[2], out intFontNums);
                                if (intFontNums > 0)
                                {
                                    this.MaxLength = intFontNums * 2;
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

                    clsMedicalTerm obj = null;
                    this.m_lstMedicalTerm.Sort();
                    for (int i = m_lstMedicalTerm.Count - 1; i >= 0; i--)
                    {
                        obj = m_lstMedicalTerm[i];
                        if (obj.m_strValue.StartsWith("[") && obj.m_strValue.EndsWith("]"))
                        {
                            rich.Select(obj.m_intEndIndex, 1);
                            clsRichTextBoxPlus.InsertText(rich, "");

                            rich.Select(obj.m_intStartIndex, 1);
                            clsRichTextBoxPlus.InsertText(rich, "");

                            obj.m_strValue = obj.m_strValue.Substring(1);
                            obj.m_strValue = obj.m_strValue.Substring(0, obj.m_strValue.Length - 1);
                            blnStatus = true;
                        }
                    }
                    this.Rtf = rich.Rtf;
                }

                if (blnStatus)
                {
                    clsRichTextBoxTool.AdjustTermPosition(ref this.m_lstMedicalTerm, this.FntElement, this);
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
            foreach (enumPatientInfoType enPatInfo in Enum.GetValues(typeof(enumPatientInfoType)))
            {
                if (enPatInfo.ToString() == p_strElementName) strInfo = clsPatientInfoHelper.GetTypePatientInfo(enPatInfo);
            }
            return strInfo;
        }
        #endregion

        #region 获取XML文本
        /// <summary>
        /// 获取XML文本
        /// </summary>
        /// <returns>XML文本</returns>
        public string m_strGetXmlText()
        {
            this.m_mthFinishEdit();
            return GetXmlFromInfo(this.Text, DateTime.Now, ExtSpecModifyFlag, this.FormTypeName,
                this.m_objCurrentModifyUser, this.m_lstDoubleStrikeThrough, this.m_lstModifyUsers,
                this.m_lstTextContentInfos, this.m_lstMedicalTerm, m_lstICDInfo, null);
        }

        /// <summary>
        /// 获取XML文本
        /// </summary>
        /// <param name="p_dtmCaseWriteDate"></param>
        /// <returns></returns>
        public string m_strGetXmlText(DateTime? p_dtmCaseWriteDate)
        {
            this.m_mthFinishEdit();
            return GetXmlFromInfo(this.Text, DateTime.Now, ExtSpecModifyFlag, this.FormTypeName,
                this.m_objCurrentModifyUser, this.m_lstDoubleStrikeThrough, this.m_lstModifyUsers,
                this.m_lstTextContentInfos, this.m_lstMedicalTerm, m_lstICDInfo, p_dtmCaseWriteDate);
        }

        /// <summary>
        /// 从XMLInfo对象获取xml文本
        /// </summary>
        private string GetXmlFromInfo(string textStr, DateTime dateNow, bool extSpecModifyFlag, string formTypeName,
            clsModifyUserInfo userInfo, List<clsDSTInfo> p_lstDSTIndex, List<clsModifyUserInfo> p_lstUserInfo,
            List<clsUserContentInfo> p_lstTextContentInfos, List<clsMedicalTerm> p_lstMedicalTerm,
            List<clsICDInfo> p_lstICDInfo, DateTime? p_dtmCaseWriteDate)
        {
            string result;
            if (string.IsNullOrEmpty(textStr.Trim()))
            {
                result = string.Empty;
            }
            else
            {
                MemoryStream memoryStream = new MemoryStream();
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode);
                xmlTextWriter.WriteStartDocument();
                xmlTextWriter.WriteStartElement("Main");
                DateTime dateTime = dateNow;
                if (p_lstUserInfo.Count > 0 || p_lstTextContentInfos.Count > 0)
                {
                    xmlTextWriter.WriteStartElement("Content");
                    List<clsUserContentInfo> list = new List<clsUserContentInfo>();
                    clsUserContentInfo objContentInfo = null;
                    for (int i = 0; i < p_lstTextContentInfos.Count; i++)
                    {
                        objContentInfo = p_lstTextContentInfos[i];
                        if (string.IsNullOrEmpty(objContentInfo.m_strUserID))
                        {
                            objContentInfo.m_strUserID = userInfo.m_strUserID;
                        }
                        if (string.IsNullOrEmpty(objContentInfo.m_strUserName))
                        {
                            objContentInfo.m_strUserName = userInfo.m_strUserName;
                        }
                        if (objContentInfo.m_intEndIndex <= textStr.Length - 1)
                        {
                            if (!extSpecModifyFlag || !(objContentInfo.m_strUserID == clsGlobalLoginInfo.objLoginInfo.strEmpId))
                            {
                                if (!list.Any((clsUserContentInfo t) => t.m_intStartIndex == objContentInfo.m_intStartIndex && t.m_intEndIndex == objContentInfo.m_intEndIndex && t.m_strUserID == objContentInfo.m_strUserID))
                                {
                                    list.Add(objContentInfo);
                                    xmlTextWriter.WriteStartElement("C");
                                    xmlTextWriter.WriteAttributeString("S", objContentInfo.m_intStartIndex.ToString());
                                    xmlTextWriter.WriteAttributeString("E", objContentInfo.m_intEndIndex.ToString());
                                    xmlTextWriter.WriteAttributeString("I", objContentInfo.m_strUserID);
                                    xmlTextWriter.WriteAttributeString("N", objContentInfo.m_strUserName);
                                    xmlTextWriter.WriteAttributeString("R", objContentInfo.m_clrText.ToArgb().ToString());
                                    if (!p_dtmCaseWriteDate.HasValue)
                                    {
                                        if (objContentInfo.m_dtmModifyDate.ToString("yyyy") == "0001")
                                        {
                                            xmlTextWriter.WriteAttributeString("D", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                                            objContentInfo.m_dtmModifyDate = dateTime;
                                        }
                                        else
                                        {
                                            xmlTextWriter.WriteAttributeString("D", objContentInfo.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
                                        }
                                    }
                                    else
                                    {
                                        xmlTextWriter.WriteAttributeString("D", p_dtmCaseWriteDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                                        objContentInfo.m_dtmModifyDate = p_dtmCaseWriteDate.Value;
                                    }
                                    xmlTextWriter.WriteEndElement();
                                }
                            }
                        }
                    }
                    xmlTextWriter.WriteEndElement();
                }
                if (p_lstMedicalTerm.Count > 0)
                {
                    p_lstMedicalTerm.Sort();
                    xmlTextWriter.WriteStartElement("MedicalTerm");
                    for (int i = 0; i < p_lstMedicalTerm.Count; i++)
                    {
                        clsMedicalTerm clsMedicalTerm = p_lstMedicalTerm[i];
                        if ((!(clsMedicalTerm.m_strTID == "PatInfo") || !(formTypeName != clsRichTextBoxTool.TemplateFormClass)) && (!clsMedicalTerm.m_strTID.StartsWith("Intellection") || !(formTypeName != clsRichTextBoxTool.TemplateFormClass)))
                        {
                            if (formTypeName == clsRichTextBoxTool.TemplateFormClass)
                            {
                                clsMedicalTerm.m_strUserID = string.Empty;
                                clsMedicalTerm.m_strUserName = string.Empty;
                            }
                            xmlTextWriter.WriteStartElement("C");
                            xmlTextWriter.WriteAttributeString("S", clsMedicalTerm.m_intStartIndex.ToString());
                            xmlTextWriter.WriteAttributeString("E", clsMedicalTerm.m_intEndIndex.ToString());
                            if (string.IsNullOrEmpty(clsMedicalTerm.m_strCaseCode))
                            {
                                xmlTextWriter.WriteAttributeString("A", clsGlobalCase.objCaseInfo.strCaseCode);
                            }
                            else
                            {
                                xmlTextWriter.WriteAttributeString("A", clsMedicalTerm.m_strCaseCode);
                            }
                            xmlTextWriter.WriteAttributeString("T", clsMedicalTerm.m_strTID);
                            xmlTextWriter.WriteAttributeString("V", clsMedicalTerm.m_strValue);
                            xmlTextWriter.WriteEndElement();
                        }
                    }
                    xmlTextWriter.WriteEndElement();
                }
                if (p_lstDSTIndex.Count > 0)
                {
                    p_lstDSTIndex.Sort();
                    xmlTextWriter.WriteStartElement("DDL");
                    for (int i = 0; i < p_lstDSTIndex.Count; i++)
                    {
                        clsDSTInfo clsDSTInfo = p_lstDSTIndex[i];
                        xmlTextWriter.WriteStartElement("C ");
                        xmlTextWriter.WriteAttributeString("S", clsDSTInfo.m_intStartIndex.ToString());
                        xmlTextWriter.WriteAttributeString("E", clsDSTInfo.m_intEndIndex.ToString());
                        xmlTextWriter.WriteAttributeString("V", clsDSTInfo.m_strValue.Substring(1));
                        xmlTextWriter.WriteAttributeString("I", clsDSTInfo.m_strUserID);
                        xmlTextWriter.WriteAttributeString("N", clsDSTInfo.m_strUserName);
                        if (!p_dtmCaseWriteDate.HasValue)
                        {
                            if (clsDSTInfo.m_dtmDeleteTime.ToString("yyyy") == "0001")
                            {
                                xmlTextWriter.WriteAttributeString("D", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                                clsDSTInfo.m_dtmDeleteTime = dateTime;
                            }
                            else
                            {
                                xmlTextWriter.WriteAttributeString("D", clsDSTInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        else
                        {
                            xmlTextWriter.WriteAttributeString("D", p_dtmCaseWriteDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                            clsDSTInfo.m_dtmDeleteTime = p_dtmCaseWriteDate.Value;
                        }
                        xmlTextWriter.WriteEndElement();
                    }
                    xmlTextWriter.WriteEndElement();
                }
                if (p_lstICDInfo.Count > 0)
                {
                    xmlTextWriter.WriteStartElement("ICDInfo");
                    for (int i = 0; i < p_lstICDInfo.Count; i++)
                    {
                        clsICDInfo objICDInfo = p_lstICDInfo[i];
                        xmlTextWriter.WriteStartElement("C ");
                        xmlTextWriter.WriteAttributeString("S", objICDInfo.m_intStartIndex.ToString());
                        xmlTextWriter.WriteAttributeString("E", objICDInfo.m_intEndIndex.ToString());
                        xmlTextWriter.WriteAttributeString("F", objICDInfo.m_intFlags.ToString());
                        xmlTextWriter.WriteAttributeString("I", strEscapeCharacter(objICDInfo.m_strICDCode));
                        xmlTextWriter.WriteAttributeString("N", strEscapeCharacter(objICDInfo.m_strICDName));
                        xmlTextWriter.WriteEndElement();
                    }
                    xmlTextWriter.WriteEndElement();
                }
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndDocument();
                xmlTextWriter.Flush();
                string @string = Encoding.Unicode.GetString(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                int num = @string.IndexOf("<Main");
                result = @string.Substring(num, @string.Length - num);
            }
            return result;
        }

        /// <summary>
        /// 对xml字符转义
        /// </summary>
        /// <param name="p_strOrgString"></param>
        /// <returns></returns>
        private string strEscapeCharacter(string p_strOrgString)
        {
            if (string.IsNullOrEmpty(p_strOrgString))
            {
                return p_strOrgString;
            }

            p_strOrgString = p_strOrgString.Replace("<", "&lt;");
            p_strOrgString = p_strOrgString.Replace(">", "&gt;");
            p_strOrgString = p_strOrgString.Replace("&", "&amp;");
            p_strOrgString = p_strOrgString.Replace("'", "&apos;");
            p_strOrgString = p_strOrgString.Replace("\"", "&quot;");
            return p_strOrgString;
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
            List<clsSuperSubScript> lstScript = new List<clsSuperSubScript>();
            List<clsFontColor> lstFontColor = new List<clsFontColor>();
            List<clsFontBold> lstFontBold = new List<clsFontBold>();
            List<clsFontItalic> lstFontItalic = new List<clsFontItalic>();
            List<clsFontUnderLine> lstFontUnderLine = new List<clsFontUnderLine>();

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
                        clsSuperSubScript objScript = new clsSuperSubScript();
                        objScript.m_intCharOffset = intOffset;
                        objScript.m_intIndex = intStart;
                        objScript.m_strValue = txtTemp.Text.Substring(intStart, intEnd - intStart + 1);
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
                        clsFontColor objFontColor = new clsFontColor();
                        objFontColor.m_intIndex = intStart;
                        objFontColor.m_clrValue = clrTmp;
                        objFontColor.m_strValue = txtTemp.Text.Substring(intStart, intEnd - intStart + 1);
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
                        clsFontBold objFontBold = new clsFontBold();
                        objFontBold.m_intIndex = intStart;
                        objFontBold.m_strValue = txtTemp.Text.Substring(intStart, intEnd - intStart + 1);
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
                        clsFontItalic objFontItalic = new clsFontItalic();
                        objFontItalic.m_intIndex = intStart;
                        objFontItalic.m_strValue = txtTemp.Text.Substring(intStart, intEnd - intStart + 1);
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
                        clsFontUnderLine objFontUnderLine = new clsFontUnderLine();
                        objFontUnderLine.m_intIndex = intStart;
                        objFontUnderLine.m_strValue = txtTemp.Text.Substring(intStart, intEnd - intStart + 1);
                        lstFontUnderLine.Add(objFontUnderLine);
                    }

                    i++;
                }
                #endregion
            }

            return clsRichTextBoxTool.GetTraceXmlFromInfo(this.Text, this.m_lstDoubleStrikeThrough, this.m_lstModifyUsers, this.m_lstTextContentInfos, lstScript, lstFontColor, lstFontBold, lstFontItalic, lstFontUnderLine, this.m_lstMedicalTerm);
        }
        #endregion

        #region 重写
        /// <summary>
        /// 复制
        /// </summary>
        public new void Copy()
        {
            if (!blnDiagFlgas && this.SelectedText == "")
            {
                return;
            }

            if (clsGlobalPatient.objCurrentPatient != null && this.FindForm() is frmBaseMdiCase)
            {
                if (string.IsNullOrEmpty(clsGlobalPatient.objCurrentPatient.strPatientID))
                {
                    if (blnDiagFlgas)
                    {
                        clsCopyICDInfo objCopyICDInfo = new clsCopyICDInfo();
                        objCopyICDInfo.strText = this.m_strGetText();
                        objCopyICDInfo.lstICDInfo = m_lstICDInfo.FindAll(t => t.m_intFlags >= 0);
                        Clipboard.SetData("ICDINFO", objCopyICDInfo);
                    }
                    else
                    {
                        if (clsGlobalSysParameter.dicSysParameter.ContainsKey(87)
                            && clsGlobalSysParameter.dicSysParameter[87] == "1")
                        {
                            Clipboard.SetText(this.SelectedText);
                        }
                        else
                        {
                            if (clsGlobalHospitalCode.Code == "0008")
                            {
                                Clipboard.SetText("[ehr]" + this.m_strSysSplit + "nopatient" + this.m_strSysSplit + this.SelectedText);
                            }
                            else
                            {
                                Clipboard.SetText(this.SelectedText);
                            }
                        }
                    }
                }
                else
                {
                    if (clsGlobalSysParameter.dicSysParameter.ContainsKey(87)
                        && clsGlobalSysParameter.dicSysParameter[87] == "1")
                    {
                        if (blnDiagFlgas)
                        {
                            clsCopyICDInfo objCopyICDInfo = new clsCopyICDInfo();
                            objCopyICDInfo.strText = this.m_strGetText();
                            objCopyICDInfo.lstICDInfo = m_lstICDInfo.FindAll(t => t.m_intFlags >= 0);
                            Clipboard.SetData("ICDINFO", objCopyICDInfo);
                        }
                        else
                        {
                            Clipboard.SetText(this.SelectedText);
                        }
                    }
                    else
                    {
                        if (blnDiagFlgas)
                        {
                            clsCopyICDInfo objCopyICDInfo = new clsCopyICDInfo();
                            objCopyICDInfo.strText = this.m_strGetText();
                            objCopyICDInfo.lstICDInfo = m_lstICDInfo.FindAll(t => t.m_intFlags >= 0);
                            Clipboard.SetData(clsGlobalPatient.objCurrentPatient.strPatientID + "ICDINFO", objCopyICDInfo);
                        }
                        else
                        {
                            if (clsGlobalHospitalCode.Code == "0008")
                            {
                                Clipboard.SetText("[ehr]" + this.m_strSysSplit + clsGlobalPatient.objCurrentPatient.strPatientID + this.m_strSysSplit + this.SelectedText);
                            }
                            else
                            {
                                Clipboard.SetData(clsGlobalPatient.objCurrentPatient.strPatientID, this.SelectedText);
                            }
                        }
                    }
                }
            }
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

            //if (!(FirstlineCaption == null))//重复一次内容的情况没发现了
            //{
            this.m_mthSetRowSpacing();//在控件初始化(Init)时调用，在此处调用会导致一个问题，当FirstlineCaption == null时，第一次用输入法输入文字时，会重复一次内容。
            //}
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

        /// <summary>
        /// 文本内容改变时:调用各类操作
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            if (DesignMode) return;
            if (this.m_blnIniting) return;
            if (this.m_blnMouseMoving) return;
            if (this.IsReplaceRtf) return;
            if (this.m_blnDoubleLine) return;
            //this.m_mthAutoSetTerm();

            if (clsGlobalHospitalCode.Code == "0010")
            {
                string strRightText = this.m_strGetRightText();
                if (this.MaxLength != 0 && System.Text.Encoding.Default.GetBytes(strRightText).Length >= this.MaxLength && evtReachMaxLengthOld != null)
                {
                    evtReachMaxLengthOld(this);
                    this.FirstlineCaption = "";
                }
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
                    intTextLength = System.Text.Encoding.Default.GetBytes(this.m_strGetRightText()).Length;
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

            intTextLength = System.Text.Encoding.Default.GetBytes(this.m_strGetRightText()).Length;
            base.OnTextChanged(e);

            //若内容为空,则所有操作复位
            if (this.Text == string.Empty) this.m_mthClearText();

            if (clsGlobalHospitalCode.Code != "0010" && m_blnTableFlag)
            {
                m_mthCalRegion();
            }
            this.RedoICDInfo();
            this.Invalidate();
        }

        private void RedoICDInfo()
        {
            if (autocompleteAssistant.TargetControlWrapper != null)
            {
                List<clsICDInfo> lstNew = new List<clsICDInfo>();
                string strText = this.Text;

                List<Range> lstRange = new List<Range>();

                var regex = new Regex(@"[^\s^\r]");
                Range result = null;
                int intStart = FirstlineCaptionLen;
                int intEnd = strText.Length;

                bool blnFlags = true;
                for (int i = intEnd; i >= intStart; i--)
                {
                    if (blnFlags)
                    {
                        result = new Range(autocompleteAssistant.TargetControlWrapper);
                        result.End = i;
                        blnFlags = false;
                    }

                    if (i == intStart || !regex.IsMatch(strText[i - 1].ToString()))
                    {
                        result.Start = i;
                        lstRange.Add(result);
                        blnFlags = true;
                    }
                    else if(m_lstDoubleStrikeThrough.Exists(t => t.m_intEndIndex + 1 == i) && result.End != i)
                    {
                        result.Start = i;
                        lstRange.Add(result);
                        blnFlags = true;
                        i++;
                    }
                }

                lstNew = new List<clsICDInfo>();
                clsICDInfo objICDInfo = null;
                clsICDInfo objTempInfo = null;

                if (m_lstICDInfo.Count > lstRange.Count)//删除了一部分
                {
                    m_lstICDInfo.RemoveAll(t => !lstRange.Exists(p => p.Text == t.m_strICDName));
                }

                foreach (Range rg in lstRange)
                {
                    if (rg.Start == rg.End) continue;
                    objTempInfo = m_lstICDInfo.FirstOrDefault(t => ((t.m_intStartIndex + FirstlineCaptionLen >= rg.Start && t.m_intStartIndex + FirstlineCaptionLen <= rg.End)
                        || (t.m_intEndIndex + FirstlineCaptionLen >= rg.Start && t.m_intEndIndex + FirstlineCaptionLen <= rg.End))
                        && !lstNew.Exists(p => p.m_strICDCode == t.m_strICDCode));
                    if (objTempInfo != null)
                    {
                        objICDInfo = new clsICDInfo();
                        objICDInfo.m_intFlags = objTempInfo.m_intFlags;
                        objICDInfo.m_strICDCode =strEscapeCharacter( objTempInfo.m_strICDCode);
                        objICDInfo.m_intStartIndex = rg.Start - FirstlineCaptionLen;
                        objICDInfo.m_intEndIndex = rg.End - FirstlineCaptionLen - 1;
                        objICDInfo.m_strICDName = strEscapeCharacter(rg.Text);
                    }
                    else
                    {
                        objICDInfo = new clsICDInfo();
                        objICDInfo.m_intFlags = 0;
                        objICDInfo.m_strICDCode = "";
                        objICDInfo.m_intStartIndex = rg.Start - FirstlineCaptionLen;
                        objICDInfo.m_intEndIndex = rg.End - FirstlineCaptionLen - 1;
                        objICDInfo.m_strICDName = strEscapeCharacter(rg.Text);
                    }

                    if (!lstNew.Contains(objICDInfo))
                    {
                        if (objICDInfo.m_strICDName.StartsWith(clsRichTextBoxTool.DSTPrefix))
                        {
                            objICDInfo.m_strICDName = objICDInfo.m_strICDName.Replace(clsRichTextBoxTool.DSTPrefix, "");
                            objICDInfo.m_intStartIndex = objICDInfo.m_intStartIndex - 1;
                        }

                        lstNew.Add(objICDInfo);
                    }
                }

                lstNew.Sort((a, b) => { return a.m_intStartIndex.CompareTo(b.m_intStartIndex); });
                m_lstICDInfo = lstNew;
            }
        }

        private const int EM_GETLINECOUNT = 0x00BA;//获取总行数的消息号 
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        private const int EM_GETLINE = 0xc4;//获取当前行文本的消息号 
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage_Ex(IntPtr hwnd, int wMsg, int wParam, StringBuilder lParam);

        /// <summary>
        /// 自动计算位置
        /// </summary>
        private void m_mthCalRegion()
        {
            using (Graphics g = this.CreateGraphics())
            {
                SizeF szfTextSize = g.MeasureString(this.Text, this.Font, this.Width);
                if (szfTextSize.Height > this.Height)//超出高度
                {
                    int intLineCount = SendMessage(this.Handle, EM_GETLINECOUNT, IntPtr.Zero, "");
                    StringBuilder sbBuffer = null;
                    StringBuilder sbTemp = new StringBuilder();
                    string strRemindText = string.Empty;
                    for (int intI = 0; intI < intLineCount; intI++)
                    {
                        sbBuffer = new StringBuilder(512);
                        sbBuffer.Append(512);
                        SendMessage_Ex(this.Handle, EM_GETLINE, intI, sbBuffer);
                        strRemindText = sbTemp.ToString();
                        sbTemp.Append(sbBuffer.ToString());
                        SizeF szfTempSize = g.MeasureString(sbTemp.ToString(), this.Font, this.Width);
                        if (szfTempSize.Height > this.Height)
                        {
                            break;
                        }
                    }

                    if (evtReachMaxLength != null)
                    {
                        int intSelectionStart = this.SelectionStart;
                        List<clsDSTInfo> lstDSTInfo = this.m_lstDoubleStrikeThrough.FindAll(t => t.m_intEndIndex >= strRemindText.Length);
                        if (lstDSTInfo.Count > 0)
                        {
                            int intMinStartIndex = lstDSTInfo.Min(t => t.m_intStartIndex);
                            if (intMinStartIndex < strRemindText.Length)
                            {
                                strRemindText = strRemindText.Substring(0, intMinStartIndex);
                            }
                            foreach (clsDSTInfo obj in lstDSTInfo)
                            {
                                obj.m_intStartIndex = obj.m_intStartIndex - strRemindText.Length;
                                obj.m_intEndIndex = obj.m_intEndIndex - strRemindText.Length;
                                this.m_lstDoubleStrikeThrough.Remove(obj);
                            }
                        }
                        this.Select(0, strRemindText.Length);
                        string strRemindRtf = this.SelectedRtf;
                        int intOutlineTextLen = this.Text.Length - strRemindText.Length;
                        this.Select(strRemindText.Length, intOutlineTextLen);
                        string strOutlineRtf = this.SelectedRtf;

                        bool blnTemp = intSelectionStart >= strRemindText.Length;
                        evtReachMaxLength(this, strOutlineRtf, lstDSTInfo, intOutlineTextLen, intSelectionStart - strRemindText.Length);
                        this.Rtf = strRemindRtf;
                        if (!blnTemp)
                        {
                            this.SelectionStart = intSelectionStart;
                            this.Focus();
                        }
                        this.Text = this.Text;//重新赋值一次，解决由于字段换行导致部分文字显示不出来的问题。
                    }
                }
            }
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
                    if (clsGlobalPatient.objCurrentPatient != null && !string.IsNullOrEmpty(clsGlobalPatient.objCurrentPatient.strPatientID))
                    {
                        Clipboard.SetData(clsGlobalPatient.objCurrentPatient.strPatientID, this.SelectedText);
                    }
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
            #region 按键
            KeyCtrlC = false;
            if (!CheckValidCaptionCursor())
            {
                m_blnIn = true;
                this.SelectionLength = 0;
                this.SelectionStart = this.m_strGetFirstlineCaption().Length;
                this.m_intCurrentCursorIndex = this.SelectionStart;
                this.SelectionCharOffset = 0;
                m_blnIn = false;

                ImeMode = ImeMode.Off;
                e.Handled = true;
                return;
            }

            this.m_mthHintConfineInfo();

            int intCurrPos = this.m_intCurrentCursorIndex;
            if (e.KeyCode == Keys.Down)
            {
                if (!this.m_blnTableFlag && this.GetLineFromCharIndex(intCurrPos) == this.GetLineFromCharIndex(this.Text.Length - 1) && !autocompleteAssistant.IsOpen)
                {
                    SendKeys.SendWait("{TAB}");
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (!this.m_blnTableFlag && this.GetLineFromCharIndex(intCurrPos) == this.GetLineFromCharIndex(0) && !autocompleteAssistant.IsOpen)
                {
                    SendKeys.SendWait("+{TAB}");
                }
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.Clear();
                this.Copy();
                if (m_blnIsSelectedChanged)
                {
                    m_blnIsSelectedChanged = false;
                }
                KeyCtrlC = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X && !blnCutFlags)
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
                    clsMedicalTerm objElement = CursorPositionToTerm(this.SelectionStart);
                    if (objElement != null && objElement.m_strTID.ToLower() == clsRichTextBoxTool.OpCall)
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

            if (this.IsAutoSignature == 0)//需要痕迹控制
            {
                #region 删除控制
                this.m_blnIsBackspace = false;
                if (e.KeyCode == Keys.Delete || (m_blnMuliSelectedFlag && e.KeyCode == Keys.Enter))
                {
                    if (!blnDelPosition)
                    {
                        if (this.Text.Replace("\n", "").Trim() == this.SelectedText.Replace("\n", "").Trim())
                        {
                            if (clsGlobalSysCaseScope.blnExtCall && (m_lstDoubleStrikeThrough == null || m_lstDoubleStrikeThrough.Count == 0))
                            {
                                this.m_mthClearText();
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
                    foreach (clsUserContentInfo objContentInfo in this.m_lstTextContentInfos)
                    {
                        if (objContentInfo.m_intStartIndex <= m_intCurrentCursorIndex && m_intCurrentCursorIndex <= objContentInfo.m_intEndIndex)
                        {
                            if (!CompareModifier(m_objCurrentModifyUser, objContentInfo.objUserInfo, clsHelper.s_dtmMidderTime(), this.m_blnTableFlag, this.DBColName, this.m_lstTextContentInfos, this.m_objCurrentModifyUser) && !this.ExtSpecModifyFlag)
                            {
                                e.Handled = true;
                                return;
                            }
                            break;
                        }
                        else if (objContentInfo.m_intEndIndex < m_intCurrentCursorIndex)
                        {
                            continue;
                        }
                        else
                        {
                            if ((m_intCurrentCursorIndex != 0) && (!CompareModifier(m_objCurrentModifyUser, objContentInfo.objUserInfo, clsHelper.s_dtmMidderTime(), this.m_blnTableFlag, this.DBColName, this.m_lstTextContentInfos, this.m_objCurrentModifyUser)) && !this.ExtSpecModifyFlag)
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
                        foreach (clsDSTInfo obj in this.m_lstDoubleStrikeThrough)
                        {
                            if ((intCurrIndex + 1 > obj.m_intStartIndex && intCurrIndex + 1 < obj.m_intEndIndex) && !this.ExtSpecModifyFlag)
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
                                if ((intCurrIndex >= this.m_lstMedicalTerm[i].m_intStartIndex && intCurrIndex <= this.m_lstMedicalTerm[i].m_intEndIndex) && !this.ExtSpecModifyFlag)
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
                            if (clsGlobalSysCaseScope.blnExtCall && (m_lstDoubleStrikeThrough == null || m_lstDoubleStrikeThrough.Count == 0))
                            {
                                this.m_mthClearText();
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
                    foreach (clsUserContentInfo objContentInfo in this.m_lstTextContentInfos)
                    {
                        if (objContentInfo.m_intStartIndex - 1 <= m_intCurrentCursorIndex && m_intCurrentCursorIndex <= objContentInfo.m_intEndIndex + 1)
                        {
                            if (!CompareModifier(m_objCurrentModifyUser, objContentInfo.objUserInfo, clsHelper.s_dtmMidderTime(), this.m_blnTableFlag, this.DBColName, this.m_lstTextContentInfos, this.m_objCurrentModifyUser) && !this.ExtSpecModifyFlag)
                            {
                                e.Handled = true;
                                return;
                            }
                            break;
                        }
                        else if (objContentInfo.m_intEndIndex < m_intCurrentCursorIndex - 1)		//删除光标的前一个字符
                        {
                            continue;
                        }
                        else
                        {
                            if (((m_intCurrentCursorIndex != 0) && CompareModifier(m_objCurrentModifyUser, objContentInfo.objUserInfo, clsHelper.s_dtmMidderTime(), this.m_blnTableFlag, this.DBColName, this.m_lstTextContentInfos, this.m_objCurrentModifyUser)) && !this.ExtSpecModifyFlag)
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
                        foreach (clsDSTInfo obj in this.m_lstDoubleStrikeThrough)
                        {
                            if ((intCurrIndex - 1 > obj.m_intStartIndex && intCurrIndex - 1 <= obj.m_intEndIndex) && !this.ExtSpecModifyFlag)
                            {
                                e.Handled = true;
                                return;
                            }
                        }

                        if (!IsAllowElementFreeEdit)
                        {
                            for (int i = 0; i < this.m_lstMedicalTerm.Count; i++)
                            {
                                if ((intCurrIndex - 1 >= this.m_lstMedicalTerm[i].m_intStartIndex && intCurrIndex - 1 <= this.m_lstMedicalTerm[i].m_intEndIndex) && !this.ExtSpecModifyFlag)
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
                    this.m_mthClearText();
                    return;
                }
                #endregion
            }

            #region 元素
            if (e.KeyCode == Keys.F5 && !(this is ctlAllergy))
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
                    clsDialog.Msg("元素不可以编辑。");
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

            if (e.KeyCode == Keys.Enter && !autocompleteAssistant.IsOpen)
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

            if (ReadOnly) return; //控件只读不处理  
            if (intMaxLength <= 0) return; //intMaxLength <= 0 不处理
            if (char.IsControl(e.KeyChar)) return; //Backspace, Enter...等控制键不处理      

            if (clsGlobalHospitalCode.Code == "0010")
            {
                int intTempLen = intTextLength + System.Text.Encoding.Default.GetBytes(e.KeyChar.ToString()).Length; //新长度    

                if (intTempLen - intSelectedTextLength > intMaxLength)
                {
                    e.Handled = true;
                    evtReachMaxLengthOld(this);
                }
            }
        }

        /// <summary>
        /// 是否在方法里
        /// </summary>
        private bool m_blnIn = false;
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
            if (m_blnIn)
            {
                return;
            }
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
                if (this is ctlAllergy)
                { }
                else
                {
                    if (!KeyChar && !CursorPositionToTerm_Midd(this.SelectionStart))
                    {
                        this.ImeMode = ImeMode.On;
                    }
                    else
                    {
                        if (clsGlobalSysParameter.dicSysParameter[65] == "1")
                        {
                            this.ImeMode = ImeMode.Off;
                        }
                    }
                }
            }
            else
            {
                if (this.m_blnCursorPositionToCaption(this.SelectionStart))
                {
                    m_blnIn = true;
                    this.SelectionLength = 0;
                    this.SelectionStart = this.m_strGetFirstlineCaption().Length;
                    this.m_intCurrentCursorIndex = this.SelectionStart;
                    //this.SelectionFont = this.Font;
                    this.SelectionCharOffset = 0;
                    m_blnIn = false;
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

                if (m_blnIsSelectedChanged)
                {
                    CheckModifySelection(this.m_lstTextContentInfos, this.m_objCurrentModifyUser, m_intSelectedTextStartIndex, m_intSelectedTextLength, ref m_blnCanModifySelection);
                }
            }

            intSelectedTextLength = System.Text.Encoding.Default.GetBytes(this.SelectedText).Length;
        }

        /// <summary>
        /// 修改者比较(摘自Common.Utility.dll并修改)
        /// </summary>
        /// <param name="p_objModifierA"></param>
        /// <param name="p_objModifierB"></param>
        /// <returns></returns>
        public bool CompareModifier(clsModifyUserInfo p_objModifierCur, clsModifyUserInfo p_objModifierOrg, DateTime dateNow, bool tableFlag, string dbColName, List<clsUserContentInfo> lstTextContentInfos, clsModifyUserInfo currentModifyUser)
        {
            bool flag = true;
            bool result;
            try
            {
                if (clsGlobalCase.objCaseInfo.intCaseStatus != 2)
                {
                    result = true;
                    return result;
                }
                if (p_objModifierCur == null || p_objModifierOrg == null)
                {
                    result = false;
                    return result;
                }
                bool flag2 = false;
                if (clsGlobalSysParameter.dicSysParameter.ContainsKey(74))
                {
                    flag2 = (clsGlobalSysParameter.dicSysParameter[74].ToString().Trim() == "1");
                }
                int num = 0;
                if (intModifyLimit != null)
                {
                    num = intModifyLimit.Value;
                }
                else if (clsGlobalSysParameter.dicSysParameter.ContainsKey(7))
                {
                    int.TryParse(clsGlobalSysParameter.dicSysParameter[7], out num);
                }
                if (p_objModifierOrg.m_dtmModifyDate.AddHours(double.Parse(num.ToString())) <= dateNow)
                {
                    if (flag2)
                    {
                        result = true;
                        return result;
                    }
                    result = false;
                    return result;
                }
                else
                {
                    if (p_objModifierCur.m_strUserID != p_objModifierOrg.m_strUserID)
                    {
                        result = false;
                        return result;
                    }
                    if (p_objModifierOrg.m_dtmModifyDate <= clsGlobalCase.objCaseInfo.dtmCreateDate)
                    {
                        if (!clsRichTextBoxTool.CompareModifier(true, tableFlag, dbColName, lstTextContentInfos, currentModifyUser))
                        {
                            result = false;
                            return result;
                        }
                    }
                    if (p_objModifierCur.m_strUserID == p_objModifierOrg.m_strUserID && p_objModifierCur.m_dtmModifyDate == p_objModifierOrg.m_dtmModifyDate)
                    {
                        result = true;
                        return result;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            result = flag;
            return result;
        }

        /// <summary>
        /// 用户选中了文本时,设置是否可以修改选择文本的标志(摘自Common.Utility.dll并修改)
        /// </summary>
        /// <param name="lstTextContentInfos"></param>
        /// <param name="currentModifyUser"></param>
        /// <param name="selectedTextStartIndex"></param>
        /// <param name="selectedTextLength"></param>
        /// <param name="canModifySelection"></param>
        public void CheckModifySelection(List<clsUserContentInfo> lstTextContentInfos, clsModifyUserInfo currentModifyUser,
            int selectedTextStartIndex, int selectedTextLength, ref bool canModifySelection)
        {
            int num = selectedTextStartIndex + selectedTextLength - 1;
            int intHour = 0;
            if (intModifyLimit != null)
            {
                intHour = intModifyLimit.Value;
            }
            else if (clsGlobalSysParameter.dicSysParameter.ContainsKey(7))
            {
                int.TryParse(clsGlobalSysParameter.dicSysParameter[7], out intHour);
            }
            DateTime dtmNow = clsMidderTime.s_dtmMidderTime();

            for (int i = 0; i < lstTextContentInfos.Count; i++)
            {
                clsUserContentInfo clsUserContentInfo = lstTextContentInfos[i];
                if (selectedTextStartIndex >= clsUserContentInfo.m_intStartIndex && selectedTextStartIndex <= clsUserContentInfo.m_intEndIndex)
                {
                    if (clsUserContentInfo.objUserInfo.m_strUserID != currentModifyUser.m_strUserID)
                    {
                        canModifySelection = false;
                        break;
                    }
                    else
                    {
                        if (clsUserContentInfo.m_dtmModifyDate.AddHours(double.Parse(intHour.ToString())) < dtmNow)
                        {
                            canModifySelection = false;
                            break;
                        }
                    }
                    if (num >= clsUserContentInfo.m_intStartIndex && num <= clsUserContentInfo.m_intEndIndex)
                    {
                        break;
                    }
                }
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
                clsMedicalTerm ele = null;
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
                    if (this is ctlAllergy) { }
                    else
                    {
                        if (!KeyChar && !CursorPositionToTerm_Midd(this.SelectionStart))
                        {
                            this.ImeMode = ImeMode.On;
                        }
                        else
                        {
                            this.ImeMode = ImeMode.Off;
                        }
                    }
                    //this.ReadOnly = false;
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

            SwitchIME();

            base.OnMouseDown(e);
        }

        /// <summary>
        /// 切换半角
        /// </summary>
        private void SwitchIME()
        {
            IntPtr HIme = ImmGetContext(this.Handle);
            if (ImmGetOpenStatus(HIme))     //如果输入法处于打开状态   
            {
                int iMode = 0;
                int iSentence = 0;
                bool bSuccess = ImmGetConversionStatus(HIme, ref   iMode, ref   iSentence);     //检索输入法信息   
                if (bSuccess)
                {
                    if ((iMode & IME_CMODE_FULLSHAPE) > 0)       //如果是全角   
                        ImmSimulateHotKey(this.Handle, IME_CHOTKEY_SHAPE_TOGGLE);     //转换成半角   
                }
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            SwitchIME();

            base.OnGotFocus(e);
        }

        #region 输入法API
        //声明一些API函数   
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr himc);
        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr himc, bool b);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr himc, ref   int lpdw, ref   int lpdw2);
        [DllImport("imm32.dll")]
        public static extern int ImmSimulateHotKey(IntPtr hwnd, int lngHotkey);
        private const int IME_CMODE_FULLSHAPE = 0x8;
        private const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;
        #endregion


        /*
        protected override void OnMouseHover(EventArgs e)
        {
            int intLen = 0;
            if (this.FirstlineCaption != null) intLen = this.FirstlineCaption.Length;
            int intCharIndex = this.GetCharIndexFromPosition(PointToClient(Control.MousePosition)) - intLen;
            clsICDInfo objICDInfo = m_lstICDInfo.FirstOrDefault(t => t.m_intStartIndex <= intCharIndex && t.m_intEndIndex >= intCharIndex);
            if (objICDInfo != null)
            {
                Point sPoint = this.GetPositionFromCharIndex(objICDInfo.m_intStartIndex + intLen);
                Point ePoint = this.GetPositionFromCharIndex(objICDInfo.m_intEndIndex + intLen + 1);
                Graphics g = this.CreateGraphics();
                g.DrawString(objICDInfo.m_strICDCode, new Font("宋体", 7, FontStyle.Italic), Brushes.Gray, sPoint);
                g.DrawLine(Pens.Gray, sPoint, new Point(sPoint.X, sPoint.Y + this.m_intRowSpacing));
                g.DrawLine(Pens.Gray, ePoint, new Point(ePoint.X, ePoint.Y + this.m_intRowSpacing));
            }

            base.OnMouseHover(e);
        }*/

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
            Cursor = Cursors.Default;

            base.OnMouseUp(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            this.ResetElementCurrColor();
            base.OnLeave(e);
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
                            this.SelectionStart = this.m_lstMedicalTerm[k].m_intStartIndex;
                            this.SelectionLength = this.m_lstMedicalTerm[k].m_strValue.Length;
                            if (this.m_lstMedicalTerm[k].m_strTID == clsRichTextBoxTool.OpCall)
                                this.SelectionColor = Color.Black;
                            else
                                this.SelectionColor = clsRichTextBoxTool.TermColor;
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

            string hintInfo = clsRichTextBoxTool.GetHintInfo(this.m_lstTextContentInfos, this.m_lstDoubleStrikeThrough, this.m_lstTextView, this.m_lstDDLView, e);
            if (!string.IsNullOrEmpty(hintInfo))
            {
                this.m_objTipArgs.ToolTip = hintInfo;
                this.m_objTipCtrl.ShowHint(this.m_objTipArgs);
            }
            else
            {
                this.m_objTipCtrl.HideHint();
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// OnMouseDoubleClick
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (this.MaskType == 3 || this.MaskType == 4 || this.MaskType == 5 || this.MaskType == 6)
            {
                autocompleteAssistant.TargetControlWrapper = autocompleteAssistant.FindWrapper(this);
                autocompleteAssistant.ShowAutocomplete(true);
            }

            if (clsGlobalPatient.objCurrentPatient == null)
            {
                clsDialog.Msg("请先选择病人。", MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
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
            Form frmParent = this.FindForm();
            try
            {
                if (frmParent is frmBaseMdiCase)
                {
                    if (e.Delta > 0)
                    {
                        ((frmBaseMdiCase)frmParent).m_objGetContainer().VerticalScroll.Value -= 10;
                    }
                    else if (e.Delta < 0)
                    {
                        ((frmBaseMdiCase)frmParent).m_objGetContainer().VerticalScroll.Value += 10;
                    }
                }
            }
            catch { }
            finally
            {
                frmParent = null;
            }
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

        public override bool PreProcessMessage(ref Message msg)
        {
            if (this.intConfineType == 2)
            {
                clsDialog.Msg("书写受限\r\n\r\n病人的年龄、性别受限。", MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
                return true;
            }
            return base.PreProcessMessage(ref msg);
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
            DateTime dtmFinishEdit = clsHelper.s_dtmMidderTime();

            //用户信息
            int intUserSequence = m_objCurrentModifyUser.m_intUserSequence;

            if (intUserSequence == -1)
            {
                intUserSequence = this.m_lstModifyUsers.Count + 1;
            }

            //更新双画线区域的时间及用户信息
            foreach (clsDSTInfo objDSTInfo in this.m_lstDoubleStrikeThrough)
            {
                if (objDSTInfo.m_dtmDeleteTime.Year == 1900)
                {
                    objDSTInfo.m_dtmDeleteTime = dtmFinishEdit;
                    objDSTInfo.m_intUserSequence = intUserSequence;
                }
            }

            //更新当前用户信息
            if (m_objCurrentModifyUser.m_intUserSequence == -1)
            {
                m_objCurrentModifyUser.m_intUserSequence = intUserSequence;
                m_objCurrentModifyUser.m_dtmModifyDate = dtmFinishEdit;
                m_objCurrentModifyUser.m_clrText = m_clrOldPartInsertText;

                this.m_lstModifyUsers.Add(m_objCurrentModifyUser);
            }

            if (this.Text.Length > 0 && this.m_lstTextContentInfos.Count == 0)
            {
                clsUserContentInfo objContentInfo = new clsUserContentInfo();

                objContentInfo.objUserInfo = m_objCurrentModifyUser;
                objContentInfo.m_intStartIndex = 0;
                objContentInfo.m_intEndIndex = this.Text.Length - 1;
                objContentInfo.m_strUserID = m_objCurrentModifyUser.m_strUserID;
                objContentInfo.m_strUserName = m_objCurrentModifyUser.m_strUserName;
                objContentInfo.m_dtmModifyDate = clsMidderTime.s_dtmMidderTime();
                objContentInfo.m_clrText = this.m_clrDefaultViewText;

                this.m_lstTextContentInfos.Add(objContentInfo);
            }
            this.m_mthResetPoint();

            if (!this.ValueChangedFlag) return;
            this.ValueChangedFlag = false;
        }

        private void m_mthResetPoint()
        {
            bool blnCheck = false;
            foreach (clsMedicalTerm objElement in this.m_lstMedicalTerm)
            {
                if (string.IsNullOrEmpty(objElement.m_strUserID))
                {
                    objElement.m_strUserID = this.m_objCurrentModifyUser.m_strUserID;
                    objElement.m_strUserName = this.m_objCurrentModifyUser.m_strUserName;
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
        public void m_mthClearText()
        {
            this.m_blnCanTextChanged = false;
            //if (this.Text != string.Empty) this.ValueChangedFlag = true;
            this.Text = string.Empty;

            this.m_lstDoubleStrikeThrough.Clear();
            this.m_lstModifyUsers.Clear();
            this.m_lstTextContentInfos.Clear();
            this.m_lstMedicalTerm.Clear();
            this.m_lstImageInfo.Clear();
            this.m_lstICDInfo.Clear();

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
            this.intTextLength = 0;

            try
            {
                this.m_mthSetFirstlineCaption();
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
            //clsRichTextBoxTool.StopRedraw(this.Handle);
            clsRichTextBoxPlus.InsertText(this, "");
            //this.SelectionLength = 0;
            //clsRichTextBoxTool.Redraw(this.Handle);
            //this.Focus();
        }
        #endregion

        #region 获取正确的文本

        /// <summary>
        /// 获取正确的文本
        /// </summary>
        /// <returns></returns>
        public string m_strGetText()
        {
            string strText = m_strGetRightText();
            if (this.FirstlineCaption != null && strText.StartsWith(this.FirstlineCaption))
            {
                strText = strText.Substring(this.FirstlineCaption.Length);
            }

            return strText;
        }

        /// <summary>
        /// 获取正确的文本
        /// </summary>
        /// <returns></returns>
        public string m_strGetRightText()
        {
            return clsRichTextBoxTool.GetRightText(this.m_lstDoubleStrikeThrough, this.Text);
        }
        /// <summary>
        /// 重载,当判断com.digitalwave.Controls.ctlRichTextBox内容是否改变时用
        /// </summary>
        /// <param name="p_blnCheckStatus"></param>
        /// <returns></returns>
        public string m_strGetRightText(bool p_blnCheckStatus)
        {
            if (!p_blnCheckStatus)
                m_mthFinishEdit();

            return m_strGetRightText();
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

            return clsRichTextBoxTool.GetXmlOfDST(this.m_lstDoubleStrikeThrough);
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

            return clsRichTextBoxTool.GetXmlOfTerm(this.m_lstMedicalTerm);
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

            clsRichTextBoxTool.StopRedraw(this.Handle);
            int intStartIndex = this.SelectionStart;
            int intEndIndex = intStartIndex + this.SelectionLength;
            var lstICDInfo = m_lstICDInfo.FindAll(t => !((t.m_intStartIndex + FirstlineCaptionLen > intEndIndex) || (t.m_intEndIndex + FirstlineCaptionLen < intStartIndex)));
            if (lstICDInfo.Count > 0)
            {
                intStartIndex = Math.Min(lstICDInfo.Min((obj) => { return obj.m_intStartIndex; }) + FirstlineCaptionLen, intStartIndex);
                intEndIndex = Math.Max(lstICDInfo.Max((obj) => { return obj.m_intEndIndex; }) + FirstlineCaptionLen + 1, intEndIndex);
                foreach (var obj in lstICDInfo)
                {
                    obj.m_intFlags = -1 - obj.m_intFlags;
                }
            }

            this.m_intPreviouslyLen = this.TextLength + 1;
            this.SelectionLength = 0;
            this.SelectionStart = intStartIndex;
            m_blnDoubleLine = true;
            this.m_mthInsertText(clsRichTextBoxTool.DSTPrefix);
            m_blnDoubleLine = false;
            intEndIndex += 1;

            clsDSTInfo objDST = new clsDSTInfo();
            objDST.m_intStartIndex = intStartIndex;
            objDST.m_intEndIndex = intEndIndex - 1;
            objDST.m_clrDST = Color.Red;
            objDST.m_strUserID = m_objCurrentModifyUser.m_strUserID;
            objDST.m_strUserName = m_objCurrentModifyUser.m_strUserName;
            objDST.m_strValue = this.Text.Substring(intStartIndex, intEndIndex - intStartIndex);
            objDST.m_dtmDeleteTime = clsHelper.s_dtmMidderTime();
            this.m_lstDoubleStrikeThrough.Add(objDST);

            m_mthModifyIndex(intStartIndex, 1);

            this.SelectionStart = intStartIndex;
            this.SelectionLength = 0;
            clsRichTextBoxTool.Redraw(this.Handle);
            this.m_mthFireEvtTextChange(false);
            this.ValueChangedFlag = true;
            this.Invalidate();
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
            m_blnDoubleLine = true;
            foreach (clsDSTInfo obj in this.m_lstDoubleStrikeThrough)
            {
                if (obj.m_intStartIndex <= intCurrIndex && intCurrIndex <= obj.m_intEndIndex)
                {
                    if (obj.m_strUserID != m_objCurrentModifyUser.m_strUserID) return;
                    if (clsGlobalCase.objCaseInfo.intCaseStatus == 2)
                    {
                        intEmpID = Convert.ToInt32(obj.m_strUserID);
                        if (clsGlobalCase.objCaseInfo.lstSignature.Any(t => t.dtmSignature >= obj.m_dtmDeleteTime && t.intEmpID == intEmpID))
                        {
                            return;
                        }
                    }
                    clsDSTInfo objTmp = new clsDSTInfo();
                    objTmp.m_intStartIndex = obj.m_intStartIndex;
                    objTmp.m_intEndIndex = obj.m_intEndIndex;
                    this.m_lstDoubleStrikeThrough.Remove(obj);
                    var lstICDInfo = m_lstICDInfo.FindAll(t => (t.m_intStartIndex + FirstlineCaptionLen >= objTmp.m_intStartIndex) && (t.m_intEndIndex + FirstlineCaptionLen <= objTmp.m_intEndIndex) && t.m_intFlags < 0);
                    foreach (var objtemp in lstICDInfo)
                    {
                        objtemp.m_intFlags = -1 * (objtemp.m_intFlags + 1);
                    }


                    clsRichTextBoxTool.StopRedraw(this.Handle);
                    this.Select(objTmp.m_intStartIndex, 1);
                    clsRichTextBoxPlus.InsertText(this, string.Empty);
                    m_mthModifyIndex(objTmp.m_intStartIndex, -1);
                    clsRichTextBoxTool.Redraw(this.Handle);
                    break;
                }
            }
            m_blnDoubleLine = false;
            this.Invalidate();
        }

        /// <summary>
        /// 修改所有涉及标识位置的地方，包括双划线，用户修改范围信息，医学术语，图片
        /// </summary>
        /// <param name="p_intModifyIndex">变化的位置</param>
        /// <param name="p_intReduction">增量</param>
        private void m_mthModifyIndex(int p_intModifyIndex, int p_intReduction)
        {
            var lstDSTInfo = m_lstDoubleStrikeThrough.FindAll(t => t.m_intStartIndex > p_intModifyIndex);
            if (lstDSTInfo.Count > 0)
            {
                foreach (clsDSTInfo obj in lstDSTInfo)
                {
                    obj.m_intStartIndex = obj.m_intStartIndex + p_intReduction;
                    obj.m_intEndIndex = obj.m_intEndIndex + p_intReduction;
                }
            }

            var lstTextContentInfos = m_lstTextContentInfos.FindAll(t => t.m_intStartIndex > p_intModifyIndex);
            if (lstTextContentInfos.Count > 0)
            {
                foreach (clsUserContentInfo obj in lstTextContentInfos)
                {
                    obj.m_intStartIndex = obj.m_intStartIndex + p_intReduction;
                    obj.m_intEndIndex = obj.m_intEndIndex + p_intReduction;
                }
            }

            var lstMedicalTerm = m_lstMedicalTerm.FindAll(t => t.m_intStartIndex > p_intModifyIndex);
            if (lstMedicalTerm.Count > 0)
            {
                foreach (clsMedicalTerm obj in lstMedicalTerm)
                {
                    obj.m_intStartIndex = obj.m_intStartIndex + p_intReduction;
                    obj.m_intEndIndex = obj.m_intEndIndex + p_intReduction;
                }
            }

            var lstImageInfo = m_lstImageInfo.FindAll(t => t.m_intStartIndex > p_intModifyIndex);
            if (lstImageInfo.Count > 0)
            {
                foreach (clsImageInfo obj in lstImageInfo)
                {
                    obj.m_intStartIndex = obj.m_intStartIndex + p_intReduction;
                    obj.m_intEndIndex = obj.m_intEndIndex + p_intReduction;
                }
            }

            var lstICDInfo = m_lstICDInfo.FindAll(t => t.m_intStartIndex > p_intModifyIndex);
            if (lstICDInfo.Count > 0)
            {
                foreach (clsICDInfo obj in lstICDInfo)
                {
                    obj.m_intStartIndex = obj.m_intStartIndex + p_intReduction;
                    obj.m_intEndIndex = obj.m_intEndIndex + p_intReduction;
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
                foreach (clsDSTInfo obj in this.m_lstDoubleStrikeThrough)
                {
                    if (obj.m_intStartIndex <= p_intIndex && p_intIndex <= obj.m_intEndIndex)
                    {
                        blnRet = true;
                        break;
                    }
                }
            }
            else if (p_objKey == Keys.Back)
            {
                foreach (clsDSTInfo obj in this.m_lstDoubleStrikeThrough)
                {
                    if (obj.m_intStartIndex < p_intIndex && p_intIndex <= obj.m_intEndIndex + 1)
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
                foreach (clsDSTInfo obj in this.m_lstDoubleStrikeThrough)
                {
                    if ((obj.m_intStartIndex < intStartIndex && intStartIndex <= obj.m_intEndIndex) ||
                        (obj.m_intStartIndex > intStartIndex && obj.m_intStartIndex < intEndIndex) ||
                        (obj.m_intEndIndex > intStartIndex && obj.m_intEndIndex <= intEndIndex))
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
            foreach (clsDSTInfo obj in this.m_lstDoubleStrikeThrough)
            {
                if ((obj.m_intStartIndex < intStartIndex && intStartIndex <= obj.m_intEndIndex) ||
                    (obj.m_intStartIndex > intStartIndex && obj.m_intStartIndex < intEndIndex) ||
                    (obj.m_intEndIndex > intStartIndex && obj.m_intEndIndex <= intEndIndex) ||
                    (obj.m_intStartIndex <= intStartIndex && intEndIndex <= obj.m_intEndIndex))
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
            foreach (clsDSTInfo obj in this.m_lstDoubleStrikeThrough)
            {
                if (obj.m_intStartIndex < p_intIndex && p_intIndex <= obj.m_intEndIndex)
                {
                    blnRet = true;
                    break;
                }
            }
            return blnRet;
        }
        #endregion

        #region 调整元素.词汇位置

        //private void AdjustElementPosition_DeleteBath(int p_intIndex, int p_intDiffLength)
        //{
        //    if (!IsAllowElementFreeEdit) return;
        //    if (this.m_lstMedicalTerm.Count == 0) return;
        //    if (p_intDiffLength == 0) return;

        //    clsRichTextBoxTool.AdjustElementPosition_DeleteBath(p_intIndex, p_intDiffLength, this.Text, ref this.m_lstMedicalTerm);
        //}

        ///// <summary>
        ///// 调整元素.词汇位置---Delete
        ///// </summary>
        ///// <param name="p_intIndex"></param>
        ///// <param name="p_intDiffLength"></param>
        //private void AdjustElementPosition_Delete(int p_intIndex, int p_intDiffLength)
        //{
        //    if (!IsAllowElementFreeEdit) return;

        //    clsMedicalTerm objElement = CursorPositionToTerm_Delete(p_intIndex);
        //    if (objElement != null)
        //    {
        //        objElement.m_intEndIndex -= p_intDiffLength;

        //        if (!clsRichTextBoxTool.RemoveElement(objElement, ref this.m_lstMedicalTerm))
        //            objElement.m_strValue = this.Text.Substring(objElement.m_intStartIndex, objElement.m_intEndIndex - objElement.m_intStartIndex + 1);
        //    }
        //}

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
                    clsDialog.Msg("删除内容不能设置字体属性。", MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
                    return;
                }
                else if (p_intFlag == 0 && !m_blnCanModifyColor())
                {
                    clsDialog.Msg("选定的内容不能设置字体颜色。", MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
                    return;
                }
                else
                {
                    if (CheckOpCallDealIdeaElement()) // this.m_blnCursorPositionToTerm())
                    {
                        clsDialog.Msg("医学术语不能设置字体属性。", MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
                        return;
                    }

                    if (this.m_blnCursorPositionToImage() || this.m_blnCursorPositionToCaption())
                    {
                        return;
                    }
                }
                //Font currentFont = this.SelectionFont;
                FontStyle newFontStyle = this.SelectionFont.Style;

                using (RichTextBox rtx = new RichTextBox())
                {
                    rtx.Rtf = this.SelectedRtf;
                    rtx.SelectAll();

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
                    int intPos = rtx.SelectionStart;
                    int myLength = rtx.SelectionLength;

                    for (int i = intPos; i < rtx.Text.Length; i++)
                    {
                        rtx.SelectionStart = i;
                        rtx.SelectionLength = 1;

                        rtx.SelectionFont = new Font(rtx.SelectionFont.FontFamily, rtx.SelectionFont.Size, newFontStyle);
                    }
                    rtx.Select(0, rtx.Text.Length);
                    this.SelectedRtf = rtx.SelectedRtf;
                }
            }
        }
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
        private void m_mthAddElement(string p_strCaseCode, string p_strElementID, string p_strElmentName, clsDragRichItem dragRichItem, bool p_blnReplaceFlag)
        {
            clsRichTextBoxTool.StopRedraw(this.Handle);
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

            string strUserID = m_objCurrentModifyUser.m_strUserID;
            string struserName = m_objCurrentModifyUser.m_strUserName;
            if (this.FormTypeName == clsRichTextBoxTool.TemplateFormClass)
            {
                strUserID = "TempUser";
                struserName = "TempUser";
            }

            clsMedicalTerm objMedicalTerm = new clsMedicalTerm();
            objMedicalTerm.m_intStartIndex = intStartIndex;
            objMedicalTerm.m_intEndIndex = intStartIndex + p_strElmentName.Length - 1;
            objMedicalTerm.m_clrTerm = clsRichTextBoxTool.TermColor;
            objMedicalTerm.m_strUserID = strUserID;
            objMedicalTerm.m_strUserName = struserName;
            objMedicalTerm.m_dtmCreateTime = DateTime.Now;
            objMedicalTerm.m_intType = 0;
            if (string.IsNullOrEmpty(p_strCaseCode))
                objMedicalTerm.m_strCaseCode = clsGlobalCase.objCaseInfo.strCaseCode;
            else
                objMedicalTerm.m_strCaseCode = p_strCaseCode;
            objMedicalTerm.m_strTID = p_strElementID;
            objMedicalTerm.m_strValue = p_strElmentName;
            this.m_lstMedicalTerm.Add(objMedicalTerm);
            this.m_mthSetMedicalTermColor(0);
            this.m_mthFireEvtTextChange(false);
            this.SelectionStart = objMedicalTerm.m_intEndIndex + 1;
            this.SelectionLength = 0;
            clsRichTextBoxTool.AdjustTermPosition(ref this.m_lstMedicalTerm, this.FntElement, this);
            clsRichTextBoxTool.Redraw(this.Handle);
            this.Invalidate();

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
            clsDragRichItem dragRichItem = null;
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
            clsDragRichItem dragRichItem = null;
            frmLoadElement frm = new frmLoadElement();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.TemplateFlag = this.TemplateFlag;
            if (frm.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                strElementID = frm.ElementID;
                strElementName = frm.ElementName;
                dragRichItem = frm.DragRichItem;
            }
            else
                return;

            this.m_mthAddElement(string.Empty, strElementID, strElementName, dragRichItem, false);
            this.Invalidate();
        }

        /// <summary>
        /// 调用常用医学术语
        /// </summary>
        /// <param name="p_strElementID"></param>
        /// <param name="p_strElementName"></param>
        private void m_mthInsertMedicalTerm(string p_strCaseCode, string p_strElementID, string p_strElementName, clsDragRichItem dragRichItem, bool p_blnReplaceFlag)
        {
            this.m_mthAddElement(p_strCaseCode, p_strElementID, p_strElementName, dragRichItem, p_blnReplaceFlag);
        }

        /// <summary>
        /// 添加元素(智能引用)
        /// </summary>
        /// <param name="p_objRefItem"></param>
        private void m_mthAddElement(clsIntellectiveRefItem p_objRefItem)
        {
            if (p_objRefItem == null) return;

            string strElementID = "Intellection;" + p_objRefItem.strCaseCode + ";" + p_objRefItem.strColCode;
            string strElementName = clsHelper.s_strCaseColumnContent(p_objRefItem.strCaseCode, p_objRefItem.strColCode, p_objRefItem.strColName);

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
                this.m_mthInsertElement(clsRichTextBoxTool.OpCall, p_strContent);
            }
        }

        /// <summary>
        /// 插入元素
        /// </summary>
        /// <param name="strElementID"></param>
        /// <param name="strElementName"></param>
        public void m_mthInsertElement(string strElementID, string strElementName)
        {
            clsRichTextBoxTool.StopRedraw(this.Handle);

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
            strElementName = clsRichTextBoxTool.TermPrefix + strElementName.Trim();
            this.m_mthInsertText(strElementName);
            //this.SelectionStart = intStartIndex;
            //this.SelectionLength = strElementName.Length;
            //this.SelectionFont = FntElement;
            //this.SelectionColor = m_clrTerm;//Color.Red;
            this.IsInsertNorElement = false;
            this.SelectionLength = 0;

            clsMedicalTerm objMedicalTerm = new clsMedicalTerm();
            objMedicalTerm.m_intStartIndex = intStartIndex;
            objMedicalTerm.m_intEndIndex = intStartIndex + strElementName.Length - 1;
            objMedicalTerm.m_clrTerm = clsRichTextBoxTool.TermColor;
            objMedicalTerm.m_strUserID = m_objCurrentModifyUser.m_strUserID;
            objMedicalTerm.m_strUserName = m_objCurrentModifyUser.m_strUserName;
            objMedicalTerm.m_dtmCreateTime = DateTime.Now;
            objMedicalTerm.m_intType = 0;
            objMedicalTerm.m_strTID = strElementID;
            objMedicalTerm.m_strValue = strElementName;
            this.m_lstMedicalTerm.Add(objMedicalTerm);
            this.m_mthSetMedicalTermColor(0);
            this.m_mthFireEvtTextChange(false);
            this.SelectionStart = objMedicalTerm.m_intEndIndex + 1;
            this.SelectionLength = 0;
            clsRichTextBoxTool.Redraw(this.Handle);
        }

        private void m_mthSetMedicalTermColor(int p_intType)
        {
            int intCurrIndex = this.SelectionStart;
            string rtf = clsRichTextBoxTool.SetMedicalTermColor(p_intType, FntElement, this, this.m_lstMedicalTerm);
            if (rtf.Trim() != string.Empty)
            {
                IsReplaceRtf = true;
                this.Rtf = rtf;
                this.intTextLength = System.Text.Encoding.Default.GetBytes(this.m_strGetRightText()).Length;
                IsReplaceRtf = false;
                this.SelectionLength = 0;
                this.SelectionStart = intCurrIndex;
            }
        }
        #endregion

        #region 检查选择的内容是否不能修改颜色的
        private bool m_blnCanModifyColor()
        {
            bool blnResult = true;
            if (clsGlobalHospitalCode.Code == "0010")
            {
                int intSelectEnd = this.SelectionStart + this.SelectionLength;
                foreach (clsUserContentInfo objContentInfo in this.m_lstTextContentInfos)
                {
                    int intContentEndIndex = objContentInfo.m_intEndIndex + 1;
                    if ((m_blnBetween(this.SelectionStart, objContentInfo.m_intStartIndex, intContentEndIndex)
                        && this.SelectionStart != intContentEndIndex) ||
                        (m_blnBetween(intSelectEnd, objContentInfo.m_intStartIndex, intContentEndIndex)
                        && intSelectEnd != objContentInfo.m_intStartIndex) ||
                        (this.SelectionStart < objContentInfo.m_intStartIndex && intSelectEnd > intContentEndIndex))
                    {
                        if (objContentInfo.m_clrText.ToArgb() == Color.Red.ToArgb())
                        {
                            blnResult = false;
                            break;
                        }
                    }
                }
            }

            return blnResult;
        }

        private bool m_blnBetween(int p_intCurrent, int p_intStart, int p_intEnd)
        {
            if (p_intCurrent >= p_intStart && p_intCurrent <= p_intEnd)
            {
                return true;
            }
            return false;
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
            foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if ((obj.m_intStartIndex < intStartIndex && intStartIndex <= obj.m_intEndIndex) ||
                    (obj.m_intStartIndex > intStartIndex && obj.m_intStartIndex < intEndIndex) ||
                    (obj.m_intEndIndex > intStartIndex && obj.m_intEndIndex <= intEndIndex) ||
                    (obj.m_intStartIndex <= intStartIndex && intEndIndex <= obj.m_intEndIndex) ||
                    (obj.m_intEndIndex >= intStartIndex && obj.m_intEndIndex < intEndIndex) ||
                    (obj.m_intStartIndex <= intStartIndex && obj.m_intEndIndex <= intEndIndex && obj.m_intEndIndex >= intStartIndex))
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
                        if (obj.m_intStartIndex == intStartIndex)
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
                foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
                {
                    if (obj.m_intStartIndex <= p_intIndex && p_intIndex <= obj.m_intEndIndex)
                    {
                        blnRet = true;
                        break;
                    }
                }
            }
            else if (p_objKey == Keys.Back)
            {
                foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
                {
                    if (obj.m_intStartIndex < p_intIndex && p_intIndex <= obj.m_intEndIndex + 1)
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
                foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
                {
                    if (obj.m_intStartIndex <= p_intIndex && p_intIndex <= obj.m_intEndIndex && obj.m_strTID.ToLower() == clsRichTextBoxTool.OpCall)
                    {
                        blnRet = true;
                        break;
                    }
                }
            }
            else if (p_objKey == Keys.Back)
            {
                foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
                {
                    if (obj.m_intStartIndex < p_intIndex && p_intIndex <= obj.m_intEndIndex + 1 && obj.m_strTID.ToLower() == clsRichTextBoxTool.OpCall)
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
        private bool m_blnCursorPositionToTerm2(int p_intIndex, ref clsMedicalTerm p_objTerm)
        {
            bool blnRet = false;
            p_objTerm = null;
            foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.m_intStartIndex < p_intIndex && p_intIndex <= obj.m_intEndIndex)
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

        private clsMedicalTerm CursorPositionToTerm(int index)
        {
            foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.m_intStartIndex < index && index <= obj.m_intEndIndex)
                {
                    return obj;
                }
            }
            return null;
        }

        private clsMedicalTerm CursorPositionToTerm_Delete(int index)
        {
            foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.m_intStartIndex <= index && index <= obj.m_intEndIndex)
                {
                    return obj;
                }
            }
            return null;
        }

        private bool CursorPositionToTerm_Midd(int index)
        {
            if (this.m_lstMedicalTerm != null)
            {
                foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
                {
                    if (obj.m_intStartIndex < index - 1 && index <= obj.m_intEndIndex)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckEditElementPosition()
        {
            foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.m_intStartIndex <= this.SelectionStart && this.SelectionStart <= obj.m_intEndIndex)
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
            foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.m_intStartIndex < p_intIndex && p_intIndex <= obj.m_intEndIndex)
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
            foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.m_intStartIndex < p_intIndex && p_intIndex <= obj.m_intEndIndex && obj.m_strTID.ToLower() == clsRichTextBoxTool.OpCall)
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
            foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if ((obj.m_intStartIndex < p_intStart && p_intStart <= obj.m_intEndIndex) ||
                    (obj.m_intStartIndex < p_intEnd && p_intEnd <= obj.m_intEndIndex))
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

        #region 设置医学术语
        /// <summary>
        /// 设置医学术语
        /// </summary>
        public void m_mthSetMedicalTerm()
        {
            if (!this.m_blnCheckCursorValidPostion()) return;
            if (this.SelectionLength == 0) return;

            clsRichTextBoxTool.StopRedraw(this.Handle);
            int intStartIndex = this.SelectionStart;
            int intEndIndex = intStartIndex + this.SelectionLength;
            //this.SelectionLength = 0;
            //this.SelectionStart = intEndIndex;
            //this.m_mthInsertText("]");
            //this.SelectionStart = intStartIndex;
            //this.m_mthInsertText(this.m_strTermPrefix + "[");
            //intEndIndex += Convert.ToString(this.m_strTermPrefix + "[]").Length;

            clsMedicalTerm objMedicalTerm = new clsMedicalTerm();
            objMedicalTerm.m_intStartIndex = intStartIndex;
            objMedicalTerm.m_intEndIndex = intEndIndex - 1;
            objMedicalTerm.m_strUserID = m_objCurrentModifyUser.m_strUserID;
            objMedicalTerm.m_strUserName = m_objCurrentModifyUser.m_strUserName;
            objMedicalTerm.m_dtmCreateTime = DateTime.Now;
            objMedicalTerm.m_clrTerm = clsRichTextBoxTool.TermColor;
            objMedicalTerm.m_strValue = this.Text.Substring(intStartIndex, intEndIndex - intStartIndex);
            this.m_lstMedicalTerm.Add(objMedicalTerm);

            this.m_mthSetMedicalTermColor(0);
            this.m_mthFireEvtTextChange(false);
            this.SelectionStart = intStartIndex;
            this.SelectionLength = 0;
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
            foreach (clsMedicalTerm obj in this.m_lstMedicalTerm)
            {
                if (obj.m_intStartIndex < intCurrIndex && intCurrIndex <= obj.m_intEndIndex)
                {
                    clsMedicalTerm objTmp = new clsMedicalTerm();
                    objTmp.m_intStartIndex = obj.m_intStartIndex;
                    objTmp.m_intEndIndex = obj.m_intEndIndex;
                    this.m_lstMedicalTerm.Remove(obj);

                    this.SelectionStart = objTmp.m_intStartIndex;
                    this.SelectionLength = objTmp.m_intEndIndex - objTmp.m_intStartIndex + 1;
                    this.SelectionColor = Color.Black;

                    this.SelectionStart = objTmp.m_intEndIndex;
                    this.SelectionLength = 1;
                    this.m_mthDel();

                    this.SelectionStart = objTmp.m_intStartIndex;
                    if (clsRichTextBoxTool.TermPrefix == string.Empty)
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
        public void m_mthEditImage(clsImageInfo p_objImageInfo, Image p_objImage)
        {
            clsRichTextBoxTool.StopRedraw(this.Handle);
            this.SelectionStart = p_objImageInfo.m_intStartIndex;
            this.SelectionLength = 1;
            clsRichTextBoxPlus.InsertImage(this, p_objImage);
            clsRichTextBoxTool.Redraw(this.Handle);
            this.Invalidate();
            this.ValueChangedFlag = true;
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="p_objImage"></param>
        public void m_mthInsertImage(Image p_objImage)
        {
            //缩放
            if (this.FixedHeight)
            {
                double dblWidthFactor = 1;
                double dblHeightFactor = 1;
                if (p_objImage.Width > this.Width)
                {
                    dblWidthFactor = this.Width * 1.0d / p_objImage.Width;
                }
                if (p_objImage.Height > this.Height)
                {
                    dblHeightFactor = this.Height * 1.0d / p_objImage.Height;
                }
                double dblFactor = Math.Min(dblWidthFactor, dblHeightFactor);

                Bitmap bmpDest = new Bitmap((int)Math.Round((double)(p_objImage.Width * dblFactor)) - 4, (int)Math.Round((double)(p_objImage.Height * dblFactor)) - 4);
                using (Graphics g = Graphics.FromImage(bmpDest))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawImage(p_objImage, 0, 0, (int)(bmpDest.Width + 1), (int)(bmpDest.Height + 1));
                }

                byte[] bytArr = clsFunction.s_bytConvertImageToByte(bmpDest, 0);
                this.m_mthInsertImage("noknow", bmpDest, bytArr);
            }
            else
            {
                byte[] bytArr = clsFunction.s_bytConvertImageToByte(p_objImage, 0);
                this.m_mthInsertImage("noknow", p_objImage, bytArr);
            }

            this.Invalidate();
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        public void m_mthInsertImage(string p_strFile)
        {
            Image img = Image.FromFile(p_strFile);
            byte[] bytArr = clsFunction.s_bytConvertImageToByte(img, 0);
            this.m_mthInsertImage("noknow", img, bytArr);
        }
        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="p_strImageID"></param>
        /// <param name="p_btyObjectArr"></param>
        public void m_mthInsertImage(string p_strImageID, byte[] p_bytObjectArr)
        {
            Image img = clsFunction.s_imgConvertByteToImage(p_bytObjectArr);
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
                clsRichTextBoxTool.StopRedraw(this.Handle);
                clsRichTextBoxPlus.InsertImage(this, p_objImage);
                clsRichTextBoxTool.Redraw(this.Handle);
                this.ValueChangedFlag = true;
            }
            catch (Exception objEx)
            {
                clsDialog.Msg("插入图片异常:\r\n" + objEx.Message, MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
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

            clsRichTextBoxPlus.InsertText(this, p_strText);
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
        public void ColumnReference(string p_strText, List<clsMedicalTerm> p_lstElement)
        {
            this.Focus();
            try
            {
                clsRichTextBoxTool.StopRedraw(this.Handle);
                if (this.Text.Trim() != string.Empty)
                {
                    this.m_mthClearText();
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
                clsRichTextBoxTool.Redraw(this.Handle);
                this.Parent.Invalidate();
            }
        }

        /// <summary>
        /// 引用插入
        /// </summary>
        /// <param name="p_strText"></param>
        /// <param name="p_lstElement"></param>
        public void m_mthReferenceInsert(string p_strText, List<clsMedicalTerm> p_lstElement)
        {
            this.Focus();
            clsRichTextBoxTool.StopRedraw(this.Handle);
            this.SelectionStart = this.Text.Length;
            this.SelectionLength = 0;
            this.m_intCurrentCursorIndex = this.SelectionStart;

            string strFirstlineCaption = this.m_strGetFirstlineCaption();
            if (!string.IsNullOrEmpty(strFirstlineCaption) && p_strText.StartsWith(strFirstlineCaption))
            {
                p_strText = p_strText.Replace(strFirstlineCaption, "");
            }
            this.m_mthInsertText(p_strText);

            DateTime dtmNow = clsMidderTime.s_dtmMidderTime();
            foreach (clsMedicalTerm obj in p_lstElement)
            {
                if (string.IsNullOrEmpty(obj.m_strUserID))
                {
                    obj.m_strUserID = m_objCurrentModifyUser.m_strUserID;
                    obj.m_strUserName = m_objCurrentModifyUser.m_strUserName;
                    obj.m_dtmCreateTime = dtmNow;
                }
            }
            this.m_mthSetMedicalTerm(p_lstElement);
            clsRichTextBoxTool.Redraw(this.Handle);
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
            clsRichTextBoxPlus.InsertText(this, p_strChr);
            this.Select(idx, p_strChr.Length);
            this.SelectionFont = new Font("宋体", 18, FontStyle.Regular);
            this.Select(idx, p_strChr.Length);
            clsRichTextBoxPlus.InsertText(this, "");
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
                        clsRichTextBoxPlus.GetImageIndex(this, 0, this.Text.Length, ref lstImageIdx);
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
            //if (this.SelectionLength > 0) this.SelectionLength = 0;

            if (blnDiagFlgas)//诊断控件的粘贴特殊处理
            {
                clsCopyICDInfo objICDInfo = null;
                object obj = Clipboard.GetData(clsGlobalPatient.objCurrentPatient.strPatientID + "ICDINFO");
                if (obj == null)
                {
                    obj = Clipboard.GetData("ICDINFO");
                }

                if (obj != null)
                {
                    objICDInfo = obj as clsCopyICDInfo;
                }

                if (objICDInfo != null)
                {
                    this.m_mthClearText();
                    this.m_lstICDInfo = objICDInfo.lstICDInfo;
                    this.m_mthInsertText(objICDInfo.strText);
                }
            }
            else
            {
                string strContent = string.Empty;
                if (clsGlobalSysParameter.dicSysParameter.ContainsKey(87)
                    && clsGlobalSysParameter.dicSysParameter[87] == "1")
                {
                    strContent = Clipboard.GetText(TextDataFormat.UnicodeText);
                }
                else
                {
                    if (clsGlobalHospitalCode.Code == "0008")
                    {
                        string strTempContent = Clipboard.GetText(TextDataFormat.UnicodeText);
                        if (strTempContent.StartsWith("[ehr]||"))
                        {
                            int pos = strTempContent.IndexOf(this.m_strSysSplit, 7);
                            if (pos > 0)
                            {
                                string strPatId = string.Empty;
                                if (clsGlobalPatient.objCurrentPatient != null)
                                {
                                    strPatId = clsGlobalPatient.objCurrentPatient.strPatientID;
                                }
                                if (strPatId == strTempContent.Substring(7, pos - 7) || strTempContent.Substring(7, pos - 7) == "nopatient")
                                {
                                    strContent = strTempContent.Substring(pos + 2);
                                }
                            }
                        }
                    }
                    else
                    {
                        object obj = Clipboard.GetData(clsGlobalPatient.objCurrentPatient.strPatientID);
                        if (obj == null)
                        {
                            strContent = Clipboard.GetText(TextDataFormat.UnicodeText);
                        }
                        else
                        {
                            strContent = obj.ToString();
                        }
                    }
                }

                if (clsGlobalHospitalCode.Code == "0007" || clsGlobalHospitalCode.Code == "0010")
                {
                    Image img = Clipboard.GetImage();
                    if (img != null)
                    {
                        m_mthInsertImage(img);
                    }
                }

                if (string.IsNullOrEmpty(strContent)) return;

                strContent = strContent.Replace("[", "").Replace("]", "");

                if (clsGlobalHospitalCode.Code != "0007" && clsGlobalHospitalCode.Code != "0013")//暂时针对新会处理，功能成熟后再放开
                {
                    if (MaxLength > 0)// && evtReachMaxLength != null)
                    {
                        if (System.Text.Encoding.Default.GetBytes(this.Text).Length + System.Text.Encoding.Default.GetBytes(strContent).Length > this.MaxLength)
                        {
                            clsDialog.Msg("文本长度大于允许的最大的长度。", MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
                            return;
                        }
                    }
                }

                if (this.SelectionLength > 0)//有选中文字
                {
                    if (!m_blnCanPasteOver())
                        this.SelectionLength = 0;
                }
                this.m_mthInsertText(strContent);
                this.m_mthSetMedicalTermColor(0);
            }
        }

        /// <summary>
        /// 是否可以粘贴覆盖
        /// </summary>
        /// <returns></returns>
        private bool m_blnCanPasteOver()
        {
            if (this.IsAutoSignature == 0)//需要痕迹控制
            {
                #region 删除控制
                foreach (clsUserContentInfo objContentInfo in this.m_lstTextContentInfos)
                {
                    if (objContentInfo.m_intStartIndex <= m_intCurrentCursorIndex && m_intCurrentCursorIndex <= objContentInfo.m_intEndIndex)
                    {
                        if (!CompareModifier(m_objCurrentModifyUser, objContentInfo.objUserInfo, clsHelper.s_dtmMidderTime(),
                            this.m_blnTableFlag, this.DBColName, this.m_lstTextContentInfos, this.m_objCurrentModifyUser))
                        {
                            return false;
                        }
                        break;
                    }
                    else if (objContentInfo.m_intEndIndex < m_intCurrentCursorIndex)
                    {
                        continue;
                    }
                    else
                    {
                        if ((m_intCurrentCursorIndex != 0) && (!CompareModifier(m_objCurrentModifyUser, objContentInfo.objUserInfo,
                            clsHelper.s_dtmMidderTime(), this.m_blnTableFlag, this.DBColName, this.m_lstTextContentInfos, this.m_objCurrentModifyUser)))
                        {
                            return false;
                        }
                        break;
                    }
                }
                #endregion
            }
            return true;
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
        }

        /// <summary>
        /// 检查当前光标位置是否在片图内
        /// </summary>
        /// <param name="p_intIndex"></param>
        /// <returns></returns>
        private bool m_blnCursorPositionToImage4(int p_intIndex)
        {
            this.m_objCurrentSelectedImage = null;
            bool blnRet = clsRichTextBoxPlus.GetImageStatus(this, p_intIndex);
            if (blnRet)
            {
                Image imgPos = null;
                clsRichTextBoxPlus.GetImage(this, p_intIndex, ref imgPos);
                if (imgPos != null)
                {
                    this.m_objCurrentSelectedImage = new clsImageInfo();
                    this.m_objCurrentSelectedImage.m_intStartIndex = p_intIndex;
                    this.m_objCurrentSelectedImage.m_bytImageArr = clsFunction.s_bytConvertImageToByte(imgPos, 0);
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

            clsMedicalTerm objTerm = this.m_lstMedicalTerm[p_intIndex];
            if (objTerm == null) return;
            this.m_lstMedicalTerm.RemoveAt(p_intIndex);
            clsMedicalTerm[] objTermArr = new clsMedicalTerm[this.m_lstMedicalTerm.Count];
            this.m_lstMedicalTerm.CopyTo(objTermArr);
            this.m_lstMedicalTerm.Clear();

            this.SelectionStart = objTerm.m_intStartIndex;
            this.SelectionLength = objTerm.m_strValue.Length;
            this.m_mthDel();

            this.m_lstMedicalTerm.AddRange(objTermArr);
            clsRichTextBoxTool.AdjustTermPosition(ref this.m_lstMedicalTerm, this.FntElement, this);
        }

        /// <summary>
        /// 删除元素
        /// </summary>
        /// <param name="p_objElement"></param>
        public void m_mthDelTerm(clsMedicalTerm p_objElement)
        {
            if (this.m_lstMedicalTerm.Count == 0)
                return;

            if (p_objElement == null) return;

            // 旧模板数据问题，临时处理(存在多个同元素)
            for (int i = this.m_lstMedicalTerm.Count - 1; i >= 0; i--)
            {
                if (this.m_lstMedicalTerm[i].m_strTID == p_objElement.m_strTID && this.m_lstMedicalTerm[i].m_intStartIndex == p_objElement.m_intStartIndex && this.m_lstMedicalTerm[i].m_intEndIndex == p_objElement.m_intEndIndex)
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
            Image img = clsFunction.s_imgConvertByteToImage(this.m_objCurrentSelectedImage.m_bytImageArr);
            control.frmImageEdit frmEdit = new control.frmImageEdit(img);
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
        public List<clsDCUniversalCaseRtbImage> m_lstGetRtbImage()
        {
            List<clsDCUniversalCaseRtbImage> lstImage = new List<clsDCUniversalCaseRtbImage>();

            return lstImage;
        }
        #endregion

        #region (外部)设置图片信息
        /// <summary>
        /// (外部)设置图片信息
        /// </summary>
        /// <returns></returns>
        public void m_mthSetRtbImage(List<clsDCUniversalCaseRtbImage> p_lstRtbImage)
        {
        }
        #endregion

        #region 修改元素

        private clsMedicalTerm m_objEditElement = null;
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
            if (DBColName == null) return;
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
                    if (this.m_objEditElement.m_strTID == "PatInfo")
                    {
                        clsDialog.Msg("病人基本信息元素，不能编辑。", MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
                        return;
                    }
                    else if (this.m_objEditElement.m_strTID.StartsWith("Intellection"))
                    {
                        clsDialog.Msg("智能引用字段，不能编辑。", MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
                        return;
                    }
                    frmLoadElement frm = new frmLoadElement(this.m_objEditElement.m_strTID, this.m_objEditElement.m_strValue, this.m_objEditElement.m_strCaseCode);
                    frm.Owner = this.FindForm();
                    if (frm.Owner is frmBaseMdiCase)
                    {
                        frm.StartPosition = FormStartPosition.CenterScreen;
                    }
                    else
                    {
                        if (frm.Owner != null)
                        {
                            Point pntLoc = frm.Owner.PointToScreen(frm.Owner.Location) + new Size(50, 50);
                            frm.Location = frm.Owner.PointToClient(pntLoc);
                        }
                    }

                    frm.TemplateFlag = this.TemplateFlag;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        this.m_mthDelTerm(this.m_objEditElement);
                        this.SelectionStart = this.m_objEditElement.m_intStartIndex;
                        this.SelectionLength = this.m_objEditElement.m_strValue.Length;
                        this.m_intPreviouslyLen = this.TextLength - this.m_objEditElement.m_strValue.Length;
                        this.m_mthInsertMedicalTerm(frm.CaseCode, frm.ElementID, frm.ElementName, frm.DragRichItem, true);
                        clsRichTextBoxTool.AdjustTermPosition(ref this.m_lstMedicalTerm, this.FntElement, this);
                        this.Invalidate();

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
        public byte[] m_bytGetAllRtf()
        {
            this.m_mthResetPoint();
            return clsFunction.s_bytConvertObjectToByte(this.Rtf);
        }
        #endregion

        #region 获取打印RTF

        /// <summary>
        /// 获取打印RTF
        /// </summary>
        /// <returns></returns>
        public byte[] m_bytGetPrintRtf()
        {
            this.m_mthResetPoint();

            string strRtf = string.Empty;
            List<clsTempData> lstTempData = new List<clsTempData>();
            clsRichTextBoxTool.ComputeChar(ref lstTempData, this.m_lstDoubleStrikeThrough, this.m_lstMedicalTerm, this.Text);
            if (lstTempData.Count > 0)
            {
                this.m_objRtb = new ctlRichTextBox();
                this.m_objRtb.Text = this.Text;
                this.m_objRtb.Rtf = this.Rtf;
                if (m_lstICDInfo.Exists(t => t.m_intFlags < 0))
                {
                    string str1 = string.Empty;
                    string str2 = string.Empty;
                    for (int i = lstTempData.Count - 1; i >= 0; i--)
                    {
                        this.m_objRtb.SelectionStart = lstTempData[i].m_intStartIndex;
                        this.m_objRtb.SelectionLength = lstTempData[i].m_intLen;
                        str1 = this.m_objRtb.SelectedText + "\n";
                        this.m_objRtb.SelectionStart = lstTempData[i].m_intStartIndex;
                        this.m_objRtb.SelectionLength = lstTempData[i].m_intLen + 1;
                        str2 = this.m_objRtb.SelectedText;
                        if (str1 == str2)
                        {
                            lstTempData[i].m_intLen = lstTempData[i].m_intLen + 1;
                            lstTempData[i].m_intEndIndex = lstTempData[i].m_intEndIndex + 1;
                        }
                    }
                }

                for (int i = lstTempData.Count - 1; i >= 0; i--)
                {
                    this.m_objRtb.SelectionStart = lstTempData[i].m_intStartIndex;
                    this.m_objRtb.SelectionLength = lstTempData[i].m_intLen;
                    clsRichTextBoxPlus.InsertText(this.m_objRtb, "");
                }
                strRtf = this.m_objRtb.Rtf;
                this.m_objRtb = null;
            }
            else
            {
                strRtf = this.Rtf;
            }
            return clsFunction.s_bytConvertObjectToByte(strRtf);
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
            string strRtf = clsFunction.s_strGetRtf(p_bytRtfArr);

            if (this.m_blnLoadTemplate)
            {
                using (RichTextBox rtx = new RichTextBox())
                {
                    rtx.Rtf = strRtf;
                    //if (strRtf.IndexOf("pict") == -1) //引发行间距小
                    //{
                    //    rtx.Text = rtx.Text.Trim();
                    //}
                    rtx.SelectAll();
                    //有模板需要颜色标示，如中山，暂取消屏蔽。
                    //rtx.SelectionColor = Color.Black;
                    Font fntSelect = this.Font;
                    if (fntSelect == null)
                    {
                        fntSelect = rtx.SelectionFont;
                    }

                    rtx.SelectionFont = new Font(fntSelect.FontFamily, fntSelect.Size, FontStyle.Regular);

                    strRtf = rtx.SelectedRtf;
                }
            }

            IsReplaceRtf = true;
            this.Rtf = strRtf;
            this.intTextLength = System.Text.Encoding.Default.GetBytes(this.m_strGetRightText()).Length;
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
                clsRichTextBoxPlus.InsertText(rtx, p_strCaption);
                IsReplaceRtf = true;
                this.Rtf = rtx.Rtf;
                this.intTextLength = System.Text.Encoding.Default.GetBytes(this.m_strGetRightText()).Length;
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
            get
            {
                return this.DBColReadOnly;
            }
            set
            {
                this.DBColReadOnly = value;
            }
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
                return null;
            }
            set
            {
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

        private int _presentationMode = 0;
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
        public void m_mthSetFirstlineCaption()
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
                this.Location = new Point(this.Location.X, this.Location.Y + this.Height - 30);
                this.Height = 30;
            }

            this.SetTextLength();
            if (!DesignMode)
            {
                this.IsAllowElementFreeEdit = ElementFreeEdit();
            }
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
                        if (this.Text.Length >= 5 && clsGlobalCase.objCaseInfo != null && clsGlobalCase.objCaseInfo.intCaseStatus != 2)
                        {
                            if (this.Text.Substring(0, 5) == "月经生育史")
                            {
                                this.Select(0, 5);
                                this.m_mthInsertText("月经史");
                            }
                        }

                        this.Select(0, this.FirstlineCaption.Length);
                        Font fntCurrent = this.Font;
                        this.SelectionFont = new Font(fntCurrent.FontFamily, fntCurrent.Size, FontStyle.Bold);
                        this.SelectionStart = this.FirstlineCaption.Length;
                        this.SelectionLength = 0;
                        this.SelectionFont = fntCurrent;
                        /*
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
                        */
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
            if (string.IsNullOrEmpty(DBColName))
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
        public void InsertExtItemTemple(string p_strItemName, List<clsMedicalTerm> p_lstMedTerm)
        {
            if (p_lstMedTerm != null && p_lstMedTerm.Count > 0)
            {
                p_lstMedTerm.Sort();
                int intStartIndex = SelectionStart;
                foreach (clsMedicalTerm obj in p_lstMedTerm)
                {
                    obj.m_intStartIndex += intStartIndex;
                    obj.m_intEndIndex = obj.m_intStartIndex + obj.m_strValue.Length - 1;
                }
            }

            this.m_mthAddItemTemple(p_strItemName, p_lstMedTerm);
        }

        /// <summary>
        /// 添加字段模板内容
        /// </summary>
        /// <param name="p_strItemName"></param>
        /// <param name="p_lstMedTerm"></param>
        private void m_mthAddItemTemple(string p_strItemName, List<clsMedicalTerm> p_lstMedTerm)
        {
            if (!this.m_blnCheckCursorValidPostion()) return;
            clsRichTextBoxTool.StopRedraw(this.Handle);
            this.SelectionStart = this.m_intCurrentCursorIndex;
            this.SelectionLength = 0;
            if (this.SelectionStart == this.m_strGetFirstlineCaption().Length)
            {
                //处理先点击前缀，再引用资料时，引用出来的文字会自定变粗的问题。先对妇幼做特殊处理，成熟之后再开放给其他医院
                //光标点击前缀的时候，SelectionFont的Bold是ture，需要把SelectionFont的Bold设置回来，否则设置SelectedText属性时，会将字体也变粗
                if (SelectionFont.Bold && !this.Font.Bold)
                {
                    this.SelectionFont = new Font(this.SelectionFont.FontFamily, this.SelectionFont.Size, FontStyle.Regular);
                }
            }
            this.m_mthInsertText(p_strItemName);
            if (p_lstMedTerm != null && p_lstMedTerm.Count > 0)
            {
                this.m_lstMedicalTerm.AddRange(p_lstMedTerm);
                this.m_mthSetMedicalTermColor(0);
                clsRichTextBoxTool.AdjustTermPosition(ref this.m_lstMedicalTerm, this.FntElement, this);
            }
            this.m_mthFireEvtTextChange(false);
            this.SelectionLength = 0;
            clsRichTextBoxTool.Redraw(this.Handle);
            this.Refresh();
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
            int intIndex = this.SelectionStart - (this.FirstlineCaption == null ? 0 : this.FirstlineCaption.Length);
            if (!this.m_blnCheckCursorValidPostion()) return;
            //modify by tongyongan@2012-05-21
            string strPath = Application.StartupPath + "\\Common.Utility.dll";
            Assembly assembly = Assembly.LoadFrom(strPath);
            Type objTypeRichItem = assembly.GetType("com.HopeBridge.ehr.control.clsDragRichItem");
            Type objTypeIntellectiveRefItem = assembly.GetType("com.HopeBridge.ehr.control.clsIntellectiveRefItem");

            Type objTypePatInfo = typeof(enumPatientInfoType);
            Type objTypeRichTextBoxItem = typeof(clsDragRichTextBoxItem);
            if (e.Data.GetDataPresent(objTypeRichItem))
            {
                //从右侧资料引用面板拖入数据 
                clsDragRichItem dragRichItem = e.Data.GetData(objTypeRichItem) as clsDragRichItem;
                string strContent = clsHelper.s_strCaseColumnContent(dragRichItem.strCaseCode, dragRichItem.strColCode, dragRichItem.DragString);
                foreach (clsMedicalTerm obj in dragRichItem.DragMedicalTerm)
                {
                    obj.m_intStartIndex += intIndex;
                    obj.m_intEndIndex += intIndex;
                }
                this.m_mthAddItemTemple(strContent, dragRichItem.DragMedicalTerm);
            }
            else if (e.Data.GetDataPresent(objTypeRichTextBoxItem))
            {
                //从右侧资料引用面板拖入数据 
                clsDragRichTextBoxItem dragRichItem = e.Data.GetData(objTypeRichTextBoxItem) as clsDragRichTextBoxItem;
                this.m_mthSetXmlText(dragRichItem.bytRTFData, dragRichItem.strXml, false);
            }
            else if (e.Data.GetDataPresent(objTypeIntellectiveRefItem))
            {
                Form frm = this.FindForm();
                if (frm.GetType().ToString() == clsRichTextBoxTool.TemplateFormClass)
                {
                    this.m_mthAddElement((clsIntellectiveRefItem)e.Data.GetData(objTypeIntellectiveRefItem));
                }
            }
            else if (e.Data.GetDataPresent(objTypePatInfo))
            {
                Form frm = this.FindForm();
                if (frm.GetType().ToString() == clsRichTextBoxTool.TemplateFormClass)
                {
                    this.m_mthAddElement(string.Empty, "PatInfo", Convert.ToString((enumPatientInfoType)e.Data.GetData(objTypePatInfo)), null, false);
                }
                else
                {
                    string strInfo = clsPatientInfoHelper.GetTypePatientInfo((enumPatientInfoType)e.Data.GetData(objTypePatInfo));
                    if (string.IsNullOrEmpty(strInfo))
                    {
                        clsDialog.Msg("没有资料", MessageBoxIcon.Information, clsHelper.s_frmCurrentCaseWin);
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
            //重新获取焦点，并且光标移到最后面。
            //处理表单中引用检验结果时，点击保存按钮会跳转的界面的问题。--hyl 20131209
            this.Focus();
            //if (!string.IsNullOrEmpty(this.Text))
            //{
            //    this.SelectionStart = this.Text.Length;
            //}
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
            clsRichTextBoxPlus.InsertNewLine(this, p_intRowCount, this.m_strGetFirstlineCaption());
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
                if (clsGlobalSysParameter.dicSysParameter.ContainsKey(46) && !string.IsNullOrEmpty(clsGlobalSysParameter.dicSysParameter[46]))
                {
                    int intRoleID = -1;
                    string strParamValue = clsGlobalSysParameter.dicSysParameter[46];
                    int.TryParse(strParamValue, out intRoleID);

                    if (clsGlobalLoginInfo.objLoginInfo.lstRoleID.IndexOf(intRoleID) >= 0)
                    {
                        blnStatus = true;
                    }
                }
                if (blnStatus)
                    this.m_mthClearText();
                else
                    clsDialog.Msg("对不起，无权限删除单元格。", MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        public event dlgHandleReachMaxLength evtReachMaxLength;

        public event HandleReachMaxLength evtReachMaxLengthOld;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // autocompleteMenu1
            // 
            this.autocompleteAssistant.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autocompleteAssistant.ImageList = null;
            this.autocompleteAssistant.Items = new string[0];
            this.autocompleteAssistant.LeftPadding = 0;
            this.autocompleteAssistant.MaximumSize = new System.Drawing.Size(450, 300);
            this.autocompleteAssistant.MinFragmentLength = 1;
            this.autocompleteAssistant.ToolTipDuration = 5000;
            this.autocompleteAssistant.Selecting -= new System.EventHandler<com.HopeBridge.ehr.control.SelectingEventArgs>(this.autocompleteAssistant_Selecting);
            this.autocompleteAssistant.Selecting += new System.EventHandler<com.HopeBridge.ehr.control.SelectingEventArgs>(this.autocompleteAssistant_Selecting);
            this.autocompleteAssistant.Selected -= new System.EventHandler<com.HopeBridge.ehr.control.SelectedEventArgs>(this.autocompleteAssistant_Selected);
            this.autocompleteAssistant.Selected += new System.EventHandler<com.HopeBridge.ehr.control.SelectedEventArgs>(this.autocompleteAssistant_Selected);
            this.autocompleteAssistant.Hovered -= new EventHandler<HoveredEventArgs>(autocompleteAssistant_Hovered);
            this.autocompleteAssistant.Hovered += new EventHandler<HoveredEventArgs>(autocompleteAssistant_Hovered);
            // 
            // ctlRichTextBox
            // 
            this.autocompleteAssistant.SetAutocompleteMenu(this, this.autocompleteAssistant);
            this.ResumeLayout(false);
        }

        void autocompleteAssistant_Hovered(object sender, HoveredEventArgs e)
        {
            AutocompleteItem item = e.Item;
            if (item is DictAutocompleteItem)
            {
                DictAutocompleteItem dictItem = item as DictAutocompleteItem;
                if (m_lstICDInfo.Exists(t => t.m_strICDCode == dictItem.ItemKey))
                {
                    dictItem.ToolTipTitle = "选择提示";
                    dictItem.ToolTipText = string.Format("已存在选项[{0}:{1}]", dictItem.ItemKey, dictItem.ItemValue);
                }
            }
        }

        private void autocompleteAssistant_Selected(object sender, SelectedEventArgs e)
        {
            if (e.Control is ctlRichTextBox && ((ctlRichTextBox)e.Control).IsAllowSignNull == 0)
            {
                int intLen = 0;
                if (this.FirstlineCaption != null) intLen = this.FirstlineCaption.Length;
                AutocompleteItem item = e.Item;
                if (item is DictAutocompleteItem)
                {
                    DictAutocompleteItem dictItem = item as DictAutocompleteItem;
                    clsICDInfo objInfo = new clsICDInfo();
                    objInfo.m_intFlags = 1;
                    objInfo.m_strICDCode = strEscapeCharacter(dictItem.ItemKey);
                    objInfo.m_strICDName = strEscapeCharacter(dictItem.ItemValue);
                    objInfo.m_intStartIndex = this.Text.IndexOf(dictItem.ItemValue, this.SelectionStart - dictItem.ItemValue.Length) - intLen;
                    objInfo.m_intEndIndex = objInfo.m_intStartIndex + dictItem.ItemValue.Length - 1;

                    clsICDInfo objDel = m_lstICDInfo.FirstOrDefault(t => t.m_intStartIndex >= objInfo.m_intStartIndex && t.m_intEndIndex <= objInfo.m_intEndIndex);
                    if (objDel != null)
                    {
                        m_lstICDInfo.Remove(objDel);
                    }

                    clsICDInfo objExists = m_lstICDInfo.FirstOrDefault(t => t.m_intStartIndex <= objInfo.m_intStartIndex
                        && t.m_intEndIndex >= objInfo.m_intEndIndex && t.m_strICDCode == ""
                        && t.m_strICDName.Contains(objInfo.m_strICDName));
                    if (objExists != null)
                    {
                        objExists.m_strICDCode = objInfo.m_strICDCode;
                    }
                    else
                    {
                        m_lstICDInfo.Add(objInfo);
                    }

                    m_lstICDInfo.Sort((a, b) => { return a.m_intStartIndex.CompareTo(b.m_intStartIndex); });
                }
            }
        }

        private void autocompleteAssistant_Selecting(object sender, SelectingEventArgs e)
        {
            AutocompleteItem item = e.Item;
            if (item is DictAutocompleteItem)
            {
                DictAutocompleteItem dictItem = item as DictAutocompleteItem;
                if (m_lstICDInfo.Exists(t => t.m_strICDCode == dictItem.ItemKey))
                {
                    e.Cancel = true;
                    e.Handled = true;
                }
            }
        }
    }

    public delegate void dlgHandleReachMaxLength(ctlRichTextBox p_objSender, string p_strOutRtf, List<clsDSTInfo> p_lstDSTInfo, int p_intOutlineTextLen, int p_intFocusIndex);

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


    #endregion
    [Serializable]
    class clsICDInfo : IComparable
    {
        /// <summary>
        /// 起始位置
        /// </summary>
        public int m_intStartIndex = 0;
        /// <summary>
        /// 结束位置
        /// </summary>
        public int m_intEndIndex = 0;
        /// <summary>
        /// 标志 1-标准,0-自由录入,-1-双划线删除自由录入诊断,-2-双划线删除标准诊断
        /// </summary>
        public int m_intFlags = 0;
        /// <summary>
        /// ICD内容
        /// </summary>
        public string m_strICDName = "";
        /// <summary>
        /// ICD码
        /// </summary>
        public string m_strICDCode = "";

        public int CompareTo(object obj)
        {
            return this.m_strICDCode.CompareTo(((clsICDInfo)obj).m_strICDCode);
        }
    }

    [Serializable]
    class clsCopyICDInfo
    {
        /// <summary>
        /// 文本内容
        /// </summary>
        public string strText { get; set; }
        /// <summary>
        /// 诊断内容
        /// </summary>
        public List<clsICDInfo> lstICDInfo { get; set; }
    }
}


