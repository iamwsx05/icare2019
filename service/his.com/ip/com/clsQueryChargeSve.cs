using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 病人费用查询    
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsQueryChargeSve : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 根据病区ID查询病人费用信息
        /// <summary>
        /// 根据病区ID查询病人费用信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeInfoByAreaID(string p_strAreaID, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT a.registerid_chr, a.sumcharge, b.sumcharge AS medicimecharge,
       c.registerid_chr, c.inpatientid_chr, d.lastname_vchr, d.sex_chr,
       d.birth_dat, c.areaid_chr, e.deptname_vchr,
       f.lastname_vchr AS maindoctor
  FROM (SELECT   t1.registerid_chr,
                 SUM (t1.amount_dec * t1.unitprice_dec) AS sumcharge
            FROM t_opr_bih_patientcharge t1
           WHERE t1.status_int = 1
        GROUP BY t1.registerid_chr) a,
       (SELECT   t1.registerid_chr,
                 SUM (t1.amount_dec * t1.unitprice_dec) AS sumcharge
            FROM t_opr_bih_patientcharge t1,
                 t_opr_bih_order t2,
                 t_bse_bih_orderdic t3
           WHERE t1.orderid_chr = t2.orderid_chr(+)
             AND t2.orderdicid_chr = t3.orderdicid_chr(+)
             AND t3.ordercateid_chr = '01'
             AND t1.status_int = 1
        GROUP BY t1.registerid_chr) b,
       t_opr_bih_register c,
       t_bse_patient d,
       t_bse_deptdesc e,                                                   
       t_bse_employee f
 WHERE a.registerid_chr = b.registerid_chr(+)
   AND a.registerid_chr = c.registerid_chr
   AND c.patientid_chr = d.patientid_chr
   AND c.areaid_chr = e.deptid_chr
   AND c.casedoctor_chr = f.empid_chr(+)";
            if (p_strAreaID != "")
            {
                strSQL += @"
   AND c.areaid_chr = '" + p_strAreaID + "'";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据病人入院登记ID查询病人医嘱信息
        /// <summary>
        /// 根据病人入院登记ID查询病人医嘱信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记ID</param>
        /// <param name="p_dtbRecord">查询结果数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderInfoByRegisterID(string p_strRegisterID, out DataTable p_dtbRecord)
        {
            p_dtbRecord = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT t1.recipeno_int, t1.executetype_int, t1.name_vchr,
       t1.dosage_dec || t1.dosageunit_chr AS dosage,
       t1.get_dec || t1.getunit_chr AS usequantity, t1.execfreqname_chr,
       t1.dosetypename_chr,t1.creator_chr,t1.createdate_dat
  FROM t_opr_bih_order t1
 WHERE t1.status_int IN (0, 7, 1, 5, 2, 3, 6)
   AND t1.registerid_chr = '" + p_strRegisterID + @"'
ORDER BY t1.createdate_dat";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }

}
