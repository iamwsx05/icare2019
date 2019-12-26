using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// �������������Domain��
    /// </summary>
    class clsDcl_BatchSaveReport:com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region ��ѯ���뵥��Ϣͨ�������
        /// <summary>
        /// ��ѯ���뵥��Ϣͨ�������
        /// </summary>
        /// <param name="p_strBarcode"></param>
        /// <param name="p_objMainVO"></param>
        /// <returns></returns>
        public long m_lngQuerySampleInfo(string p_strBarcode, out clsLisApplMainVO p_objMainVO)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngQuerySampleInfo(p_strBarcode, out p_objMainVO);
            return lngRes;
        }
        #endregion

        #region �������������
        /// <summary>
        /// �������������
        /// </summary>
        /// <param name="p_objMainArr"></param>
        /// <param name="p_strOperator"></param>
        /// <returns></returns>
        public long m_lngUpdateCheckNUM(clsLisApplMainVO[] p_objMainArr, string p_strOperator)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngUpdateCheckNUM(p_objMainArr, p_strOperator);
            return lngRes;
        }
        #endregion
    }
}
