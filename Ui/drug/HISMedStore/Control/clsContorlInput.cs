using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsContorlInput ��ժҪ˵����
	/// </summary>
	public class clsContorlInput  : com.digitalwave.GUI_Base.clsController_Base
	{
		/// <summary>
		/// jjj
		/// </summary>
		public clsContorlInput()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ô������
		private bool blcomm;
		clsDomainControlMedStore objcontrol=new clsDomainControlMedStore();
		frmInput m_objViewer;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmInput)frmMDI_Child_Base_in;
		}
		#endregion

		#region �ж�Ա������
		public void Determinant()
		{
			if(this.m_objViewer.txtNo.Text!="")
			{
				string p_strName="";
				string empID="";
				string p_strID=this.m_objViewer.txtNo.Text.Trim();
				m_lngFineName(p_strID,out p_strName,out empID);
				if(p_strName=="")
				{
					blcomm=false;
					this.m_objViewer.txtname.Text="�����Ա����";
				}
				else
				{
					this.m_objViewer.txtNo.Tag=empID;
					blcomm=true;
					this.m_objViewer.txtname.Text=p_strName;
				}
				this.m_objViewer.m_cmdSave.Focus();
			}
			else
			{
				blcomm=false;
				this.m_objViewer.txtname.Text="���Ų���Ϊ��";
				this.m_objViewer.txtname.Focus();
			}
		}
		#endregion

		#region ȷ����ť�¼�
		public bool m_cmdSaveClick()
		{
		  return blcomm;
		}
		#endregion

		public void ShowForm()
		{
			m_objViewer.Show();
		}

		#region ����Ա������
		public void m_lngFineName(string p_strID,out string p_strName,out string empID)
		{
            objcontrol.m_lngfinedata(p_strID,out p_strName,out empID);
		}
		#endregion
	}
}
