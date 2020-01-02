using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    public partial class frmRptNusingForCS : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmRptNusingForCS()
        {
            InitializeComponent();
        }

        public void m_mthShow(string p_strPatSorce)
        {
            m_arrSorce = clsPublic.m_ArrGettoken(p_strPatSorce, ";");
            this.Show();
        }

        private void buttonXP2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<string> m_arrSorce = null;//��Ժ���� ��Ժ����
        private ArrayList m_arrDeptID = null;
        private string m_strDeptName = "";
        private clsCtl_ReportZY m_objZy = null;

        private void btnDept_Click(object sender, EventArgs e)
        {
            frmAidDeptList frmTmp = new frmAidDeptList();
            frmTmp.ShowDialog();
            if (frmTmp.DialogResult == DialogResult.OK)
            {
                m_arrDeptID = frmTmp.DeptIDArr;
                m_strDeptName = frmTmp.DeptName;
            }
        }

        private void frmRptNusingForCS_Load(object sender, EventArgs e)
        {
            this.dw.LibraryList = clsPublic.PBLPath;
            this.dw.DataWindowObject = "d_bih_nursinglog";
            this.dw.InsertRow(0);
            this.m_objZy = new clsCtl_ReportZY();
            this.dtpMonth.Value = DateTime.Now;
            this.Text = Common.Entity.GlobalParm.HospitalName + "��������ͳ�Ʊ�";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (m_arrDeptID == null || m_arrDeptID.Count < 1)
            {
                return;
            }

            if (m_arrDeptID.Count > 1)
            {
                MessageBox.Show(this, "Ŀǰֻ�ܼ���һ�����������ݡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //this.m_objZy.m_mthGetNusingLog(this.dtpMonth.Value, m_arrDeptID[0].ToString(), this.dw, this.m_arrSorce);
            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsPublic.PlayAvi("���ڼ������ݣ����Ժ�.....");
                this.m_objZy.m_mthGetNusingLog(this.dtpMonth.Value, m_arrDeptID[0].ToString(), this.dw, this.m_arrSorce);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "����ʧ�ܣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                clsPublic.CloseAvi();
                this.Cursor = Cursors.Default;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dw.RowCount > 0)
            {
                clsPublic.ExportDataWindow(this.dw, null);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.dw.PrintProperties.Preview = !this.dw.PrintProperties.Preview;
            this.dw.PrintProperties.ShowPreviewRulers = this.dw.PrintProperties.Preview;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (dw.RowCount > 0)
            {
                clsPublic.ChoosePrintDialog(this.dw, false);
            }
        }

    }
}