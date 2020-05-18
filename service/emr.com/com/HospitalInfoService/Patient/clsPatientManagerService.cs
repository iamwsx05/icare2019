using System;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.PatientManagerService
{
    /// <summary>
    /// 通过床号模糊查询时，只查询当前正在住院的病人，通过住院号或姓名模糊查询时，查询所有的病人（包括已经出院的病人）
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPatientManagerService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 病案首页查询
        /// <summary>
        /// 病案首页查询
        /// </summary>
        /// <param name="p_strDeptId"></param>
        /// <param name="p_strPatantId"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngHospitalQuery(string p_strDeptId, string p_strPatantId, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0;
            string strWhere = "";
            DateTime dateOut = DateTime.Today;
            dateOut = dateOut.AddDays(-7);
            if (p_strPatantId != "")
            {
                strWhere = " and a.PATIENTID_CHR='" + p_strPatantId + "'";
            }
            string strSQl = @"select f.modify_dat,g.lastname_vchr,b.pathologydiagnosisseq, b.condictionwhenin,b.confirmdiagnosisdate,b.diagnosis,c.emr_seqid,b.complication,
b.maindiagnosis,b.mainconditionseq,b.othermaincondition,d.emr_seqod,b.complicationseq,b.othercomplication,b.infectiondiagnosis,b.infectioncondictionseq,b.otherinfectioncondiction,
b.pathologydiagnosis,b.scachesource,b.new5disease,b.secondleveltransfer,b.sensitive,b.hbsag,b.hcv_ab,b.hiv_ab,e.emr_seqop,b.neonatedisease1,b.neonatedisease2,b.neonatedisease3,
b.neonatedisease4,b.salvetimes,b.salvesuccess,b.hasremind,b.remindterm,b.accordwithouthospital,b.accordinwithout,b.accordbfoprwithaf,b.accordclinicwithpathology,
b.accorddeathwithbodycheck,b.accordclinicwithradiate,b.modelcase,b.firstcase,b.quality,b.antibacterial,b.pathogeny,b.pathogenyresult,b.bloodtransactoin,
b.transfusionsaction,b.ctcheck,b.mricheck,b.bloodtype,b.bloodrh,b.xraycheck,b.rbc,b.plt,b.plasm,b.wholeblood,b.otherblood,b.deptdirectordt,b.dt,b.inhospitaldoc,
b.outhospitaldoc,b.directordt,b.subdirectordt,b.attendinforadvancesstudydt,b.graduatestudentintern,b.intern,b.totalamt,b.bedamt,b.nurseamt,b.wmamt,b.cmfinishedamt,
b.cmsemifinishedamt,b.radiationamt,b.assayamt,b.o2amt,b.bloodamt,b.treatmentamt,b.operationamt,b.checkamt,b.anaethesiaamt,b.deliverychildamt,b.babyamt,b.accompanyamt,
b.otheramt,b.neatenname,b.codingname,b.inputmachinename,b.statisticname,a.registerid_chr,h.emrinpatientid,h.emrinpatientdate,h.hisinpatientid_chr,h.hisinpatientdate,
a.deptid_chr,a.areaid_chr,a.patientid_chr
  from t_opr_bih_register a,
       t_emr_inhospitalmainrec_gxcon b,
       (select distinct(emr_seq) as emr_seqid  from t_emr_inhospitalmainrec_gxid where status=0) c,
       (select distinct(emr_seq) as emr_seqod  from t_emr_inhospitalmainrec_gxod where status=0) d,
       (select distinct(emr_seq) as emr_seqop  from t_emr_inhospitalmainrec_gxop where status=0) e,
       t_opr_bih_leave f,
        t_opr_bih_registerdetail g,
        t_bse_hisemr_relation h
 where a.registerid_chr = b.registerid_chr
   and b.emr_seq = c.emr_seqid(+)
   and b.emr_seq = d.emr_seqod(+)
   and b.emr_seq = e.emr_seqop(+)
   and a.registerid_chr = h.registerid_chr
   and b.status=0 and a.status_int = 1
   and f.status_int = 1
   and a.registerid_chr=g.registerid_chr
   and b.registerid_chr=f.registerid_chr
   and a.pstatus_int=3
    and f.modify_dat>=to_date(?,'yyyy-mm-dd')
    and a.deptid_chr=?" + strWhere + " order by f.modify_dat";
            clsHRPTableService objTabService = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objTabService.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = dateOut.ToShortDateString();
            objDPArr[1].Value = p_strDeptId;

            try
            {
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref p_dtbResult, objDPArr);
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
        /// 
        /// </summary>
        /// <param name="p_strResultXml">返回的结果</param>
        /// <param name="p_intResultRows">记录的数量</param>
        /// <returns>
        ///操作结果。 0：失败。1：成功。
        /// </returns>
        [AutoComplete]
        public long m_lngGetPatientInfo(string p_strInPatientID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                //			 string strSQL = @"select *
                //								from PatientBaseInfo
                //								Where trim(InPatientID) = '"+p_strInPatientID+"'"; 

                //由于添加了病人床头卡信息为了方便读取此处改为以下形式
                #region 屏蔽  modified by tfzhang at 2005年10月15日 11:43:20
                //				string strSQL=null;
                //				if (clsHRPTableService.bytDatabase_Selector==0)
                //					strSQL = @"select PatientBaseInfo.*,case when IBI.STATE is null then'无' when IBI.STATE =0 then '稳定'when IBI.STATE =1 then '慢性'when IBI.STATE =2 then '病重' when IBI.STATE =3 then '病危' end BedClipState
                //									from PatientBaseInfo LEFT OUTER JOIN 
                //															(SELECT * FROM INPAT_BEDINFO where OPENDATE IN 
                //															(SELECT MAX(OPENDATE) FROM INPAT_BEDINFO group by inpatientid,INPATIENTDATE))IBI 
                //															ON (PatientBaseInfo.Inpatientid=IBI.INPATIENTID )
                //									Where rtrim(PatientBaseInfo.InPatientID) = '"+p_strInPatientID+"'"; 
                //
                //				else
                //					strSQL = @"select PatientBaseInfo.*,decode(IBI.STATE,null,'无',0,'稳定',1,'慢性',2,'病重',3,'病危') BedClipState
                //										from PatientBaseInfo LEFT OUTER JOIN 
                //																(SELECT * FROM INPAT_BEDINFO where OPENDATE IN 
                //																(SELECT MAX(OPENDATE) FROM INPAT_BEDINFO group by inpatientid,INPATIENTDATE))IBI 
                //																ON (PatientBaseInfo.Inpatientid=IBI.INPATIENTID )
                //										Where trim(PatientBaseInfo.InPatientID) = '"+p_strInPatientID+"'"; 

                #endregion
                //使用新表 modified by tfzhang at 2005年10月15日 11:43:46
                string strSQL = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    //sqlserver未改 modified by tfzhang at 2005年10月15日 11:46:07
                    strSQL = @"select top 1 k.registerid_chr,
       k.lastname_vchr,
       k.idcard_chr,
       k.married_chr,
       k.birthplace_vchr,
       k.homeaddress_vchr,
       k.sex_chr,
       k.nationality_vchr,
       k.firstname_vchr,
       k.birth_dat,
       k.race_vchr,
       k.nativeplace_vchr,
       k.occupation_vchr,
       k.name_vchr,
       k.homephone_vchr,
       k.officephone_vchr,
       k.insuranceid_vchr,
       k.mobile_chr,
       k.officeaddress_vchr,
       k.employer_vchr,
       k.officepc_vchr,
       k.homepc_chr,
       k.email_vchr,
       k.contactpersonfirstname_vchr,
       k.contactpersonlastname_vchr,
       k.contactpersonaddress_vchr,
       k.contactpersonphone_vchr,
       k.contactpersonpc_chr,
       k.patientrelation_vchr,
       k.firstdate_dat,
       k.isemployee_int,
       k.status_int,
       k.deactivate_dat,
       k.operatorid_chr,
       k.modify_dat,
       k.optimes_int,
       k.govcard_chr,
       k.bloodtype_chr,
       k.ifallergic_int,
       k.allergicdesc_vchr,
       k.difficulty_vchr,
       k.insuredtotalmoney_mny,
       k.insuredpaymoney_mny,
       k.insuredpaytime_int,
       k.insuredpayscale_dec,
       k.bedid_chr,
       k.patientid_chr,
       k.inpatientid_chr,
       k.bedclipstate,
       k.deptid_chr,
       k.areaid_chr,
       k.paytypename_vchr,
       kk.code_chr,
       r.emrinpatientid,
       r.hisinpatientid_chr,
       r.emrinpatientdate,
       r.hisinpatientdate
  from (select t.registerid_chr,
               t.lastname_vchr,
               t.idcard_chr,
               t.married_chr,
               t.birthplace_vchr,
               t.homeaddress_vchr,
               t.sex_chr,
               t.nationality_vchr,
               t.firstname_vchr,
               t.birth_dat,
               t.race_vchr,
               t.nativeplace_vchr,
               t.occupation_vchr,
               t.name_vchr,
               t.homephone_vchr,
               t.officephone_vchr,
               t.insuranceid_vchr,
               t.mobile_chr,
               t.officeaddress_vchr,
               t.employer_vchr,
               t.officepc_vchr,
               t.homepc_chr,
               t.email_vchr,
               t.contactpersonfirstname_vchr,
               t.contactpersonlastname_vchr,
               t.contactpersonaddress_vchr,
               t.contactpersonphone_vchr,
               t.contactpersonpc_chr,
               t.patientrelation_vchr,
               t.firstdate_dat,
               t.isemployee_int,
               t.status_int,
               t.deactivate_dat,
               t.operatorid_chr,
               t.modify_dat,
               t.optimes_int,
               t.govcard_chr,
               t.bloodtype_chr,
               t.ifallergic_int,
               t.allergicdesc_vchr,
               t.difficulty_vchr,
               t.insuredtotalmoney_mny,
               t.insuredpaymoney_mny,
               t.insuredpaytime_int,
               t.insuredpayscale_dec,
               d.bedid_chr,
               d.patientid_chr,
               d.inpatientid_chr,
               case
                 when d.state_int is null then
                  '无'
                 when d.state_int = 1 then
                  '危'
                 when d.state_int = 2 then
                  '急'
                 when d.state_int = 3 then
                  '普通'
               end bedclipstate,
               d.deptid_chr,
               d.areaid_chr,
               pt.paytypename_vchr
          from t_opr_bih_registerdetail t
         inner join t_opr_bih_register d on t.registerid_chr =
                                            d.registerid_chr
                                        and d.status_int > 0
          left outer join t_bse_patientpaytype pt on pt.paytypeid_chr =
                                                     d.paytypeid_chr
                                                 and pt.isusing_num = 1
         where d.inpatientid_chr = ?) k
 inner join t_bse_hisemr_relation r on k.registerid_chr = r.registerid_chr
  left outer join t_bse_bed kk on k.bedid_chr = kk.bedid_chr
 order by r.hisinpatientdate desc";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    strSQL = @"select registerid_chr,
       lastname_vchr,
       idcard_chr,
       married_chr,
       birthplace_vchr,
       homeaddress_vchr,
       sex_chr,
       nationality_vchr,
       firstname_vchr,
       birth_dat,
       race_vchr,
       nativeplace_vchr,
       occupation_vchr,
       name_vchr,
       homephone_vchr,
       officephone_vchr,
       insuranceid_vchr,
       mobile_chr,
       officeaddress_vchr,
       employer_vchr,
       officepc_vchr,
       homepc_chr,
       email_vchr,
       contactpersonfirstname_vchr,
       contactpersonlastname_vchr,
       contactpersonaddress_vchr,
       contactpersonphone_vchr,
       contactpersonpc_chr,
       patientrelation_vchr,
       firstdate_dat,
       isemployee_int,
       status_int,
       deactivate_dat,
       operatorid_chr,
       modify_dat,
       optimes_int,
       govcard_chr,
       bloodtype_chr,
       ifallergic_int,
       allergicdesc_vchr,
       difficulty_vchr,
       insuredtotalmoney_mny,
       insuredpaymoney_mny,
       insuredpaytime_int,
       insuredpayscale_dec,
       bedid_chr,
       patientid_chr,
       bedclipstate,
       deptid_chr,
       areaid_chr,
       paytypename_vchr,
       code_chr,
       emrinpatientid,
       hisinpatientid_chr,
       emrinpatientdate,
       hisinpatientdate
  from (select k.registerid_chr,
               k.lastname_vchr,
               k.idcard_chr,
               k.married_chr,
               k.birthplace_vchr,
               k.homeaddress_vchr,
               k.sex_chr,
               k.nationality_vchr,
               k.firstname_vchr,
               k.birth_dat,
               k.race_vchr,
               k.nativeplace_vchr,
               k.occupation_vchr,
               k.name_vchr,
               k.homephone_vchr,
               k.officephone_vchr,
               k.insuranceid_vchr,
               k.mobile_chr,
               k.officeaddress_vchr,
               k.employer_vchr,
               k.officepc_vchr,
               k.homepc_chr,
               k.email_vchr,
               k.contactpersonfirstname_vchr,
               k.contactpersonlastname_vchr,
               k.contactpersonaddress_vchr,
               k.contactpersonphone_vchr,
               k.contactpersonpc_chr,
               k.patientrelation_vchr,
               k.firstdate_dat,
               k.isemployee_int,
               k.status_int,
               k.deactivate_dat,
               k.operatorid_chr,
               k.modify_dat,
               k.optimes_int,
               k.govcard_chr,
               k.bloodtype_chr,
               k.ifallergic_int,
               k.allergicdesc_vchr,
               k.difficulty_vchr,
               k.insuredtotalmoney_mny,
               k.insuredpaymoney_mny,
               k.insuredpaytime_int,
               k.insuredpayscale_dec,
               k.bedid_chr,
               k.patientid_chr,
               k.bedclipstate,
               k.deptid_chr,
               k.areaid_chr,
               k.paytypename_vchr,
               kk.code_chr,
               r.emrinpatientid,
               r.hisinpatientid_chr,
               r.emrinpatientdate,
               r.hisinpatientdate
          from (select t.registerid_chr,
                       t.lastname_vchr,
                       t.idcard_chr,
                       t.married_chr,
                       t.birthplace_vchr,
                       t.homeaddress_vchr,
                       t.sex_chr,
                       t.nationality_vchr,
                       t.firstname_vchr,
                       t.birth_dat,
                       t.race_vchr,
                       t.nativeplace_vchr,
                       t.occupation_vchr,
                       t.name_vchr,
                       t.homephone_vchr,
                       t.officephone_vchr,
                       t.insuranceid_vchr,
                       t.mobile_chr,
                       t.officeaddress_vchr,
                       t.employer_vchr,
                       t.officepc_vchr,
                       t.homepc_chr,
                       t.email_vchr,
                       t.contactpersonfirstname_vchr,
                       t.contactpersonlastname_vchr,
                       t.contactpersonaddress_vchr,
                       t.contactpersonphone_vchr,
                       t.contactpersonpc_chr,
                       t.patientrelation_vchr,
                       t.firstdate_dat,
                       t.isemployee_int,
                       t.status_int,
                       t.deactivate_dat,
                       t.operatorid_chr,
                       t.modify_dat,
                       t.optimes_int,
                       t.govcard_chr,
                       t.bloodtype_chr,
                       t.ifallergic_int,
                       t.allergicdesc_vchr,
                       t.difficulty_vchr,
                       t.insuredtotalmoney_mny,
                       t.insuredpaymoney_mny,
                       t.insuredpaytime_int,
                       t.insuredpayscale_dec,
                       d.bedid_chr,
                       d.patientid_chr,
                       decode(d.state_int,
                              null,
                              '无',
                              1,
                              '危',
                              2,
                              '急',
                              3,
                              '普通') bedclipstate,
                       d.deptid_chr,
                       d.areaid_chr,
                       pt.paytypename_vchr
                  from t_opr_bih_registerdetail t
                 inner join t_opr_bih_register d on t.registerid_chr =
                                                    d.registerid_chr
                                                and d.status_int > 0
                  left outer join t_bse_patientpaytype pt on pt.paytypeid_chr =
                                                             d.paytypeid_chr
                                                         and pt.isusing_num = 1
                 where d.inpatientid_chr = ?) k
         inner join t_bse_hisemr_relation r on k.registerid_chr =
                                               r.registerid_chr
          left outer join t_bse_bed kk on k.bedid_chr = kk.bedid_chr
         order by r.hisinpatientdate desc) t1
 where rownum = 1";
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                    strSQL = @"select k.registerid_chr,
       k.lastname_vchr,
       k.idcard_chr,
       k.married_chr,
       k.birthplace_vchr,
       k.homeaddress_vchr,
       k.sex_chr,
       k.nationality_vchr,
       k.firstname_vchr,
       k.birth_dat,
       k.race_vchr,
       k.nativeplace_vchr,
       k.occupation_vchr,
       k.name_vchr,
       k.homephone_vchr,
       k.officephone_vchr,
       k.insuranceid_vchr,
       k.mobile_chr,
       k.officeaddress_vchr,
       k.employer_vchr,
       k.officepc_vchr,
       k.homepc_chr,
       k.email_vchr,
       k.contactpersonfirstname_vchr,
       k.contactpersonlastname_vchr,
       k.contactpersonaddress_vchr,
       k.contactpersonphone_vchr,
       k.contactpersonpc_chr,
       k.patientrelation_vchr,
       k.firstdate_dat,
       k.isemployee_int,
       k.status_int,
       k.deactivate_dat,
       k.operatorid_chr,
       k.modify_dat,
       k.optimes_int,
       k.govcard_chr,
       k.bloodtype_chr,
       k.ifallergic_int,
       k.allergicdesc_vchr,
       k.difficulty_vchr,
       k.insuredtotalmoney_mny,
       k.insuredpaymoney_mny,
       k.insuredpaytime_int,
       k.insuredpayscale_dec,
       k.bedid_chr,
       k.patientid_chr,
       k.bedclipstate,
       k.deptid_chr,
       k.areaid_chr,
       k.paytypename_vchr,
       kk.code_chr,
       r.emrinpatientid,
       r.hisinpatientid_chr,
       r.emrinpatientdate,
       r.hisinpatientdate
  from (select t.registerid_chr,
               t.lastname_vchr,
               t.idcard_chr,
               t.married_chr,
               t.birthplace_vchr,
               t.homeaddress_vchr,
               t.sex_chr,
               t.nationality_vchr,
               t.firstname_vchr,
               t.birth_dat,
               t.race_vchr,
               t.nativeplace_vchr,
               t.occupation_vchr,
               t.name_vchr,
               t.homephone_vchr,
               t.officephone_vchr,
               t.insuranceid_vchr,
               t.mobile_chr,
               t.officeaddress_vchr,
               t.employer_vchr,
               t.officepc_vchr,
               t.homepc_chr,
               t.email_vchr,
               t.contactpersonfirstname_vchr,
               t.contactpersonlastname_vchr,
               t.contactpersonaddress_vchr,
               t.contactpersonphone_vchr,
               t.contactpersonpc_chr,
               t.patientrelation_vchr,
               t.firstdate_dat,
               t.isemployee_int,
               t.status_int,
               t.deactivate_dat,
               t.operatorid_chr,
               t.modify_dat,
               t.optimes_int,
               t.govcard_chr,
               t.bloodtype_chr,
               t.ifallergic_int,
               t.allergicdesc_vchr,
               t.difficulty_vchr,
               t.insuredtotalmoney_mny,
               t.insuredpaymoney_mny,
               t.insuredpaytime_int,
               t.insuredpayscale_dec,
               d.bedid_chr,
               d.patientid_chr,
               case
                 when d.state_int is null then
                  '无'
                 when d.state_int = 1 then
                  '危'
                 when d.state_int = 2 then
                  '急'
                 when d.state_int = 3 then
                  '普通'
               end bedclipstate,
               d.deptid_chr,
               d.areaid_chr,
               pt.paytypename_vchr
          from t_opr_bih_registerdetail t
         inner join t_opr_bih_register d on t.registerid_chr =
                                            d.registerid_chr
                                        and d.status_int > 0
          left outer join t_bse_patientpaytype pt on pt.paytypeid_chr =
                                                     d.paytypeid_chr
                                                 and pt.isusing_num = 1
         where d.inpatientid_chr = ?) k
 inner join t_bse_hisemr_relation r on k.registerid_chr = r.registerid_chr
  left outer join t_bse_bed kk on k.bedid_chr = kk.bedid_chr
 order by r.hisinpatientdate desc fetch first 1 row only";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();


                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }
        /// <summary>
        /// 根据入院登记号获取病人信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfoByRegisterID(string p_strRegisterID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                //使用新表 modified by tfzhang at 2005年10月15日 11:43:46
                string strSQL = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strSQL = @"select k.registerid_chr,
       k.lastname_vchr,
       k.idcard_chr,
       k.married_chr,
       k.birthplace_vchr,
       k.homeaddress_vchr,
       k.sex_chr,
       k.nationality_vchr,
       k.firstname_vchr,
       k.birth_dat,
       k.race_vchr,
       k.nativeplace_vchr,
       k.occupation_vchr,
       k.name_vchr,
       k.homephone_vchr,
       k.officephone_vchr,
       k.insuranceid_vchr,
       k.mobile_chr,
       k.officeaddress_vchr,
       k.employer_vchr,
       k.officepc_vchr,
       k.homepc_chr,
       k.email_vchr,
       k.contactpersonfirstname_vchr,
       k.contactpersonlastname_vchr,
       k.contactpersonaddress_vchr,
       k.contactpersonphone_vchr,
       k.contactpersonpc_chr,
       k.patientrelation_vchr,
       k.firstdate_dat,
       k.isemployee_int,
       k.status_int,
       k.deactivate_dat,
       k.operatorid_chr,
       k.modify_dat,
       k.optimes_int,
       k.govcard_chr,
       k.bloodtype_chr,
       k.ifallergic_int,
       k.allergicdesc_vchr,
       k.difficulty_vchr,
       k.insuredtotalmoney_mny,
       k.insuredpaymoney_mny,
       k.insuredpaytime_int,
       k.insuredpayscale_dec,
       k.inpatientid_chr,
       k.patientid_chr,
       k.bedid_chr,
       k.bedclipstate,
       k.inpatient_dat,
       k.deptid_chr,
       k.areaid_chr,
       k.paytypename_vchr,
       k.extendid_vchr,
       k.residenceplace_vchr,
       kk.code_chr,
       r.emrinpatientid,
       r.hisinpatientid_chr,
       r.emrinpatientdate,
       r.hisinpatientdate
  from (select d.registerid_chr,
               d.lastname_vchr,
               d.idcard_chr,
               d.married_chr,
               d.birthplace_vchr,
               d.homeaddress_vchr,
               d.sex_chr,
               d.nationality_vchr,
               d.firstname_vchr,
               d.birth_dat,
               d.race_vchr,
               d.nativeplace_vchr,
               d.occupation_vchr,
               d.name_vchr,
               d.homephone_vchr,
               d.officephone_vchr,
               d.insuranceid_vchr,
               d.mobile_chr,
               d.officeaddress_vchr,
               d.employer_vchr,
               d.officepc_vchr,
               d.homepc_chr,
               d.email_vchr,
               d.contactpersonfirstname_vchr,
               d.contactpersonlastname_vchr,
               d.contactpersonaddress_vchr,
               d.contactpersonphone_vchr,
               d.contactpersonpc_chr,
               d.patientrelation_vchr,
               d.firstdate_dat,
               d.isemployee_int,
               d.status_int,
               d.deactivate_dat,
               d.operatorid_chr,
               d.modify_dat,
               d.optimes_int,
               d.govcard_chr,
               d.bloodtype_chr,
               d.ifallergic_int,
               d.allergicdesc_vchr,
               d.difficulty_vchr,
               d.insuredtotalmoney_mny,
               d.insuredpaymoney_mny,
               d.insuredpaytime_int,
               d.insuredpayscale_dec,
               d.residenceplace_vchr,
               t.inpatientid_chr,
               t.patientid_chr,
               t.bedid_chr,
               t.extendid_vchr,
               case
                 when t.state_int is null then
                  '无'
                 when d.state_int = 1 then
                  '危'
                 when d.state_int = 2 then
                  '急'
                 when d.state_int = 3 then
                  '普通'
               end bedclipstate,
               t.inpatient_dat,
               t.deptid_chr,
               t.areaid_chr,
               pt.paytypename_vchr
          from t_opr_bih_register t
         inner join t_opr_bih_registerdetail d on t.registerid_chr =
                                                  d.registerid_chr
          left outer join t_bse_patientpaytype pt on pt.paytypeid_chr =
                                                     t.paytypeid_chr
                                                 and pt.isusing_num = 1
         where t.registerid_chr = ?
           and t.status_int = 1) k
 inner join t_bse_hisemr_relation r on k.registerid_chr = r.registerid_chr
  left outer join t_bse_bed kk on k.bedid_chr = kk.bedid_chr";

                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    strSQL = @"select k.registerid_chr,
       k.lastname_vchr,
       k.idcard_chr,
       k.married_chr,
       k.birthplace_vchr,
       k.homeaddress_vchr,
       k.sex_chr,
       k.nationality_vchr,
       k.firstname_vchr,
       k.birth_dat,
       k.race_vchr,
       k.nativeplace_vchr,
       k.occupation_vchr,
       k.name_vchr,
       k.homephone_vchr,
       k.officephone_vchr,
       k.insuranceid_vchr,
       k.mobile_chr,
       k.officeaddress_vchr,
       k.employer_vchr,
       k.officepc_vchr,
       k.homepc_chr,
       k.email_vchr,
       k.contactpersonfirstname_vchr,
       k.contactpersonlastname_vchr,
       k.contactpersonaddress_vchr,
       k.contactpersonphone_vchr,
       k.contactpersonpc_chr,
       k.patientrelation_vchr,
       k.firstdate_dat,
       k.isemployee_int,
       k.status_int,
       k.deactivate_dat,
       k.operatorid_chr,
       k.modify_dat,
       k.optimes_int,
       k.govcard_chr,
       k.bloodtype_chr,
       k.ifallergic_int,
       k.allergicdesc_vchr,
       k.difficulty_vchr,
       k.insuredtotalmoney_mny,
       k.insuredpaymoney_mny,
       k.insuredpaytime_int,
       k.insuredpayscale_dec,
       k.inpatientid_chr,
       k.patientid_chr,
       k.bedid_chr,
       k.bedclipstate,
       k.inpatient_dat,
       k.deptid_chr,
       k.areaid_chr,
       k.paytypename_vchr,
       k.extendid_vchr,
       k.residenceplace_vchr,
       kk.code_chr,
       r.emrinpatientid,
       r.hisinpatientid_chr,
       r.emrinpatientdate,
       r.hisinpatientdate
  from (select d.registerid_chr,
               d.lastname_vchr,
               d.idcard_chr,
               d.married_chr,
               d.birthplace_vchr,
               d.homeaddress_vchr,
               d.sex_chr,
               d.nationality_vchr,
               d.firstname_vchr,
               d.birth_dat,
               d.race_vchr,
               d.nativeplace_vchr,
               d.occupation_vchr,
               d.name_vchr,
               d.homephone_vchr,
               d.officephone_vchr,
               d.insuranceid_vchr,
               d.mobile_chr,
               d.officeaddress_vchr,
               d.employer_vchr,
               d.officepc_vchr,
               d.homepc_chr,
               d.email_vchr,
               d.contactpersonfirstname_vchr,
               d.contactpersonlastname_vchr,
               d.contactpersonaddress_vchr,
               d.contactpersonphone_vchr,
               d.contactpersonpc_chr,
               d.patientrelation_vchr,
               d.firstdate_dat,
               d.isemployee_int,
               d.status_int,
               d.deactivate_dat,
               d.operatorid_chr,
               d.modify_dat,
               d.optimes_int,
               d.govcard_chr,
               d.bloodtype_chr,
               d.ifallergic_int,
               d.allergicdesc_vchr,
               d.difficulty_vchr,
               d.insuredtotalmoney_mny,
               d.insuredpaymoney_mny,
               d.insuredpaytime_int,
               d.insuredpayscale_dec,
               d.residenceplace_vchr,
               t.inpatientid_chr,
               t.patientid_chr,
               t.bedid_chr,
               decode(t.state_int, null, '无', 1, '危', 2, '急', 3, '普通') bedclipstate,
               t.inpatient_dat,
               t.deptid_chr,
               t.areaid_chr,
               t.extendid_vchr,
               pt.paytypename_vchr
          from t_opr_bih_register t
         inner join t_opr_bih_registerdetail d on t.registerid_chr =
                                                  d.registerid_chr
          left outer join t_bse_patientpaytype pt on pt.paytypeid_chr =
                                                     t.paytypeid_chr
                                                 and pt.isusing_num = 1
         where t.registerid_chr = ?
           and t.status_int = 1) k
 inner join t_bse_hisemr_relation r on k.registerid_chr = r.registerid_chr
  left outer join t_bse_bed kk on k.bedid_chr = kk.bedid_chr";
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                    strSQL = @"select k.registerid_chr,
       k.lastname_vchr,
       k.idcard_chr,
       k.married_chr,
       k.birthplace_vchr,
       k.homeaddress_vchr,
       k.sex_chr,
       k.nationality_vchr,
       k.firstname_vchr,
       k.birth_dat,
       k.race_vchr,
       k.nativeplace_vchr,
       k.occupation_vchr,
       k.name_vchr,
       k.homephone_vchr,
       k.officephone_vchr,
       k.insuranceid_vchr,
       k.mobile_chr,
       k.officeaddress_vchr,
       k.employer_vchr,
       k.officepc_vchr,
       k.homepc_chr,
       k.email_vchr,
       k.contactpersonfirstname_vchr,
       k.contactpersonlastname_vchr,
       k.contactpersonaddress_vchr,
       k.contactpersonphone_vchr,
       k.contactpersonpc_chr,
       k.patientrelation_vchr,
       k.firstdate_dat,
       k.isemployee_int,
       k.status_int,
       k.deactivate_dat,
       k.operatorid_chr,
       k.modify_dat,
       k.optimes_int,
       k.govcard_chr,
       k.bloodtype_chr,
       k.ifallergic_int,
       k.allergicdesc_vchr,
       k.difficulty_vchr,
       k.insuredtotalmoney_mny,
       k.insuredpaymoney_mny,
       k.insuredpaytime_int,
       k.insuredpayscale_dec,
       k.inpatientid_chr,
       k.patientid_chr,
       k.bedid_chr,
       k.bedclipstate,
       k.inpatient_dat,
       k.deptid_chr,
       k.areaid_chr,
       k.paytypename_vchr,
       k.extendid_vchr,
       k.residenceplace_vchr,
       kk.code_chr,
       r.emrinpatientid,
       r.hisinpatientid_chr,
       r.emrinpatientdate,
       r.hisinpatientdate
  from (select d.registerid_chr,
               d.lastname_vchr,
               d.idcard_chr,
               d.married_chr,
               d.birthplace_vchr,
               d.homeaddress_vchr,
               d.sex_chr,
               d.nationality_vchr,
               d.firstname_vchr,
               d.birth_dat,
               d.race_vchr,
               d.nativeplace_vchr,
               d.occupation_vchr,
               d.name_vchr,
               d.homephone_vchr,
               d.officephone_vchr,
               d.insuranceid_vchr,
               d.mobile_chr,
               d.officeaddress_vchr,
               d.employer_vchr,
               d.officepc_vchr,
               d.homepc_chr,
               d.email_vchr,
               d.contactpersonfirstname_vchr,
               d.contactpersonlastname_vchr,
               d.contactpersonaddress_vchr,
               d.contactpersonphone_vchr,
               d.contactpersonpc_chr,
               d.patientrelation_vchr,
               d.firstdate_dat,
               d.isemployee_int,
               d.status_int,
               d.deactivate_dat,
               d.operatorid_chr,
               d.modify_dat,
               d.optimes_int,
               d.govcard_chr,
               d.bloodtype_chr,
               d.ifallergic_int,
               d.allergicdesc_vchr,
               d.difficulty_vchr,
               d.insuredtotalmoney_mny,
               d.insuredpaymoney_mny,
               d.insuredpaytime_int,
               d.insuredpayscale_dec,
               d.residenceplace_vchr,
               t.inpatientid_chr,
               t.patientid_chr,
               t.bedid_chr,
               t.extendid_vchr,
               case
                 when t.state_int is null then
                  '无'
                 when d.state_int = 1 then
                  '危'
                 when d.state_int = 2 then
                  '急'
                 when d.state_int = 3 then
                  '普通'
               end bedclipstate,
               t.inpatient_dat,
               t.deptid_chr,
               t.areaid_chr,
               pt.paytypename_vchr
          from t_opr_bih_register t
         inner join t_opr_bih_registerdetail d on t.registerid_chr =
                                                  d.registerid_chr
          left outer join t_bse_patientpaytype pt on pt.paytypeid_chr =
                                                     t.paytypeid_chr
                                                 and pt.isusing_num = 1
         where t.registerid_chr = ?
           and t.status_int = 1) k
 inner join t_bse_hisemr_relation r on k.registerid_chr = r.registerid_chr
  left outer join t_bse_bed kk on k.bedid_chr = kk.bedid_chr";


                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID.Trim();


                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetPatientInfoOld(string p_strInPatientID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                //			 string strSQL = @"select *
                //								from PatientBaseInfo
                //								Where trim(InPatientID) = '"+p_strInPatientID+"'"; 

                //由于添加了病人床头卡信息为了方便读取此处改为以下形式
                #region 读取旧表，暂供电子病历临床检索使用
                string strSQL = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strSQL = @"select patientbaseinfo.inpatientid,
       patientbaseinfo.patientid,
       patientbaseinfo.firstname,
       patientbaseinfo.lastname,
       patientbaseinfo.idcard,
       patientbaseinfo.sex,
       patientbaseinfo.married,
       patientbaseinfo.birth,
       patientbaseinfo.chargecategory,
       patientbaseinfo.paymentpercent,
       patientbaseinfo.homeplace,
       patientbaseinfo.nationality,
       patientbaseinfo.nation,
       patientbaseinfo.nativeplace,
       patientbaseinfo.occupation,
       patientbaseinfo.officephone,
       patientbaseinfo.homephone,
       patientbaseinfo.mobile,
       patientbaseinfo.officeaddress,
       patientbaseinfo.homeaddress,
       patientbaseinfo.job,
       patientbaseinfo.officepc,
       patientbaseinfo.homepc,
       patientbaseinfo.email,
       patientbaseinfo.linkmanfirstname,
       patientbaseinfo.linkmanlastname,
       patientbaseinfo.linkmanaddress,
       patientbaseinfo.linkmanphone,
       patientbaseinfo.linkmanpc,
       patientbaseinfo.patientrelation,
       patientbaseinfo.firstdate,
       patientbaseinfo.isemployee,
       patientbaseinfo.status,
       patientbaseinfo.deactivated_date,
       patientbaseinfo.de_employeeid,
       patientbaseinfo.times,
       patientbaseinfo.hic_no,
       patientbaseinfo.book_no,
       patientbaseinfo.vip_code,
       patientbaseinfo.office_name,
       patientbaseinfo.office_district,
       patientbaseinfo.office_street,
       patientbaseinfo.linkman_district,
       patientbaseinfo.linkman_street,
       patientbaseinfo.home_district,
       patientbaseinfo.home_street,
       patientbaseinfo.temp_district,
       patientbaseinfo.temp_street,
       patientbaseinfo.temp_tel,
       patientbaseinfo.temp_zipcode,
       patientbaseinfo.insurance,
       patientbaseinfo.admiss_diag_str,
       patientbaseinfo.admiss_status,
       patientbaseinfo.visit_type,
       case
         when ibi.state is null then
          '无'
         when ibi.state = 0 then
          '稳定'
         when ibi.state = 1 then
          '慢性'
         when ibi.state = 2 then
          '病重'
         when ibi.state = 3 then
          '病危'
       end bedclipstate
  from patientbaseinfo
  left outer join (select inpatientid, state
                     from inpat_bedinfo
                    where opendate in
                          (select max(opendate)
                             from inpat_bedinfo
                            group by inpatientid, inpatientdate)) ibi on (patientbaseinfo.inpatientid =
                                                                         ibi.inpatientid)
 where patientbaseinfo.inpatientid = ?";

                else
                    strSQL = @"select patientbaseinfo.inpatientid,
       patientbaseinfo.patientid,
       patientbaseinfo.firstname,
       patientbaseinfo.lastname,
       patientbaseinfo.idcard,
       patientbaseinfo.sex,
       patientbaseinfo.married,
       patientbaseinfo.birth,
       patientbaseinfo.chargecategory,
       patientbaseinfo.paymentpercent,
       patientbaseinfo.homeplace,
       patientbaseinfo.nationality,
       patientbaseinfo.nation,
       patientbaseinfo.nativeplace,
       patientbaseinfo.occupation,
       patientbaseinfo.officephone,
       patientbaseinfo.homephone,
       patientbaseinfo.mobile,
       patientbaseinfo.officeaddress,
       patientbaseinfo.homeaddress,
       patientbaseinfo.job,
       patientbaseinfo.officepc,
       patientbaseinfo.homepc,
       patientbaseinfo.email,
       patientbaseinfo.linkmanfirstname,
       patientbaseinfo.linkmanlastname,
       patientbaseinfo.linkmanaddress,
       patientbaseinfo.linkmanphone,
       patientbaseinfo.linkmanpc,
       patientbaseinfo.patientrelation,
       patientbaseinfo.firstdate,
       patientbaseinfo.isemployee,
       patientbaseinfo.status,
       patientbaseinfo.deactivated_date,
       patientbaseinfo.de_employeeid,
       patientbaseinfo.times,
       patientbaseinfo.hic_no,
       patientbaseinfo.book_no,
       patientbaseinfo.vip_code,
       patientbaseinfo.office_name,
       patientbaseinfo.office_district,
       patientbaseinfo.office_street,
       patientbaseinfo.linkman_district,
       patientbaseinfo.linkman_street,
       patientbaseinfo.home_district,
       patientbaseinfo.home_street,
       patientbaseinfo.temp_district,
       patientbaseinfo.temp_street,
       patientbaseinfo.temp_tel,
       patientbaseinfo.temp_zipcode,
       patientbaseinfo.insurance,
       patientbaseinfo.admiss_diag_str,
       patientbaseinfo.admiss_status,
       patientbaseinfo.visit_type,
       decode(ibi.state,
              null,
              '无',
              0,
              '稳定',
              1,
              '慢性',
              2,
              '病重',
              3,
              '病危') bedclipstate
  from patientbaseinfo
  left outer join (select inpatientid, state
                     from inpat_bedinfo
                    where opendate in
                          (select max(opendate)
                             from inpat_bedinfo
                            group by inpatientid, inpatientdate)) ibi on (patientbaseinfo.inpatientid =
                                                                         ibi.inpatientid)
 where trim(patientbaseinfo.inpatientid) = ?";

                #endregion

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInitInBedInfo(string p_strInPatientID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {

                //				string strSQL = @"SELECT InDeptInfo.InPatientDate, InPatientDateInfo.InPatientEndDate, InDeptInfo.InDeptID, 
                //										InDeptInfo.Area_ID, InDeptInfo.Room_ID, InDeptInfo.Bed_ID, InDeptInfo.ModifyDate, 
                //										InDeptInfo.InBedEndDate
                //									FROM InDeptInfo INNER JOIN
                //										InPatientDateInfo ON InDeptInfo.InPatientID = InPatientDateInfo.InPatientID AND 
                //										InDeptInfo.InPatientDate = InPatientDateInfo.InPatientDate
                //									WHERE (InDeptInfo.InPatientID = '"+p_strInPatientID+@"' and InDeptInfo.Bed_Status is null)
                //									ORDER BY InDeptInfo.InPatientDate, InDeptInfo.ModifyDate";

                //使用新表 modified by tfzhang at 2005年10月15日 11:16:42
                string strSQL = @"select  
									d.inpatient_dat  inpatientdate,
									bh.modify_dat  inpatientenddate,
									d.deptid_chr  indeptid,
									d.areaid_chr  area_id,
									d.areaid_chr  room_id,
									d.bedid_chr  bed_id,
									d.modify_dat  modifydate,
									d.modify_dat  inbedenddate,
									t.shortno_chr  deptshortno,
									tt.shortno_chr  areashortno,
                                    d.registerid_chr,
                                    rehis.hisinpatientid_chr hisinpatientid,
                                    rehis.hisinpatientdate hisindate,
                                    rehis.emrinpatientid, 
                                    rehis.emrinpatientdate
								from t_opr_bih_register d inner join t_bse_deptdesc t on d.deptid_chr=t.deptid_chr
                                inner join t_bse_hisemr_relation rehis on rehis.registerid_chr = d.registerid_chr
								left join t_bse_deptdesc tt on d.areaid_chr=tt.deptid_chr
								left outer join t_opr_bih_leave bh on d.registerid_chr = bh.registerid_chr
																	and bh.status_int = 1
								where d.pstatus_int <> 0 and d.status_int = 1
								and rehis.hisinpatientid_chr = ? order by inpatientdate ,modifydate";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 从T_BSE_HISEMR_RELATION获取EMR住院号及EMR住院日期
        /// </summary>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_strEMRInPatientID">EMR住院号</param>
        /// <param name="p_dtmEMRInDate">EMR住院日期</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEMRInPatientInfo(string p_strRegisterID,
            out string p_strEMRInPatientID,
            out DateTime p_dtmEMRInDate)
        {
            p_strEMRInPatientID = string.Empty;
            p_dtmEMRInDate = new DateTime(1900, 1, 1);

            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                string strSQL = @"select t.emrinpatientid, t.emrinpatientdate
                              from t_bse_hisemr_relation t
                             where t.registerid_chr = ?";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID.Trim();

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count == 1)
                {
                    p_strEMRInPatientID = dtResult.Rows[0]["emrinpatientid"].ToString();
                    p_dtmEMRInDate = Convert.ToDateTime(dtResult.Rows[0]["emrinpatientdate"]);
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
        /// 
        /// </summary>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInitInBedInfoOld(string p_strInPatientID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                #region 使用旧表，暂供电子病历临床检索使用
                string strSQL = @"select indeptinfo.inpatientdate, inpatientdateinfo.inpatientenddate, indeptinfo.indeptid, 
										indeptinfo.area_id, indeptinfo.room_id, indeptinfo.bed_id, indeptinfo.modifydate, 
										indeptinfo.inbedenddate
									from indeptinfo inner join
										inpatientdateinfo on indeptinfo.inpatientid = inpatientdateinfo.inpatientid and 
										indeptinfo.inpatientdate = inpatientdateinfo.inpatientdate
									where (indeptinfo.inpatientid = ? and indeptinfo.bed_status is null)
									order by indeptinfo.inpatientdate , indeptinfo.modifydate";
                #endregion

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 返回当前正在住院的病人，现在改为返回所有的病人（包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllInPatient(ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                //返回当前正在住院的病人

                //			string strSQL = @"select distinct i.InPatientID,p.FirstName,p.Birth
                //								from InPatientDateInfo i,PatientBaseInfo p
                //								where i.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" and i.InPatientID =p.InPatientID and p.Status=0";

                //返回所有的病人，Jacky-2003-7-29
                //				string strSQL = @"select distinct i.InPatientID,p.FirstName,p.Birth
                //									from InPatientDateInfo i,PatientBaseInfo p
                //									where i.InPatientID =p.InPatientID and i.inpatientenddate<>"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat();

                string strSQL = @"select distinct a.inpatientid_chr inpatientid,
                b.lastname_vchr firstname,
                b.birth_dat birth,
                c.code_chr bed_id,
                a.registerid_chr,
                rehis.emrinpatientid,
                rehis.emrinpatientdate,
                rehis.hisinpatientid_chr,
                rehis.hisinpatientdate
  from t_opr_bih_register a
 inner join t_opr_bih_registerdetail b on a.registerid_chr =
                                          b.registerid_chr
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                           a.registerid_chr
  left outer join t_bse_bed c on a.bedid_chr = c.bedid_chr
 where a.status_int > 0";

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 模糊查询当前正在住院的病人，现在改为模糊查询所有的病人（包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientLikeString"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllInPatientLikeQuery(string p_strPatientLikeString, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                string strSQL = "";
                //模糊查询当前正在住院的病人

                //			try
                //			{
                //				long.Parse(p_strPatientLikeString);
                //				strSQL = @"select distinct i.InPatientID,p.FirstName,p.Birth
                //								from InPatientDateInfo i,PatientBaseInfo p
                //								where i.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" and i.InPatientID =p.InPatientID and p.Status=0 and i.InPatientID Like '"+p_strPatientLikeString+"%'";
                //			}
                //			catch
                //			{
                //				strSQL = @"select distinct i.InPatientID,p.FirstName,p.Birth
                //								from InPatientDateInfo i,PatientBaseInfo p
                //								where i.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" and i.InPatientID =p.InPatientID and p.Status=0 and p.FirstName Like '"+p_strPatientLikeString+"%'";
                //			}

                //模糊查询所有的病人（包括已经出院的病人）

                //				try
                //				{
                //					long.Parse(p_strPatientLikeString);
                //					strSQL = @"select distinct i.InPatientID,p.FirstName,p.Birth
                //									from InPatientDateInfo i,PatientBaseInfo p
                //									where i.InPatientID =p.InPatientID  and i.InPatientID Like '"+p_strPatientLikeString+"%'";
                //				}
                //				catch
                //				{
                //					select distinct i.InPatientID,p.FirstName,p.Birth
                //									from InPatientDateInfo i,PatientBaseInfo p
                //									where i.InPatientID =p.InPatientID  and p.FirstName Like '"+p_strPatientLikeString+"%'";
                //				}
                //病人住院号可能包含字母，故不能直接用long.Parse(..)判断
                strSQL = @"select distinct a.inpatientid_chr inpatientid,
                b.lastname_vchr firstname,
                b.birth_dat birth,
                c.code_chr bed_id,
                a.registerid_chr,
                rehis.emrinpatientid,
                rehis.emrinpatientdate,
                rehis.hisinpatientid_chr,
                rehis.hisinpatientdate
  from t_opr_bih_register a
 inner join t_opr_bih_registerdetail b on a.registerid_chr =
                                          b.registerid_chr
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                           a.registerid_chr
  left outer join t_bse_bed c on a.bedid_chr = c.bedid_chr
 where a.status_int > 0
   and a.inpatientid_chr like '" + p_strPatientLikeString + @"%'
    or b.lastname_vchr like '" + p_strPatientLikeString + "%'";

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 模糊查询所有病人,包括已出院的。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientLikeString"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllPatientLikeQuery(string p_strPatientLikeString, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                string strSQL = "";
                //				try
                //				{
                //					long.Parse(p_strPatientLikeString);
                //					strSQL = @"select distinct p.InPatientID,p.FirstName,p.Birth
                //									from InPatientDateInfo i, PatientBaseInfo p
                //									where i.InPatientID=p.InPatientID and p.Status=0 and p.InPatientID Like '"+p_strPatientLikeString+"%'";
                //				}
                //				catch
                //				{
                //					strSQL = @"select distinct p.InPatientID,p.FirstName,p.Birth
                //									from InPatientDateInfo i, PatientBaseInfo p
                //									where i.InPatientID=p.InPatientID and p.Status=0 and p.FirstName Like '"+p_strPatientLikeString+"%'";
                //				}

                //病人住院号可能包含字母，故不能直接用long.Parse(..)判断
                strSQL = @"select distinct a.inpatientid_chr inpatientid,
                            b.lastname_vchr firstname,
                            b.birth_dat birth,
                            a.registerid_chr,
                            rehis.emrinpatientid,
                            rehis.emrinpatientdate,
                            rehis.hisinpatientid_chr,
                            rehis.hisinpatientdate
              from t_opr_bih_register a, t_opr_bih_registerdetail b, t_bse_hisemr_relation rehis
             where a.registerid_chr = b.registerid_chr
               and rehis.registerid_chr = a.registerid_chr
               and a.status_int > 0
               and a.inpatientid_chr like '" + p_strPatientLikeString + @"%'
                or b.lastname_vchr like '" + p_strPatientLikeString + "%'";

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }


        /// <summary>
        /// 获取当前正在住院的病人，现在改为获取所有的病人（包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByDeptID(string p_strDeptID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();

            try
            {

                //			string strSQL = @"select distinct IDI.InPatientID ,p.FirstName,p.Birth
                //								from InDeptInfo as IDI,InPatientDateInfo as IPDI,PatientBaseInfo p
                //								where 
                //								IDI.InPatientID = IPDI.InPatientID  and p.InPatientID = IPDI.InPatientID and p.Status=0
                //								and IDI.InPatientDate = IPDI.InPatientDate
                //								and IDI.InDeptID = '"+p_strDeptID+@"'
                //								and IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"";

                //				string strSQL = @"select distinct IDI.InPatientID ,p.FirstName,p.Birth
                //									from InDeptInfo IDI,InPatientDateInfo IPDI,PatientBaseInfo p
                //									where 
                //									IDI.InPatientID = IPDI.InPatientID  and p.InPatientID = IPDI.InPatientID 
                //									and IDI.InPatientDate = IPDI.InPatientDate
                //									and IDI.InDeptID = '"+p_strDeptID+@"'
                //									";

                string strSQL = @"select distinct a.inpatientid_chr inpatientid,
                                                b.lastname_vchr firstname,
                                                b.birth_dat birth,
                                                a.registerid_chr,
                                                rehis.emrinpatientid,
                                                rehis.emrinpatientdate,
                                                rehis.hisinpatientid_chr,
                                                rehis.hisinpatientdate
                                  from t_opr_bih_register    a,
                                       t_opr_bih_registerdetail         b,
                                       t_opr_bih_transfer    c,
                                       t_bse_hisemr_relation rehis
                                 where a.registerid_chr = b.registerid_chr
                                   and rehis.registerid_chr = a.registerid_chr
                                   and a.status_int > 0
                                   and (c.sourcedeptid_chr = ?
                                    or c.targetdeptid_chr = ?)
                                   and c.type_int <> 1";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDeptID;
                objDPArr[1].Value = p_strDeptID;

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 获取当前正在住院的病人，现在改为获取所有的病人（包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByAreaID(string p_strAreaID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                #region RegionName
                //			string strSQL = @"select distinct IDI.InPatientID ,p.FirstName
                //								from InDeptInfo as IDI,InPatientDateInfo as IPDI,PatientBaseInfo p
                //								where 
                //								IDI.InPatientID = IPDI.InPatientID  and p.InPatientID = IPDI.InPatientID and p.Status=0
                //								and IDI.InPatientDate = IPDI.InPatientDate
                //								and IDI.Area_ID = '"+p_strAreaID+@"'
                //								and IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"";

                //				string strSQL = @"select distinct IDI.InPatientID ,p.FirstName,IDI.Bed_ID
                //									from InDeptInfo IDI,InPatientDateInfo IPDI,PatientBaseInfo p
                //									where 
                //									IDI.InPatientID = IPDI.InPatientID  and p.InPatientID = IPDI.InPatientID 
                //									and IDI.InPatientDate = IPDI.InPatientDate
                //									and IDI.Area_ID = '"+p_strAreaID+@"' and IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" and  IDI.Bed_Status is null
                //									order by IDI.Bed_ID
                //									";
                #endregion

                //读取新表 modified by tfzhang at 2005年10月14日 17:29:08
                string strSQL = string.Empty;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQL = @"select dd.inpatientid_chr inpatientid,
                                   p.lastname_vchr firstname,
                                   dd.code_chr bed_id,
                                   dd.deptid_chr deptid,
                                   dd.areaid_chr areaid,
                                   dd.patientid_chr,
                                   right('0000' + code_chr, 4) sequenceno, 
                                   dd.registerid_chr, 
                                   dd.emrinpatientid, 
                                   dd.emrinpatientdate, 
                                   dd.hisinpatientid_chr, 
                                   dd.hisinpatientdate
                              from (select t.code_chr,
                                           d.patientid_chr,
                                           d.inpatientid_chr,
                                           d.deptid_chr,
                                           d.areaid_chr,
                                           d.registerid_chr,
                                           rehis.emrinpatientid,
                                           rehis.emrinpatientdate,
                                           rehis.hisinpatientid_chr,
                                           rehis.hisinpatientdate
                                      from t_bse_bed t
                                     inner join t_opr_bih_register d on t.areaid_chr = d.areaid_chr
                                                                       
                                                                    and t.bedid_chr = d.bedid_chr
                                                                    and d.pstatus_int <> 3
                                                                    and d.pstatus_int <> 0 and d.status_int = 1
                                     inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                               d.registerid_chr
                                    
                                     where t.status_int <> 5
                                       and t.areaid_chr = ?) dd
                             inner join t_opr_bih_registerdetail p on dd.registerid_chr = p.registerid_chr
                             order by sequenceno";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQL = @"select dd.inpatientid_chr inpatientid,
                                   p.lastname_vchr firstname,
                                   dd.code_chr bed_id,
                                   dd.deptid_chr deptid,
                                   dd.areaid_chr areaid,
                                   dd.patientid_chr,
                                   lpad(dd.code_chr, 4, '0') sequenceno,
                                   dd.registerid_chr,
                                   dd.emrinpatientid,
                                   dd.emrinpatientdate,
                                   dd.hisinpatientid_chr,
                                   dd.hisinpatientdate
                              from (select t.code_chr,
                                           d.patientid_chr,
                                           d.inpatientid_chr,
                                           d.deptid_chr,
                                           d.areaid_chr,
                                           d.registerid_chr,
                                           rehis.emrinpatientid,
                                           rehis.emrinpatientdate,
                                           rehis.hisinpatientid_chr,
                                           rehis.hisinpatientdate
                                      from t_bse_bed t
                                     inner join t_opr_bih_register d on t.areaid_chr = d.areaid_chr
                                                                       
                                                                    and t.bedid_chr = d.bedid_chr
                                                                    and d.pstatus_int <> 3
                                                                    and d.pstatus_int <> 0 and d.status_int = 1
                                     inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                               d.registerid_chr
                                    
                                     where t.status_int <> 5
                                       and t.areaid_chr = ?) dd
                             inner join t_opr_bih_registerdetail p on dd.registerid_chr = p.registerid_chr
                             order by sequenceno";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSQL = @"select dd.inpatientid_chr inpatientid,
                                   p.lastname_vchr firstname,
                                   dd.code_chr bed_id,
                                   dd.deptid_chr deptid,
                                   dd.areaid_chr areaid,
                                   dd.patientid_chr,
                                   right('0000' || dd.code_chr, 4) sequenceno, 
                                   dd.registerid_chr,
                                   dd.emrinpatientid,
                                   dd.emrinpatientdate,
                                   dd.hisinpatientid_chr,
                                   dd.hisinpatientdate
                              from (select t.code_chr,
                                           d.patientid_chr,
                                           d.inpatientid_chr,
                                           d.deptid_chr,
                                           d.areaid_chr,
                                           d.registerid_chr
                                      from t_bse_bed t
                                     inner join t_opr_bih_register d on t.areaid_chr = d.areaid_chr
                                                                       
                                                                    and t.bedid_chr = d.bedid_chr
                                                                    and d.pstatus_int <> 3
                                                                    and d.pstatus_int <> 0 and d.status_int = 1
                                     inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                               d.registerid_chr
                                    
                                     where t.status_int <> 5
                                       and t.areaid_chr = ?) dd
                             inner join t_opr_bih_registerdetail p on dd.registerid_chr = p.registerid_chr
                             order by sequenceno";
                }

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strAreaID;

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 获取当前正在住院的病人，现在改为获取所有的病人（包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByNoAreaID(string p_strDeptID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                //读取新表 modified by tfzhang at 2005年10月14日 17:29:08
                string strSQL = string.Empty;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQL = @"select dd.inpatientid_chr as inpatientid,
                               p.lastname_vchr as firstname,
                               dd.code_chr as bed_id,
                               dd.deptid_chr as deptid,
                               dd.areaid_chr as areaid,
                               dd.patientid_chr,
                               right('0000' + code_chr, 4) sequenceno, 
                               dd.registerid_chr, 
                               dd.emrinpatientid, 
                               dd.emrinpatientdate, 
                               dd.hisinpatientid_chr, 
                               dd.hisinpatientdate
                          from (select t.code_chr,
                                       d.patientid_chr,
                                       d.inpatientid_chr,
                                       d.deptid_chr,
                                       d.areaid_chr,
                                       d.registerid_chr,
                                       rehis.emrinpatientid,
                                       rehis.emrinpatientdate,
                                       rehis.hisinpatientid_chr,
                                       rehis.hisinpatientdate
                                  from t_bse_bed t
                                 inner join t_opr_bih_register d on t.areaid_chr = d.deptid_chr
                                                                   
                                                                and t.bedid_chr = d.bedid_chr
                                                                and d.pstatus_int <> 3
                                                                and d.pstatus_int <> 0 and d.status_int = 1
                                 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                           d.registerid_chr
                                
                                 where t.status_int <> 5
                                   and t.areaid_chr = ?) dd
                         inner join t_opr_bih_registerdetail p on dd.registerid_chr = p.registerid_chr
                         order by sequenceno";
                }

                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQL = @"select dd.inpatientid_chr as inpatientid,
                               p.lastname_vchr as firstname,
                               dd.code_chr as bed_id,
                               dd.deptid_chr as deptid,
                               dd.areaid_chr as areaid,
                               dd.patientid_chr,
                               lpad(dd.code_chr, 4, '0') sequenceno,
                               dd.registerid_chr,
                               dd.emrinpatientid,
                               dd.emrinpatientdate,
                               dd.hisinpatientid_chr,
                               dd.hisinpatientdate
                          from (select t.code_chr,
                                       d.patientid_chr,
                                       d.inpatientid_chr,
                                       d.deptid_chr,
                                       d.areaid_chr,
                                       d.registerid_chr,
                                       rehis.emrinpatientid,
                                       rehis.emrinpatientdate,
                                       rehis.hisinpatientid_chr,
                                       rehis.hisinpatientdate
                                  from t_bse_bed t
                                 inner join t_opr_bih_register d on t.areaid_chr = d.deptid_chr
                                                                   
                                                                and t.bedid_chr = d.bedid_chr
                                                                and d.pstatus_int <> 3
                                                                and d.pstatus_int <> 0 and d.status_int = 1
                                 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                           d.registerid_chr
                                
                                 where t.status_int <> 5
                                   and t.areaid_chr = ?) dd
                         inner join t_opr_bih_registerdetail p on dd.registerid_chr = p.registerid_chr
                         order by sequenceno";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSQL = @"select dd.inpatientid_chr inpatientid,
                               p.lastname_vchr firstname,
                               dd.code_chr bed_id,
                               dd.deptid_chr deptid,
                               dd.areaid_chr areaid,
                               dd.patientid_chr,
                               right('0000' || dd.code_chr, 4) sequenceno, 
                               dd.registerid_chr, 
                               dd.emrinpatientid, 
                               dd.emrinpatientdate, 
                               dd.hisinpatientid_chr, 
                               dd.hisinpatientdate
                          from (select t.code_chr,
                                       d.patientid_chr,
                                       d.inpatientid_chr,
                                       d.deptid_chr,
                                       d.areaid_chr,
                                       d.registerid_chr,
                                       rehis.emrinpatientid,
                                       rehis.emrinpatientdate,
                                       rehis.hisinpatientid_chr,
                                       rehis.hisinpatientdate
                                  from t_bse_bed t
                                 inner join t_opr_bih_register d on t.areaid_chr = d.deptid_chr
                                                                   
                                                                and t.bedid_chr = d.bedid_chr
                                                                and d.pstatus_int <> 3
                                                                and d.pstatus_int <> 0 and d.status_int = 1
                                 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                           d.registerid_chr
                                
                                 where t.status_int <> 5
                                   and t.areaid_chr = ?) dd
                         inner join t_opr_bih_registerdetail p on dd.registerid_chr = p.registerid_chr
                         order by sequenceno";
                }

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 模糊查询当前正在住院的病人，现在改为模糊查询所有的病人（包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatienIDLike"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByDeptIDLike(string p_strInPatienIDLike, string p_strDeptID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                //			string strSQL = @"select distinct IDI.InPatientID ,p.FirstName,p.Birth
                //								from InDeptInfo as IDI,InPatientDateInfo as IPDI,PatientBaseInfo p
                //								where 
                //								IDI.InPatientID = IPDI.InPatientID  and p.InPatientID = IPDI.InPatientID and p.Status=0
                //								and IDI.InPatientID like '"+p_strInPatienIDLike+@"%'
                //								and IDI.InPatientDate = IPDI.InPatientDate
                //								and IDI.InDeptID = '"+p_strDeptID+@"'
                //								and IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"";

                //				string strSQL = @"select distinct IDI.InPatientID ,p.FirstName,p.Birth
                //									from InDeptInfo IDI,InPatientDateInfo IPDI,PatientBaseInfo p
                //									where 
                //									IDI.InPatientID = IPDI.InPatientID  and p.InPatientID = IPDI.InPatientID 
                //									and IDI.InPatientID like '"+p_strInPatienIDLike+@"%'
                //									and IDI.InPatientDate = IPDI.InPatientDate
                //									and IDI.InDeptID = '"+p_strDeptID+@"'
                //									";

                string strSQL = @"select distinct a.inpatientid_chr inpatientid,
                                                b.lastname_vchr firstname,
                                                b.birth_dat birth,
                                                d.deptid_chr deptid,
                                                a.registerid_chr,
                                                rehis.emrinpatientid,
                                                rehis.emrinpatientdate,
                                                rehis.hisinpatientid_chr,
                                                rehis.hisinpatientdate
                                  from t_opr_bih_register a
                                 inner join t_opr_bih_registerdetail b on a.registerid_chr = b.registerid_chr
                                 inner join t_opr_bih_transfer c on c.registerid_chr = a.registerid_chr and c.type_int <> 1
                                 inner join t_bse_deptdesc d on c.sourcedeptid_chr = d.deptid_chr
                                                             or c.targetdeptid_chr = d.deptid_chr
                                 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                           a.registerid_chr
                                 where d.shortno_chr = '" + p_strDeptID + @"'
                                   and a.inpatientid_chr like '" + p_strInPatienIDLike + @"%'
                                   and a.status_int > 0";

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 模糊查询当前正在住院的病人，现在改为模糊查询所有的病人（包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatienIDLike"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByAreaIDLike(string p_strInPatienIDLike, string p_strAreaID, ref string p_strResultXml, ref int p_intResultRows)
        {
            //			string strSQL = @"select distinct IDI.InPatientID ,p.FirstName
            //								from InDeptInfo as IDI,InPatientDateInfo as IPDI,PatientBaseInfo p
            //								where 
            //								IDI.InPatientID = IPDI.InPatientID and p.InPatientID = IPDI.InPatientID and p.Status=0
            //								and IDI.InPatientID like '"+p_strInPatienIDLike+@"%'
            //								and IDI.InPatientDate = IPDI.InPatientDate
            //								and IDI.Area_ID = '"+p_strAreaID+@"'
            //								and IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
            //								and IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"";

            //			string strSQL = @"select distinct IDI.InPatientID ,p.FirstName
            //								from InDeptInfo IDI,InPatientDateInfo IPDI,PatientBaseInfo p
            //								where 
            //								IDI.InPatientID = IPDI.InPatientID and p.InPatientID = IPDI.InPatientID 
            //								and IDI.InPatientID like '"+p_strInPatienIDLike+@"%'
            //								and IDI.InPatientDate = IPDI.InPatientDate
            //								and IDI.Area_ID = '"+p_strAreaID+@"'
            //								";
            string strSQL = @"select distinct a.inpatientid_chr inpatientid,
                                            b.lastname_vchr firstname,
                                            d.deptid_chr areaid,
                                            e.code_chr bed_id,
                                            a.registerid_chr,
                                            rehis.emrinpatientid,
                                            rehis.emrinpatientdate,
                                            rehis.hisinpatientid_chr,
                                            rehis.hisinpatientdate
                              from t_opr_bih_register a
                             inner join t_opr_bih_registerdetail b on a.registerid_chr = b.registerid_chr
                             inner join t_opr_bih_transfer c on c.registerid_chr = a.registerid_chr and c.type_int <> 1
                             inner join t_bse_deptdesc d on c.sourceareaid_chr = d.deptid_chr
                                                         or c.targetareaid_chr = d.deptid_chr
                             inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                       a.registerid_chr
                              left outer join t_bse_bed e on e.bedid_chr = a.bedid_chr
                             where d.shortno_chr = '" + p_strAreaID + @"'
                               and a.inpatientid_chr like '" + p_strInPatienIDLike + @"%'
                               and a.status_int > 0";
            clsHRPTableService objTabService = new clsHRPTableService();

            return objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

        }

        /// <summary>
        /// 模糊查询当前正在住院的病人，现在改为模糊查询所有的病人（包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientNameLike"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByDeptIDLike_PatientName(string p_strPatientNameLike, string p_strDeptID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                //			string strSQL = @"select distinct IDI.InPatientID ,p.FirstName
                //								from InDeptInfo as IDI,InPatientDateInfo as IPDI,PatientBaseInfo p 
                //								where 
                //								IDI.InPatientID = IPDI.InPatientID and IDI.InPatientID = p.InPatientID and p.Status=0 
                //								and p.FirstName like '"+p_strPatientNameLike+@"%'
                //								and IDI.InPatientDate = IPDI.InPatientDate
                //								and IDI.InDeptID = '"+p_strDeptID+@"'
                //								and IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								order by p.FirstName";

                //				string strSQL = @"select distinct IDI.InPatientID ,p.FirstName
                //									from InDeptInfo IDI,InPatientDateInfo IPDI,PatientBaseInfo p 
                //									where 
                //									IDI.InPatientID = IPDI.InPatientID and IDI.InPatientID = p.InPatientID 
                //									and p.FirstName like '"+p_strPatientNameLike+@"%'
                //									and IDI.InPatientDate = IPDI.InPatientDate
                //									and IDI.InDeptID = '"+p_strDeptID+@"'								
                //									order by p.FirstName";

                string strSQL = @"select distinct a.inpatientid_chr inpatientid,
                                                b.lastname_vchr firstname,
                                                a.registerid_chr,
                                                rehis.emrinpatientid,
                                                rehis.emrinpatientdate,
                                                rehis.hisinpatientid_chr,
                                                rehis.hisinpatientdate
                                  from t_opr_bih_register a
                                 inner join t_opr_bih_registerdetail b on a.registerid_chr = b.registerid_chr
                                 inner join t_opr_bih_transfer c on c.registerid_chr = a.registerid_chr and c.type_int <> 1
                                 inner join t_bse_deptdesc d on c.sourcedeptid_chr = d.deptid_chr
                                                             or c.targetdeptid_chr = d.deptid_chr
                                 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                           a.registerid_chr
                                 where d.shortno_chr = '" + p_strDeptID + @"'
                                   and b.lastname_vchr like '" + p_strPatientNameLike + @"%'
                                   and a.status_int > 0
                                 order by b.lastname_vchr";

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 模糊查询当前正在住院的病人，现在改为模糊查询所有的病人（包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientNameLike"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByAreaIDLike_PatientName(string p_strPatientNameLike, string p_strAreaID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                //只查当前住院病人
                //				string strSQL = @"select distinct IDI.InPatientID ,p.FirstName
                //									from InDeptInfo IDI,InPatientDateInfo IPDI,PatientBaseInfo p 
                //									where 
                //									IDI.InPatientID = IPDI.InPatientID and IDI.InPatientID = p.InPatientID and p.Status=0 
                //									and p.FirstName like '"+p_strPatientNameLike+@"%'
                //									and IDI.InPatientDate = IPDI.InPatientDate
                //									and IDI.Area_ID = '"+p_strAreaID+@"'
                //									and IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //									and IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //									order by p.FirstName";
                string strSQL = @"select distinct a.inpatientid_chr inpatientid,
                                                b.lastname_vchr firstname,
                                                a.registerid_chr,
                                                rehis.emrinpatientid,
                                                rehis.emrinpatientdate,
                                                rehis.hisinpatientid_chr,
                                                rehis.hisinpatientdate
                                  from t_opr_bih_register    a,
                                       t_opr_bih_registerdetail         b,
                                       t_bse_deptdesc        d,
                                       t_bse_bed             e,
                                       t_bse_hisemr_relation rehis
                                 where a.registerid_chr = b.registerid_chr
                                   and e.areaid_chr = d.deptid_chr
                                   and a.registerid_chr = rehis.registerid_chr
                                   and d.shortno_chr = '" + p_strAreaID + @"'
                                   and b.lastname_vchr like '" + p_strPatientNameLike + @"%'
                                   and a.pstatus_int <> 3
                                   and a.status_int > 0
                                   and e.status_int <> 1
                                   and a.bedid_chr = e.bedid_chr
                                 order by b.lastname_vchr";

                //所有病人，包括已出院病人

                //			string strSQL = @"select distinct IDI.InPatientID ,p.FirstName
                //								from InDeptInfo as IDI,InPatientDateInfo as IPDI,PatientBaseInfo p 
                //								where 
                //								IDI.InPatientID = IPDI.InPatientID and IDI.InPatientID = p.InPatientID
                //								and p.FirstName like '"+p_strPatientNameLike+@"%'
                //								and IDI.InPatientDate = IPDI.InPatientDate
                //								and IDI.Area_ID = '"+p_strAreaID+@"'
                //								order by p.FirstName";

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);
            }

            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 模糊查询当前正在住院的病人（不包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBedNOLike"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByDeptIDLike_BedNO(string p_strBedNOLike, string p_strDeptID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                if (p_strBedNOLike == null)
                    return -1;
                string strSQL = "";

                //首先,按照Name查找
                //				strSQL=@"select distinct IDI.InPatientID,p.FirstName
                //								from InDeptInfo IDI,InPatientDateInfo IPDI,InPatient_Bed_Desc b ,PatientBaseInfo p
                //								where 
                //								IDI.InPatientID = IPDI.InPatientID  and p.InPatientID = IPDI.InPatientID and p.Status=0
                //								and IDI.Bed_ID=b.Bed_ID and b.End_Date_Bed_Naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" 
                //								and b.Bed_Name like '"+p_strBedNOLike+@"%'
                //								and IDI.InPatientDate = IPDI.InPatientDate
                //								and IDI.InDeptID = '"+p_strDeptID+@"'
                //								and IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								order by b.Bed_Name";

                strSQL = @"select distinct a.inpatientid_chr inpatientid,
                                        b.lastname_vchr firstname,
                                        a.registerid_chr,
                                        rehis.emrinpatientid,
                                        rehis.emrinpatientdate,
                                        rehis.hisinpatientid_chr,
                                        rehis.hisinpatientdate
                          from t_opr_bih_register    a,
                               t_opr_bih_registerdetail         b,
                               t_bse_deptdesc        d,
                               t_bse_bed             e,
                               t_bse_hisemr_relation rehis
                         where a.registerid_chr = b.registerid_chr
                           and e.areaid_chr = d.deptid_chr
                           and a.registerid_chr = rehis.registerid_chr
                           and d.shortno_chr = '" + p_strDeptID + @"'
                           and e.code_chr = '" + p_strBedNOLike + @"'
                           and a.pstatus_int <> 3
                           and a.status_int > 0
                           and e.status_int <> 1
                           and a.bedid_chr = e.bedid_chr
                         order by e.code_chr";

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

                if (lngRes > 0 && p_intResultRows > 0)
                    return lngRes;

                //若名称没有,再按照ID查找
                //				strSQL=@"select distinct IDI.InPatientID ,p.FirstName
                //								from InDeptInfo IDI,InPatientDateInfo IPDI,InPatient_Bed_Desc b ,PatientBaseInfo p
                //								where 
                //								IDI.InPatientID = IPDI.InPatientID  and p.InPatientID = IPDI.InPatientID and p.Status=0
                //								and IDI.Bed_ID=b.Bed_ID and b.End_Date_Bed_Naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.Bed_ID like '"+p_strBedNOLike+@"%' 
                //								and IDI.InPatientDate = IPDI.InPatientDate
                //								and IDI.InDeptID = '"+p_strDeptID+@"'
                //								and IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.InBedEndDate ="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								order by b.Bed_Name";

                strSQL = @"select distinct a.inpatientid_chr inpatientid,
                                        b.lastname_vchr firstname,
                                        a.registerid_chr,
                                        rehis.emrinpatientid,
                                        rehis.emrinpatientdate,
                                        rehis.hisinpatientid_chr,
                                        rehis.hisinpatientdate
                          from t_opr_bih_register    a,
                               t_opr_bih_registerdetail         b,
                               t_bse_deptdesc        d,
                               t_bse_bed             e,
                               t_bse_hisemr_relation rehis
                         where a.registerid_chr = b.registerid_chr
                           and e.areaid_chr = d.deptid_chr
                           and a.registerid_chr = rehis.registerid_chr
                           and d.shortno_chr = '" + p_strDeptID + @"'
                           and e.bed_no = '" + p_strBedNOLike + @"'
                           and a.pstatus_int <> 3
                           and a.status_int > 0
                           and e.status_int <> 1
                           and a.bedid_chr = e.bedid_chr
                         order by e.code_chr";

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 模糊查询当前正在住院的病人（不包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBedNOLike"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByAreaIDLike_BedNO(string p_strBedNOLike, string p_strAreaID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                if (p_strBedNOLike == null)
                    return -1;
                string strSQL = "";

                //首先,按照Name查找
                //				strSQL=@"select distinct IDI.InPatientID,p.FirstName,b.Bed_Name
                //								from InDeptInfo IDI,InPatientDateInfo IPDI,InPatient_Bed_Desc b ,PatientBaseInfo p
                //								where 
                //								IDI.InPatientID = IPDI.InPatientID  and p.InPatientID = IPDI.InPatientID and p.Status=0
                //								and IDI.Bed_ID=b.Bed_ID and b.End_Date_Bed_Naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and b.Bed_Name like '"+p_strBedNOLike+@"%'
                //								and IDI.InPatientDate = IPDI.InPatientDate
                //								and IDI.Area_ID	 = '"+p_strAreaID+@"'
                //								and IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.Bed_Status is null
                //								order by b.Bed_Name";
                //使用新表 modified by tfzhang at 2005年10月24日 11:57:44

                strSQL = @"select dd.inpatientid_chr inpatientid,
                               p.lastname_vchr firstname,
                               dd.code_chr bed_id,
                               dd.deptid_chr deptid,
                               dd.areaid_chr areaid,
                               dd.patientid_chr,
                               dd.registerid_chr,
                               dd.emrinpatientid,
                               dd.emrinpatientdate,
                               dd.hisinpatientid_chr,
                               dd.hisinpatientdate
                          from (select t.code_chr,
                                       d.patientid_chr,
                                       d.inpatientid_chr,
                                       d.deptid_chr,
                                       d.areaid_chr,
                                       d.registerid_chr,
                                       rehis.emrinpatientid,
                                       rehis.emrinpatientdate,
                                       rehis.hisinpatientid_chr,
                                       rehis.hisinpatientdate
                                  from t_bse_bed t
                                 inner join t_opr_bih_register d on t.areaid_chr = d.areaid_chr
                                                                   
                                                                and t.bedid_chr = d.bedid_chr
                                                                and d.pstatus_int <> 3
                                                                and d.pstatus_int <> 0 and d.status_int = 1
                                 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                           d.registerid_chr
                                
                                 where t.status_int <> 5
                                   and t.areaid_chr = '" + p_strAreaID.Trim() + @"'
                                   and t.code_chr like '" + p_strBedNOLike + @"%') dd
                         inner join t_opr_bih_registerdetail p on dd.registerid_chr = p.registerid_chr
                         order by dd.code_chr";


                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

                if (lngRes > 0 && p_intResultRows > 0)
                    return lngRes;

                //若名称没有,再按照ID查找
                //				strSQL=@"select distinct IDI.InPatientID ,p.FirstName,b.Bed_Name
                //								from InDeptInfo IDI,InPatientDateInfo IPDI,InPatient_Bed_Desc b ,PatientBaseInfo p
                //								where 
                //								IDI.InPatientID = IPDI.InPatientID  and p.InPatientID = IPDI.InPatientID and p.Status=0
                //								and IDI.Bed_ID=b.Bed_ID and b.End_Date_Bed_Naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.Bed_ID like '"+p_strBedNOLike+@"%' 
                //								and IDI.InPatientDate = IPDI.InPatientDate
                //								and IDI.Area_ID = '"+p_strAreaID+@"'
                //								and IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								and IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								order by b.Bed_Name";
                //使用新表 modified by tfzhang at 2005年10月24日 11:57:44
                strSQL = @"select dd.inpatientid_chr inpatientid, p.lastname_vchr firstname, dd.code_chr bed_id,dd.deptid_chr deptid,dd.areaid_chr areaid,dd.patientid_chr,dd.registerid_chr
								from (select t.code_chr, d.patientid_chr, d.inpatientid_chr,d.deptid_chr,d.areaid_chr,d.registerid_chr
										from t_bse_bed t
										inner  join t_opr_bih_register d on t.areaid_chr = d.areaid_chr
								                                                
																			and t.bedid_chr = d.bedid_chr and d.status_int = 1
																			and d.pstatus_int <> 3 and d.pstatus_int<>0
																			
										where t.status_int <> 5
										and t.areaid_chr ='" + p_strAreaID.Trim() + @"'
										and t.bedid_chr like'" + p_strBedNOLike + @"%'
										) dd
								t_opr_bih_registerdetail p on dd.registerid_chr = p.registerid_chr
								order by dd.code_chr";


                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 模糊查询当前正在住院的病人（不包括已经出院的病人）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBedNOLike"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByNoAreaIDLike_BedNO(string p_strBedNOLike, string p_strDeptID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                if (p_strBedNOLike == null)
                    return -1;
                string strSQL = "";

                strSQL = @"select dd.inpatientid_chr inpatientid,
                           p.lastname_vchr firstname,
                           dd.code_chr bed_id,
                           dd.deptid_chr deptid,
                           dd.areaid_chr areaid,
                           dd.patientid_chr,
                           dd.registerid_chr,
                           dd.emrinpatientid,
                           dd.emrinpatientdate,
                           dd.hisinpatientid_chr,
                           dd.hisinpatientdate
                      from (select t.code_chr,
                                   d.patientid_chr,
                                   d.inpatientid_chr,
                                   d.deptid_chr,
                                   d.areaid_chr,
                                   d.registerid_chr,
                                   rehis.emrinpatientid,
                                   rehis.emrinpatientdate,
                                   rehis.hisinpatientid_chr,
                                   rehis.hisinpatientdate
                              from t_bse_bed t
                             inner join t_opr_bih_register d on t.areaid_chr = d.deptid_chr
                                                               
                                                            and t.bedid_chr = d.bedid_chr
                                                            and d.pstatus_int <> 3
                                                            and d.pstatus_int <> 0 and d.status_int = 1
                             inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                       d.registerid_chr
                            
                             where t.status_int <> 5
                               and t.areaid_chr = '" + p_strDeptID.Trim() + @"'
                               and t.code_chr like '" + p_strBedNOLike + @"%') dd
                     inner join t_opr_bih_registerdetail p on dd.registerid_chr = p.registerid_chr
                     order by dd.code_chr";


                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

                if (lngRes > 0 && p_intResultRows > 0)
                    return lngRes;
                //使用新表 modified by tfzhang at 2005年10月24日 11:57:44
                strSQL = @"select dd.inpatientid_chr inpatientid,
                           p.lastname_vchr firstname,
                           dd.code_chr bed_id,
                           dd.deptid_chr deptid,
                           dd.areaid_chr areaid,
                           dd.patientid_chr,
                           dd.registerid_chr,
                           dd.emrinpatientid,
                           dd.emrinpatientdate,
                           dd.hisinpatientid_chr,
                           dd.hisinpatientdate
                      from (select t.code_chr,
                                   d.patientid_chr,
                                   d.inpatientid_chr,
                                   d.deptid_chr,
                                   d.areaid_chr,
                                   d.registerid_chr,
                                   rehis.emrinpatientid,
                                   rehis.emrinpatientdate,
                                   rehis.hisinpatientid_chr,
                                   rehis.hisinpatientdate
                              from t_bse_bed t
                             inner join t_opr_bih_register d on t.areaid_chr = d.deptid_chr
                                                               
                                                            and t.bedid_chr = d.bedid_chr
                                                            and d.pstatus_int <> 3 and d.status_int = 1
                             inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                       d.registerid_chr
                            
                             where t.status_int <> 5
                               and t.areaid_chr = '" + p_strDeptID.Trim() + @"'
                               and t.bedid_chr like '" + p_strBedNOLike + @"%') dd
                     inner join t_opr_bih_registerdetail p on dd.registerid_chr = p.registerid_chr
                     order by dd.code_chr";


                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 查找病人初步诊断
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetPatientDiagnose(string p_strInPatientID, string p_strInPatientDate)
        {
            string strRes = "";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                string strSql = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSql = @"select top 1 primarydiagnose from ipcasehistcont_primarydiagnose
								where inpatientid = ? and inpatientdate = ?
								order by lastmodifydate desc ";

                }
                else
                {
                    strSql = @"select primarydiagnose
  from (select primarydiagnose
          from ipcasehistcont_primarydiagnose
         where inpatientid = ?
           and inpatientdate = ?
         order by lastmodifydate desc)
 where rownum = 1";

                }

                //				IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[2];
                //				objDPArr[0] = new Oracle.DataAccess.Client.OracleParameter();
                //				objDPArr[1] = new Oracle.DataAccess.Client.OracleParameter();

                //clsHRPTableService p_objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                DataTable dtResult = new DataTable();

                long lngRes = objTabService.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    return dtResult.Rows[0]["PRIMARYDIAGNOSE"].ToString().Trim();
                }
                else
                    return "";

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return strRes;
        }
        /// <summary>
        /// 查找病区下某时间后出院的所有病人
        /// </summary>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strFirstDate"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEndDateInPatientByAreaID(string p_strAreaID, string p_strFirstDate, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                if (p_strAreaID == null || p_strAreaID == string.Empty)
                    return -1;
                //				string strSQL = @"select distinct pb.FirstName,ipd.*,op.Times as RevisitTimes,op.Status as RevisitStatus from PatientBaseInfo pb inner join InPatientDateInfo ipd
                //								on pb.InPatientID = ipd.InPatientID inner join InDeptInfo idi on idi.InPatientID = ipd.InPatientID and idi.InPatientDate = ipd.InPatientDate
                //								left outer join OutPatientRevisitRemind op on op.InPatientID = ipd.InPatientID and op.InPatientDate = ipd.InPatientDate
                //							where ipd.InPatientDate = (select max(InPatientDate) from InPatientDateInfo 
                //								where InPatientID = ipd.InPatientID) and ipd.InPatientEndDate <> "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+" and idi.Area_ID = '"+p_strAreaID+@"'
                //								and ipd.InPatientEndDate >= "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strFirstDate)+" order by ipd.InPatientEndDate";
                string strSQL = @"select dd.inpatientid_chr inpatientid,
                                   dd.lastname_vchr firstname,
                                   dd.inpatient_dat,
                                   tt.status revisitstatus,
                                   tt.revisittime revisittimes,
                                   dd.registerid_chr,
                                   dd.emrinpatientid,
                                   dd.emrinpatientdate,
                                   dd.hisinpatientid_chr,
                                   dd.hisinpatientdate
                              from (select t.registerid_chr,
                                           t.inpatient_dat,
                                           g.lastname_vchr,
                                           g.inpatientid_chr,
                                           rehis.emrinpatientid,
                                           rehis.emrinpatientdate,
                                           rehis.hisinpatientid_chr,
                                           rehis.hisinpatientdate
                                      from t_opr_bih_register    t,
                                           t_opr_bih_leave       d,
                                           t_opr_bih_registerdetail         g,
                                           t_bse_hisemr_relation rehis
                                     where d.registerid_chr = t.registerid_chr
                                       and rehis.registerid_chr = t.registerid_chr
                                       and d.modify_dat >= ?
                                       and g.registerid_chr = t.registerid_chr
                                       and t.areaid_chr = ? and t.status_int = 1) dd
                              left outer join outpatientrevisitremind tt on dd.emrinpatientid =
                                                                            tt.inpatientid";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(p_strFirstDate);
                objDPArr[1].Value = p_strAreaID.Trim();

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 查找病区下一段时间内出院的病人
        /// </summary>
        /// <param name="p_strAreaID">病区或科室</param>
        /// <param name="p_strStartDate">开始时间</param>
        /// <param name="p_strEndDate">结束时间</param>
        /// <param name="p_blnIsOutPatient">是否出院病人</param>
        /// <param name="p_intDeptOrArea">科室或病区(0.科室;1.病区)</param>
        /// <param name="p_strResultXml">查询结果</param>
        /// <param name="p_intResultRows">行</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBetwDateInPatientByAreaID(string p_strAreaID, string p_strStartDate,
            string p_strEndDate, bool p_blnIsOutPatient, int p_intDeptOrArea,
            ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            string strSQL = "";
            string strDeptOrArea = "";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                if (p_strAreaID == null || p_strAreaID == string.Empty)
                    return -1;

                if (p_intDeptOrArea == 0)
                    strDeptOrArea = "t.DEPTID_CHR";
                else if (p_intDeptOrArea == 1)
                    strDeptOrArea = "t.areaid_chr";
                else
                    return -1;

                if (p_blnIsOutPatient)
                    //					strSQL = @"select distinct pb.FirstName,ipd.*,op.Times as RevisitTimes,op.Status as RevisitStatus from PatientBaseInfo pb inner join InPatientDateInfo ipd
                    //								on pb.InPatientID = ipd.InPatientID inner join InDeptInfo idi on idi.InPatientID = ipd.InPatientID and idi.InPatientDate = ipd.InPatientDate
                    //								left outer join OutPatientRevisitRemind op on op.InPatientID = ipd.InPatientID and op.InPatientDate = ipd.InPatientDate
                    //							where ipd.InPatientDate = (select max(InPatientDate) from InPatientDateInfo 
                    //								where InPatientID = ipd.InPatientID) and ipd.InPatientEndDate <> "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+" and idi.Area_ID = '"+p_strAreaID+@"'
                    //								and ipd.InPatientEndDate >= "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strStartDate)+" and ipd.InPatientEndDate <= "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strEndDate)+" order by ipd.InPatientEndDate";
                    strSQL = @"select distinct dd.inpatientid_chr as inpatientid,
                                            dd.lastname_vchr as firstname,
                                            dd.inpatient_dat,
                                            tt.status as revisitstatus,
                                            tt.times as revisittimes,
                                            dd.registerid_chr,
                                            dd.emrinpatientid,
                                            dd.emrinpatientdate,
                                            dd.hisinpatientid_chr,
                                            dd.hisinpatientdate
                              from (select t.registerid_chr,
                                           t.inpatient_dat,
                                           g.lastname_vchr,
                                           g.inpatientid_chr,
                                           rehis.emrinpatientid,
                                           rehis.emrinpatientdate,
                                           rehis.hisinpatientid_chr,
                                           rehis.hisinpatientdate
                                      from t_opr_bih_register    t,
                                           t_opr_bih_leave       d,
                                           t_opr_bih_registerdetail         g,
                                           t_bse_hisemr_relation rehis
                                     where d.registerid_chr = t.registerid_chr
                                       and rehis.registerid_chr = t.registerid_chr
                                       and t.pstatus_int = 3 and t.status_int = 1
                                       and d.modify_dat >= ?
                                       and d.modify_dat <= ?
                                       and g.registerid_chr = t.registerid_chr
                                       and " + strDeptOrArea + @" =
                                           (select deptid_chr from t_bse_deptdesc where shortno_chr = ?)) dd
                              left outer join outpatientrevisitremind tt on dd.emrinpatientid =
                                                                            tt.inpatientid";

                else
                    //					strSQL = @"select distinct pb.FirstName,ipd.*,op.Times as RevisitTimes,op.Status as RevisitStatus from PatientBaseInfo pb inner join InPatientDateInfo ipd
                    //								on pb.InPatientID = ipd.InPatientID inner join InDeptInfo idi on idi.InPatientID = ipd.InPatientID and idi.InPatientDate = ipd.InPatientDate
                    //								left outer join OutPatientRevisitRemind op on op.InPatientID = ipd.InPatientID and op.InPatientDate = ipd.InPatientDate
                    //							where ipd.InPatientDate = (select max(InPatientDate) from InPatientDateInfo 
                    //								where InPatientID = ipd.InPatientID) and ipd.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+" and idi.Area_ID = '"+p_strAreaID+@"'
                    //								and ipd.InPatientDate >= "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strStartDate)+" and ipd.InPatientDate <= "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strEndDate)+" order by ipd.InPatientEndDate";
                    strSQL = @"select distinct dd.inpatientid_chr as inpatientid,
                                            dd.lastname_vchr as firstname,
                                            dd.inpatient_dat,
                                            tt.status as revisitstatus,
                                            tt.times as revisittimes,
                                            dd.registerid_chr,
                                            dd.emrinpatientid,
                                            dd.emrinpatientdate,
                                            dd.hisinpatientid_chr,
                                            dd.hisinpatientdate
                              from (select t.registerid_chr,
                                           t.inpatient_dat,
                                           g.lastname_vchr,
                                           g.inpatientid_chr,
                                           rehis.emrinpatientid,
                                           rehis.emrinpatientdate,
                                           rehis.hisinpatientid_chr,
                                           rehis.hisinpatientdate
                                      from t_opr_bih_register    t,
                                           t_opr_bih_registerdetail         g,
                                           t_bse_hisemr_relation rehis
                                     where t.pstatus_int <> 3
                                       and rehis.registerid_chr = t.registerid_chr
                                       and t.pstatus_int <> 0 and t.status_int = 1
                                       and t.inpatient_dat >= ?
                                       and t.inpatient_dat <= ?
                                       and g.registerid_chr = t.registerid_chr
                                       and " + strDeptOrArea + @" =
                                           (select deptid_chr from t_bse_deptdesc where shortno_chr = ?)) dd
                              left outer join outpatientrevisitremind tt on dd.emrinpatientid =
                                                                            tt.inpatientid";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(p_strStartDate);
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strEndDate);
                objDPArr[2].Value = p_strAreaID.Trim();

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 根据住院号查找病人
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByInPatientID(string p_strInPatientID, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                if (p_strInPatientID == null || p_strInPatientID == string.Empty)
                    return -1;
                //				string strSQL = @"select distinct pb.FirstName,ipd.*,op.Times as RevisitTimes,op.Status as RevisitStatus from PatientBaseInfo pb inner join InPatientDateInfo ipd
                //								on pb.InPatientID = ipd.InPatientID inner join InDeptInfo idi on idi.InPatientID = ipd.InPatientID and idi.InPatientDate = ipd.InPatientDate
                //								left outer join OutPatientRevisitRemind op on op.InPatientID = ipd.InPatientID and op.InPatientDate = ipd.InPatientDate
                //							where ipd.InPatientDate = (select max(InPatientDate) from InPatientDateInfo 
                //								where InPatientID = ipd.InPatientID) and ipd.InPatientID='"+p_strInPatientID+"' order by ipd.InPatientEndDate";
                string strSQL = @"select dd.inpatientid_chr as inpatientid,
                                       dd.lastname_vchr as firstname,
                                       dd.inpatient_dat,
                                       tt.status as revisitstatus,
                                       tt.revisittime as revisittimes,
                                       dd.registerid_chr,
                                       dd.emrinpatientid,
                                       dd.emrinpatientdate,
                                       dd.hisinpatientid_chr,
                                       dd.hisinpatientdate
                                  from (select t.registerid_chr,
                                               t.inpatient_dat,
                                               g.lastname_vchr,
                                               g.inpatientid_chr,
                                               rehis.emrinpatientid,
                                               rehis.emrinpatientdate,
                                               rehis.hisinpatientid_chr,
                                               rehis.hisinpatientdate
                                          from t_opr_bih_register    t,
                                               t_opr_bih_leave       d,
                                               t_opr_bih_registerdetail         g,
                                               t_bse_hisemr_relation rehis
                                         where d.registerid_chr = t.registerid_chr
                                           and rehis.registerid_chr = t.registerid_chr
                                           and g.registerid_chr = t.registerid_chr
                                           and t.inpatientid_chr = ? and t.status_int = 1) dd
                                  left outer join outpatientrevisitremind tt on dd.emrinpatientid =
                                                                                tt.inpatientid";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 根据病人姓名查找病人信息
        /// </summary>
        /// <param name="p_strPatientName"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInPatientByPatientName(string p_strPatientName, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                if (p_strPatientName == null || p_strPatientName == string.Empty)
                    return -1;
                //				string strSQL = @"select distinct pb.FirstName,ipd.*,op.Times as RevisitTimes,op.Status as RevisitStatus from PatientBaseInfo pb inner join InPatientDateInfo ipd
                //								on pb.InPatientID = ipd.InPatientID inner join InDeptInfo idi on idi.InPatientID = ipd.InPatientID and idi.InPatientDate = ipd.InPatientDate
                //								left outer join OutPatientRevisitRemind op on op.InPatientID = ipd.InPatientID and op.InPatientDate = ipd.InPatientDate
                //							where ipd.InPatientDate = (select max(InPatientDate) from InPatientDateInfo 
                //								where InPatientID = ipd.InPatientID) and pb.FirstName='"+p_strPatientName+"' order by ipd.InPatientEndDate";
                //
                string strSQL = @"select dd.inpatientid_chr as inpatientid,
                                       dd.lastname_vchr as firstname,
                                       dd.inpatient_dat,
                                       tt.status as revisitstatus,
                                       tt.revisittime as revisittimes,
                                       dd.registerid_chr,
                                       dd.emrinpatientid,
                                       dd.emrinpatientdate,
                                       dd.hisinpatientid_chr,
                                       dd.hisinpatientdate
                                  from (select t.registerid_chr,
                                               t.inpatient_dat,
                                               g.lastname_vchr,
                                               g.inpatientid_chr,
                                               rehis.emrinpatientid,
                                               rehis.emrinpatientdate,
                                               rehis.hisinpatientid_chr,
                                               rehis.hisinpatientdate
                                          from t_opr_bih_register    t,
                                               t_opr_bih_leave       d,
                                               t_opr_bih_registerdetail         g,
                                               t_bse_hisemr_relation rehis
                                         where d.registerid_chr = t.registerid_chr
                                           and rehis.registerid_chr = t.registerid_chr
                                           and g.registerid_chr = t.registerid_chr
                                           and g.lastname_vchr = ? and t.status_int = 1) dd
                                  left outer join outpatientrevisitremind tt on dd.emrinpatientid =
                                                                                tt.inpatientid";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strPatientName;

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 根据病人ID获取住院号
        /// </summary>
        /// <param name="p_strPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetInPatientIDByPatientID(string p_strPatient)
        {
            string strRes = "";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                if (p_strPatient == null || p_strPatient == "")
                    return "";
                string strSql = @"select t.inpatientid from patientbaseinfo t where t.patientid = ?";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strPatient.Trim();

                DataTable dtResult = new DataTable();
                long lngRes = objTabService.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count == 1)
                {
                    return dtResult.Rows[0]["INPATIENTID"].ToString().Trim();
                }
                else
                    return "";

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //返回
            return strRes;
        }
        /// <summary>
        /// 根据病人id获取指定表单的记录时间列表和住院时间列表
        /// </summary>
        /// <param name="p_strPatientID">病人ID</param>
        /// <param name="strTableName">指定表单的表名</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCreateDateByPatientID(string p_strPatientID, string strQueryDate, string strTableName, out string[] strCreateDateArr, out string[] strInpatientDateArr)
        {
            long lngRes = 0;
            strCreateDateArr = null;
            strInpatientDateArr = null;
            string Sql = string.Empty;
            string strTable = strTableName;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;
            DataTable dt = new DataTable();

            try
            {
                Sql = @"select count(*) from User_Tables where lower(table_name) = ?";
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strTable.ToLower();
                lngRes = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0 && Convert.ToDecimal(dt.Rows[0][0]) > 0)
                {
                    Sql = "select distinct  inpatientdate," + strQueryDate + " from " + strTable + " where inpatientid =? and status<>'1'";
                    svc.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = p_strPatientID;

                    lngRes = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                    if (lngRes > 0 && dt.Rows.Count > 0)
                    {
                        strCreateDateArr = new string[dt.Rows.Count];
                        strInpatientDateArr = new string[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            strCreateDateArr[i] = dt.Rows[i][strQueryDate].ToString().Trim();
                            strInpatientDateArr[i] = dt.Rows[i]["inpatientdate"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            return lngRes;


        }

        /// <summary>
        /// 根据病人id和入院时间获取指定表单的创建时间列表
        /// </summary>
        /// <param name="p_strPatientID">病人住院ID</param>
        /// <param name="strTableName">指定表单的表名</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCreateDateByIDDate(string p_strInPatientID, string strInPatientDate, string strQueryDate, string strTableName, out string[] strCreateDateArr)
        {
            long lngRes = 0;
            strCreateDateArr = null;
            string strSQl = string.Empty;
            string strTable = strTableName;
            clsHRPTableService objTabService = new clsHRPTableService();
            strSQl = "select " + strQueryDate + " from " + strTable + " where inpatientid=?  and inpatientdate=? and status<>'1' order by  " + strQueryDate;
            try
            {
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strInPatientDate);

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    strCreateDateArr = new string[dtResult.Rows.Count];
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        strCreateDateArr[i] = dtResult.Rows[i][strQueryDate].ToString().Trim();
                    }
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// m_lngGetOutHospitalDate2
        /// </summary>
        /// <param name="ipNo"></param>
        /// <param name="dtmIn"></param>
        /// <returns></returns>
        [AutoComplete]
        public DateTime? m_lngGetOutHospitalDate2(string ipNo, DateTime dtmIn)
        {
            DateTime? dtmOut = null;
            clsHRPTableService svc = new clsHRPTableService();
            string Sql = @"select t.inpatient_dat, b.outhospital_dat
                              from t_opr_bih_register t
                             inner join t_opr_bih_leave b
                                on t.registerid_chr = b.registerid_chr
                             where b.pstatus_int = 1
                               and t.inpatientid_chr = ?";
            try
            {
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = ipNo;

                DataTable dt = new DataTable();
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToDateTime(dr["inpatient_dat"]).ToString("yyyy-MM-dd") == dtmIn.ToString("yyyy-MM-dd"))
                        {
                            dtmOut = Convert.ToDateTime(dr["outhospital_dat"]);
                            break;
                        }
                    }
                }
                if (dtmOut == null) dtmOut = DateTime.Now;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
            }
            return dtmOut;
        }

        /// <summary>
        /// 根据入院登记流水号获取病人当次出院时间
        /// </summary>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_dtmOutTime">出院时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutHospitalDate(string p_strRegisterID, out DateTime p_dtmOutTime)
        {
            p_dtmOutTime = DateTime.MinValue;
            if (p_strRegisterID == null || p_strRegisterID == "")
                return -1;
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            string strSQl = "select outhospital_dat from t_opr_bih_leave where registerid_chr=?  and status_int=1";
            try
            {
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID.Trim();

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_dtmOutTime = dtResult.Rows[0]["OUTHOSPITAL_DAT"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dtResult.Rows[0]["OUTHOSPITAL_DAT"]);
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 根据入院登记流水号获取病人预出院时间
        /// </summary>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_dtmPrepOutTime">预出院时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrepOutHospitalDate(string p_strRegisterID, out DateTime p_dtmPrepOutTime)
        {
            p_dtmPrepOutTime = DateTime.MinValue;
            if (string.IsNullOrEmpty(p_strRegisterID))
                return -1;
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            string strSQl = "select modify_dat from t_opr_bih_transfer where registerid_chr=?  and type_int=7";
            try
            {
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID.Trim();

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_dtmPrepOutTime = dtResult.Rows[0]["MODIFY_DAT"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dtResult.Rows[0]["MODIFY_DAT"]);
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 根据病人ID及住院日期取得登记流水号
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_strInpatientDate"></param>
        /// <param name="p_strRegisterID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRegisterIDByPatient(string p_strPatientID, string p_strInpatientDate, out string p_strRegisterID)
        {
            p_strRegisterID = "";
            if (p_strPatientID == null || p_strPatientID == "" || p_strInpatientDate == null || p_strInpatientDate == "")
                return -1;
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            string strSQl = "select registerid_chr from t_opr_bih_register where patientid_chr=? and inpatient_dat=? and status_int=1";
            try
            {
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInpatientDate);

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_strRegisterID = dtResult.Rows[0]["REGISTERID_CHR"].ToString();
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }

            return lngRes;
        }

        /// <summary>
        /// 根据病人ID获取该病人信息
        /// </summary>
        /// <param name="p_strPatientID">病人ID</param>
        /// <param name="p_dtbPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfoByID(string p_strPatientID, out DataTable p_dtbPatient)
        {
            p_dtbPatient = new DataTable();
            if (p_strPatientID == null || p_strPatientID == "")
                return -1;
            long lngRes = 0;
            string strSQl = @"select t.registerid_chr,
       t.lastname_vchr,
       t.idcard_chr,
       t.married_chr,
       t.birthplace_vchr,
       t.homeaddress_vchr,
       t.sex_chr,
       t.nationality_vchr,
       t.firstname_vchr,
       t.birth_dat,
       t.race_vchr,
       t.nativeplace_vchr,
       t.occupation_vchr,
       t.name_vchr,
       t.homephone_vchr,
       t.officephone_vchr,
       t.insuranceid_vchr,
       t.mobile_chr,
       t.officeaddress_vchr,
       t.employer_vchr,
       t.officepc_vchr,
       t.homepc_chr,
       t.email_vchr,
       t.contactpersonfirstname_vchr,
       t.contactpersonlastname_vchr,
       t.contactpersonaddress_vchr,
       t.contactpersonphone_vchr,
       t.contactpersonpc_chr,
       t.patientrelation_vchr,
       t.firstdate_dat,
       t.isemployee_int,
       t.status_int,
       t.deactivate_dat,
       t.operatorid_chr,
       t.modify_dat,
       t.optimes_int,
       t.govcard_chr,
       t.bloodtype_chr,
       t.ifallergic_int,
       t.allergicdesc_vchr,
       t.difficulty_vchr,
       t.insuredtotalmoney_mny,
       t.insuredpaymoney_mny,
       t.insuredpaytime_int,
       t.insuredpayscale_dec,
       c.patientid_chr,
       c.inpatientid_chr
  from t_opr_bih_registerdetail t
 inner join t_opr_bih_register c on c.registerid_chr = t.registerid_chr
 where c.patientid_chr = ?
   and c.status_int = 1";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strPatientID.Trim();

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_dtbPatient = dtResult;
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 根据HISID获取EMRID
        /// </summary>
        /// <param name="p_strHISID">病人HISID</param>
        /// <param name="p_strEMRID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEMRIDByHISID(string p_strHISID, out string p_strEMRID)
        {
            p_strEMRID = null;
            if (string.IsNullOrEmpty(p_strHISID))
                return -1;
            long lngRes = 0;
            string strSQl = @"select t.emrinpatientid
							from t_bse_hisemr_relation t
							where t.hisinpatientid_chr = ?";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strHISID.Trim();

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_strEMRID = dtResult.Rows[0]["EMRINPATIENTID"].ToString();
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 根据EMRID获取HISID
        /// </summary>
        /// <param name="p_strEMRID"></param>
        /// <param name="p_strHISID">病人HISID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHISIDByEMRID(string p_strEMRID, out string p_strHISID)
        {
            p_strHISID = null;
            if (string.IsNullOrEmpty(p_strEMRID))
                return -1;
            long lngRes = 0;
            string strSQl = @"select t.hisinpatientid_chr
							from t_bse_hisemr_relation t
							where t.emrinpatientid = ?";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEMRID.Trim();

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_strHISID = dtResult.Rows[0]["HISINPATIENTID_CHR"].ToString();
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// patientid_chr ： 病人ID
        /// lastname_vchr ： 姓名
        /// idcard_chr    ： 身份证号
        /// married_chr   ： 婚姻
        /// homeaddress_vchr ： 家庭地址
        /// sex_chr       ： 性别
        /// birth_dat     ： 生日
        /// Age           ：年龄
        /// contactpersonlastname_vchr ： 联系人
        /// nativeplace_vchr ： 籍贯
        /// occupation_vchr ： 职业
        /// insuranceid_vchr ： 医疗卡号
        /// contactpersonphone_vchr ： 联系人电话
        /// patientrelation_vchr ： 联系人关系
        /// inpatientid_chr ： 住院号
        /// inpatient_dat ： 住院时间
        /// paytypeid_chr ： 付款类型
        /// maindescriptionall ： 主诉
        /// currentstatusall ： 现病史
        /// beforetimestatusall ： 既往史
        /// ownhistoryall ： 个人史
        /// catameniahistoryall ： 生育史
        /// primarydiagnoseall ： 入院诊断
        /// familyhistoryall ： 家族史
        /// DIAG_VCHR ： 门诊诊断
        /// </summary>
        /// <param name="p_strPatientCardId"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGet120PatientInfo(string p_strPatientCardId, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            if (string.IsNullOrEmpty(p_strPatientCardId))
                return -1;
            long lngRes = 0;
            string strSQl = @"select t.patientid_chr,
       t.lastname_vchr,
       t.idcard_chr,
       t.married_chr,
       t.homeaddress_vchr,
       t.sex_chr,
       t.birth_dat,
       t.contactpersonlastname_vchr,
       t.nativeplace_vchr,
       t.occupation_vchr,
       t.insuranceid_vchr,
       t.contactpersonphone_vchr,
       t.patientrelation_vchr,
       t.inpatientid_chr,
       g.inpatient_dat,
       t.paytypeid_chr,
       c1.maindescriptionall,
       c1.currentstatusall,
       c1.beforetimestatusall,
       c1.ownhistoryall,
       c1.catameniahistoryall,
       c1.primarydiagnoseall,
       c1.familyhistoryall,
       o.diag_vchr, " + clsDatabaseSQLConvert.s_strGetAgeSQL("t.birth_dat") + @" Age,  
        s.patientpic_grp,
        card.patientcardid_chr
  from t_bse_patient t
 inner join t_opr_bih_register g on t.patientid_chr = g.patientid_chr and g.status_int = 1
 inner join t_bse_hisemr_relation r on g.registerid_chr = r.registerid_chr
inner join t_bse_patientcard card on t.patientid_chr = card.patientid_chr
  left outer join inpatientcasehistory_history c1 on r.emrinpatientid =
                                                     c1.inpatientid
                                                 and c1.inpatientdate =
                                                     r.emrinpatientdate
left join t_opr_outpatientcasehis o on t.patientid_chr = o.patientid_chr
left join t_bse_patientsign s on t.patientid_chr = s.patientid_chr
 where card.patientcardid_chr = ?
   and c1.status = 0
   and rownum = 1
   order by g.inpatient_dat desc";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strPatientCardId.Trim();

                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref p_dtbResult, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 根据病人住院号获取指定表单的有记录的入院时间列表
        /// </summary>
        /// <param name="p_strInPatientID">病人住院ID</param>
        /// <param name="strTableName">指定表单的表名</param>
        /// <param name="strInDateArr">入院时间列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHasRecordInDateByInPatientID(string p_strInPatientID, string strTableName, out string[] strInDateArr)
        {
            long lngRes = 0;
            strInDateArr = null;
            string strSQl = string.Empty;
            string strTable = strTableName;
            clsHRPTableService objTabService = new clsHRPTableService();
            strSQl = "select distinct  inpatientdate from " + strTable + " where inpatientid=? and status<>'1'";
            try
            {
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    strInDateArr = new string[dtResult.Rows.Count];
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        strInDateArr[i] = dtResult.Rows[i][0].ToString().Trim();
                    }
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            return lngRes;


        }

    }
}