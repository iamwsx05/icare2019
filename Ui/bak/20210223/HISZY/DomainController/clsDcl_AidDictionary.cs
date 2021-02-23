using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ��������DOMAIN��
    /// </summary>
    public class clsDcl_AidDictionary : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsDcl_AidDictionary()
        {
        }

        weCare.Proxy.ProxyIP02 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyIP02();
            }
        }

        #endregion

        #region ����ҽ��ְ��
        /// <summary>
        /// ����ҽ��ְ��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetDoctorDuty(out DataTable dt)
        {
            return proxy.Service.m_lngGetDoctorDuty(out dt); 
        }
        #endregion

        #region ��������Ĭ�ϼ�����Ŀ��
        /// <summary>
        /// ��������Ĭ�ϼ�����Ŀ��
        /// </summary>
        /// <param name="RecordsArr"></param>
        /// <param name="Flag">-1 ֻɾ��</param>
        /// <param name="PayTypeID"></param>
        /// <returns></returns>        
        public long m_lngSaveOutPatientDefaultAddItem(List<clsOutPatientDefaultAddItem_VO> RecordsArr, int Flag, string PayTypeID)
        {
            return proxy.Service.m_lngSaveOutPatientDefaultAddItem(RecordsArr, Flag, PayTypeID); 
        }
        #endregion

        #region ��ȡ����Ĭ�ϼ�����Ŀ��
        /// <summary>
        /// ��ȡ����Ĭ�ϼ�����Ŀ��
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PayTypeID"></param>
        /// <returns></returns>
        public long m_lngGetOutPatientDefaultAddItem(out DataTable dt, string PayTypeID)
        { 
            return proxy.Service.m_lngGetOutPatientDefaultAddItem(out dt, PayTypeID); 
        }
        #endregion
    }
}
