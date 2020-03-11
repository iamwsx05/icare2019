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
	/// clsSinature 的摘要说明。
	/// </summary>
	public class clsSinature: com.digitalwave.GUI_Base.clsController_Base
	{
		public clsSinature()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 设置窗体对象
		private frmSinature m_objViewer;
		/// <summary>
		/// 设置窗体对象
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
