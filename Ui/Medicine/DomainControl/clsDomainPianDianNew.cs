using System;
using System.Data;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    #region 盘点业务控制类 ：created by weiling.huang  at 2005-10-10
    /// <summary>
    ///盘点业务控制类

    /// <summary>
    public class clsDomainPianDianNew : clsDomainController_Base//GUI_Base.dll
    {


        public DataTable m_mthGetDatap(DataTable dtbResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //	(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            return (new weCare.Proxy.ProxyMedStore01()).Service.m_mthGetDatap(dtbResult);
        }


    }
    #endregion
}
