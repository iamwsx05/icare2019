using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.AssistantToolService
{
    /// <summary>
    /// 医嘱系统-检验申请单接口
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_HIS_CheckRequisitionServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取检查申请单内容
        /// <summary>
        /// 获取检查申请单内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_strOrderID">医嘱流水号</param>
        /// <param name="p_objValue">申请单内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckRequisitionValue( string p_strRegisterID, string p_strOrderID, out clsEMR_HIS_CheckRequisitionValue p_objValue)
        {
            p_objValue = null;
            if (string.IsNullOrEmpty(p_strRegisterID) || string.IsNullOrEmpty(p_strOrderID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select t.casesummary_vchr,
       t.physexam_vchr,
       t.admissiondiagnosis_vchr,
       t.createdate_dat,
       t.status,
       t.recorddate_dat,
       t.createuserid
  from t_emr_his_checkrequisition t
 where t.registerid_chr = ?
   and t.orderid_chr = ?
   and t.status = 1";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strRegisterID.Trim();
                objDPArr[1].Value = p_strOrderID.Trim();

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null)
                {
                    int rowsCount = dtbValue.Rows.Count;
                    if (rowsCount <= 0)
                    {
                        return -1;
                    }

                    p_objValue = new clsEMR_HIS_CheckRequisitionValue();
                    p_objValue.m_strREGISTERID_CHR = p_strRegisterID;
                    p_objValue.m_strORDERID_CHR = p_strOrderID;
                    p_objValue.m_intSTATUS = Convert.ToInt32(dtbValue.Rows[0]["STATUS"]);
                    p_objValue.m_strCASESUMMARY_VCHR = dtbValue.Rows[0]["CASESUMMARY_VCHR"].ToString();
                    p_objValue.m_strADMISSIONDIAGNOSIS_VCHR = dtbValue.Rows[0]["ADMISSIONDIAGNOSIS_VCHR"].ToString();
                    p_objValue.m_strPHYSEXAM_VCHR = dtbValue.Rows[0]["PHYSEXAM_VCHR"].ToString();
                    p_objValue.m_dtmCREATEDATE_DAT = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE_DAT"]);
                    p_objValue.m_dtmRECORDDATE_DAT = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE_DAT"]);
                    p_objValue.m_strCREATEUSERID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 获取检查申请单内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_objValue">申请单内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckRequisitionValue( string p_strRegisterID, out clsEMR_HIS_CheckRequisitionValue[] p_objValue)
        {
            p_objValue = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select t.casesummary_vchr,orderid_chr,
       t.physexam_vchr,
       t.admissiondiagnosis_vchr,
       t.createdate_dat,
       t.status,
       t.recorddate_dat,
       t.createuserid
  from t_emr_his_checkrequisition t
 where t.registerid_chr = ?
   and t.status = 1";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strRegisterID.Trim();

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null)
                {
                    int rowsCount = dtbValue.Rows.Count;
                    if (rowsCount <= 0)
                    {
                        return -1;
                    }

                    p_objValue = new clsEMR_HIS_CheckRequisitionValue[rowsCount];
                    DataRow drCurrent = null;
                    for (int i = 0; i < rowsCount; i++)
                    {
                        drCurrent = dtbValue.Rows[i];
                        p_objValue[i] = new clsEMR_HIS_CheckRequisitionValue();
                        p_objValue[i].m_strREGISTERID_CHR = p_strRegisterID;
                        p_objValue[i].m_strORDERID_CHR = drCurrent["ORDERID_CHR"].ToString();
                        p_objValue[i].m_intSTATUS = Convert.ToInt32(drCurrent["STATUS"]);
                        p_objValue[i].m_strCASESUMMARY_VCHR = drCurrent["CASESUMMARY_VCHR"].ToString();
                        p_objValue[i].m_strADMISSIONDIAGNOSIS_VCHR = drCurrent["ADMISSIONDIAGNOSIS_VCHR"].ToString();
                        p_objValue[i].m_strPHYSEXAM_VCHR = drCurrent["PHYSEXAM_VCHR"].ToString();
                        p_objValue[i].m_dtmCREATEDATE_DAT = Convert.ToDateTime(drCurrent["CREATEDATE_DAT"]);
                        p_objValue[i].m_dtmRECORDDATE_DAT = Convert.ToDateTime(drCurrent["RECORDDATE_DAT"]);
                        p_objValue[i].m_strCREATEUSERID = drCurrent["CREATEUSERID"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion

        #region 添加申请
        /// <summary>
        /// 添加申请
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue">申请单内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewRequisition( clsEMR_HIS_CheckRequisitionValue p_objValue)
        {
            if (p_objValue == null)
            {
                return -1;
            }

            if (string.IsNullOrEmpty(p_objValue.m_strREGISTERID_CHR) || string.IsNullOrEmpty(p_objValue.m_strORDERID_CHR))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"insert into t_emr_his_checkrequisition
  (registerid_chr,
   orderid_chr,
   casesummary_vchr,
   physexam_vchr,
   admissiondiagnosis_vchr,
   createdate_dat,
   status,
   recorddate_dat,
   createuserid)
values
  (?, ?, ?, ?, ?, " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @", 1, ?, ?)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(7, out objDPArr);

                objDPArr[0].Value = p_objValue.m_strREGISTERID_CHR;
                objDPArr[1].Value = p_objValue.m_strORDERID_CHR;
                objDPArr[2].Value = p_objValue.m_strCASESUMMARY_VCHR;
                objDPArr[3].Value = p_objValue.m_strPHYSEXAM_VCHR;
                objDPArr[4].Value = p_objValue.m_strADMISSIONDIAGNOSIS_VCHR;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objValue.m_dtmRECORDDATE_DAT;
                objDPArr[6].Value = p_objValue.m_strCREATEUSERID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion

        #region 修改申请内容
        /// <summary>
        /// 修改申请内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue">申请单内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyRequisition(  clsEMR_HIS_CheckRequisitionValue p_objValue)
        {
            if (p_objValue == null)
            {
                return -1;
            }

            if (string.IsNullOrEmpty(p_objValue.m_strREGISTERID_CHR) || string.IsNullOrEmpty(p_objValue.m_strORDERID_CHR))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"update t_emr_his_checkrequisition t
   set t.casesummary_vchr        = ?,
       t.physexam_vchr           = ?,
       t.admissiondiagnosis_vchr = ?,
       t.recorddate_dat          = ?
 where t.registerid_chr = ?
   and t.orderid_chr = ?
   and t.status = 1";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);

                objDPArr[0].Value = p_objValue.m_strCASESUMMARY_VCHR;
                objDPArr[1].Value = p_objValue.m_strPHYSEXAM_VCHR;
                objDPArr[2].Value = p_objValue.m_strADMISSIONDIAGNOSIS_VCHR;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objValue.m_dtmRECORDDATE_DAT;
                objDPArr[4].Value = p_objValue.m_strREGISTERID_CHR;
                objDPArr[5].Value = p_objValue.m_strORDERID_CHR;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 删除申请
        /// <summary>
        /// 删除申请
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeactivedUser">删除者ID</param>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_strOrderID">医嘱流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRequisition( string p_strDeactivedUser, string p_strRegisterID, string p_strOrderID)
        {
            if (string.IsNullOrEmpty(p_strRegisterID) || string.IsNullOrEmpty(p_strOrderID) || string.IsNullOrEmpty(p_strDeactivedUser))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"update t_emr_his_checkrequisition t
   set t.status = 0,deactiveddate = " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @",deactivedoperatorid = ?
 where t.registerid_chr = ?
   and t.orderid_chr = ?
   and t.status = 1";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strDeactivedUser;
                objDPArr[1].Value = p_strRegisterID;
                objDPArr[2].Value = p_strOrderID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion
    }
}
