using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.iCare.gui.Security;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlCreatePlan ��ժҪ˵����
	/// </summary>
	public class clsControlCreatePlan:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlCreatePlan()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objDoMain=new clsDomainConrol_Plan();
		}
		clsDomainConrol_Plan m_objDoMain=null;
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmCreatePlanByDate m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmCreatePlanByDate)frmMDI_Child_Base_in;
		}
		#endregion

		#region ����ƻ�
		public long m_lngCreatePlan(DateTime startDate,DateTime endDate)
		{
			com.digitalwave.iCare.gui.Security.clsController_Security clsSec=new clsController_Security();
			string strOperator=clsSec.objGetCurrentLoginEmployee().strEmpID;
			if(MessageBox.Show("�����ܼƻ��������������ݣ�ȷ�ϵ�����","",MessageBoxButtons.YesNo)==DialogResult.No)
				return -1;
			if(startDate>endDate)
			{
				MessageBox.Show("��ʼ���ڲ��ܴ��ڽ�������","��ʾ");
				return -1;
			}
			long lngRes=m_objDoMain.m_lngCreatePlan(startDate,endDate,strOperator);
			if (lngRes>=0)
				MessageBox.Show("�����ܼƻ��ɹ���","��ʾ");
			else
			{
				if(lngRes<0)
					MessageBox.Show("�����ܼƻ�ʧ�ܣ�","��ʾ");
			}
			return lngRes;
		}
		#endregion

	}
}
