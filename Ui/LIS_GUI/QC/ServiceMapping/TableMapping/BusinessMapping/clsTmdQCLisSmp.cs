using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// �ʿع������ݷ�����
    /// </summary>
    public class clsTmdQCLisSmp
    {
        public static clsTmdQCLisSmp s_object
        {
            get
            {
                return new clsTmdQCLisSmp();
            }
        }

        #region Parameters
        private System.Security.Principal.IPrincipal m_objPrincipal;
        #endregion

        public clsTmdQCLisSmp()
        {
            m_objPrincipal = new clsGetPrincipal().m_objPrincipal;
        }

        /// <summary>
        /// ͨ���豸��Ż�ȡ�������ʿ���Ŀ��Ӧ�ļ�����Ŀ
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <returns></returns>
        public long m_lngGetDeviceQCCheckItemByID(string p_strDeviceID, out clsLISCheckItemNode[] p_objResultArr)
        {
            p_objResultArr = null;
            if (string.IsNullOrEmpty(p_strDeviceID))
                return 0L;

            return (new weCare.Proxy.ProxyLis02()).Service.m_lngGetDeviceQCCheckItemByID(p_strDeviceID, out p_objResultArr);
        }
        /// <summary>
        /// ��ȡ�ʿ�����������ݣ����ù̶������������
        /// </summary>
        /// <param name="p_strSampleID">�����������</param>
        /// <param name="p_strStartDat">��ʼʱ��</param>
        /// <param name="p_strEndDat">����ʱ��</param>
        /// <param name="p_intBatchSeqArr">�ʿ������</param>
        /// <param name="p_objQCDataArr"></param>
        /// <returns></returns>
        public long m_lngGetDeviceQCDataBySampleID(string p_strSampleID,
            string p_strStartDat, string p_strEndDat, int[] p_intBatchSeqArr, out clsLisQCDataVO[] p_objQCDataArr)
        {
            p_objQCDataArr = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strSampleID) || string.IsNullOrEmpty(p_strStartDat) || string.IsNullOrEmpty(p_strEndDat) || p_intBatchSeqArr == null || p_intBatchSeqArr.Length <= 0)
                return lngRes;

            return (new weCare.Proxy.ProxyLis02()).Service.m_lngGetDeviceQCDataBySampleID(p_strSampleID, p_strStartDat, p_strEndDat, p_intBatchSeqArr, out p_objQCDataArr);

        }

    }
}
