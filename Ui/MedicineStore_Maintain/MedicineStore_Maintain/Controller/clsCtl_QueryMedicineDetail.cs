using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.gui.MedicineStore;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.Utility;
using com.digitalwave.iCare.common;
using System.IO;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// ҩƷ�������ϸ��ѯ
    /// </summary>
    public class clsCtl_QueryMedicineDetail : com.digitalwave.GUI_Base.clsController_Base
    {
      ��#region ȫ�ֱ���
        /// <summary>
        /// ģ�������
        /// </summary>
        private clsDcl_QueryMedicineDetail m_objDomain = null;
        /// <summary>
        /// ����
        /// </summary>
        private com.digitalwave.iCare.gui.MedicineStore_Maintain.frmQueryMedicineDetail m_objViewer;
        /// <summary>
        /// ҩƷ��ѯ�ؼ�
        /// </summary>
        private ctlQueryMedicintLeastElement m_ctlQueryMedicint = null;


        #endregion

      ��#region ���캯��
        /// <summary>
        /// ҩƷ�������ϸ��ѯ��
        /// </summary>
        public clsCtl_QueryMedicineDetail()
        {
            m_objDomain = new clsDcl_QueryMedicineDetail();
        } 
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������.
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmQueryMedicineDetail)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ȡҩƷ�ֵ���СԪ�ؼ�
        /// <summary>
        /// ��ȡҩƷ�ֵ���СԪ�ؼ�
        /// </summary>
        internal void m_mthGetMedicineInfo()
        {
            clsDcl_QueryMedicineDetail objOSD = new clsDcl_QueryMedicineDetail();
            long lngRes = objOSD.m_lngGetBaseMedicine(string.Empty, m_objViewer.m_strStorageID, out m_objViewer.m_dtbMedicinDict);
            objOSD = null;
        }
        #endregion

        #region ��ʾҩƷ�ֵ���СԪ����Ϣ��ѯ����
        /// <summary>
        /// ��ʾҩƷ�ֵ���СԪ����Ϣ��ѯ����
        /// </summary>
        /// <param name="p_strSearchCon">��ѯ����</param>
        internal void m_mthShowQueryMedicineForm(string p_strSearchCon)
        {
            if (m_ctlQueryMedicint == null)
            {

                m_ctlQueryMedicint = new ctlQueryMedicintLeastElement(m_objViewer.m_dtbMedicinDict);
                m_objViewer.Controls.Add(m_ctlQueryMedicint);

                int X = m_objViewer.m_txtMedicine.Location.X;// -(m_ctlQueryMedicint.Size.Width - m_objViewer.m_txtMedicineCode.Size.Width);
                int Y = m_objViewer.m_txtMedicine.Location.Y + m_objViewer.m_txtMedicine.Size.Height;

                m_ctlQueryMedicint.Location = new System.Drawing.Point(X, Y);

                m_ctlQueryMedicint.ReturnInfo += new ReturnMedicineInfo(frmQueryForm_ReturnInfo);
                m_ctlQueryMedicint.CancelResult += new MecicineCancelAndReturn(m_ctlQueryMedicint_CancelResult);
            }

            m_ctlQueryMedicint.Visible = true;
            m_ctlQueryMedicint.BringToFront();
            m_ctlQueryMedicint.Focus();
            m_ctlQueryMedicint.m_mthSetSearchText(p_strSearchCon);
        }

        internal void m_ctlQueryMedicint_CancelResult()
        {
            m_objViewer.m_txtMedicine.Focus();
        }

        internal void frmQueryForm_ReturnInfo( clsMS_MedicintLeastElement_VO MS_VO)
        {
            if (MS_VO == null)
            {
                return;
            }

            m_objViewer.m_txtMedicine.Tag = MS_VO.m_strMedicineID;
            m_objViewer.m_txtMedicine.Text = MS_VO.m_strMedicineName;
            m_objViewer.m_txtMEDSPEC.Text = MS_VO.m_strMedicineSpec;
            m_objViewer.m_txtPACKUNIT.Text = MS_VO.m_strMedicineUnit;
            m_objViewer.cmdQuery.Focus();
        }
        #endregion

        #region ��ȡҩƷ�������ϸ
        internal void m_mthGetQueryMedicineDetail()
        {
            clsMS_QueryMedicineDetailVO clsQuerVO = new clsMS_QueryMedicineDetailVO();
            long lngRes = m_objDomain.m_lngGetQueryMedicineDetail(Convert.ToDateTime(m_objViewer.m_dtpSearchBeginDate.Text + " 00:00:00"), Convert.ToDateTime(m_objViewer.m_dtpSearchEndDate.Text + " 23:59:59"),
                 m_objViewer.m_strStorageID, m_objViewer.m_txtMedicine.Tag.ToString(), out m_objViewer.m_dtbMedicint, out clsQuerVO);
            m_objViewer.datWindow.LibraryList = clsPublic.PBLPath;
            m_objViewer.datWindow.Modify("t_date.text='" + m_objViewer.m_dtpSearchBeginDate.Text + " ~ " + m_objViewer.m_dtpSearchEndDate.Text + "'");

            m_objViewer.datWindow.Modify("m_dblInAmount.text='" + clsQuerVO.m_dblInAmount.ToString() + "'");
            m_objViewer.datWindow.Modify("m_dblOutAmount.text='" + clsQuerVO.m_dblOutAmount.ToString() + "'");
            m_objViewer.datWindow.Modify("m_dblOutStorageAmount.text='" + clsQuerVO.m_dblOutStorageAmount.ToString() + "'");
            m_objViewer.datWindow.Modify("m_dblInStorageAmount.text='" + clsQuerVO.m_dblInStorageAmount.ToString() + "'");
            m_objViewer.datWindow.Modify("m_dblRejectAmount.text='" + clsQuerVO.m_dblRejectAmount.ToString() + "'");
            m_objViewer.datWindow.Modify("m_dblCheckAmount.text='" + clsQuerVO.m_dblCheckAmount.ToString() + "'");
            m_objViewer.datWindow.Modify("m_dblNewGross.text='" + clsQuerVO.m_dblNewGross.ToString() + "'");
            m_objViewer.datWindow.Modify("m_dblOldGross2.text='" + clsQuerVO.m_dblOldGross.ToString() + "'");


            if (clsQuerVO.m_dblQcAmount != 0)
            {
                m_objViewer.datWindow.Modify("m_dblBeginAmount.text='�ڳ�������" + clsQuerVO.m_dblQcAmount.ToString() + "'");
            }
            else
            {
                m_objViewer.datWindow.Modify("m_dblBeginAmount.text=''");
            }
            m_objViewer.datWindow.Modify("m_dblRetailPriceAmount.text='" + clsQuerVO.m_dblRetailPriceAmount.ToString() + "'");
            //m_dtbMedicint.defaultview.Sort = 
            m_objViewer.datWindow.SetRedrawOff();
            m_objViewer.datWindow.Retrieve(m_objViewer.m_dtbMedicint);
            m_objViewer.datWindow.SetRedrawOn();
            m_objViewer.datWindow.Refresh();

        }
        #endregion

        #region ��ӡ
        internal void m_mthPrintDialog()
        {
            clsCtl_Public clsPub = new clsCtl_Public();
            clsPub.ChoosePrintDialog(m_objViewer.datWindow, true);

        }
        #endregion

        #region ��datatable������excel
        /// <summary>
        /// ��datatable������excel
        /// </summary>
        /// <param name="p_dt"></param>
        public void m_mthOutExcel(DataTable p_dt)
        {
            int intCount = 0;
            intCount++;
            DataTable dttemp = new DataTable("Table" + intCount.ToString());
            string str = "";
            for (int i = 0; i < p_dt.Columns.Count; i++)
            {
                str = p_dt.Columns[i].ColumnName.Replace("(", "");
                str = str.Replace(")", "");
                dttemp.Columns.Add(str, p_dt.Columns[i].DataType);
            }
            DataRow dr = null;
            for (int i = 0; i < p_dt.Rows.Count; i++)
            {
                dr = dttemp.NewRow();
                for (int i2 = 0; i2 < p_dt.Columns.Count; i2++)
                {
                    dr[i2] = p_dt.Rows[i][i2];
                }
                dttemp.Rows.Add(dr);
            }
            DataSet ds = new DataSet();
            ds.Tables.Clear();
            ds.Tables.Add(dttemp);
            ExcelExporter excel = new ExcelExporter(ds);
            bool b = excel.m_mthExport();
            if (b)
            {
                MessageBox.Show("�������ݳɹ�!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("��������ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            ds.Tables.Clear();
            dttemp = null;
            ds = null;
        }
        #endregion
    }
}
