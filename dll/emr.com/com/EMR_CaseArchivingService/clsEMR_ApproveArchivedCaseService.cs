using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.emr.EMR_CaseArchivingService
{
    /// <summary>
    /// 病案审批
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_ApproveArchivedCaseService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取病案借阅情况
        /// <summary>
        /// 获取病案借阅情况
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtmStart">查询时间范围－开始时间</param>
        /// <param name="p_dtmEnd">查询时间范围－结束时间</param>
        /// <param name="p_dtbResult">病案借阅情况</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBorrowArchivedCaseInfo( DateTime p_dtmStart, DateTime p_dtmEnd, out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            long lngRes = -1;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select distinct ca.*,
                ma.casecount,
                emp.lastname_vchr subscribername,
                emp1.lastname_vchr SUPERIORNAME,
                emp2.lastname_vchr APPROVERNAME,
                dept.deptid_chr defaultdeptid,
                dept.deptname_vchr defaultdeptname
  from T_EMR_CASESUBSCRIB ca
 inner join (select distinct count(a.archivedid_chr) casecount,
                             a.subscribid_chr
               from T_EMR_CASEEMPMAP a
              group by a.subscribid_chr) ma on ca.subscribid_chr =
                                               ma.subscribid_chr
 inner join t_bse_employee emp on emp.empid_chr = ca.subscriber_chr
  left outer join t_bse_employee emp1 on emp1.empid_chr = ca.SUPERIOR_CHR
  left outer join t_bse_employee emp2 on emp2.empid_chr = ca.APPROVER_CHR
  left outer join (select p.deptname_vchr, p.deptid_chr, d.empid_chr
                     from t_bse_deptdesc p, t_bse_deptemp d
                    where p.deptid_chr = d.deptid_chr
                      and d.default_inpatient_dept_int = 1) dept on dept.empid_chr =
                                                                    ca.subscriber_chr
 where ca.SUBSCRIBSTATUS_INT <> -1 and ca.CREATEDATE_DAT between ? and ?
 order by ca.SUBSCRIBSTATUS_INT, ca.BEGINDATE_DAT desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //赋值


                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmEnd;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取病案借阅情况
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_dtmStart">查询时间范围－开始时间</param>
        /// <param name="p_dtmEnd">查询时间范围－结束时间</param>
        /// <param name="p_dtbResult">病案借阅情况</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBorrowArchivedCaseInfo( string p_strDeptID, DateTime p_dtmStart, DateTime p_dtmEnd, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strDeptID))
            {
                return -1;
            }

            long lngRes = -1;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select distinct ca.*,
                ma.casecount,
                emp.lastname_vchr subscribername,
                emp1.lastname_vchr SUPERIORNAME,
                emp2.lastname_vchr APPROVERNAME,
                dept.deptid_chr defaultdeptid,
                dept.deptname_vchr defaultdeptname
  from T_EMR_CASESUBSCRIB ca
 inner join T_BSE_DEPTEMP de on de.empid_chr = ca.subscriber_chr
                            and de.default_inpatient_dept_int = 1
 inner join (select distinct count(a.archivedid_chr) casecount,
                             a.subscribid_chr
               from T_EMR_CASEEMPMAP a
              group by a.subscribid_chr) ma on ca.subscribid_chr =
                                                                 ma.subscribid_chr
 inner join t_bse_employee emp on emp.empid_chr = ca.subscriber_chr
 left outer join t_bse_employee emp1 on emp1.empid_chr = ca.SUPERIOR_CHR
  left outer join t_bse_employee emp2 on emp2.empid_chr = ca.APPROVER_CHR
  left outer join (select p.deptname_vchr, p.deptid_chr, d.empid_chr
                     from t_bse_deptdesc p, t_bse_deptemp d
                    where p.deptid_chr = d.deptid_chr
                      and d.default_inpatient_dept_int = 1) dept on dept.empid_chr =
                                                                    ca.subscriber_chr
 where de.deptid_chr = ? and ca.SUBSCRIBSTATUS_INT <> -1 and ca.CREATEDATE_DAT between ? and ? 
 order by ca.SUBSCRIBSTATUS_INT,ca.BEGINDATE_DAT desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值


                objDPArr[0].Value = p_strDeptID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmStart;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmEnd;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 审批病案
        /// <summary>
        /// 审批病案
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue">VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngApproveArchivedCase( clsEMR_CaseSubscribeValue p_objValue)
        {
            if (p_objValue == null || (p_objValue.m_intSUBSCRIBSTATUS_INT != 1 && p_objValue.m_intSUBSCRIBSTATUS_INT != 2))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strDateSQL = string.Empty;
                if (p_objValue.m_intSUBSCRIBSTATUS_INT == 1)
                {
                    strDateSQL = @"ca.begindate_dat = ?,
       ca.enddate_dat   = ?,";
                }

                string strSQL = @"update T_EMR_CASESUBSCRIB ca
   set " + strDateSQL + @"
       CLINICSIGN_CHR   = ?,
       APPROVER_CHR     = ?,
       APPROVEDDATE_DAT = " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @",
       ca.subscribstatus_int = ?
 where ca.subscribid_chr = ?";

                IDataParameter[] objDPArr = null;

                if (p_objValue.m_intSUBSCRIBSTATUS_INT == 1)
                {
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    //赋值

                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = p_objValue.m_dtmBEGINDATE_DAT;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objValue.m_dtmENDDATE_DAT;
                    objDPArr[2].Value = p_objValue.m_strCLINICSIGN_CHR;
                    objDPArr[3].Value = p_objValue.m_strAPPROVER_CHR;
                    objDPArr[4].Value = p_objValue.m_intSUBSCRIBSTATUS_INT;
                    objDPArr[5].Value = p_objValue.m_lngSUBSCRIBID;
                }
                else
                {
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].Value = p_objValue.m_strCLINICSIGN_CHR;
                    objDPArr[1].Value = p_objValue.m_strAPPROVER_CHR;
                    objDPArr[2].Value = p_objValue.m_intSUBSCRIBSTATUS_INT;
                    objDPArr[3].Value = p_objValue.m_lngSUBSCRIBID;
                }

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 取消批准
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSubscribid">申请流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCancelApprove( long p_lngSubscribid)
        {
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"update T_EMR_CASESUBSCRIB ca
   set ca.subscribstatus_int = 2
 where ca.subscribid_chr = ?";

                IDataParameter[] objDPArr = null;

                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngSubscribid;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据病案申请流水号获取申请病案的级别
        /// <summary>
        /// 根据病案申请流水号获取申请病案的级别
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSubscribID">病案申请流水号</param>
        /// <param name="p_intCaseLevelArr">病案级别</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCaseLevelBySubscribID( long p_lngSubscribID, out int[] p_intCaseLevelArr)
        {
            long lngRes = 0;
            p_intCaseLevelArr = null;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select ca.CASELEVEL_INT
  from T_EMR_CASESUBSCRIB su
 inner join T_EMR_CASEEMPMAP ma on su.subscribid_chr = ma.subscribid_chr
 inner join T_EMR_CASEARCHIVING ca on ca.archivedid_int = ma.archivedid_chr
 where su.subscribid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值


                objDPArr[0].Value = p_lngSubscribID;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    DataRow drCurrent = null;
                    p_intCaseLevelArr = new int[intRowsCount];
                    for (int i1 = 0; i1 < intRowsCount; i1++)
                    {
                        drCurrent = dtbResult.Rows[i1];
                        p_intCaseLevelArr[i1] = Convert.ToInt32(drCurrent[0]);
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

        #region 获取有效的未进行审批操作的病案申请

        /// <summary>
        /// 获取有效的未进行审批操作的病案申请(即时提醒用)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValueArr">病案申请内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUnApprovedArchivedCaseInfo( out clsEMR_CaseSubscribeValue[] p_objValueArr)
        {
            long lngRes = 0;
            p_objValueArr = null;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select ca.*, emp.lastname_vchr,
                dept.deptid_chr defaultdeptid,
                dept.deptname_vchr defaultdeptname
  from T_EMR_CASESUBSCRIB ca
 inner join t_bse_employee emp on ca.subscriber_chr = emp.empid_chr
  left outer join (select p.deptname_vchr, p.deptid_chr, d.empid_chr
                     from t_bse_deptdesc p, t_bse_deptemp d
                    where p.deptid_chr = d.deptid_chr
                      and d.default_inpatient_dept_int = 1) dept on dept.empid_chr =
                                                                    ca.subscriber_chr
 where ca.subscribstatus_int = 0
   and ca.enddate_dat > " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @"
 order by ca.createdate_dat";

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    DataRow drCurrent = null;
                    p_objValueArr = new clsEMR_CaseSubscribeValue[intRowsCount];
                    for (int i1 = 0; i1 < intRowsCount; i1++)
                    {
                        drCurrent = dtbResult.Rows[i1];
                        p_objValueArr[i1] = new clsEMR_CaseSubscribeValue();
                        p_objValueArr[i1].m_dtmBEGINDATE_DAT = Convert.ToDateTime(drCurrent["BEGINDATE_DAT"]);
                        p_objValueArr[i1].m_dtmENDDATE_DAT = Convert.ToDateTime(drCurrent["ENDDATE_DAT"]);
                        p_objValueArr[i1].m_intSUBSCRIBSTATUS_INT = 0;
                        p_objValueArr[i1].m_lngSUBSCRIBID = Convert.ToInt64(drCurrent["SUBSCRIBID_CHR"]);
                        p_objValueArr[i1].m_strACCOUNTFOR_VCHR = drCurrent["ACCOUNTFOR_VCHR"].ToString();
                        p_objValueArr[i1].m_strSUBSCRIBER_CHR = drCurrent["SUBSCRIBER_CHR"].ToString();
                        p_objValueArr[i1].m_strSUBSCRIBERNAME = drCurrent["lastname_vchr"].ToString();
                        p_objValueArr[i1].m_strDefaultDeptID = drCurrent["defaultdeptid"].ToString();
                        p_objValueArr[i1].m_strDefaultDeptName = drCurrent["defaultdeptname"].ToString();
                        p_objValueArr[i1].m_dtmCREATEDATE_DAT = Convert.ToDateTime(drCurrent["CREATEDATE_DAT"]);
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

        #region 获取指定员工申请查阅指定病人病案的审批情况

        /// <summary>
        /// 获取指定员工申请查阅指定病人病案的审批情况,
        /// 只查询查阅申请结束时间未超过当前时间的记录

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSubscriberID">员工ID</param>
        /// <param name="p_strRegisterID">病人入院登记号</param>
        /// <param name="p_intSubscribStatusArr">审批情况</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSpecifyApproveInfo( string p_strSubscriberID, string p_strRegisterID, out int[] p_intSubscribStatusArr)
        {
            long lngRes = 0;
            p_intSubscribStatusArr = null;

            if (string.IsNullOrEmpty(p_strSubscriberID) || string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select su.subscribstatus_int
  from T_EMR_CASEARCHIVING ar
 inner join T_EMR_CASEEMPMAP ma on ma.archivedid_chr = ar.archivedid_int
 inner join T_EMR_CASESUBSCRIB su on su.subscribid_chr = ma.subscribid_chr
 where ar.registerid_chr = ?
   and su.subscriber_chr = ?
   and su.enddate_dat > " + clsDatabaseSQLConvert.s_StrGetServDateFuncName;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                objDPArr[1].Value = p_strSubscriberID;

                DataTable dtValue = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtValue, objDPArr);

                if (lngRes > 0 && dtValue != null && dtValue.Rows.Count > 0)
                {
                    int rowsConut = dtValue.Rows.Count;
                    List<int> intStatus = new List<int>();
                    int intTemp = 0;
                    for (int i1 = 0; i1 < rowsConut; i1++)
                    {
                        if (int.TryParse(dtValue.Rows[i1][0].ToString(),out intTemp))
                        {
                            intStatus.Add(intTemp);
                        }
                    }
                    p_intSubscribStatusArr = intStatus.ToArray();
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
