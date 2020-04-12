using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// 出院病历缺陷分类统计服务类

    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsCaseViceStatServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 获得统计病历
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_Query">查询条件</param>
        /// <param name="p_dtResult">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStatCase( clsQueryCondition_VO p_Query, out DataTable p_dtResult)
        {
            p_dtResult = null;
            if (p_Query == null)
                return -1;

            long lngRes = 0;
            try
            { 
                string strSQL = @"select a.hisinpatientid_chr,
       b.graded_int,
       b.gradeseqid_int,
       d.itemid_int,
       d.parentitemid_int
  from t_bse_hisemr_relation  a,
       t_emr_casegraded       b,
       t_emr_casegradeddetail c,
       t_emr_casegradeitem    d
 where a.registerid_chr = b.registerid_chr
   and c.gradeseqid_int = b.gradeseqid_int
   and c.itemid_int = d.itemid_int
   and b.status_int = 1
   and c.status_int = 1
   and d.status_int = 1
   and b.outhospitaldate_dat between ? and ?";

                int iParameterCount = 2;
                if (p_Query.m_strAreaId_chr != string.Empty)
                {
                    strSQL += " and b.areaid_chr = ?";
                    iParameterCount++;
                }
                strSQL += " order by a.hisinpatientid_chr, b.outhospitaldate_dat";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(iParameterCount, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_Query.m_dtOutHospitalDate1_dat.Date;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_Query.m_dtOutHospitalDate2_dat.Date.AddHours(24);
                if (iParameterCount == 3)
                {
                    objDPArr[2].Value = p_Query.m_strAreaId_chr;
                }

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            p_Query = null;
            return lngRes;
        }
    }
}
