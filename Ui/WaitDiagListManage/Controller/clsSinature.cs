using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using weCare.Core.Entity;
using iCare.CustomForm;
using com.digitalwave.controls;
using System.Data;
using com.digitalwave.iCare.middletier.HI;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsSinature ��ժҪ˵����
	/// </summary>
	public class clsSinature: com.digitalwave.GUI_Base.clsController_Base
	{
		public clsSinature()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ô������
		private frmSinature m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmSinature)frmMDI_Child_Base_in;
		}
		#endregion


	}
}
