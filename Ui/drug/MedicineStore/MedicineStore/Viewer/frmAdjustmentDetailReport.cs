using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    /// <summary>
    /// ҩƷ���������
    /// </summary>
    public partial class frmAdjustmentDetailReport : Form
    {
        /// <summary>
        /// ��ӡ����
        /// </summary>
        public DataTable dtbPrin;

        /// <summary>
        /// �Ƶ���
        /// </summary>
        public string p_strCREATORID_CHR;
        /// <summary>
        /// ������
        /// </summary>
        public string p_strEXAMERID_CHR;
        /// <summary>
        /// ��ע
        /// </summary>
        public string p_strCOMMENT_VCHR;
        /// <summary>
        /// ���ţ���ҩ�⣩
        /// </summary>
        public string p_strStorageName;
        /// <summary>
        /// ҩƷ���������
        /// </summary>
        public frmAdjustmentDetailReport()
        {
            InitializeComponent();
            datWindow.LibraryList = clsMedicineStoreFormFactory.PBLPath;
        }

        private void frmAdjustmentDetailReport_Load(object sender, EventArgs e)
        {
            datWindow.Modify("t_creator.text='" + p_strCREATORID_CHR + "'");
            datWindow.Modify("t_examerid.text='" + p_strEXAMERID_CHR + "'");
            datWindow.Modify("t_comment.text='" + p_strCOMMENT_VCHR + "'");
            datWindow.Modify("t_storageid_chr.text = '" + p_strStorageName + "'");

            if (datWindow.DataWindowObject == "adjustment_lj")
            {
                DataRow dro;
                if (dtbPrin.Rows.Count % 7 != 0)
                {
                    int ros = 7 - dtbPrin.Rows.Count % 7;
                    int valCount = dtbPrin.Rows.Count + ros;
                    for (int i = 0; i < ros; i++)
                    {
                        dro = dtbPrin.NewRow();
                        //dro["seriesid2_int"] = i;
                        dtbPrin.Rows.Add(dro);
                    }
                }
            }

            datWindow.Retrieve(dtbPrin);
           
            datWindow.PrintProperties.Preview = true;
        }

        private void m_cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            clsCtl_Public clsPub = new clsCtl_Public();
            clsPub.ChoosePrintDialog(datWindow, true);
        }
    }
}