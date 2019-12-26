using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    ///  ҽ�������ж�	�߼����Ʋ�
    /// ���ߣ� 
    /// ����ʱ�䣺 2006-4-6
    /// </summary>
    class clsCtl_OrderSaveCheck : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        clsDcl_OrderSaveCheck m_objManage = null;
        
        
        #endregion 

        #region ���캯��
        public clsCtl_OrderSaveCheck()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
            m_objManage = new clsDcl_OrderSaveCheck();
			
		}
		#endregion 

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmOrderSaveCheck m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmOrderSaveCheck)frmMDI_Child_Base_in;
		
		}
		#endregion





        public void LoadtoList()
        {
            m_objViewer.m_dtOrderdicList.Items.Clear();
            if (m_objViewer.m_dtTable.Rows.Count > 0)
            {
                for (int i = 0; i < m_objViewer.m_dtTable.Rows.Count; i++)
                {
                   
                     ListViewItem lviTemp=new ListViewItem((i+1).ToString());
                    lviTemp.SubItems.Add(m_objViewer.m_dtTable.Rows[i]["NAME_CHR"].ToString().Trim());
                    lviTemp.SubItems.Add(m_objViewer.m_dtTable.Rows[i]["ItemPrice"].ToString().Trim());
                    lviTemp.SubItems.Add(m_objViewer.m_dtTable.Rows[i]["get_dec"].ToString().Trim());
                    lviTemp.SubItems.Add(m_objViewer.m_dtTable.Rows[i]["pricesum"].ToString().Trim());
                    m_objViewer.m_dtOrderdicList.Items.Add(lviTemp);
                  
                }
            }
            
        }

        public void ConfirmMaxValue()
        {
              string EMPNO_CHR= m_objViewer.m_txtEMPNO_CHR.Text.ToString().Trim();
              string PSW_CHR = m_objViewer.m_txtPSW_CHR.Text.ToString().Trim();
              double maxvalue = 0;
              for (int i = 0; i < m_objViewer.m_dtOrderdicList.Items.Count; i++)
              {
                  if (!m_objViewer.m_dtOrderdicList.Items[i].SubItems[4].Text.ToString().Trim().Equals(""))
                  {
                      if (double.Parse(m_objViewer.m_dtOrderdicList.Items[i].SubItems[4].Text.ToString().Trim()) > maxvalue)
                      {
                          maxvalue = double.Parse(m_objViewer.m_dtOrderdicList.Items[i].SubItems[4].Text.ToString().Trim());
                      }
                  }
              }
              DataTable dtbResult = new DataTable();
              long    lngRes = m_objManage.ConfirmMaxValue(EMPNO_CHR,PSW_CHR,maxvalue,ref dtbResult);
              if (dtbResult.Rows.Count > 0)
              {
                  m_objViewer.DialogResult = DialogResult.OK;
                  m_objViewer.Close();
              }
              else
              {
                  MessageBox.Show("ȷ��ʧ�ܣ���ǰ���Ž�ɫȨ�޲�������벻�ԣ�");
              }

              
            
           
              
        }

        internal bool ConfirmPassWork(string EMPNO_CHR, string PSW_CHR, out DataTable dtbResult)
        {
           
            dtbResult = new DataTable();
            long lngRes = m_objManage.ConfirmPassWord(EMPNO_CHR, PSW_CHR, ref dtbResult);
            if (dtbResult.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }
    }
}
