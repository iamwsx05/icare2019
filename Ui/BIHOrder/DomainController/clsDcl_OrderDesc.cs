using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;

namespace com.digitalwave.iCare.BIHOrder
{
    public class clsDcl_OrderDesc : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 整体保存医嘱组套成员
        /// <summary>
        /// 整体保存医嘱组套成员
        /// </summary>
        /// <param name="p_dtDataName"></param>
        /// <param name="p_objResult"></param>
        public long m_lngAddNewOrderdescVO(clsOrderdescVO[] m_arrClsOrderdescVO)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrderdescVO(m_arrClsOrderdescVO);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
    }
}
