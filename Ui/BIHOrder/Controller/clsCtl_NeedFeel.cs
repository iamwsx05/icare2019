using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 

namespace com.digitalwave.iCare.BIHOrder
{
	/// <summary>
	/// Ƥ��¼���	�߼����Ʋ�
	/// ���ߣ� ����
	/// ����ʱ�䣺 2004-12-23 
	/// </summary>
	public class clsCtl_NeedFeel: com.digitalwave.GUI_Base.clsController_Base
	{
		#region ����
		clsDcl_ExecuteOrder m_objManage = null;
		public string m_strReportID;
		/// <summary>
		/// ��½�û�ID
		/// </summary>
		public string m_strOperatorID;
		/// <summary>
		/// ��ǰƤ�Խ������ˮ��
		/// </summary>
		internal string m_strOrderFeelID="";
        
		#endregion 
		#region ���캯��
		public clsCtl_NeedFeel()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDcl_ExecuteOrder();
			m_strReportID = null;
		}
		#endregion 
		#region ���ô������
		com.digitalwave.iCare.BIHOrder.frmNeedFeel m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmNeedFeel)frmMDI_Child_Base_in;
		}
		#endregion
		#region  ����
		/// <summary>
		/// ��������
		/// </summary>
		public void LoadData()
		{
			if(m_objViewer.m_strOrderID==string.Empty) return;

            clsT_Opr_Bih_OrderFeel_VO objTem;
            long lngRes = m_objManage.m_lngGetOrderFeelByOrderID(m_objViewer.m_strOrderID, out objTem);
            if (lngRes > 0 && objTem != null && objTem.m_strORDERFEELID_CHR != null)
            {
                m_strOrderFeelID = objTem.m_strORDERFEELID_CHR;
                m_objViewer.m_cmbRESULTTYPE_INT.SelectedIndex = objTem.m_intRESULTTYPE_INT;
                m_objViewer.m_txbDES_VCHR.Text = objTem.m_strDES_VCHR;
            }
		}
		#endregion

        private clsT_Opr_Bih_OrderFeel_VO objBuckUpVo;

        public void SetVo(clsT_Opr_Bih_OrderFeel_VO obj)
        {
            objBuckUpVo = obj;
        }

		#region �¼�
		/// <summary>
		/// ����
		/// </summary>
		public void SaveOrderFeel()
		{
			//��֤����
			if(!IsPassValidate()) return;

			//���Ƥ�Խ��Vo
			clsT_Opr_Bih_OrderFeel_VO p_objRecord;
			FillOrderFeel_VO(out p_objRecord);

			long lngRes =0;
			string p_strRecordID ="";
			if(m_strOrderFeelID!=string.Empty)
			{
				//�޸�
				lngRes =m_objManage.m_lngModifyOrderFeel(p_objRecord);				
			}
			else
			{
				//����Ƥ��
				lngRes =m_objManage.m_lngAddNewOrderFeel(out p_strRecordID,p_objRecord);				
			}

			if(lngRes<=0) 
			{
				MessageBox.Show(m_objViewer,"����ʧ��!","��ʾ��!",MessageBoxButtons.OK,MessageBoxIcon.Information);
				m_objViewer.m_intResult =-1;
				return ;
			}			
			if(p_strRecordID!=string.Empty) 
			{
				m_strOrderFeelID =p_strRecordID;
			}
			MessageBox.Show(m_objViewer,"����ɹ�!","��ʾ��!",MessageBoxButtons.OK,MessageBoxIcon.Information);	
			m_objViewer.m_intResult =1;		
			m_objViewer.m_objFeelEdit.m_intExitState =1;
			m_objViewer.m_objFeelEdit.m_intFeelResult =p_objRecord.m_intRESULTTYPE_INT;			
			m_objViewer.m_objFeelEdit.m_strFeelResult =(p_objRecord.m_intRESULTTYPE_INT==1)?"����":"����";
			m_objViewer.Close();
		}

        /// <summary>
        /// ����Ƥ��
        /// </summary>
        public void SaveOrderFeel2()
        {
            //��֤����
            if (!IsPassValidate()) return;
            FillOrderFeel_VO(out this.m_objViewer.p_objRecord);

            long lngRes = 0;
            string p_strRecordID = "";
            //if (m_strOrderFeelID != string.Empty)
            //{
            //    //�޸�
            //    lngRes = m_objManage.m_lngModifyOrderFeel(p_objRecord);
            //}
            //else
            //{
            //    //����Ƥ��
            //   lngRes = m_objManage.m_lngAddNewOrderFeel(out p_strRecordID, p_objRecord);
            //}
            //�޸�
            lngRes = m_objManage.m_lngModifyOrderFeelEnd(this.m_objViewer.p_objRecord);
            if (lngRes <= 0)
            {
                MessageBox.Show(m_objViewer, "����ʧ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (p_strRecordID != string.Empty)
            {
                m_strOrderFeelID = p_strRecordID;
            }
            MessageBox.Show(m_objViewer, "����ɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //m_objViewer.m_intResult = 1;
            //m_objViewer.m_objFeelEdit.m_intExitState = 1;
            //m_objViewer.m_objFeelEdit.m_intFeelResult = p_objRecord.m_intRESULTTYPE_INT;
            //m_objViewer.m_objFeelEdit.m_strFeelResult = (p_objRecord.m_intRESULTTYPE_INT == 1) ? "����" : "����";
            this.m_objViewer.DialogResult = DialogResult.OK;
        }
		#endregion 
		#region ����
		/// <summary>
		/// ��֤����
		/// </summary>
		/// <returns></returns>
		private bool IsPassValidate()
		{
			if(m_objViewer.m_strOrderID==string.Empty)
			{
				MessageBox.Show(m_objViewer,"��ѡ��ҽ��!","��ʾ��!",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return false;
			}			
			if(m_objViewer.m_cmbRESULTTYPE_INT.SelectedIndex <=0)
			{
				MessageBox.Show(m_objViewer,"Ƥ�Խ������!","��ʾ��!",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return false;
			}
			return true;
		}
		/// <summary>
		/// ���Ƥ�Խ��Vo
		/// </summary>
		/// <param name="p_objRecord">Ƥ�Խ��Vo [out ����]</param>
		private void FillOrderFeel_VO(out clsT_Opr_Bih_OrderFeel_VO p_objRecord)
		{ 
            if (this.objBuckUpVo.m_intRESULTTYPE_INT == 2 && m_objViewer.m_cmbRESULTTYPE_INT.SelectedIndex == 1)
            {
                this.m_objViewer.m_intFeelFlag = 1;
            }
            else if (this.objBuckUpVo.m_intRESULTTYPE_INT == 2 && m_objViewer.m_cmbRESULTTYPE_INT.SelectedIndex == 2)
            {
                this.m_objViewer.m_intFeelFlag = 2;
            }
            p_objRecord = new clsT_Opr_Bih_OrderFeel_VO();
            p_objRecord.m_strORDERID_CHR = m_objViewer.m_strOrderID;
			p_objRecord.m_intRESULTTYPE_INT =m_objViewer.m_cmbRESULTTYPE_INT.SelectedIndex;
			p_objRecord.m_strDES_VCHR =m_objViewer.m_txbDES_VCHR.Text;				
		}
		#endregion
	}
}
