using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
     public class clsCtl_StorageQueryReport : com.digitalwave.GUI_Base.clsController_Base
    {

        #region ���ô������

        /// <summary>
        /// ���ô������.
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
           // m_objViewer = (frmStorageQueryReport)frmMDI_Child_Base_in;
        }
        #endregion
    }
}
