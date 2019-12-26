using System;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;
using System.Collections.Generic;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsTmdQCBatchSmp : clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        public static clsTmdQCBatchSmp s_object
        {
            get
            {
                return new clsTmdQCBatchSmp();
            }
        }

        #region Parameters 
        #endregion

        #region Construtor
        public clsTmdQCBatchSmp()
        {
        }
        #endregion

        #region INSERT
        /// <summary>
        /// 保存质控批类
        /// </summary>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngInsert(clsLisQCBatchVO p_objRecord)
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
        /// <summary>
        /// 保存质控批类
        /// </summary>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngInsertArr(ref clsLisQCBatchVO[] p_objRecordArr)
        {
            int[] iSeqArr = null;
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertByArr(p_objRecordArr, out iSeqArr);
            }
            catch
            {
                lngRes = 0;
            }

            if (lngRes > 0 && iSeqArr != null)
            {
                for (int index = 0; index < p_objRecordArr.Length; index++)
                {
                    p_objRecordArr[index].m_intSeq = iSeqArr[index];
                }
            }
            return lngRes;
        }
        #endregion

        #region UPDATE
        public long m_lngUpdate(clsLisQCBatchVO p_objRecord)
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
        /// <summary>
        /// 查找定质控批序号的质控设置
        /// </summary>
        /// <param name="p_intID"></param>
        /// <param name="p_blnExtFind"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngFind(int p_intID, bool p_blnExtFind, out clsLisQCBatchVO p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFind(p_intID, p_blnExtFind, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        /// <summary>
        /// 查找定质控批序号的质控设置
        /// </summary>
        /// <param name="p_intIDArr"></param>
        /// <param name="p_blnExtFind"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngFind(int[] p_intIDArr, bool p_blnExtFind, out clsLisQCBatchVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFind(p_intIDArr, p_blnExtFind, out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        /// <summary>
        /// 查找质控设置
        /// </summary>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngFind(out clsLisQCBatchVO[] p_objRecordArr)
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
        #endregion

        #region frmQCBatchMangerNew 

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_intBatchSeq"></param>
        /// <param name="p_strSampleId"></param>
        /// <returns></returns>
        public long m_lngQueryDeviceSampleID(int p_intBatchSeq, out string p_strSampleId)
        {
            p_strSampleId = null;

            return (new weCare.Proxy.ProxyLis02()).Service.m_lngQueryDeviceSampleID(p_intBatchSeq, out p_strSampleId);

        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strSampleID"></param>
        /// <param name="p_strStartDat"></param>
        /// <param name="p_strEndDat"></param>
        /// <param name="p_intBatchSeqArr"></param>
        /// <param name="p_objQCDataArr"></param>
        /// <returns></returns>
        public long m_lngReceiveDeviceQCDataBySampleID(string p_strSampleID, string p_strStartDat, string p_strEndDat, int[] p_intBatchSeqArr, out clsLisQCDataVO[] p_objQCDataArr)
        {

            return (new weCare.Proxy.ProxyLis02()).Service.m_lngReceiveDeviceQCDataBySampleID(p_strSampleID, p_strStartDat, p_strEndDat, p_intBatchSeqArr, out p_objQCDataArr);

        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_intSeqArr"></param>
        /// <param name="p_blnExtFind"></param>
        /// <param name="p_objQCBatchArr"></param>
        /// <returns></returns>
        public long m_lngFindQCBatch(int[] p_intSeqArr, bool p_blnExtFind, out clsLisQCBatchVO[] p_objQCBatchArr)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCBatch(p_intSeqArr, p_blnExtFind, out p_objQCBatchArr);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_intQCBatchSeqArr"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindQCConcentration(int[] p_intQCBatchSeqArr, out clsLisQCConcentrationVO[] p_objResultArr)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCConcentration(p_intQCBatchSeqArr, out p_objResultArr);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_intQCBatchSeqArr"></param>
        /// <param name="p_datBegin"></param>
        /// <param name="p_datEnd"></param>
        /// <returns></returns>
        public long m_lngFindQCData(out clsLisQCDataVO[] p_objResultArr, int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCData(out p_objResultArr, p_intQCBatchSeqArr, p_datBegin, p_datEnd);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_intQCBatchSeqArr"></param>
        /// <param name="p_datBegin"></param>
        /// <param name="p_datEnd"></param>
        /// <param name="p_status"></param>
        /// <param name="p_objQCReportArr"></param>
        /// <returns></returns>
        public long m_lngFindQCReport(int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd, enmQCStatus p_status, out clsLisQCReportVO[] p_objQCReportArr)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCReport(p_intQCBatchSeqArr, p_datBegin, p_datEnd, p_status, out p_objQCReportArr);

        }


        public long m_lngFindQCBatch(int p_intSeq, bool p_blnExtFind, out clsLisQCBatchVO p_objQCBatch)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCBatch(p_intSeq, p_blnExtFind, out p_objQCBatch);

        }

        public long m_lngFindQCConcentration(int p_intQCBatchSeq, out clsLisQCConcentrationVO[] p_objResultArr)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCConcentration(p_intQCBatchSeq, out p_objResultArr);

        }

        public long m_lngFindQCData(out clsLisQCDataVO[] p_objResultArr, int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCData(out p_objResultArr, p_intQCBatchSeq, p_datBegin, p_datEnd);

        }


        public long m_lngFindQCReport(int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd, enmQCStatus p_status, out clsLisQCReportVO[] p_objQCReportArr)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCReport(p_intQCBatchSeq, p_datBegin, p_datEnd, p_status, out p_objQCReportArr);

        }


        public long m_lngUpdateQCData(clsLisQCDataVO QCBatch)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdateQCData(QCBatch);

        }

        internal long m_lngInsertQCData(clsLisQCDataVO p_objQCData, out int p_intSeq)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertQCData(p_objQCData, out p_intSeq);

        }


        internal long m_lngDeleteQCData(int p_intSeq)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngDeleteQCData(p_intSeq);

        }

        internal long m_lngSaveAllQCData(clsLisQCDataVO[] p_objInsertArr, clsLisQCDataVO[] p_objUpdateArr, int[] p_intDelArr, out int[] p_intISeqArr)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngSaveAllQCData(p_objInsertArr, p_objUpdateArr, p_intDelArr, out p_intISeqArr);

        }


        public long m_lngReceiveDeviceQCData(string p_strStartDat, string p_strEndDat, int[] p_intBatchSeqArr, out clsLisQCDataVO[] p_objQCDataArr)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngReceiveDeviceQCData(p_strStartDat, p_strEndDat, p_intBatchSeqArr, out p_objQCDataArr);

        }


        public long m_lngUpdateSDXCV(clsLisQCConcentrationVO p_objQCConcentration)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdateSDXCV(p_objQCConcentration);

        }

        public string m_strGetSysParam(string p_strParam)
        {
            string result = "";
            try
            {

                (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSysParam(p_strParam, out result);

            }
            catch
            {
                result = "";
            }
            return result;
        }
        #endregion

        #region frmQCSetup

        #region m_lngFindQCRule
        /// <summary>
        /// m_lngFindQCRule
        /// </summary>
        /// <param name="p_intSeq"></param>
        /// <param name="p_objRule"></param>
        /// <returns></returns>
        public long m_lngFindQCRule(int p_intSeq, out clsLisQCRuleVO p_objRule)
        {

            return (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCRule(p_intSeq, out p_objRule);

        }
        #endregion

        #region m_lngFindQCRule
        /// <summary>
        /// m_lngFindQCRule
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindQCRule(out clsLisQCRuleVO[] p_objResultArr)
        {

            return (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCRule(out p_objResultArr);

        }

        #endregion

        #region m_lngGetDeviceQCCheckItemByID
        /// <summary>
        /// m_lngGetDeviceQCCheckItemByID
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetDeviceQCCheckItemByID(string p_strDeviceID, out clsLISCheckItemNode[] p_objResultArr)
        {
            p_objResultArr = null;
            long num = 0L;
            long result;

            if (string.IsNullOrEmpty(p_strDeviceID))
            {
                result = num;
            }
            else
            {
                result = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetDeviceQCCheckItemByID(p_strDeviceID, out p_objResultArr);

            }
            return result;
        }

        #endregion

        #region m_lngInsertQCBatch
        /// <summary>
        /// m_lngInsertQCBatch
        /// </summary>
        /// <param name="p_objQCBatch"></param>
        /// <param name="p_intSeq"></param>
        /// <returns></returns>
        public long m_lngInsertQCBatch(clsLisQCBatchVO p_objQCBatch, out int p_intSeq)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertQCBatch(p_objQCBatch, out p_intSeq);

        }
        #endregion

        #region m_lngInsertQCBatchByArr
        /// <summary>
        /// m_lngInsertQCBatchByArr
        /// </summary>
        /// <param name="p_objQCBatchArr"></param>
        /// <param name="p_intSeqArr"></param>
        /// <returns></returns>
        public long m_lngInsertQCBatchByArr(clsLisQCBatchVO[] p_objQCBatchArr, out int[] p_intSeqArr)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertQCBatchByArr(p_objQCBatchArr, out p_intSeqArr);

        }
        #endregion

        #region m_lngUpdateQCBatch
        /// <summary>
        /// m_lngUpdateQCBatch
        /// </summary>
        /// <param name="QCBatch"></param>
        /// <returns></returns>
        public long m_lngUpdateQCBatch(clsLisQCBatchVO QCBatch)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdateQCBatch(QCBatch);

        }
        #endregion

        #region m_lngDeleteQCBatch
        /// <summary>
        /// m_lngDeleteQCBatch
        /// </summary>
        /// <param name="p_intSeq"></param>
        /// <returns></returns>
        public long m_lngDeleteQCBatch(int p_intSeq)
        {

            return (new weCare.Proxy.ProxyLis02()).Service.m_lngDeleteQCBatch(p_intSeq);

        }
        #endregion

        #region m_lngInsertBatchSet
        /// <summary>
        /// m_lngInsertBatchSet
        /// </summary>
        /// <param name="p_lstResult"></param>
        /// <param name="p_lstContion"></param>
        /// <returns></returns>
        public long m_lngInsertBatchSet(List<clsLisQCBatchVO> p_lstResult, List<clsLisQCConcentrationVO> p_lstContion)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertBatchSet(p_lstResult, p_lstContion);

        }
        #endregion

        #endregion

    }
}