using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlLibrary;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    public partial class frmRecipeByIdentityReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// 药房Id
        /// </summary>
        internal string m_strStorageid;
        internal string m_strStorageName = string.Empty;
        public frmRecipeByIdentityReport()
        {
            InitializeComponent();
        }

        public override void CreateController()
        {
            objController = new clsCtl_RecipeByIdentityReport();
            objController.Set_GUI_Apperance(this);
        }

        public void m_mthShow(string p_strStorageid)
        {
            m_strStorageid = p_strStorageid;
            clsPublic.m_lngGetStorageName(true, p_strStorageid, out m_strStorageName);

            this.Text = "处方出库按身份统计(" + m_strStorageName + ")";
            this.Show();
        }

        private void frmRptInstorageStat_Load(object sender, EventArgs e)
        {
            this.m_dgvMedDetail.AutoGenerateColumns = false;
            this.m_dtpSearchBeginDate.Text = clsPublic.SysDateTimeNow.ToString("yyyy年MM月dd日 00时00分00秒");
            this.m_dtpSearchEndDate.Text = clsPublic.SysDateTimeNow.ToString("yyyy年MM月dd日 HH时mm分ss秒");

            ((clsCtl_RecipeByIdentityReport)this.objController).m_mthInit();
           
        }

        private void m_cmdSearch_Click(object sender, EventArgs e)
        {
            ((clsCtl_RecipeByIdentityReport)this.objController).m_mthSearch();
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            if (this.dw.RowCount > 0)
            {
                com.digitalwave.iCare.gui.HIS.clsPublic.ChoosePrintDialog(this.dw, true);
            }
        }

        private void m_cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_cmdExport_Click(object sender, EventArgs e)
        {
            //SaveFileDialog frmSFD = new SaveFileDialog();
            //frmSFD.Filter = "Excel文件|*.xls";
            //if (frmSFD.ShowDialog() == DialogResult.OK)
            //{
            //    if (frmSFD.FileName != string.Empty)
            //        dw.SaveAs(frmSFD.FileName, Sybase.DataWindow.FileSaveAsType.Excel);
            //}
            if (this.m_dgvMedDetail.Rows.Count > 0)
            {
                com.digitalwave.iCare.gui.HIS.clsPub.m_mthExportToExcel(this.m_dgvMedDetail);
            }
        }

        private void m_cmdPreview_Click(object sender, EventArgs e)
        {
            //dw.PrintProperties.Preview = !dw.PrintProperties.Preview;
            //dw.PrintProperties.ShowPreviewRulers = !dw.PrintProperties.ShowPreviewRulers;
            com.digitalwave.iCare.gui.HIS.clsPublic.PrintDialog(this.dw);
        }

        private void m_dgvMedDetail_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.m_dgvMedDetail.DataSource == null) return;
            DataTable dt = this.m_dgvMedDetail.DataSource as DataTable;
            if (dt.Rows.Count == 0) return;
            dt.Rows.Remove(dt.Rows[dt.Rows.Count - 1]);
            dt.AcceptChanges();
            DataGridViewColumn dgvColumn = this.m_dgvMedDetail.Columns[e.ColumnIndex];
            dgvColumn.Tag = (dgvColumn.Tag == null || (ListSortDirection)dgvColumn.Tag == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
            this.m_dgvMedDetail.Sort(dgvColumn, (ListSortDirection)dgvColumn.Tag);

            for (int i = 0; i < this.m_dgvMedDetail.Rows.Count; i++)
            {
                DataRowView drv = this.m_dgvMedDetail.Rows[i].DataBoundItem as DataRowView;
                DataRow dr = drv.Row;
                dr.BeginEdit();
                dr["SortRowNo"] = i;
                dr.EndEdit();
            }
            ((clsCtl_RecipeByIdentityReport)objController).m_mthAddTotalSumRow(dt);
            dt.Rows[dt.Rows.Count - 1]["SortRowNo"] = dt.Rows.Count;
            dt.AcceptChanges();
            this.m_dgvMedDetail.Sort(m_dgvMedDetail.Columns["m_dgvSortRowNo"], ListSortDirection.Ascending);
        }
    }
}