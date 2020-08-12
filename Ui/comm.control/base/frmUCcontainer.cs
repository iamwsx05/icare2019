using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 用户控件.容器
    /// </summary>
    public partial class frmUCcontainer : frmBase
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_uc"></param>
        public frmUCcontainer(ucBase _uc, frmBase _frmOwner, List<EntitySysModule> _lstFuncItems)
        {
            if (!DesignMode && _frmOwner != null)
            {
                _frmOwner.StartRedraw();
            }
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            if (!DesignMode)
            {
                this.ucCustom = _uc;
                this.xtraScrollableControl.Controls.Add(this.ucCustom);
                this.ucCustom.Location = new System.Drawing.Point(0, 0);
                this.Text = this.ucCustom.Caption;

                int width = this.ucCustom.Width + 33;
                int height = this.ucCustom.Height + 30;
                if (width > Screen.PrimaryScreen.WorkingArea.Width)
                    width = Screen.PrimaryScreen.WorkingArea.Width;
                if (height > Screen.PrimaryScreen.WorkingArea.Height)
                    height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Size = new System.Drawing.Size(width, height);
                this.frmParent = _frmOwner;
                this.lstFuncItems = _lstFuncItems;
                // 资源
                this.rm = new System.Resources.ResourceManager(typeof(Common.Controls.Properties.Resources));
            }
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// 用户控件
        /// </summary>
        private ucBase ucCustom { get; set; }

        /// <summary>
        /// 是否保存
        /// </summary>
        public bool IsSave { get; set; }

        /// <summary>
        /// 父窗体
        /// </summary>
        public frmBase frmParent { get; set; }

        /// <summary>
        /// 是否不检查
        /// </summary>
        public bool IsNoCheck { get; set; }

        private List<EntitySysModule> lstFuncItems { get; set; }

        /// <summary>
        /// rm
        /// </summary>
        internal System.Resources.ResourceManager rm { get; set; }

        #endregion

        #region 虚方法

        protected virtual DevExpress.XtraEditors.XtraScrollableControl GetScrollableContainer()
        {
            return new DevExpress.XtraEditors.XtraScrollableControl();
        }

        protected virtual void NavPatientChange()
        {

        }

        #endregion

        #region CheckValueChanged
        /// <summary>
        /// CheckValueChanged
        /// </summary>
        /// <returns></returns>
        private int CheckValueChanged()
        {
            if (!ValueChanged)
            {
                if (!this.ucCustom.CheckDataChanged())
                {
                    return 0;
                }
            }

            DialogResult dr = DialogBox.Msg("【" + this.Text.Replace(" ", "") + "】内容已变动，是否保存？", MessageBoxIcon.Question, true);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    this.ucCustom.Save();
                }
                catch (Exception e)
                {
                    DialogBox.Msg(e.Message);
                    return -1;
                }
                return 1;
            }
            else if (dr == DialogResult.No)
                return 0;
            else if (dr == DialogResult.Cancel)
                return -1;
            else
                return 0;
        }
        #endregion

        #region 权限列表
        
        /// <summary>
        /// 权限列表
        /// </summary>
        /// <returns></returns>
        //private List<EntitySysModule> GetFuncButton()
        //{
        //    using (ProxyLogin proxy = new ProxyLogin())
        //    {
        //        return proxy.Service.GetFormFuncButton(Function.Int(GlobalLoginInfo.objLoginInfo.EmpId), this.ucCustom.GetType().FullName);
        //    }
        //}

        /// <summary>
        /// SetToolBar
        /// </summary>
        private void SetToolBar()
        {
            List<EntitySysModule> lstFuncButton = this.lstFuncItems;
            if (lstFuncButton == null || lstFuncButton.Count == 0)
            {
                this.barTools.Visible = false;
            }
            else
            {
                #region 默认统一按钮
                // 分辨率
                EntitySysModule vo = null;
                //vo = new EntitySysModule();
                //vo.FuncId = 9999901;
                //vo.FuncName = "分辨率";
                //vo.OperName = "ratio";
                //lstFuncButton.Add(vo);
                // 锁系统
                //vo = new EntitySysModule();
                //vo.FuncId = 9999902;
                //vo.FuncName = "锁系统";
                //vo.OperName = "locksys";
                //lstFuncButton.Add(vo);
                // 关闭窗口
                vo = new EntitySysModule();
                vo.FuncId = 9999903;
                vo.FuncName = "关闭窗口";
                vo.OperName = "close";
                vo.ImageSource = "Close";
                lstFuncButton.Add(vo);
                #endregion

                this.barManager.BeginInit();
                this.barManager.BeginUpdate();
                DevExpress.XtraBars.BarLargeButtonItem bbi = null;
                foreach (EntitySysModule item in lstFuncButton)
                {
                    bbi = new DevExpress.XtraBars.BarLargeButtonItem();
                    bbi.Id = Function.Int(item.FuncCode);
                    bbi.Caption = item.FuncName;
                    bbi.Name = "bbi" + item.OperName.ToLower();
                    bbi.Appearance.Font = new System.Drawing.Font("宋体", 9.5f);
                    bbi.Glyph = (rm.GetObject(item.ImageSource) as System.Drawing.Image);
                    switch (item.OperName.ToLower())
                    {
                        case "load":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiLoad_ItemClick);
                            break;
                        case "export":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiExport_ItemClick);
                            break;
                        case "new":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiNew_ItemClick);
                            break;
                        case "save":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiSave_ItemClick);
                            break;
                        case "delete":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiDel_ItemClick);
                            break;
                        case "edit":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiEdit_ItemClick);
                            break;
                        case "template":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiTemplate_ItemClick);
                            break;
                        case "check":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiCheck_ItemClick);
                            break;
                        case "cancel":
                            break;
                        case "confirm":
                            break;
                        case "stop":
                            break;
                        case "print":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiPrint_ItemClick);
                            break;
                        case "search":
                            break;
                        case "close":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiClose_ItemClick);
                            break;
                        case "design":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiDesign_ItemClick);
                            break;
                        case "customform":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiCustomForm_ItemClick);
                            break;
                        case "defineitem":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiDefineItem_ItemClick);
                            break;
                        default:
                            break;
                    }

                    this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { bbi });
                    this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                    new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, bbi, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});

                }
                this.barManager.EndUpdate();
                this.barManager.EndInit();
            }
        }

        private void bbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.Export();
        }

        private void bbiLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.LoadData();
        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.New();
        }

        private void bbiDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.Delete();
        }

        private void bbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.Preview();
        }

        private void bbiTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.Template();
        }

        private void bbiCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.Check();
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.Edit();
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.Save();
        }

        private void bbiDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.Design();
        }

        private void bbiCustomForm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.CustomForm();
        }

        private void bbiDefineItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ucCustom.DefineItem();
        }
        
        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 窗体事件

        private void frmUCcontainer_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (this.ucCustom.IsDockFill)
                {
                    this.ucCustom.Dock = DockStyle.Fill;
                }
                this.SetToolBar();

                if (this.ucCustom.Name == "ucOrderItem" || this.ucCustom.Name == "ucOrderGrid")
                {
                    foreach (DevExpress.XtraBars.BarLargeButtonItem item in this.barManager.Items)
                    {
                        if (item.Name.ToLower() == "bbibasicinfo")
                        {
                            item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            break;
                        }
                    }
                }
            }
        }

        private void frmUCcontainer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    this.ucCustom.Save();
                }
            }
            else
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
        }

        private void frmUCcontainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsNoCheck)
            {
                if (CheckValueChanged() == -1)
                {
                    e.Cancel = true;
                }
            }

            this.IsSave = this.ucCustom.IsSave;
            this.Tag = this.ucCustom.Tag;
        }

        #endregion
              

    }
}
