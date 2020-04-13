using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsMedicineSourceSvc 的摘要说明。
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsMedicineSourceSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsMedicineSourceSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 查询药品
        /// <summary>
        /// 查询药品
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strType"></param>
        /// <param name="strContent"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindChargeItem(string strType, string strContent, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;

            try
            {

                string strSQL = @"SELECT   a.MEDICINEID_CHR ID, A.MEDICINENAME_VCHR NAME, a.MEDICINESTDID_CHR,
         b.MEDICINESTDNAME_VCHR
    FROM t_bse_medicine a, T_BSE_MEDICINESTD b
   WHERE a.MEDICINESTDID_CHR = b.MEDICINESTDID_CHR(+)
order by A.MEDICINEID_CHR";
                if (strContent.Trim() != "")
                {
                    strSQL = @"SELECT   a.MEDICINEID_CHR ID, A.MEDICINENAME_VCHR NAME, a.MEDICINESTDID_CHR,
         b.MEDICINESTDNAME_VCHR
    FROM t_bse_medicine a, T_BSE_MEDICINESTD b
   WHERE a.MEDICINESTDID_CHR = b.MEDICINESTDID_CHR(+)
and A." + strType + @" like '" + strContent + @"%'
order by A.MEDICINEID_CHR";
                }
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据源类型取回源的列表
        [AutoComplete]
        public long m_lngFindAllSour(out DataTable dtResult)
        {
            string strSQL = "select MEDICINESTDID_CHR ID,MEDICINESTDNAME_VCHR Name From T_BSE_MEDICINESTD";
            dtResult = new DataTable();
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region 修改用法
        [AutoComplete]
        public long m_mthSaveData(string strItemID, string strSourceID)
        {
            long lngRes = 0;
            string strSQL = "UPDate T_BSE_MEDICINE Set MEDICINESTDID_CHR='" + strSourceID.Trim() + "' Where MEDICINEID_CHR='" + strItemID.Trim() + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }
}
