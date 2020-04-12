using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using ServMain = com.digitalwave.iCare.middletier.HRPService;//10g
using com.digitalwave.Utility;

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// 病历审签
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsAuditCaseServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 获取审签病历内容(统计用)
        /// </summary>
        /// <param name="p_dtmBeginDate">查询开始时间</param>
        /// <param name="p_dtmEndDate">查询结束时间</param>
        /// <param name="p_dtbCase">病历内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAuditCaseForStat(DateTime p_dtmBeginDate, DateTime p_dtmEndDate, out DataTable p_dtbCase)
        {
            p_dtbCase = null;

            long lngRes = 0;
            try
            {
                string strSQL = @"select d.deptid_chr,
       d.deptname_vchr,
       a.status_int,
       to_number(to_char((a.auditdate - a.createdate) * 24,'fm9990.99')) auditdiff
  from t_emr_caseaudit a
 inner join t_bse_deptdesc d on a.areaid_chr = d.deptid_chr
                            and d.status_int = 1
 where a.status_int <> -1
   and a.createdate between ? and ?";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(2, out objSeqArr1);
                objSeqArr1[0].DbType = DbType.DateTime;
                objSeqArr1[0].Value = p_dtmBeginDate;
                objSeqArr1[1].DbType = DbType.DateTime;
                objSeqArr1[1].Value = p_dtmEndDate;

                lngRes = objHRPMain.lngGetDataTableWithParameters(strSQL, ref p_dtbCase, objSeqArr1);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取审签病历内容
        /// </summary>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_dtmBeginDate">查询开始时间</param>
        /// <param name="p_dtmEndDate">查询结束时间</param>
        /// <param name="p_dtbCase">病历内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAuditCase(string p_strDeptID, DateTime p_dtmBeginDate, DateTime p_dtmEndDate, out DataTable p_dtbCase)
        {
            p_dtbCase = null;

            long lngRes = 0;
            try
            {
                string strSQL = @"select a.registerid_chr,
       a.createdate,
       a.formid_vchr,
       a.formname_vchr,
       a.status_int,
       a.creatorid,
       a.auditorid,
       a.auditdate,
       a.case_seq,
       a.deptid_chr,
       a.areaid_chr,
       decode(a.status_int, 0, '未审签', 1, '已审签', '') statusdesc,
       b.lastname_vchr creatorname,
       d.lastname_vchr auditorname,
       be.bed_no,
       rd.lastname_vchr
  from t_emr_caseaudit a
  inner join t_opr_bih_register r on a.registerid_chr = r.registerid_chr and r.status_int = 1
  inner join t_bse_bed be on be.bedid_chr = r.bedid_chr and be.status_int <> 5
  inner join t_opr_bih_registerdetail rd on rd.registerid_chr = a.registerid_chr and rd.status_int=1
 inner join t_bse_employee b on a.creatorid = b.empid_chr
                            and b.status_int = 1
  left outer join t_bse_employee d on a.auditorid = d.empid_chr
                                  and d.status_int = 1
 where a.status_int <> -1
   and a.areaid_chr = ?
   and a.createdate between ? and ?
 order by createdate desc";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(3, out objSeqArr1);
                objSeqArr1[0].Value = p_strDeptID;
                objSeqArr1[1].DbType = DbType.DateTime;
                objSeqArr1[1].Value = p_dtmBeginDate;
                objSeqArr1[2].DbType = DbType.DateTime;
                objSeqArr1[2].Value = p_dtmEndDate;

                lngRes = objHRPMain.lngGetDataTableWithParameters(strSQL, ref p_dtbCase, objSeqArr1);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取审签病历内容
        /// </summary>
        /// <param name="p_strEmpID">员工ID</param>
        /// <param name="p_dtbCase">病历内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAuditCase(string p_strEmpID, out DataTable p_dtbCase)
        {
            p_dtbCase = null;

            long lngRes = 0;
            try
            {
                string strSQL = @"select a.registerid_chr,
       a.createdate,
       a.formid_vchr,
       a.formname_vchr,
       a.status_int,
       a.creatorid,
       a.auditorid,
       a.auditdate,
       a.case_seq,
       a.deptid_chr,
       a.areaid_chr,
       decode(a.status_int, 0, '未审签', 1, '已审签', '') statusdesc,
       b.lastname_vchr creatorname,
       c.lastname_vchr auditorname,
       be.bed_no,
       rd.lastname_vchr
  from t_emr_caseaudit a
  inner join t_opr_bih_register r on a.registerid_chr = r.registerid_chr and r.status_int = 1
  inner join t_bse_bed be on be.bedid_chr = r.bedid_chr and be.status_int <> 5
  inner join t_opr_bih_registerdetail rd on rd.registerid_chr = a.registerid_chr and rd.status_int=1
 inner join t_bse_employee b on a.creatorid = b.empid_chr
                            and b.status_int = 1
  left outer join t_bse_employee c on a.auditorid = c.empid_chr
                                  and c.status_int = 1
 where a.status_int <> -1
   and b.empid_chr = ?
 order by createdate desc";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(1, out objSeqArr1);
                objSeqArr1[0].Value = p_strEmpID;

                lngRes = objHRPMain.lngGetDataTableWithParameters(strSQL, ref p_dtbCase, objSeqArr1);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
    }

    /// <summary>
    /// 病历审签
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsAuditCaseServ_Modify : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 添加记录至病历审签表
        /// </summary>
        /// <param name="p_objVO">记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddCaseAudit(clsEMR_CaseAuditVO p_objVO)
        {
            if (p_objVO == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"insert into t_emr_caseaudit
  (registerid_chr,
   createdate,
   formid_vchr,
   formname_vchr,
   status_int,
   creatorid,
   deptid_chr,
   areaid_chr,
   case_seq)
values
  (?, ?, ?, ?, ?, ?, ?, ?,seq_auditcase.nextval)";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(8, out objSeqArr1);
                objSeqArr1[0].Value = p_objVO.m_strREGISTERID_CHR;
                objSeqArr1[1].DbType = DbType.DateTime;
                objSeqArr1[1].Value = p_objVO.m_dtmCREATEDATE;
                objSeqArr1[2].Value = p_objVO.m_strFORMID_VCHR;
                objSeqArr1[3].Value = p_objVO.m_strFORMNAME_VCHR;
                objSeqArr1[4].Value = p_objVO.m_intSTATUS_INT;
                objSeqArr1[5].Value = p_objVO.m_strCREATORID;
                objSeqArr1[6].Value = p_objVO.m_strDeptID;
                objSeqArr1[7].Value = p_objVO.m_strAreaID;

                long lngEff = -1;
                lngRes = objHRPMain.lngExecuteParameterSQL(strSQL, ref lngEff, objSeqArr1);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        /// <summary>
        /// 删除病案审签内容
        /// </summary>
        /// <param name="p_objVO">病案审签内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelCaseAudit(clsEMR_CaseAuditVO p_objVO)
        {
            if (p_objVO == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update t_emr_caseaudit
   set status_int = -1
 where registerid_chr = ?
   and createdate = ?
   and formid_vchr = ?
   and creatorid = ?";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(4, out objSeqArr1);
                objSeqArr1[0].Value = p_objVO.m_strREGISTERID_CHR;
                objSeqArr1[1].DbType = DbType.DateTime;
                objSeqArr1[1].Value = p_objVO.m_dtmCREATEDATE;
                objSeqArr1[2].Value = p_objVO.m_strFORMID_VCHR;
                objSeqArr1[3].Value = p_objVO.m_strCREATORID;

                long lngEff = -1;
                lngRes = objHRPMain.lngExecuteParameterSQL(strSQL, ref lngEff, objSeqArr1);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        /// <summary>
        /// 审签病历
        /// </summary>
        /// <param name="p_objVO">病历</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAuditCase(clsEMR_CaseAuditVO p_objVO)
        {
            if (p_objVO == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update t_emr_caseaudit
   set status_int = 1, auditorid = ?, auditdate = ?
 where case_seq = ? and status_int = 0";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(3, out objSeqArr1);
                objSeqArr1[0].Value = p_objVO.m_strAUDITORID;
                objSeqArr1[1].DbType = DbType.DateTime;
                objSeqArr1[1].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objSeqArr1[2].Value = p_objVO.m_lngCASE_SEQ;

                long lngEff = -1;
                lngRes = objHRPMain.lngExecuteParameterSQL(strSQL, ref lngEff, objSeqArr1);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
    }
}
