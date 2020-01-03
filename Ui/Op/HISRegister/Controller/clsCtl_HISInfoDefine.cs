using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsCtl_HISInfoDefine ��ժҪ˵����
	/// </summary>
	public class clsCtl_HISInfoDefine :com.digitalwave.GUI_Base.clsController_Base
	{
		public clsCtl_HISInfoDefine()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain=new clsDcl_HISInfoDefine();
		}
		clsDcl_HISInfoDefine clsDomain;
		#region ���ô������	�Ź���	 2004-8-8
		com.digitalwave.iCare.gui.HIS.frmHISInfoDefine m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmHISInfoDefine)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ȡҽԺ������Ϣ	�Ź���		2004-8-12
		public void m_GetHospitalinfo()
		{
			
			clsHISInfoDefine_VO[] objResult;
			long lngRes=clsDomain.m_lngFindHospitalInfo(out objResult);
			if(lngRes>0 && objResult.Length>0)
			{
				m_objViewer.m_txtName.Text=objResult[0].m_strHOSPITAL_NAME_CHR;
				m_objViewer.m_txtAddress.Text=objResult[0].m_strADDRESS_VCHR;
				m_objViewer.m_txtPhone1.Text=objResult[0].m_strPHONE_NUMBER_CHR;
				m_objViewer.m_txtPhone2.Text=objResult[0].m_strPHONE_NUMBER2_CHR;
				m_objViewer.m_txtZIP.Text=objResult[0].m_strZIP_CHR;
				m_objViewer.m_txtMemo.Text=objResult[0].m_strMEMO_VCHR;
					
				
			}
		}
		#endregion

		#region ����ҽԺ������Ϣ	�Ź���	 2004-8-12
		public void m_lngSaveHospitalinfo()
		{
			
			if(m_objViewer.m_txtName.Text.Trim()=="")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				return;
			}
			long lngRes=0;
			
			clsHISInfoDefine_VO objResult=new clsHISInfoDefine_VO();
			
				objResult.m_strHOSPITAL_NAME_CHR=m_objViewer.m_txtName.Text;
				objResult.m_strADDRESS_VCHR=m_objViewer.m_txtAddress.Text; 
				objResult.m_strPHONE_NUMBER_CHR=m_objViewer.m_txtPhone1.Text;
				objResult.m_strPHONE_NUMBER2_CHR=m_objViewer.m_txtPhone2.Text;
				objResult.m_strZIP_CHR=m_objViewer.m_txtZIP.Text; 
				objResult.m_strMEMO_VCHR=m_objViewer.m_txtMemo.Text;
				lngRes=clsDomain.m_lngDoUpdHospitalInfo(objResult);
				if(lngRes>0)
				{

					MessageBox.Show("����ɹ���","��ʾ");
					
				}
			else MessageBox.Show("����ʧ�ܣ�","��ʾ");
			
		}
		#endregion

		
	}
}
