using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_FollowUpSurveyServ
{
    /// <summary>
    /// 病人随访
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_FollowUpSurveyServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取出院病人信息
        /// <summary>
        /// 获取出院病人信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDept">科室ID</param>
        /// <param name="p_intDeptType">科室类型(2:科室,3:病区)</param>
        /// <param name="p_dtmDateStart">查询开始时间</param>
        /// <param name="p_dtmEndDate">查询结束时间</param>
        /// <param name="p_strHospitalNO">医院代码</param>
        /// <param name="p_dtbResult">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutPatientInfo(string p_strDept, int p_intDeptType, DateTime p_dtmDateStart, DateTime p_dtmEndDate, string p_strHospitalNO, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strDept) || string.IsNullOrEmpty(p_strHospitalNO) || (p_intDeptType != 2 && p_intDeptType != 3))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                #region 根据不同情况生成不同SQL
                string strDept = string.Empty;
                if (p_intDeptType == 2)
                {
                    strDept = "outdeptid_chr";
                }
                else
                {
                    strDept = "outareaid_chr";
                }

                string strDeathRecordTable = string.Empty;
                string strSearchInHospitalMainDiagnosis = string.Empty;
                if (p_strHospitalNO == "450101001")
                {
                    strDeathRecordTable = "deadrecord";
                    strSearchInHospitalMainDiagnosis = @"
          left outer join t_emr_inhospitalmainrec_gxop imop on imop.registerid_chr =
                                                               le.registerid_chr
                                                           and imop.status = 0
                                                           and imop.seqid = '0'
          left outer join t_emr_inhospitalmainrec_gxcon imcon on imcon.registerid_chr =
                                                                                            imop.registerid_chr
                                                                                            and imcon.status = 0";
                }
                else
                {
                    strDeathRecordTable = "DEATHRECORD";
                    strSearchInHospitalMainDiagnosis = @"
          left outer join inhospitalmainrecord_operation imop on imop.inpatientid =
                                                                 re.emrinpatientid
                                                             and imop.inpatientdate =
                                                                 re.emrinpatientdate
                                                             and imop.status = 1
                                                             and imop.seqid = '0'
          left outer join inhospitalmainrecord_content imcon on imcon.inpatientid =
                                                                                            re.emrinpatientid
                                                                                        and imcon.inpatientdate =
                                                                                            re.emrinpatientdate
                                                                                        and imcon.status = 0";
                }
                #endregion

                #region 合并SQL
                StringBuilder stbSQL = new StringBuilder(200);
                stbSQL.Append(@"select *
  from (select distinct le.modify_dat outdate,
               re.hisinpatientid_chr,
               re.hisinpatientdate,
               re.emrinpatientid,
               re.emrinpatientdate,
               le.registerid_chr,
               pa.lastname_vchr,
               pa.sex_chr,
               pa.birth_dat,
               pa.homeaddress_vchr,
               pa.homephone_vchr,
               fr.registerid_chr recordid,
               imop.operationname,
               imcon.maindiagnosis
          from t_opr_bih_leave le
         inner join t_opr_bih_registerdetail pa on pa.registerid_chr =
                                                   le.registerid_chr
         inner join t_bse_hisemr_relation re on le.registerid_chr =
                                                re.registerid_chr
          left outer join t_emr_followupsurveyrecord fr on fr.registerid_chr =
                                                           pa.registerid_chr");
                stbSQL.Append(strSearchInHospitalMainDiagnosis);
                stbSQL.Append(@"
         where le.");
                stbSQL.Append(strDept);
                stbSQL.Append(@" = ?
           and le.status_int = 1
           and le.modify_dat between ? and ?
           and not exists
         (select s.registerid_chr
                  from t_emr_followupsurveypatient s
                 where s.registerid_chr = le.registerid_chr)
           and not exists (select t.inpatientid
                  from ");
                stbSQL.Append(strDeathRecordTable);
                stbSQL.Append(@" t
                 where t.inpatientid = re.emrinpatientid
                   and t.inpatientdate = re.emrinpatientdate
                   and t.status = 0)
        union
        select distinct le.modify_dat outdate,
               re.hisinpatientid_chr,
               re.hisinpatientdate,
               re.emrinpatientid,
               re.emrinpatientdate,
               le.registerid_chr,
               f.lastname_vchr,
               f.sex_chr,
               f.birth_dat,
               f.homeaddress_vchr,
               f.homephone_vchr,
               fr.registerid_chr recordid,
               imop.operationname,
               imcon.maindiagnosis
          from t_emr_followupsurveypatient f
         inner join t_opr_bih_leave le on le.registerid_chr =
                                          f.registerid_chr
         inner join t_bse_hisemr_relation re on le.registerid_chr =
                                                re.registerid_chr
          left outer join t_emr_followupsurveyrecord fr on fr.registerid_chr =
                                                           f.registerid_chr");
                stbSQL.Append(strSearchInHospitalMainDiagnosis);
                stbSQL.Append(@"
         where le.");
                stbSQL.Append(strDept);
                stbSQL.Append(@" = ?
           and le.status_int = 1
           and le.modify_dat between ? and ?
           and f.status_int = 1
           and not exists (select t.inpatientid
                  from ");
                stbSQL.Append(strDeathRecordTable);
                stbSQL.Append(@" t
                 where t.inpatientid = re.emrinpatientid
                   and t.inpatientdate = re.emrinpatientdate
                   and t.status = 0)) a
 order by a.outdate desc");
                string strSQL = stbSQL.ToString();
                #endregion

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                //赋值


                objDPArr[0].Value = p_strDept;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmDateStart;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmEndDate;
                objDPArr[3].Value = p_strDept;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_dtmDateStart;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_dtmEndDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
                p_dtbResult.Columns.Add("subdirectordt", typeof(string));
                int intRowCount = p_dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    strSQL = @"
select re.registerid_chr,
       ts.empid_vchr,
       ts.technicallevel_vchr,
       em.lastname_vchr subdirectordt
  from checkroomrecord cr
 inner join t_bse_hisemr_relation re on re.emrinpatientid = cr.inpatientid
                                    and re.emrinpatientdate =
                                        cr.inpatientdate
 inner join t_emr_signcollection ts on cr.sequence_int = ts.sign_int
 inner join t_bse_employee em on ts.empid_vchr = em.empid_chr
 inner join t_opr_bih_register reg on re.registerid_chr =
                                      reg.registerid_chr
 inner join t_opr_bih_leave le on le.registerid_chr = re.registerid_chr
 inner join (select cr.inpatientid,
                    cr.inpatientdate,
                    min(cr.createdate) createdate
               from checkroomrecord cr
              where cr.status = 0
              group by cr.inpatientid, cr.inpatientdate) cr2 on cr.inpatientid =
                                                                cr2.inpatientid
                                                            and cr.inpatientdate =
                                                                cr2.inpatientdate
                                                            and cr.createdate =
                                                                cr2.createdate
 where cr.status = 0
   and em.status_int = 1
   and le.status_int = 1
   and reg.status_int = 1
   and reg.areaid_chr = ?
   and le.outhospital_dat between ? and ?";
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_strDept;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_dtmDateStart;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_dtmEndDate;
                    DataTable dtbDoc = new DataTable();
                    long lngRes2 = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbDoc, objDPArr);
                    if (lngRes2 > 0)
                        m_mthGetDoc(dtbDoc, ref p_dtbResult);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// 查找第一条查房记录的医生级别最高的一个

        /// </summary>
        /// <param name="p_dtbResult"></param>
        private void m_mthGetDoc(DataTable p_dtbDoc, ref DataTable p_dtbResult)
        {
            try
            {
                if (p_dtbDoc.Rows.Count > 0)
                {
                    DataRow objRow = null;
                    string strRegisterId = string.Empty;
                    DataRow[] objRowArr = null;
                    for (int i = 0; i < p_dtbResult.Rows.Count; i++)
                    {
                        objRow = p_dtbResult.Rows[i];
                        strRegisterId = objRow["registerid_chr"].ToString();
                        objRowArr = p_dtbDoc.Select("registerid_chr = '" + strRegisterId + "'", "technicallevel_vchr desc");
                        if (objRowArr.Length > 0)
                        {
                            objRow.BeginEdit();
                            objRow["subdirectordt"] = objRowArr[0]["subdirectordt"].ToString();
                            objRow.EndEdit();
                        }
                    }
                    p_dtbDoc.Clear();
                    p_dtbDoc.Dispose();
                }
            }
            catch (Exception Ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(Ex, false);
            }
        }


        /// <summary>
        /// 获取出院病人信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strHospitalNO">医院代码</param>
        /// <param name="p_dtbResult">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutPatientInfoByInID(string p_strInPatientID, string p_strHospitalNO, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strHospitalNO))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                #region 根据不同情况生成不同SQL
                string strDeathRecordTable = string.Empty;
                string strSearchInHospitalMainDiagnosis = string.Empty;
                if (p_strHospitalNO == "450101001")
                {
                    strDeathRecordTable = "deadrecord";
                    strSearchInHospitalMainDiagnosis = @"
          left outer join t_emr_inhospitalmainrec_gxop imop on imop.registerid_chr =
                                                               le.registerid_chr
                                                           and imop.status = 0
                                                           and imop.seqid = '0'
          left outer join t_emr_inhospitalmainrec_gxcon imcon on imcon.registerid_chr =
                                                                                            imop.registerid_chr";
                }
                else
                {
                    strDeathRecordTable = "deathrecord";
                    strSearchInHospitalMainDiagnosis = @"
          left outer join inhospitalmainrecord_operation imop on imop.inpatientid =
                                                                 re.emrinpatientid
                                                             and imop.inpatientdate =
                                                                 re.emrinpatientdate
                                                             and imop.status = 1
                                                             and imop.seqid = '0'
          left outer join inhospitalmainrecord_content imcon on imcon.inpatientid =
                                                                                            re.emrinpatientid
                                                                                        and imcon.inpatientdate =
                                                                                            re.emrinpatientdate";
                }
                #endregion

                string strSQL = @"select distinct le.modify_dat outdate,
       re.hisinpatientid_chr,
       re.hisinpatientdate,
       re.emrinpatientid,
       re.emrinpatientdate,
       le.registerid_chr,
       pa.lastname_vchr,
       pa.sex_chr,
       pa.birth_dat,
       pa.homeaddress_vchr,
       pa.homephone_vchr,
       fr.registerid_chr recordid,
       imop.operationname,
       imcon.maindiagnosis,
       ttt.subdirectordt
  from t_opr_bih_leave le
 inner join t_opr_bih_registerdetail pa on pa.registerid_chr =
                                           le.registerid_chr
 inner join t_bse_hisemr_relation re on le.registerid_chr =
                                        re.registerid_chr
  left outer join t_emr_followupsurveyrecord fr on fr.registerid_chr =
                                                   pa.registerid_chr" + strSearchInHospitalMainDiagnosis + @"
left join (select distinct re.registerid_chr,
                             em.lastname_vchr subdirectordt
               from checkroomrecord t
              inner join t_bse_hisemr_relation re on re.emrinpatientid =
                                                     t.inpatientid
                                                 and re.emrinpatientdate =
                                                     t.inpatientdate
              inner join t_emr_signcollection ts on t.sequence_int =
                                                    ts.sign_int
              inner join t_bse_employee em on ts.empid_vchr = em.empid_chr
              where re.hisinpatientid_chr = ?
                and t.status = 0
                and em.status_int = 1
                and t.createdate =
                    (select min(t2.createdate)
                       from checkroomrecord t2
                      where t.inpatientid = t2.inpatientid
                        and t.inpatientdate = t.inpatientdate)
                and ts.technicallevel_vchr =
                    (select max(ts2.technicallevel_vchr)
                       from t_emr_signcollection ts2
                      where t.sequence_int = ts2.sign_int)) ttt on re.registerid_chr =
                                                                   ttt.registerid_chr
 where re.hisinpatientid_chr = ?  and le.status_int=1
   and not exists (select t.inpatientid
          from " + strDeathRecordTable + @" t
         where t.inpatientid = re.emrinpatientid
           and t.inpatientdate = re.emrinpatientdate
           and t.status = 0) order by le.modify_dat desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = p_strInPatientID;

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
        /// 获取出院病人信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <param name="p_strHospitalNO">医院代码</param>
        /// <param name="p_dtbResult">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutPatientInfoByName(string p_strPatientName, string p_strHospitalNO, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strPatientName) || string.IsNullOrEmpty(p_strHospitalNO))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                #region 根据不同情况生成不同SQL
                string strDeathRecordTable = string.Empty;
                string strSearchInHospitalMainDiagnosis = string.Empty;
                if (p_strHospitalNO == "450101001")
                {
                    strDeathRecordTable = "deadrecord";
                    strSearchInHospitalMainDiagnosis = @"
          left outer join t_emr_inhospitalmainrec_gxop imop on imop.registerid_chr =
                                                               le.registerid_chr
                                                           and imop.status = 0
                                                           and imop.seqid = '0'
          left outer join t_emr_inhospitalmainrec_gxcon imcon on imcon.registerid_chr =
                                                                                            imop.registerid_chr";
                }
                else
                {
                    strDeathRecordTable = "deathrecord";
                    strSearchInHospitalMainDiagnosis = @"
          left outer join inhospitalmainrecord_operation imop on imop.inpatientid =
                                                                 re.emrinpatientid
                                                             and imop.inpatientdate =
                                                                 re.emrinpatientdate
                                                             and imop.status = 1
                                                             and imop.seqid = '0'
          left outer join inhospitalmainrecord_content imcon on imcon.inpatientid =
                                                                                            re.emrinpatientid
                                                                                        and imcon.inpatientdate =
                                                                                            re.emrinpatientdate";
                }
                #endregion

                string strSQL = @"select distinct le.modify_dat outdate,
       re.hisinpatientid_chr,
       re.hisinpatientdate,
       re.emrinpatientid,
       re.emrinpatientdate,
       le.registerid_chr,
       pa.lastname_vchr,
       pa.sex_chr,
       pa.birth_dat,
       pa.homeaddress_vchr,
       pa.homephone_vchr,
       fr.registerid_chr recordid,
       imop.operationname,
       imcon.maindiagnosis,
       ttt.subdirectordt
  from t_opr_bih_leave le
 inner join t_opr_bih_registerdetail pa on pa.registerid_chr =
                                           le.registerid_chr
 inner join t_bse_hisemr_relation re on le.registerid_chr =
                                        re.registerid_chr
  left outer join t_emr_followupsurveyrecord fr on fr.registerid_chr =
                                                   pa.registerid_chr" + strSearchInHospitalMainDiagnosis + @"
left join (select distinct re.registerid_chr,
                             em.lastname_vchr subdirectordt
               from checkroomrecord t
              inner join t_bse_hisemr_relation re on re.emrinpatientid =
                                                     t.inpatientid
                                                 and re.emrinpatientdate =
                                                     t.inpatientdate
              inner join t_opr_bih_registerdetail pa2 on pa2.registerid_chr =
                                                         re.registerid_chr
              inner join t_emr_signcollection ts on t.sequence_int =
                                                    ts.sign_int
              inner join t_bse_employee em on ts.empid_vchr = em.empid_chr
              where pa2.lastname_vchr = ?
                and t.status = 0
                and em.status_int = 1
                and t.createdate =
                    (select min(t2.createdate)
                       from checkroomrecord t2
                      where t.inpatientid = t2.inpatientid
                        and t.inpatientdate = t.inpatientdate)
                and ts.technicallevel_vchr =
                    (select max(ts2.technicallevel_vchr)
                       from t_emr_signcollection ts2
                      where t.sequence_int = ts2.sign_int)) ttt on re.registerid_chr =
                                                                   ttt.registerid_chr
 where pa.lastname_vchr = ?  and le.status_int=1
   and not exists (select t.inpatientid
          from " + strDeathRecordTable + @" t
         where t.inpatientid = re.emrinpatientid
           and t.inpatientdate = re.emrinpatientdate
           and t.status = 0) order by le.modify_dat desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strPatientName;
                objDPArr[1].Value = p_strPatientName;

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

        #region 获取随访病人记录表病人信息


        /// <summary>
        /// 获取随访病人记录表病人信息.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strHospitalNO">医院代码</param>
        /// <param name="p_dtbResult">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFollowUpSurveyPatientInfoByInID(string p_strInPatientID, string p_strHospitalNO, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strHospitalNO))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                #region 根据不同情况生成不同SQL
                string strDeathRecordTable = string.Empty;
                string strSearchInHospitalMainDiagnosis = string.Empty;
                if (p_strHospitalNO == "450101001")
                {
                    strDeathRecordTable = "deadrecord";
                    strSearchInHospitalMainDiagnosis = @"
          left outer join t_emr_inhospitalmainrec_gxop imop on imop.registerid_chr =
                                                               le.registerid_chr
                                                           and imop.status = 0
                                                           and imop.seqid = '0'
          left outer join t_emr_inhospitalmainrec_gxcon imcon on imcon.registerid_chr =
                                                                                            imop.registerid_chr";
                }
                else
                {
                    strDeathRecordTable = "deathrecord";
                    strSearchInHospitalMainDiagnosis = @"
          left outer join inhospitalmainrecord_operation imop on imop.inpatientid =
                                                                 re.emrinpatientid
                                                             and imop.inpatientdate =
                                                                 re.emrinpatientdate
                                                             and imop.status = 1
                                                             and imop.seqid = '0'
          left outer join inhospitalmainrecord_content imcon on imcon.inpatientid =
                                                                                            re.emrinpatientid
                                                                                        and imcon.inpatientdate =
                                                                                            re.emrinpatientdate";
                }
                #endregion

                string strSQL = @"select distinct le.modify_dat outdate,
       re.hisinpatientid_chr,
       re.hisinpatientdate,
       re.emrinpatientid,
       re.emrinpatientdate,
       le.registerid_chr,
       pa.lastname_vchr,
       pa.sex_chr,
       pa.birth_dat,
       pa.homeaddress_vchr,
       pa.homephone_vchr,
       fr.registerid_chr recordid,
       imop.operationname,
       imcon.maindiagnosis
  from t_opr_bih_leave le
 inner join t_emr_followupsurveypatient pa on pa.registerid_chr =
                                           le.registerid_chr
 inner join t_bse_hisemr_relation re on le.registerid_chr =
                                        re.registerid_chr
  left outer join t_emr_followupsurveyrecord fr on fr.registerid_chr =
                                                   pa.registerid_chr" + strSearchInHospitalMainDiagnosis + @"
 where re.hisinpatientid_chr = ?  and le.status_int=1
   and not exists (select t.inpatientid
          from " + strDeathRecordTable + @" t
         where t.inpatientid = re.emrinpatientid
           and t.inpatientdate = re.emrinpatientdate
           and t.status = 0) order by le.modify_dat desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;

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
        /// 获取随访病人记录表病人信息.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <param name="p_strHospitalNO">医院代码</param>
        /// <param name="p_dtbResult">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFollowUpSurveyPatientInfoByName(string p_strPatientName, string p_strHospitalNO, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strPatientName) || string.IsNullOrEmpty(p_strHospitalNO))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                #region 根据不同情况生成不同SQL
                string strDeathRecordTable = string.Empty;
                string strSearchInHospitalMainDiagnosis = string.Empty;
                if (p_strHospitalNO == "450101001")
                {
                    strDeathRecordTable = "deadrecord";
                    strSearchInHospitalMainDiagnosis = @"
          left outer join t_emr_inhospitalmainrec_gxop imop on imop.registerid_chr =
                                                               le.registerid_chr
                                                           and imop.status = 0
                                                           and imop.seqid = '0'
          left outer join t_emr_inhospitalmainrec_gxcon imcon on imcon.registerid_chr =
                                                                                            imop.registerid_chr";
                }
                else
                {
                    strDeathRecordTable = "deathrecord";
                    strSearchInHospitalMainDiagnosis = @"
          left outer join inhospitalmainrecord_operation imop on imop.inpatientid =
                                                                 re.emrinpatientid
                                                             and imop.inpatientdate =
                                                                 re.emrinpatientdate
                                                             and imop.status = 1
                                                             and imop.seqid = '0'
          left outer join inhospitalmainrecord_content imcon on imcon.inpatientid =
                                                                                            re.emrinpatientid
                                                                                        and imcon.inpatientdate =
                                                                                            re.emrinpatientdate";
                }
                #endregion

                string strSQL = @"select distinct le.modify_dat outdate,
       re.hisinpatientid_chr,
       re.hisinpatientdate,
       re.emrinpatientid,
       re.emrinpatientdate,
       le.registerid_chr,
       pa.lastname_vchr,
       pa.sex_chr,
       pa.birth_dat,
       pa.homeaddress_vchr,
       pa.homephone_vchr,
       fr.registerid_chr recordid,
       imop.operationname,
       imcon.maindiagnosis
  from t_opr_bih_leave le
 inner join t_emr_followupsurveypatient pa on pa.registerid_chr =
                                           le.registerid_chr
 inner join t_bse_hisemr_relation re on le.registerid_chr =
                                        re.registerid_chr
  left outer join t_emr_followupsurveyrecord fr on fr.registerid_chr =
                                                   pa.registerid_chr" + strSearchInHospitalMainDiagnosis + @"
 where pa.lastname_vchr = ?  and le.status_int=1 
   and not exists (select t.inpatientid
          from " + strDeathRecordTable + @" t
         where t.inpatientid = re.emrinpatientid
           and t.inpatientdate = re.emrinpatientdate
           and t.status = 0) order by le.modify_dat desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strPatientName;

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
