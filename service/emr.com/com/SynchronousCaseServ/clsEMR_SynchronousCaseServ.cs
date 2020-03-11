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
    /// 病案同步
    /// </summary>
    [Transaction(TransactionOption.NotSupported)]
    [ObjectPooling(true)]
    public class clsEMR_SynchronousCaseServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取同步的科室列表


        /// <summary>
        /// 获取同步的科室列表

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDeptArr">科室列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSynchronousDeptList(out clsEmrDept_VO[] p_objDeptArr)
        {
            p_objDeptArr = null;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select t.DEPTID_CHR code, t.deptname_vchr name
  from t_bse_deptdesc t
 where t.attributeid = '0000003'";

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (dtbResult == null)
                {
                    return -1;
                }


                int intRowsCount = dtbResult.Rows.Count;
                if (intRowsCount <= 0)
                {
                    return -1;
                }

                p_objDeptArr = new clsEmrDept_VO[intRowsCount];
                DataRow drCurrent = null;
                for (int iR = 0; iR < intRowsCount; iR++)
                {
                    drCurrent = dtbResult.Rows[iR];
                    p_objDeptArr[iR] = new clsEmrDept_VO();
                    p_objDeptArr[iR].m_strDEPTID_CHR = drCurrent["code"].ToString();
                    p_objDeptArr[iR].m_strDEPTNAME_VCHR = drCurrent["name"].ToString();
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

        #region 获取病案内容
        /// <summary>
        /// 获取病案内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtmStartDate">查询开始时间</param>
        /// <param name="p_dtmEndDate">查询结束时间</param>
        /// <param name="p_dtbResult">病案内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCaseData(DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                #region SQL语句
                string strSelecteDiagnosis = string.Empty;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSelecteDiagnosis = @"select top 1 dia2.icd10         ryzd,
                          dia2.inpatientid   diainid,
                          dia2.inpatientdate diaindate
                     from inhospitalmainrecord_diagnosis dia2
                    where dia2.status = 1
                      and dia2.diagnosistype = 1
                      and dia2.lastmodifydate =
                          (select max(dia1.lastmodifydate)
                             from inhospitalmainrecord_diagnosis dia1
                            where dia1.inpatientid = dia2.inpatientid
                              and dia1.inpatientdate = dia2.inpatientdate
                              and dia1.opendate = dia2.opendate)
                    order by dia2.seqid  ";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSelecteDiagnosis = @"select dia2.icd10         ryzd,
                          dia2.inpatientid   diainid,
                          dia2.inpatientdate diaindate
                     from inhospitalmainrecord_diagnosis dia2
                    where dia2.status = 1
                      and dia2.diagnosistype = 1
                      and dia2.lastmodifydate =
                          (select max(dia1.lastmodifydate)
                             from inhospitalmainrecord_diagnosis dia1
                            where dia1.inpatientid = dia2.inpatientid
                              and dia1.inpatientdate = dia2.inpatientdate
                              and dia1.opendate = dia2.opendate)
                    order by dia2.seqid  fetch first 1 row only ";
                }
                else
                {
                    strSelecteDiagnosis = @"select * from(select dia2.icd10         ryzd,
                          dia2.inpatientid   diainid,
                          dia2.inpatientdate diaindate
                     from inhospitalmainrecord_diagnosis dia2
                    where dia2.status = 1
                      and dia2.diagnosistype = 1
                      and dia2.lastmodifydate =
                          (select max(dia1.lastmodifydate)
                             from inhospitalmainrecord_diagnosis dia1
                            where dia1.inpatientid = dia2.inpatientid
                              and dia1.inpatientdate = dia2.inpatientdate
                              and dia1.opendate = dia2.opendate)
                    order by dia2.seqid)where rownum = 1";
                }
                string strSQL = @"select distinct le.outhospital_dat cydate,
                de2.deptid_chr outdeptid,
                de2.deptname_vchr cydept,
                de3.deptid_chr indeptid,
                de3.deptname_vchr rydept,
                re.state_int ryInfo,
                red.lastname_vchr name,
                red.sex_chr sex,
                red.birth_dat birthday,
                red.nativeplace_vchr birthpl,
                red.idcard_chr idcard,
                red.nationality_vchr native,
                red.race_vchr nation,
                red.occupation_vchr job,
                red.married_chr statu,
                red.employer_vchr dwname,
                red.officeaddress_vchr dwaddr,
                red.officephone_vchr dwtele,
                red.officepc_vchr dwpost,
                red.residenceplace_vchr hkaddr,
                red.homepc_chr hkpost,
                red.contactpersonfirstname_vchr lxname,
                red.patientrelation_vchr relate,
                red.contactpersonaddress_vchr lxaddr,
                red.contactpersonphone_vchr lxtele,
                tr.targetareaid_chr transareaid,
                tr.deptname_vchr zkdept,
                im.confirmdiagnosisdate qzdate,
                im.*,
                re.patientid_chr,
                rel.hisinpatientid_chr prn,
                rel.hisinpatientdate rydate,
                re.inpatientcount_int times,
                dia.ryzd,
                re.registerid_chr,
                re.casedoctor_chr,
                empdt.lastname_vchr dtname,
                re.paytypeid_chr,
                tr.trtndoctor,
                tr.trtngroup zknum,
                trin.tringroup rynum,
                trou.trougroup cynum
  from t_opr_bih_leave le
 inner join t_bse_hisemr_relation rel on le.registerid_chr =
                                         rel.registerid_chr
 inner join t_opr_bih_register re on re.registerid_chr = le.registerid_chr
 inner join t_opr_bih_registerdetail red on red.registerid_chr =
                                            le.registerid_chr
 inner join t_bse_deptdesc de2 on de2.deptid_chr = le.outareaid_chr
 inner join t_bse_deptdesc de3 on de3.deptid_chr = re.areaid_chr
  left outer join t_bse_employee empdt on empdt.empid_chr =
                                          re.casedoctor_chr
  left outer join (select tr4.registerid_chr inreid,
                          tr4.doctorid_chr   trindoctor,
                          gre4.ba_deptnum    tringroup
                     from t_opr_bih_transfer tr4, t_emr_group_relation gre4
                    where tr4.type_int = 5
                      and gre4.groupid_chr = tr4.doctorgroupid_chr) trin on trin.inreid =
                                                                            le.registerid_chr
  left outer join (select tr2.registerid_chr outreid,
                          tr2.doctorid_chr   troudoctor,
                          gre2.ba_deptnum    trougroup
                     from t_opr_bih_transfer tr2, t_emr_group_relation gre2
                    where tr2.type_int = 7
                      and gre2.groupid_chr = tr2.doctorgroupid_chr
                      and tr2.transferid_chr =
                          (select max(tr3.transferid_chr)
                             from t_opr_bih_transfer tr3
                            where tr2.registerid_chr = tr3.registerid_chr
                              and tr3.type_int = 7)) trou on trou.outreid =
                                                             le.registerid_chr
  left outer join (select tr1.modify_dat,
                          tr1.targetareaid_chr,
                          tr1.registerid_chr,
                          de1.deptname_vchr,
                          tr1.doctorid_chr trtndoctor,
                          gre1.ba_deptnum trtngroup
                     from t_opr_bih_transfer   tr1,
                          t_bse_deptdesc       de1,
                          t_emr_group_relation gre1
                    where tr1.type_int = 3
                      and tr1.targetareaid_chr = de1.deptid_chr
                      and tr1.doctorgroupid_chr = gre1.groupid_chr
                      and tr1.transferid_chr =
                          (select max(tr2.transferid_chr)
                             from t_opr_bih_transfer tr2
                            where tr2.registerid_chr = tr1.registerid_chr
                              and tr2.type_int = 3)) tr on tr.registerid_chr =
                                                           le.registerid_chr
  left outer join (select im2.*,
                          ihmain.mzicd10 mzzd,
                          ihmain.mainicd10,
                          emp1.lastname_vchr directorname,
                          emp3.lastname_vchr inhospitaldtname,
                          emp4.lastname_vchr codername
                     from inhospitalmainrecord ihmain
                    inner join inhospitalmainrecord_content im2 on im2.inpatientid =
                                                                   ihmain.inpatientid
                                                               and im2.inpatientdate =
                                                                   ihmain.inpatientdate
                                                               and im2.opendate =
                                                                   ihmain.opendate
                                                               and im2.status = 1
                     left outer join t_bse_employee emp1 on emp1.empid_chr =
                                                            im2.directordt
                     left outer join t_bse_employee emp3 on emp3.empid_chr =
                                                            im2.inhospitaldt
                     left outer join t_bse_employee emp4 on emp4.empid_chr =
                                                            im2.coder
                    where ihmain.status = 1
                      and im2.lastmodifydate =
                          (select max(im1.lastmodifydate)
                             from inhospitalmainrecord_content im1
                            where im1.inpatientid = im2.inpatientid
                              and im1.inpatientdate = im2.inpatientdate
                              and im2.opendate = im1.opendate)) im on im.inpatientid =
                                                                      rel.emrinpatientid
                                                                  and im.inpatientdate =
                                                                      rel.emrinpatientdate
  left outer join (" + strSelecteDiagnosis + @") dia on dia.diainid =
                                                rel.emrinpatientid
                                            and dia.diaindate =
                                                rel.emrinpatientdate
 where le.status_int = 1
   and le.outhospital_dat >= ?
   and le.outhospital_dat <= ?
 order by le.outhospital_dat desc";
                #endregion

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmStartDate;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmEndDate;

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
        /// 获取病案内容(科室关联，非专业组)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtmStartDate">查询开始时间</param>
        /// <param name="p_dtmEndDate">查询结束时间</param>
        /// <param name="p_dtbResult">病案内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCaseData_dept(DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                #region SQL语句
                string strSelecteDiagnosis = string.Empty;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSelecteDiagnosis = @"select top 1 dia2.icd10         ryzd,
                          dia2.inpatientid   diainid,
                          dia2.inpatientdate diaindate
                     from inhospitalmainrecord_diagnosis dia2
                    where dia2.status = 1
                      and dia2.diagnosistype = 1
                      and dia2.lastmodifydate =
                          (select max(dia1.lastmodifydate)
                             from inhospitalmainrecord_diagnosis dia1
                            where dia1.inpatientid = dia2.inpatientid
                              and dia1.inpatientdate = dia2.inpatientdate
                              and dia1.opendate = dia2.opendate)
                    order by dia2.seqid  ";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSelecteDiagnosis = @"select dia2.icd10         ryzd,
                          dia2.inpatientid   diainid,
                          dia2.inpatientdate diaindate
                     from inhospitalmainrecord_diagnosis dia2
                    where dia2.status = 1
                      and dia2.diagnosistype = 1
                      and dia2.lastmodifydate =
                          (select max(dia1.lastmodifydate)
                             from inhospitalmainrecord_diagnosis dia1
                            where dia1.inpatientid = dia2.inpatientid
                              and dia1.inpatientdate = dia2.inpatientdate
                              and dia1.opendate = dia2.opendate)
                    order by dia2.seqid  fetch first 1 row only ";
                }
                else
                {
                    strSelecteDiagnosis = @"select * from(select dia2.icd10         ryzd,
                          dia2.inpatientid   diainid,
                          dia2.inpatientdate diaindate
                     from inhospitalmainrecord_diagnosis dia2
                    where dia2.status = 1
                      and dia2.diagnosistype = 1
                      and dia2.lastmodifydate =
                          (select max(dia1.lastmodifydate)
                             from inhospitalmainrecord_diagnosis dia1
                            where dia1.inpatientid = dia2.inpatientid
                              and dia1.inpatientdate = dia2.inpatientdate
                              and dia1.opendate = dia2.opendate)
                    order by dia2.seqid)where rownum = 1";
                }
                string strSQL = @"select distinct le.outhospital_dat cydate,
                de2.deptid_chr outdeptid,
                de2.deptname_vchr cydept,
                de3.deptid_chr indeptid,
                de3.deptname_vchr rydept,
                re.state_int ryInfo,
                red.lastname_vchr name,
                red.sex_chr sex,
                red.birth_dat birthday,
                red.nativeplace_vchr birthpl,
                red.idcard_chr idcard,
                red.nationality_vchr native,
                red.race_vchr nation,
                red.occupation_vchr job,
                red.married_chr statu,
                red.employer_vchr dwname,
                red.officeaddress_vchr dwaddr,
                red.officephone_vchr dwtele,
                red.officepc_vchr dwpost,
                red.residenceplace_vchr hkaddr,
                red.homepc_chr hkpost,
                red.contactpersonfirstname_vchr lxname,
                red.patientrelation_vchr relate,
                red.contactpersonaddress_vchr lxaddr,
                red.contactpersonphone_vchr lxtele,
                tr.targetareaid_chr transareaid,
                tr.deptname_vchr zkdept,
                im.confirmdiagnosisdate qzdate,
                im.*,
                re.patientid_chr,
                rel.hisinpatientid_chr prn,
                rel.hisinpatientdate rydate,
                re.inpatientcount_int times,
                dia.ryzd,
                re.registerid_chr,
                re.casedoctor_chr,
                empdt.lastname_vchr dtname,
                re.paytypeid_chr,
                tr.trtndoctor,
                tr.trtngroup zknum,
                trin.tringroup rynum,
                trou.trougroup cynum
  from t_opr_bih_leave le
 inner join t_bse_hisemr_relation rel on le.registerid_chr =
                                         rel.registerid_chr
 inner join t_opr_bih_register re on re.registerid_chr = le.registerid_chr
 inner join t_opr_bih_registerdetail red on red.registerid_chr =
                                            le.registerid_chr
 inner join t_bse_deptdesc de2 on de2.deptid_chr = le.outareaid_chr
 inner join t_bse_deptdesc de3 on de3.deptid_chr = re.areaid_chr
  left outer join t_bse_employee empdt on empdt.empid_chr =
                                          re.casedoctor_chr
  left outer join (select tr4.registerid_chr inreid,
                          tr4.doctorid_chr   trindoctor,
                          gre4.ba_deptnum    tringroup
                     from t_opr_bih_transfer tr4, t_emr_group_relation gre4
                    where tr4.type_int = 5
                      and gre4.groupid_chr = tr4.targetareaid_chr) trin on trin.inreid =
                                                                            le.registerid_chr
  left outer join (select tr2.registerid_chr outreid,
                          tr2.doctorid_chr   troudoctor,
                          gre2.ba_deptnum    trougroup
                     from t_opr_bih_transfer tr2, t_emr_group_relation gre2
                    where tr2.type_int = 7
                      and gre2.groupid_chr = tr2.sourceareaid_chr
                      and tr2.transferid_chr =
                          (select max(tr3.transferid_chr)
                             from t_opr_bih_transfer tr3
                            where tr2.registerid_chr = tr3.registerid_chr
                              and tr3.type_int = 7)) trou on trou.outreid =
                                                             le.registerid_chr
  left outer join (select tr1.modify_dat,
                          tr1.targetareaid_chr,
                          tr1.registerid_chr,
                          de1.deptname_vchr,
                          tr1.doctorid_chr trtndoctor,
                          gre1.ba_deptnum trtngroup
                     from t_opr_bih_transfer   tr1,
                          t_bse_deptdesc       de1,
                          t_emr_group_relation gre1
                    where tr1.type_int = 3
                      and tr1.targetareaid_chr = de1.deptid_chr
                      and tr1.targetareaid_chr = gre1.groupid_chr
                      and tr1.transferid_chr =
                          (select max(tr2.transferid_chr)
                             from t_opr_bih_transfer tr2
                            where tr2.registerid_chr = tr1.registerid_chr
                              and tr2.type_int = 3)) tr on tr.registerid_chr =
                                                           le.registerid_chr
  left outer join (select im2.*,
                          ihmain.mzicd10 mzzd,
                          ihmain.mainicd10,
                          emp1.lastname_vchr directorname,
                          emp3.lastname_vchr inhospitaldtname,
                          emp4.lastname_vchr codername
                     from inhospitalmainrecord ihmain
                    inner join inhospitalmainrecord_content im2 on im2.inpatientid =
                                                                   ihmain.inpatientid
                                                               and im2.inpatientdate =
                                                                   ihmain.inpatientdate
                                                               and im2.opendate =
                                                                   ihmain.opendate
                                                               and im2.status = 1
                     left outer join t_bse_employee emp1 on emp1.empid_chr =
                                                            im2.directordt
                     left outer join t_bse_employee emp3 on emp3.empid_chr =
                                                            im2.inhospitaldt
                     left outer join t_bse_employee emp4 on emp4.empid_chr =
                                                            im2.coder
                    where ihmain.status = 1
                      and im2.lastmodifydate =
                          (select max(im1.lastmodifydate)
                             from inhospitalmainrecord_content im1
                            where im1.inpatientid = im2.inpatientid
                              and im1.inpatientdate = im2.inpatientdate
                              and im2.opendate = im1.opendate)) im on im.inpatientid =
                                                                      rel.emrinpatientid
                                                                  and im.inpatientdate =
                                                                      rel.emrinpatientdate
  left outer join (" + strSelecteDiagnosis + @") dia on dia.diainid =
                                                rel.emrinpatientid
                                            and dia.diaindate =
                                                rel.emrinpatientdate
 where le.status_int = 1
   and le.outhospital_dat >= ?
   and le.outhospital_dat <= ?
 order by le.outhospital_dat desc";
                #endregion

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmStartDate;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmEndDate;

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

        #region 获取已同步病人


        /// <summary>
        /// 获取已同步病人

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtmStartDate">查询开始时间</param>
        /// <param name="p_dtmEndDate">查询结束时间</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        //[AutoComplete]
        public long m_lngGetHasSynchronousPatien(DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select b.prn,b.times
  from batemp b
 where b.cydate >= ?
   and b.cydate <= ?";
                //                string strSQL = @"select b.prn,b.times
                //  from batemp b
                // where b.cydate >= '2007/01/15'
                //   and b.cydate <= '2007/01/16'";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_dtmStartDate.ToString("yyyy/MM/dd");
                objDPArr[1].Value = p_dtmEndDate.ToString("yyyy/MM/dd");

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
                //lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 检查是否已存在指定住院号的病人
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strPatientNameArr">姓名</param>
        /// <returns></returns>
        //[AutoComplete]
        public long m_lngCheckSamePRN(string p_strInPatientID, out string[] p_strPatientNameArr)
        {
            p_strPatientNameArr = null;
            long lngRes = -1;
            try
            {
                string strCheckSQL = @"select name from batemp where prn = ?";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCheckSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null)
                {
                    int intRowsCount = dtbValue.Rows.Count;
                    if (intRowsCount > 0)
                    {
                        p_strPatientNameArr = new string[intRowsCount];
                        for (int iR = 0; iR < intRowsCount; iR++)
                        {
                            p_strPatientNameArr[iR] = dtbValue.Rows[iR][0].ToString();
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

        #region 提交记录到病人信息临时表
        /// <summary>
        /// 提交记录到病人信息临时表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue">病人信息</param>
        /// <returns></returns>
        //[AutoComplete]
        public long m_lngCommitToBATemp(clsEMR_SynchronousCaseValue p_objValue)
        {
            long lngRes = -1;
            try
            {
                if (p_objValue == null)
                {
                    return -1;
                }

                string strMarrid = string.Empty;
                int intMarrid = 1;
                if (int.TryParse(p_objValue.m_strSTATU, out intMarrid))
                {
                    strMarrid = intMarrid.ToString();
                }
                else
                {
                    m_lngGetDictCode(p_objValue.m_strSTATU, "5", out strMarrid);
                }
                string strFB = string.Empty;

                string strSex = string.Empty;
                if (p_objValue.m_strSEX == "男")
                {
                    strSex = "1";
                }
                else if (p_objValue.m_strSEX == "女")
                {
                    strSex = "2";
                }

                com.digitalwave.Utility.clsLogText objLogger1 = new com.digitalwave.Utility.clsLogText();
                bool blnRes1 = objLogger1.LogError(strSex);

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);

                string strCommitSQL = @"insert into batemp (prn,name,times,sex,birthday,birthpl,idcard,native,nation,fb,age,job,
statu,dwname,dwaddr,dwtele,dwpost,hkaddr,hkpost,lxname,relate,lxaddr,lxtele,rydate,rytime,ryinfo,source,rynum,
rydept,zknum,zkdept,cynum,cydept,cydate,cytime,days,mzzd,ryzd,qzdate,bfz,yngr,phzd,gmyw,blood,qjtimes,suctimes,
szqx,body,sum1,cwf,xyf,zyf,zcyf,zchf,jcf,zlf,fsf,ssf,hyf,sxf,syf,jsf,hlf,qtf,mzf,yef,pcf,sample,quality,zrdoct,
zzdoct,zydoct,sxdoct,bmy,mzacco,ryacco,opacco,twill,qjbr,qjsuc,babynum,sxfy,syfy,ifzdss,ifbzzl,ifinput,rysoure,
mzzd_zy,mzzd_zy10,ryzd_zy,ryzd_zy10,wyzd,cyfs,zllb,zzzy,icdm10_z) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?)";

                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(96, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_objValue.m_strPRN;
                objLisAddItemRefArr[1].Value = p_objValue.m_strNAME;
                objLisAddItemRefArr[2].Value = p_objValue.m_strTIMES;
                objLisAddItemRefArr[3].Value = strSex;
                objLisAddItemRefArr[4].Value = p_objValue.m_strBIRTHDAY;
                objLisAddItemRefArr[5].Value = p_objValue.m_strBIRTHPL;
                objLisAddItemRefArr[6].Value = p_objValue.m_strIDCARD;
                objLisAddItemRefArr[7].Value = p_objValue.m_strNATIVE;
                objLisAddItemRefArr[8].Value = p_objValue.m_strNATION;
                objLisAddItemRefArr[9].Value = p_objValue.m_strFB;
                objLisAddItemRefArr[10].Value = p_objValue.m_strAGE;
                objLisAddItemRefArr[11].Value = p_objValue.m_strJOB;
                objLisAddItemRefArr[12].Value = strMarrid;
                objLisAddItemRefArr[13].Value = p_objValue.m_strDWNAME;
                objLisAddItemRefArr[14].Value = p_objValue.m_strDWADDR;
                objLisAddItemRefArr[15].Value = p_objValue.m_strDWTELE;
                objLisAddItemRefArr[16].Value = p_objValue.m_strDWPOST;
                objLisAddItemRefArr[17].Value = p_objValue.m_strHKADDR;
                objLisAddItemRefArr[18].Value = p_objValue.m_strHKPOST;
                objLisAddItemRefArr[19].Value = p_objValue.m_strLXNAME;
                objLisAddItemRefArr[20].Value = p_objValue.m_strRELATE;
                objLisAddItemRefArr[21].Value = p_objValue.m_strLXADDR;
                objLisAddItemRefArr[22].Value = p_objValue.m_strLXTELE;
                objLisAddItemRefArr[23].Value = p_objValue.m_strRYDATE;
                objLisAddItemRefArr[24].Value = p_objValue.m_strRYTIME;
                objLisAddItemRefArr[25].Value = p_objValue.m_strRYINFO;
                objLisAddItemRefArr[26].Value = p_objValue.m_strSOURCE;
                objLisAddItemRefArr[27].Value = p_objValue.m_strRYNUM;
                objLisAddItemRefArr[28].Value = p_objValue.m_strRYDEPT;
                objLisAddItemRefArr[29].Value = p_objValue.m_strZKNUM;
                objLisAddItemRefArr[30].Value = p_objValue.m_strZKDEPT;
                objLisAddItemRefArr[31].Value = p_objValue.m_strCYNUM;
                objLisAddItemRefArr[32].Value = p_objValue.m_strCYDEPT;
                objLisAddItemRefArr[33].Value = p_objValue.m_strCYDATE;
                objLisAddItemRefArr[34].Value = p_objValue.m_strCYTIME;
                objLisAddItemRefArr[35].Value = p_objValue.m_strDAYS;
                objLisAddItemRefArr[36].Value = p_objValue.m_strMZZD;
                objLisAddItemRefArr[37].Value = p_objValue.m_strRYZD;
                objLisAddItemRefArr[38].Value = p_objValue.m_strQZDATE;
                objLisAddItemRefArr[39].Value = p_objValue.m_strBFZ;
                double dblTemp = 0D;
                if (double.TryParse(p_objValue.m_strYNGR, out dblTemp))
                {
                    objLisAddItemRefArr[40].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[40].Value = DBNull.Value;
                }
                objLisAddItemRefArr[41].Value = p_objValue.m_strPHZD;
                objLisAddItemRefArr[42].Value = p_objValue.m_strGMYW;
                objLisAddItemRefArr[43].Value = p_objValue.m_strBLOOD;
                if (double.TryParse(p_objValue.m_strQJTIMES, out dblTemp))
                {
                    objLisAddItemRefArr[44].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[44].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strSUCTIMES, out dblTemp))
                {
                    objLisAddItemRefArr[45].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[45].Value = DBNull.Value;
                }
                objLisAddItemRefArr[46].Value = p_objValue.m_strSZQX;
                objLisAddItemRefArr[47].Value = p_objValue.m_strBODY;
                if (double.TryParse(p_objValue.m_strSUM1, out dblTemp))
                {
                    objLisAddItemRefArr[48].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[48].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strCWF, out dblTemp))
                {
                    objLisAddItemRefArr[49].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[49].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strXYF, out dblTemp))
                {
                    objLisAddItemRefArr[50].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[50].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strZYF, out dblTemp))
                {
                    objLisAddItemRefArr[51].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[51].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strZCYF, out dblTemp))
                {
                    objLisAddItemRefArr[52].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[52].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strZCHF, out dblTemp))
                {
                    objLisAddItemRefArr[53].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[53].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strJCF, out dblTemp))
                {
                    objLisAddItemRefArr[54].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[54].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strZLF, out dblTemp))
                {
                    objLisAddItemRefArr[55].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[55].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strFSF, out dblTemp))
                {
                    objLisAddItemRefArr[56].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[56].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strSSF, out dblTemp))
                {
                    objLisAddItemRefArr[57].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[57].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strHYF, out dblTemp))
                {
                    objLisAddItemRefArr[58].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[58].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strHLF, out dblTemp))
                {
                    objLisAddItemRefArr[59].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[59].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strSYF, out dblTemp))
                {
                    objLisAddItemRefArr[60].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[60].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strJSF, out dblTemp))
                {
                    objLisAddItemRefArr[61].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[61].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strSXF, out dblTemp))
                {
                    objLisAddItemRefArr[62].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[62].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strQTF, out dblTemp))
                {
                    objLisAddItemRefArr[63].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[63].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strMZF, out dblTemp))
                {
                    objLisAddItemRefArr[64].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[64].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strYEF, out dblTemp))
                {
                    objLisAddItemRefArr[65].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[65].Value = DBNull.Value;
                }
                if (double.TryParse(p_objValue.m_strPCF, out dblTemp))
                {
                    objLisAddItemRefArr[66].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[66].Value = DBNull.Value;
                }
                objLisAddItemRefArr[67].Value = p_objValue.m_strSAMPLE;
                objLisAddItemRefArr[68].Value = p_objValue.m_strQUALITY;
                objLisAddItemRefArr[69].Value = p_objValue.m_strZRDOCT;
                objLisAddItemRefArr[70].Value = p_objValue.m_strZZDOCT;
                objLisAddItemRefArr[71].Value = p_objValue.m_strZYDOCT;
                objLisAddItemRefArr[72].Value = p_objValue.m_strSXDOCT;
                objLisAddItemRefArr[73].Value = p_objValue.m_strBMY;
                objLisAddItemRefArr[74].Value = p_objValue.m_strMZACCO;
                objLisAddItemRefArr[75].Value = p_objValue.m_strRYACCO;
                objLisAddItemRefArr[76].Value = p_objValue.m_strOPACCO;
                objLisAddItemRefArr[77].Value = p_objValue.m_strTWILL;
                objLisAddItemRefArr[78].Value = p_objValue.m_strQJBR;
                objLisAddItemRefArr[79].Value = p_objValue.m_strQJSUC;
                if (double.TryParse(p_objValue.m_strBABYNUM, out dblTemp))
                {
                    objLisAddItemRefArr[80].Value = dblTemp;
                }
                else
                {
                    objLisAddItemRefArr[80].Value = DBNull.Value;
                }
                objLisAddItemRefArr[81].Value = p_objValue.m_strSXFY;
                objLisAddItemRefArr[82].Value = p_objValue.m_strSYFY;
                objLisAddItemRefArr[83].Value = p_objValue.m_strIFZDSS;
                objLisAddItemRefArr[84].Value = p_objValue.m_strIFBZZL;
                objLisAddItemRefArr[85].Value = p_objValue.m_strIFINPUT;
                objLisAddItemRefArr[86].Value = p_objValue.m_strRYSOURE;
                objLisAddItemRefArr[87].Value = p_objValue.m_strMZZD_ZY;
                objLisAddItemRefArr[88].Value = p_objValue.m_strMZZD_ZY10;
                objLisAddItemRefArr[89].Value = p_objValue.m_strRYZD_ZY;
                objLisAddItemRefArr[90].Value = p_objValue.m_strRYZD_ZY10;
                objLisAddItemRefArr[91].Value = p_objValue.m_strWYZD;
                objLisAddItemRefArr[92].Value = p_objValue.m_strCYFS;
                objLisAddItemRefArr[93].Value = p_objValue.m_strZLLB;
                objLisAddItemRefArr[94].Value = p_objValue.m_strZZZY;
                objLisAddItemRefArr[95].Value = p_objValue.m_strICDM10_Z;

                //执行SQL	
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objLisAddItemRefArr);
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="p_strName">名称</param>
        /// <param name="p_strDictKind">字典类型</param>
        /// <param name="p_strReturnValue">返回编码</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetDictCode(string p_strName, string p_strDictKind, out string p_strReturnValue)
        {
            p_strReturnValue = string.Empty;
            long lngRes = 0;

            if (string.IsNullOrEmpty(p_strName) || string.IsNullOrEmpty(p_strDictKind))
            {
                return -1;
            }

            try
            {
                string strSQL = @"select t.dictid_chr
  from t_aid_dict t
 where t.dictkind_chr = ? and t.dictname_vchr like ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDictKind;
                objDPArr[1].Value = p_strName + "%";

                DataTable dtbResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_strReturnValue = dtbResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取同步表费用信息

        /// <summary>
        /// 产科获取同步表费用信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">病人入院登记流水号</param>
        /// <param name="p_objRecordArr">记录列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeChanKe(string p_strRegisterID, DataTable p_strbbReisterID, out clsInHospitalMainCharge[] p_objRecordArr)
        {
            p_objRecordArr = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string parm = "?,";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                int length = p_strbbReisterID.Rows.Count + 1;
                objHRPServ.CreateDatabaseParameter(length, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                if (p_strbbReisterID.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < p_strbbReisterID.Rows.Count; i1++)
                    {
                        parm = parm + "?,";
                        objDPArr[i1 + 1].Value = p_strbbReisterID.Rows[0][0].ToString();
                    }
                }
                parm = parm.Substring(0, parm.Length - 1);
                string strSQL = @"select sum(k.tolfee_mny) tolfee_mny, k.groupname_chr
  from (select (round(b.amount_dec * b.unitprice_dec, 2) + round(nvl(b.totaldiffcostmoney_dec,0), 2)) as tolfee_mny,
               --b.totalmoney_dec   tolfee_mny,
               c.itembihctype_chr,
               d.typename_vchr    groupname_chr
          from t_opr_bih_patientcharge b,
               t_bse_chargeitem        c,
               t_bse_chargeitemextype  d
         where b.chargeitemid_chr = c.itemid_chr
           and b.status_int > 0
           --and b.pstatus_int in (1, 2)
           and c.itembihctype_chr = d.typeid_chr
           and d.flag_int = 5
           and b.registerid_chr in(" + parm + @"))k
 group by k.groupname_chr
";

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    DataRow drCurrent = null;
                    p_objRecordArr = new clsInHospitalMainCharge[intRowsCount];
                    double dblTemp = 0D;
                    for (int i = 0; i < intRowsCount; i++)
                    {
                        p_objRecordArr[i] = new clsInHospitalMainCharge();
                        drCurrent = dtbResult.Rows[i];
                        p_objRecordArr[i].m_strRegisterID = p_strRegisterID;
                        if (double.TryParse(drCurrent["tolfee_mny"].ToString(), out dblTemp))
                        {
                            p_objRecordArr[i].m_dblMoney = dblTemp;
                        }
                        else
                        {
                            p_objRecordArr[i].m_dblMoney = 0.00D;
                        }
                        p_objRecordArr[i].m_strTypeName = drCurrent["groupname_chr"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 关联产妇住院号获取婴儿流水号
        /// </summary>
        /// <param name="m_dtmUpdateDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable m_lngGetRgisterIDByInpatientID(string p_strRegisterid)
        {
            DataTable dtbResult = null;
            long lngRes = 0;
            string bbRegisterID = string.Empty;
            string strSQL = @"select registerid_chr
  from t_opr_bih_register
 where relateregisterid_chr = ?
";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterid;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return dtbResult;

        }


        /// <summary>
        /// 获取住院结算表里面病人自付金额部分
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSelfPay(string p_strRegisterID, out clsInHospitalMainRecord_Content p_objRecordcontent)
        {
            p_objRecordcontent = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                string strSQL = @"select sum(t.sbsum_mny) sbsum_mny
  from t_opr_bih_charge t, t_Opr_Bih_Register t2
 where t.registerid_chr = t2.registerid_chr
   and t2.registerid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                //objDPArr[1].DbType = DbType.DateTime;
                //objDPArr[1].Value = p_dtmInhospitalDate;
                p_objRecordcontent = new clsInHospitalMainRecord_Content();
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objRecordcontent.m_strSelfamt = dtbValue.Rows[0]["sbsum_mny"].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 获取同步表费用信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">病人入院登记流水号</param>
        /// <param name="p_objRecordArr">记录列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCHRCATE(string p_strRegisterID, out clsInHospitalMainCharge[] p_objRecordArr)
        {
            p_objRecordArr = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select sum(k.tolfee_mny) tolfee_mny, k.groupname_chr
                                      from (select (round(b.amount_dec * b.unitprice_dec, 2) +
                                                   round(nvl(b.totaldiffcostmoney_dec, 0), 2)) as tolfee_mny,
                                                   c.itembihctype_chr,
                                                   d.typename_vchr groupname_chr
                                              from t_opr_bih_patientcharge b,
                                                   t_bse_chargeitem        c,
                                                   t_bse_chargeitemextype  d
                                             where b.chargeitemid_chr = c.itemid_chr
                                               and b.status_int = 1
                                               and b.pstatus_int > 0
                                               and c.itembihctype_chr = d.typeid_chr
                                               and d.flag_int = 5
                                               and b.registerid_chr = ?) k
                                     group by k.groupname_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    DataRow drCurrent = null;
                    p_objRecordArr = new clsInHospitalMainCharge[intRowsCount];
                    double dblTemp = 0D;
                    for (int i = 0; i < intRowsCount; i++)
                    {
                        p_objRecordArr[i] = new clsInHospitalMainCharge();
                        drCurrent = dtbResult.Rows[i];
                        p_objRecordArr[i].m_strRegisterID = p_strRegisterID;
                        if (double.TryParse(drCurrent["tolfee_mny"].ToString(), out dblTemp))
                        {
                            p_objRecordArr[i].m_dblMoney = dblTemp;
                        }
                        else
                        {
                            p_objRecordArr[i].m_dblMoney = 0.00D;
                        }
                        p_objRecordArr[i].m_strTypeName = drCurrent["groupname_chr"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 检查指定病人的病案资料是否已同步


        /// <summary>
        /// 检查指定病人的病案资料是否已同步

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPRN">病案号</param>
        /// <param name="p_strTimes">入院次数</param>
        /// <param name="p_blnHasSynchronous">是否已同步</param>
        /// <returns></returns>
        //[AutoComplete]
        public long m_lngCheckHasSynchronous(string p_strPRN, string p_strTimes, out bool p_blnHasSynchronous)
        {
            p_blnHasSynchronous = false;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strPRN) || string.IsNullOrEmpty(p_strTimes))
            {
                return -1;
            }
            try
            {
                string strSQL = @"select b.name, b.prn, b.times
  from batemp b
 where b.prn = ?
   and b.times = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strPRN;
                objDPArr[1].Value = p_strTimes;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_blnHasSynchronous = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion
    }
}
