using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 
using com.digitalwave.iCare.BIHOrder.Control; 
using System.Collections;

namespace com.digitalwave.iCare.BIHOrder
{
   public  class clsCtl_BIHOrderInputMain : com.digitalwave.GUI_Base.clsController_Base
    {
         #region ����
       clsDcl_BIHOrderInputMain m_objManager = null;
        
        #endregion 

        #region ���캯��
        public clsCtl_BIHOrderInputMain()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
            m_objManager = new clsDcl_BIHOrderInputMain();
			
		}
		#endregion 

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmReport_Treat m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmReport_Treat)frmMDI_Child_Base_in;

        }
        #endregion
    }
}
