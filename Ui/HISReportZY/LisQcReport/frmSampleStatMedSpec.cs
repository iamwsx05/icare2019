﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.iCare.gui.HIS.Reports;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmSampleStatMedSpec : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmSampleStatMedSpec()
        {
            InitializeComponent();
        }

        #region
        /// <summary>
        /// 窗体设计器
        /// </summary>
        private clsCtl_SampleStatMedSpec m_objController;
        public string DeptIdArr = string.Empty;

        public override void CreateController()
        {
            this.m_objController = new com.digitalwave.iCare.gui.HIS.clsCtl_SampleStatMedSpec();
            this.m_objController.Set_GUI_Apperance(this);
        }
        #endregion


        private void frmSampleStatMedSpec_Load(object sender, EventArgs e)
        {
            m_objController.m_mthInit();
        }

        private void btnByDept_Click(object sender, EventArgs e)
        {
            frmByDept frmAidChooseDept = new frmByDept();
            if (frmAidChooseDept.ShowDialog() == DialogResult.OK)
            {
                this.DeptIdArr = frmAidChooseDept.DeptIDArr;
            }
        }

        private void dgvItem_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgvItem.CurrentRow != null)
            {
                {
                    object[] value = new object[dgvItem.Columns.Count];
                    for (int i = 0; i < dgvItem.Columns.Count; i++)
                    {
                        value[i] = dgvItem.CurrentRow.Cells[i].Value;
                    }

                    dgvCheckItem.Rows.Add(value);
                }
            }
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            int intTem = this.xtraTabControl.SelectedTabPageIndex;

            //"检验标本周转中位数统计表", "检验标本周转中位数明细" 
            
            if (intTem == 0)
            {
                m_objController.m_mthGeSampleStatSpec2();
            }
            if (intTem == 1)
            {
                m_objController.m_mthGeSampleStatSpecStat();
            }
        }

        private void tabColse_Click(object sender, EventArgs e)
        {
            this.tabContorl.Visible = false;
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            m_objController.m_mthGetCheckItemByName();
        }

        private void cbxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tabContorl.Visible = true;
            this.tabContorl.SelectedTab.Text = "选择检验项目";
            m_objController.m_mthListCheckItem();
        }

        private void cbxGroup_MouseDown(object sender, MouseEventArgs e)
        {
            this.tabContorl.Visible = true;
            this.tabContorl.SelectedTab.Text = "选择检验项目";
            m_objController.m_mthListCheckItem();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            m_objController.m_mthExportToExcel();
        }


        private void buttonXP4_Click(object sender, EventArgs e)
        {
            m_objController.Closed();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            m_objController.Clear();
        }

        private void btnTabClose_Click(object sender, EventArgs e)
        {
            this.tabContorl.Visible = false;
        }
    }
}
