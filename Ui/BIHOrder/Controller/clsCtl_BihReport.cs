using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 
using com.digitalwave.iCare.BIHOrder.Control; 
using System.Collections;

namespace com.digitalwave.iCare.BIHOrder
{
    class clsCtl_BihReport : com.digitalwave.GUI_Base.clsController_Base
    {

          #region ����
        clsDCl_BihReport m_objManager = null;
        
        #endregion 

        #region ���캯��
        public clsCtl_BihReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
            m_objManager = new clsDCl_BihReport();
			
		}
		#endregion 

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmReport_Treat m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmReport_Treat)frmMDI_Child_Base_in;

        }
        #endregion


        internal void LoadData()
        {
            long lngRes = 0;
            DataTable table1 = new DataTable();
            lngRes = m_objManager.m_lngGetORDERCHARGEDEPT(this.m_objViewer.LoginInfo.m_strInpatientAreaID,"","8",DateTime.Now, out table1);
            bindTheData(table1);
          
        }

        private void bindTheData(DataTable table1)
        {
          
            m_objViewer.dw_1.Reset();
            int newRow; DateTime executedate_dat, INPATIENT_DAT;

            m_objViewer.dw_1.Modify("area_name.text='"+this.m_objViewer.LoginInfo.m_strInpatientAreaName+"'");
            m_objViewer.dw_1.Modify("execute_dat.text='" + DateTime.Now.ToString("yyyy.MM.dd")+"'");
           
             for (int i = 0; i < table1.Rows.Count; i++)
             {
                 newRow = m_objViewer.dw_1.InsertRow();
                 m_objViewer.dw_1.SetItemString(newRow, "col1", table1.Rows[i]["bed_no"].ToString().Trim());
                 m_objViewer.dw_1.SetItemString(newRow, "col2", table1.Rows[i]["LASTNAME_VCHR"].ToString().Trim());
                 m_objViewer.dw_1.SetItemString(newRow, "col3", table1.Rows[i]["RECIPENO_INT"].ToString().Trim());
                 m_objViewer.dw_1.SetItemString(newRow, "col4", table1.Rows[i]["item_name"].ToString().Trim());
                 m_objViewer.dw_1.SetItemString(newRow, "col5", table1.Rows[i]["DOSAGE_DEC"].ToString().Trim() + " " + table1.Rows[i]["Dosageunit_Chr"].ToString().Trim());
                 m_objViewer.dw_1.SetItemString(newRow, "col6", table1.Rows[i]["FREQNAME"].ToString().Trim());
                 m_objViewer.dw_1.SetItemString(newRow, "col7", table1.Rows[i]["Dosetypename_Chr"].ToString().Trim());
                 m_objViewer.dw_1.SetItemString(newRow, "col8", table1.Rows[i]["Entrust_Vchr"].ToString().Trim());
                 m_objViewer.dw_1.SetItemString(newRow, "col9", "");
                 
                 try
                 {
                     executedate_dat =Convert.ToDateTime(table1.Rows[i]["executedate_dat"].ToString().Trim());
                     TimeSpan span1 = executedate_dat - System.DateTime.Now;
                     if (span1.Days == 0)
                     {
                         m_objViewer.dw_1.SetItemString(newRow, "col10", "��");
                     }
                     else
                     {
                         m_objViewer.dw_1.SetItemString(newRow, "col10", "");
                     }
                 }
                 catch
                 {
                     m_objViewer.dw_1.SetItemString(newRow, "col10", "");
                 }
                 try
                 {
                     INPATIENT_DAT = Convert.ToDateTime(table1.Rows[i]["INPATIENT_DAT"].ToString().Trim());
                     TimeSpan span1 = INPATIENT_DAT - System.DateTime.Now;
                     if (span1.Days == 0)
                     {
                         m_objViewer.dw_1.SetItemString(newRow, "col11", "����Ժ");
                     }
                     else
                     {
                         m_objViewer.dw_1.SetItemString(newRow, "col11", "");
                     }
                 }
                 catch
                 {
                     m_objViewer.dw_1.SetItemString(newRow, "col11", "");
                 }
               

             }


             m_objViewer.dw_1.AcceptText();
             m_objViewer.dw_1.Sort();
             m_objViewer.dw_1.CalculateGroups();
             m_objViewer.dw_1.Visible = true;
             
        }
    }
}
