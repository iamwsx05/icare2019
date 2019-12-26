using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using Common.Controls;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraReports.UI;
using System.IO;
using Common.Utils;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// 用血申请
    /// </summary>
    public partial class frmBloodApply : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmBloodApply(clsBIHPatientInfo _patVo)
        {
            InitializeComponent();
            PatVo = _patVo;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, 0);
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// isInit
        /// </summary>
        bool isInit { get; set; }

        /// <summary>
        /// PatVo
        /// </summary>
        clsBIHPatientInfo PatVo { get; set; }

        /// <summary>
        /// 类型: 1 普通输血申请; 2 自体输血申请
        /// </summary>
        int TypeId { get; set; }

        /// <summary>
        /// firstVo
        /// </summary>
        EntityBloodApply firstVo { get; set; }

        #endregion

        #region 方法

        #region CreateTree
        /// <summary>
        /// CreateTree
        /// </summary>
        void CreateTree()
        {
            // 树结构
            this.tvApply.Columns.Clear();
            uiHelper.SetGridCol(this.tvApply, new string[] { "appName" }, new string[] { "申请单列表" }, new int[] { 200 });
            this.tvApply.Columns["appName"].AppearanceCell.Font = new Font("宋体", 9);
            this.tvApply.KeyFieldName = "fappid";
            this.tvApply.ParentFieldName = "";
            this.tvApply.ImageIndexFieldName = "imageIndex";

            this.tvApply.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.CellFocus;
            this.tvApply.Appearance.FocusedRow.Options.UseBackColor = true;
            this.tvApply.Appearance.FocusedRow.BackColor = Color.LightGreen;    // Color.LightSkyBlue;
            this.tvApply.Appearance.FocusedRow.BackColor2 = Color.White;
            this.tvApply.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.tvApply.Appearance.HideSelectionRow.BackColor = Color.LightGreen;  // Color.LightSkyBlue;
            this.tvApply.Appearance.HideSelectionRow.BackColor2 = Color.White;
            this.tvApply.MouseClick += TvApply_MouseClick;
        }

        /// <summary>
        /// 树操作中
        /// </summary>
        bool isTreeDoing { get; set; }

        private void TvApply_MouseClick(object sender, MouseEventArgs e)
        {
            if (isInit) return;

            bool isValueChange = false;
            if (this.TypeId == 1)
            {
                isValueChange = this.ucBloodApplyForm1.IsValueChange;
                if (this.ucBloodApplyForm1.Tag != null && (((EntityBloodApply)this.ucBloodApplyForm1.Tag).fstatus == 1 || ((EntityBloodApply)this.ucBloodApplyForm1.Tag).fstatus == 3))
                {
                    isValueChange = false;
                }
            }
            else if (this.TypeId == 2)
            {
                isValueChange = this.ucBloodApplyForm2.IsValueChange;
                if (this.ucBloodApplyForm2.Tag != null && (((EntityBloodApply)this.ucBloodApplyForm2.Tag).fstatus == 1 || ((EntityBloodApply)this.ucBloodApplyForm2.Tag).fstatus == 3))
                {
                    isValueChange = false;
                }
            }

            if (isValueChange)// && !isSave)
            {
                if (DialogBox.Msg("是否保存数据？", MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Save();
                    return;
                }
            }

            try
            {
                if (isTreeDoing) return;
                isTreeDoing = true;
                LoadAppInfo(this.tvApply.CalcHitInfo(e.Location).Node);
            }
            finally
            {
                isTreeDoing = false;
            }
        }
        #endregion

        #region LoadAppInfo
        /// <summary>
        /// LoadAppInfo
        /// </summary>
        /// <param name="node"></param>
        void LoadAppInfo(TreeListNode node)
        {
            if (node == null) return;
            com.digitalwave.iCare.gui.HIS.clsPublic.PlayAvi("");
            try
            {
                SetContent((EntityBloodApply)this.tvApply.GetDataRecordByNode(node));
            }
            finally
            {
                com.digitalwave.iCare.gui.HIS.clsPublic.CloseAvi();
            }
        }
        #endregion

        #region LoadDataSource
        /// <summary>
        /// LoadDataSource
        /// </summary>
        void LoadDataSource()
        {
            using (weCare.Proxy.ProxyIP proxy = new weCare.Proxy.ProxyIP())
            {
                this.isInit = true;
                EntityParm parm = new EntityParm() { key = "registerid", value = PatVo.m_strRegisterID };
                List<EntityBloodApply> data = proxy.Service.GetBloodApply(new List<EntityParm>() { parm });
                this.tvApply.BeginUpdate();
                this.tvApply.DataSource = data;
                this.tvApply.ExpandAll();
                this.tvApply.EndUpdate();
                if (data != null && data.Count > 0)
                    firstVo = data[0];
                this.isInit = false;
            }
        }
        #endregion

        #region SetContent/New
        /// <summary>
        /// SetContent/New
        /// </summary>
        /// <param name="vo"></param>
        void SetContent(EntityBloodApply vo)
        {
            try
            {
                this.isInit = true;
                if (vo != null)
                {
                    this.TypeId = (int)vo.fappclass;
                }
                else
                {
                    if (this.TypeId < 1) this.TypeId = 1;
                }
                if (this.TypeId == 1)
                {
                    this.ucBloodApplyForm1.Enabled = true;
                    this.ucBloodApplyForm1.Location = new Point(8, 5);
                    this.ucBloodApplyForm1.Visible = true;
                    this.ucBloodApplyForm2.Visible = false;
                    this.ucBloodApplyForm1.Tag = vo;
                    this.ucBloodApplyForm1.SetContent(this.GetContent(vo == null ? string.Empty : vo.fappxml));
                    this.ucBloodApplyForm1.IsValueChange = false;
                    if (vo != null && (vo.fstatus == 1 || vo.fstatus == 3)) this.ucBloodApplyForm1.Enabled = false;
                }
                else
                {
                    this.ucBloodApplyForm2.Enabled = true;
                    this.ucBloodApplyForm2.Location = new Point(8, 5);
                    this.ucBloodApplyForm2.Visible = true;
                    this.ucBloodApplyForm1.Visible = false;
                    this.ucBloodApplyForm2.Tag = vo;
                    this.ucBloodApplyForm2.SetContent(this.GetContent(vo == null ? string.Empty : vo.fappxml));
                    this.ucBloodApplyForm2.IsValueChange = false;
                    if (vo != null && (vo.fstatus == 1 || vo.fstatus == 3)) this.ucBloodApplyForm2.Enabled = false;
                }
                if (vo == null)
                {
                    this.blbiDel.Enabled = false;
                    this.blbiPut.Enabled = false;
                }
                else
                {
                    if (vo.fstatus == 0)
                    {
                        this.blbiDel.Enabled = true;
                        this.blbiSave.Enabled = true;
                        this.blbiPut.Enabled = true;
                    }
                    else if (vo.fstatus == 1)
                    {
                        this.blbiDel.Enabled = false;
                        this.blbiSave.Enabled = false;
                        this.blbiPut.Enabled = false;
                    }
                    else if (vo.fstatus == 2)
                    {
                        this.blbiDel.Enabled = false;
                        this.blbiSave.Enabled = true;
                        this.blbiPut.Enabled = false;
                    }
                    else if (vo.fstatus == 3)
                    {
                        this.blbiDel.Enabled = false;
                        this.blbiSave.Enabled = false;
                        this.blbiPut.Enabled = false;
                    }
                    else
                    {
                        this.blbiDel.Enabled = true;
                        this.blbiSave.Enabled = true;
                        this.blbiPut.Enabled = false;
                    }
                }
            }
            finally
            {
                this.isInit = false;
            }
        }
        #endregion

        #region GetContent
        /// <summary>
        /// GetContent
        /// </summary>
        /// <param name="xmlVal"></param>
        /// <returns></returns>
        Dictionary<string, string> GetContent(string xmlVal)
        {
            Dictionary<string, string> dicVal = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(xmlVal))
            {
                dicVal.Add("DeptName", PatVo.m_strDeptName);
                dicVal.Add("BedNo", PatVo.m_strBedName);
                dicVal.Add("IpNo", PatVo.m_strInHospitalNo);
                dicVal.Add("PatName", PatVo.m_strPatientName);
                dicVal.Add("Sex", PatVo.m_strSex);
                dicVal.Add("Age", PatVo.m_strAge);
                dicVal.Add("Lczd", PatVo.m_strDiagnose);
                dicVal.Add("Lczd01", PatVo.m_strDiagnose);
            }
            else
            {
                dicVal = weCare.Core.Utils.Function.ReadXmlNodes(xmlVal, "appData");
            }
            return dicVal;
        }
        #endregion

        #region GetCurrVo
        /// <summary>
        /// GetCurrVo
        /// </summary>
        /// <returns></returns>
        EntityBloodApply GetCurrVo()
        {
            EntityBloodApply appVo = null;
            if (this.TypeId == 1 && this.ucBloodApplyForm1.Tag != null)
                appVo = this.ucBloodApplyForm1.Tag as EntityBloodApply;
            else if (this.TypeId == 2 && this.ucBloodApplyForm2.Tag != null)
                appVo = this.ucBloodApplyForm2.Tag as EntityBloodApply;
            return appVo;
        }
        #endregion

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            try
            {
                this.blbiPut.Enabled = false;
                this.isInit = true;
                this.TypeId = 1;
                this.CreateTree();
                this.LoadDataSource();
                this.SetContent(firstVo);
            }
            finally
            {
                isInit = false;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        void Delete()
        {
            EntityBloodApply appVo = this.GetCurrVo();
            if (appVo == null || appVo.fappid == 0)
            {
                SetContent(null);
                return;
            }
            if (DialogBox.Msg("是否删除当前输血申请表？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (weCare.Proxy.ProxyIP proxy = new weCare.Proxy.ProxyIP())
                {
                    int ret = proxy.Service.DelBloodApply(appVo.fappid);
                    if (ret > 0)
                    {
                        SetContent(null);
                        this.tvApply.Nodes.Remove(this.tvApply.FocusedNode);
                        DialogBox.Msg("删除成功！");
                    }
                    else
                    {
                        DialogBox.Msg("删除失败。");
                    }
                }
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        void Save()
        {
            bool isNew = true;
            EntityBloodApply saveVo = new EntityBloodApply();
            string content = string.Empty;
            if (this.TypeId == 1)
            {
                content = this.ucBloodApplyForm1.GetContent();
                if (this.ucBloodApplyForm1.Tag != null)
                {
                    saveVo.fappid = (this.ucBloodApplyForm1.Tag as EntityBloodApply).fappid;
                    isNew = false;
                }
            }
            else if (this.TypeId == 2)
            {
                content = this.ucBloodApplyForm2.GetContent();
                if (this.ucBloodApplyForm2.Tag != null)
                {
                    saveVo.fappid = (this.ucBloodApplyForm2.Tag as EntityBloodApply).fappid;
                    isNew = false;
                }
            }
            saveVo.fappclass = this.TypeId;
            saveVo.fregisterid = PatVo.m_strRegisterID;
            saveVo.fappoperid = this.LoginInfo.m_strEmpID;
            saveVo.fappdate = DateTime.Now;
            saveVo.fappxml = content;
            saveVo.fstatus = 0;
            decimal appId = 0;
            if ((new weCare.Proxy.ProxyIP()).Service.SaveBloodApply(saveVo, out appId) > 0)
            {
                saveVo.fappid = appId;
                if (this.TypeId == 1)
                {
                    this.ucBloodApplyForm1.Tag = saveVo;
                    this.ucBloodApplyForm1.IsValueChange = false;
                }
                else if (this.TypeId == 2)
                {
                    this.ucBloodApplyForm2.Tag = saveVo;
                    this.ucBloodApplyForm2.IsValueChange = false;
                }
                if (isNew)
                {
                    (this.tvApply.DataSource as List<EntityBloodApply>).Add(saveVo);
                    this.tvApply.RefreshDataSource();
                    this.tvApply.FocusedNode = this.tvApply.FindNodeByKeyID(saveVo.fappid);
                }
                else
                {
                    int index = (this.tvApply.DataSource as List<EntityBloodApply>).FindIndex(t => t.fappid == saveVo.fappid);
                    (this.tvApply.DataSource as List<EntityBloodApply>)[index] = saveVo;
                    this.tvApply.RefreshDataSource();
                }
                DialogBox.Msg("保存成功！");
            }
            else
            {
                DialogBox.Msg("保存失败。");
            }
        }
        #endregion

        #region Put
        /// <summary>
        /// Put
        /// </summary>
        void Put()
        {
            EntityBloodApply appVo = this.GetCurrVo();
            if (appVo == null || appVo.fappid == 0) return;
            if (DialogBox.Msg("是否提交当前输血申请表？\r\n\r\n提交后资料将不能再修改。", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (weCare.Proxy.ProxyIP proxy = new weCare.Proxy.ProxyIP())
                {
                    int ret = proxy.Service.PutBloodApply(appVo.fappid, this.LoginInfo.m_strEmpID);
                    if (ret > 0)
                    {
                        this.blbiDel.Enabled = false;
                        this.blbiSave.Enabled = false;
                        this.blbiPut.Enabled = false;
                        DialogBox.Msg("提交成功！");
                    }
                    else
                    {
                        DialogBox.Msg("提交失败。");
                    }
                }
            }
        }
        #endregion

        #region Print
        /// <summary>
        /// Print
        /// </summary>
        void Print()
        {
            XtraReport xr = GetXR();
            if (xr != null && xr.DataSource != null)
            {
                ReportPrintTool rpt = new ReportPrintTool(xr);
                rpt.ShowRibbonPreviewDialog();
            }
            else
            {
                DialogBox.Msg("请先保存数据。");
            }
        }
        #endregion

        #region Export
        /// <summary>
        /// Export
        /// </summary>
        void Export()
        {
            XtraReport xr = GetXR();
            if (xr != null && xr.DataSource != null)
            {
                uiHelper.Export(xr);
            }
            else
            {
                DialogBox.Msg("请先保存数据。");
            }
        }
        #endregion

        #region RefreshTV
        /// <summary>
        /// RefreshTV
        /// </summary>
        void RefreshTV()
        {
            LoadDataSource();
            EntityBloodApply appVo = GetCurrVo();
            if (appVo != null)
            {
                this.tvApply.FocusedNode = this.tvApply.FindNodeByKeyID(appVo.fappid);
            }
        }
        #endregion

        #region XR
        /// <summary>
        /// GetXR
        /// </summary>
        /// <returns></returns>
        XtraReport GetXR()
        {
            try
            {
                decimal rptId = 0;
                if (this.TypeId == 1)
                    rptId = 24;          // 待赋值 - 暂写死
                else if (this.TypeId == 2)
                    rptId = 25;          // 待赋值 - 暂写死

                EntitySysReport rptVo = null;
                using (ProxyCommon proxy = new ProxyCommon())
                {
                    rptVo = proxy.Service.GetReport(rptId);
                }
                if (rptVo != null && rptVo.rptFile != null)
                {
                    XtraReport xr = new XtraReport();
                    MemoryStream ms = new MemoryStream();
                    ms.Write(rptVo.rptFile, 0, rptVo.rptFile.Length);
                    xr.LoadLayout(ms);
                    xr.DataSource = this.GetXrDataSource();
                    xr.CreateDocument();
                    return xr;
                }
                return null;
            }
            catch (Exception ex)
            {
                DialogBox.Msg(ex.Message);
            }
            return null;
        }

        #region GetXrDataSource
        /// <summary>
        /// GetXrDataSource
        /// </summary>
        /// <returns></returns>
        DataTable GetXrDataSource()
        {
            string xmlData = string.Empty;
            if (this.TypeId == 1)
            {
                xmlData = this.ucBloodApplyForm1.GetContent(); 
            }
            else if (this.TypeId == 2)
            {
                xmlData = this.ucBloodApplyForm2.GetContent(); 
            }             
            if (string.IsNullOrEmpty(xmlData)) return null;

            DataSet ds = weCare.Core.Utils.Function.ReadXml(xmlData);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        #endregion
        
        #endregion

        #endregion

        #region 事件

        private void frmBloodApply_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void blbiApply1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.TypeId = 1;
            this.SetContent(null);
        }

        private void blbiApply2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.TypeId = 2;
            this.SetContent(null);
        }

        private void blbiDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Delete();
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Save();
        }

        private void blbiPut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Put();
        }

        private void blbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void blbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Export();
        }

        private void blbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshTV();
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
