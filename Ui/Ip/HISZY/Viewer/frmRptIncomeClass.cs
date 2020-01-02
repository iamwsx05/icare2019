using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlLibrary;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ȫԺ�ɿ���౨��UI��
    /// </summary>
    public partial class frmRptIncomeClass : Form //: com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ����
        /// <summary>
        /// ����ҵ����
        /// </summary>
        private clsCtl_Report objReport;
        /// <summary>
        /// �������� 0 �ɿ����(�ѽ�) 1 ��Ʊ����(����)
        /// </summary>
        private int RepType = 0;
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        public frmRptIncomeClass()
        {
            InitializeComponent();
            objReport = new clsCtl_Report();
        }

        /// <summary>
        /// �ⲿ�ӿ�(ȫԺ��Ʊ����ͳ�Ʊ���)
        /// </summary>
        internal string str_parmval = "";
        public void mthShow(string ParmVal)
        {
            str_parmval = ParmVal;
            RepType = 1;
            this.Show();
        }

        private void frmRptIncomeClass_Load(object sender, EventArgs e)
        {
            this.dwRep.LibraryList = clsPublic.PBLPath;
            this.dwRep.DataWindowObject = "d_bih_incomeclass";
            this.dwRep.InsertRow(0);

            if (RepType == 0)
            {
                dwRep.Modify("t_title.text = '" + this.objReport.HospitalName + "סԺ�ɿ���౨��'");                
            }
            else if (RepType == 1)
            {
                dwRep.Modify("t_title.text = '" + this.objReport.HospitalName + "סԺ��Ʊ���౨��'");

                this.Text = "סԺ��Ʊ���౨��";
                this.lblDate.Text = "��Ʊ���ڣ���";
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.dwRep.PrintProperties.Preview = !this.dwRep.PrintProperties.Preview;
            this.dwRep.PrintProperties.ShowPreviewRulers = this.dwRep.PrintProperties.Preview;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            clsPublic.ChoosePrintDialog(this.dwRep, true);
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            if (!clsPublic.m_blnCheckDateRange(str_parmval, this.dteBegin.Value.ToString("yyyy-MM-dd"), this.dteEnd.Value.ToString("yyyy-MM-dd")))
            {
                return;
            }
            string BeginDate = this.dteBegin.Value.ToString("yyyy-MM-dd");
            string EndDate = this.dteEnd.Value.ToString("yyyy-MM-dd");
                        
            clsPublic.PlayAvi("findFILE.avi", "�������ɷ����嵥�����Ժ�...");            
            this.objReport.m_mthRptIncomeClass(BeginDate, EndDate, RepType, this.dwRep);
            clsPublic.CloseAvi();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.dwRep.RowCount > 0)
            {
                clsPublic.ExportDataWindow(this.dwRep, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }    
}