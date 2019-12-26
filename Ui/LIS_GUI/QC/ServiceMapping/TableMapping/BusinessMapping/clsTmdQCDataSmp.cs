using System;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;


namespace com.digitalwave.iCare.gui.LIS
{
    public class clsTmdQCDataSmp : clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        public static clsTmdQCDataSmp s_object
        {
            get
            {
                return new clsTmdQCDataSmp();
            }
        }

        #region Parameters 
        #endregion

        #region Construtor
        public clsTmdQCDataSmp()
        {
        }
        #endregion

        #region INSERT
        public long m_lngInsert(clsLisQCDataVO p_objRecord)
        {

            int intID = -1;
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsert(p_objRecord, out intID);
            }
            catch { lngRes = 0; }
            if (lngRes > 0)
            {
                p_objRecord.m_intSeq = intID;
            }
            return lngRes;
        }
        #endregion

        #region UPDATE
        public long m_lngUpdate(clsLisQCDataVO p_objRecord)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdate(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region DELETE
        public long m_lngDelete(int p_intID)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngDelete(p_intID);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region FIND
        public long m_lngFind(int p_intID, out clsLisQCDataVO p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFind(p_intID, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        public long m_lngFind(out clsLisQCDataVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFind(out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        public long m_lngFind(out clsLisQCDataVO[] p_objRecordArr, int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFind(out p_objRecordArr, p_intQCBatchSeq, p_datBegin, p_datEnd);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        /// <summary>
        /// 查找质控样本结果数据
        /// </summary>
        /// <param name="p_objRecordArr"></param>
        /// <param name="p_intQCBatchSeqArr"></param>
        /// <param name="p_datBegin"></param>
        /// <param name="p_datEnd"></param>
        /// <returns></returns>
        public long m_lngFind(out clsLisQCDataVO[] p_objRecordArr, int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFind(out p_objRecordArr, p_intQCBatchSeqArr, p_datBegin, p_datEnd);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存,将所有保存数据在一个事务完成
        /// </summary>
        /// <param name="p_objInsertArr"></param>
        /// <param name="p_objUpdateArr"></param>
        /// <param name="p_intDelArr"></param>
        /// <param name="p_intISeqArr"></param>
        /// <returns></returns>
        public long m_lngSaveAll(clsLisQCDataVO[] p_objInsertArr, clsLisQCDataVO[] p_objUpdateArr, int[] p_intDelArr, out int[] p_intISeqArr)
        {
            p_intISeqArr = null;
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngSaveAll(p_objInsertArr, p_objUpdateArr, p_intDelArr, out p_intISeqArr);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        #endregion

        #region m_lngInsertQCReport
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objQCReport"></param>
        /// <param name="p_intSeq"></param>
        /// <returns></returns>
        public long m_lngInsertQCReport(clsLisQCReportVO p_objQCReport, out int p_intSeq)
        {

            return (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertQCReport(p_objQCReport, out p_intSeq);

        }
        #endregion

        #region m_lngUpdateQCReport
        /// <summary>
        /// m_lngUpdateQCReport
        /// </summary>
        /// <param name="QCBatch"></param>
        /// <returns></returns>
        public long m_lngUpdateQCReport(clsLisQCReportVO QCBatch)
        {

            return (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdateQCReport(QCBatch);

        }
        #endregion

    }
}