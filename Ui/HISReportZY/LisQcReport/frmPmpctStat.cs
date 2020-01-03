using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.iCare.gui.HIS.Reports;
using Common.Controls;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmPmpctStat : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmPmpctStat()
        {
            InitializeComponent();
        }

        #region
        /// <summary>
        /// 窗体设计器
        /// </summary>
        private clsCtl_PmpctStat m_objController;
        public string DeptIdArr = string.Empty;

        public override void CreateController()
        {
            this.m_objController = new com.digitalwave.iCare.gui.HIS.Reports.clsCtl_PmpctStat();
            this.m_objController.Set_GUI_Apperance(this);
        }
        #endregion


        private void frmPmpctStat_Load(object sender, EventArgs e)
        {
            m_objController.m_mthInit();
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            int intTem = this.xtraTabControl.SelectedTabPageIndex;

            if (intTem == 0)
            {
                m_objController.m_mthGetPmpctStat();
            }

            if (intTem == 1)
            {
                m_objController.m_mthGetPmpctDetail();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            int intTem = this.xtraTabControl.SelectedTabPageIndex;

            if (intTem == 0)
                uiHelper.ExportToXls(gvData);
            else
                uiHelper.ExportToXls(gvDetail);
        }


        private void btnExite_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxGroup_MouseDown(object sender, MouseEventArgs e)
        {
            this.tabContorl.Visible = true;
            this.tabContorl.SelectedTab.Text = "选择检验项目";
            m_objController.m_mthListCheckItem();
        }

        private void cbxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tabContorl.Visible = true;
            this.tabContorl.SelectedTab.Text = "选择检验项目";
            m_objController.m_mthListCheckItem();
        }

        private void dgvItem_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgvItem.CurrentRow != null)
            {
                object[] value = new object[dgvItem.Columns.Count];
                for (int i = 0; i < dgvItem.Columns.Count; i++)
                {
                    value[i] = dgvItem.CurrentRow.Cells[i].Value;
                }

                dgvCheckItem.Rows.Add(value);
            }
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            m_objController.m_mthGetCheckItemByName();
        }

        private void btnTabClose_Click(object sender, EventArgs e)
        {
            this.tabContorl.Visible = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.dgvCheckItem.Rows.Clear();
        }
    }
}
