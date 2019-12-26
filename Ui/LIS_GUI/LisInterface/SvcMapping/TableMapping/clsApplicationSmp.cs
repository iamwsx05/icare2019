using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    ///  T_OPR_LIS_APPLICATION【检验申请单新增,修改,删除】

    /// </summary>
    internal class clsApplicationSmp : clsDomainController_Base
    {

        #region 构造



        private clsApplicationSmp()
        {
        }

        public static clsApplicationSmp s_obj
        {
            get
            {
                return new clsApplicationSmp();
            }
        }

        #endregion

        /// <summary>
        ///新增检验申请单
        /// </summary>
        /// <param name="objApplMainVO"></param>
        /// <returns></returns>
        public long m_lngAddNewApplication(clsLisApplMainVO objApplMainVO)
        {
            clsLisApplMainVO objOutVO = null;
            long lngRes = 0;

            try
            {
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewAppl(objApplMainVO, out objOutVO);
                if (lngRes > 0 && objOutVO != null)
                {
                    objOutVO.m_mthCopyTo(objApplMainVO);
                }
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

    }
}
