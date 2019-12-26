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
    public partial class frmRptMedicineDetailReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// �ⷿID
        /// </summary>
        internal string m_strStorageID;
        /// <summary>
        /// �ⷿ����
        /// </summary>
        internal string m_strStorageName;
        internal DataTable m_dtMedince;
        internal DataTable m_dtStorageName;
        /// <summary>
        /// �Ƿ�ҩ��ʹ��
        /// </summary>
        internal bool m_blnIsDS = false;
        /// <summary>
        /// �Ƿ�סԺҩ��ʹ��
        /// </summary>
        internal bool m_blnIsHospital;
        /// <summary>
        /// �ܽ��
        /// </summary>
        internal double m_dblSumMoney;
        /// <summary>
        /// ҩƷ����
        /// </summary>
        internal string m_strMedicineName = string.Empty;
        /// <summary>
        /// ҩƷ���
        /// </summary>
        internal string m_strMedicineSpec = string.Empty;
        public frmRptMedicineDetailReport()
        {
            InitializeComponent();
        }

        public override void CreateController()
        {
            objController = new clsCtl_RptMedicineDetailReport();
            objController.Set_GUI_Apperance(this);
        }

        /// <summary>
        /// ҩ�����÷���
        /// </summary>
        /// <param name="p_strStorageid"></param>
        public void m_mthShow(string p_strStorageid)
        {
            m_strStorageID = p_strStorageid;
            m_blnIsDS = true;
            ((clsCtl_RptMedicineDetailReport)objController).m_lngCheckIsHospital(m_strStorageID, out m_blnIsHospital);
            this.Show();
        }

        /// <summary>
        /// ҩ����÷���
        /// </summary>
        /// <param name="p_strStorageID"></param>
        public void ShowThis(string p_strStorageID)
        {
            m_strStorageID = p_strStorageID;
            this.Show();
        }


        private void frmRptInstorageStat_Load(object sender, EventArgs e)
        {
            this.m_dgvMedDetail.AutoGenerateColumns = false;
            this.m_dtpSearchBeginDate.Text = clsPublic.CurrentDateTimeNow.AddMonths(-1).ToString("yyyy��MM��dd�� 00:00:00");
            this.m_dtpSearchEndDate.Text = clsPublic.CurrentDateTimeNow.ToString("yyyy��MM��dd�� HH:mm:ss");

            ((clsCtl_RptMedicineDetailReport)this.objController).m_mthInit();
            ((clsCtl_RptMedicineDetailReport)this.objController).m_mthGetStorageName(out m_dtStorageName);
           
            if (m_dtStorageName.Rows.Count > 0)
            {
                for (int j2 = 0; j2 < m_dtStorageName.Rows.Count; j2++)
                {
                    if (m_dtStorageName.Rows[j2]["storageid_chr"].ToString() == m_strStorageID)
                    {
                        m_strStorageName = m_dtStorageName.Rows[j2]["storagename_vchr"].ToString();
                    }
                }
            }

            this.Text += "(" + m_strStorageName + ")";

            if (m_blnIsHospital)
            {
                m_dgvMedDetail.Columns["patientcardid_chr"].HeaderText = "סԺ��";
            }
        }

        private void m_cmdSearch_Click(object sender, EventArgs e)
        {            
            ((clsCtl_RptMedicineDetailReport)this.objController).m_mthSearch();            
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            if (this.dw.RowCount > 0)
            {
                com.digitalwave.iCare.gui.MedicineStore.clsCtl_Public clsPub = new com.digitalwave.iCare.gui.MedicineStore.clsCtl_Public();
                clsPub.ChoosePrintDialog(this.dw, true);
            }
        }

        private void m_cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_txtMedicineCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ((clsCtl_RptMedicineDetailReport)this.objController).m_mthSearch();
        }
        
        private void m_txtMedicineCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (m_dtMedince == null || m_dtMedince.Rows.Count == 0)
                {
                    ((clsCtl_RptMedicineDetailReport)this.objController).m_mthGetMedBaseInfo(m_strStorageID, out m_dtMedince);
                }
                ((clsCtl_RptMedicineDetailReport)this.objController).m_mthShowMedince(this.m_txtMedicineCode.Text.Trim());
            }
        }

        private void m_cmdExport_Click(object sender, EventArgs e)
        {
            //SaveFileDialog frmSFD = new SaveFileDialog();
            //frmSFD.Filter = "Excel�ļ�|*.xls";
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
            com.digitalwave.iCare.gui.MedicineStore.clsCtl_Public.PrintDialog(this.dw);
        }

        private void m_txtMedicineCode_TextChanged(object sender, EventArgs e)
        {
            if (m_txtMedicineCode.Text.Trim() == "")
                m_txtMedicineCode.Tag = "";
        }

        private void m_dgvMedDetail_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.m_dgvMedDetail.Columns[e.ColumnIndex].DisplayIndex > 0)
            {
                --this.m_dgvMedDetail.Columns[e.ColumnIndex].DisplayIndex;
            }
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
            ((clsCtl_RptMedicineDetailReport)objController).m_mthAddTotalSumRow(dt);
            dt.Rows[dt.Rows.Count - 1]["SortRowNo"] = dt.Rows.Count;
            dt.AcceptChanges();
            this.m_dgvMedDetail.Sort(m_dgvMedDetail.Columns["m_dgvSortRowNo"], ListSortDirection.Ascending);
        }

        private void m_rbtCombine_CheckedChanged(object sender, EventArgs e)
        {
            m_txtMedicineCode.Clear();
            m_txtMedicineCode.Tag = "";
            m_txtMedicineCode.Focus();
        }
    }
}