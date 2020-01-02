using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// ���˴�ӡ��������Domain��
    /// </summary>
    public class clsDcl_LisBarcodeSort : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region ��ȡ���˻�����Ϣ
        /// <summary>
        /// ��ȡ���˻�����Ϣ
        /// </summary>
        /// <param name="p_strPatientCardID">���ƿ���</param>
        /// <param name="p_objPatientInfoVO">���ز�����ϢVO</param>
        /// <returns></returns>
        public long m_lngQueryPatientInfo(string p_strPatientCardID, out clsPatientBaseInfo_VO p_objPatientInfoVO)
        {
            long lngRes = 0;
             lngRes = proxy.Service.m_lngQueryPatientInfo(p_strPatientCardID, out p_objPatientInfoVO);
            return lngRes;
        }
        #endregion

        #region ��ȡ���˼�������
        /// <summary>
        /// ��ȡ���˼�������
        /// </summary>
        /// <param name="p_strPatientCardID">���ƿ���</param>
        /// <param name="p_strCheckContent">���ؼ�������</param>
        /// <param name="p_objApplMainArr">�������뵥��Ϣ</param>
        /// <returns></returns>
        public long m_lngQueryPatientCheckContent(string p_strPatientCardID,string p_strFromDate,string p_strToDate, out string p_strCheckContent, out clsLisApplMainVO[] p_objApplMainArr)
        {
            long lngRes = 0;
            p_strCheckContent = null;
            p_objApplMainArr = null;
             lngRes = proxy.Service.m_lngQueryPatientCheckContent(p_strPatientCardID,p_strFromDate,p_strToDate, out p_strCheckContent, out p_objApplMainArr);
            return lngRes;
        }
        #endregion

        #region �޸Ĳ�����Ա
        /// <summary>
        /// �޸Ĳ�����Ա
        /// </summary>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_strSampleId"></param>
        /// <returns></returns>
        public long m_lngInsertCollector(string p_strEmpId, string p_strSampleId, string p_strApplicationID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertCollector( p_strEmpId, p_strSampleId, p_strApplicationID);
            return lngRes;
        }
        #endregion

    }
}
