using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 

namespace com.digitalwave.iCare.BIHOrder
{
	/// <summary>
	/// ���ӵ��ݱ༭	�߼����Ʋ�
	/// ���ߣ� ����
	/// ����ʱ�䣺 2005-01-11
	/// </summary>
	public class clsCtl_OrderAttachEdit: com.digitalwave.GUI_Base.clsController_Base
	{
		#region ����
		clsDcl_ExecuteOrder m_objManage = null;
		public string m_strReportID;
		public string m_strOperatorID;
		/// <summary>
		/// ���ӵ�����ˮ��
		/// </summary>
		public string m_strATTACHID_CHR ="";
		#endregion 
		#region ���캯��
		public clsCtl_OrderAttachEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDcl_ExecuteOrder();
			m_strReportID = null;			
			m_strOperatorID = "";
		}
		#endregion 
		#region ���ô������
		com.digitalwave.iCare.BIHOrder.frmOrderAttachEdit m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmOrderAttachEdit)frmMDI_Child_Base_in;
		
		}
		#endregion

		#region ��Ϊֻ��
		/// <summary>
		/// ����ֻ��
		/// </summary>
		public void m_SetReadOnly()
		{
			m_objViewer.m_cboMAZUI_CHR.Enabled =false;
			m_objViewer.m_txtDESC_VCHR.Enabled =false;
		}
		#endregion

		#region ����
		/// <summary>
		/// ���벡�ˡ����ӵ�����Ϣ
		/// </summary>
		public void m_LoadData()
		{
			long lngRes =0;

			//���벡����Ϣ	
			if(m_objViewer.m_strPatientID.Trim()=="") return ;
			clsPatient_VO objItem =new clsPatient_VO();
			lngRes =m_objManage.m_lngGetPatientInfoByPatientID(m_objViewer.m_strPatientID,out objItem);
			if(lngRes>0 && objItem!=null)
			{
				m_objViewer.m_lblPATIENTNAME_CHR.Text =objItem.m_strNAME_VCHR;
				m_objViewer.m_lblSEX_CHR.Text =objItem.m_strSEX_CHR;
				m_objViewer.m_lblINPATIENTID_CHR.Text =objItem.m_strINPATIENTID_CHR;
				m_objViewer.m_lblIDCARD_CHR.Text =objItem.m_strIDCARD_CHR;			
			}

			//���븽�ӵ�����Ϣ
			string strAttachID =m_objViewer.m_strAttachID.Trim();
			if(strAttachID=="")return;
			clsT_Opr_Bih_Temfororder_VO objResult =null;
			lngRes =m_objManage.m_lngGetTemfororderByID(strAttachID,out objResult);
			if(lngRes>0 && objResult!=null)
			{
				m_strATTACHID_CHR =objResult.m_strID_CHR;
				m_objViewer.m_strPatientID =objResult.m_strPATIENTID_CHR;				
				m_objViewer.m_txtDESC_VCHR.Text =objResult.m_strDESC_VCHR;
				m_objViewer.m_cboMAZUI_CHR.SelectedItem =objResult.m_strMAZUI_CHR;	
				m_objViewer.m_lblPSTATUS_CHR.Tag =objResult.m_fltPSTATUS_CHR;
				switch(objResult.m_fltPSTATUS_CHR.ToString().Trim())
				{
					case "0": 
						m_objViewer.m_lblPSTATUS_CHR.Text ="δ����";
						break;
					case "1": 
						m_objViewer.m_lblPSTATUS_CHR.Text ="�ѷ���";
						break;
					case "2": 
						m_objViewer.m_lblPSTATUS_CHR.Text ="���н��";
						break;
					default: 
						m_objViewer.m_lblPSTATUS_CHR.Text ="δ֪״̬";
						break;
				}
			}
		}
		#endregion

		#region �¼�
		/// <summary>
		/// ��|���¼�
		/// </summary>
		public void m_OK()
		{
			long lngRes =0;
			if(!CheckInput()) return ;
			clsT_Opr_Bih_Temfororder_VO objItem =null;
			SetVo(out objItem);
			if(m_objViewer.m_intEditState==0)//����
			{
				string strRecordID ="";
				lngRes =m_objManage.m_lngAddNewTemfororder(out strRecordID,objItem);
				if(lngRes>0)
				{
					//���Ӹ��ӵ���Ӱ��--���
					m_objViewer.m_strAttachID =strRecordID;
					//com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objTem =new com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService();
					lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddAttachOrder(m_objViewer.m_strOrderID,strRecordID);
				}
			}
			else if(m_objViewer.m_intEditState==1)//�༭
			{
				lngRes =m_objManage.m_lngModifyTemfororder(objItem);
			}

			//����������
			if(lngRes>0)
				MessageBox.Show("�����ɹ���");
			else
				MessageBox.Show("����ʧ�ܣ�");
			m_objViewer.Close();
		}
		/// <summary>
		/// ɾ���¼�	
		/// </summary>
		public void m_Del()
		{
			if(m_objViewer.m_strAttachID.Trim()=="") return;
			//�Ƿ����ɾ��
			if(!MayDelete()) return;

			long lngRes =0;
			//ɾ�����ӵ���Ӱ��--��ɾ
			//com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objTem =new com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService();
			lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteAttachOrder(m_objViewer.m_strAttachID);
			if(lngRes>0)
			{
				lngRes =m_objManage.m_lngDeleteTemfororder(m_objViewer.m_strAttachID);				
			}
			//����������
			if(lngRes>0)
				MessageBox.Show("ɾ���ɹ���");
			else
				MessageBox.Show("ɾ��ʧ�ܣ�");
			m_objViewer.Close();
		}
		/// <summary>
		/// �ύ�¼�
		/// </summary>
		public void m_Commit()
		{
			if(m_objViewer.m_strAttachID.Trim()=="") return;
			int IntState =-1;//״̬��־	{0=δ���ͣ�1=�ѷ��ͣ�2=���н���ˣ�}
			try{IntState =Int32.Parse(m_objViewer.m_lblPSTATUS_CHR.Tag.ToString());}
			catch{}
			if(IntState!=0) return;
			long lngRes =0;
			lngRes =m_objManage.m_lngCommitTemfororder(m_objViewer.m_strAttachID);
			//����������
			if(lngRes>0)
				MessageBox.Show("�ύ�ɹ���");
			else
				MessageBox.Show("�ύʧ�ܣ�");
			m_objViewer.Close();
		}
		#region ˽�з���
		/// <summary>
		/// ��֤����
		/// </summary>
		/// <returns></returns>
		private bool CheckInput()
		{
			return true;
		}
		/// <summary>
		/// ��丽�ӵ���Vo����
		/// </summary>
		/// <param name="objItem"></param>
		private void SetVo(out clsT_Opr_Bih_Temfororder_VO objItem)
		{
			objItem =new clsT_Opr_Bih_Temfororder_VO();
			objItem.m_strID_CHR =m_objViewer.m_strAttachID;
			objItem.m_strPATIENTID_CHR =m_objViewer.m_strPatientID;
			objItem.m_strREGISTERID_CHR ="";
			objItem.m_strPATIENTNAME_CHR =m_objViewer.m_lblPATIENTNAME_CHR.Text.Trim();
			objItem.m_strMAZUI_CHR =m_objViewer.m_cboMAZUI_CHR.Text;
			objItem.m_strDESC_VCHR =m_objViewer.m_txtDESC_VCHR.Text;
			try
			{
				objItem.m_fltPSTATUS_CHR =Convert.ToSingle(m_objViewer.m_lblPSTATUS_CHR.Tag.ToString());
			}
			catch
			{
				objItem.m_fltPSTATUS_CHR =0;
			}
		}
		/// <summary>
		/// ��ȡ�Ƿ����ɾ��
		/// </summary>
		/// <returns></returns>
		private bool MayDelete()
		{
			if(m_objViewer.m_intEditState==2) return false;
			return true;
		}
		#endregion
		#endregion
	}
}
