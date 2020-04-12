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
    /// 住院病历评分公共类.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPublicGradeServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取一个序列号

        /// <summary>
        /// 获取一个序列号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSeq">序列号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSequence(out int p_lngSeq)
        {
            p_lngSeq = 0;
            //if (string.IsNullOrEmpty(strSeqName))
            //    return -1;

            long lngRes = 0;
            try
            {
                if (clsHRPTableService.bytDatabase_Selector == (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    string strSQL = "select seq_emr_casegrade.nextval from dual";
                    clsHRPTableService objHRPServ = new clsHRPTableService();

                    DataTable dtResult = null;
                    lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtResult);
                    if (dtResult != null && dtResult.Rows.Count == 1)
                    {
                        p_lngSeq = Convert.ToInt32(dtResult.Rows[0][0]);
                    }
                    else
                    {
                        p_lngSeq = 1;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region 获取多个序列号

        /// <summary>
        /// 获取多个序列号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intNum">数量</param>
        /// <param name="p_intSeqStart">起始序列号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSequenceArr(int p_intNum, out int p_intSeqStart)
        {
            p_intSeqStart = 0;
            if (p_intNum <= 0)
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                if (clsHRPTableService.bytDatabase_Selector == (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    DataTable dt = null;
                    string Sql = string.Format("select {0}.nextval from dual", "seq_emr_casegrade");
                    clsHRPTableService svc = new clsHRPTableService();
                    for (int i = p_intNum - 1; i >= 0; i--)
                    {
                        svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                        p_intSeqStart = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                    return 1;


                    //string strSQL = "select getseq(?,?) from dual";

                    //clsHRPTableService objHRPServ = new clsHRPTableService();
                    //DataTable dtValue = null;

                    //IDataParameter[] objDPArr = null;
                    //objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                    //objDPArr[0].Value = "seq_emr_casegrade";
                    //objDPArr[1].Value = p_intNum;

                    //lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtValue, objDPArr);
                    //if (lngRes > 0 && dtValue != null && dtValue.Rows.Count == 1)
                    //    p_intSeqStart = Convert.ToInt32(dtValue.Rows[0][0]);
                    //else
                    //    p_intSeqStart = 1;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion
    }
}
