using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 
using System.Collections;

namespace com.digitalwave.iCare.BIHOrder
{
	/// <summary>
	/// �ֹ�ִ������ҽ������	�߼����Ʋ�
	/// ���ߣ� ����
	/// ����ʱ�䣺 2005-04-18
	/// </summary>
	public class clsCtl_HandworkContinuousOrder: com.digitalwave.GUI_Base.clsController_Base
	{
		#region ����
		clsDcl_ExecuteOrder m_objManage = null;
		public string m_strReportID;
		/// <summary>
		/// ������ID
		/// </summary>
		public string m_strOperatorID;
		/// <summary>
		/// ������
		/// </summary>
		public string m_strOperatorName;
		#endregion 
		#region ���캯��
		public clsCtl_HandworkContinuousOrder()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDcl_ExecuteOrder();
			m_strReportID = null;
		}
		#endregion 
		#region ���ô������
		com.digitalwave.iCare.BIHOrder.frmHandworkContinuousOrder m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmHandworkContinuousOrder)frmMDI_Child_Base_in;
		}
		#endregion
		#region ���
		/// <summary>
		/// ���ListView
		/// </summary>
		private void ClearListView()
		{
			
		}
		#endregion
		#region �����¼�
		/// <summary>
		/// ����������ҽ���Ĺ�����Ϣ	{ҵ��˵��: }
		/// </summary>
		public void m_LoadMoneyForContinuousOrder()
		{
			ClearListView();
		}
		#endregion

		#region ��ť�¼�
		/// <summary>
		/// ����
		/// </summary>
		public void m_AutoCumulateMoneyForContinuousOrder()
		{
			//��ȡ����ʱ��
			string strAuto =m_objViewer.m_dtpAuto.Value.ToString("yyyy-MM-dd") + " 23:59:59";
			DateTime dtAuto =System.Convert.ToDateTime(strAuto);

			//��֤�Ƿ���Թ���	��ֻ���ֹ���������ǰ�ĵķ��ã�
			if(!blnValidateCumulateMoney(dtAuto)) return;

			if(MessageBox.Show(m_objViewer,"ȷ���ֹ��ԡ�" +dtAuto.ToString("yyyy��MM��dd��")+ "�������Ѳ�����\r\n��ʾ:�Ѿ����ѣ��򲻼Ʒѣ�","��ʾ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
				return;			
			long lngRes =m_objManage.m_lngAuToCumulateMoneyForContinuousOrder(m_strOperatorName,m_strOperatorID,dtAuto);
			if(lngRes>0)
			{
				if(lngRes==999)
				{
					MessageBox.Show(m_objViewer,"û����Ҫ���ѵ�����ҽ�����Ѿ��������ˣ�","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show(m_objViewer,"���ѳɹ���","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
				}
			}
			else
			{
				MessageBox.Show(m_objViewer,"����ʧ�ܣ�","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		/// <summary>
		/// ��֤�Ƿ���Թ���	��ֻ�ܶԹ�ȥ�����������Ѳ�����
		/// </summary>
		/// <param name="p_dtAuToExecDataTime">ִ��ʱ��</param>
		/// <returns></returns>
		private bool blnValidateCumulateMoney(DateTime p_dtAuToExecDataTime)
		{
			if(p_dtAuToExecDataTime>System.DateTime.Now)
			{
				MessageBox.Show(m_objViewer,"ֻ�ܶԹ�ȥ�����������Ѳ�����","���棡",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return false;
			}
			return true;
		}
		#endregion
	}
}
