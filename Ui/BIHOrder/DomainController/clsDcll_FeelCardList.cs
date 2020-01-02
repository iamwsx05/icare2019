using System;
using System.Data;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.BIHOrder
{
    public class clsDcl_FeelCardList : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 整体保存医嘱组套成员
        /// <summary>
        /// 整体保存医嘱组套成员
        /// </summary>
        /// <param name="p_dtDataName"></param>
        /// <param name="p_objResult"></param>
        public long m_lngGetFeelOrderByAreaID(string m_strAreaId, out DataTable m_dtExecOrder)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetFeelOrderByAreaID(m_strAreaId, out m_dtExecOrder);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
    }
}