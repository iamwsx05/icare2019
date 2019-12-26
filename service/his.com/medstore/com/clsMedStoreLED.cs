using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 药房查询电子屏业务
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [System.EnterpriseServices.ObjectPooling(true)]
    public class clsMedStoreLED : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsMedStoreLED()
        {

        }

        #region 09-10-12 杨镇伟添加: 查询所有配药病人信息
        /// <summary>
        /// 查询所有配药病人信息
        /// </summary>
        /// <param name="p_strMedStoreId">药房ID</param>
        /// <param name="p_strDate">查询时间(默认当天)</param>
        /// <param name="p_dtPatient">返回:病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDispensingPatient(string p_strMedStoreId, string p_strDate, out DataTable p_dtPatient)
        {
            long lngRes = 0;
            p_dtPatient = null;
            try
            {
                string strSQL = @"select b.lastname_vchr
                                  from t_opr_recipesend a, t_bse_patient b,t_opr_outpatientrecipe c,t_opr_recipesendentry d
                                 where a.patientid_chr = b.patientid_chr
                                   and a.medstoreid_chr = ?
                                   and a.createdate_chr = ?
                                   and a.pstatus_int = 1
                                   and a.sid_int = d.sid_int
                                   and d.outpatrecipeid_chr = c.outpatrecipeid_chr
                                   and c.pstauts_int <> -2
                                 order by a.serno_chr asc";
                clsHRPTableService clsHrpSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                clsHrpSvc.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strMedStoreId;
                objDPArr[1].Value = p_strDate;

                lngRes = clsHrpSvc.lngGetDataTableWithParameters(strSQL, ref p_dtPatient, objDPArr);
            }
            catch (Exception objex)
            {
                com.digitalwave.Utility.clsLogText clsError = new com.digitalwave.Utility.clsLogText();
                bool blnRes = clsError.LogError(objex.ToString());
            }
            return lngRes;
        }
        #endregion

        #region 查询发药窗口病人信息
        /// <summary>
        /// 查询发药窗口病人信息
        /// </summary>
        /// <param name="p_lstMedStoreId">药房ID</param>
        /// <param name="p_strDate">查询时间</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngSendMedicinePatient(List<string> p_lstMedStoreId, string p_strDate, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long lngRes = 0;
            string strSub = null;
            try {
                string strSQL = @" select a.serno_chr,
                                   a.sendwindowid_chr,
                                   b.lastname_vchr,
                                   a.called_int,
                                   a.recalled_int
                              from t_opr_recipesend       a,
                                   t_bse_patient          b,
                                   t_bse_medstorewin      c,
                                   t_opr_outpatientrecipe d,
                                   t_opr_recipesendentry  e
                             where a.createdate_chr = ?
                               and (a.pstatus_int = 2 or a.pstatus_int = 3)
                               and (a.called_int = 1 or a.recalled_int = 1 or a.quit_int = 1)
                               and a.patientid_chr = b.patientid_chr
                               and a.windowid_chr = c.windowid_chr(+)
                               and a.sid_int = e.sid_int
                               and e.outpatrecipeid_chr = d.outpatrecipeid_chr
                               and d.pstauts_int <> -2
                               and a.medstoreid_chr in (";
                for (int i1 = 0; i1 < p_lstMedStoreId.Count; i1++)
                {
                    strSub += "'" + p_lstMedStoreId[i1].ToString() + "',";
                }
                strSQL += strSub.Substring(0, strSub.Length - 1) + @") order by a.recalled_int desc,
           a.called_int   desc,
           a.quit_int     asc,
           a.recalled_dat asc";
                clsHRPTableService clsHrpSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                clsHrpSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDate;

                lngRes = clsHrpSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objDPArr);
                clsHrpSvc.Dispose();
                clsHrpSvc = null;
            }
            catch (Exception objex)
            {
                clsLogText clsError = new clsLogText();
                bool blnRes = clsError.LogError(objex.ToString());
            }
            return lngRes;
        }
        #endregion
    }
}
