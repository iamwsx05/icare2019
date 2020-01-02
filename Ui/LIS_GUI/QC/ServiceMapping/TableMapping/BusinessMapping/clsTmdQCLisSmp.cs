using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// 质控管理数据访问类
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
        /// 通过设备编号获取仪器的质控项目对应的检验项目
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
        /// 获取质控样本结果数据，采用固定检验样本编号
        /// </summary>
        /// <param name="p_strSampleID">仪器样本编号</param>
        /// <param name="p_strStartDat">开始时间</param>
        /// <param name="p_strEndDat">结束时间</param>
        /// <param name="p_intBatchSeqArr">质控批序号</param>
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
