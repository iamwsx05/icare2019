using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.emr.EMR_FollowUpSurveyServ
{
    /// <summary>
    /// 随访病人统计
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_FollowUpSurveyStatisticsServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取已随访病人数量

        /// <summary>
        /// 获取已随访病人数量(员工所属科室)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEMPID">员工ID</param>
        /// <param name="p_dtmDateStart">查询开始时间</param>
        /// <param name="p_dtmDateEnd">查询结束时间</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCountHasFollorUpSurveyPatient_Emp(string p_strEMPID, DateTime p_dtmDateStart, DateTime p_dtmDateEnd, out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            if (string.IsNullOrEmpty(p_strEMPID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select count(f.registerid_chr) patientcount, d.deptname_vchr,d.deptid_chr
  from T_EMR_FOLLOWUPSURVEYRECORD f
 inner join t_opr_bih_leave le on le.registerid_chr = f.registerid_chr
                              and le.STATUS_INT = 1
 inner join t_bse_deptdesc d on le.outdeptid_chr = d.deptid_chr
 where f.status = 1
   and le.modify_dat between ? and ?
   and exists (select t.deptid_chr
          from T_BSE_DEPTEMP t
         where t.empid_chr = ?
           and le.outdeptid_chr = t.deptid_chr)
 group by d.deptid_chr,d.deptname_vchr
 order by d.deptid_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDateStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmDateEnd;
                objDPArr[2].Value = p_strEMPID;

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
        /// 获取已随访病人数量(指定科室)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_dtmDateStart">查询开始时间</param>
        /// <param name="p_dtmDateEnd">查询结束时间</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCountHasFollorUpSurveyPatient_Dept(string p_strDeptID, DateTime p_dtmDateStart, DateTime p_dtmDateEnd, out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            if (string.IsNullOrEmpty(p_strDeptID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select count(f.registerid_chr) patientcount, d.deptname_vchr,d.deptid_chr
  from T_EMR_FOLLOWUPSURVEYRECORD f
 inner join t_opr_bih_leave le on le.registerid_chr = f.registerid_chr
                              and le.STATUS_INT = 1
 inner join t_bse_deptdesc d on le.outdeptid_chr = d.deptid_chr
 where f.status = 1
   and le.modify_dat between ? and ?
   and le.outdeptid_chr = ?
 group by d.deptid_chr,d.deptname_vchr
 order by d.deptid_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDateStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmDateEnd;
                objDPArr[2].Value = p_strDeptID;

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

        #region 获取出院病人数量
        /// <summary>
        /// 获取出院病人数量(员工所属科室)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEMPID">员工ID</param>
        /// <param name="p_dtmDateStart">查询开始时间</param>
        /// <param name="p_dtmDateEnd">查询结束时间</param>
        /// <param name="p_strDeathRecordTable">死亡记录表名(广西与其它医院不同表)</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCountLeavePatient_Emp(string p_strEMPID, DateTime p_dtmDateStart, DateTime p_dtmDateEnd, string p_strDeathRecordTable, out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            if (string.IsNullOrEmpty(p_strEMPID) || string.IsNullOrEmpty(p_strDeathRecordTable))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct count(le.registerid_chr) patientcount, d.deptname_vchr, d.deptid_chr
  from t_opr_bih_leave le
 inner join T_BSE_HISEMR_RELATION re on le.registerid_chr =
                                        re.registerid_chr
 inner join t_bse_deptdesc d on le.outdeptid_chr = d.deptid_chr
 where le.STATUS_INT = 1
   and le.modify_dat between ? and ?
   and exists (select t.deptid_chr
          from T_BSE_DEPTEMP t
         where t.empid_chr = ?
           and le.outdeptid_chr = t.deptid_chr)
   and not exists (select t.inpatientid
          from " + p_strDeathRecordTable + @" t
         where t.inpatientid = re.emrinpatientid
           and t.inpatientdate = re.emrinpatientdate
           and t.status = 0)
 group by d.deptid_chr, d.deptname_vchr
 order by d.deptid_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDateStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmDateEnd;
                objDPArr[2].Value = p_strEMPID;

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
        /// 获取出院病人数量(指定科室)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_dtmDateStart">查询开始时间</param>
        /// <param name="p_dtmDateEnd">查询结束时间</param>
        /// <param name="p_strDeathRecordTable">死亡记录表名(广西与其它医院不同表)</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCountLeavePatient_Dept(string p_strDeptID, DateTime p_dtmDateStart, DateTime p_dtmDateEnd, string p_strDeathRecordTable, out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            if (string.IsNullOrEmpty(p_strDeptID) || string.IsNullOrEmpty(p_strDeathRecordTable))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct count(le.registerid_chr) patientcount, d.deptname_vchr, d.deptid_chr
  from t_opr_bih_leave le
 inner join T_BSE_HISEMR_RELATION re on le.registerid_chr =
                                        re.registerid_chr
 inner join t_bse_deptdesc d on le.outdeptid_chr = d.deptid_chr
 where le.STATUS_INT = 1
   and le.modify_dat between ? and ?
   and le.outdeptid_chr = ?
   and not exists (select t.inpatientid
          from " + p_strDeathRecordTable + @" t
         where t.inpatientid = re.emrinpatientid
           and t.inpatientdate = re.emrinpatientdate
           and t.status = 0)
 group by d.deptid_chr, d.deptname_vchr";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDateStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmDateEnd;
                objDPArr[2].Value = p_strDeptID;

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

        #region 获取门诊随访人数
        /// <summary>
        /// 获取门诊随访人数(员工所属科室)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEMPID">员工ID</param>
        /// <param name="p_dtmDateStart">查询开始时间</param>
        /// <param name="p_dtmDateEnd">查询结束时间</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCountOutPatient_Emp(string p_strEMPID, DateTime p_dtmDateStart, DateTime p_dtmDateEnd, out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            if (string.IsNullOrEmpty(p_strEMPID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct count(f.registerid_chr) patientcount, d.deptname_vchr, d.deptid_chr
  from T_EMR_FOLLOWUPSURVEYRECORD f
 inner join T_EMR_FOLLOWUPSURVEYPATIENT pa on f.registerid_chr =
                                              pa.registerid_chr
 inner join t_opr_bih_leave le on le.registerid_chr = pa.registerid_chr
 inner join t_bse_deptdesc d on le.outdeptid_chr = d.deptid_chr
 where pa.isoutpatient = 1
   and f.status = 1
   and le.STATUS_INT = 1
   and le.modify_dat between ? and ?
   and exists (select t.deptid_chr
          from T_BSE_DEPTEMP t
         where t.empid_chr = ?
           and le.outdeptid_chr = t.deptid_chr)
 group by d.deptid_chr, d.deptname_vchr";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDateStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmDateEnd;
                objDPArr[2].Value = p_strEMPID;

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
        /// 获取门诊随访人数(指定科室)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_dtmDateStart">查询开始时间</param>
        /// <param name="p_dtmDateEnd">查询结束时间</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCountOutPatient_Dept(string p_strDeptID, DateTime p_dtmDateStart, DateTime p_dtmDateEnd, out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            if (string.IsNullOrEmpty(p_strDeptID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct count(f.registerid_chr) patientcount, d.deptname_vchr, d.deptid_chr
  from T_EMR_FOLLOWUPSURVEYRECORD f
 inner join T_EMR_FOLLOWUPSURVEYPATIENT pa on f.registerid_chr =
                                              pa.registerid_chr
 inner join t_opr_bih_leave le on le.registerid_chr = pa.registerid_chr
 inner join t_bse_deptdesc d on le.outdeptid_chr = d.deptid_chr
 where pa.isoutpatient = 1
   and f.status = 1
   and le.STATUS_INT = 1
   and le.modify_dat between ? and ?
   and le.outdeptid_chr = ?
 group by d.deptid_chr, d.deptname_vchr";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDateStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmDateEnd;
                objDPArr[2].Value = p_strDeptID;

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

        #region 统计诊断
        /// <summary>
        /// 统计诊断(员工所属科室)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">员工ID</param>
        /// <param name="p_dtmDateStart">查询开始时间</param>
        /// <param name="p_dtmDateEnd">查询结束时间</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCountDiagnosis_Emp(string p_strEmpID, DateTime p_dtmDateStart, DateTime p_dtmDateEnd, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strEmpID))
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @"select distinct count(i.registerid_chr) patientcount,
                d.deptname_vchr,
                d.deptid_chr,
                i.icd10,
                i.diagnosis
  from T_EMR_FOLLOWUPSURVEY_ICD i
 inner join t_opr_bih_leave le on i.registerid_chr = le.registerid_chr
 inner join t_bse_deptdesc d on le.outdeptid_chr = d.deptid_chr
 where i.status = 1
   and le.STATUS_INT = 1
   and le.modify_dat between ? and ?
   and exists (select t.deptid_chr
          from T_BSE_DEPTEMP t
         where t.empid_chr = ?
           and le.outdeptid_chr = t.deptid_chr)
 group by d.deptid_chr, d.deptname_vchr, i.icd10, i.diagnosis";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值


                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDateStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmDateEnd;
                objDPArr[2].Value = p_strEmpID;

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
        /// 统计诊断(指定科室)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_dtmDateStart">查询开始时间</param>
        /// <param name="p_dtmDateEnd">查询结束时间</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCountDiagnosis_Dept(string p_strDeptID, DateTime p_dtmDateStart, DateTime p_dtmDateEnd, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strDeptID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct count(i.registerid_chr) patientcount,
                d.deptname_vchr,
                d.deptid_chr,
                i.icd10,
                i.diagnosis
  from T_EMR_FOLLOWUPSURVEY_ICD i
 inner join t_opr_bih_leave le on i.registerid_chr = le.registerid_chr
 inner join t_bse_deptdesc d on le.outdeptid_chr = d.deptid_chr
 where i.status = 1
   and le.STATUS_INT = 1
   and le.modify_dat between ? and ?
   and le.outdeptid_chr = ?
 group by d.deptid_chr, d.deptname_vchr, i.icd10, i.diagnosis";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值


                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDateStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmDateEnd;
                objDPArr[2].Value = p_strDeptID;

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
    }
}
