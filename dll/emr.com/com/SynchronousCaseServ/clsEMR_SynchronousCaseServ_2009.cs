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
    /// 病案同步2009新版中间件
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsEMR_SynchronousCaseServ_2009 : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 获取病案基本内容(关联科室)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtmStartDate">查询开始时间</param>
        /// <param name="p_dtmEndDate">查询结束时间</param>
        /// <param name="p_dtbResult">病案基本内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCaseData_Dept(DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = -1;
            try
            {
                string strSQL = @"select distinct le.outhospital_dat              fcydate,
                le.outdeptid_chr,
                gre2.ba_deptname                fcydept,
                gre2.ba_deptnum                 fcytykh,
                trin.ba_deptname                frydept,
                re.state_int                    fryInfo,
                red.homeaddress_vchr            FCURRADDR,
                red.homephone_vchr              FCURRTELE,
                red.contactpersonpc_chr         FCURRPOST,
                red.lastname_vchr               fname,
                red.sex_chr                     fsex,
                red.birth_dat                   fbirthday,
                red.birthplace_vchr             fbirthplace,
                red.idcard_chr                  fidcard,
                red.nationality_vchr            fcountry,
                red.race_vchr                   fnationality,
                red.nativeplace_vchr            FNATIVE,
                red.occupation_vchr             fjob,
                red.married_chr                 fstatus,
                red.employer_vchr               fdwname,
                red.officeaddress_vchr          fdwaddr,
                red.officephone_vchr            fdwtele,
                red.officepc_vchr               fdwpost,
                red.residenceplace_vchr         fhkaddr,
                red.homepc_chr                  fhkpost,
                red.contactpersonfirstname_vchr flxname,
                red.patientrelation_vchr        frelate,
                red.contactpersonaddress_vchr   flxaddr,
                red.contactpersonphone_vchr     flxtele,
                red.insuranceid_vchr,
                re.patientid_chr,
                re.inpatientid_chr              fprn,
                re.inpatient_dat                frydate,
                re.inpatientcount_int           ftimes,
                re.registerid_chr,
                re.casedoctor_chr,
                re.paytypeid_chr,
                trin.ba_deptnum                 frytykh,
                pa.patientsources_vchr,
                pay.ba_paytypeid_chr
  from t_opr_bih_leave le
 inner join t_opr_bih_register re
    on re.registerid_chr = le.registerid_chr
   and re.status_int != '-1'
 inner join t_opr_bih_registerdetail red
    on red.registerid_chr = le.registerid_chr
 inner join t_bse_patient pa
    on pa.patientid_chr = re.patientid_chr
   and pa.status_int = 1
  left outer join t_emr_group_relation gre2
    on le.outdeptid_chr = gre2.groupid_chr
  left outer join (select tr4.registerid_chr,
                          tr4.doctorid_chr,
                          gre4.ba_deptnum,
                          gre4.ba_deptname
                     from t_opr_bih_transfer tr4, t_emr_group_relation gre4
                    where tr4.type_int = 5
                      and gre4.groupid_chr = tr4.targetdeptid_chr) trin
    on trin.registerid_chr = le.registerid_chr
  left outer join t_emr_paytype_relation pay
    on pay.paytypeid_chr = re.paytypeid_chr
 where le.status_int = 1
   and le.outhospital_dat >= ?
   and le.outhospital_dat <= ?
 order by le.outhospital_dat desc
";

                clsHRPTableService objHRPServ = new clsHRPTableService();
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
        /// 获取病案基本内容(关联科室)，指定病人
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">病案基本内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCaseData_Dept(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = -1;
            try
            {
                string strSQL = @"select distinct le.outhospital_dat              fcydate,
                le.outdeptid_chr,
                gre2.ba_deptname                fcydept,
                gre2.ba_deptnum                 fcytykh,
                trin.ba_deptname                frydept,
                re.state_int                    fryinfo,
                red.lastname_vchr               fname,
                red.sex_chr                     fsex,
                red.birth_dat                   fbirthday,
                red.birthplace_vchr             fbirthplace,
                red.idcard_chr                  fidcard,
                red.nationality_vchr            fcountry,
                red.race_vchr                   fnationality,
                red.nativeplace_vchr            fnative,
                red.homeaddress_vchr            fcurraddr,
                red.homephone_vchr              fcurrtele,
                red.contactpersonpc_chr         fcurrpost,
                red.occupation_vchr             fjob,
                red.married_chr                 fstatus,
                red.employer_vchr               fdwname,
                red.officeaddress_vchr          fdwaddr,
                red.officephone_vchr            fdwtele,
                red.officepc_vchr               fdwpost,
                red.residenceplace_vchr         fhkaddr,
                red.homepc_chr                  fhkpost,
                red.contactpersonfirstname_vchr flxname,
                red.patientrelation_vchr        frelate,
                red.contactpersonaddress_vchr   flxaddr,
                red.contactpersonphone_vchr     flxtele,
                red.insuranceid_vchr,
                re.patientid_chr,
                re.inpatientid_chr              fprn,
                re.inpatient_dat                frydate,
                re.inpatientcount_int           ftimes,
                re.registerid_chr,
                re.casedoctor_chr,
                re.paytypeid_chr,
                trin.ba_deptnum                 frytykh,
                pa.patientsources_vchr,
                pay.ba_paytypeid_chr
  from t_opr_bih_leave le
 inner join t_opr_bih_register re
    on re.registerid_chr = le.registerid_chr
   and re.status_int != '-1'
 inner join t_opr_bih_registerdetail red
    on red.registerid_chr = le.registerid_chr
 inner join t_bse_patient pa
    on pa.patientid_chr = re.patientid_chr
   and pa.status_int = 1
  left outer join t_emr_group_relation gre2
    on le.outdeptid_chr = gre2.groupid_chr
  left outer join (select tr4.registerid_chr,
                          tr4.doctorid_chr,
                          gre4.ba_deptnum,
                          gre4.ba_deptname
                     from t_opr_bih_transfer tr4, t_emr_group_relation gre4
                    where tr4.type_int = 5
                      and gre4.groupid_chr = tr4.targetdeptid_chr) trin
    on trin.registerid_chr = le.registerid_chr
  left outer join t_emr_paytype_relation pay
    on pay.paytypeid_chr = re.paytypeid_chr
 where le.status_int = 1
   and le.registerid_chr = ?
 order by le.outhospital_dat desc
";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

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
        /// 获取广东病案系统字典
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbDICT">字典</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGDCaseDICT(out DataTable p_dtbDICT)
        {
            p_dtbDICT = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.fmc, t.fcode, t.fbh, t.fzjc from tstandardmx t where t.fzf = 0";
                 
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbDICT);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取病案首页病人入院信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">入院信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select a.diagnosisxml,
       a.mzicd10,
       b.diagnosis,
       b.condictionwhenin,
       b.confirmdiagnosisdate,
       b.doctor,
       b.insurancenum,
       b.inhospitalway,
       b.condictionwhenin,
       b.path,
       b.newbabyweight,
       b.newbabyinhostpitalweight,
       b.modeofpayment
  from inhospitalmainrecord a
 inner join inhospitalmainrecord_content b
    on a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
 inner join t_bse_hisemr_relation r
    on r.emrinpatientid = a.inpatientid
   and r.emrinpatientdate = a.inpatientdate
 where a.status = 1
   and b.status = 1
   and r.registerid_chr = ?";


                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

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
        /// 获取病案首页病人诊断信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">诊断信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientDiagnosisInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select a.maindiagnosisxml,
       a.icd_10ofmainxml,
       a.pathologydiagnosisxml,
       a.scachesourcexml,
       '' scachesourceicdxml,
       a.sensitivexml,
       a.hbsagxml,
       a.hcv_abxml,
       a.hiv_abxml,
       a.accordwithouthospitalxml,
       a.accordinwithoutxml,
       a.accordbfoprwithafxml,
       a.accordclinicwithpathologyxml,
       a.accordradiatewithpathologyxml,
       a.salvetimesxml,
       a.salvesuccessxml,
       '' subsidiarydiagnosisxml,
       '' subsidiarydiagnosis,
       '' icdofsubsidiarydiagnosis,
       '' subsidiarydiagnosisseq,
       b.maindiagnosis,
       b.mainconditionseq,
       b.icd_10ofmain,
       b.pathologydiagnosis,
       b.scachesource,
       b.sszyj_jbbm scachesourceicd,
       b.sensitive,
       b.hbsag,
       b.hcv_ab,
       b.hiv_ab,
       b.accordwithouthospital,
       b.accordinwithout,
       b.accordbeforeoperationwithafter,
       b.accordclinicwithpathology,
       b.accordradiatewithpathology,
       b.salvetimes,
       b.salvesuccess,
       b.quality,
       b.qctime,
       b.directordt,
       b.subdirectordt,
       b.dt,
       b.inhospitaldt,
       b.attendinforadvancesstudydt,
       b.graduatestudentintern,
       b.intern,
       b.coder,
       b.qcdt,
       b.qcnurse,
       b.blzd_blh,
       b.blzd_jbbm,
       b.discharged_int,
       b.discharged_varh,
       b.readmitted31_int,
       b.readmitted31_varh,
       b.inrnssday,
       b.inrnsshour,
       b.inrnssmin,
       b.outrnssday,
       b.outrnsshour,
       b.outrnssmin
  from inhospitalmainrecord a
 inner join inhospitalmainrecord_content b
    on a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
 inner join t_bse_hisemr_relation r
    on r.emrinpatientid = a.inpatientid
   and r.emrinpatientdate = a.inpatientdate
 where a.status = 1
   and b.status = 1
   and r.registerid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

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
        /// 获取诊断信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strType">诊断类型</param>
        /// <param name="p_dtbResult">诊断内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDiagnosis(string p_strRegisterID, string p_strType, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select b.icd10 code, b.diagnosis name, b.result outinfo
  from inhospitalmainrecord a
 inner join inhospitalmainrecord_diagnosis b
    on a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
 inner join t_bse_hisemr_relation r
    on r.emrinpatientid = a.inpatientid
   and r.emrinpatientdate = a.inpatientdate
 where a.status = 1
   and b.status = 1
   and b.diagnosistype = ?
   and r.registerid_chr = ?
 order by b.seqid";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strType;
                objDPArr[1].Value = p_strRegisterID;

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
        /// 获取病人手术信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbOP">手术信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOperationInfo(string p_strRegisterID, out DataTable p_dtbOP)
        {
            p_dtbOP = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select b.operationid opcode,
       b.operationdate opdate,
       b.operationname opname,
       b.operator,
       b.assistant1,
       b.assistant2,
       b.aanaesthesiamodeid,
       b.cutlevel,
       b.anaesthetist,
       b.operationaanaesthesiamodename ananame,
       b.operationanaesthetistname anadoctor,
       '' cutlevelid,
       b.operationlevel,
       b.operationelective,
       e1.lastname_vchr opdoctor,
       e1.empno_chr opdoctorno,
       e2.lastname_vchr firstassist,
       e2.empno_chr firstassistno,
       e3.lastname_vchr secondassist,
       e3.empno_chr secondassistno,
       e4.empno_chr anadoctorno
  from inhospitalmainrecord a
 inner join inhospitalmainrecord_operation b
    on a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
 inner join t_bse_hisemr_relation r
    on r.emrinpatientid = a.inpatientid
   and r.emrinpatientdate = a.inpatientdate
  left outer join t_bse_employee e1
    on b.operator = e1.empid_chr
   and e1.status_int = 1
  left outer join t_bse_employee e2
    on b.assistant1 = e2.empid_chr
   and e2.status_int = 1
  left outer join t_bse_employee e3
    on b.assistant2 = e3.empid_chr
   and e3.status_int = 1
  left outer join t_bse_employee e4
    on b.anaesthetist = e4.empid_chr
   and e4.status_int = 1
 where a.status = 1
   and b.status = 1
   and r.registerid_chr = ?
 order by b.seqid
";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbOP, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 产科分娩婴儿记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbLaborInfo">产科分娩婴儿记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngLaborInfo(string p_strRegisterID, out DataTable p_dtbLaborInfo)
        {
            p_dtbLaborInfo = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select b.male,
       b.female,
       b.liveborn,
       b.dieborn,
       b.dienotborn,
       b.weight infantweight,
       b.die,
       b.changedepartment,
       b.outhospital,
       b.suffocate2,
       b.naturalcondiction,
       b.suffocate1,
       b.infectiontimes,
       b.infectionname name,
       b.icd10 code,
       b.salvetimes rescuetimes,
       b.salvesuccesstimes rescuesucctimes,
       b.seqid
  from inhospitalmainrecord      a,
       inhospitalmainrecord_baby b,
       t_bse_hisemr_relation     r
 where a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.status = 1
   and b.status = 1
   and a.inpatientid = r.emrinpatientid
   and a.inpatientdate = r.emrinpatientdate
   and r.registerid_chr = ?
 order by b.seqid";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbLaborInfo, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取肿瘤专科病人治疗记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">肿瘤专科病人治疗记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChemotherapyInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select a.originaldiseasegyxml,
       a.originaldiseasetimesxml,
       a.originaldiseasedaysxml,
       a.lymphgyxml,
       a.lymphtimesxml,
       a.lymphdaysxml,
       a.metastasisgyxml,
       a.metastasistimesxml,
       a.metastasisdaysxml,
       b.rtmodeseq,
       b.rtruleseq,
       b.rtco,
       b.rtaccelerator,
       b.rtx_ray,
       b.rtlacuna,
       b.originaldiseaseseq,
       b.originaldiseasegy,
       b.originaldiseasetimes,
       b.originaldiseasedays,
       b.originaldiseasebegindate,
       b.originaldiseaseenddate,
       b.lymphseq,
       b.lymphgy,
       b.lymphtimes,
       b.lymphdays,
       b.lymphbegindate,
       b.lymphenddate,
       b.metastasisgy,
       b.metastasistimes,
       b.metastasisdays,
       b.metastasisbegindate,
       b.metastasisenddate,
       b.chemotherapymodeseq,
       b.chemotherapywholebody,
       b.chemotherapylocal,
       b.chemotherapyintubate,
       b.chemotherapythorax,
       b.chemotherapyabdomen,
       b.chemotherapyspinal,
       b.chemotherapyothertry,
       b.chemotherapyother
  from inhospitalmainrecord a
 inner join inhospitalmainrecord_content b
    on a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
 inner join t_bse_hisemr_relation r
    on r.emrinpatientid = a.inpatientid
   and r.emrinpatientdate = a.inpatientdate
 where a.status = 1
   and b.status = 1
   and r.registerid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

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
        /// 获取肿瘤专科病人药物治疗记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">肿瘤专科病人药物治疗记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChemotherapyMedicine(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select b.chemotherapydate curedate,
       b.medicinename medicine,
       b.period treatment,
       b.field_cr,
       b.field_pr,
       b.field_mr,
       b.field_s,
       b.field_p,
       b.field_na
  from inhospitalmainrecord a, ihmainrecord_chemotherapy b,
       t_bse_hisemr_relation     r
 where a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.status = 1
   and b.status = 1
   and a.opendate = b.opendate
   and a.inpatientid = r.emrinpatientid
   and a.inpatientdate = r.emrinpatientdate
   and r.registerid_chr = ?
 order by seqid";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

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
        /// 获取病案首页其他信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">病案首页其他信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOthersCaseInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select b.corpsecheck,
       b.firstcase,
       b.follow,
       b.follow_week,
       b.follow_month,
       b.follow_year,
       b.modelcase,
       b.bloodtype,
       b.bloodrh,
       b.bloodtransactoin,
       b.rbc,
       b.plt,
       b.plasm,
       b.wholeblood,
       b.otherblood,
       b.consultation,
       b.longdistanctconsultation,
       b.toplevel,
       b.nurseleveli,
       b.nurselevelii,
       b.nurseleveliii,
       b.icu,
       b.specialnurse,
       '' singlediseasetype,
       a.follow_weekxml,
       a.follow_monthxml,
       a.follow_yearxml,
       a.bloodtypexml,
       a.rbcxml,
       a.pltxml,
       a.plasmxml,
       a.wholebloodxml,
       a.otherbloodxml,
       a.consultationxml,
       a.longdistanctconsultationxml,
       a.toplevelxml,
       a.nurselevelixml,
       a.nurseleveliixml,
       a.nurseleveliiixml,
       a.icuxml,
       a.specialnursexml
  from inhospitalmainrecord a, inhospitalmainrecord_content b,
       t_bse_hisemr_relation     r
 where a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.status = 1
   and b.status = 1
   and a.opendate = b.opendate
   and a.inpatientid = r.emrinpatientid
   and a.inpatientdate = r.emrinpatientdate
   and r.registerid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

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
        /// 获取HIS_BA1表的结构
        /// </summary>
        /// <param name="p_dtbHIS_BA1">HIS_BA1表</param>
        /// <returns></returns>
        public long m_lngGetHIS_BA1Schema(out DataTable p_dtbHIS_BA1)
        {
            p_dtbHIS_BA1 = null;
            long lngRes = 0;
            try
            {
                //此语句不会查出任何内容，只为得到HIS_BA1的结构
                string strSQL = @"select * from his_ba1 hb where hb.fprn='0' and hb.ftimes=-1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbHIS_BA1);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取简要住院周转记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">简要住院周转记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTransferInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select d.deptname_vchr, gre1.ba_deptnum, t.modify_dat,t.type_int
  from t_opr_bih_transfer t, t_emr_group_relation gre1, t_bse_deptdesc d
 where t.targetdeptid_chr = gre1.groupid_chr
   and t.targetdeptid_chr = d.deptid_chr
   and t.registerid_chr = ?
 order by t.modify_dat";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取病案同步记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_objVO">病案同步记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCaseRecord(string p_strRegisterID, out clsEMR_SynchronousCase2009_VO p_objVO)
        {
            p_objVO = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select registerid_chr, caserecord, status
  from t_emr_synchronouscaserecord
 where registerid_chr = ?";

                DataTable dtbValue = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    p_objVO = new clsEMR_SynchronousCase2009_VO();
                    p_objVO.m_strRegisterID = dtbValue.Rows[0]["registerid_chr"].ToString();
                    p_objVO.m_strContentXML = dtbValue.Rows[0]["caserecord"].ToString();
                    p_objVO.m_intType = Convert.ToInt32(dtbValue.Rows[0]["status"]);
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


        /// <summary>
        /// 获取同步病案记录
        /// </summary>
        /// <param name="p_dtmOutBegin">出院开始时间</param>
        /// <param name="p_dtmOutEnd">出院结束时间</param>
        /// <param name="p_dtbRecord">同步病案记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSynchronousCaseRecord(DateTime p_dtmOutBegin, DateTime p_dtmOutEnd, out DataTable p_dtbRecord)
        {
            p_dtbRecord = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select a.status issave, a.registerid_chr
  from t_emr_synchronouscaserecord a, t_opr_bih_leave b
 where a.registerid_chr = b.registerid_chr
   and b.status_int = 1
   and b.outhospital_dat between ? and ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmOutBegin;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmOutEnd;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
    }
}
