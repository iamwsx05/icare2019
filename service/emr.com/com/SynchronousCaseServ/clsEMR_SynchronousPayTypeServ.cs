using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCaseServ
{
    /// <summary>
    /// 费别同步
    /// </summary>
    [Transaction(TransactionOption.NotSupported)]
    [ObjectPooling(true)]
    public class clsEMR_SynchronousPayTypeServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取费别列表
        /// <summary>
        /// 获取费别列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResutl">费别列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRelationPayTypeList(out DataTable p_dtbResutl)
        {
            p_dtbResutl = null;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select t.paytypeid_chr,t.ba_paytypeid_chr from t_emr_paytype_relation t";

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResutl);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取iCare费别列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResutl">费别列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetICarePayTypeList( out DataTable p_dtbResutl)
        {
            p_dtbResutl = null;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select t.paytypeid_chr code, t.paytypename_vchr name
  from t_bse_patientpaytype t
 where t.payflag_dec = 0
    or t.payflag_dec = 2
 order by t.paytypename_vchr";

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResutl);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取病案系统费别列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResutl">费别列表</param>
        /// <returns></returns>
        //[AutoComplete]
        public long m_lngGetBAPayTypeList( out DataTable p_dtbResutl)
        {
            p_dtbResutl = null;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);

                string strSQL = @"select t.fbh code, t.fmc name
  from tstandardmx t
 where t.fcode = 'gbfkfs'
   and t.fzf = 0
 order by t.fpx";

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResutl);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 保存费别设置
        /// <summary>
        /// 保存费别设置
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBAPayTypeID">病案费别ID</param>
        /// <param name="p_strICarePayTypeID">与病案费别ID对应的iCare系统费别ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSavePayTypeConfig(string p_strBAPayTypeID, string[] p_strICarePayTypeID)
        {
            if (string.IsNullOrEmpty(p_strBAPayTypeID))
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strDelSQL = @"delete from t_emr_paytype_relation where BA_PAYTYPEID_CHR = ?";
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr1);
                objDPArr1[0].Value = p_strBAPayTypeID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strDelSQL, ref lngEff, objDPArr1);

                if (lngRes <= 0)
                {
                    return -1;
                }

                if (p_strICarePayTypeID == null || p_strICarePayTypeID.Length <= 0)
                {
                    return 1;
                }

                string strSQL = @"insert into t_emr_paytype_relation t (t.paytypeid_chr,t.ba_paytypeid_chr) values(?,?)";
                lngEff = -1;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;
                    for (int iD = 0; iD < p_strICarePayTypeID.Length; iD++)
                    {
                        objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                        objDPArr[0].Value = p_strICarePayTypeID[iD];
                        objDPArr[1].Value = p_strBAPayTypeID;

                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.String };
                    object[][] objValues = new object[2][];

                    int intItemCount = p_strICarePayTypeID.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//初始化


                    }

                    for (int rows = 0; rows < intItemCount; rows++)
                    {
                        objValues[0][rows] = p_strICarePayTypeID[rows];
                        objValues[1][rows] = p_strBAPayTypeID;
                    }

                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取相关的病案系统费别
        /// <summary>
        /// 获取相关的病案系统费别
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPayTypeID">iCare系统费别</param>
        /// <param name="p_strBA_PayTypeID">病案系统费别</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBAPayType(string p_strPayTypeID, out string p_strBA_PayTypeID)
        {
            p_strBA_PayTypeID = string.Empty;
            if (string.IsNullOrEmpty(p_strPayTypeID))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select ba_paytypeid_chr from t_emr_paytype_relation where paytypeid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strPayTypeID;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0)
                {
                    if (dtbResult != null && dtbResult.Rows.Count == 1)
                    {
                        p_strBA_PayTypeID = dtbResult.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取相关的iCare系统费别
        /// <summary>
        /// 获取相关的iCare系统费别
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBA_PayTypeID">病案系统费别</param>
        /// <param name="p_strPayTypeIDArr">iCare系统费别</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetICarePayType(string p_strBA_PayTypeID, out string[] p_strPayTypeIDArr)
        {
            p_strPayTypeIDArr = null;
            if (string.IsNullOrEmpty(p_strBA_PayTypeID))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select paytypeid_chr from t_emr_paytype_relation where ba_paytypeid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBA_PayTypeID;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount > 0)
                    {
                        p_strPayTypeIDArr = new string[intRowsCount];
                        for (int iT = 0; iT < intRowsCount; iT++)
                        {
                            p_strPayTypeIDArr[iT] = dtbResult.Rows[iT][0].ToString();
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }
}
