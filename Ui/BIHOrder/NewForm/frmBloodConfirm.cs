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

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// 审核发送
    /// </summary>
    public partial class frmBloodConfirm : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmBloodConfirm()
        {
            InitializeComponent();
        }
        #endregion

        #region 变量.属性

        #endregion

        #region method

        #region Query
        /// <summary>
        /// Query
        /// </summary>
        void Query()
        {
            List<EntityParm> lstParm = new List<EntityParm>();
            lstParm.Add(new EntityParm() { key = "issend", value = "" });

            string ipNo = this.txtIpNo.Text.Trim();
            if (ipNo != "") lstParm.Add(new EntityParm() { key = "ipno", value = ipNo });

            string startDate = this.dteStart.Text.Trim();
            string endDate = this.dteEnd.Text.Trim();
            if (startDate != "" && endDate != "")
            {
                if (Convert.ToDateTime(startDate) > Convert.ToDateTime(endDate))
                {
                    DialogBox.Msg("开始时间不能大于结束时间。");
                    this.dteStart.Focus();
                    return;
                }
                lstParm.Add(new EntityParm() { key = "appdate", value = startDate + "|" + endDate });
            }

            int statusIdx = this.cboStatus.SelectedIndex;
            if (statusIdx > 0)
            {
                if (statusIdx == 2) statusIdx = 3;
                lstParm.Add(new EntityParm() { key = "appstatus", value = statusIdx.ToString() });
            }

            try
            {
                com.digitalwave.iCare.gui.HIS.clsPublic.PlayAvi("");
                this.gcApply.DataSource = (new weCare.Proxy.ProxyIP()).Service.GetBloodApply(lstParm);
            }
            finally
            {
                com.digitalwave.iCare.gui.HIS.clsPublic.CloseAvi();
            }
        }
        #endregion

        #region PopupPatient
        /// <summary>
        /// PopupPatient
        /// </summary>
        /// <param name="vo"></param>
        void PopupPatient(EntityBloodApply vo)
        {
            frmBloodPopup frm = new frmBloodPopup(vo);
            frm.ShowDialog();
        }
        #endregion

        #region AppBack
        /// <summary>
        /// AppBack
        /// </summary>
        void AppBack()
        {
            if (this.gvApply.FocusedRowHandle >= 0)
            {
                EntityBloodApply vo = this.gvApply.GetRow(this.gvApply.FocusedRowHandle) as EntityBloodApply;
                if (vo.fappid > 0)
                {
                    if (DialogBox.Msg("是否退回该用血申请吗？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if ((new weCare.Proxy.ProxyIP()).Service.BackBloodApply(vo.fappid, LoginInfo.m_strEmpID, "") > 0)
                        {
                            this.Query();
                            DialogBox.Msg("退回成功！");
                        }
                        else
                        {
                            DialogBox.Msg("退回失败。");
                        }
                    }
                }
            }
        }
        #endregion

        #endregion

        #region event

        private void frmBloodConfirm_Load(object sender, EventArgs e)
        {

        }

        private void gcApply_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.gvApply.FocusedRowHandle >= 0)
                this.PopupPatient(this.gvApply.GetRow(this.gvApply.FocusedRowHandle) as EntityBloodApply);
        }

        private void gvApply_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Appearance.ForeColor = Color.Gray;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void blbiQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Query();
        }

        private void blbiSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void blbiBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AppBack();
        }

        private void blbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Common.Controls.uiHelper.Print(this.gcApply);
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
