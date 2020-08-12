using Common.Controls.Emr;
using Common.Entity;
using Common.Utils;
using weCare.Core.Utils;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controls;
using Common.Entity;
using DevExpress.XtraBars;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Console.Ui
{
    public partial class ShowPanelForm : UserControl
    {
        #region 变量.属性

        public int Formid { get; set; }

        List<EntityFormCtrl> FormCtrls = null;

        List<ICheckBox> lstCheck = new List<ICheckBox>();

        public HandleItemMouseClick ItemMouseClick;

        public HandleItemClick ItemClick;

        string FormLayout { get; set; }

        class EntityCtrlPoint
        {
            public string CtrlName { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

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
            try
            {
                this.SuspendLayout();
                if (!string.IsNullOrEmpty(this.FormLayout))
                {
                    FormCtrls = FormTool.Entities(this.FormLayout);
                }
                else
                {
                    if (Formid > 0)
                    {
                        EntityFormDesign vo = null;
                        ProxyFormDesign proxy = new ProxyFormDesign();
                        proxy.Service.GetForm(Formid, out vo);
                        FormCtrls = FormTool.Entities(vo.Layout);
                    }
                }

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
                    int diffTableHeight = 0;
                    object obj = null;
                    Control ctrl = null;
                    EntityCtrlPoint point = null;
                    IRuntimeDesignControl ICtrl = null;
                    List<EntityCtrlPoint> lstCtrlPoint = new List<EntityCtrlPoint>();
                    FormTool.SetParentObject(this, ControlContainer);
                    FormCtrls.Sort();
                    foreach (EntityFormCtrl node in FormCtrls)
                    {
                        if (!string.IsNullOrEmpty(node.Parent)) continue;
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
                                ICtrl = ctrl as IRuntimeDesignControl;
                                ICtrl.Location = new System.Drawing.Point(node.Left, node.Top);
                                ICtrl.Width = node.Width;
                                ICtrl.Height = node.Height;
                                if (ctrl is IRtfEditor) { }
                                else ICtrl.Text = node.Text;
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
                #endregion
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
            if (parent.HasChildren)
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
                            vo.Checked = (((ctlCheckBox)ctrl).Checked ? "1" : "0");
                        else if (ctrl is ctlSignature)
                            vo.Text = ((ctlSignature)ctrl).GetSignEmpName();
                        else
                            vo.Text = ctrl.Text;
                    }

                    if (ctrl.HasChildren)
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
                foreach (Control ctrl in ControlContainer.Controls)
                {
                    if (FormCtrls.Exists(t => t.ItemType == type && t.ControlName == ctrl.Name))
                    {
                        if (ctrl.Text.Trim() == string.Empty)
                        {
                            ctrl.Text = info;
                        }
                    }
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
                    ((IPatientControl)ctrl).RefreshData();
                }
            }
        }
        #endregion
    }
}
