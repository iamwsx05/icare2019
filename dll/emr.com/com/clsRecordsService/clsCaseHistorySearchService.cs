using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.clsRecordsService
{
    /// <summary>
    /// 病案首页查询统计
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsCaseHistorySearchService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 查询一段时间内尚未编目的出院病人
        /// <summary>
        /// 查询一段时间内尚未编目的出院病人
        /// </summary>
        /// <param name="p_strOutDateBegin">查询的出院日期开始时间</param>
        /// <param name="p_strOutDateEnd">查询的出院日期结束时间</param>
        /// <param name="p_strDeptID">科室ID(如为空则查询所有科室)</param>
        /// <param name="p_dtpResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUnlistOutPatient(string p_strOutDateBegin,
            string p_strOutDateEnd,
            string p_strDeptID,
            out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0;
            if (p_strOutDateBegin == null || p_strOutDateBegin == string.Empty ||
                p_strOutDateEnd == null || p_strOutDateEnd == string.Empty)
                return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select le.outdeptid_chr as OutDeptID,
                                       dept.deptname_vchr as OutDeptName,
                                       le. registerid_chr as RegisterID,
                                       le. MODIFY_DAT as OutHospitalDate,
                                       case T.Submit_Int
                                         when 1 then
                                          T.LASTMODIFYDATE
                                         when 0 then
                                          le.modify_dat + k.tk
                                       end as SubmitDate,
                                       pa.lastname_vchr as PatientName,
                                       pa.patientid_chr as PatientID,
                                       pa.inpatientid_chr as InPatientID,
                                       T.Emr_Seq
                                  from T_OPR_BIH_LEAVE le
                                 inner join (select tt.SETSTATUS_INT / 24 tk
                                               from t_sys_setting tt
                                              where tt.setid_chr = '3006') k on 1 = 1
                                 inner join (select T1.Emr_Seq,
                                                    T1.registerid_chr,
                                                    T2.LASTMODIFYDATE,
                                                    T1.Submit_Int
                                               from T_EMR_INHOSPITALMAINRECORD_GX T1,
                                                    T_EMR_INHOSPITALMAINREC_GXCON T2
                                              where T1.Emr_Seq = T2.Emr_Seq
                                                and T1.Status = 0
                                                and T2.Status = 0
                                                and (T1.SUBMIT_INT = 1 or
                                                    (T1.SUBMIT_INT = 0 and
                                                    T1.registerid_chr in
                                                    (select b.REGISTERID_CHR
                                                         from T_OPR_BIH_LEAVE b
                                                        where sysdate - b.modify_dat >
                                                              (select ts.SETSTATUS_INT / 24
                                                                 from t_sys_setting ts
                                                                where ts.setid_chr = '3006'))))
                                                and T2.Catalog_Date is null) T on T.registerid_chr =
                                                                                  le.registerid_chr
                                 inner join T_OPR_BIH_REGISTER re on re.registerid_chr = le.registerid_chr and re.STATUS_INT = 1
                                 inner join t_bse_patient pa on re.patientid_chr = pa.patientid_chr
                                 inner join t_bse_deptdesc dept on dept.deptid_chr = le.outdeptid_chr
                                 where le.modify_dat between ? and ?";

                if (p_strDeptID != null && p_strDeptID != string.Empty)
                {
                    strSQL += " and le.outdeptid_chr = ?";

                    System.Data.IDataParameter[] objLisAddItemRefArr = null;
                    objHRPServ.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].DbType = DbType.DateTime;
                    objLisAddItemRefArr[0].Value = Convert.ToDateTime(p_strOutDateBegin);
                    objLisAddItemRefArr[1].DbType = DbType.DateTime;
                    objLisAddItemRefArr[1].Value = Convert.ToDateTime(p_strOutDateEnd);
                    objLisAddItemRefArr[2].Value = p_strDeptID.Trim();

                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);
                }
                else
                {
                    strSQL += @" and le.outdeptid_chr in 
                                    (select c.DEPTID_CHR
	                                from T_BSE_DEPTDESC c
	                                where c.STATUS_INT = 1
	                                and c.INPATIENTOROUTPATIENT_INT = 1
	                                and c.CATEGORY_INT = 0
	                                and c.ATTRIBUTEID = '0000002')";

                    System.Data.IDataParameter[] objLisAddItemRefArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].DbType = DbType.DateTime;
                    objLisAddItemRefArr[0].Value = Convert.ToDateTime(p_strOutDateBegin);
                    objLisAddItemRefArr[1].DbType = DbType.DateTime;
                    objLisAddItemRefArr[1].Value = Convert.ToDateTime(p_strOutDateEnd);

                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);
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

        #region 根据病人住院号获取入出院情况
        /// <summary>
        /// 根据病人住院号获取入出院情况
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInAndOutInfo(string p_strInPatientID, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0;
            if (p_strInPatientID == null || p_strInPatientID == string.Empty)
                return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select re.registerid_chr,
                                   tr.sourcedeptid_chr as InDeptID,
                                   tr.deptname_vchr as InDeptName,
                                   tr.modify_dat as InDate,
                                   le.outdeptid_chr as OutDeptID,
                                   le.deptname_vchr as OutDeptName,
                                   le.modify_dat as OutDate
                              from t_opr_bih_register re
                             inner join (select c.REGISTERID_CHR,
                                                c.SOURCEDEPTID_CHR,
                                                c.modify_dat,
                                                d.deptname_vchr
                                           from T_OPR_BIH_TRANSFER c, t_bse_deptdesc d
                                          where c.sourcedeptid_chr = d.deptid_chr and c.TYPE_INT <> 1) tr on re.registerid_chr =
                                                                                         tr.registerid_chr
                              left outer join (select a.outdeptid_chr,
                                                      a.modify_dat,
                                                      b.deptname_vchr,
                                                      a.registerid_chr
                                                 from t_opr_bih_leave a, t_bse_deptdesc b
                                                where a.outdeptid_chr = b.deptid_chr
                                                  and a.status_int = 1) le on re.registerid_chr =
                                                                              le.registerid_chr

                             where re.inpatientid_chr = ? and re.STATUS_INT = 1
                               and tr.modify_dat =
                                   (select min(modify_dat)
                                      from T_OPR_BIH_TRANSFER
                                     where registerid_chr = tr.registerid_chr and TYPE_INT <> 1)";

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_strInPatientID.Trim();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 获取病案首页诊断及手术信息
        /// <summary>
        /// 获取病案首页诊断及手术信息
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetDiagnoseAndOpInfo(string p_strRegisterID, out clsInHospitalMainRecord_GX_Collection p_objCollection)
        {
            p_objCollection = null;
            long lngRes = 0;
            if (p_strRegisterID == null || p_strRegisterID == string.Empty)
                return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 主表内的诊断
                string strSQL = @"select a.DIAGNOSIS,
                                       a.MAINDIAGNOSIS,
                                       a.MAINCONDITIONSEQ,
                                       a.COMPLICATION,
                                       a.COMPLICATIONSEQ,
                                       a.INFECTIONDIAGNOSIS,
                                       a.INFECTIONCONDICTIONSEQ,
                                       a.PATHOLOGYDIAGNOSIS,
                                       a.PATHOLOGYDIAGNOSISSEQ,
                                       a.SCACHESOURCE,
                                       a.NEONATEDISEASE1,
                                       a.Neonatedisease2,
                                       a.Neonatedisease3,
                                       a.NEONATEDISEASE4,
                                       a.emr_seq,
                                       a.CONFIRMDIAGNOSISDATE
                                  from T_EMR_INHOSPITALMAINREC_GXCON a
                                 where a.registerid_chr = ?
                                   and a.status = 0";
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_strRegisterID.Trim();

                DataTable dtResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objLisAddItemRefArr);
                if (lngRes <= 0 || dtResult == null || dtResult.Rows.Count <= 0)
                    return 0;

                long lngEmr_Seq = Convert.ToInt64(dtResult.Rows[0]["emr_seq"]);
                p_objCollection = new clsInHospitalMainRecord_GX_Collection();
                p_objCollection.m_objContent = new clsInHospitalMainRecord_GXContent();
                p_objCollection.m_objContent.m_strDIAGNOSIS = dtResult.Rows[0]["DIAGNOSIS"].ToString();
                p_objCollection.m_objContent.m_strMAINDIAGNOSIS = dtResult.Rows[0]["MAINDIAGNOSIS"].ToString();
                p_objCollection.m_objContent.m_intMAINCONDITIONSEQ = Convert.ToInt32(dtResult.Rows[0]["MAINCONDITIONSEQ"]);
                p_objCollection.m_objContent.m_strCOMPLICATION = dtResult.Rows[0]["COMPLICATION"].ToString();
                p_objCollection.m_objContent.m_intCOMPLICATIONSEQ = Convert.ToInt32(dtResult.Rows[0]["COMPLICATIONSEQ"]);
                p_objCollection.m_objContent.m_strINFECTIONDIAGNOSIS = dtResult.Rows[0]["INFECTIONDIAGNOSIS"].ToString();
                p_objCollection.m_objContent.m_intINFECTIONCONDICTIONSEQ = Convert.ToInt32(dtResult.Rows[0]["INFECTIONCONDICTIONSEQ"]);
                p_objCollection.m_objContent.m_strPATHOLOGYDIAGNOSIS = dtResult.Rows[0]["PATHOLOGYDIAGNOSIS"].ToString();
                p_objCollection.m_objContent.m_intPATHOLOGYDIAGNOSISSEQ = Convert.ToInt32(dtResult.Rows[0]["PATHOLOGYDIAGNOSISSEQ"]);
                p_objCollection.m_objContent.m_strSCACHESOURCE = dtResult.Rows[0]["SCACHESOURCE"].ToString();
                p_objCollection.m_objContent.m_strNEONATEDISEASE1 = dtResult.Rows[0]["NEONATEDISEASE1"].ToString();
                p_objCollection.m_objContent.m_strNEONATEDISEASE2 = dtResult.Rows[0]["NEONATEDISEASE2"].ToString();
                p_objCollection.m_objContent.m_strNEONATEDISEASE3 = dtResult.Rows[0]["NEONATEDISEASE3"].ToString();
                p_objCollection.m_objContent.m_strNEONATEDISEASE4 = dtResult.Rows[0]["NEONATEDISEASE4"].ToString();
                p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE = Convert.ToDateTime(dtResult.Rows[0]["CONFIRMDIAGNOSISDATE"]);
                #endregion

                #region 其他诊断
                strSQL = @"select a.DIAGNOSISDESC, a.CONDITIONSEQ, a.SEQID
                                  from T_EMR_INHOSPITALMAINREC_GXOD a
                                 where a.emr_seq = ?
                                   and a.status = 0 order by a.SEQID";
                System.Data.IDataParameter[] objLisAddItemRefArr1 = null;
                objHRPServ.CreateDatabaseParameter(1, out objLisAddItemRefArr1);
                objLisAddItemRefArr1[0].Value = lngEmr_Seq;

                dtResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objLisAddItemRefArr1);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_objCollection.m_objOtherDiagnosisArr = new clsInHospitalMainRecord_GXOtherDiagnose[dtResult.Rows.Count];
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        p_objCollection.m_objOtherDiagnosisArr[i] = new clsInHospitalMainRecord_GXOtherDiagnose();
                        p_objCollection.m_objOtherDiagnosisArr[i].m_strDIAGNOSISDESC = dtResult.Rows[i]["DIAGNOSISDESC"].ToString();
                        p_objCollection.m_objOtherDiagnosisArr[i].m_intCONDITIONSEQ = Convert.ToInt32(dtResult.Rows[i]["CONDITIONSEQ"]);
                        p_objCollection.m_objOtherDiagnosisArr[i].m_strSEQID = dtResult.Rows[i]["SEQID"].ToString();
                    }
                }
                #endregion

                #region 入院诊断
                strSQL = @"select a.DIAGNOSISDESC,  a.SEQID
                              from T_EMR_INHOSPITALMAINREC_GXID a
                             where a.emr_seq = ?
                               and a.status = 0 order by a.SEQID";
                System.Data.IDataParameter[] objLisAddItemRefArr2 = null;
                objHRPServ.CreateDatabaseParameter(1, out objLisAddItemRefArr2);
                objLisAddItemRefArr2[0].Value = lngEmr_Seq;

                dtResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objLisAddItemRefArr2);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_objCollection.m_objInDiagnosisArr = new clsInHospitalMainRecord_GXInDiagnosis[dtResult.Rows.Count];
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        p_objCollection.m_objInDiagnosisArr[i] = new clsInHospitalMainRecord_GXInDiagnosis();
                        p_objCollection.m_objInDiagnosisArr[i].m_strDIAGNOSISDESC = dtResult.Rows[i]["DIAGNOSISDESC"].ToString();
                        p_objCollection.m_objInDiagnosisArr[i].m_strSEQID = dtResult.Rows[i]["SEQID"].ToString();
                    }
                }
                #endregion

                #region 手术信息
                strSQL = @"select a.OPERATIONNAME,
                               a.OPERATIONAANAESTHESIAMODENAME,
                               a.OPERATOR,
                               F_GETEMPNAMEBYNO(a.OPERATOR) as OperatorName,
                               a.OPERATIONDATE,
                               a.SEQID
                          from T_EMR_INHOSPITALMAINREC_GXOP a
                         where a.emr_seq = ?
                           and a.status = 0 order by a.SEQID";
                System.Data.IDataParameter[] objLisAddItemRefArr3 = null;
                objHRPServ.CreateDatabaseParameter(1, out objLisAddItemRefArr3);
                objLisAddItemRefArr3[0].Value = lngEmr_Seq;

                dtResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objLisAddItemRefArr3);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_objCollection.m_objOperationArr = new clsInHospitalMainRecord_GXOperation[dtResult.Rows.Count];
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        p_objCollection.m_objOperationArr[i] = new clsInHospitalMainRecord_GXOperation();
                        p_objCollection.m_objOperationArr[i].m_strOPERATIONNAME = dtResult.Rows[i]["OPERATIONNAME"].ToString();
                        p_objCollection.m_objOperationArr[i].m_strSEQID = dtResult.Rows[i]["SEQID"].ToString();
                        p_objCollection.m_objOperationArr[i].m_strOPERATIONAANAESTHESIAMODENAME = dtResult.Rows[i]["OPERATIONAANAESTHESIAMODENAME"].ToString();
                        p_objCollection.m_objOperationArr[i].m_strOPERATORNAME = dtResult.Rows[i]["OperatorName"].ToString();
                        p_objCollection.m_objOperationArr[i].m_strOPERATOR = dtResult.Rows[i]["OPERATOR"].ToString();
                        p_objCollection.m_objOperationArr[i].m_dtmOPERATIONDATE = Convert.ToDateTime(dtResult.Rows[i]["OPERATIONDATE"]);
                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 查询出院病人情况
        /// <summary>
        /// 查询出院病人情况
        /// </summary>
        /// <param name="p_intDiagnoseResult">治疗结果(出院方式)</param>
        /// <param name="p_strDeptID">科室ID(为空时查询所有科室)</param>
        /// <param name="p_dtmOutDateBegin">查询出院时间开始</param>
        /// <param name="p_dtmOutDateEnd">查询出院时间结束</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutInfo(int p_intDiagnoseResult,
            string p_strDeptID,
            DateTime p_dtmOutDateBegin,
            DateTime p_dtmOutDateEnd,
            out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSeq = "";
                if (p_intDiagnoseResult < 0)
                {
                    strSeq = " is not null";
                }
                else
                {
                    strSeq = " = " + p_intDiagnoseResult.ToString();
                }
                string strSQL = @"select le.outdeptid_chr as OutDeptID,
                                       dept.deptname_vchr as OutDeptName,
                                       le. registerid_chr as RegisterID,
                                       le. MODIFY_DAT as OutHospitalDate,
                                       T.LASTMODIFYDATE as SubmitDate,
                                       pa.lastname_vchr as PatientName,
                                       pa.sex_chr as PatientSex,
                                       pa.birth_dat as PatientBirthDate,
                                       T.INPATIENTID,
                                       T.Maindiagnosis as MainDiagnosis,
                                       T.Mainconditionseq as MainConditionseq,
                                       T.Emr_Seq
                                  from T_OPR_BIH_LEAVE le
                                 inner join (select T1.Emr_Seq,
                                                    T1.registerid_chr,
                                                    T2.LASTMODIFYDATE,
                                                    T2.Maindiagnosis,
                                                    T2.Mainconditionseq,
                                                    T1.INPATIENTID
                                               from T_EMR_INHOSPITALMAINRECORD_GX T1,
                                                    T_EMR_INHOSPITALMAINREC_GXCON T2
                                              where T1.Emr_Seq = T2.Emr_Seq
                                                and T1.Status = 0
                                                and T2.Status = 0
                                                and T2.MAINCONDITIONSEQ " + strSeq+ @") T on T.registerid_chr =
                                                                                  le.registerid_chr
                                 inner join T_OPR_BIH_REGISTER re on re.registerid_chr = le.registerid_chr and re.STATUS_INT = 1
                                 inner join t_bse_patient pa on re.patientid_chr = pa.patientid_chr
                                 inner join t_bse_deptdesc dept on dept.deptid_chr = le.outdeptid_chr
                                 where le.modify_dat between ? and ?";

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                if (p_strDeptID != null && p_strDeptID.Trim() != string.Empty)
                {
                    strSQL += " and le.outdeptid_chr = ?";
                    objHRPServ.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].DbType = DbType.DateTime;
                    objLisAddItemRefArr[0].Value = p_dtmOutDateBegin;
                    objLisAddItemRefArr[1].DbType = DbType.DateTime;
                    objLisAddItemRefArr[1].Value = p_dtmOutDateEnd;
                    objLisAddItemRefArr[2].Value = p_strDeptID.Trim();

                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);
                }
                else
                {
                    strSQL += @" and le.outdeptid_chr in 
                                    (select c.DEPTID_CHR
	                                from T_BSE_DEPTDESC c
	                                where c.STATUS_INT = 1
	                                and c.INPATIENTOROUTPATIENT_INT = 1
	                                and c.CATEGORY_INT = 0
	                                and c.ATTRIBUTEID = '0000002')";
                    objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].DbType = DbType.DateTime;
                    objLisAddItemRefArr[0].Value = p_dtmOutDateBegin;
                    objLisAddItemRefArr[1].DbType = DbType.DateTime;
                    objLisAddItemRefArr[1].Value = p_dtmOutDateEnd;

                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);
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

        #region 疾病谱统计
        /// <summary>
        /// 疾病谱统计
        /// </summary>
        /// <param name="p_blnIsFirst">是否第一诊断</param>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_dtmOutDateBegin">查询出院时间开始</param>
        /// <param name="p_dtmOutDateEnd">查询出院时间结束</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStatDiagnose(bool p_blnIsFirst, 
            string p_strDeptID,
            DateTime p_dtmOutDateBegin,
            DateTime p_dtmOutDateEnd, 
            out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            string strSQL = "";

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strDept = "";
                if (p_strDeptID != null && p_strDeptID.Trim() != string.Empty)
                {
                    strDept = @" and le.outdeptid_chr = '"+p_strDeptID.Trim()+"'";
                }

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                if (p_blnIsFirst)
                {
                    strSQL = @"select t.maindiagnosis DiagName, t.icd_10ofmain ICD10, count(*) Num
                                  from t_emr_inhospitalmainrec_gxcon t
                                  inner join T_OPR_BIH_LEAVE le on t.registerid_chr = le.registerid_chr
                                  where t.Status = 0 and t.catalog_date is not null " + strDept + @"
                                  and le.modify_dat between ? and ?
                                 group by t.icd_10ofmain, t.maindiagnosis";
                    objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].DbType = DbType.DateTime;
                    objLisAddItemRefArr[0].Value = p_dtmOutDateBegin;
                    objLisAddItemRefArr[1].DbType = DbType.DateTime;
                    objLisAddItemRefArr[1].Value = p_dtmOutDateEnd;
                }
                else
                {
                    strSQL = @"select DiagName, ICD10, count(*) Num
                                  from ((select t.maindiagnosis DiagName, t.icd_10ofmain ICD10
                                           from t_emr_inhospitalmainrec_gxcon t
                                        inner join T_OPR_BIH_LEAVE le on t.registerid_chr = le.registerid_chr 
                                          where t.Status = 0 and t.catalog_date is not null " + strDept + @"
                                             and le.modify_dat between ? and ?)union
                                        (select od.diagnosisdesc DiagName, od.icd10 ICD10
                                           from t_emr_inhospitalmainrec_gxcon t
                                           inner join T_EMR_INHOSPITALMAINREC_GXOD  od on t.emr_seq = od.emr_seq
                                           inner join T_OPR_BIH_LEAVE le on t.registerid_chr = le.registerid_chr 
                                           " + strDept+ @"
                                          where t.catalog_date is not null
                                             and le.modify_dat between ? and ?))
                                 group by DiagName, ICD10";

                    objHRPServ.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].DbType = DbType.DateTime;
                    objLisAddItemRefArr[0].Value = p_dtmOutDateBegin;
                    objLisAddItemRefArr[1].DbType = DbType.DateTime;
                    objLisAddItemRefArr[1].Value = p_dtmOutDateEnd;
                    objLisAddItemRefArr[2].DbType = DbType.DateTime;
                    objLisAddItemRefArr[2].Value = p_dtmOutDateBegin;
                    objLisAddItemRefArr[3].DbType = DbType.DateTime;
                    objLisAddItemRefArr[3].Value = p_dtmOutDateEnd;
                }

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 手术谱统计
        /// <summary>
        /// 手术谱统计
        /// </summary>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_dtmOutDateBegin">查询出院时间开始</param>
        /// <param name="p_dtmOutDateEnd">查询出院时间结束</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStatOperation(string p_strDeptID,
            DateTime p_dtmOutDateBegin,
            DateTime p_dtmOutDateEnd,
            out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            string strSQL = "";

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strDept = "";
                if (p_strDeptID != null && p_strDeptID.Trim() != string.Empty)
                {
                    strDept = @" and le.outdeptid_chr = '" + p_strDeptID.Trim() + "'";
                }

                strSQL = @"select op.operationname OpName,
                               op.OPERATIONID OpID,
                               count(*) Num
                          from t_emr_inhospitalmainrec_gxop  op,
                               t_emr_inhospitalmainrec_gxcon co,
                               t_opr_bih_leave               le
                         where op.emr_seq = co.emr_seq
                           and op.Status = 0 and co.Status = 0
                           and co.catalog_date is not null
                           and co.registerid_chr = le.registerid_chr
                           and le.modify_dat between ? and ? " + strDept + @"
                         group by op.operationid, op.operationname";

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = p_dtmOutDateBegin;
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = p_dtmOutDateEnd;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 查询出院且未编目病人
        /// <summary>
        /// 查询出院且未编目病人
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_dtmOutDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutAndNoCatalogPatient(string p_strDeptID, DateTime p_dtmOutDate, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            string strSQL = "";

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strDept = "";
                if (p_strDeptID != null && p_strDeptID.Trim() != string.Empty)
                {
                    strDept = @" and l.outdeptid_chr = '" + p_strDeptID.Trim() + "'";
                }

                strSQL = @"select a.inpatientid,
                               p.lastname_vchr PatientName," +clsDatabaseSQLConvert.s_strGetAgeSQL("p.BIRTH_DAT") + @" PatientAge,
                               p.sex_chr PatientSex,
                               a.inpatientdate,
                               d.deptname_vchr DeptName,
                               l.modify_dat OutDate,
                               pay.paytypename_vchr PayType
                          from T_EMR_INHOSPITALMAINRECORD_GX a
                         inner join t_emr_inhospitalmainrec_gxcon b on a.emr_seq = b.emr_seq
                         inner join t_opr_bih_leave l on a.registerid_chr = l.registerid_chr
                         inner join t_opr_bih_register r on r.registerid_chr = a.registerid_chr and r.STATUS_INT = 1
                         inner join t_bse_patient p on r.patientid_chr = p.patientid_chr
                         inner join t_bse_deptdesc d on d.deptid_chr = l.outdeptid_chr
                          left outer join t_bse_patientpaytype pay on pay.paytypeid_chr =
                                                                      p.paytypeid_chr
                         where (a.submit_int = 1 or
                                (a.SUBMIT_INT = 0 and
                                sysdate - l.modify_dat >
                                          (select ts.SETSTATUS_INT / 24
                                             from t_sys_setting ts
                                            where ts.setid_chr = '3006')))
                           and a.Status = 0 and b.Status = 0
                           and b.catalog_date is null
                           and l.modify_dat = ?" + strDept;

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = p_dtmOutDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 编目工作量统计

        #region 编目病案数
        /// <summary>
        /// 编目病案数
        /// </summary>
        /// <param name="p_dtmBegin">查询开始时间</param>
        /// <param name="p_dtmEnd">查询结束时间</param>
        /// <param name="p_strNum">病案数</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCatalogCaseNum(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            long lngRes = 0;
            p_strNum = "0";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select count(*) Num
                                  from t_emr_inhospitalmainrec_gxcon
                                 where status = 0
                                   and catalog_date >= ? and catalog_date < ?";

                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = Convert.ToDateTime(p_dtmBegin.ToString("yyyy-MM-dd"));
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = Convert.ToDateTime(p_dtmEnd.ToString("yyyy-MM-dd"));

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    p_strNum = dtbResult.Rows[0][0].ToString();
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

        #region 死亡病案数
        /// <summary>
        /// 死亡病案数
        /// </summary>
        /// <param name="p_dtmBegin">查询开始时间</param>
        /// <param name="p_dtmEnd">查询结束时间</param>
        /// <param name="p_strNum">病案数</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeadCaseNum(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            long lngRes = 0;
            p_strNum = "0";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select count(*) Num
                                  from t_emr_inhospitalmainrec_gxcon a, T_OPR_BIH_LEAVE b
                                 where a.status = 0
                                   and a.catalog_date >= ?
                                   and a.catalog_date < ?
                                   and a.registerid_chr = b.registerid_chr
                                   and b.type_int = 4";

                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = Convert.ToDateTime(p_dtmBegin.ToString("yyyy-MM-dd"));
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = Convert.ToDateTime(p_dtmEnd.ToString("yyyy-MM-dd"));

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    p_strNum = dtbResult.Rows[0][0].ToString();
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

        #region 编目手术总数
        /// <summary>
        /// 编目手术总数
        /// </summary>
        /// <param name="p_dtmBegin">查询开始时间</param>
        /// <param name="p_dtmEnd">查询结束时间</param>
        /// <param name="p_strNum">病案数</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCatalogOpNum(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            long lngRes = 0;
            p_strNum = "0";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select count(*) Num
                                  from t_emr_inhospitalmainrec_gxcon a, t_emr_inhospitalmainrec_gxop b
                                 where a.status = 0
                                   and b.status = 0
                                   and a.catalog_date >= ?
                                   and a.catalog_date < ?
                                   and a.emr_seq = b.emr_seq";

                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = Convert.ToDateTime(p_dtmBegin.ToString("yyyy-MM-dd"));
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = Convert.ToDateTime(p_dtmEnd.ToString("yyyy-MM-dd"));

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    p_strNum = dtbResult.Rows[0][0].ToString();
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

        #region 编目手术种类数
        /// <summary>
        /// 编目手术种类数
        /// </summary>
        /// <param name="p_dtmBegin">查询开始时间</param>
        /// <param name="p_dtmEnd">查询结束时间</param>
        /// <param name="p_strNum">病案数</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCatalogOpTypeNum(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            long lngRes = 0;
            p_strNum = "0";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select count(distinct b.operationid) Num
                                  from t_emr_inhospitalmainrec_gxcon a, t_emr_inhospitalmainrec_gxop b
                                 where a.status = 0
                                   and b.status = 0
                                   and a.catalog_date >= ?
                                   and a.catalog_date < ?
                                   and a.emr_seq = b.emr_seq
                                   and b.operationid is not null";

                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = Convert.ToDateTime(p_dtmBegin.ToString("yyyy-MM-dd"));
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = Convert.ToDateTime(p_dtmEnd.ToString("yyyy-MM-dd"));

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    p_strNum = dtbResult.Rows[0][0].ToString();
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

        #endregion
    }
}
