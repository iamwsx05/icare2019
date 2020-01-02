using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll commoninfo.dll
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Drawing;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// clsController_ConfirmLogin ��ժҪ˵����
	/// </summary>
	public class clsController_ConfirmLogin : com.digitalwave.GUI_Base.clsController_Base
	{
		public clsController_ConfirmLogin()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private frmConfirmLogin m_objViewer;
		#region ���ô������
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmConfirmLogin)frmMDI_Child_Base_in;
		}
		#endregion

		#region		����������ȷ�� xing.chen add
		public long m_lngCheckComfirLogin(string p_strLoginName,string p_strLoginPwd,out bool blnLogin,out string strLoginMsg,out string strEmpID)
		{
			blnLogin = false;
			strLoginMsg = "";
			strEmpID = "";
			long lngRes = 0;
			try
			{
				lngRes = new clsDomainController_ApplicationManage().m_lngCheckComfirmLogin(p_strLoginName,p_strLoginPwd,out blnLogin,out strLoginMsg,out strEmpID);
				if( lngRes < 0 )
				{
					return -1;
				}
			}
			catch
			{
				strLoginMsg = "��˻�ȡ��Ϣʧ��";
			}
			return lngRes;
		}
		#endregion
	}
}
