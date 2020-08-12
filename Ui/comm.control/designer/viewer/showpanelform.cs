using Common.Controls.Emr;
using Common.Entity;
using Common.Utils;
using weCare.Core.Utils;
using weCare.Core.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Common.Controls
{
    public partial class ShowPanelForm : UserControl
    {
        #region 变量.属性

        public int Formid { get; set; }

        public string caseCode { get; set; }

        List<EntityFormCtrl> FormCtrls = null;

        List<ICheckBox> lstCheck = new List<ICheckBox>();

        public HandleItemMouseClick ItemMouseClick;

        public HandleItemClick ItemClick;

        /// <summary>
        /// 表单布局XML
        /// </summary>
        public string FormLayout { get; set; }

        /// <summary>
        /// 表单数据XML
        /// </summary>
        public string FormXmlData { get; set; }

        class EntityCtrlPoint
        {
            public string CtrlName { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        /// <summary>
        /// 是否允许保存
        /// </summary>
        public bool IsAllowSave { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string HintInfo { get; set; }

        /// <summary>
        /// 必填检测数组
        /// </summary>
        Dictionary<string, bool> dicEssential { get; set; }

        /// <summary>
        /// 从全局对象获取病人宏元素信息
        /// </summary>
        bool ReadPatientInfoFromGolbolEnv { get; set; }

        ///// <summary>
        ///// 患者唯一标识
        ///// </summary>
        //public string RegisterID { get; set; }

        /// <summary>
        /// 当前选中的RichTextBox控件
        /// </summary>
        public ctlRichTextBox CurrentSelectedRichTextBox { get; set; }

        /// <summary>
        /// 是否不构造初始布局
        /// </summary>
        public bool IsNoCtorLayout { get; set; }

        #endregion

        #region 构造

        public ShowPanelForm()
        {
            InitializeComponent();
        }

        public ShowPanelForm(int _Efid)
        {
            InitializeComponent();
            this.BackColor = Color.White;
            if (!DesignMode)
            {
                this.Size = new Size(0, 0);
                Formid = _Efid;
            }
        }

        public ShowPanelForm(string _formLayout, int _Efid)
        {
            InitializeComponent();
            this.BackColor = Color.White;
            if (!DesignMode)
            {
                this.Size = new Size(0, 0);
                FormLayout = _formLayout;
                Formid = _Efid;
            }
        }
        #endregion

        #region Load
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPanelEF_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            //this.timer.Enabled = true;
            this.ReadPatientInfoFromGolbolEnv = true;
            InitComponent(true);
        }
        #endregion

        #region InitComponent
        /// <summary>
        /// InitComponent
        /// </summary>
        public void InitComponent()
        {
            this.FormLayout = string.Empty;
            InitComponent(true);
        }
        #endregion

        #region InitComponent
        /// <summary>
        /// InitComponent
        /// </summary>
        public void InitComponent(string _FormLayout)
        {
            this.FormLayout = _FormLayout;
            InitComponent(true);
        }
        #endregion

        #region InitComponent
        /// <summary>
        /// InitComponent
        /// </summary>
        /// <param name="_FormLayout"></param>
        /// <param name="_XmlData"></param>
        public void InitComponent(string _FormLayout, string _XmlData)
        {
            if (!string.IsNullOrEmpty(_FormLayout))
                this.FormLayout = _FormLayout;
            else
                _FormLayout = this.FormLayout;
            if (string.IsNullOrEmpty(_XmlData)) return;
            XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(_FormLayout);

            List<EntityNodeCtrl> data = EmrTool.GetXmlNodes(_XmlData);
            if (data != null && data.Count > 0)
            {
                this.ReadPatientInfoFromGolbolEnv = false;
                System.Xml.XmlNodeList nodelist = doc["eflayout"].GetElementsByTagName("ctrl");
                EntityNodeCtrl nodeCtrl = null;
                List<EntityNodeCtrl> lstSignField = new List<EntityNodeCtrl>();
                foreach (System.Xml.XmlNode node in nodelist)
                {
                    nodeCtrl = data.FirstOrDefault(t => t.FieldName == node.Attributes["itemname"].Value);
                    if (nodeCtrl != null)
                    {
                        if (node.Attributes["ctrltype"].Value.StartsWith("Common.Controls.Emr.ctlCheckBox"))
                            node.Attributes["checked"].Value = nodeCtrl.value;
                        else
                            node.Attributes["ctrlText"].Value = nodeCtrl.value;

                        if (node.Attributes["ctrltype"].Value.StartsWith("Common.Controls.Emr.ctlSignature"))
                        {
                            lstSignField.Add(nodeCtrl);
                        }
                    }
                }
                this.FormLayout = doc.OuterXml;

                // 初始化组件
                InitComponent(false);
                // 签名控件
                if (lstSignField.Count > 0 && FormCtrls != null && FormCtrls.Count > 0)
                {
                    EntityFormCtrl obj = null;
                    foreach (EntityNodeCtrl item in lstSignField)
                    {
                        if (FormCtrls.Any(t => t.ItemName == item.FieldName))
                        {
                            obj = FormCtrls.FirstOrDefault(t => t.ItemName == item.FieldName);
                            SetSignature(ControlContainer, obj, item.value);
                        }
                    }
                }
            }
            else
            {
                // 初始化组件
                InitComponent(false);
            }
            // 记录初始表单结果XML
            this.FormXmlData = _XmlData;

            #region 表格

            DataSet ds = Function.ReadXml(_XmlData);
            EntityFormCtrl vo = null;
            foreach (Control ctrl in this.ControlContainer.Controls)
            {
                if (!(ctrl is ctlTableCase)) continue;
                if (FormCtrls.Exists(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name))
                {
                    vo = FormCtrls.FirstOrDefault(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name);
                    if (ds.Tables.Contains("Row"))
                    {
                        ((ctlTableCase)ctrl).m_mthLoadData(1);
                    }
                }
            }
            doc = null;
            #endregion
        }
        #endregion

        #region SetSignature
        /// <summary>
        /// SetSignature
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="value"></param>
        public void SetSignature(string itemName, string value)
        {
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                if (FormCtrls.Any(t => t.ItemName == itemName))
                {
                    EntityFormCtrl obj = FormCtrls.FirstOrDefault(t => t.ItemName == itemName);
                    SetSignature(ControlContainer, obj, value);
                }
            }
        }
        /// <summary>
        /// SetSignature
        /// </summary>
        /// <param name="parentCtrl"></param>
        /// <param name="obj"></param>
        /// <param name="fieldValue"></param>
        void SetSignature(Control parentCtrl, EntityFormCtrl obj, string fieldValue)
        {
            if (parentCtrl != null && parentCtrl.Controls.Count > 1 && !(parentCtrl is ctlSignature))
            {
                foreach (Control ctrl in parentCtrl.Controls)
                {
                    SetSignature(ctrl, obj, fieldValue);
                }
            }
            else
            {
                if (obj.ControlName == parentCtrl.Name && parentCtrl is ctlSignature)
                {
                    EntitySignature sig = new EntitySignature();
                    sig.caseCode = this.caseCode;
                    sig.empName = fieldValue;
                    sig.signKeyId = "***";
                    sig.signDate = DateTime.Now;
                    sig.recordDate = DateTime.Now;

                    ((ctlSignature)parentCtrl).AddSignEmp(sig);
                    ((ctlSignature)parentCtrl).SetSignName(fieldValue);
                    ((ctlSignature)parentCtrl).m_lstNoSaveSignature.Add(sig);
                }
            }
        }
        #endregion

        #region InitComponent
        /// <summary>
        /// InitComponent
        /// </summary>
        /// <param name="isInit"></param>
        void InitComponent(bool isInit)
        {
            try
            {
                if (string.IsNullOrEmpty(this.FormLayout) && Formid > 0)
                {
                    EntityFormDesign vo = null;
                    ProxyFormDesign proxy = new ProxyFormDesign();
                    proxy.Service.GetForm(Formid, out vo);
                    FormCtrls = FormTool.Entities(vo.Layout);
                    this.FormLayout = vo.Layout;
                }
                this.FormXmlData = string.Empty;
                this.ClearComponent();
                FormCtrls = FormTool.Entities(this.FormLayout);
                this.SuspendLayout();

                #region 用下面代码设定:宽*高
                //if (!string.IsNullOrEmpty(eafMain.PanelSize))
                //{
                //    string[] size = eafMain.PanelSize.Split('|');
                //    if (size.Length == 2)
                //    {
                //        int height = HopeBridge.FrameWork.Utils.Function.Int(size[0]);
                //        if (height > 0)
                //        {
                //            this.Height = height + 8;
                //        }
                //        int width = HopeBridge.FrameWork.Utils.Function.Int(size[1]);
                //        if (width > 0)
                //        {
                //            this.Width = width + 8;
                //        }
                //    }
                //}
                #endregion

                #region Controls
                int intMaxTop = 0;
                int intMaxLeft = 0;
                if (FormCtrls != null)
                {
                    if (isInit && this.IsNoCtorLayout)
                    {
                        foreach (EntityFormCtrl node in FormCtrls)
                        {
                            intMaxTop = Math.Max(node.Top + node.Height, intMaxTop);
                            intMaxLeft = Math.Max(node.Left + node.Width, intMaxLeft);
                        }
                        this.Width = intMaxLeft + 20;
                        this.Height = intMaxTop + 20;
                        FormTool.maxHeight = this.Height;
                    }
                    else
                    {
                        int diffTableHeight = 0;
                        object obj = null;
                        Control ctrl = null;
                        EntityCtrlPoint point = null;
                        IRuntimeDesignControl ICtrl = null;
                        List<EntityCtrlPoint> lstCtrlPoint = new List<EntityCtrlPoint>();
                        FormTool.SetParentObject(this, ControlContainer);
                        FormTool.ClearStaticObj();
                        FormCtrls.Sort();           // 按Top排序
                        List<Control> lstCtrlTop = new List<Control>();
                        Dictionary<int, int> dicDiff = new Dictionary<int, int>();
                        Dictionary<IRuntimeDesignControl, Point> dicPoint = new Dictionary<IRuntimeDesignControl, Point>();
                        int i = -1;
                        foreach (EntityFormCtrl node in FormCtrls)
                        {
                            ++i;
                            if (!string.IsNullOrEmpty(node.Parent)) continue;
                            node.ReadPatientInfoFromGolbolEnv = this.ReadPatientInfoFromGolbolEnv;
                            obj = FormTool.CreateControl(node, FormCtrls);
                            if (obj == null) continue;
                            if (obj is Control)
                            {
                                ctrl = obj as Control;
                                ctrl.Name = node.ControlName;
                                ControlContainer.Controls.Add(ctrl);
                                if (ctrl is IRuntimeDesignControl)
                                {
                                    if (diffTableHeight > 0 && intMaxTop > node.Top)
                                    {
                                        node.Top += diffTableHeight;
                                    }
                                    if (ctrl is IRtfEditor)
                                    {
                                        ((IRtfEditor)ctrl).ContextMenuStrip = this.contextMenuStrip;
                                        if (((IRtfEditor)ctrl).Multiline && Math.Abs(ctrl.Height - node.Height) > 10)
                                            dicDiff.Add(i, ctrl.Height - node.Height);     // 主要是richtextbox
                                    }
                                    int sumTop = 0;
                                    if (dicDiff.Count > 0)
                                    {
                                        sumTop = dicDiff.Where(t => t.Key < i).Sum(t => t.Value);
                                    }
                                    ICtrl = ctrl as IRuntimeDesignControl;
                                    if (sumTop != 0)
                                    {
                                        node.Top += sumTop;
                                        ICtrl.Location = new System.Drawing.Point(node.Left, node.Top);
                                        dicPoint.Add(ICtrl, ICtrl.Location);
                                    }
                                    if (isInit == false)
                                    {
                                        if (ctrl is IRtfEditor) { }
                                        else if (ctrl is IPatientControl)
                                            ((IPatientControl)ctrl).CaptionText = node.Text;
                                        else
                                            ICtrl.Text = node.Text;
                                    }
                                    ICtrl.ForeColor = node.ForeColor;
                                    if (!string.IsNullOrEmpty(node.TextFont))
                                    {
                                        ((Control)ctrl).Font = FontSerializationService.Deserialize(node.TextFont);
                                    }
                                    if (ctrl is ICheckBox && node.Checked == "1")
                                    {
                                        ctrl.ForeColor = Color.Blue;
                                    }
                                    if (ctrl is IFormCtrl)
                                    {
                                        ((Control)ctrl).Tag = node;
                                    }
                                    if ((ctrl is ctlTextBox) || (ctrl is ctlCheckBox) || ctrl is ICtlLine || ctrl is IRtfEditor)
                                    {
                                        lstCtrlTop.Add(ctrl);
                                        if (ctrl is ctlTextBox)
                                        {
                                            if (ctrl.TabIndex == 0)
                                            {
                                                ((ctlTextBox)ctrl).Properties.ReadOnly = true;
                                            }
                                        }
                                    }
                                    if (ctrl is ctlTextBox || ctrl is ctlCheckBox || ctrl is ctlDatetime || ctrl is ctlRadioButton || ctrl is ctlComboBox)
                                    {
                                        //((ctlTextBox)ctrl).KeyDown += new KeyEventHandler(TextBox_KeyDown);
                                        ctrl.KeyDown += new KeyEventHandler(TextBox_KeyDown);
                                    }
                                    if (ctrl is IFormCtrl)
                                    {
                                        ((IFormCtrl)ctrl).ValueChangedFlag = false;
                                    }
                                }
                                else if (ctrl is ctlTableCase)
                                {
                                    node.Top += diffTableHeight;
                                    ctrl.Location = new System.Drawing.Point(node.Left, node.Top);
                                    diffTableHeight = ctrl.Height - node.Height;
                                    node.Width = ctrl.Width;
                                    node.Height = ctrl.Height;
                                    ctrl.Width = node.Width;
                                    ctrl.Height = node.Height;
                                    ((ctlTableCase)ctrl).m_mthClearPage();
                                }
                                else
                                {
                                    if (diffTableHeight > 0 && intMaxTop > node.Top)
                                    {
                                        node.Top += diffTableHeight;
                                    }
                                    ctrl.Location = new System.Drawing.Point(node.Left, node.Top);
                                    ctrl.Width = node.Width;
                                    ctrl.Height = node.Height;
                                }

                                point = new EntityCtrlPoint();
                                point.CtrlName = node.ControlName;
                                point.X = node.Left;
                                point.Y = node.Top;
                                lstCtrlPoint.Add(point);

                                intMaxTop = Math.Max(node.Top + node.Height, intMaxTop);
                                intMaxLeft = Math.Max(node.Left + node.Width, intMaxLeft);
                            }
                        }
                        foreach (Control ctrl2 in lstCtrlTop)
                        {
                            ctrl2.BringToFront();
                        }

                        #region 补richtextbox多行Bug，类似于重新定位
                        if (dicPoint.Count > 0)
                        {
                            foreach (IRuntimeDesignControl item in dicPoint.Keys)
                            {
                                item.Location = dicPoint[item];
                            }
                        }
                        #endregion

                        ButtonItemClick(ControlContainer);
                        this.Width = intMaxLeft + 20;
                        this.Height = intMaxTop + 20;
                        FormTool.maxHeight = this.Height;

                        #region 多个表格时，有时需要重整坐标
                        if (diffTableHeight > 0)
                        {
                            foreach (Control ctrl1 in ControlContainer.Controls)
                            {
                                EntityCtrlPoint point1 = lstCtrlPoint.FirstOrDefault(t => t.CtrlName == ctrl1.Name);
                                ctrl1.Location = new Point(point1.X, point1.Y);
                            }
                        }
                        #endregion
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                DialogBox.Msg(ex.Message);
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        #endregion

        #region ClearComponent
        /// <summary>
        /// ClearComponent
        /// </summary>
        public void ClearComponent()
        {
            try
            {
                this.SuspendLayout();
                this.ControlContainer.Controls.Clear();
                this.FormCtrls = null;
            }
            finally
            {
                this.ResumeLayout();
            }
        }
        #endregion

        #region Button.ItemClick
        /// <summary>
        /// Button.ItemClick
        /// </summary>
        /// <param name="parent"></param>
        void ButtonItemClick(Control parent)
        {
            if (parent != null && parent.Controls.Count > 1 && !(parent is ctlSignature))
            {
                foreach (Control ctrl in parent.Controls)
                {
                    ButtonItemClick(ctrl);
                }
            }
            else
            {
                if (parent is ICtlButton)
                {
                    ((ICtlButton)parent).ItemClick += new HandleItemClick(ShowPanelEAF_ItemClick);
                }
                else if (parent is ICheckBox)
                {
                    ((ICheckBox)parent).ItemClick += new HandleItemClick(ShowPanelEAF_ItemClick);
                }
            }
        }

        void ShowPanelEAF_ItemClick(object sender, EntityFormCtrl ctrl)
        {
            if (ItemClick != null)
            {
                ItemClick(sender, ctrl);
            }
        }
        #endregion

        #region SetDisplayText
        /// <summary>
        /// SetDisplayText
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="value"></param>
        public void SetDisplayText(string itemName, string value)
        {
            if (!string.IsNullOrEmpty(itemName))
            {
                EntityFormCtrl eaf = FormCtrls.FirstOrDefault(t => t.ItemName.Trim() == itemName.Trim());
                if (eaf != null)
                {
                    Control ctrl = FormTool.CreateControl(eaf, FormCtrls) as Control;
                    if (ctrl != null)
                    {
                        ctrl.Text = value;
                    }
                }
            }
        }
        #endregion

        #region XmlData
        /// <summary>
        /// XmlData
        /// </summary>
        /// <returns></returns>
        public string XmlData()
        {
            this.dicEssential = null;
            this.dicEssential = new Dictionary<string, bool>();
            List<EntityNodeCtrl> data = new List<EntityNodeCtrl>();
            List<string> parent = new List<string>();

            this.HintInfo = string.Empty;
            this.IsAllowSave = true;
            XmlDataNormal(this.ControlContainer, ref data, ref parent);
            #region 必填项检查
            if (this.dicEssential.Keys.Count > 0)
            {
                foreach (string key in this.dicEssential.Keys)
                {
                    if (this.dicEssential[key] == false)
                    {
                        this.IsAllowSave = false;
                        this.HintInfo = this.GetItemCaption(key) + "(" + key + ")";
                        return string.Empty;
                    }
                }

                foreach (bool ib in this.dicEssential.Values)
                {
                    if (ib == false)
                    {
                        this.IsAllowSave = false;
                        return string.Empty;
                        //break;
                    }
                }
            }
            if (this.IsAllowSave == false)
            {
                return string.Empty;
            }
            #endregion

            #region 检查表头
            this.IsAllowSave = false;
            // 检查表头信息
            if (!string.IsNullOrEmpty(this.FormXmlData) && data.Count > 0)
            {
                try
                {
                    DataSet ds = Function.ReadXml(this.FormXmlData);
                    if (ds.Tables.Contains("FormData") && ds.Tables["FormData"].Rows.Count > 0)
                    {
                        DataTable dtMain = ds.Tables["FormData"];
                        DataRow dr = dtMain.Rows[0];
                        foreach (EntityNodeCtrl item in data)
                        {
                            try
                            {
                                if (dtMain.Columns.IndexOf(item.FieldName) >= 0 && dr[item.FieldName].ToString() != item.value)
                                {
                                    this.IsAllowSave = true;
                                    break;
                                }
                            }
                            catch { }
                        }
                    }
                    ds = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Msg(ex.ToString());
                }
            }
            #endregion
            #region 表格
            string tableXmlData = string.Empty;
            if (XmlDataTable(this.ControlContainer, ref tableXmlData) == false)
            {
                if (this.IsAllowSave)
                {
                    tableXmlData = this.FormXmlData;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                this.IsAllowSave = true;
            }
            #endregion

            string xmldata = EmrTool.GetXmlData(data, parent, tableXmlData);
            //Log.Output(xmldata);
            return xmldata;
        }

        #region 普通节点对象
        /// <summary>
        /// 普通节点对象
        /// </summary>
        /// <param name="parentContainer"></param>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        void XmlDataNormal(Control parentContainer, ref List<EntityNodeCtrl> data, ref List<string> parent)
        {
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                EntityFormCtrl vo = null;
                EntityNodeCtrl node = null;
                foreach (Control ctrl in parentContainer.Controls)
                {
                    if (ctrl is ctlTableCase)
                        continue;
                    if (FormCtrls.Exists(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name))
                    {
                        vo = FormCtrls.FirstOrDefault(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name);
                        node = new EntityNodeCtrl();
                        node.ParentName = vo.ParentNode;
                        node.FieldName = vo.ItemName;
                        string essInfo = vo.ItemName + " " + vo.ItemCaption + Environment.NewLine;
                        if (ctrl is ctlCheckBox)
                        {
                            node.value = (((ctlCheckBox)ctrl).Checked ? "1" : "0");
                            if (((ctlCheckBox)ctrl).GroupName == string.Empty)
                            {
                                if (((ctlCheckBox)ctrl).Essential && string.IsNullOrEmpty(node.value))
                                {
                                    this.IsAllowSave = false;
                                    this.HintInfo += essInfo;
                                }
                            }
                            else
                            {
                                if (((ctlCheckBox)ctrl).Essential)
                                {
                                    if (this.dicEssential.ContainsKey(node.ParentName))
                                    {
                                        if (node.value == "1")
                                            this.dicEssential[node.ParentName] = true;
                                        //else
                                        //    this.HintInfo += essInfo;
                                    }
                                    else
                                    {
                                        this.dicEssential.Add(node.ParentName, node.value == "1" ? true : false);
                                        //if (node.value != "1") this.HintInfo += essInfo;
                                    }
                                }
                            }
                        }
                        else if (ctrl is ctlSignature)
                        {
                            node.value = ((ctlSignature)ctrl).GetSignEmpName();
                            if (string.IsNullOrEmpty(node.value))
                            {
                                if (((ctlSignature)ctrl).IsAutoSignature == 1)
                                {
                                    EntitySignature dcSign = new EntitySignature();
                                    dcSign.empId = GlobalLogin.objLogin.EmpNo;
                                    dcSign.empName = GlobalLogin.objLogin.EmpName;
                                    dcSign.signDate = DateTime.Now;
                                    dcSign.recordDate = DateTime.Now;
                                    dcSign.techLevelCode = GlobalLogin.objLogin.TechLevelCode;
                                    dcSign.techLevelName = GlobalLogin.objLogin.TechLevelName;
                                    dcSign.registerId = GlobalPatient.currPatient.RegisterID;
                                    dcSign.caseCode = GlobalCase.caseInfo.CaseCode;
                                    dcSign.objectID = ((ctlSignature)ctrl).ItemName;
                                    ((ctlSignature)ctrl).AddSignEmp(dcSign);
                                }
                                else
                                {
                                    if (((ctlSignature)ctrl).Essential)
                                    {
                                        this.IsAllowSave = false;
                                        this.HintInfo += essInfo;
                                        DialogBox.Msg("请签名...");
                                    }
                                }
                            }
                        }
                        else
                        {
                            node.IsCdata = true;
                            node.value = ctrl.Text.Trim();
                            if (ctrl is IRuntimeDesignControl && ((IRuntimeDesignControl)ctrl).Essential && string.IsNullOrEmpty(node.value))
                            {
                                this.IsAllowSave = false;
                                this.HintInfo += essInfo; //((IRuntimeDesignControl)ctrl).Text;
                            }
                        }
                        node.TabIndex = vo.TabIndex;
                        data.Add(node);
                        if (!string.IsNullOrEmpty(node.ParentName) && parent.IndexOf(node.ParentName) < 0)
                        {
                            parent.Add(node.ParentName);
                        }
                    }
                    if (ctrl != null && ctrl.Controls.Count > 1 && !(ctrl is ctlSignature))
                    {
                        LayoutXML(ctrl);
                    }
                }
            }
        }
        #endregion

        #region 表格对象
        /// <summary>
        /// 表格对象
        /// </summary>
        /// <param name="parentContainer"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>
        bool XmlDataTable(Control parentContainer, ref string tableXmlData)
        {
            List<bool> lstRet = new List<bool>();
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                try
                {
                    tableXmlData = string.Empty;
                    string oriFormXmlData = string.Empty;
                    string oriTableXmlData = string.Empty;

                    #region 多Table
                    List<string> lstTableXml = new List<string>();
                    if (!string.IsNullOrEmpty(this.FormXmlData))
                    {
                        string xmldata = this.FormXmlData.Trim();
                        int pos1 = xmldata.IndexOf("<Table>");
                        int pos2 = xmldata.LastIndexOf("</Table>");
                        if (pos1 >= 0 && pos2 > 0)
                        {
                            xmldata = xmldata.Substring(pos1, pos2 - pos1 + 8);
                            oriTableXmlData = xmldata;
                        }
                        if (xmldata.Length > 0)
                        {
                            string[] sp = xmldata.Split((new string[] { "<Table>" }), StringSplitOptions.None);
                            foreach (string str in sp)
                            {
                                if (string.IsNullOrEmpty(str))
                                    continue;
                                lstTableXml.Add("<Table>" + str);
                            }
                        }
                    }
                    #endregion

                    EntityFormCtrl vo = null;
                    foreach (Control ctrl in parentContainer.Controls)
                    {
                        if (!(ctrl is ctlTableCase))
                            continue;
                        if (FormCtrls.Exists(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name))
                        {
                            vo = FormCtrls.FirstOrDefault(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name);
                            List<EntityEmrData> lstCaseDataNew = new List<EntityEmrData>();
                            List<EntityEmrData> lstCaseDataUpdate = new List<EntityEmrData>();
                            List<EntityEmrData> lstCaseDataInsert = new List<EntityEmrData>();
                            List<EntitySignature> lstSignature = new List<EntitySignature>();
                            List<EntitySignature> lstSignatureInsert = new List<EntitySignature>();
                            bool blnMainDataModiStatus = false;
                            bool blnCaseSaveFlag = true; //false;
                            DateTime? dtmCaseWriteDate = null;
                            string tableCode = vo.ItemName;
                            if (((ctlTableCase)ctrl).IsParentContainerReLoadData)
                            {
                                if (this.Tag != null && this.Tag is EntityEmrDataTable)
                                {
                                    using (ProxyCommon proxy = new ProxyCommon())
                                    {
                                        string xmldata = proxy.Service.GetTableCaseData(this.Tag as EntityEmrDataTable); // 电子病历(表格)--数据
                                        if (!string.IsNullOrEmpty(xmldata))
                                        {
                                            this.FormXmlData = xmldata;
                                            ((ctlTableCase)ctrl).IsParentContainerReLoadData = false;
                                        }
                                    }
                                }
                            }
                            if (((ctlTableCase)ctrl).m_blnGetTableData(ref lstCaseDataNew, ref lstCaseDataUpdate, ref lstSignature, ref lstCaseDataInsert, ref lstSignatureInsert, blnMainDataModiStatus, blnCaseSaveFlag, dtmCaseWriteDate, this.IsAllowSave))
                            {
                                #region 逻辑处理
                                List<string> lstTableData = new List<string>();
                                List<EntityEmrTableFieldInfo> lstTableFields = ((ctlTableCase)ctrl).TableFields;

                                bool isExistTableNode = false;
                                XmlDocument xmlDoc = new XmlDocument();
                                if (!string.IsNullOrEmpty(this.FormXmlData))
                                {
                                    oriFormXmlData = this.FormXmlData;
                                    foreach (string tableStr in lstTableXml)
                                    {
                                        if (tableStr.IndexOf(string.Format("<tableCode>{0}</tableCode>", tableCode)) >= 0)
                                        {
                                            oriFormXmlData = oriFormXmlData.Replace(oriTableXmlData, tableStr);
                                            isExistTableNode = true;
                                            break;
                                        }
                                    }
                                    if (isExistTableNode == false)
                                    {
                                        oriFormXmlData = oriFormXmlData.Replace(oriTableXmlData, string.Empty);
                                    }
                                    xmlDoc.LoadXml(oriFormXmlData);
                                    //XmlElement xmlElem = xmlDoc.DocumentElement;//获取根节点
                                    //XmlNodeList bodyNode = xmlElem.GetElementsByTagName("Table");
                                    //if (bodyNode.Count <= 0)
                                    //{
                                    //    isExistTableNode = false;
                                    //}
                                }
                                if (string.IsNullOrEmpty(this.FormXmlData) || isExistTableNode == false)
                                {
                                    if (lstCaseDataNew != null && lstCaseDataNew.Count > 0)
                                    {
                                        foreach (EntityEmrData item in lstCaseDataNew)
                                        {
                                            item.RowIndex = -1 - item.RowIndex; //0 - item.RowIndex;
                                        }
                                    }
                                    lstTableData.AddRange(GetTableXmlNode(tableCode, lstCaseDataNew, lstTableFields));
                                    lstTableData.AddRange(GetTableXmlNode(tableCode, lstCaseDataUpdate, lstTableFields));
                                    lstTableData.AddRange(GetTableXmlNode(tableCode, lstCaseDataInsert, lstTableFields));
                                    tableXmlData += "<Table>" + Environment.NewLine;
                                    if (lstTableData != null && lstTableData.Count > 0)
                                    {
                                        foreach (string rowData in lstTableData)
                                        {
                                            tableXmlData += rowData;
                                        }
                                    }
                                    tableXmlData += "</Table>" + Environment.NewLine;
                                }
                                else
                                {
                                    int rowIndex = 0;
                                    int maxRowIndex = 0;
                                    bool isTableOk = false;
                                    bool isRowOk = false;
                                    List<EntityEmrData> lstCaseData = null;
                                    XmlNodeList rowNodes = null;

                                    if (lstCaseDataUpdate != null && lstCaseDataUpdate.Count > 0)
                                    {
                                        // modify
                                        rowNodes = xmlDoc.SelectSingleNode("FormData").SelectSingleNode("Table").ChildNodes;
                                        for (int i = rowNodes.Count - 1; i >= 0; i--)
                                        {
                                            isTableOk = false;
                                            isRowOk = false;
                                            foreach (XmlNode item in rowNodes[i].ChildNodes)
                                            {
                                                if (item.Name == "tableCode" && item.InnerText == vo.ItemName) isTableOk = true;
                                                if (item.Name == "rowIndex")
                                                {
                                                    rowIndex = int.Parse(item.InnerText);
                                                    if (lstCaseDataUpdate.Any(t => t.RowIndex == rowIndex)) isRowOk = true;
                                                }
                                                if (isTableOk && isRowOk) break;
                                            }
                                            if (isTableOk && isRowOk)
                                            {
                                                List<EntityEmrData> tmpData = lstCaseDataUpdate.FindAll(t => t.RowIndex == rowIndex);
                                                foreach (XmlNode item in rowNodes[i].ChildNodes)
                                                {
                                                    if (item.Name == "tableCode" || item.Name == "rowIndex") continue;
                                                    if (tmpData.Any(t => t.FieldName == item.Name))
                                                    {
                                                        item.InnerText = tmpData.FirstOrDefault(t => t.FieldName == item.Name).FieldText;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (lstCaseDataInsert != null && lstCaseDataInsert.Count > 0)
                                    {
                                        // 插入行号计算
                                        List<int> lstRowIndexInsert = new List<int>();
                                        foreach (EntityEmrData item in lstCaseDataInsert)
                                        {
                                            if (lstRowIndexInsert.IndexOf(item.RowIndex) < 0)
                                            {
                                                lstRowIndexInsert.Add(item.RowIndex);
                                            }
                                        }
                                        foreach (int insRowIndex in lstRowIndexInsert)
                                        {
                                            // modify
                                            rowNodes = xmlDoc.SelectSingleNode("FormData").SelectSingleNode("Table").ChildNodes;
                                            for (int i = rowNodes.Count - 1; i >= 0; i--)
                                            {
                                                isTableOk = false;
                                                isRowOk = false;
                                                foreach (XmlNode item in rowNodes[i].ChildNodes)
                                                {
                                                    if (item.Name == "tableCode" && item.InnerText == tableCode) isTableOk = true;
                                                    if (item.Name == "rowIndex" && int.Parse(item.InnerText) >= insRowIndex) isRowOk = true;
                                                    if (isTableOk && isRowOk)
                                                    {
                                                        item.InnerText = Convert.ToString(int.Parse(item.InnerText) + 1);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        foreach (int insRowIndex in lstRowIndexInsert)
                                        {
                                            XmlNode root = xmlDoc.SelectSingleNode("FormData").SelectSingleNode("Table");
                                            XmlElement xeRow = xmlDoc.CreateElement("Row");
                                            // tableCode
                                            XmlElement xeField = xmlDoc.CreateElement("tableCode");
                                            xeField.InnerText = tableCode;
                                            xeRow.AppendChild(xeField);
                                            // rowIndex
                                            xeField = xmlDoc.CreateElement("rowIndex");
                                            xeField.InnerText = insRowIndex.ToString();
                                            xeRow.AppendChild(xeField);

                                            lstCaseData = lstCaseDataInsert.FindAll(t => t.RowIndex == insRowIndex);
                                            foreach (EntityEmrData item in lstCaseData)
                                            {
                                                xeField = xmlDoc.CreateElement(item.FieldName);
                                                xeField.InnerText = item.FieldText;
                                                xeRow.AppendChild(xeField);
                                            }
                                            root.AppendChild(xeRow);
                                        }
                                    }

                                    if (lstCaseDataNew != null && lstCaseDataNew.Count > 0)
                                    {
                                        rowNodes = xmlDoc.SelectSingleNode("FormData").SelectSingleNode("Table").ChildNodes;
                                        for (int i = rowNodes.Count - 1; i >= 0; i--)
                                        {
                                            isTableOk = false;
                                            foreach (XmlNode item in rowNodes[i].ChildNodes)
                                            {
                                                if (item.Name == "tableCode" && item.InnerText == tableCode) isTableOk = true;
                                                if (isTableOk && item.Name == "rowIndex")
                                                {
                                                    maxRowIndex = Math.Max(int.Parse(item.InnerText), maxRowIndex);
                                                    break;
                                                }
                                            }
                                        }

                                        // 新行号计算
                                        List<int> lstRowIndexNew = new List<int>();
                                        foreach (EntityEmrData item in lstCaseDataNew)
                                        {
                                            if (lstRowIndexNew.IndexOf(item.RowIndex) < 0)
                                            {
                                                lstRowIndexNew.Add(item.RowIndex);
                                            }
                                        }
                                        int no = 0;
                                        foreach (int newRowIndex in lstRowIndexNew)
                                        {
                                            XmlNode root = xmlDoc.SelectSingleNode("FormData").SelectSingleNode("Table");
                                            XmlElement xeRow = xmlDoc.CreateElement("Row");
                                            // tableCode
                                            XmlElement xeField = xmlDoc.CreateElement("tableCode");
                                            xeField.InnerText = tableCode;
                                            xeRow.AppendChild(xeField);
                                            // rowIndex
                                            xeField = xmlDoc.CreateElement("rowIndex");
                                            xeField.InnerText = Convert.ToString(++no + maxRowIndex);
                                            xeRow.AppendChild(xeField);

                                            lstCaseData = lstCaseDataNew.FindAll(t => t.RowIndex == newRowIndex);
                                            foreach (EntityEmrData item in lstCaseData)
                                            {
                                                xeField = xmlDoc.CreateElement(item.FieldName);
                                                xeField.InnerText = item.FieldText;
                                                xeRow.AppendChild(xeField);
                                            }
                                            root.AppendChild(xeRow);
                                        }
                                    }
                                    // tableXmlData += xmlDoc.InnerXml + Environment.NewLine;

                                    string xmldata = xmlDoc.InnerXml;
                                    int pos1 = xmlDoc.InnerXml.IndexOf("<Table>");
                                    int pos2 = xmlDoc.InnerXml.LastIndexOf("</Table>");
                                    if (pos1 >= 0 && pos2 > 0)
                                    {
                                        tableXmlData += xmlDoc.InnerXml.Substring(pos1, pos2 - pos1 + 8) + Environment.NewLine;
                                    }

                                    #region bak
                                    //string tableNodeName = "table_" + vo.ItemName;
                                    //XmlDocument doc = new XmlDocument();
                                    //doc.LoadXml(this.FormXmlData);
                                    //XmlNode xmlNode = doc.SelectSingleNode("/FormData/Table");
                                    //if (xmlNode != null)
                                    //{
                                    //    int maxRowIndex = 0;
                                    //    System.Xml.XmlNodeList nodelist = doc["FormData"]["Table"].GetElementsByTagName("Row");
                                    //    if (nodelist != null && nodelist.Count > 0)
                                    //    {
                                    //        // 插入行号计算
                                    //        List<int> lstRowIndexInsert = new List<int>();
                                    //        if (lstCaseDataInsert != null && lstCaseDataInsert.Count > 0)
                                    //        {
                                    //            foreach (EntityEmrData item in lstCaseDataInsert)
                                    //            {
                                    //                if (lstRowIndexInsert.IndexOf(item.RowIndex) < 0)
                                    //                {
                                    //                    lstRowIndexInsert.Add(item.RowIndex);
                                    //                }
                                    //            }
                                    //        }
                                    //        int rowIndex = 0;
                                    //        XmlNode node = null;
                                    //        // 插入调整行号
                                    //        foreach (int rowIndexInsert in lstRowIndexInsert)
                                    //        {
                                    //            for (int i = 0; i < nodelist.Count; i++)
                                    //            {
                                    //                node = nodelist.Item(i);
                                    //                rowIndex = Function.Int(node.Attributes["no"].Value);
                                    //                if (rowIndex >= rowIndexInsert)
                                    //                {
                                    //                    node.Attributes["no"].Value = Convert.ToString(rowIndex + 1);
                                    //                }
                                    //            }
                                    //        }
                                    //        for (int i = 0; i < nodelist.Count; i++)
                                    //        {
                                    //            node = nodelist.Item(i);
                                    //            rowIndex = Function.Int(node.Attributes["no"].Value);
                                    //            maxRowIndex = Math.Max(maxRowIndex, rowIndex);

                                    //            #region 更新
                                    //            if (lstCaseDataUpdate != null && lstCaseDataUpdate.Count > 0)
                                    //            {
                                    //                if (lstCaseDataUpdate.Any(t => t.RowIndex == rowIndex))
                                    //                {
                                    //                    List<EntityEmrData> tmpData = lstCaseDataUpdate.FindAll(t => t.RowIndex == rowIndex);
                                    //                    foreach (EntityEmrData item in tmpData)
                                    //                    {
                                    //                        node.Attributes[item.FieldName].Value = item.FieldText;
                                    //                    }
                                    //                }
                                    //            }
                                    //            #endregion
                                    //        }
                                    //    }

                                    //    // XML结构体
                                    //    string innerXml = xmlNode.InnerXml;

                                    //    #region 添加
                                    //    if (lstCaseDataNew != null && lstCaseDataNew.Count > 0)
                                    //    {
                                    //        foreach (EntityEmrData item in lstCaseDataNew)
                                    //        {
                                    //            item.RowIndex = maxRowIndex - item.RowIndex;
                                    //        }
                                    //        lstTableData = GetTableXmlNode(vo.ItemName, lstCaseDataNew, lstTableFields);
                                    //        foreach (string elementXml in lstTableData)
                                    //        {
                                    //            innerXml += elementXml;
                                    //        }
                                    //    }
                                    //    #endregion

                                    //    #region 插入
                                    //    if (lstCaseDataInsert != null && lstCaseDataInsert.Count > 0)
                                    //    {
                                    //        lstTableData = GetTableXmlNode(vo.ItemName, lstCaseDataInsert, lstTableFields);
                                    //        foreach (string elementXml in lstTableData)
                                    //        {
                                    //            innerXml += elementXml;
                                    //        }
                                    //    }
                                    //    #endregion

                                    //    xmlNode.InnerXml = innerXml;
                                    //    tableXmlData = xmlNode.OuterXml + Environment.NewLine;
                                    //}
                                    #endregion
                                }
                                lstRet.Add(true);
                                #endregion
                            }
                            else
                            {
                                foreach (string tableStr in lstTableXml)
                                {
                                    if (tableStr.IndexOf(string.Format("<tableCode>{0}</tableCode>", tableCode)) >= 0)
                                    {
                                        tableXmlData += tableStr + Environment.NewLine;
                                        break;
                                    }
                                }
                                lstRet.Add(false);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DialogBox.Msg(ex.Message);
                    return false;
                }
            }
            if (lstRet.Count == 0)
                return true;

            foreach (bool ret in lstRet)
            {
                if (ret == true)
                    return true;
            }

            return false;
        }

        #region GetTableXmlNode
        /// <summary>
        /// GetTableXmlNode
        /// </summary>
        /// <param name="lstCaseData"></param>
        /// <param name="lstTableFields"></param>
        /// <returns></returns>
        List<string> GetTableXmlNode(string tableCode, List<EntityEmrData> lstCaseData, List<EntityEmrTableFieldInfo> lstTableFields)
        {
            List<string> lstNodes = new List<string>();
            List<int> lstRowNo = new List<int>();
            if (lstCaseData != null && lstCaseData.Count > 0)
            {
                foreach (EntityEmrData item in lstCaseData)
                {
                    if (lstRowNo.IndexOf(item.RowIndex) < 0)
                    {
                        lstRowNo.Add(item.RowIndex);
                    }
                }
            }
            if (lstRowNo.Count > 0)
            {
                string nodeXml = string.Empty;
                EntityEmrData entity = null;
                List<EntityEmrData> lstCaseDataRow = null;
                foreach (int rowIndex in lstRowNo)
                {
                    lstCaseDataRow = lstCaseData.FindAll(t => t.RowIndex == rowIndex);
                    if (lstCaseDataRow != null && lstCaseDataRow.Count > 0)
                    {
                        nodeXml = "<Row>" + Environment.NewLine;
                        nodeXml += "<tableCode>" + tableCode + "</tableCode>";
                        nodeXml += "<rowIndex>" + rowIndex + "</rowIndex>";
                        foreach (EntityEmrTableFieldInfo obj in lstTableFields)
                        {
                            if (lstCaseDataRow.Any(t => t.FieldName == obj.fieldName))
                            {
                                entity = lstCaseDataRow.FirstOrDefault(t => t.FieldName == obj.fieldName);
                                nodeXml += "<" + obj.fieldName + ">" + entity.FieldText + "</" + obj.fieldName + ">" + Environment.NewLine;
                            }
                        }
                        nodeXml += "</Row>" + System.Environment.NewLine;
                        lstNodes.Add(nodeXml);
                    }
                }
            }
            return lstNodes;
        }
        #endregion

        #endregion

        #endregion

        #region LayoutXML
        /// <summary>
        /// LayoutXML
        /// </summary>
        /// <returns></returns>
        public string LayoutXML()
        {
            LayoutXML(this.ControlContainer);
            return FormTool.LayoutXml(FormCtrls);
        }
        /// <summary>
        /// LayoutXML
        /// </summary>
        /// <param name="parentContainer"></param>
        void LayoutXML(Control parentContainer)
        {
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                EntityFormCtrl vo = null;
                foreach (Control ctrl in parentContainer.Controls)
                {
                    if (FormCtrls.Exists(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name))
                    {
                        vo = FormCtrls.FirstOrDefault(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name);
                        if (ctrl is ctlCheckBox)
                        {
                            vo.Checked = (((ctlCheckBox)ctrl).Checked ? "1" : "0");
                            if (((ctlCheckBox)ctrl).GroupName == string.Empty)
                            {
                                if (((ctlCheckBox)ctrl).Essential && string.IsNullOrEmpty(vo.Checked))
                                {
                                    this.IsAllowSave = false;
                                    this.HintInfo = vo.ItemName + " " + vo.ItemCaption;
                                }
                            }
                            else
                            {
                                if (((ctlCheckBox)ctrl).Essential)
                                {
                                    if (this.dicEssential.ContainsKey(vo.ParentNode))
                                    {
                                        if (vo.Checked == "1") this.dicEssential[vo.ParentNode] = true;
                                    }
                                    else
                                    {
                                        this.dicEssential.Add(vo.ParentNode, vo.Checked == "1" ? true : false);
                                    }
                                }
                            }
                        }
                        else if (ctrl is ctlTableCase)
                        {

                        }
                        else if (ctrl is ctlSignature)
                        {
                            vo.Text = ((ctlSignature)ctrl).GetSignEmpName();
                            if (((ctlSignature)ctrl).Essential && string.IsNullOrEmpty(vo.Text))
                            {
                                this.IsAllowSave = false;
                                this.HintInfo = vo.ItemName + " " + vo.ItemCaption;
                            }
                        }
                        else
                        {
                            vo.Text = ctrl.Text;
                            if (ctrl is IRuntimeDesignControl && ((IRuntimeDesignControl)ctrl).Essential && string.IsNullOrEmpty(vo.Text))
                            {
                                this.IsAllowSave = false;
                                this.HintInfo = ((IRuntimeDesignControl)ctrl).Text;
                            }
                        }
                    }

                    if (ctrl != null && ctrl.Controls.Count > 1 && !(ctrl is ctlSignature) && !(ctrl is ctlTableCase))
                    {
                        LayoutXML(ctrl);
                    }
                }
            }
        }
        #endregion

        #region PrintTable
        /// <summary>
        /// PrintTable
        /// </summary>
        /// <returns></returns>
        public DataTable PrintTable()
        {
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                DataTable dtColumn = new System.Data.DataTable();
                foreach (EntityFormCtrl item in FormCtrls)
                {
                    if (item.ItemType == "1" || item.ItemType == "2" || item.ItemType == "3" ||
                        item.ItemType == "4" || item.ItemType == "5" || item.ItemType == "6")
                    {
                        dtColumn.Columns.Add(item.ControlName);
                        //if (string.IsNullOrEmpty(item.Text))
                        //{
                        //    dtColumn.Columns.Add(item.ControlName);
                        //}
                        //else
                        //{
                        //    dtColumn.Columns.Add(item.ControlName + "(" + item.Text + ")");
                        //}
                    }
                }
                return dtColumn;
            }
            return null;
        }
        #endregion

        #region PrintData
        /// <summary>
        /// PrintData
        /// </summary>
        /// <returns></returns>
        public DataTable PrintData()
        {
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                string val = string.Empty;
                DataTable dtSource = PrintTable();
                DataRow dr = dtSource.NewRow();
                foreach (Control ctrl in ControlContainer.Controls)
                {
                    if (FormCtrls.Exists(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "3" ||
                        t.ItemType == "4" || t.ItemType == "5" || t.ItemType == "6") && t.ControlName == ctrl.Name))
                    {
                        if (ctrl is ctlCheckBox)
                            val = (((ctlCheckBox)ctrl).Checked ? "1" : "0");
                        else
                            val = ctrl.Text;
                        dr[ctrl.Name] = val;
                    }
                }
                dtSource.LoadDataRow(dr.ItemArray, true);
                return dtSource;
            }
            return null;
        }
        #endregion

        #region GetItemInfo
        /// <summary>
        /// GetItemInfo
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string GetItemInfo(string fieldName)
        {
            string fieldValue = string.Empty;
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                if (FormCtrls.Any(t => t.ItemName == fieldName))
                {
                    EntityFormCtrl obj = FormCtrls.FirstOrDefault(t => t.ItemName == fieldName);
                    GetItemInfo(ControlContainer, obj, ref fieldValue);
                }
            }
            return fieldValue;
        }
        /// <summary>
        /// GetItemInfo
        /// </summary>
        /// <param name="parentCtrl"></param>
        /// <param name="obj"></param>
        /// <param name="fieldValue"></param>
        public void GetItemInfo(Control parentCtrl, EntityFormCtrl obj, ref string fieldValue)
        {
            if (parentCtrl != null && parentCtrl.Controls.Count > 1 && !(parentCtrl is ctlSignature))
            {
                foreach (Control ctrl in parentCtrl.Controls)
                {
                    GetItemInfo(ctrl, obj, ref fieldValue);
                }
            }
            else
            {
                if (obj.ControlName == parentCtrl.Name)
                {
                    if (parentCtrl is ctlCheckBox)
                    {
                        fieldValue = (((ctlCheckBox)parentCtrl).Checked ? "1" : "0");
                    }
                    else if (parentCtrl is ctlSignature)
                    {
                        fieldValue = ((ctlSignature)parentCtrl).GetSignEmpName();
                    }
                    else
                    {
                        fieldValue = parentCtrl.Text;
                    }
                }
            }
        }
        #endregion

        #region GetItemCaption
        /// <summary>
        /// GetItemCaption
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string GetItemCaption(string fieldName)
        {
            string fieldDesc = string.Empty;
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                if (FormCtrls.Any(t => t.ItemName == fieldName))
                {
                    EntityFormCtrl obj = FormCtrls.FirstOrDefault(t => t.ItemName == fieldName);
                    GetItemInfo(ControlContainer, obj, ref fieldDesc);
                }
            }
            return fieldDesc;
        }
        /// <summary>
        /// GetItemCaption
        /// </summary>
        /// <param name="parentCtrl"></param>
        /// <param name="obj"></param>
        /// <param name="fieldValue"></param>
        public void GetItemCaption(Control parentCtrl, EntityFormCtrl obj, ref string fieldDesc)
        {
            if (parentCtrl != null && parentCtrl.Controls.Count > 1 && !(parentCtrl is ctlSignature))
            {
                foreach (Control ctrl in parentCtrl.Controls)
                {
                    GetItemInfo(ctrl, obj, ref fieldDesc);
                }
            }
            else
            {
                if (obj.ControlName == parentCtrl.Name)
                {
                    fieldDesc = obj.ItemCaption;
                }
            }
        }
        #endregion

        #region SetItemInfo
        /// <summary>
        /// SetItemInfo
        /// </summary>
        /// <param name="info"></param>
        /// <param name="type">2 打印汇总项 3 打印诊断项 4 打印登录医生 5 打印当前时间 6 XML节点</param>
        public void SetItemInfo(string info, string type)
        {
            if (type == "5")
            {
                DateTime dtm = DateTime.Now;
                info = dtm.ToString("yyyy-MM-dd");
            }
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                string val = string.Empty;
                DataTable dtSource = PrintTable();
                DataRow dr = dtSource.NewRow();
                SetItemInfo(ControlContainer, info, type);
            }
        }
        /// <summary>
        /// SetItemInfo
        /// </summary>
        /// <param name="parentCtrl"></param>
        /// <param name="info"></param>
        /// <param name="type"></param>
        void SetItemInfo(Control parentCtrl, string info, string type)
        {
            if (parentCtrl != null && parentCtrl.Controls.Count > 1 && !(parentCtrl is ctlSignature))
            {
                foreach (Control ctrl in parentCtrl.Controls)
                {
                    SetItemInfo(ctrl, info, type);
                }
            }
            else
            {
                if (FormCtrls.Exists(t => t.ItemType == type && t.ControlName == parentCtrl.Name))
                {
                    if (parentCtrl.Text.Trim() == string.Empty)
                    {
                        parentCtrl.Text = info;
                    }
                }
            }
        }
        #endregion

        #region SetFieldValue
        /// <summary>
        /// SetFieldValue
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetFieldValue(string fieldName, string value)
        {
            if (this.FormCtrls != null && this.FormCtrls.Count > 0)
            {
                if (this.FormCtrls.Any(t => t.ItemName == fieldName))
                {
                    SetItemInfo(ControlContainer, this.FormCtrls.FirstOrDefault(t => t.ItemName == fieldName), value);
                }
            }
        }
        /// <summary>
        /// SetItemInfo
        /// </summary>
        /// <param name="parentCtrl"></param>
        /// <param name="formCtrl"></param>
        /// <param name="value"></param>
        void SetItemInfo(Control parentCtrl, EntityFormCtrl formCtrl, string value)
        {
            if (parentCtrl != null && parentCtrl.Controls.Count > 1 && !(parentCtrl is ctlSignature))
            {
                foreach (Control ctrl in parentCtrl.Controls)
                {
                    SetItemInfo(ctrl, formCtrl, value);
                }
            }
            else
            {
                if (formCtrl.ControlName == parentCtrl.Name)
                {
                    if (parentCtrl is ctlCheckBox)
                        ((ctlCheckBox)parentCtrl).Checked = Function.Int(value) == 1 ? true : false;
                    else
                        parentCtrl.Text = value;
                }
            }
        }
        #endregion

        #region RefreshPatInfo
        /// <summary>
        /// RefreshPatInfo
        /// </summary>
        public void RefreshPatInfo()
        {
            foreach (Control ctrl in ControlContainer.Controls)
            {
                if (ctrl is IPatientControl)
                {
                    ((IPatientControl)ctrl).ReadPatientInfoFromGolbolEnv = true;
                    ((IPatientControl)ctrl).RefreshData();
                    ((IPatientControl)ctrl).ReadPatientInfoFromGolbolEnv = false;
                }
            }
        }
        #endregion

        #region SetReadOnly
        /// <summary>
        /// SetReadOnly
        /// </summary>
        /// <returns></returns>
        public void SetReadOnly()
        {
            SetReadOnly(this.ControlContainer);
        }
        /// <summary>
        /// LayoutXML
        /// </summary>
        /// <param name="parentContainer"></param>
        void SetReadOnly(Control parentContainer)
        {
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                EntityFormCtrl vo = null;
                foreach (Control ctrl in parentContainer.Controls)
                {
                    if (FormCtrls.Exists(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name))
                    {
                        vo = FormCtrls.FirstOrDefault(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name);
                        if (ctrl is ctlCheckBox)
                            ((ctlCheckBox)ctrl).Properties.ReadOnly = true;
                        else if (ctrl is ctlTextBox)
                            ((ctlTextBox)ctrl).Properties.ReadOnly = true;
                        else if (ctrl is ctlMemoEdit)
                            ((ctlMemoEdit)ctrl).Properties.ReadOnly = true;
                        else if (ctrl is ctlComboBox)
                            ((ctlComboBox)ctrl).Properties.ReadOnly = true;
                        else if (ctrl is ctlDatetime)
                            ((ctlDatetime)ctrl).Properties.ReadOnly = true;
                        //else if (ctrl is ctlLabel)
                        //{ }
                        //else
                        //    ctrl.Enabled = false;
                    }

                    if (ctrl != null && ctrl.Controls.Count > 1 && !(ctrl is ctlSignature))
                    {
                        SetReadOnly(ctrl);
                    }
                }
            }
        }
        #endregion

        #region 延时加载
        /// <summary>
        /// 延时加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            //this.timer.Enabled = false;
            //InitComponent();
        }
        #endregion

        #region IsValueChanged
        /// <summary>
        /// IsValueChanged
        /// </summary>
        /// <returns></returns>
        public bool IsValueChanged()
        {
            return ValueChanged(this.ControlContainer);
        }
        /// <summary>
        /// ValueChanged
        /// </summary>
        /// <param name="parentContainer"></param>
        /// <returns></returns>
        bool ValueChanged(Control parentContainer)
        {
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                foreach (Control ctrl in parentContainer.Controls)
                {
                    if (ctrl is ctlTableCase)
                    {
                        return ((ctlTableCase)ctrl).IsValueChanged();
                    }
                    else
                    {
                        if (FormCtrls.Exists(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name))
                        {
                            if (ctrl is ctlCheckBox)
                            {
                                if (((ctlCheckBox)ctrl).ValueChangedFlag) return true;
                            }
                            else if (ctrl is ctlDatetime)
                            {
                                if (((ctlDatetime)ctrl).ValueChangedFlag) return true;
                            }
                            else if (ctrl is ctlTextBox)
                            {
                                if (((ctlTextBox)ctrl).ValueChangedFlag) return true;
                            }
                            else if (ctrl is ctlComboBox)
                            {
                                if (((ctlComboBox)ctrl).ValueChangedFlag) return true;
                            }
                            else if (ctrl is ctlMemoEdit)
                            {
                                if (((ctlMemoEdit)ctrl).ValueChangedFlag) return true;
                            }
                            else if (ctrl is ctlRichTextBox)
                            {
                                if (((ctlRichTextBox)ctrl).ValueChangedFlag) return true;
                            }
                        }
                    }
                    if (ctrl != null && ctrl.Controls.Count > 1 && !(ctrl is ctlSignature))
                    {
                        return ValueChanged(ctrl);
                    }
                }
            }
            return false;
        }
        #endregion

        #region ResetValueChange
        /// <summary>
        /// ResetValueChange
        /// </summary>
        public void ResetValueChange()
        {
            ResetValueChangeFlag(this.ControlContainer);
        }
        /// <summary>
        /// ResetValueChangeFlag
        /// </summary>
        /// <param name="parentContainer"></param>
        void ResetValueChangeFlag(Control parentContainer)
        {
            if (FormCtrls != null && FormCtrls.Count > 0)
            {
                foreach (Control ctrl in parentContainer.Controls)
                {
                    if (ctrl is ctlTableCase)
                    {
                        ((ctlTableCase)ctrl).ReLoadPage();
                    }
                    if (FormCtrls.Exists(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name))
                    {
                        if (ctrl is ctlCheckBox)
                        {
                            ((ctlCheckBox)ctrl).ValueChangedFlag = false;
                        }
                        else if (ctrl is ctlDatetime)
                        {
                            ((ctlDatetime)ctrl).ValueChangedFlag = false;
                        }
                        else if (ctrl is ctlTextBox)
                        {
                            ((ctlTextBox)ctrl).ValueChangedFlag = false;
                        }
                        else if (ctrl is ctlComboBox)
                        {
                            ((ctlComboBox)ctrl).ValueChangedFlag = false;
                        }
                        else if (ctrl is ctlMemoEdit)
                        {
                            ((ctlMemoEdit)ctrl).ValueChangedFlag = false;
                        }
                        else if (ctrl is ctlRichTextBox)
                        {
                            ((ctlRichTextBox)ctrl).ValueChangedFlag = false;
                        }
                    }
                    if (ctrl != null && ctrl.Controls.Count > 1 && !(ctrl is ctlSignature))
                    {
                        ResetValueChangeFlag(ctrl);
                    }
                }
            }
        }
        #endregion

        #region ResetTablePage
        /// <summary>
        /// ResetTablePage
        /// </summary>
        public void ResetTablePage()
        {
            if (FormCtrls == null || FormCtrls.Count == 0) return;
            foreach (Control ctrl in this.ControlContainer.Controls)
            {
                if (!(ctrl is ctlTableCase)) continue;
                if (FormCtrls.Exists(t => (t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6") && t.ControlName == ctrl.Name))
                {
                    try
                    {
                        ((ctlTableCase)ctrl).m_mthLoadData(((ctlTableCase)ctrl).CurrentPageNo);
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Msg(ex.Message);
                    }
                }
            }
        }
        #endregion

        #region FindNode
        /// <summary>
        /// FindNode
        /// </summary>
        /// <param name="value"></param>
        public void FindNode(string value)
        {
            if (this.FormCtrls != null && this.FormCtrls.Count > 0)
            {
                if (this.FormCtrls.Any(t => t.ItemName.Contains(value) || t.ItemCaption.Contains(value) || SpellCodeHelper.GetPyCode(t.ItemCaption).Contains(value) || SpellCodeHelper.GetWbCode(t.ItemCaption).Contains(value)))
                {
                    FindNode(ControlContainer, this.FormCtrls.FirstOrDefault(t => t.ItemName.Contains(value) || t.ItemCaption.Contains(value) || SpellCodeHelper.GetPyCode(t.ItemCaption).Contains(value) || SpellCodeHelper.GetWbCode(t.ItemCaption).Contains(value)));
                }
            }
        }
        /// <summary>
        /// FindNode
        /// </summary>
        /// <param name="parentCtrl"></param>
        /// <param name="formCtrl"></param>
        void FindNode(Control parentCtrl, EntityFormCtrl formCtrl)
        {
            if (parentCtrl != null && parentCtrl.Controls.Count > 1 && !(parentCtrl is ctlSignature))
            {
                foreach (Control ctrl in parentCtrl.Controls)
                {
                    FindNode(ctrl, formCtrl);
                }
            }
            else
            {
                if (formCtrl.ControlName == parentCtrl.Name)
                {
                    if (parentCtrl is ctlCheckBox)
                        ((ctlCheckBox)parentCtrl).Checked = true;
                    else
                        parentCtrl.Text = "";
                }
            }
        }
        #endregion

        #region Richtextbox 右键

        private void tsmiCut_Click(object sender, EventArgs e)
        {
            if (this.CurrentSelectedRichTextBox != null)
            {
                this.CurrentSelectedRichTextBox.Cut();
            }
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            if (this.CurrentSelectedRichTextBox != null)
            {
                this.CurrentSelectedRichTextBox.Copy();
            }
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            if (this.CurrentSelectedRichTextBox != null)
            {
                this.CurrentSelectedRichTextBox.Paste();
            }
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            if (this.CurrentSelectedRichTextBox != null)
            {
                this.CurrentSelectedRichTextBox.Clear();
            }
        }

        #endregion

    }
}
