using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    public partial class frmQueryMedicineDetail : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// �ֿ�ID
        /// </summary>
        internal string m_strStorageID = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public DataTable m_dtbMedicint;
        /// <summary>
        /// ҩƷ�ֵ�
        /// </summary>
        internal DataTable m_dtbMedicinDict = null;
        public frmQueryMedicineDetail()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_strReportName">��������</param>
        public void ShowThis(string p_strStorageID,string p_strReportName)
        {
            m_strStorageID = p_strStorageID;
            datWindow.LibraryList = clsPublic.PBLPath;
            datWindow.DataWindowObject = p_strReportName == "" ? "querymedicinedetail" : "querymedicinedetail_" + p_strReportName;
            this.Show();
        }

        #region ���ô��������.
        /// <summary>
        /// ���ط���,���ô��������.
        /// </summary>
        public override void CreateController()
        {
            this.objController = new clsCtl_QueryMedicineDetail();
            objController.Set_GUI_Apperance(this);
        }
        #endregion

        private void m_txtMedicine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_QueryMedicineDetail)objController).m_mthShowQueryMedicineForm(m_txtMedicine.Text.Trim());

            }
        }

        private void frmQueryMedicineDetail_Load(object sender, EventArgs e)
        {
            m_dtpSearchBeginDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy��MM��dd��");
            m_dtpSearchEndDate.Text = DateTime.Now.ToString("yyyy��MM��dd��");
            ((clsCtl_QueryMedicineDetail)objController).m_mthGetMedicineInfo();
           
        }

        private void cmdQuery_Click(object sender, EventArgs e)
        {
            if (m_txtMedicine.Text.Trim() == "")
            {
                MessageBox.Show("����ѡ��ҩƷ!", "ҩƷ�������ϸ��ѯ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ((clsCtl_QueryMedicineDetail)objController).m_mthGetQueryMedicineDetail();
        }
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ((clsCtl_QueryMedicineDetail)objController).m_mthPrintDialog();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("��������", typeof(System.DateTime));
            dt.Columns.Add("���ݺ�", typeof(System.String));
            dt.Columns.Add("ժҪ", typeof(System.String));
            dt.Columns.Add("����", typeof(System.String));
            dt.Columns.Add("�����", typeof(System.Double));
            dt.Columns.Add("��Ч��", typeof(System.String));
            dt.Columns.Add("����", typeof(System.String));
            dt.Columns.Add("�跽����", typeof(System.Double));
            dt.Columns.Add("�跽���ۼ�", typeof(System.Double));
            dt.Columns.Add("�跽���", typeof(System.Double));
            dt.Columns.Add("��������", typeof(System.Double));
            dt.Columns.Add("�������ۼ�", typeof(System.Double));
            dt.Columns.Add("�������", typeof(System.Double));
            dt.Columns.Add("�������", typeof(System.Double));
            //dt.Columns.Add("������ۼ�", typeof(System.Double));
            dt.Columns.Add("�����", typeof(System.Double));
            if (m_dtbMedicint.Rows.Count > 0)
            {
                double dblInAmount=0;
                double dblInRetailprice=0;
                double dblOutAmount=0;
                double dblOutRetailprice=0;
                double dblAmount = 0;
                double dblRetailprice = 0; 
                
                for (int i1 = 0; i1 < m_dtbMedicint.Rows.Count; i1++)
                {

                    DataRow row = dt.NewRow();
                    DataRow dtbMedRow = m_dtbMedicint.Rows[i1];

                    double.TryParse(dtbMedRow["inAmount_int"].ToString(), out dblInAmount);
                    double.TryParse(dtbMedRow["inRetailprice_int"].ToString(), out dblInRetailprice);
                    double.TryParse(dtbMedRow["outAmount_int"].ToString(), out dblOutAmount);
                    double.TryParse(dtbMedRow["outRetailprice_int"].ToString(), out dblOutRetailprice);
                    double.TryParse(dtbMedRow["oldgross_int"].ToString(), out dblAmount);
                    double.TryParse(dtbMedRow["balance"].ToString(), out dblRetailprice);

                    row["��������"] = dtbMedRow["operatedate_dat"];
                    row["���ݺ�"] = dtbMedRow["chittyid_vchr"];
                    row["ժҪ"] = dtbMedRow["brief_vchr"];
                    row["����"] = dtbMedRow["lotno_vchr"];
                    row["�����"] = dtbMedRow["callprice_int"];
                    row["��Ч��"] = dtbMedRow["validperiod_chr"];
                    row["����"] = dtbMedRow["productorid_chr"];

                    row["�跽����"] = dblInAmount;
                    row["�跽���ۼ�"] = dblInRetailprice;
                    row["�跽���"] = dblInAmount * dblInRetailprice;

                    row["��������"] = dblOutAmount;
                    row["�������ۼ�"] = dblOutRetailprice;
                    row["�������"] = dblOutAmount * dblOutRetailprice;

                    row["�������"] = dblAmount;
                    //row["������ۼ�"] = dblRetailprice;
                    row["�����"] = dtbMedRow["oldmoney"];

                    dt.Rows.Add(row);
                }
            }
            ((clsCtl_QueryMedicineDetail)this.objController).m_mthOutExcel(dt);
        }

        private void m_txtMedicine_MouseDown(object sender, MouseEventArgs e)
        {
            m_txtMedicine.SelectAll();
        }
    }
}