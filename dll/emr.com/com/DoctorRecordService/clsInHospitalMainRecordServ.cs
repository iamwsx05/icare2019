using System;
using System.EnterpriseServices;
using System.Data;
using System.Xml;
using System.Collections;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.PublicMiddleTier;
using com.digitalwave.Utility.SQLConvert;
using weCare.Core.Entity;

namespace com.digitalwave.InHospitalMainRecord
{
	/// <summary>
	/// Summary description for clsInHospitalMainRecordServ.
	/// 住院病案首页的Middle Tiers
	/// Alex 2003-03-13
	/// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsInHospitalMainRecordServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region SQL语句
        #region 新添记录
        /// <summary>
        /// 新添记录至主表
        /// </summary>
        private string m_strAddNewRecord = @"insert into inhospitalmainrecord (inpatientid,inpatientdate,opendate,createuserid,status,
diagnosisxml,inhospitaldiagnosisxml,maindiagnosisxml,icd_10ofmainxml,infectiondiagnosisxml,icd_10ofinfectionxml,pathologydiagnosisxml,
scachesourcexml,sensitivexml,hbsagxml,hcv_abxml,hiv_abxml,accordwithouthospitalxml,accordinwithoutxml,accordbfoprwithafxml,
accordclinicwithpathologyxml,accordradiatewithpathologyxml,salvetimesxml,salvesuccessxml,originaldiseasegyxml,originaldiseasetimesxml,
originaldiseasedaysxml,lymphgyxml,lymphtimesxml,lymphdaysxml,metastasisgyxml,metastasistimesxml,metastasisdaysxml,totalamtxml,
bedamtxml,nurseamtxml,wmamtxml,cmfinishedamtxml,cmsemifinishedamtxml,radiationamtxml,assayamtxml,o2amtxml,bloodamtxml,treatmentamtxml,
operationamtxml,deliverychildamtxml,checkamtxml,anaethesiaamtxml,babyamtxml,accompanyamtxml,otheramt1xml,otheramt2xml,otheramt3xml,
follow_weekxml,follow_monthxml,follow_yearxml,bloodtypexml,rbcxml,pltxml,plasmxml,wholebloodxml,otherbloodxml,consultationxml,
longdistanctconsultationxml,toplevelxml,nurselevelixml,nurseleveliixml,nurseleveliiixml,icuxml,specialnursexml,insurancenumxml,
modeofpaymentxml,patienthistorynoxml,mzicd10,mainicd10,ishandin) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,0)";

        //        /// <summary>
        //        /// 新添记录至子表
        //        /// </summary>
        //        private string m_strAddNewContent = @"insert into inhospitalmainrecord_content (inpatientid,inpatientdate,opendate,lastmodifydate,
        //lastmodifyuserid,status,diagnosis,inhospitaldiagnosis,doctor,confirmdiagnosisdate,condictionwhenin,maindiagnosis,mainconditionseq,
        //icd_10ofmain,infectiondiagnosis,infectioncondictionseq,icd_10ofinfection,pathologydiagnosis,scachesource,sensitive,hbsag,hcv_ab,hiv_ab,
        //accordwithouthospital,accordinwithout,accordbeforeoperationwithafter,accordclinicwithpathology,accordradiatewithpathology,salvetimes,
        //salvesuccess,directordt,subdirectordt,dt,inhospitaldt,attendinforadvancesstudydt,graduatestudentintern,intern,coder,quality,qcdt,qcnurse,
        //qctime,rtmodeseq,rtruleseq,rtco,rtaccelerator,rtx_ray,rtlacuna,originaldiseaseseq,originaldiseasegy,originaldiseasetimes,originaldiseasedays,
        //originaldiseasebegindate,originaldiseaseenddate,lymphseq,lymphgy,lymphtimes,lymphdays,lymphbegindate,lymphenddate,metastasisgy,
        //metastasistimes,metastasisdays,metastasisbegindate,metastasisenddate,chemotherapymodeseq,chemotherapywholebody,chemotherapylocal,
        //chemotherapyintubate,chemotherapythorax,chemotherapyabdomen,chemotherapyspinal,chemotherapyothertry,chemotherapyother,totalamt,bedamt,
        //nurseamt,wmamt,cmfinishedamt,cmsemifinishedamt,radiationamt,assayamt,o2amt,bloodamt,treatmentamt,operationamt,deliverychildamt,
        //checkamt,anaethesiaamt,babyamt,accompanyamt,otheramt1,otheramt2,otheramt3,corpsecheck,firstcase,follow,follow_week,follow_month,follow_year,
        //modelcase,bloodtype,bloodrh,bloodtransactoin,rbc,plt,plasm,wholeblood,otherblood,consultation,longdistanctconsultation,toplevel,nurseleveli,
        //nurselevelii,nurseleveliii,icu,specialnurse,insurancenum,modeofpayment,patienthistoryno,outpatientdate,birthplace,operation,baby,chemotherapy) 
        //values (?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?,?,?,?,?,?,
        //        ?,?,?,?,?)";

        /// <summary>
        /// 新添记录至子表
        /// </summary>
        private string m_strAddNewContent = @"insert into inhospitalmainrecord_content (inpatientid,inpatientdate,opendate,lastmodifydate,
lastmodifyuserid,status,diagnosis,inhospitaldiagnosis,doctor,confirmdiagnosisdate,condictionwhenin,maindiagnosis,mainconditionseq,
icd_10ofmain,infectiondiagnosis,infectioncondictionseq,icd_10ofinfection,pathologydiagnosis,scachesource,sensitive,hbsag,hcv_ab,hiv_ab,
accordwithouthospital,accordinwithout,accordbeforeoperationwithafter,accordclinicwithpathology,accordradiatewithpathology,salvetimes,
salvesuccess,directordt,subdirectordt,dt,inhospitaldt,attendinforadvancesstudydt,graduatestudentintern,intern,coder,quality,qcdt,qcnurse,
qctime,rtmodeseq,rtruleseq,rtco,rtaccelerator,rtx_ray,rtlacuna,originaldiseaseseq,originaldiseasegy,originaldiseasetimes,originaldiseasedays,
originaldiseasebegindate,originaldiseaseenddate,lymphseq,lymphgy,lymphtimes,lymphdays,lymphbegindate,lymphenddate,metastasisgy,
metastasistimes,metastasisdays,metastasisbegindate,metastasisenddate,chemotherapymodeseq,chemotherapywholebody,chemotherapylocal,
chemotherapyintubate,chemotherapythorax,chemotherapyabdomen,chemotherapyspinal,chemotherapyothertry,chemotherapyother,totalamt,bedamt,
nurseamt,wmamt,cmfinishedamt,cmsemifinishedamt,radiationamt,assayamt,o2amt,bloodamt,treatmentamt,operationamt,deliverychildamt,
checkamt,anaethesiaamt,babyamt,accompanyamt,otheramt1,otheramt2,otheramt3,corpsecheck,firstcase,follow,follow_week,follow_month,follow_year,
modelcase,bloodtype,bloodrh,bloodtransactoin,rbc,plt,plasm,wholeblood,otherblood,consultation,longdistanctconsultation,toplevel,nurseleveli,
nurselevelii,nurseleveliii,icu,specialnurse,insurancenum,modeofpayment,patienthistoryno,outpatientdate,birthplace,operation,baby,chemotherapy,path,
newbabyweight,newbabyinhostpitalweight,sszyj_jbbm,blzd_blh,blzd_jbbm,discharged_int,discharged_varh,readmitted31_int,readmitted31_varh,inrnssday,
inrnsshour,inrnssmin,outrnssday,outrnsshour,outrnssmin,inhospitalway,
medicalamt_new,treatmentamt_new,compositeeotheramt_new,pdamt_new,ldamt_new,idamt_new,cdamt_new,noopamt_new,opbytreatmentamt_new,physicalamt_new,
rehabilitationamt_new,cmtamt_new,aaamt_new,albuminamt_new,globulinamt_new,cfamt_new,cytokinesamt_new,onetimebysupmt_new,onetimebytmamt_new,onttimebyopamt_new,
tumor,t,n,m,installments,metastasiscount
) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?)";

        /// <summary>
        /// 新添记录至产科分娩婴儿记录表
        /// </summary>
        private string m_strAddNewBaby = @"insert into inhospitalmainrecord_baby (inpatientid,inpatientdate,opendate,lastmodifydate,lastmodifyuserid,status,
seqid,male,female,liveborn,dieborn,dienotborn,weight,die,changedepartment,outhospital,naturalcondiction,suffocate1,suffocate2,infectiontimes,
infectionname,icd10,salvetimes,salvesuccesstimes) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?)";

        /// <summary>
        /// 新添记录至肿瘤专科病人治疗记录表
        /// </summary>
        private string m_strAddNewChemotherapy = @"insert into ihmainrecord_chemotherapy (inpatientid,inpatientdate,opendate,lastmodifydate,lastmodifyuserid,status,seqid,
chemotherapydate,medicinename,period,field_cr,field_pr,field_mr,field_s,field_p,field_na) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?)";

        /// <summary>
        /// 新添记录至手术资料表
        /// </summary>
        private string m_strAddNewOperation = @"insert into inhospitalmainrecord_operation (inpatientid,inpatientdate,opendate,lastmodifydate,lastmodifyuserid,status,seqid,
operationid,operationdate,operationname,operator,assistant1,assistant2,aanaesthesiamodeid,cutlevel,anaesthetist,operationaanaesthesiamodename,operationanaesthetistname,operationlevel,operationelective) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        /// 新添记录至诊断表(入院诊断,医院感染诊断,出院其它诊断)
        /// </summary>
        private string m_strAddNewDiagnosis = @"insert into inhospitalmainrecord_diagnosis (inpatientid,inpatientdate,opendate,lastmodifydate,lastmodifyuserid,status,seqid,
diagnosistype,icd10,result,diagnosis) 
values (?,?,?,?,?,?,?,?,?,?,
        ?)";
        #endregion

        #region 修改主表记录
        /// <summary>
        /// 修改主表记录
        /// </summary>
        private string m_strModifyRecord = @"update inhospitalmainrecord  set 
diagnosisxml = ?,inhospitaldiagnosisxml = ?,maindiagnosisxml = ?,icd_10ofmainxml = ?,infectiondiagnosisxml = ?,icd_10ofinfectionxml = ?,pathologydiagnosisxml = ?,
scachesourcexml = ?,sensitivexml = ?,hbsagxml = ?,hcv_abxml = ?,hiv_abxml = ?,accordwithouthospitalxml = ?,accordinwithoutxml = ?,accordbfoprwithafxml = ?,
accordclinicwithpathologyxml = ?,accordradiatewithpathologyxml = ?,salvetimesxml = ?,salvesuccessxml = ?,originaldiseasegyxml = ?,originaldiseasetimesxml = ?,
originaldiseasedaysxml = ?,lymphgyxml = ?,lymphtimesxml = ?,lymphdaysxml = ?,metastasisgyxml = ?,metastasistimesxml = ?,metastasisdaysxml = ?,totalamtxml = ?,
bedamtxml = ?,nurseamtxml = ?,wmamtxml = ?,cmfinishedamtxml = ?,cmsemifinishedamtxml = ?,radiationamtxml = ?,assayamtxml = ?,o2amtxml = ?,bloodamtxml = ?,treatmentamtxml = ?,
operationamtxml = ?,deliverychildamtxml = ?,checkamtxml = ?,anaethesiaamtxml = ?,babyamtxml = ?,accompanyamtxml = ?,otheramt1xml = ?,otheramt2xml = ?,otheramt3xml = ?,
follow_weekxml = ?,follow_monthxml = ?,follow_yearxml = ?,bloodtypexml = ?,rbcxml = ?,pltxml = ?,plasmxml = ?,wholebloodxml = ?,otherbloodxml = ?,consultationxml = ?,
longdistanctconsultationxml = ?,toplevelxml = ?,nurselevelixml = ?,nurseleveliixml = ?,nurseleveliiixml = ?,icuxml = ?,specialnursexml = ?,insurancenumxml = ?,
modeofpaymentxml = ?,patienthistorynoxml = ?,mzicd10 = ?,mainicd10 = ?
 where inpatientid = ? and inpatientdate = ? and opendate = ? and status = 1";
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除主表记录
        /// </summary>
        private string m_strDelRecord = @"update inhospitalmainrecord set  status = 0, deactiveddate = ?, deactivedoperatorid = ? where inpatientid = ? and inpatientdate = ? and opendate = ? and status = 1";
        /// <summary>
        /// 删除子表记录
        /// </summary>
        private string m_strDelContent = @"update inhospitalmainrecord_content set  status = 0, deactiveddate = ?, deactivedoperatorid = ? where inpatientid = ? and inpatientdate = ? and opendate = ? and status = 1";
        /// <summary>
        /// 删除产科分娩婴儿记录
        /// </summary>
        private string m_strDelBaby = @"update inhospitalmainrecord_baby set  status = 0, deactiveddate = ?, deactivedoperatorid = ? where inpatientid = ? and inpatientdate = ? and opendate = ? and status = 1";
        /// <summary>
        /// 删除肿瘤专科病人治疗记录
        /// </summary>
        private string m_strDelChemotherapy = @"update ihmainrecord_chemotherapy set  status = 0, deactiveddate = ?, deactivedoperatorid = ? where inpatientid = ? and inpatientdate = ? and opendate = ? and status = 1";
        /// <summary>
        /// 删除手术资料
        /// </summary>
        private string m_strDelOperation = @"update inhospitalmainrecord_operation set  status = 0, deactiveddate = ?, deactivedoperatorid = ? where inpatientid = ? and inpatientdate = ? and opendate = ? and status = 1";
        /// <summary>
        /// 删除诊断
        /// </summary>
        private string m_strDelDiagnosis = @"update inhospitalmainrecord_diagnosis set  status = 0, deactiveddate = ?, deactivedoperatorid = ? where inpatientid = ? and inpatientdate = ? and opendate = ? and status = 1";
        #endregion

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFirstPrintDate(
            string p_strInPatientID, string p_strInPatientDate,/*string p_strOpenDate,*/out string p_strFirstPrintDate)
        {
            p_strFirstPrintDate = "";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetFirstPrintDate");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //查询第一次打印时间				
                string strCommand = "select firstprintdate  from inhospitalmainrecord where status=1 and inpatientid=? and inpatientdate=?";
                System.Data.DataTable dtbResult = new System.Data.DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref dtbResult, objDPArr);
                p_strFirstPrintDate = "";
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                    p_strFirstPrintDate = dtbResult.Rows[0][0].ToString();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            //返回
            return lngRes;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateFirstPrintDate(
            string p_strInPatientID, string p_strInPatientDate/*string p_strOpenDate,*/)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //更新第一次打印时间				
                string strCommand = "update inhospitalmainrecord set firstprintdate= ?  where firstprintdate is null and status=1 and inpatientid=? and inpatientdate= ? ";
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strCommand, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            //返回
            return lngRes;

        }

        #region 添加新纪录
        /// <summary>
        /// 添加新纪录(旧方法，已废弃)
        /// </summary>
        /// <param name="p_strMainXML"></param>
        /// <param name="p_strContentXML"></param>
        /// <param name="p_strOtherDiagnosisXMLArr"></param>
        /// <param name="p_strOperationXMLArr"></param>
        /// <param name="p_strBabyXMLArr"></param>
        /// <param name="m_strChemotherapyXMLArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNew(
            string p_strMainXML, string p_strContentXML, string[] p_strOtherDiagnosisXMLArr, string[] p_strOperationXMLArr, string[] p_strBabyXMLArr, string[] m_strChemotherapyXMLArr)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngAddNew");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strMainXML == null || p_strMainXML == "")
                    return -1;
                if (p_strContentXML == null || p_strContentXML == "")
                    return -1;

                lngRes = objHRPServ.add_new_record("InHospitalMainRecord", p_strMainXML);
                if (lngRes <= 0)
                    return -1;
                else
                    lngRes = objHRPServ.add_new_record("InHospitalMainRecord_Content", p_strContentXML);
                if (lngRes <= 0)
                    return -1;

                if (p_strOtherDiagnosisXMLArr != null && p_strOtherDiagnosisXMLArr.Length > 0)
                {
                    for (int i1 = 0; i1 < p_strOtherDiagnosisXMLArr.Length; i1++)
                    {
                        lngRes = objHRPServ.add_new_record("IHMainRecord_OtherDiagnosis", p_strOtherDiagnosisXMLArr[i1]);
                        if (lngRes < 0)
                            return -1;
                    }
                }

                if (p_strOperationXMLArr != null && p_strOperationXMLArr.Length > 0)
                {
                    for (int i1 = 0; i1 < p_strOperationXMLArr.Length; i1++)
                    {
                        lngRes = objHRPServ.add_new_record("InHospitalMainRecord_Operation", p_strOperationXMLArr[i1]);
                        if (lngRes < 0)
                            return -1;
                    }
                }

                if (p_strBabyXMLArr != null && p_strBabyXMLArr.Length > 0)
                {
                    for (int i1 = 0; i1 < p_strBabyXMLArr.Length; i1++)
                    {
                        lngRes = objHRPServ.add_new_record("InHospitalMainRecord_Baby", p_strBabyXMLArr[i1]);
                        if (lngRes < 0)
                            return -1;
                    }
                }

                if (m_strChemotherapyXMLArr != null && m_strChemotherapyXMLArr.Length > 0)
                {
                    for (int i1 = 0; i1 < m_strChemotherapyXMLArr.Length; i1++)
                    {
                        lngRes = objHRPServ.add_new_record("IHMainRecord_Chemotherapy", m_strChemotherapyXMLArr[i1]);
                        if (lngRes < 0)
                            return -1;
                    }
                }


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }		//返回
            return lngRes;
        }

        /// <summary>
        /// 新添病案首页
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCollection">病案首页内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNew( clsInHospitalMainRecord_Collection p_objCollection)
        {
            if (p_objCollection.m_objMain == null || p_objCollection.m_objMain == null || p_objCollection.m_objContent == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strNow = string.Empty;
                clsPublicMiddleTier objPM = new clsPublicMiddleTier();
                strNow = objPM.m_strGetDBServerTime( );
                if (string.IsNullOrEmpty(strNow))
                {
                    strNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                p_objCollection.m_objMain.m_strOpenDate = strNow;
                lngRes = m_lngAddNewMainRecord(p_objCollection.m_objMain);

                if (lngRes <= 0)
                {
                    return -1;
                }

                p_objCollection.m_objContent.m_strOpenDate = strNow;
                p_objCollection.m_objContent.m_strLastModifyDate = strNow;
                lngRes = m_lngAddNewContent(p_objCollection.m_objContent);

                if (lngRes <= 0)
                {
                    return -1;
                }

                if (p_objCollection.m_objOperationArr != null && p_objCollection.m_objOperationArr.Length > 0)
                {
                    for (int rows = 0; rows < p_objCollection.m_objOperationArr.Length; rows++)
                    {
                        p_objCollection.m_objOperationArr[rows].m_strOpenDate = strNow;
                        p_objCollection.m_objOperationArr[rows].m_strLastModifyDate = strNow;
                    }
                    lngRes = m_lngAddNewOperation(p_objCollection.m_objOperationArr);
                }

                if (p_objCollection.m_objChemotherapyArr != null && p_objCollection.m_objChemotherapyArr.Length > 0)
                {
                    for (int rows = 0; rows < p_objCollection.m_objChemotherapyArr.Length; rows++)
                    {
                        p_objCollection.m_objChemotherapyArr[rows].m_strOpenDate = strNow;
                        p_objCollection.m_objChemotherapyArr[rows].m_strLastModifyDate = strNow;
                    }
                    lngRes = m_lngAddNewChemotherapy(p_objCollection.m_objChemotherapyArr);
                }

                if (p_objCollection.m_objBabyArr != null && p_objCollection.m_objBabyArr.Length > 0)
                {
                    for (int rows = 0; rows < p_objCollection.m_objBabyArr.Length; rows++)
                    {
                        p_objCollection.m_objBabyArr[rows].m_strOpenDate = strNow;
                        p_objCollection.m_objBabyArr[rows].m_strLastModifyDate = strNow;
                    }
                    lngRes = m_lngAddNewBaby(p_objCollection.m_objBabyArr);
                }

                if (p_objCollection.m_objDiagnosisArr != null && p_objCollection.m_objDiagnosisArr.Length > 0)
                {
                    for (int rows = 0; rows < p_objCollection.m_objDiagnosisArr.Length; rows++)
                    {
                        p_objCollection.m_objDiagnosisArr[rows].m_strOPENDATE = strNow;
                        p_objCollection.m_objDiagnosisArr[rows].m_strLASTMODIFYDATE = strNow;
                    }
                    lngRes = m_lngAddNewDiagnosis(p_objCollection.m_objDiagnosisArr);
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

        #region 新添主表
        /// <summary>
        /// 新添主表
        /// </summary>
        /// <param name="p_objMainRecord">主表记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewMainRecord(clsInHospitalMainRecord_Main p_objMainRecord)
        {
            if (p_objMainRecord == null)
            {
                return -1;
            }

            long lngRes = 0;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(75, out objDPArr);
                objDPArr[0].Value = p_objMainRecord.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_objMainRecord.m_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_objMainRecord.m_strOpenDate);
                objDPArr[3].Value = p_objMainRecord.m_strCreateUserID;
                objDPArr[4].Value = 1;
                objDPArr[5].Value = p_objMainRecord.m_strDiagnosisXML;
                objDPArr[6].Value = DBNull.Value;
                objDPArr[7].Value = p_objMainRecord.m_strMainDiagnosisXML;
                objDPArr[8].Value = p_objMainRecord.m_strICD_10OfMainXML;
                objDPArr[9].Value = DBNull.Value;
                objDPArr[10].Value = DBNull.Value;
                objDPArr[11].Value = p_objMainRecord.m_strPathologyDiagnosisXML;
                objDPArr[12].Value = p_objMainRecord.m_strScacheSourceXML;
                objDPArr[13].Value = p_objMainRecord.m_strSensitiveXML;
                objDPArr[14].Value = p_objMainRecord.m_strHbsAgXML;
                objDPArr[15].Value = p_objMainRecord.m_strHCV_AbXML;
                objDPArr[16].Value = p_objMainRecord.m_strHIV_AbXML;
                objDPArr[17].Value = p_objMainRecord.m_strAccordWithOutHospitalXML;
                objDPArr[18].Value = p_objMainRecord.m_strAccordInWithOutXML;
                objDPArr[19].Value = p_objMainRecord.m_strAccordBeforeOperationWithAfterXML;
                objDPArr[20].Value = p_objMainRecord.m_strAccordClinicWithPathologyXML;
                objDPArr[21].Value = p_objMainRecord.m_strAccordRadiateWithPathologyXML;
                objDPArr[22].Value = p_objMainRecord.m_strSalveTimesXML;
                objDPArr[23].Value = p_objMainRecord.m_strSalveSuccessXML;
                objDPArr[24].Value = p_objMainRecord.m_strOriginalDiseaseGyXML;
                objDPArr[25].Value = p_objMainRecord.m_strOriginalDiseaseTimesXML;
                objDPArr[26].Value = p_objMainRecord.m_strOriginalDiseaseDaysXML;
                objDPArr[27].Value = p_objMainRecord.m_strLymphGyXML;
                objDPArr[28].Value = p_objMainRecord.m_strLymphTimesXML;
                objDPArr[29].Value = p_objMainRecord.m_strLymphDaysXML;
                objDPArr[30].Value = p_objMainRecord.m_strMetastasisGyXML;
                objDPArr[31].Value = p_objMainRecord.m_strMetastasisTimesXML;
                objDPArr[32].Value = p_objMainRecord.m_strMetastasisDaysXML;
                objDPArr[33].Value = p_objMainRecord.m_strTotalAmtXML;
                objDPArr[34].Value = p_objMainRecord.m_strBedAmtXML;
                objDPArr[35].Value = p_objMainRecord.m_strNurseAmtXML;
                objDPArr[36].Value = p_objMainRecord.m_strWMAmtXML;
                objDPArr[37].Value = p_objMainRecord.m_strCMFinishedAmtXML;
                objDPArr[38].Value = p_objMainRecord.m_strCMSemiFinishedAmtXML;
                objDPArr[39].Value = p_objMainRecord.m_strRadiationAmtXML;
                objDPArr[40].Value = p_objMainRecord.m_strAssayAmtXML;
                objDPArr[41].Value = p_objMainRecord.m_strO2AmtXML;
                objDPArr[42].Value = p_objMainRecord.m_strBloodAmtXML;
                objDPArr[43].Value = p_objMainRecord.m_strTreatmentAmtXML;
                objDPArr[44].Value = p_objMainRecord.m_strOperationAmtXML;
                objDPArr[45].Value = p_objMainRecord.m_strDeliveryChildAmtXML;
                objDPArr[46].Value = p_objMainRecord.m_strCheckAmtXML;
                objDPArr[47].Value = p_objMainRecord.m_strAnaethesiaAmtXML;
                objDPArr[48].Value = p_objMainRecord.m_strBabyAmtXML;
                objDPArr[49].Value = p_objMainRecord.m_strAccompanyAmtXML;
                objDPArr[50].Value = p_objMainRecord.m_strOtherAmt1XML;
                objDPArr[51].Value = p_objMainRecord.m_strOtherAmt2XML;
                objDPArr[52].Value = p_objMainRecord.m_strOtherAmt3XML;
                objDPArr[53].Value = p_objMainRecord.m_strFollow_WeekXML;
                objDPArr[54].Value = p_objMainRecord.m_strFollow_MonthXML;
                objDPArr[55].Value = p_objMainRecord.m_strFollow_YearXML;
                objDPArr[56].Value = p_objMainRecord.m_strBloodTypeXML;
                objDPArr[57].Value = p_objMainRecord.m_strRBCXML;
                objDPArr[58].Value = p_objMainRecord.m_strPLTXML;
                objDPArr[59].Value = p_objMainRecord.m_strPlasmXML;
                objDPArr[60].Value = p_objMainRecord.m_strWholeBloodXML;
                objDPArr[61].Value = p_objMainRecord.m_strOtherBloodXML;
                objDPArr[62].Value = p_objMainRecord.m_strConsultationXML;
                objDPArr[63].Value = p_objMainRecord.m_strLongDistanctConsultationXML;
                objDPArr[64].Value = p_objMainRecord.m_strTOPLevelXML;
                objDPArr[65].Value = p_objMainRecord.m_strNurseLevelIXML;
                objDPArr[66].Value = p_objMainRecord.m_strNurseLevelIIXML;
                objDPArr[67].Value = p_objMainRecord.m_strNurseLevelIIIXML;
                objDPArr[68].Value = p_objMainRecord.m_strICUXML;
                objDPArr[69].Value = p_objMainRecord.m_strSpecialNurseXML;
                objDPArr[70].Value = p_objMainRecord.m_strInsuranceNumXML;
                objDPArr[71].Value = p_objMainRecord.m_strModeOfPaymentXML;
                objDPArr[72].Value = p_objMainRecord.m_strPatientHistoryNOXML;
                objDPArr[73].Value = p_objMainRecord.m_strMZICD10;
                objDPArr[74].Value = p_objMainRecord.m_strMAINICD10;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strAddNewRecord, ref lngEff, objDPArr);
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

        #region 新添子表
        /// <summary>
        /// 新添子表
        /// </summary>
        /// <param name="p_objContent">子表内容</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewContent(clsInHospitalMainRecord_Content p_objContent)
        {
            if (p_objContent == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(168, out objDPArr);
                objDPArr[0].Value = p_objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_objContent.m_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_objContent.m_strOpenDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_objContent.m_strLastModifyDate);
                objDPArr[4].Value = p_objContent.m_strLastModifyUserID;
                objDPArr[5].Value = 1;
                objDPArr[6].Value = p_objContent.m_strDiagnosis;
                objDPArr[7].Value = DBNull.Value;
                objDPArr[8].Value = p_objContent.m_strDoctor;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = DateTime.Now;// Parse(p_objContent.m_strConfirmDiagnosisDate); 此字段新版不用
                objDPArr[10].Value = p_objContent.m_strCondictionWhenIn;
                objDPArr[11].Value = p_objContent.m_strMainDiagnosis;
                objDPArr[12].Value = p_objContent.m_strMainConditionSeq;
                objDPArr[13].Value = p_objContent.m_strICD_10OfMain;
                objDPArr[14].Value = DBNull.Value;
                objDPArr[15].Value = DBNull.Value;
                objDPArr[16].Value = DBNull.Value;
                objDPArr[17].Value = p_objContent.m_strPathologyDiagnosis;
                objDPArr[18].Value = p_objContent.m_strScacheSource;
                objDPArr[19].Value = p_objContent.m_strSensitive;
                objDPArr[20].Value = p_objContent.m_strHbsAg;
                objDPArr[21].Value = p_objContent.m_strHCV_Ab;
                objDPArr[22].Value = p_objContent.m_strHIV_Ab;
                objDPArr[23].Value = p_objContent.m_strAccordWithOutHospital;
                objDPArr[24].Value = p_objContent.m_strAccordInWithOut;
                objDPArr[25].Value = p_objContent.m_strAccordBeforeOperationWithAfter;
                objDPArr[26].Value = p_objContent.m_strAccordClinicWithPathology;
                objDPArr[27].Value = p_objContent.m_strAccordRadiateWithPathology;
                objDPArr[28].Value = p_objContent.m_strSalveTimes;
                objDPArr[29].Value = p_objContent.m_strSalveSuccess;
                objDPArr[30].Value = p_objContent.m_strDirectorDt;
                objDPArr[31].Value = p_objContent.m_strSubDirectorDt;
                objDPArr[32].Value = p_objContent.m_strDt;
                objDPArr[33].Value = p_objContent.m_strInHospitalDt;
                objDPArr[34].Value = p_objContent.m_strAttendInForAdvancesStudyDt;
                objDPArr[35].Value = p_objContent.m_strGraduateStudentIntern;
                objDPArr[36].Value = p_objContent.m_strIntern;
                objDPArr[37].Value = p_objContent.m_strCoder;
                objDPArr[38].Value = p_objContent.m_strQuality;
                objDPArr[39].Value = p_objContent.m_strQCDt;
                objDPArr[40].Value = p_objContent.m_strQCNurse;
                objDPArr[41].DbType = DbType.DateTime;
                objDPArr[41].Value = DateTime.Parse(p_objContent.m_strQCTime);
                objDPArr[42].Value = p_objContent.m_strRTModeSeq;
                objDPArr[43].Value = p_objContent.m_strRTRuleSeq;
                objDPArr[44].Value = p_objContent.m_strRTCo;
                objDPArr[45].Value = p_objContent.m_strRTAccelerator;
                objDPArr[46].Value = p_objContent.m_strRTX_Ray;
                objDPArr[47].Value = p_objContent.m_strRTLacuna;
                objDPArr[48].Value = p_objContent.m_strOriginalDiseaseSeq;
                objDPArr[49].Value = p_objContent.m_strOriginalDiseaseGy;
                objDPArr[50].Value = p_objContent.m_strOriginalDiseaseTimes;
                objDPArr[51].Value = p_objContent.m_strOriginalDiseaseDays;
                objDPArr[52].DbType = DbType.DateTime;
                objDPArr[52].Value = DateTime.Parse(p_objContent.m_strOriginalDiseaseBeginDate.Substring(0, 1) == " " ? "1900-01-01" : p_objContent.m_strOriginalDiseaseBeginDate);
                objDPArr[53].DbType = DbType.DateTime;
                objDPArr[53].Value = DateTime.Parse(p_objContent.m_strOriginalDiseaseEndDate.Substring(0, 1) == " " ? "1900-01-01" : p_objContent.m_strOriginalDiseaseEndDate);
                objDPArr[54].Value = p_objContent.m_strLymphSeq;
                objDPArr[55].Value = p_objContent.m_strLymphGy;
                objDPArr[56].Value = p_objContent.m_strLymphTimes;
                objDPArr[57].Value = p_objContent.m_strLymphDays;
                objDPArr[58].DbType = DbType.DateTime;
                objDPArr[58].Value = DateTime.Parse(p_objContent.m_strLymphBeginDate.Substring(0, 1) == " " ? "1900-01-01" : p_objContent.m_strLymphBeginDate);
                objDPArr[59].DbType = DbType.DateTime;
                objDPArr[59].Value = DateTime.Parse(p_objContent.m_strLymphEndDate.Substring(0, 1) == " " ? "1900-01-01" : p_objContent.m_strLymphEndDate);
                objDPArr[60].Value = p_objContent.m_strMetastasisGy;
                objDPArr[61].Value = p_objContent.m_strMetastasisTimes;
                objDPArr[62].Value = p_objContent.m_strMetastasisDays;
                objDPArr[63].DbType = DbType.DateTime;
                objDPArr[63].Value = DateTime.Parse(p_objContent.m_strMetastasisBeginDate.Substring(0, 1) == " " ? "1900-01-01" : p_objContent.m_strMetastasisBeginDate);
                objDPArr[64].DbType = DbType.DateTime;
                objDPArr[64].Value = DateTime.Parse(p_objContent.m_strMetastasisEndDate.Substring(0, 1) == " " ? "1900-01-01" : p_objContent.m_strMetastasisEndDate);
                objDPArr[65].Value = p_objContent.m_strChemotherapyModeSeq;
                objDPArr[66].Value = p_objContent.m_strChemotherapyWholeBody;
                objDPArr[67].Value = p_objContent.m_strChemotherapyLocal;
                objDPArr[68].Value = p_objContent.m_strChemotherapyIntubate;
                objDPArr[69].Value = p_objContent.m_strChemotherapyThorax;
                objDPArr[70].Value = p_objContent.m_strChemotherapyAbdomen;
                objDPArr[71].Value = p_objContent.m_strChemotherapySpinal;
                objDPArr[72].Value = p_objContent.m_strChemotherapyOtherTry;
                objDPArr[73].Value = p_objContent.m_strChemotherapyOther;
                objDPArr[74].Value = p_objContent.m_strTotalAmt;
                objDPArr[75].Value = p_objContent.m_strBedAmt;
                objDPArr[76].Value = p_objContent.m_strNurseAmt;
                objDPArr[77].Value = p_objContent.m_strWMAmt;
                objDPArr[78].Value = p_objContent.m_strCMFinishedAmt;
                objDPArr[79].Value = p_objContent.m_strCMSemiFinishedAmt;
                objDPArr[80].Value = p_objContent.m_strRadiationAmt;
                objDPArr[81].Value = p_objContent.m_strAssayAmt;
                objDPArr[82].Value = p_objContent.m_strO2Amt;
                objDPArr[83].Value = p_objContent.m_strBloodAmt;
                objDPArr[84].Value = p_objContent.m_strTreatmentAmt;
                objDPArr[85].Value = p_objContent.m_strOperationAmt;
                objDPArr[86].Value = p_objContent.m_strDeliveryChildAmt;
                objDPArr[87].Value = p_objContent.m_strCheckAmt;
                objDPArr[88].Value = p_objContent.m_strAnaethesiaAmt;
                objDPArr[89].Value = p_objContent.m_strBabyAmt;
                objDPArr[90].Value = p_objContent.m_strAccompanyAmt;
                objDPArr[91].Value = p_objContent.m_strOtherAmt1;
                objDPArr[92].Value = p_objContent.m_strOtherAmt2;
                objDPArr[93].Value = p_objContent.m_strOtherAmt3;
                objDPArr[94].Value = p_objContent.m_strCorpseCheck;
                objDPArr[95].Value = p_objContent.m_strFirstCase;
                objDPArr[96].Value = p_objContent.m_strFollow;
                objDPArr[97].Value = p_objContent.m_strFollow_Week;
                objDPArr[98].Value = p_objContent.m_strFollow_Month;
                objDPArr[99].Value = p_objContent.m_strFollow_Year;
                objDPArr[100].Value = p_objContent.m_strModelCase;
                objDPArr[101].Value = p_objContent.m_strBloodType;
                objDPArr[102].Value = p_objContent.m_strBloodRh;
                objDPArr[103].Value = p_objContent.m_strBloodTransActoin;
                objDPArr[104].Value = p_objContent.m_strRBC;
                objDPArr[105].Value = p_objContent.m_strPLT;
                objDPArr[106].Value = p_objContent.m_strPlasm;
                objDPArr[107].Value = p_objContent.m_strWholeBlood;
                objDPArr[108].Value = p_objContent.m_strOtherBlood;
                objDPArr[109].Value = p_objContent.m_strConsultation;
                objDPArr[110].Value = p_objContent.m_strLongDistanctConsultation;
                objDPArr[111].Value = p_objContent.m_strTOPLevel;
                objDPArr[112].Value = p_objContent.m_strNurseLevelI;
                objDPArr[113].Value = p_objContent.m_strNurseLevelII;
                objDPArr[114].Value = p_objContent.m_strNurseLevelIII;
                objDPArr[115].Value = p_objContent.m_strICU;
                objDPArr[116].Value = p_objContent.m_strSpecialNurse;
                objDPArr[117].Value = p_objContent.m_strInsuranceNum;
                if (p_objContent.m_strModeOfPayment == "自费")
                    objDPArr[118].Value = "全自费";
                else
                    objDPArr[118].Value = p_objContent.m_strModeOfPayment;
                objDPArr[119].Value = p_objContent.m_strPatientHistoryNO;
                DateTime dtmTemp = DateTime.MinValue;
                if (DateTime.TryParse(p_objContent.m_strOutPatientDate, out dtmTemp))
                {
                    objDPArr[120].DbType = DbType.DateTime;
                    objDPArr[120].Value = dtmTemp;
                }
                else
                {
                    objDPArr[120].Value = DBNull.Value;
                }
                objDPArr[121].Value = p_objContent.m_strBirthPlace;
                objDPArr[122].Value = p_objContent.m_strOperation;
                objDPArr[123].Value = p_objContent.m_strBaby;
                objDPArr[124].Value = p_objContent.m_strChemotherapy;
                objDPArr[125].Value = p_objContent.m_strpath;
                /********************************************/
                objDPArr[126].Value = p_objContent.m_strNewBabyWeight;
                objDPArr[127].Value = p_objContent.m_strNewBabyInhostpitalWeight;
                objDPArr[128].Value = p_objContent.m_strSSZYJ_jbbm;
                objDPArr[129].Value = p_objContent.m_strBLZD_blh;
                objDPArr[130].Value = p_objContent.m_strBLZD_jbbm;
                objDPArr[131].Value = p_objContent.m_intDischarged;
                objDPArr[132].Value = p_objContent.m_strDischargedHospitalName;
                objDPArr[133].Value = p_objContent.m_intReadmitted31;
                objDPArr[134].Value = p_objContent.m_strReadmitted31;
                objDPArr[135].Value = p_objContent.m_strInRnssDay;
                objDPArr[136].Value = p_objContent.m_strInRnssHour;
                objDPArr[137].Value = p_objContent.m_strInRnssMin;
                objDPArr[138].Value = p_objContent.m_strOutRnssDay;
                objDPArr[139].Value = p_objContent.m_strOutRnssHour;
                objDPArr[140].Value = p_objContent.m_strOutRnssMin;
                objDPArr[141].Value = p_objContent.m_strInhospitalWay;
                /********************************************/
                objDPArr[142].Value = p_objContent.m_strMEDICALAMT_NEW;
                objDPArr[143].Value = p_objContent.m_strTREATMENTAMT_NEW;
                objDPArr[144].Value = p_objContent.m_strCOMPOSITEEOTHERAMT_NEW;
                objDPArr[145].Value = p_objContent.m_strPDAMT_NEW;
                objDPArr[146].Value = p_objContent.m_strLDAMT_NEW;
                objDPArr[147].Value = p_objContent.m_strIDAMT_NEW;
                objDPArr[148].Value = p_objContent.m_strCDAMT_NEW;
                objDPArr[149].Value = p_objContent.m_strNOOPAMT_NEW;
                objDPArr[150].Value = p_objContent.m_strOPBYTREATMENTAMT_NEW;
                objDPArr[151].Value = p_objContent.m_strPHYSICALAMT_NEW;
                objDPArr[152].Value = p_objContent.m_strREHABILITATIONAMT_NEW;
                objDPArr[153].Value = p_objContent.m_strCMTAMT_NEW;
                objDPArr[154].Value = p_objContent.m_strAAAMT_NEW;
                objDPArr[155].Value = p_objContent.m_strALBUMINAMT_NEW;
                objDPArr[156].Value = p_objContent.m_strGLOBULINAMT_NEW;
                objDPArr[157].Value = p_objContent.m_strCFAMT_NEW;
                objDPArr[158].Value = p_objContent.m_strCYTOKINESAMT_NEW;
                objDPArr[159].Value = p_objContent.m_strONETIMEBYSUPMT_NEW;
                objDPArr[160].Value = p_objContent.m_strONETIMEBYTMAMT_NEW;
                objDPArr[161].Value = p_objContent.m_strONTTIMEBYOPAMT_NEW;

                objDPArr[162].Value = Convert.ToInt32(p_objContent.m_strTumor);
                objDPArr[163].Value = Convert.ToInt32(p_objContent.m_strT);
                objDPArr[164].Value = Convert.ToInt32(p_objContent.m_strN);
                objDPArr[165].Value = Convert.ToInt32(p_objContent.m_strM);
                objDPArr[166].Value = p_objContent.m_strInstallments;
                objDPArr[167].Value = p_objContent.m_strMetastaisCount;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strAddNewContent, ref lngEff, objDPArr);
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

        #region 新添手术资料
        /// <summary>
        /// 新添手术资料
        /// </summary>
        /// <param name="p_objOperationArr">手术资料</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewOperation(clsInHospitalMainRecord_Operation[] p_objOperationArr)
        {
            if (p_objOperationArr == null || p_objOperationArr.Length <= 0)
            {
                return -1;
            }

            long lngRes = 0;
            long lngEff = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;
                    DateTime dtmTemp = DateTime.Now;
                    for (int rows = 0; rows < p_objOperationArr.Length; rows++)
                    {
                        objHRPServ.CreateDatabaseParameter(20, out objDPArr);
                        objDPArr[0].Value = p_objOperationArr[rows].m_strInPatientID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objOperationArr[rows].m_strInPatientDate);
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_objOperationArr[rows].m_strOpenDate);
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(p_objOperationArr[rows].m_strLastModifyDate);
                        objDPArr[4].Value = p_objOperationArr[rows].m_strLastModifyUserID;
                        objDPArr[5].Value = 1;
                        objDPArr[6].Value = p_objOperationArr[rows].m_strSeqID;
                        objDPArr[7].Value = p_objOperationArr[rows].m_strOperationID;
                        if (DateTime.TryParse(p_objOperationArr[rows].m_strOperationDate, out dtmTemp))
                        {
                            objDPArr[8].DbType = DbType.DateTime;
                            objDPArr[8].Value = DateTime.Parse(p_objOperationArr[rows].m_strOperationDate);
                        }
                        else
                        {
                            objDPArr[8].Value = DBNull.Value;
                        }
                        objDPArr[9].Value = p_objOperationArr[rows].m_strOperationName;
                        //objDPArr[10].Value = p_objOperationArr[rows].m_strOperator;// m_strOperatorName;// m_strOperator;
                        //objDPArr[11].Value = p_objOperationArr[rows].m_strAssistant1;//m_strAssistant1Name;// 
                        //objDPArr[12].Value = p_objOperationArr[rows].m_strAssistant2;//m_strAssistant2Name;// 
                        //objDPArr[13].Value = p_objOperationArr[rows].m_strAanaesthesiaModeID;
                        //objDPArr[14].Value = p_objOperationArr[rows].m_strCutLevel;
                        //objDPArr[15].Value = p_objOperationArr[rows].m_strAnaesthetist;//m_strAnaesthetistName;// 
                        objDPArr[10].Value = p_objOperationArr[rows].m_strOperator;
                        objDPArr[11].Value = p_objOperationArr[rows].m_strAssistant1;
                        objDPArr[12].Value = p_objOperationArr[rows].m_strAssistant2;
                        objDPArr[13].Value = p_objOperationArr[rows].m_strAanaesthesiaModeID;
                        objDPArr[14].Value = p_objOperationArr[rows].m_strCutLevel;
                        objDPArr[15].Value = p_objOperationArr[rows].m_strAnaesthetist;
                        objDPArr[16].Value = p_objOperationArr[rows].m_strAanaesthesiaModeName;
                        objDPArr[17].Value = p_objOperationArr[rows].m_strAnaesthetistName;
                        objDPArr[18].Value = p_objOperationArr[rows].m_strOpreationLevel;
                        objDPArr[19].Value = p_objOperationArr[rows].m_strOpreationElective;

                        lngRes = objHRPServ.lngExecuteParameterSQL(m_strAddNewOperation, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.DateTime, DbType.DateTime, DbType.DateTime, DbType.String, DbType.String, DbType.String, DbType.String, DbType.DateTime,
                    DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String};
                    object[][] objValues = new object[20][];

                    int intItemCount = p_objOperationArr.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//初始化
                    }

                    DateTime dtmTemp = DateTime.Now;
                    for (int rows = 0; rows < intItemCount; rows++)
                    {
                        objValues[0][rows] = p_objOperationArr[rows].m_strInPatientID;
                        objValues[1][rows] = DateTime.Parse(p_objOperationArr[rows].m_strInPatientDate);
                        objValues[2][rows] = DateTime.Parse(p_objOperationArr[rows].m_strOpenDate);
                        objValues[3][rows] = DateTime.Parse(p_objOperationArr[rows].m_strLastModifyDate);
                        objValues[4][rows] = p_objOperationArr[rows].m_strLastModifyUserID;
                        objValues[5][rows] = 1;
                        objValues[6][rows] = p_objOperationArr[rows].m_strSeqID;
                        objValues[7][rows] = p_objOperationArr[rows].m_strOperationID;
                        if (DateTime.TryParse(p_objOperationArr[rows].m_strOperationDate, out dtmTemp))
                        {
                            objValues[8][rows] = DateTime.Parse(p_objOperationArr[rows].m_strOperationDate);
                        }
                        else
                        {
                            objValues[8][rows] = DBNull.Value;
                        }
                        objValues[9][rows] = p_objOperationArr[rows].m_strOperationName;
                        objValues[10][rows] = p_objOperationArr[rows].m_strOperator;// m_strOperatorName;// m_strOperator;
                        objValues[11][rows] = p_objOperationArr[rows].m_strAssistant1;// m_strAssistant1Name;// m_strAssistant1;
                        objValues[12][rows] = p_objOperationArr[rows].m_strAssistant2;// m_strAssistant2Name;// m_strAssistant2;
                        objValues[13][rows] = p_objOperationArr[rows].m_strAanaesthesiaModeID;
                        objValues[14][rows] = p_objOperationArr[rows].m_strCutLevel;
                        objValues[15][rows] = p_objOperationArr[rows].m_strAnaesthetist; //m_strAnaesthetistName;// m_strAnaesthetist;
                        objValues[16][rows] = p_objOperationArr[rows].m_strAanaesthesiaModeName;
                        objValues[17][rows] = p_objOperationArr[rows].m_strAnaesthetistName;
                        objValues[18][rows] = p_objOperationArr[rows].m_strOpreationLevel;
                        objValues[19][rows] = p_objOperationArr[rows].m_strOpreationElective;

                        //m_strAnaesthetistName 麻醉医师
                        //m_strAanaesthesiaModeName  麻醉方式
                        //m_strAnaesthetistName 麻醉医师
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(m_strAddNewOperation, objValues, dbTypes);
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

        #region 新添肿瘤专科病人治疗记录
        /// <summary>
        /// 新添肿瘤专科病人治疗记录
        /// </summary>
        /// <param name="p_objChemotherapyArr">肿瘤专科病人治疗记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewChemotherapy(clsInHospitalMainRecord_Chemotherapy[] p_objChemotherapyArr)
        {
            if (p_objChemotherapyArr == null || p_objChemotherapyArr.Length <= 0)
            {
                return -1;
            }

            long lngRes = 0;
            long lngEff = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;
                    for (int rows = 0; rows < p_objChemotherapyArr.Length; rows++)
                    {
                        objHRPServ.CreateDatabaseParameter(16, out objDPArr);
                        objDPArr[0].Value = p_objChemotherapyArr[rows].m_strInPatientID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objChemotherapyArr[rows].m_strInPatientDate);
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_objChemotherapyArr[rows].m_strOpenDate);
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(p_objChemotherapyArr[rows].m_strLastModifyDate);
                        objDPArr[4].Value = p_objChemotherapyArr[rows].m_strLastModifyUserID;
                        objDPArr[5].Value = 1;
                        objDPArr[6].Value = p_objChemotherapyArr[rows].m_strSeqID;
                        objDPArr[7].DbType = DbType.DateTime;
                        objDPArr[7].Value = DateTime.Parse(p_objChemotherapyArr[rows].m_strChemotherapyDate);
                        objDPArr[8].Value = p_objChemotherapyArr[rows].m_strMedicineName;
                        objDPArr[9].Value = p_objChemotherapyArr[rows].m_strPeriod;
                        objDPArr[10].Value = p_objChemotherapyArr[rows].m_strField_CR;
                        objDPArr[11].Value = p_objChemotherapyArr[rows].m_strField_PR;
                        objDPArr[12].Value = p_objChemotherapyArr[rows].m_strField_MR;
                        objDPArr[13].Value = p_objChemotherapyArr[rows].m_strField_S;
                        objDPArr[14].Value = p_objChemotherapyArr[rows].m_strField_P;
                        objDPArr[15].Value = p_objChemotherapyArr[rows].m_strField_NA;

                        lngRes = objHRPServ.lngExecuteParameterSQL(m_strAddNewChemotherapy, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.DateTime, DbType.DateTime, DbType.DateTime, DbType.String, DbType.String, DbType.String,  DbType.DateTime,
                    DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String};
                    object[][] objValues = new object[16][];

                    int intItemCount = p_objChemotherapyArr.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//初始化
                    }

                    for (int rows = 0; rows < intItemCount; rows++)
                    {
                        objValues[0][rows] = p_objChemotherapyArr[rows].m_strInPatientID;
                        objValues[1][rows] = DateTime.Parse(p_objChemotherapyArr[rows].m_strInPatientDate);
                        objValues[2][rows] = DateTime.Parse(p_objChemotherapyArr[rows].m_strOpenDate);
                        objValues[3][rows] = DateTime.Parse(p_objChemotherapyArr[rows].m_strLastModifyDate);
                        objValues[4][rows] = p_objChemotherapyArr[rows].m_strLastModifyUserID;
                        objValues[5][rows] = 1;
                        objValues[6][rows] = p_objChemotherapyArr[rows].m_strSeqID;
                        objValues[7][rows] = DateTime.Parse(p_objChemotherapyArr[rows].m_strChemotherapyDate);
                        objValues[8][rows] = p_objChemotherapyArr[rows].m_strMedicineName;
                        objValues[9][rows] = p_objChemotherapyArr[rows].m_strPeriod;
                        objValues[10][rows] = p_objChemotherapyArr[rows].m_strField_CR;
                        objValues[11][rows] = p_objChemotherapyArr[rows].m_strField_PR;
                        objValues[12][rows] = p_objChemotherapyArr[rows].m_strField_MR;
                        objValues[13][rows] = p_objChemotherapyArr[rows].m_strField_S;
                        objValues[14][rows] = p_objChemotherapyArr[rows].m_strField_P;
                        objValues[15][rows] = p_objChemotherapyArr[rows].m_strField_NA;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(m_strAddNewChemotherapy, objValues, dbTypes);
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

        #region 新添产科分娩婴儿记录
        /// <summary>
        /// 新添产科分娩婴儿记录
        /// </summary>
        /// <param name="p_objBabyArr">产科分娩婴儿记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewBaby(clsInHospitalMainRecord_Baby[] p_objBabyArr)
        {
            if (p_objBabyArr == null || p_objBabyArr.Length <= 0)
            {
                return -1;
            }

            long lngRes = 0;
            long lngEff = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;
                    for (int rows = 0; rows < p_objBabyArr.Length; rows++)
                    {
                        objHRPServ.CreateDatabaseParameter(24, out objDPArr);
                        objDPArr[0].Value = p_objBabyArr[rows].m_strInPatientID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objBabyArr[rows].m_strInPatientDate);
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_objBabyArr[rows].m_strOpenDate);
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(p_objBabyArr[rows].m_strLastModifyDate);
                        objDPArr[4].Value = p_objBabyArr[rows].m_strLastModifyUserID;
                        objDPArr[5].Value = 1;
                        objDPArr[6].Value = p_objBabyArr[rows].m_strSeqID;
                        objDPArr[7].Value = p_objBabyArr[rows].m_strMale;
                        objDPArr[8].Value = p_objBabyArr[rows].m_strFemale;
                        objDPArr[9].Value = p_objBabyArr[rows].m_strLiveBorn;
                        objDPArr[10].Value = p_objBabyArr[rows].m_strDieBorn;
                        objDPArr[11].Value = p_objBabyArr[rows].m_strDieNotBorn;
                        objDPArr[12].Value = p_objBabyArr[rows].m_strWeight;
                        objDPArr[13].Value = p_objBabyArr[rows].m_strDie;
                        objDPArr[14].Value = p_objBabyArr[rows].m_strChangeDepartment;
                        objDPArr[15].Value = p_objBabyArr[rows].m_strOutHospital;
                        objDPArr[16].Value = p_objBabyArr[rows].m_strNaturalCondiction;
                        objDPArr[17].Value = p_objBabyArr[rows].m_strSuffocate1;
                        objDPArr[18].Value = p_objBabyArr[rows].m_strSuffocate2;
                        objDPArr[19].Value = p_objBabyArr[rows].m_strInfectionTimes;
                        objDPArr[20].Value = p_objBabyArr[rows].m_strInfectionName;
                        objDPArr[21].Value = p_objBabyArr[rows].m_strICD10;
                        objDPArr[22].Value = p_objBabyArr[rows].m_strSalveTimes;
                        objDPArr[23].Value = p_objBabyArr[rows].m_strSalveSuccessTimes;

                        lngRes = objHRPServ.lngExecuteParameterSQL(m_strAddNewChemotherapy, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.DateTime, DbType.DateTime, DbType.DateTime, DbType.String, DbType.String, DbType.String,  DbType.String,
                    DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String
                    ,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String};

                    object[][] objValues = new object[24][];

                    int intItemCount = p_objBabyArr.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//初始化
                    }

                    for (int rows = 0; rows < intItemCount; rows++)
                    {
                        objValues[0][rows] = p_objBabyArr[rows].m_strInPatientID;
                        objValues[1][rows] = DateTime.Parse(p_objBabyArr[rows].m_strInPatientDate);
                        objValues[2][rows] = DateTime.Parse(p_objBabyArr[rows].m_strOpenDate);
                        objValues[3][rows] = DateTime.Parse(p_objBabyArr[rows].m_strLastModifyDate);
                        objValues[4][rows] = p_objBabyArr[rows].m_strLastModifyUserID;
                        objValues[5][rows] = 1;
                        objValues[6][rows] = p_objBabyArr[rows].m_strSeqID;
                        objValues[7][rows] = p_objBabyArr[rows].m_strMale;
                        objValues[8][rows] = p_objBabyArr[rows].m_strFemale;
                        objValues[9][rows] = p_objBabyArr[rows].m_strLiveBorn;
                        objValues[10][rows] = p_objBabyArr[rows].m_strDieBorn;
                        objValues[11][rows] = p_objBabyArr[rows].m_strDieNotBorn;
                        objValues[12][rows] = p_objBabyArr[rows].m_strWeight;
                        objValues[13][rows] = p_objBabyArr[rows].m_strDie;
                        objValues[14][rows] = p_objBabyArr[rows].m_strChangeDepartment;
                        objValues[15][rows] = p_objBabyArr[rows].m_strOutHospital;
                        objValues[16][rows] = p_objBabyArr[rows].m_strNaturalCondiction;
                        objValues[17][rows] = p_objBabyArr[rows].m_strSuffocate1;
                        objValues[18][rows] = p_objBabyArr[rows].m_strSuffocate2;
                        objValues[19][rows] = p_objBabyArr[rows].m_strInfectionTimes;
                        objValues[20][rows] = p_objBabyArr[rows].m_strInfectionName;
                        objValues[21][rows] = p_objBabyArr[rows].m_strICD10;
                        objValues[22][rows] = p_objBabyArr[rows].m_strSalveTimes;
                        objValues[23][rows] = p_objBabyArr[rows].m_strSalveSuccessTimes;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(m_strAddNewBaby, objValues, dbTypes);
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

        #region 新添诊断
        /// <summary>
        /// 新添诊断
        /// </summary>
        /// <param name="p_objDiagnosisArr">诊断内容</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewDiagnosis(clsInHospitalMainRecord_Diagnosis[] p_objDiagnosisArr)
        {
            if (p_objDiagnosisArr == null || p_objDiagnosisArr.Length <= 0)
            {
                return -1;
            }

            long lngRes = 0;
            long lngEff = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;
                    for (int rows = 0; rows < p_objDiagnosisArr.Length; rows++)
                    {
                        objHRPServ.CreateDatabaseParameter(11, out objDPArr);
                        objDPArr[0].Value = p_objDiagnosisArr[rows].m_strINPATIENTID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objDiagnosisArr[rows].m_strINPATIENTDATE);
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_objDiagnosisArr[rows].m_strOPENDATE);
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(p_objDiagnosisArr[rows].m_strLASTMODIFYDATE);
                        objDPArr[4].Value = p_objDiagnosisArr[rows].m_strLASTMODIFYUSERID;
                        objDPArr[5].Value = 1;
                        objDPArr[6].Value = p_objDiagnosisArr[rows].m_strSEQID;
                        objDPArr[7].Value = p_objDiagnosisArr[rows].m_strDIAGNOSISTYPE;
                        objDPArr[8].Value = p_objDiagnosisArr[rows].m_strICD10;
                        objDPArr[9].Value = p_objDiagnosisArr[rows].m_strRESULT;
                        objDPArr[10].Value = p_objDiagnosisArr[rows].m_strDIAGNOSIS;
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.DateTime, DbType.DateTime, DbType.DateTime, DbType.String, DbType.String, DbType.String,  DbType.String,
                    DbType.String,DbType.String,DbType.String};

                    object[][] objValues = new object[11][];

                    int intItemCount = p_objDiagnosisArr.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//初始化
                    }

                    for (int rows = 0; rows < intItemCount; rows++)
                    {
                        objValues[0][rows] = p_objDiagnosisArr[rows].m_strINPATIENTID;
                        objValues[1][rows] = DateTime.Parse(p_objDiagnosisArr[rows].m_strINPATIENTDATE);
                        objValues[2][rows] = DateTime.Parse(p_objDiagnosisArr[rows].m_strOPENDATE);
                        objValues[3][rows] = DateTime.Parse(p_objDiagnosisArr[rows].m_strLASTMODIFYDATE);
                        objValues[4][rows] = p_objDiagnosisArr[rows].m_strLASTMODIFYUSERID;
                        objValues[5][rows] = 1;
                        objValues[6][rows] = p_objDiagnosisArr[rows].m_strSEQID;
                        objValues[7][rows] = p_objDiagnosisArr[rows].m_strDIAGNOSISTYPE;
                        objValues[8][rows] = p_objDiagnosisArr[rows].m_strICD10;
                        objValues[9][rows] = p_objDiagnosisArr[rows].m_strRESULT;
                        objValues[10][rows] = p_objDiagnosisArr[rows].m_strDIAGNOSIS;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(m_strAddNewDiagnosis, objValues, dbTypes);
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
        #endregion

        #region 修改记录
        /// <summary>
        /// 修改记录(旧方法，已废弃)
        /// </summary>
        /// <param name="p_strMainXML"></param>
        /// <param name="p_strContentXML"></param>
        /// <param name="p_strOtherDiagnosisXMLArr"></param>
        /// <param name="p_strOperationXMLArr"></param>
        /// <param name="p_strBabyXMLArr"></param>
        /// <param name="m_strChemotherapyXMLArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModify( string p_strInpatientId, string p_strInpateintDate, string p_strOpenDate, string p_strEmpId,
            string p_strMainXML, string p_strContentXML, string[] p_strOtherDiagnosisXMLArr, string[] p_strOperationXMLArr, string[] p_strBabyXMLArr, string[] m_strChemotherapyXMLArr)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInHospitalMainRecordServ","m_lngModify");
                //if(lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strMainXML == null || p_strMainXML == "")
                    return -1;

                if (p_strContentXML == null || p_strContentXML == "")
                    return -1;

                lngRes = objHRPServ.modify_record("INHOSPITALMAINRECORD", p_strMainXML, new string[] { "INPATIENTID", "INPATIENTDATE", "OPENDATE" });
                if (lngRes <= 0)
                    return -1;

                lngRes = m_lngDeleteSubRecord("InHospitalMainRecord_Content", p_strInpatientId, p_strInpateintDate, p_strOpenDate, p_strEmpId);
                lngRes = objHRPServ.add_new_record("InHospitalMainRecord_Content", p_strContentXML);

                if (lngRes <= 0)
                    return -1;

                lngRes = m_lngDeleteSubRecord("IHMainRecord_OtherDiagnosis", p_strInpatientId, p_strInpateintDate, p_strOpenDate, p_strEmpId);
                lngRes = m_lngDeleteSubRecord("InHospitalMainRecord_Operation", p_strInpatientId, p_strInpateintDate, p_strOpenDate, p_strEmpId);
                lngRes = m_lngDeleteSubRecord("IHMainRecord_Chemotherapy", p_strInpatientId, p_strInpateintDate, p_strOpenDate, p_strEmpId);
                lngRes = m_lngDeleteSubRecord("InHospitalMainRecord_Baby", p_strInpatientId, p_strInpateintDate, p_strOpenDate, p_strEmpId);

                if (p_strOtherDiagnosisXMLArr != null && p_strOtherDiagnosisXMLArr.Length > 0)
                {
                    for (int i1 = 0; i1 < p_strOtherDiagnosisXMLArr.Length; i1++)
                    {
                        lngRes = objHRPServ.add_new_record("IHMainRecord_OtherDiagnosis", p_strOtherDiagnosisXMLArr[i1]);
                        if (lngRes < 0)
                            return -1;
                    }
                }

                if (p_strOperationXMLArr != null && p_strOperationXMLArr.Length > 0)
                {
                    for (int i1 = 0; i1 < p_strOperationXMLArr.Length; i1++)
                    {
                        lngRes = objHRPServ.add_new_record("InHospitalMainRecord_Operation", p_strOperationXMLArr[i1]);
                        if (lngRes < 0)
                            return -1;
                    }
                }

                if (p_strBabyXMLArr != null && p_strBabyXMLArr.Length > 0)
                {
                    for (int i1 = 0; i1 < p_strBabyXMLArr.Length; i1++)
                    {
                        lngRes = objHRPServ.add_new_record("InHospitalMainRecord_Baby", p_strBabyXMLArr[i1]);
                        if (lngRes < 0)
                            return -1;
                    }
                }

                if (m_strChemotherapyXMLArr != null && m_strChemotherapyXMLArr.Length > 0)
                {
                    for (int i1 = 0; i1 < m_strChemotherapyXMLArr.Length; i1++)
                    {
                        lngRes = objHRPServ.add_new_record("IHMainRecord_Chemotherapy", m_strChemotherapyXMLArr[i1]);
                        if (lngRes < 0)
                            return -1;
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
        /// 修改病案首页
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCollection">病案首页内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModify( clsInHospitalMainRecord_Collection p_objCollection)
        {
            if (p_objCollection == null || p_objCollection.m_objMain == null || p_objCollection.m_objContent == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strNow = string.Empty;
                clsPublicMiddleTier objPM = new clsPublicMiddleTier();
                strNow = objPM.m_strGetDBServerTime( );
                if (string.IsNullOrEmpty(strNow))
                {
                    strNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                lngRes = m_lngModifyMainRecord(p_objCollection.m_objMain);

                if (lngRes <= 0)
                {
                    return -1;
                }

                lngRes = m_lngDeleteContent(p_objCollection.m_objContent.m_strInPatientID, p_objCollection.m_objContent.m_strInPatientDate, p_objCollection.m_objContent.m_strOpenDate, p_objCollection.m_objContent.m_strLastModifyUserID, DateTime.Parse(strNow));
                if (lngRes <= 0)
                {
                    return -1;
                }

                p_objCollection.m_objContent.m_strLastModifyDate = strNow;
                lngRes = m_lngAddNewContent(p_objCollection.m_objContent);
                if (lngRes <= 0)
                {
                    return -1;
                }

                lngRes = m_lngDeleteOperation(p_objCollection.m_objContent.m_strInPatientID, p_objCollection.m_objContent.m_strInPatientDate, p_objCollection.m_objContent.m_strOpenDate, p_objCollection.m_objContent.m_strLastModifyUserID, DateTime.Parse(strNow));
                if (p_objCollection.m_objOperationArr != null && p_objCollection.m_objOperationArr.Length > 0)
                {
                    for (int rows = 0; rows < p_objCollection.m_objOperationArr.Length; rows++)
                    {
                        p_objCollection.m_objOperationArr[rows].m_strLastModifyDate = strNow;
                    }
                    lngRes = m_lngAddNewOperation(p_objCollection.m_objOperationArr);
                }

                lngRes = m_lngDeleteBaby(p_objCollection.m_objContent.m_strInPatientID, p_objCollection.m_objContent.m_strInPatientDate, p_objCollection.m_objContent.m_strOpenDate, p_objCollection.m_objContent.m_strLastModifyUserID, DateTime.Parse(strNow));
                if (p_objCollection.m_objBabyArr != null && p_objCollection.m_objBabyArr.Length > 0)
                {
                    for (int rows = 0; rows < p_objCollection.m_objBabyArr.Length; rows++)
                    {
                        p_objCollection.m_objBabyArr[rows].m_strLastModifyDate = strNow;
                    }
                    lngRes = m_lngAddNewBaby(p_objCollection.m_objBabyArr);
                }

                lngRes = m_lngDeleteChemotherapy(p_objCollection.m_objContent.m_strInPatientID, p_objCollection.m_objContent.m_strInPatientDate, p_objCollection.m_objContent.m_strOpenDate, p_objCollection.m_objContent.m_strLastModifyUserID, DateTime.Parse(strNow));
                if (p_objCollection.m_objChemotherapyArr != null && p_objCollection.m_objChemotherapyArr.Length > 0)
                {
                    for (int rows = 0; rows < p_objCollection.m_objChemotherapyArr.Length; rows++)
                    {
                        p_objCollection.m_objChemotherapyArr[rows].m_strLastModifyDate = strNow;
                    }
                    lngRes = m_lngAddNewChemotherapy(p_objCollection.m_objChemotherapyArr);
                }

                lngRes = m_lngDeleteDiagnosis(p_objCollection.m_objContent.m_strInPatientID, p_objCollection.m_objContent.m_strInPatientDate, p_objCollection.m_objContent.m_strOpenDate, p_objCollection.m_objContent.m_strLastModifyUserID, DateTime.Parse(strNow));
                if (p_objCollection.m_objDiagnosisArr != null && p_objCollection.m_objDiagnosisArr.Length > 0)
                {
                    for (int rows = 0; rows < p_objCollection.m_objDiagnosisArr.Length; rows++)
                    {
                        p_objCollection.m_objDiagnosisArr[rows].m_strLASTMODIFYDATE = strNow;
                    }
                    lngRes = m_lngAddNewDiagnosis(p_objCollection.m_objDiagnosisArr);
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

        #region 修改主表
        /// <summary>
        /// 修改主表
        /// </summary>
        /// <param name="p_objMainRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngModifyMainRecord(clsInHospitalMainRecord_Main p_objMainRecord)
        {
            if (p_objMainRecord == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(73, out objDPArr);
                objDPArr[0].Value = p_objMainRecord.m_strDiagnosisXML;
                objDPArr[1].Value = DBNull.Value;
                objDPArr[2].Value = p_objMainRecord.m_strMainDiagnosisXML;
                objDPArr[3].Value = p_objMainRecord.m_strICD_10OfMainXML;
                objDPArr[4].Value = DBNull.Value;
                objDPArr[5].Value = DBNull.Value;
                objDPArr[6].Value = p_objMainRecord.m_strPathologyDiagnosisXML;
                objDPArr[7].Value = p_objMainRecord.m_strScacheSourceXML;
                objDPArr[8].Value = p_objMainRecord.m_strSensitiveXML;
                objDPArr[9].Value = p_objMainRecord.m_strHbsAgXML;
                objDPArr[10].Value = p_objMainRecord.m_strHCV_AbXML;
                objDPArr[11].Value = p_objMainRecord.m_strHIV_AbXML;
                objDPArr[12].Value = p_objMainRecord.m_strAccordWithOutHospitalXML;
                objDPArr[13].Value = p_objMainRecord.m_strAccordInWithOutXML;
                objDPArr[14].Value = p_objMainRecord.m_strAccordBeforeOperationWithAfterXML;
                objDPArr[15].Value = p_objMainRecord.m_strAccordClinicWithPathologyXML;
                objDPArr[16].Value = p_objMainRecord.m_strAccordRadiateWithPathologyXML;
                objDPArr[17].Value = p_objMainRecord.m_strSalveTimesXML;
                objDPArr[18].Value = p_objMainRecord.m_strSalveSuccessXML;
                objDPArr[19].Value = p_objMainRecord.m_strOriginalDiseaseGyXML;
                objDPArr[20].Value = p_objMainRecord.m_strOriginalDiseaseTimesXML;
                objDPArr[21].Value = p_objMainRecord.m_strOriginalDiseaseDaysXML;
                objDPArr[22].Value = p_objMainRecord.m_strLymphGyXML;
                objDPArr[23].Value = p_objMainRecord.m_strLymphTimesXML;
                objDPArr[24].Value = p_objMainRecord.m_strLymphDaysXML;
                objDPArr[25].Value = p_objMainRecord.m_strMetastasisGyXML;
                objDPArr[26].Value = p_objMainRecord.m_strMetastasisTimesXML;
                objDPArr[27].Value = p_objMainRecord.m_strMetastasisDaysXML;
                objDPArr[28].Value = p_objMainRecord.m_strTotalAmtXML;
                objDPArr[29].Value = p_objMainRecord.m_strBedAmtXML;
                objDPArr[30].Value = p_objMainRecord.m_strNurseAmtXML;
                objDPArr[31].Value = p_objMainRecord.m_strWMAmtXML;
                objDPArr[32].Value = p_objMainRecord.m_strCMFinishedAmtXML;
                objDPArr[33].Value = p_objMainRecord.m_strCMSemiFinishedAmtXML;
                objDPArr[34].Value = p_objMainRecord.m_strRadiationAmtXML;
                objDPArr[35].Value = p_objMainRecord.m_strAssayAmtXML;
                objDPArr[36].Value = p_objMainRecord.m_strO2AmtXML;
                objDPArr[37].Value = p_objMainRecord.m_strBloodAmtXML;
                objDPArr[38].Value = p_objMainRecord.m_strTreatmentAmtXML;
                objDPArr[39].Value = p_objMainRecord.m_strOperationAmtXML;
                objDPArr[40].Value = p_objMainRecord.m_strDeliveryChildAmtXML;
                objDPArr[41].Value = p_objMainRecord.m_strCheckAmtXML;
                objDPArr[42].Value = p_objMainRecord.m_strAnaethesiaAmtXML;
                objDPArr[43].Value = p_objMainRecord.m_strBabyAmtXML;
                objDPArr[44].Value = p_objMainRecord.m_strAccompanyAmtXML;
                objDPArr[45].Value = p_objMainRecord.m_strOtherAmt1XML;
                objDPArr[46].Value = p_objMainRecord.m_strOtherAmt2XML;
                objDPArr[47].Value = p_objMainRecord.m_strOtherAmt3XML;
                objDPArr[48].Value = p_objMainRecord.m_strFollow_WeekXML;
                objDPArr[49].Value = p_objMainRecord.m_strFollow_MonthXML;
                objDPArr[50].Value = p_objMainRecord.m_strFollow_YearXML;
                objDPArr[51].Value = p_objMainRecord.m_strBloodTypeXML;
                objDPArr[52].Value = p_objMainRecord.m_strRBCXML;
                objDPArr[53].Value = p_objMainRecord.m_strPLTXML;
                objDPArr[54].Value = p_objMainRecord.m_strPlasmXML;
                objDPArr[55].Value = p_objMainRecord.m_strWholeBloodXML;
                objDPArr[56].Value = p_objMainRecord.m_strOtherBloodXML;
                objDPArr[57].Value = p_objMainRecord.m_strConsultationXML;
                objDPArr[58].Value = p_objMainRecord.m_strLongDistanctConsultationXML;
                objDPArr[59].Value = p_objMainRecord.m_strTOPLevelXML;
                objDPArr[60].Value = p_objMainRecord.m_strNurseLevelIXML;
                objDPArr[61].Value = p_objMainRecord.m_strNurseLevelIIXML;
                objDPArr[62].Value = p_objMainRecord.m_strNurseLevelIIIXML;
                objDPArr[63].Value = p_objMainRecord.m_strICUXML;
                objDPArr[64].Value = p_objMainRecord.m_strSpecialNurseXML;
                objDPArr[65].Value = p_objMainRecord.m_strInsuranceNumXML;
                objDPArr[66].Value = p_objMainRecord.m_strModeOfPaymentXML;
                objDPArr[67].Value = p_objMainRecord.m_strPatientHistoryNOXML;
                objDPArr[68].Value = p_objMainRecord.m_strMZICD10;
                objDPArr[69].Value = p_objMainRecord.m_strMAINICD10;
                objDPArr[70].Value = p_objMainRecord.m_strInPatientID;
                objDPArr[71].DbType = DbType.DateTime;
                objDPArr[71].Value = DateTime.Parse(p_objMainRecord.m_strInPatientDate);
                objDPArr[72].DbType = DbType.DateTime;
                objDPArr[72].Value = DateTime.Parse(p_objMainRecord.m_strOpenDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strModifyRecord, ref lngEff, objDPArr);
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
        #endregion

        #region 用于在修改记录时删除无用的记录
        #region 删除手术资料
        /// <summary>
        /// 删除手术资料
        /// </summary>
        /// <param name="p_strInpatientId">住院号</param>
        /// <param name="p_strInpateintDate">入院日期</param>
        /// <param name="p_strOpenDate">最后保存时间</param>
        /// <param name="p_strEmpId">删除者</param>
        /// <param name="p_dtmDelDate">删除时间</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeleteOperation(string p_strInpatientId, string p_strInpateintDate, string p_strOpenDate, string p_strEmpId, DateTime p_dtmDelDate)
        {
            if (string.IsNullOrEmpty(p_strInpatientId) || string.IsNullOrEmpty(p_strEmpId))
            {
                return -1;
            }

            DateTime dtmTemp = DateTime.Now;
            if (!DateTime.TryParse(p_strInpateintDate, out dtmTemp) || !DateTime.TryParse(p_strOpenDate, out dtmTemp))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDelDate;
                objDPArr[1].Value = p_strEmpId;
                objDPArr[2].Value = p_strInpatientId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = Convert.ToDateTime(p_strInpateintDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = Convert.ToDateTime(p_strOpenDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strDelOperation, ref lngEff, objDPArr);
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

        #region 删除子表
        /// <summary>
        /// 删除子表
        /// </summary>
        /// <param name="p_strInpatientId">住院号</param>
        /// <param name="p_strInpateintDate">入院日期</param>
        /// <param name="p_strOpenDate">最后保存时间</param>
        /// <param name="p_strEmpId">删除者</param>
        /// <param name="p_dtmDelDate">删除时间</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeleteContent(string p_strInpatientId, string p_strInpateintDate, string p_strOpenDate, string p_strEmpId, DateTime p_dtmDelDate)
        {
            if (string.IsNullOrEmpty(p_strInpatientId) || string.IsNullOrEmpty(p_strEmpId))
            {
                return -1;
            }

            DateTime dtmTemp = DateTime.Now;
            if (!DateTime.TryParse(p_strInpateintDate, out dtmTemp) || !DateTime.TryParse(p_strOpenDate, out dtmTemp))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDelDate;
                objDPArr[1].Value = p_strEmpId;
                objDPArr[2].Value = p_strInpatientId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = Convert.ToDateTime(p_strInpateintDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = Convert.ToDateTime(p_strOpenDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strDelContent, ref lngEff, objDPArr);
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

        #region 删除肿瘤专科病人治疗记录
        /// <summary>
        /// 删除肿瘤专科病人治疗记录
        /// </summary>
        /// <param name="p_strInpatientId">住院号</param>
        /// <param name="p_strInpateintDate">入院日期</param>
        /// <param name="p_strOpenDate">最后保存时间</param>
        /// <param name="p_strEmpId">删除者</param>
        /// <param name="p_dtmDelDate">删除时间</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeleteChemotherapy(string p_strInpatientId, string p_strInpateintDate, string p_strOpenDate, string p_strEmpId, DateTime p_dtmDelDate)
        {
            if (string.IsNullOrEmpty(p_strInpatientId) || string.IsNullOrEmpty(p_strEmpId))
            {
                return -1;
            }

            DateTime dtmTemp = DateTime.Now;
            if (!DateTime.TryParse(p_strInpateintDate, out dtmTemp) || !DateTime.TryParse(p_strOpenDate, out dtmTemp))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDelDate;
                objDPArr[1].Value = p_strEmpId;
                objDPArr[2].Value = p_strInpatientId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = Convert.ToDateTime(p_strInpateintDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = Convert.ToDateTime(p_strOpenDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strDelChemotherapy, ref lngEff, objDPArr);
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

        #region 删除产科分娩婴儿记录
        /// <summary>
        /// 删除产科分娩婴儿记录
        /// </summary>
        /// <param name="p_strInpatientId">住院号</param>
        /// <param name="p_strInpateintDate">入院日期</param>
        /// <param name="p_strOpenDate">最后保存时间</param>
        /// <param name="p_strEmpId">删除者</param>
        /// <param name="p_dtmDelDate">删除时间</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeleteBaby(string p_strInpatientId, string p_strInpateintDate, string p_strOpenDate, string p_strEmpId, DateTime p_dtmDelDate)
        {
            if (string.IsNullOrEmpty(p_strInpatientId) || string.IsNullOrEmpty(p_strEmpId))
            {
                return -1;
            }

            DateTime dtmTemp = DateTime.Now;
            if (!DateTime.TryParse(p_strInpateintDate, out dtmTemp) || !DateTime.TryParse(p_strOpenDate, out dtmTemp))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDelDate;
                objDPArr[1].Value = p_strEmpId;
                objDPArr[2].Value = p_strInpatientId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = Convert.ToDateTime(p_strInpateintDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = Convert.ToDateTime(p_strOpenDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strDelBaby, ref lngEff, objDPArr);
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

        #region 删除诊断
        /// <summary>
        /// 删除诊断
        /// </summary>
        /// <param name="p_strInpatientId">住院号</param>
        /// <param name="p_strInpateintDate">入院日期</param>
        /// <param name="p_strOpenDate">最后保存时间</param>
        /// <param name="p_strEmpId">删除者</param>
        /// <param name="p_dtmDelDate">删除时间</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeleteDiagnosis(string p_strInpatientId, string p_strInpateintDate, string p_strOpenDate, string p_strEmpId, DateTime p_dtmDelDate)
        {
            if (string.IsNullOrEmpty(p_strInpatientId) || string.IsNullOrEmpty(p_strEmpId))
            {
                return -1;
            }

            DateTime dtmTemp = DateTime.Now;
            if (!DateTime.TryParse(p_strInpateintDate, out dtmTemp) || !DateTime.TryParse(p_strOpenDate, out dtmTemp))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDelDate;
                objDPArr[1].Value = p_strEmpId;
                objDPArr[2].Value = p_strInpatientId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = Convert.ToDateTime(p_strInpateintDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = Convert.ToDateTime(p_strOpenDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strDelDiagnosis, ref lngEff, objDPArr);
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

        [AutoComplete]
        private long m_lngDeleteSubRecord(string p_strTableName, string p_strInpatientId, string p_strInpateintDate, string p_strOpenDate, string p_strEmpId)
        {
            if (string.IsNullOrEmpty(p_strInpatientId) || string.IsNullOrEmpty(p_strInpateintDate) || string.IsNullOrEmpty(p_strOpenDate) || string.IsNullOrEmpty(p_strTableName))
                return -1;
            string strSql = @"update " + p_strTableName + " t set t.status = 0, t.deactivedoperatorid = ?, t.deactiveddate = "
                + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @" where t.inpatientid = ?
   and t.inpatientdate = ?
   and t.opendate = ?";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetOpenDateInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                DateTime dtmInpatientDate = new DateTime(1900, 1, 1);
                DateTime dtmOpenDate = new DateTime(1900, 1, 1);
                DateTime.TryParse(p_strInpateintDate, out dtmInpatientDate);
                DateTime.TryParse(p_strOpenDate, out dtmOpenDate);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strEmpId;
                objDPArr[1].Value = p_strInpatientId;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = dtmInpatientDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = dtmOpenDate;
                long lngRff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;

            }			//返回
            return lngRes;
        }
        #endregion 用于在修改记录时删除无用的记录

        #region 获得创建日期
        /// <summary>
        /// 获得创建日期
        /// 如果没有，则表示号没有生成过病案首页
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strXML"></param>
        /// <param name="p_intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOpenDateInfo(
            string p_strInPatientID, string p_strInPatientDate, out string p_strXML, out int p_intRows)
        {
            p_strXML = null;
            p_intRows = 0;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetOpenDateInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;

                string m_strCommand = @"select opendate
  from inhospitalmainrecord
 where inpatientid = ?
   and inpatientdate = ?
   and status = 1 ";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(m_strCommand, ref p_strXML, ref p_intRows, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }
        #endregion


        #region 获得最后修改时间
        /// <summary>
        /// 获得最后修改时间
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strXML"></param>
        /// <param name="p_intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLastModifyDateInfo(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strXML, out int p_intRows)
        {
            p_strXML = null;
            p_intRows = 0;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;

                string m_strCommand = "select max(lastmodifydate) as maxlastmodifydate from inhospitalmainrecord_content where inpatientid = ? and inpatientdate = ? and opendate = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(m_strCommand, ref p_strXML, ref p_intRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }
        #endregion

        #region 获得主表的记录
        /// <summary>
        /// 获得主表的记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objMain">主表记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMainInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Main p_objMain)
        {
            p_objMain = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetMainInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;

                string m_strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       createuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       diagnosisxml,
       inhospitaldiagnosisxml,
       maindiagnosisxml,
       icd_10ofmainxml,
       infectiondiagnosisxml,
       icd_10ofinfectionxml,
       pathologydiagnosisxml,
       scachesourcexml,
       sensitivexml,
       hbsagxml,
       hcv_abxml,
       hiv_abxml,
       accordwithouthospitalxml,
       accordinwithoutxml,
       accordbfoprwithafxml,
       accordclinicwithpathologyxml,
       accordradiatewithpathologyxml,
       salvetimesxml,
       salvesuccessxml,
       originaldiseasegyxml,
       originaldiseasetimesxml,
       originaldiseasedaysxml,
       lymphgyxml,
       lymphtimesxml,
       lymphdaysxml,
       metastasisgyxml,
       metastasistimesxml,
       metastasisdaysxml,
       totalamtxml,
       bedamtxml,
       nurseamtxml,
       wmamtxml,
       cmfinishedamtxml,
       cmsemifinishedamtxml,
       radiationamtxml,
       assayamtxml,
       o2amtxml,
       bloodamtxml,
       treatmentamtxml,
       operationamtxml,
       deliverychildamtxml,
       checkamtxml,
       anaethesiaamtxml,
       babyamtxml,
       accompanyamtxml,
       otheramt1xml,
       otheramt2xml,
       otheramt3xml,
       follow_weekxml,
       follow_monthxml,
       follow_yearxml,
       bloodtypexml,
       rbcxml,
       pltxml,
       plasmxml,
       wholebloodxml,
       otherbloodxml,
       consultationxml,
       longdistanctconsultationxml,
       toplevelxml,
       nurselevelixml,
       nurseleveliixml,
       nurseleveliiixml,
       icuxml,
       specialnursexml,
       insurancenumxml,
       modeofpaymentxml,
       patienthistorynoxml,
       firstprintdate,
       mzicd10,
       mainicd10,
       ishandin
  from inhospitalmainrecord
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 1 ";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    DataRow drCurrent = dtbResult.Rows[0];
                    p_objMain = new clsInHospitalMainRecord_Main();
                    p_objMain.m_strInPatientID = drCurrent["INPATIENTID"].ToString();
                    p_objMain.m_strInPatientDate = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objMain.m_strOpenDate = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objMain.m_strCreateUserID = drCurrent["CREATEUSERID"].ToString();
                    p_objMain.m_strDiagnosisXML = drCurrent["DIAGNOSISXML"].ToString();
                    p_objMain.m_strInHospitalDiagnosisXML = drCurrent["INHOSPITALDIAGNOSISXML"].ToString();
                    p_objMain.m_strMainDiagnosisXML = drCurrent["MAINDIAGNOSISXML"].ToString();
                    p_objMain.m_strICD_10OfMainXML = drCurrent["ICD_10OFMAINXML"].ToString();
                    p_objMain.m_strInfectionDiagnosisXML = drCurrent["INFECTIONDIAGNOSISXML"].ToString();
                    p_objMain.m_strICD_10OfInfectionXML = drCurrent["ICD_10OFINFECTIONXML"].ToString();
                    p_objMain.m_strPathologyDiagnosisXML = drCurrent["PATHOLOGYDIAGNOSISXML"].ToString();
                    p_objMain.m_strScacheSourceXML = drCurrent["SCACHESOURCEXML"].ToString();
                    p_objMain.m_strSensitiveXML = drCurrent["SENSITIVEXML"].ToString();
                    p_objMain.m_strHbsAgXML = drCurrent["HBSAGXML"].ToString();
                    p_objMain.m_strHCV_AbXML = drCurrent["HCV_ABXML"].ToString();
                    p_objMain.m_strHIV_AbXML = drCurrent["HIV_ABXML"].ToString();
                    p_objMain.m_strAccordWithOutHospitalXML = drCurrent["ACCORDWITHOUTHOSPITALXML"].ToString();
                    p_objMain.m_strAccordInWithOutXML = drCurrent["ACCORDINWITHOUTXML"].ToString();
                    p_objMain.m_strAccordBeforeOperationWithAfterXML = drCurrent["ACCORDBFOPRWITHAFXML"].ToString();
                    p_objMain.m_strAccordClinicWithPathologyXML = drCurrent["ACCORDCLINICWITHPATHOLOGYXML"].ToString();
                    p_objMain.m_strAccordRadiateWithPathologyXML = drCurrent["ACCORDRADIATEWITHPATHOLOGYXML"].ToString();
                    p_objMain.m_strSalveTimesXML = drCurrent["SALVETIMESXML"].ToString();
                    p_objMain.m_strSalveSuccessXML = drCurrent["SALVESUCCESSXML"].ToString();
                    p_objMain.m_strOriginalDiseaseGyXML = drCurrent["ORIGINALDISEASEGYXML"].ToString();
                    p_objMain.m_strOriginalDiseaseTimesXML = drCurrent["ORIGINALDISEASETIMESXML"].ToString();
                    p_objMain.m_strOriginalDiseaseDaysXML = drCurrent["ORIGINALDISEASEDAYSXML"].ToString();
                    p_objMain.m_strLymphGyXML = drCurrent["LYMPHGYXML"].ToString();
                    p_objMain.m_strLymphTimesXML = drCurrent["LYMPHTIMESXML"].ToString();
                    p_objMain.m_strLymphDaysXML = drCurrent["LYMPHDAYSXML"].ToString();
                    p_objMain.m_strMetastasisGyXML = drCurrent["METASTASISGYXML"].ToString();
                    p_objMain.m_strMetastasisTimesXML = drCurrent["METASTASISTIMESXML"].ToString();
                    p_objMain.m_strMetastasisDaysXML = drCurrent["METASTASISDAYSXML"].ToString();
                    p_objMain.m_strTotalAmtXML = drCurrent["TOTALAMTXML"].ToString();
                    p_objMain.m_strBedAmtXML = drCurrent["BEDAMTXML"].ToString();
                    p_objMain.m_strNurseAmtXML = drCurrent["NURSEAMTXML"].ToString();
                    p_objMain.m_strWMAmtXML = drCurrent["WMAMTXML"].ToString();
                    p_objMain.m_strCMFinishedAmtXML = drCurrent["CMFINISHEDAMTXML"].ToString();
                    p_objMain.m_strCMSemiFinishedAmtXML = drCurrent["CMSEMIFINISHEDAMTXML"].ToString();
                    p_objMain.m_strRadiationAmtXML = drCurrent["RADIATIONAMTXML"].ToString();
                    p_objMain.m_strAssayAmtXML = drCurrent["ASSAYAMTXML"].ToString();
                    p_objMain.m_strO2AmtXML = drCurrent["O2AMTXML"].ToString();
                    p_objMain.m_strBloodAmtXML = drCurrent["BLOODAMTXML"].ToString();
                    p_objMain.m_strTreatmentAmtXML = drCurrent["TREATMENTAMTXML"].ToString();
                    p_objMain.m_strOperationAmtXML = drCurrent["OPERATIONAMTXML"].ToString();
                    p_objMain.m_strDeliveryChildAmtXML = drCurrent["DELIVERYCHILDAMTXML"].ToString();
                    p_objMain.m_strCheckAmtXML = drCurrent["CHECKAMTXML"].ToString();
                    p_objMain.m_strAnaethesiaAmtXML = drCurrent["ANAETHESIAAMTXML"].ToString();
                    p_objMain.m_strBabyAmtXML = drCurrent["BABYAMTXML"].ToString();
                    p_objMain.m_strAccompanyAmtXML = drCurrent["ACCOMPANYAMTXML"].ToString();
                    p_objMain.m_strOtherAmt1XML = drCurrent["OTHERAMT1XML"].ToString();
                    p_objMain.m_strOtherAmt2XML = drCurrent["OTHERAMT2XML"].ToString();
                    p_objMain.m_strOtherAmt3XML = drCurrent["OTHERAMT3XML"].ToString();
                    p_objMain.m_strFollow_WeekXML = drCurrent["FOLLOW_WEEKXML"].ToString();
                    p_objMain.m_strFollow_MonthXML = drCurrent["FOLLOW_MONTHXML"].ToString();
                    p_objMain.m_strFollow_YearXML = drCurrent["FOLLOW_YEARXML"].ToString();
                    p_objMain.m_strBloodTypeXML = drCurrent["BLOODTYPEXML"].ToString();
                    p_objMain.m_strRBCXML = drCurrent["RBCXML"].ToString();
                    p_objMain.m_strPLTXML = drCurrent["PLTXML"].ToString();
                    p_objMain.m_strPlasmXML = drCurrent["PLASMXML"].ToString();
                    p_objMain.m_strWholeBloodXML = drCurrent["WHOLEBLOODXML"].ToString();
                    p_objMain.m_strOtherBloodXML = drCurrent["OTHERBLOODXML"].ToString();
                    p_objMain.m_strConsultationXML = drCurrent["CONSULTATIONXML"].ToString();
                    p_objMain.m_strLongDistanctConsultationXML = drCurrent["LONGDISTANCTCONSULTATIONXML"].ToString();
                    p_objMain.m_strTOPLevelXML = drCurrent["TOPLEVELXML"].ToString();
                    p_objMain.m_strNurseLevelIXML = drCurrent["NURSELEVELIXML"].ToString();
                    p_objMain.m_strNurseLevelIIXML = drCurrent["NURSELEVELIIXML"].ToString();
                    p_objMain.m_strNurseLevelIIIXML = drCurrent["NURSELEVELIIIXML"].ToString();
                    p_objMain.m_strICUXML = drCurrent["ICUXML"].ToString();
                    p_objMain.m_strSpecialNurseXML = drCurrent["SPECIALNURSEXML"].ToString();
                    p_objMain.m_strInsuranceNumXML = drCurrent["INSURANCENUMXML"].ToString();
                    p_objMain.m_strModeOfPaymentXML = drCurrent["MODEOFPAYMENTXML"].ToString();
                    p_objMain.m_strPatientHistoryNOXML = drCurrent["PATIENTHISTORYNOXML"].ToString();
                    p_objMain.m_strMZICD10 = drCurrent["MZICD10"].ToString();
                    p_objMain.m_strMAINICD10 = drCurrent["MAINICD10"].ToString();

                    if (drCurrent["ISHANDIN"] == DBNull.Value)
                    {
                        p_objMain.m_intISHANDIN = 0;
                    }
                    else
                    {
                        p_objMain.m_intISHANDIN = Convert.ToInt32(drCurrent["ISHANDIN"]);
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }

        /// <summary>
        /// 获得已删除的主表的记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objMain"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeletedMainInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Main p_objMain)
        {
            p_objMain = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetDeletedMainInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;

                string m_strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       createuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       diagnosisxml,
       inhospitaldiagnosisxml,
       maindiagnosisxml,
       icd_10ofmainxml,
       infectiondiagnosisxml,
       icd_10ofinfectionxml,
       pathologydiagnosisxml,
       scachesourcexml,
       sensitivexml,
       hbsagxml,
       hcv_abxml,
       hiv_abxml,
       accordwithouthospitalxml,
       accordinwithoutxml,
       accordbfoprwithafxml,
       accordclinicwithpathologyxml,
       accordradiatewithpathologyxml,
       salvetimesxml,
       salvesuccessxml,
       originaldiseasegyxml,
       originaldiseasetimesxml,
       originaldiseasedaysxml,
       lymphgyxml,
       lymphtimesxml,
       lymphdaysxml,
       metastasisgyxml,
       metastasistimesxml,
       metastasisdaysxml,
       totalamtxml,
       bedamtxml,
       nurseamtxml,
       wmamtxml,
       cmfinishedamtxml,
       cmsemifinishedamtxml,
       radiationamtxml,
       assayamtxml,
       o2amtxml,
       bloodamtxml,
       treatmentamtxml,
       operationamtxml,
       deliverychildamtxml,
       checkamtxml,
       anaethesiaamtxml,
       babyamtxml,
       accompanyamtxml,
       otheramt1xml,
       otheramt2xml,
       otheramt3xml,
       follow_weekxml,
       follow_monthxml,
       follow_yearxml,
       bloodtypexml,
       rbcxml,
       pltxml,
       plasmxml,
       wholebloodxml,
       otherbloodxml,
       consultationxml,
       longdistanctconsultationxml,
       toplevelxml,
       nurselevelixml,
       nurseleveliixml,
       nurseleveliiixml,
       icuxml,
       specialnursexml,
       insurancenumxml,
       modeofpaymentxml,
       patienthistorynoxml,
       firstprintdate,
       mzicd10,
       mainicd10,
       ishandin
  from inhospitalmainrecord
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 0";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    DataRow drCurrent = dtbResult.Rows[0];
                    p_objMain = new clsInHospitalMainRecord_Main();
                    p_objMain.m_strInPatientID = drCurrent["INPATIENTID"].ToString();
                    p_objMain.m_strInPatientDate = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objMain.m_strOpenDate = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objMain.m_strCreateUserID = drCurrent["CREATEUSERID"].ToString();
                    p_objMain.m_strDeActivedDate = Convert.ToDateTime(drCurrent["DEACTIVEDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objMain.m_strDeActivedOperatorID = drCurrent["DEACTIVEDOPERATORID"].ToString();
                    p_objMain.m_strDiagnosisXML = drCurrent["DIAGNOSISXML"].ToString();
                    p_objMain.m_strInHospitalDiagnosisXML = drCurrent["INHOSPITALDIAGNOSISXML"].ToString();
                    p_objMain.m_strMainDiagnosisXML = drCurrent["MAINDIAGNOSISXML"].ToString();
                    p_objMain.m_strICD_10OfMainXML = drCurrent["ICD_10OFMAINXML"].ToString();
                    p_objMain.m_strInfectionDiagnosisXML = drCurrent["INFECTIONDIAGNOSISXML"].ToString();
                    p_objMain.m_strICD_10OfInfectionXML = drCurrent["ICD_10OFINFECTIONXML"].ToString();
                    p_objMain.m_strPathologyDiagnosisXML = drCurrent["PATHOLOGYDIAGNOSISXML"].ToString();
                    p_objMain.m_strScacheSourceXML = drCurrent["SCACHESOURCEXML"].ToString();
                    p_objMain.m_strSensitiveXML = drCurrent["SENSITIVEXML"].ToString();
                    p_objMain.m_strHbsAgXML = drCurrent["HBSAGXML"].ToString();
                    p_objMain.m_strHCV_AbXML = drCurrent["HCV_ABXML"].ToString();
                    p_objMain.m_strHIV_AbXML = drCurrent["HIV_ABXML"].ToString();
                    p_objMain.m_strAccordWithOutHospitalXML = drCurrent["ACCORDWITHOUTHOSPITALXML"].ToString();
                    p_objMain.m_strAccordInWithOutXML = drCurrent["ACCORDINWITHOUTXML"].ToString();
                    p_objMain.m_strAccordBeforeOperationWithAfterXML = drCurrent["ACCORDBFOPRWITHAFXML"].ToString();
                    p_objMain.m_strAccordClinicWithPathologyXML = drCurrent["ACCORDCLINICWITHPATHOLOGYXML"].ToString();
                    p_objMain.m_strAccordRadiateWithPathologyXML = drCurrent["ACCORDRADIATEWITHPATHOLOGYXML"].ToString();
                    p_objMain.m_strSalveTimesXML = drCurrent["SALVETIMESXML"].ToString();
                    p_objMain.m_strSalveSuccessXML = drCurrent["SALVESUCCESSXML"].ToString();
                    p_objMain.m_strOriginalDiseaseGyXML = drCurrent["ORIGINALDISEASEGYXML"].ToString();
                    p_objMain.m_strOriginalDiseaseTimesXML = drCurrent["ORIGINALDISEASETIMESXML"].ToString();
                    p_objMain.m_strOriginalDiseaseDaysXML = drCurrent["ORIGINALDISEASEDAYSXML"].ToString();
                    p_objMain.m_strLymphGyXML = drCurrent["LYMPHGYXML"].ToString();
                    p_objMain.m_strLymphTimesXML = drCurrent["LYMPHTIMESXML"].ToString();
                    p_objMain.m_strLymphDaysXML = drCurrent["LYMPHDAYSXML"].ToString();
                    p_objMain.m_strMetastasisGyXML = drCurrent["METASTASISGYXML"].ToString();
                    p_objMain.m_strMetastasisTimesXML = drCurrent["METASTASISTIMESXML"].ToString();
                    p_objMain.m_strMetastasisDaysXML = drCurrent["METASTASISDAYSXML"].ToString();
                    p_objMain.m_strTotalAmtXML = drCurrent["TOTALAMTXML"].ToString();
                    p_objMain.m_strBedAmtXML = drCurrent["BEDAMTXML"].ToString();
                    p_objMain.m_strNurseAmtXML = drCurrent["NURSEAMTXML"].ToString();
                    p_objMain.m_strWMAmtXML = drCurrent["WMAMTXML"].ToString();
                    p_objMain.m_strCMFinishedAmtXML = drCurrent["CMFINISHEDAMTXML"].ToString();
                    p_objMain.m_strCMSemiFinishedAmtXML = drCurrent["CMSEMIFINISHEDAMTXML"].ToString();
                    p_objMain.m_strRadiationAmtXML = drCurrent["RADIATIONAMTXML"].ToString();
                    p_objMain.m_strAssayAmtXML = drCurrent["ASSAYAMTXML"].ToString();
                    p_objMain.m_strO2AmtXML = drCurrent["O2AMTXML"].ToString();
                    p_objMain.m_strBloodAmtXML = drCurrent["BLOODAMTXML"].ToString();
                    p_objMain.m_strTreatmentAmtXML = drCurrent["TREATMENTAMTXML"].ToString();
                    p_objMain.m_strOperationAmtXML = drCurrent["OPERATIONAMTXML"].ToString();
                    p_objMain.m_strDeliveryChildAmtXML = drCurrent["DELIVERYCHILDAMTXML"].ToString();
                    p_objMain.m_strCheckAmtXML = drCurrent["CHECKAMTXML"].ToString();
                    p_objMain.m_strAnaethesiaAmtXML = drCurrent["ANAETHESIAAMTXML"].ToString();
                    p_objMain.m_strBabyAmtXML = drCurrent["BABYAMTXML"].ToString();
                    p_objMain.m_strAccompanyAmtXML = drCurrent["ACCOMPANYAMTXML"].ToString();
                    p_objMain.m_strOtherAmt1XML = drCurrent["OTHERAMT1XML"].ToString();
                    p_objMain.m_strOtherAmt2XML = drCurrent["OTHERAMT2XML"].ToString();
                    p_objMain.m_strOtherAmt3XML = drCurrent["OTHERAMT3XML"].ToString();
                    p_objMain.m_strFollow_WeekXML = drCurrent["FOLLOW_WEEKXML"].ToString();
                    p_objMain.m_strFollow_MonthXML = drCurrent["FOLLOW_MONTHXML"].ToString();
                    p_objMain.m_strFollow_YearXML = drCurrent["FOLLOW_YEARXML"].ToString();
                    p_objMain.m_strBloodTypeXML = drCurrent["BLOODTYPEXML"].ToString();
                    p_objMain.m_strRBCXML = drCurrent["RBCXML"].ToString();
                    p_objMain.m_strPLTXML = drCurrent["PLTXML"].ToString();
                    p_objMain.m_strPlasmXML = drCurrent["PLASMXML"].ToString();
                    p_objMain.m_strWholeBloodXML = drCurrent["WHOLEBLOODXML"].ToString();
                    p_objMain.m_strOtherBloodXML = drCurrent["OTHERBLOODXML"].ToString();
                    p_objMain.m_strConsultationXML = drCurrent["CONSULTATIONXML"].ToString();
                    p_objMain.m_strLongDistanctConsultationXML = drCurrent["LONGDISTANCTCONSULTATIONXML"].ToString();
                    p_objMain.m_strTOPLevelXML = drCurrent["TOPLEVELXML"].ToString();
                    p_objMain.m_strNurseLevelIXML = drCurrent["NURSELEVELIXML"].ToString();
                    p_objMain.m_strNurseLevelIIXML = drCurrent["NURSELEVELIIXML"].ToString();
                    p_objMain.m_strNurseLevelIIIXML = drCurrent["NURSELEVELIIIXML"].ToString();
                    p_objMain.m_strICUXML = drCurrent["ICUXML"].ToString();
                    p_objMain.m_strSpecialNurseXML = drCurrent["SPECIALNURSEXML"].ToString();
                    p_objMain.m_strInsuranceNumXML = drCurrent["INSURANCENUMXML"].ToString();
                    p_objMain.m_strModeOfPaymentXML = drCurrent["MODEOFPAYMENTXML"].ToString();
                    p_objMain.m_strPatientHistoryNOXML = drCurrent["PATIENTHISTORYNOXML"].ToString();
                    p_objMain.m_strMZICD10 = drCurrent["MZICD10"].ToString();
                    p_objMain.m_strMAINICD10 = drCurrent["MAINICD10"].ToString();
                    if (drCurrent["ISHANDIN"] == DBNull.Value)
                    {
                        p_objMain.m_intISHANDIN = 0;
                    }
                    else
                    {
                        p_objMain.m_intISHANDIN = Convert.ToInt32(drCurrent["ISHANDIN"]);
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }

        #endregion

        #region 获得主子表内容
        /// <summary>
        /// 获得主子表内容
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetContentInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Content p_objContent)
        {
            p_objContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetContentInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                string m_strCommand = null;
                #region SQL语句
                //                m_strCommand = @"SELECT a.*,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEANDTECHBYID") + @"(a.Doctor)  DoctorName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEANDTECHBYID") + @"(a.DirectorDt) DirectorDtName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEANDTECHBYID") + @"(a.SubDirectorDt) SubDirectorDtName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEANDTECHBYID") + @"(a.Dt) DtName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEANDTECHBYID") + @"(a.InHospitalDt) InHospitalDtName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEANDTECHBYID") + @"(a.AttendInForAdvancesStudyDt) AttendInForAdvancesStudyDtName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEANDTECHBYID") + @"(a.GraduateStudentIntern) GraduateStudentInternName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEANDTECHBYID") + @"(a.Coder) CoderName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEANDTECHBYID") + @"(a.QCDt) QCDtName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETEMPNAMEANDTECHBYID") + @"(a.QCNurse) QCNurseName
                //										FROM InHospitalMainRecord_Content a " +
                //                                        "Where InPatientID = ? AND " +
                //                                        "InPatientDate = ? AND " +
                //                                        "OpenDate= ? AND Status =1 ";

                System.Text.StringBuilder stbSQL = new System.Text.StringBuilder(200);
                stbSQL.Append(@"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.lastmodifydate,
       a.lastmodifyuserid,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.diagnosis,
       a.inhospitaldiagnosis,
       a.doctor,
       a.confirmdiagnosisdate,
       a.condictionwhenin,
       a.maindiagnosis,
       a.mainconditionseq,
       a.icd_10ofmain,
       a.infectiondiagnosis,
       a.infectioncondictionseq,
       a.icd_10ofinfection,
       a.pathologydiagnosis,
       a.scachesource,
       a.sensitive,
       a.hbsag,
       a.hcv_ab,
       a.hiv_ab,
       a.accordwithouthospital,
       a.accordinwithout,
       a.accordbeforeoperationwithafter,
       a.accordclinicwithpathology,
       a.accordradiatewithpathology,
       a.salvetimes,
       a.salvesuccess,
       a.directordt,
       a.subdirectordt,
       a.dt,
       a.inhospitaldt,
       a.attendinforadvancesstudydt,
       a.graduatestudentintern,
       a.intern,
       a.coder,
       a.quality,
       a.qcdt,
       a.qcnurse,
       a.qctime,
       a.rtmodeseq,
       a.rtruleseq,
       a.rtco,
       a.rtaccelerator,
       a.rtx_ray,
       a.rtlacuna,
       a.originaldiseaseseq,
       a.originaldiseasegy,
       a.originaldiseasetimes,
       a.originaldiseasedays,
       a.originaldiseasebegindate,
       a.originaldiseaseenddate,
       a.lymphseq,
       a.lymphgy,
       a.lymphtimes,
       a.lymphdays,
       a.lymphbegindate,
       a.lymphenddate,
       a.metastasisgy,
       a.metastasistimes,
       a.metastasisdays,
       a.metastasisbegindate,
       a.metastasisenddate,
       a.chemotherapymodeseq,
       a.chemotherapywholebody,
       a.chemotherapylocal,
       a.chemotherapyintubate,
       a.chemotherapythorax,
       a.chemotherapyabdomen,
       a.chemotherapyspinal,
       a.chemotherapyothertry,
       a.chemotherapyother,
       a.totalamt,
       a.bedamt,
       a.nurseamt,
       a.wmamt,
       a.cmfinishedamt,
       a.cmsemifinishedamt,
       a.radiationamt,
       a.assayamt,
       a.o2amt,
       a.bloodamt,
       a.treatmentamt,
       a.operationamt,
       a.deliverychildamt,
       a.checkamt,
       a.anaethesiaamt,
       a.babyamt,
       a.accompanyamt,
       a.otheramt1,
       a.otheramt2,
       a.otheramt3,
       a.corpsecheck,
       a.firstcase,
       a.follow,
       a.follow_week,
       a.follow_month,
       a.follow_year,
       a.modelcase,
       a.bloodtype,
       a.bloodrh,
       a.bloodtransactoin,
       a.rbc,
       a.plt,
       a.plasm,
       a.wholeblood,
       a.otherblood,
       a.consultation,
       a.longdistanctconsultation,
       a.toplevel,
       a.nurseleveli,
       a.nurselevelii,
       a.nurseleveliii,
       a.icu,
       a.specialnurse,
       a.insurancenum,
       a.modeofpayment,
       a.patienthistoryno,
       a.outpatientdate,
       a.birthplace,
       a.operation,
       a.baby,
       a.chemotherapy,path,
newbabyweight,newbabyinhostpitalweight,sszyj_jbbm,blzd_blh,blzd_jbbm,discharged_int,discharged_varh,readmitted31_int,readmitted31_varh,inrnssday,
inrnsshour,inrnssmin,outrnssday,outrnsshour,outrnssmin,inhospitalway,
medicalamt_new,treatmentamt_new,compositeeotheramt_new,pdamt_new,ldamt_new,idamt_new,cdamt_new,noopamt_new,opbytreatmentamt_new,physicalamt_new,
rehabilitationamt_new,cmtamt_new,aaamt_new,albuminamt_new,globulinamt_new,cfamt_new,cytokinesamt_new,onetimebysupmt_new,onetimebytmamt_new,onttimebyopamt_new,
tumor,t,n,m,installments,metastasiscount,");

                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.doctor)  doctorname,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.directordt) directordtname,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.subdirectordt) subdirectordtname,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.dt) dtname,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.inhospitaldt) inhospitaldtname,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.attendinforadvancesstudydt) attendinforadvancesstudydtname,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.graduatestudentintern) graduatestudentinternname,
										");

                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.intern) internname,
										");

                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.coder) codername,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.qcdt) qcdtname,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.qcnurse) qcnursename
										from inhospitalmainrecord_content a 
                                         where inpatientid = ? and 
                                         inpatientdate = ? and 
                                         opendate= ? and status =1 ");

                m_strCommand = stbSQL.ToString();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                #endregion

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    DataRow drCurrent = dtbResult.Rows[0];
                    p_objContent = new clsInHospitalMainRecord_Content();
                    p_objContent.m_strInPatientID = drCurrent["INPATIENTID"].ToString();
                    p_objContent.m_strInPatientDate = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strOpenDate = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strLastModifyDate = Convert.ToDateTime(drCurrent["LASTMODIFYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strLastModifyUserID = drCurrent["LASTMODIFYUSERID"].ToString();
                    p_objContent.m_strDiagnosis = drCurrent["DIAGNOSIS"].ToString();
                    p_objContent.m_strInHospitalDiagnosis = drCurrent["INHOSPITALDIAGNOSIS"].ToString();
                    p_objContent.m_strDoctor = drCurrent["DOCTOR"].ToString();
                    p_objContent.m_strConfirmDiagnosisDate = Convert.ToDateTime(drCurrent["CONFIRMDIAGNOSISDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strCondictionWhenIn = drCurrent["CONDICTIONWHENIN"].ToString();
                    p_objContent.m_strMainDiagnosis = drCurrent["MAINDIAGNOSIS"].ToString();
                    p_objContent.m_strMainConditionSeq = drCurrent["MAINCONDITIONSEQ"].ToString();
                    p_objContent.m_strICD_10OfMain = drCurrent["ICD_10OFMAIN"].ToString();
                    p_objContent.m_strInfectionDiagnosis = drCurrent["INFECTIONDIAGNOSIS"].ToString();
                    p_objContent.m_strICD_10OfInfection = drCurrent["ICD_10OFINFECTION"].ToString();
                    p_objContent.m_strPathologyDiagnosis = drCurrent["PATHOLOGYDIAGNOSIS"].ToString();
                    p_objContent.m_strScacheSource = drCurrent["SCACHESOURCE"].ToString();
                    p_objContent.m_strSensitive = drCurrent["SENSITIVE"].ToString();
                    p_objContent.m_strHbsAg = drCurrent["HBSAG"].ToString();
                    p_objContent.m_strHCV_Ab = drCurrent["HCV_AB"].ToString();
                    p_objContent.m_strHIV_Ab = drCurrent["HIV_AB"].ToString();
                    p_objContent.m_strAccordWithOutHospital = drCurrent["ACCORDWITHOUTHOSPITAL"].ToString();
                    p_objContent.m_strAccordInWithOut = drCurrent["ACCORDINWITHOUT"].ToString();
                    p_objContent.m_strAccordBeforeOperationWithAfter = drCurrent["ACCORDBEFOREOPERATIONWITHAFTER"].ToString();
                    p_objContent.m_strAccordClinicWithPathology = drCurrent["ACCORDCLINICWITHPATHOLOGY"].ToString();
                    p_objContent.m_strAccordRadiateWithPathology = drCurrent["ACCORDRADIATEWITHPATHOLOGY"].ToString();
                    p_objContent.m_strSalveTimes = drCurrent["SALVETIMES"].ToString();
                    p_objContent.m_strSalveSuccess = drCurrent["SALVESUCCESS"].ToString();
                    p_objContent.m_strDirectorDt = drCurrent["DIRECTORDT"].ToString();
                    p_objContent.m_strSubDirectorDt = drCurrent["SUBDIRECTORDT"].ToString();
                    p_objContent.m_strDt = drCurrent["DT"].ToString();
                    p_objContent.m_strInHospitalDt = drCurrent["INHOSPITALDT"].ToString();
                    p_objContent.m_strAttendInForAdvancesStudyDt = drCurrent["ATTENDINFORADVANCESSTUDYDT"].ToString();
                    p_objContent.m_strGraduateStudentIntern = drCurrent["GRADUATESTUDENTINTERN"].ToString();
                    p_objContent.m_strIntern = drCurrent["INTERN"].ToString();
                    p_objContent.m_strCoder = drCurrent["CODER"].ToString();
                    p_objContent.m_strQCDt = drCurrent["QCDT"].ToString();
                    p_objContent.m_strQCNurse = drCurrent["QCNURSE"].ToString();
                    p_objContent.m_strQCTime = Convert.ToDateTime(drCurrent["QCTIME"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strRTModeSeq = drCurrent["RTMODESEQ"].ToString();
                    p_objContent.m_strRTRuleSeq = drCurrent["RTRULESEQ"].ToString();
                    p_objContent.m_strRTCo = drCurrent["RTCO"].ToString();
                    p_objContent.m_strRTAccelerator = drCurrent["RTACCELERATOR"].ToString();
                    p_objContent.m_strRTX_Ray = drCurrent["RTX_RAY"].ToString();
                    p_objContent.m_strRTLacuna = drCurrent["RTLACUNA"].ToString();
                    p_objContent.m_strOriginalDiseaseSeq = drCurrent["ORIGINALDISEASESEQ"].ToString();
                    p_objContent.m_strOriginalDiseaseGy = drCurrent["ORIGINALDISEASEGY"].ToString();
                    p_objContent.m_strOriginalDiseaseTimes = drCurrent["ORIGINALDISEASETIMES"].ToString();
                    p_objContent.m_strOriginalDiseaseDays = drCurrent["ORIGINALDISEASEDAYS"].ToString();
                    p_objContent.m_strOriginalDiseaseBeginDate = Convert.ToDateTime(drCurrent["ORIGINALDISEASEBEGINDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strOriginalDiseaseEndDate = Convert.ToDateTime(drCurrent["ORIGINALDISEASEENDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strLymphSeq = drCurrent["LYMPHSEQ"].ToString();
                    p_objContent.m_strLymphGy = drCurrent["LYMPHGY"].ToString();
                    p_objContent.m_strLymphTimes = drCurrent["LYMPHTIMES"].ToString();
                    p_objContent.m_strLymphDays = drCurrent["LYMPHDAYS"].ToString();
                    p_objContent.m_strLymphBeginDate = Convert.ToDateTime(drCurrent["LYMPHBEGINDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strLymphEndDate = Convert.ToDateTime(drCurrent["LYMPHENDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strMetastasisGy = drCurrent["METASTASISGY"].ToString();
                    p_objContent.m_strMetastasisTimes = drCurrent["METASTASISTIMES"].ToString();
                    p_objContent.m_strMetastasisDays = drCurrent["METASTASISDAYS"].ToString();
                    p_objContent.m_strMetastasisBeginDate = Convert.ToDateTime(drCurrent["METASTASISBEGINDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strMetastasisEndDate = Convert.ToDateTime(drCurrent["METASTASISENDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strChemotherapyModeSeq = drCurrent["CHEMOTHERAPYMODESEQ"].ToString();
                    p_objContent.m_strChemotherapyWholeBody = drCurrent["CHEMOTHERAPYWHOLEBODY"].ToString();
                    p_objContent.m_strChemotherapyLocal = drCurrent["CHEMOTHERAPYLOCAL"].ToString();
                    p_objContent.m_strChemotherapyIntubate = drCurrent["CHEMOTHERAPYINTUBATE"].ToString();
                    p_objContent.m_strChemotherapyThorax = drCurrent["CHEMOTHERAPYTHORAX"].ToString();
                    p_objContent.m_strChemotherapyAbdomen = drCurrent["CHEMOTHERAPYABDOMEN"].ToString();
                    p_objContent.m_strChemotherapySpinal = drCurrent["CHEMOTHERAPYSPINAL"].ToString();
                    p_objContent.m_strChemotherapyOtherTry = drCurrent["CHEMOTHERAPYOTHERTRY"].ToString();
                    p_objContent.m_strChemotherapyOther = drCurrent["CHEMOTHERAPYOTHER"].ToString();
                    p_objContent.m_strTotalAmt = drCurrent["TOTALAMT"].ToString();
                    p_objContent.m_strBedAmt = drCurrent["BEDAMT"].ToString();
                    p_objContent.m_strNurseAmt = drCurrent["NURSEAMT"].ToString();
                    p_objContent.m_strWMAmt = drCurrent["WMAMT"].ToString();
                    p_objContent.m_strCMFinishedAmt = drCurrent["CMFINISHEDAMT"].ToString();
                    p_objContent.m_strCMSemiFinishedAmt = drCurrent["CMSEMIFINISHEDAMT"].ToString();
                    p_objContent.m_strRadiationAmt = drCurrent["RADIATIONAMT"].ToString();
                    p_objContent.m_strAssayAmt = drCurrent["ASSAYAMT"].ToString();
                    p_objContent.m_strO2Amt = drCurrent["O2AMT"].ToString();
                    p_objContent.m_strBloodAmt = drCurrent["BLOODAMT"].ToString();
                    p_objContent.m_strTreatmentAmt = drCurrent["TREATMENTAMT"].ToString();
                    p_objContent.m_strOperationAmt = drCurrent["OPERATIONAMT"].ToString();
                    p_objContent.m_strDeliveryChildAmt = drCurrent["DELIVERYCHILDAMT"].ToString();
                    p_objContent.m_strCheckAmt = drCurrent["CHECKAMT"].ToString();
                    p_objContent.m_strAnaethesiaAmt = drCurrent["ANAETHESIAAMT"].ToString();
                    p_objContent.m_strBabyAmt = drCurrent["BABYAMT"].ToString();
                    p_objContent.m_strAccompanyAmt = drCurrent["ACCOMPANYAMT"].ToString();
                    p_objContent.m_strOtherAmt1 = drCurrent["OTHERAMT1"].ToString();
                    p_objContent.m_strOtherAmt2 = drCurrent["OTHERAMT2"].ToString();
                    p_objContent.m_strOtherAmt3 = drCurrent["OTHERAMT3"].ToString();
                    p_objContent.m_strCorpseCheck = drCurrent["CORPSECHECK"].ToString();
                    p_objContent.m_strFirstCase = drCurrent["FIRSTCASE"].ToString();
                    p_objContent.m_strFollow = drCurrent["FOLLOW"].ToString();
                    p_objContent.m_strFollow_Week = drCurrent["FOLLOW_WEEK"].ToString();
                    p_objContent.m_strFollow_Month = drCurrent["FOLLOW_MONTH"].ToString();
                    p_objContent.m_strFollow_Year = drCurrent["FOLLOW_YEAR"].ToString();
                    p_objContent.m_strModelCase = drCurrent["MODELCASE"].ToString();
                    p_objContent.m_strBloodType = drCurrent["BLOODTYPE"].ToString();
                    p_objContent.m_strBloodRh = drCurrent["BLOODRH"].ToString();
                    p_objContent.m_strBloodTransActoin = drCurrent["BLOODTRANSACTOIN"].ToString();
                    p_objContent.m_strRBC = drCurrent["RBC"].ToString();
                    p_objContent.m_strPLT = drCurrent["PLT"].ToString();
                    p_objContent.m_strPlasm = drCurrent["PLASM"].ToString();
                    p_objContent.m_strWholeBlood = drCurrent["WHOLEBLOOD"].ToString();
                    p_objContent.m_strOtherBlood = drCurrent["OTHERBLOOD"].ToString();
                    p_objContent.m_strConsultation = drCurrent["CONSULTATION"].ToString();
                    p_objContent.m_strLongDistanctConsultation = drCurrent["LONGDISTANCTCONSULTATION"].ToString();
                    p_objContent.m_strTOPLevel = drCurrent["TOPLEVEL"].ToString();
                    p_objContent.m_strNurseLevelI = drCurrent["NURSELEVELI"].ToString();
                    p_objContent.m_strNurseLevelII = drCurrent["NURSELEVELII"].ToString();
                    p_objContent.m_strNurseLevelIII = drCurrent["NURSELEVELIII"].ToString();
                    p_objContent.m_strICU = drCurrent["ICU"].ToString();
                    p_objContent.m_strSpecialNurse = drCurrent["SPECIALNURSE"].ToString();
                    if (drCurrent["INSURANCENUM"].ToString() == "自费")
                        p_objContent.m_strInsuranceNum = "全自费";
                    else
                        p_objContent.m_strInsuranceNum = drCurrent["INSURANCENUM"].ToString();
                    p_objContent.m_strModeOfPayment = drCurrent["MODEOFPAYMENT"].ToString();
                    p_objContent.m_strPatientHistoryNO = drCurrent["PATIENTHISTORYNO"].ToString();
                    p_objContent.m_strQuality = drCurrent["QUALITY"].ToString();
                    DateTime dtmTemp = DateTime.MinValue;
                    if (DateTime.TryParse(drCurrent["OUTPATIENTDATE"].ToString(), out dtmTemp))
                    {
                        p_objContent.m_strOutPatientDate = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        p_objContent.m_strOutPatientDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    p_objContent.m_strBirthPlace = drCurrent["BIRTHPLACE"].ToString();
                    p_objContent.m_strDoctorName = drCurrent["DOCTORNAME"].ToString();
                    p_objContent.m_strDirectorDtName = drCurrent["DIRECTORDTNAME"].ToString();
                    p_objContent.m_strSubDirectorDtName = drCurrent["SUBDIRECTORDTNAME"].ToString();
                    p_objContent.m_strDtName = drCurrent["DTNAME"].ToString();
                    p_objContent.m_strInHospitalDtName = drCurrent["INHOSPITALDTNAME"].ToString();
                    p_objContent.m_strAttendInForAdvancesStudyDtName = drCurrent["ATTENDINFORADVANCESSTUDYDTNAME"].ToString();
                    p_objContent.m_strGraduateStudentInternName = drCurrent["GRADUATESTUDENTINTERNNAME"].ToString();
                    p_objContent.m_strInternName = drCurrent["INTERNNAME"].ToString();
                    p_objContent.m_strCoderName = drCurrent["CODERNAME"].ToString();
                    p_objContent.m_strQCDtName = drCurrent["QCDtNAME"].ToString();
                    p_objContent.m_strQCNurseName = drCurrent["QCNURSENAME"].ToString();
                    p_objContent.m_strOperation = drCurrent["OPERATION"].ToString();
                    p_objContent.m_strBaby = drCurrent["BABY"].ToString();
                    p_objContent.m_strChemotherapy = drCurrent["CHEMOTHERAPY"].ToString();
                    p_objContent.m_strpath = drCurrent["PATH"].ToString();
                    /***********************************************/
                    p_objContent.m_strNewBabyWeight = drCurrent["NEWBABYWEIGHT"].ToString();
                    p_objContent.m_strNewBabyInhostpitalWeight = drCurrent["NEWBABYINHOSTPITALWEIGHT"].ToString();
                    p_objContent.m_strSSZYJ_jbbm = drCurrent["SSZYJ_JBBM"].ToString();
                    p_objContent.m_strBLZD_blh = drCurrent["BLZD_BLH"].ToString();
                    p_objContent.m_strBLZD_jbbm = drCurrent["BLZD_JBBM"].ToString();
                    p_objContent.m_intDischarged = drCurrent["DISCHARGED_INT"].ToString();
                    p_objContent.m_strDischargedHospitalName = drCurrent["DISCHARGED_VARH"].ToString();
                    p_objContent.m_intReadmitted31 = drCurrent["READMITTED31_INT"].ToString();
                    p_objContent.m_strReadmitted31 = drCurrent["READMITTED31_VARH"].ToString();
                    p_objContent.m_strInRnssDay = drCurrent["INRNSSDAY"].ToString();
                    p_objContent.m_strInRnssHour = drCurrent["INRNSSHOUR"].ToString();
                    p_objContent.m_strInRnssMin = drCurrent["INRNSSMIN"].ToString();
                    p_objContent.m_strOutRnssDay = drCurrent["OUTRNSSDAY"].ToString();
                    p_objContent.m_strOutRnssHour = drCurrent["OUTRNSSHOUR"].ToString();
                    p_objContent.m_strOutRnssMin = drCurrent["OUTRNSSMIN"].ToString();
                    p_objContent.m_strInhospitalWay = drCurrent["INHOSPITALWAY"].ToString();
                    /***********************************************/
                    p_objContent.m_strMEDICALAMT_NEW = drCurrent["MEDICALAMT_NEW"].ToString();
                    p_objContent.m_strTREATMENTAMT_NEW = drCurrent["TREATMENTAMT_NEW"].ToString();
                    p_objContent.m_strCOMPOSITEEOTHERAMT_NEW = drCurrent["COMPOSITEEOTHERAMT_NEW"].ToString();
                    p_objContent.m_strPDAMT_NEW = drCurrent["PDAMT_NEW"].ToString();
                    p_objContent.m_strLDAMT_NEW = drCurrent["LDAMT_NEW"].ToString();
                    p_objContent.m_strIDAMT_NEW = drCurrent["IDAMT_NEW"].ToString();
                    p_objContent.m_strCDAMT_NEW = drCurrent["CDAMT_NEW"].ToString();
                    p_objContent.m_strNOOPAMT_NEW = drCurrent["NOOPAMT_NEW"].ToString();
                    p_objContent.m_strOPBYTREATMENTAMT_NEW = drCurrent["OPBYTREATMENTAMT_NEW"].ToString();
                    p_objContent.m_strPHYSICALAMT_NEW = drCurrent["PHYSICALAMT_NEW"].ToString();
                    p_objContent.m_strREHABILITATIONAMT_NEW = drCurrent["REHABILITATIONAMT_NEW"].ToString();
                    p_objContent.m_strCMTAMT_NEW = drCurrent["CMTAMT_NEW"].ToString();
                    p_objContent.m_strAAAMT_NEW = drCurrent["AAAMT_NEW"].ToString();
                    p_objContent.m_strALBUMINAMT_NEW = drCurrent["ALBUMINAMT_NEW"].ToString();
                    p_objContent.m_strGLOBULINAMT_NEW = drCurrent["GLOBULINAMT_NEW"].ToString();
                    p_objContent.m_strCFAMT_NEW = drCurrent["CFAMT_NEW"].ToString();
                    p_objContent.m_strCYTOKINESAMT_NEW = drCurrent["CYTOKINESAMT_NEW"].ToString();
                    p_objContent.m_strONETIMEBYSUPMT_NEW = drCurrent["ONETIMEBYSUPMT_NEW"].ToString();
                    p_objContent.m_strONETIMEBYTMAMT_NEW = drCurrent["ONETIMEBYTMAMT_NEW"].ToString();
                    p_objContent.m_strONTTIMEBYOPAMT_NEW = drCurrent["ONTTIMEBYOPAMT_NEW"].ToString();
                    /***********************************************/
                    p_objContent.m_strTumor = drCurrent["TUMOR"].ToString();
                    p_objContent.m_strT = drCurrent["T"].ToString();
                    p_objContent.m_strN = drCurrent["N"].ToString();
                    p_objContent.m_strM = drCurrent["M"].ToString();
                    p_objContent.m_strInstallments = drCurrent["INSTALLMENTS"].ToString();
                    p_objContent.m_strMetastaisCount = drCurrent["METASTASISCOUNT"].ToString();

                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }			//返回
            return lngRes;

        }

        /// <summary>
        /// 获得已删除的主子表的内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeletedContentInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Content p_objContent)
        {
            p_objContent = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInHospitalMainRecordServ","m_lngGetDeletedContentInfo");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;
            string m_strCommand = null;
            #region MyRegion
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                m_strCommand = @"select top 1 a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.lastmodifydate,
       a.lastmodifyuserid,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.diagnosis,
       a.inhospitaldiagnosis,
       a.doctor,
       a.confirmdiagnosisdate,
       a.condictionwhenin,
       a.maindiagnosis,
       a.mainconditionseq,
       a.icd_10ofmain,
       a.infectiondiagnosis,
       a.infectioncondictionseq,
       a.icd_10ofinfection,
       a.pathologydiagnosis,
       a.scachesource,
       a.sensitive,
       a.hbsag,
       a.hcv_ab,
       a.hiv_ab,
       a.accordwithouthospital,
       a.accordinwithout,
       a.accordbeforeoperationwithafter,
       a.accordclinicwithpathology,
       a.accordradiatewithpathology,
       a.salvetimes,
       a.salvesuccess,
       a.directordt,
       a.subdirectordt,
       a.dt,
       a.inhospitaldt,
       a.attendinforadvancesstudydt,
       a.graduatestudentintern,
       a.intern,
       a.coder,
       a.quality,
       a.qcdt,
       a.qcnurse,
       a.qctime,
       a.rtmodeseq,
       a.rtruleseq,
       a.rtco,
       a.rtaccelerator,
       a.rtx_ray,
       a.rtlacuna,
       a.originaldiseaseseq,
       a.originaldiseasegy,
       a.originaldiseasetimes,
       a.originaldiseasedays,
       a.originaldiseasebegindate,
       a.originaldiseaseenddate,
       a.lymphseq,
       a.lymphgy,
       a.lymphtimes,
       a.lymphdays,
       a.lymphbegindate,
       a.lymphenddate,
       a.metastasisgy,
       a.metastasistimes,
       a.metastasisdays,
       a.metastasisbegindate,
       a.metastasisenddate,
       a.chemotherapymodeseq,
       a.chemotherapywholebody,
       a.chemotherapylocal,
       a.chemotherapyintubate,
       a.chemotherapythorax,
       a.chemotherapyabdomen,
       a.chemotherapyspinal,
       a.chemotherapyothertry,
       a.chemotherapyother,
       a.totalamt,
       a.bedamt,
       a.nurseamt,
       a.wmamt,
       a.cmfinishedamt,
       a.cmsemifinishedamt,
       a.radiationamt,
       a.assayamt,
       a.o2amt,
       a.bloodamt,
       a.treatmentamt,
       a.operationamt,
       a.deliverychildamt,
       a.checkamt,
       a.anaethesiaamt,
       a.babyamt,
       a.accompanyamt,
       a.otheramt1,
       a.otheramt2,
       a.otheramt3,
       a.corpsecheck,
       a.firstcase,
       a.follow,
       a.follow_week,
       a.follow_month,
       a.follow_year,
       a.modelcase,
       a.bloodtype,
       a.bloodrh,
       a.bloodtransactoin,
       a.rbc,
       a.plt,
       a.plasm,
       a.wholeblood,
       a.otherblood,
       a.consultation,
       a.longdistanctconsultation,
       a.toplevel,
       a.nurseleveli,
       a.nurselevelii,
       a.nurseleveliii,
       a.icu,
       a.specialnurse,
       a.insurancenum,
       a.modeofpayment,
       a.patienthistoryno,
       a.outpatientdate,
       a.birthplace,
       a.operation,
       a.baby,
       a.chemotherapy,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.doctor=pbi.empno_chr) as doctorname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.directordt=pbi.empno_chr) as directordtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.subdirectordt=pbi.empno_chr) as subdirectordtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.dt=pbi.empno_chr) as dtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.inhospitaldt=pbi.empno_chr) as inhospitaldtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.attendinforadvancesstudydt=pbi.empno_chr) as attendinforadvancesstudydtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.graduatestudentintern=pbi.empno_chr) as graduatestudentinternname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.coder=pbi.empno_chr) as codername,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.qcdt=pbi.empno_chr) as qcdtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.qcnurse=pbi.empno_chr) as qcnursename
								from inhospitalmainrecord_content a " +
                    "where inpatientid = ? and " +
                    "inpatientdate = ? and " +
                    "opendate=? and status =0 " +
                    "order by lastmodifydate desc";
            }
            else if (clsHRPTableService.bytDatabase_Selector == 2)
            {
                m_strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       diagnosis,
       inhospitaldiagnosis,
       doctor,
       confirmdiagnosisdate,
       condictionwhenin,
       maindiagnosis,
       mainconditionseq,
       icd_10ofmain,
       infectiondiagnosis,
       infectioncondictionseq,
       icd_10ofinfection,
       pathologydiagnosis,
       scachesource,
       sensitive,
       hbsag,
       hcv_ab,
       hiv_ab,
       accordwithouthospital,
       accordinwithout,
       accordbeforeoperationwithafter,
       accordclinicwithpathology,
       accordradiatewithpathology,
       salvetimes,
       salvesuccess,
       directordt,
       subdirectordt,
       dt,
       inhospitaldt,
       attendinforadvancesstudydt,
       graduatestudentintern,
       intern,
       coder,
       quality,
       qcdt,
       qcnurse,
       qctime,
       rtmodeseq,
       rtruleseq,
       rtco,
       rtaccelerator,
       rtx_ray,
       rtlacuna,
       originaldiseaseseq,
       originaldiseasegy,
       originaldiseasetimes,
       originaldiseasedays,
       originaldiseasebegindate,
       originaldiseaseenddate,
       lymphseq,
       lymphgy,
       lymphtimes,
       lymphdays,
       lymphbegindate,
       lymphenddate,
       metastasisgy,
       metastasistimes,
       metastasisdays,
       metastasisbegindate,
       metastasisenddate,
       chemotherapymodeseq,
       chemotherapywholebody,
       chemotherapylocal,
       chemotherapyintubate,
       chemotherapythorax,
       chemotherapyabdomen,
       chemotherapyspinal,
       chemotherapyothertry,
       chemotherapyother,
       totalamt,
       bedamt,
       nurseamt,
       wmamt,
       cmfinishedamt,
       cmsemifinishedamt,
       radiationamt,
       assayamt,
       o2amt,
       bloodamt,
       treatmentamt,
       operationamt,
       deliverychildamt,
       checkamt,
       anaethesiaamt,
       babyamt,
       accompanyamt,
       otheramt1,
       otheramt2,
       otheramt3,
       corpsecheck,
       firstcase,
       follow,
       follow_week,
       follow_month,
       follow_year,
       modelcase,
       bloodtype,
       bloodrh,
       bloodtransactoin,
       rbc,
       plt,
       plasm,
       wholeblood,
       otherblood,
       consultation,
       longdistanctconsultation,
       toplevel,
       nurseleveli,
       nurselevelii,
       nurseleveliii,
       icu,
       specialnurse,
       insurancenum,
       modeofpayment,
       patienthistoryno,
       outpatientdate,
       birthplace,
       operation,
       baby,
       chemotherapy,
       doctorname,
       directordtname,
       subdirectordtname,
       dtname,
       inhospitaldtname,
       attendinforadvancesstudydtname,
       graduatestudentinternname,
       codername,
       qcdtname,
       qcnursename
  from (select a.inpatientid,
               a.inpatientdate,
               a.opendate,
               a.lastmodifydate,
               a.lastmodifyuserid,
               a.deactiveddate,
               a.deactivedoperatorid,
               a.status,
               a.diagnosis,
               a.inhospitaldiagnosis,
               a.doctor,
               a.confirmdiagnosisdate,
               a.condictionwhenin,
               a.maindiagnosis,
               a.mainconditionseq,
               a.icd_10ofmain,
               a.infectiondiagnosis,
               a.infectioncondictionseq,
               a.icd_10ofinfection,
               a.pathologydiagnosis,
               a.scachesource,
               a.sensitive,
               a.hbsag,
               a.hcv_ab,
               a.hiv_ab,
               a.accordwithouthospital,
               a.accordinwithout,
               a.accordbeforeoperationwithafter,
               a.accordclinicwithpathology,
               a.accordradiatewithpathology,
               a.salvetimes,
               a.salvesuccess,
               a.directordt,
               a.subdirectordt,
               a.dt,
               a.inhospitaldt,
               a.attendinforadvancesstudydt,
               a.graduatestudentintern,
               a.intern,
               a.coder,
               a.quality,
               a.qcdt,
               a.qcnurse,
               a.qctime,
               a.rtmodeseq,
               a.rtruleseq,
               a.rtco,
               a.rtaccelerator,
               a.rtx_ray,
               a.rtlacuna,
               a.originaldiseaseseq,
               a.originaldiseasegy,
               a.originaldiseasetimes,
               a.originaldiseasedays,
               a.originaldiseasebegindate,
               a.originaldiseaseenddate,
               a.lymphseq,
               a.lymphgy,
               a.lymphtimes,
               a.lymphdays,
               a.lymphbegindate,
               a.lymphenddate,
               a.metastasisgy,
               a.metastasistimes,
               a.metastasisdays,
               a.metastasisbegindate,
               a.metastasisenddate,
               a.chemotherapymodeseq,
               a.chemotherapywholebody,
               a.chemotherapylocal,
               a.chemotherapyintubate,
               a.chemotherapythorax,
               a.chemotherapyabdomen,
               a.chemotherapyspinal,
               a.chemotherapyothertry,
               a.chemotherapyother,
               a.totalamt,
               a.bedamt,
               a.nurseamt,
               a.wmamt,
               a.cmfinishedamt,
               a.cmsemifinishedamt,
               a.radiationamt,
               a.assayamt,
               a.o2amt,
               a.bloodamt,
               a.treatmentamt,
               a.operationamt,
               a.deliverychildamt,
               a.checkamt,
               a.anaethesiaamt,
               a.babyamt,
               a.accompanyamt,
               a.otheramt1,
               a.otheramt2,
               a.otheramt3,
               a.corpsecheck,
               a.firstcase,
               a.follow,
               a.follow_week,
               a.follow_month,
               a.follow_year,
               a.modelcase,
               a.bloodtype,
               a.bloodrh,
               a.bloodtransactoin,
               a.rbc,
               a.plt,
               a.plasm,
               a.wholeblood,
               a.otherblood,
               a.consultation,
               a.longdistanctconsultation,
               a.toplevel,
               a.nurseleveli,
               a.nurselevelii,
               a.nurseleveliii,
               a.icu,
               a.specialnurse,
               a.insurancenum,
               a.modeofpayment,
               a.patienthistoryno,
               a.outpatientdate,
               a.birthplace,
               a.operation,
               a.baby,
               a.chemotherapy,
               (select pbi.lastname_vchr as firstname
                  from t_bse_employee pbi
                 where a.doctor = pbi.empno_chr) as doctorname,
               (select pbi.lastname_vchr as firstname
                  from t_bse_employee pbi
                 where a.directordt = pbi.empno_chr) as directordtname,
               (select pbi.lastname_vchr as firstname
                  from t_bse_employee pbi
                 where a.subdirectordt = pbi.empno_chr) as subdirectordtname,
               (select pbi.lastname_vchr as firstname
                  from t_bse_employee pbi
                 where a.dt = pbi.empno_chr) as dtname,
               (select pbi.lastname_vchr as firstname
                  from t_bse_employee pbi
                 where a.inhospitaldt = pbi.empno_chr) as inhospitaldtname,
               (select pbi.lastname_vchr as firstname
                  from t_bse_employee pbi
                 where a.attendinforadvancesstudydt = pbi.empno_chr) as attendinforadvancesstudydtname,
               (select pbi.lastname_vchr as firstname
                  from t_bse_employee pbi
                 where a.graduatestudentintern = pbi.empno_chr) as graduatestudentinternname,
               (select pbi.lastname_vchr as firstname
                  from t_bse_employee pbi
                 where a.coder = pbi.empno_chr) as codername,
               (select pbi.lastname_vchr as firstname
                  from t_bse_employee pbi
                 where a.qcdt = pbi.empno_chr) as qcdtname,
               (select pbi.lastname_vchr as firstname
                  from t_bse_employee pbi
                 where a.qcnurse = pbi.empno_chr) as qcnursename
          from inhospitalmainrecord_content a
         where inpatientid = ?
           and inpatientdate = ?
           and opendate = ?
           and status = 0
         order by lastmodifydate desc)
 where rownum = 1";
            }
            else if (clsHRPTableService.bytDatabase_Selector == 4)
            {
                m_strCommand = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.lastmodifydate,
       a.lastmodifyuserid,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.diagnosis,
       a.inhospitaldiagnosis,
       a.doctor,
       a.confirmdiagnosisdate,
       a.condictionwhenin,
       a.maindiagnosis,
       a.mainconditionseq,
       a.icd_10ofmain,
       a.infectiondiagnosis,
       a.infectioncondictionseq,
       a.icd_10ofinfection,
       a.pathologydiagnosis,
       a.scachesource,
       a.sensitive,
       a.hbsag,
       a.hcv_ab,
       a.hiv_ab,
       a.accordwithouthospital,
       a.accordinwithout,
       a.accordbeforeoperationwithafter,
       a.accordclinicwithpathology,
       a.accordradiatewithpathology,
       a.salvetimes,
       a.salvesuccess,
       a.directordt,
       a.subdirectordt,
       a.dt,
       a.inhospitaldt,
       a.attendinforadvancesstudydt,
       a.graduatestudentintern,
       a.intern,
       a.coder,
       a.quality,
       a.qcdt,
       a.qcnurse,
       a.qctime,
       a.rtmodeseq,
       a.rtruleseq,
       a.rtco,
       a.rtaccelerator,
       a.rtx_ray,
       a.rtlacuna,
       a.originaldiseaseseq,
       a.originaldiseasegy,
       a.originaldiseasetimes,
       a.originaldiseasedays,
       a.originaldiseasebegindate,
       a.originaldiseaseenddate,
       a.lymphseq,
       a.lymphgy,
       a.lymphtimes,
       a.lymphdays,
       a.lymphbegindate,
       a.lymphenddate,
       a.metastasisgy,
       a.metastasistimes,
       a.metastasisdays,
       a.metastasisbegindate,
       a.metastasisenddate,
       a.chemotherapymodeseq,
       a.chemotherapywholebody,
       a.chemotherapylocal,
       a.chemotherapyintubate,
       a.chemotherapythorax,
       a.chemotherapyabdomen,
       a.chemotherapyspinal,
       a.chemotherapyothertry,
       a.chemotherapyother,
       a.totalamt,
       a.bedamt,
       a.nurseamt,
       a.wmamt,
       a.cmfinishedamt,
       a.cmsemifinishedamt,
       a.radiationamt,
       a.assayamt,
       a.o2amt,
       a.bloodamt,
       a.treatmentamt,
       a.operationamt,
       a.deliverychildamt,
       a.checkamt,
       a.anaethesiaamt,
       a.babyamt,
       a.accompanyamt,
       a.otheramt1,
       a.otheramt2,
       a.otheramt3,
       a.corpsecheck,
       a.firstcase,
       a.follow,
       a.follow_week,
       a.follow_month,
       a.follow_year,
       a.modelcase,
       a.bloodtype,
       a.bloodrh,
       a.bloodtransactoin,
       a.rbc,
       a.plt,
       a.plasm,
       a.wholeblood,
       a.otherblood,
       a.consultation,
       a.longdistanctconsultation,
       a.toplevel,
       a.nurseleveli,
       a.nurselevelii,
       a.nurseleveliii,
       a.icu,
       a.specialnurse,
       a.insurancenum,
       a.modeofpayment,
       a.patienthistoryno,
       a.outpatientdate,
       a.birthplace,
       a.operation,
       a.baby,
       a.chemotherapy,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.doctor=pbi.empno_chr) as doctorname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.directordt=pbi.empno_chr) as directordtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.subdirectordt=pbi.empno_chr) as subdirectordtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.dt=pbi.empno_chr) as dtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.inhospitaldt=pbi.empno_chr) as inhospitaldtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.attendinforadvancesstudydt=pbi.empno_chr) as attendinforadvancesstudydtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.graduatestudentintern=pbi.empno_chr) as graduatestudentinternname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.coder=pbi.empno_chr) as codername,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.qcdt=pbi.empno_chr) as qcdtname,
									(select pbi.lastname_vchr as firstname  from t_bse_employee pbi where a.qcnurse=pbi.empno_chr) as qcnursename
								from inhospitalmainrecord_content a " +
                    "where inpatientid = ? and " +
                    "inpatientdate = ? and " +
                    "opendate= ? and status =0 " +
                    "order by lastmodifydate desc fetch first 1 row only";
            }

            #endregion

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    DataRow drCurrent = dtbResult.Rows[0];
                    p_objContent = new clsInHospitalMainRecord_Content();
                    p_objContent.m_strInPatientID = drCurrent["INPATIENTID"].ToString();
                    p_objContent.m_strInPatientDate = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strOpenDate = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strLastModifyDate = Convert.ToDateTime(drCurrent["LASTMODIFYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strLastModifyUserID = drCurrent["LASTMODIFYUSERID"].ToString();
                    p_objContent.m_strDeActivedDate = Convert.ToDateTime(drCurrent["DEACTIVEDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strDeActivedOperatorID = drCurrent["DEACTIVEDOPERATORID"].ToString();
                    p_objContent.m_strDiagnosis = drCurrent["DIAGNOSIS"].ToString();
                    p_objContent.m_strInHospitalDiagnosis = drCurrent["INHOSPITALDIAGNOSIS"].ToString();
                    p_objContent.m_strDoctor = drCurrent["DOCTOR"].ToString();
                    p_objContent.m_strConfirmDiagnosisDate = Convert.ToDateTime(drCurrent["CONFIRMDIAGNOSISDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strCondictionWhenIn = drCurrent["CONDICTIONWHENIN"].ToString();
                    p_objContent.m_strMainDiagnosis = drCurrent["MAINDIAGNOSIS"].ToString();
                    p_objContent.m_strMainConditionSeq = drCurrent["MAINCONDITIONSEQ"].ToString();
                    p_objContent.m_strICD_10OfMain = drCurrent["ICD_10OFMAIN"].ToString();
                    p_objContent.m_strInfectionDiagnosis = drCurrent["INFECTIONDIAGNOSIS"].ToString();
                    p_objContent.m_strICD_10OfInfection = drCurrent["ICD_10OFINFECTION"].ToString();
                    p_objContent.m_strPathologyDiagnosis = drCurrent["PATHOLOGYDIAGNOSIS"].ToString();
                    p_objContent.m_strScacheSource = drCurrent["SCACHESOURCE"].ToString();
                    p_objContent.m_strSensitive = drCurrent["SENSITIVE"].ToString();
                    p_objContent.m_strHbsAg = drCurrent["HBSAG"].ToString();
                    p_objContent.m_strHCV_Ab = drCurrent["HCV_AB"].ToString();
                    p_objContent.m_strHIV_Ab = drCurrent["HIV_AB"].ToString();
                    p_objContent.m_strAccordWithOutHospital = drCurrent["ACCORDWITHOUTHOSPITAL"].ToString();
                    p_objContent.m_strAccordInWithOut = drCurrent["ACCORDINWITHOUT"].ToString();
                    p_objContent.m_strAccordBeforeOperationWithAfter = drCurrent["ACCORDBEFOREOPERATIONWITHAFTER"].ToString();
                    p_objContent.m_strAccordClinicWithPathology = drCurrent["ACCORDCLINICWITHPATHOLOGY"].ToString();
                    p_objContent.m_strAccordRadiateWithPathology = drCurrent["ACCORDRADIATEWITHPATHOLOGY"].ToString();
                    p_objContent.m_strSalveTimes = drCurrent["SALVETIMES"].ToString();
                    p_objContent.m_strSalveSuccess = drCurrent["SALVESUCCESS"].ToString();
                    p_objContent.m_strDirectorDt = drCurrent["DIRECTORDT"].ToString();
                    p_objContent.m_strSubDirectorDt = drCurrent["SUBDIRECTORDT"].ToString();
                    p_objContent.m_strDt = drCurrent["DT"].ToString();
                    p_objContent.m_strInHospitalDt = drCurrent["INHOSPITALDT"].ToString();
                    p_objContent.m_strAttendInForAdvancesStudyDt = drCurrent["ATTENDINFORADVANCESSTUDYDT"].ToString();
                    p_objContent.m_strGraduateStudentIntern = drCurrent["GRADUATESTUDENTINTERN"].ToString();
                    p_objContent.m_strIntern = drCurrent["INTERN"].ToString();
                    p_objContent.m_strCoder = drCurrent["CODER"].ToString();
                    p_objContent.m_strQCDt = drCurrent["QCDT"].ToString();
                    p_objContent.m_strQCNurse = drCurrent["QCNURSE"].ToString();
                    p_objContent.m_strQCTime = Convert.ToDateTime(drCurrent["QCTIME"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strRTModeSeq = drCurrent["RTMODESEQ"].ToString();
                    p_objContent.m_strRTRuleSeq = drCurrent["RTRULESEQ"].ToString();
                    p_objContent.m_strRTCo = drCurrent["RTCO"].ToString();
                    p_objContent.m_strRTAccelerator = drCurrent["RTACCELERATOR"].ToString();
                    p_objContent.m_strRTX_Ray = drCurrent["RTX_RAY"].ToString();
                    p_objContent.m_strRTLacuna = drCurrent["RTLACUNA"].ToString();
                    p_objContent.m_strOriginalDiseaseSeq = drCurrent["ORIGINALDISEASESEQ"].ToString();
                    p_objContent.m_strOriginalDiseaseGy = drCurrent["ORIGINALDISEASEGY"].ToString();
                    p_objContent.m_strOriginalDiseaseTimes = drCurrent["ORIGINALDISEASETIMES"].ToString();
                    p_objContent.m_strOriginalDiseaseDays = drCurrent["ORIGINALDISEASEDAYS"].ToString();
                    p_objContent.m_strOriginalDiseaseBeginDate = Convert.ToDateTime(drCurrent["ORIGINALDISEASEBEGINDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strOriginalDiseaseEndDate = Convert.ToDateTime(drCurrent["ORIGINALDISEASEENDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strLymphSeq = drCurrent["LYMPHSEQ"].ToString();
                    p_objContent.m_strLymphGy = drCurrent["LYMPHGY"].ToString();
                    p_objContent.m_strLymphTimes = drCurrent["LYMPHTIMES"].ToString();
                    p_objContent.m_strLymphDays = drCurrent["LYMPHDAYS"].ToString();
                    p_objContent.m_strLymphBeginDate = Convert.ToDateTime(drCurrent["LYMPHBEGINDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strLymphEndDate = Convert.ToDateTime(drCurrent["LYMPHENDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strMetastasisGy = drCurrent["METASTASISGY"].ToString();
                    p_objContent.m_strMetastasisTimes = drCurrent["METASTASISTIMES"].ToString();
                    p_objContent.m_strMetastasisDays = drCurrent["METASTASISDAYS"].ToString();
                    p_objContent.m_strMetastasisBeginDate = Convert.ToDateTime(drCurrent["METASTASISBEGINDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strMetastasisEndDate = Convert.ToDateTime(drCurrent["METASTASISENDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strChemotherapyModeSeq = drCurrent["CHEMOTHERAPYMODESEQ"].ToString();
                    p_objContent.m_strChemotherapyWholeBody = drCurrent["CHEMOTHERAPYWHOLEBODY"].ToString();
                    p_objContent.m_strChemotherapyLocal = drCurrent["CHEMOTHERAPYLOCAL"].ToString();
                    p_objContent.m_strChemotherapyIntubate = drCurrent["CHEMOTHERAPYINTUBATE"].ToString();
                    p_objContent.m_strChemotherapyThorax = drCurrent["CHEMOTHERAPYTHORAX"].ToString();
                    p_objContent.m_strChemotherapyAbdomen = drCurrent["CHEMOTHERAPYABDOMEN"].ToString();
                    p_objContent.m_strChemotherapySpinal = drCurrent["CHEMOTHERAPYSPINAL"].ToString();
                    p_objContent.m_strChemotherapyOtherTry = drCurrent["CHEMOTHERAPYOTHERTRY"].ToString();
                    p_objContent.m_strChemotherapyOther = drCurrent["CHEMOTHERAPYOTHER"].ToString();
                    p_objContent.m_strTotalAmt = drCurrent["TOTALAMT"].ToString();
                    p_objContent.m_strBedAmt = drCurrent["BEDAMT"].ToString();
                    p_objContent.m_strNurseAmt = drCurrent["NURSEAMT"].ToString();
                    p_objContent.m_strWMAmt = drCurrent["WMAMT"].ToString();
                    p_objContent.m_strCMFinishedAmt = drCurrent["CMFINISHEDAMT"].ToString();
                    p_objContent.m_strCMSemiFinishedAmt = drCurrent["CMSEMIFINISHEDAMT"].ToString();
                    p_objContent.m_strRadiationAmt = drCurrent["RADIATIONAMT"].ToString();
                    p_objContent.m_strAssayAmt = drCurrent["ASSAYAMT"].ToString();
                    p_objContent.m_strO2Amt = drCurrent["O2AMT"].ToString();
                    p_objContent.m_strBloodAmt = drCurrent["BLOODAMT"].ToString();
                    p_objContent.m_strTreatmentAmt = drCurrent["TREATMENTAMT"].ToString();
                    p_objContent.m_strOperationAmt = drCurrent["OPERATIONAMT"].ToString();
                    p_objContent.m_strDeliveryChildAmt = drCurrent["DELIVERYCHILDAMT"].ToString();
                    p_objContent.m_strCheckAmt = drCurrent["CHECKAMT"].ToString();
                    p_objContent.m_strAnaethesiaAmt = drCurrent["ANAETHESIAAMT"].ToString();
                    p_objContent.m_strBabyAmt = drCurrent["BABYAMT"].ToString();
                    p_objContent.m_strAccompanyAmt = drCurrent["ACCOMPANYAMT"].ToString();
                    p_objContent.m_strOtherAmt1 = drCurrent["OTHERAMT1"].ToString();
                    p_objContent.m_strOtherAmt2 = drCurrent["OTHERAMT2"].ToString();
                    p_objContent.m_strOtherAmt3 = drCurrent["OTHERAMT3"].ToString();
                    p_objContent.m_strCorpseCheck = drCurrent["CORPSECHECK"].ToString();
                    p_objContent.m_strFirstCase = drCurrent["FIRSTCASE"].ToString();
                    p_objContent.m_strFollow = drCurrent["FOLLOW"].ToString();
                    p_objContent.m_strFollow_Week = drCurrent["FOLLOW_WEEK"].ToString();
                    p_objContent.m_strFollow_Month = drCurrent["FOLLOW_MONTH"].ToString();
                    p_objContent.m_strFollow_Year = drCurrent["FOLLOW_YEAR"].ToString();
                    p_objContent.m_strModelCase = drCurrent["MODELCASE"].ToString();
                    p_objContent.m_strBloodType = drCurrent["BLOODTYPE"].ToString();
                    p_objContent.m_strBloodRh = drCurrent["BLOODRH"].ToString();
                    p_objContent.m_strBloodTransActoin = drCurrent["BLOODTRANSACTOIN"].ToString();
                    p_objContent.m_strRBC = drCurrent["RBC"].ToString();
                    p_objContent.m_strPLT = drCurrent["PLT"].ToString();
                    p_objContent.m_strPlasm = drCurrent["PLASM"].ToString();
                    p_objContent.m_strWholeBlood = drCurrent["WHOLEBLOOD"].ToString();
                    p_objContent.m_strOtherBlood = drCurrent["OTHERBLOOD"].ToString();
                    p_objContent.m_strConsultation = drCurrent["CONSULTATION"].ToString();
                    p_objContent.m_strLongDistanctConsultation = drCurrent["LONGDISTANCTCONSULTATION"].ToString();
                    p_objContent.m_strTOPLevel = drCurrent["TOPLEVEL"].ToString();
                    p_objContent.m_strNurseLevelI = drCurrent["NURSELEVELI"].ToString();
                    p_objContent.m_strNurseLevelII = drCurrent["NURSELEVELII"].ToString();
                    p_objContent.m_strNurseLevelIII = drCurrent["NURSELEVELIII"].ToString();
                    p_objContent.m_strICU = drCurrent["ICU"].ToString();
                    p_objContent.m_strSpecialNurse = drCurrent["SPECIALNURSE"].ToString();
                    p_objContent.m_strInsuranceNum = drCurrent["INSURANCENUM"].ToString();
                    p_objContent.m_strModeOfPayment = drCurrent["MODEOFPAYMENT"].ToString();
                    p_objContent.m_strPatientHistoryNO = drCurrent["PATIENTHISTORYNO"].ToString();
                    p_objContent.m_strOutPatientDate = Convert.ToDateTime(drCurrent["OUTPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objContent.m_strBirthPlace = drCurrent["BIRTHPLACE"].ToString();
                    p_objContent.m_strDoctorName = drCurrent["DoctorName"].ToString();
                    p_objContent.m_strDirectorDtName = drCurrent["DirectorDtName"].ToString();
                    p_objContent.m_strSubDirectorDtName = drCurrent["SubDirectorDtName"].ToString();
                    p_objContent.m_strDtName = drCurrent["DtName"].ToString();
                    p_objContent.m_strInHospitalDtName = drCurrent["InHospitalDtName"].ToString();
                    p_objContent.m_strAttendInForAdvancesStudyDtName = drCurrent["AttendInForAdvancesStudyDtName"].ToString();
                    p_objContent.m_strGraduateStudentInternName = drCurrent["GraduateStudentInternName"].ToString();
                    p_objContent.m_strCoderName = drCurrent["CoderName"].ToString();
                    p_objContent.m_strQCDtName = drCurrent["QCDtName"].ToString();
                    p_objContent.m_strQCNurseName = drCurrent["QCNurseName"].ToString();
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

        #region 获得其它诊断子表
        /// <summary>
        /// 获得其它诊断子表(此表已废弃)
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strXML"></param>
        /// <param name="p_intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOtherDiagnosisInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strXML, out int p_intRows)
        {
            p_strXML = null;
            p_intRows = 0;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetOtherDiagnosisInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                string m_strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       seqid,
       diagnosisdesc,
       conditionseq,
       icd10
  from ihmainrecord_otherdiagnosis
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 1
 order by seqid";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);


                lngRes = objHRPServ.lngGetXMLTableWithParameter(m_strCommand, ref p_strXML, ref p_intRows, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strXML"></param>
        /// <param name="p_intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeletedOtherDiagnosisInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strXML, out int p_intRows)
        {
            p_strXML = null;
            p_intRows = 0;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetDeletedOtherDiagnosisInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                string m_strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       seqid,
       diagnosisdesc,
       conditionseq,
       icd10 from ihmainrecord_otherdiagnosis 
                    where inpatientid = ? and 
                    inpatientdate = ? and 
                    opendate=? and status =1  order by seqid";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(m_strCommand, ref p_strXML, ref p_intRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;


        }

        #endregion

        #region 获得手术子表
        /// <summary>
        /// 获得手术子表
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOperationInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Operation[] p_objResultArr)
        {
            p_objResultArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetOperationInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                //                string m_strCommand = @"SELECT a.*,
                //										(select m.AnaesthesiaModeName  from AnaesthesiaMode m where a.AanaesthesiaModeID=m.AnaesthesiaModeID)  AanaesthesiaModeName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid") + @"(a.Operator) OperatorName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid") + @"(a.Assistant1) Assistant1Name,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid") + @"(a.Assistant2) Assistant2Name
                //					FROM InHospitalMainRecord_Operation a 
                //                    Where InPatientID = ? AND 
                //                    InPatientDate = ? AND 
                //                    OpenDate=? AND Status =1  ORDER BY SeqID";
                System.Text.StringBuilder stbSQL = new System.Text.StringBuilder(200);
                stbSQL.Append(@"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.lastmodifydate,
       a.lastmodifyuserid,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.seqid,
       a.operationid,
       a.operationdate,
       a.operationname,
       a.operator,
       a.assistant1,
       a.assistant2,
       a.aanaesthesiamodeid,
       a.cutlevel,
       a.anaesthetist,
       a.operationaanaesthesiamodename,
       a.operationanaesthetistname,a.operationlevel,a.operationelective,
                                        ");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.operator) operatorname,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.assistant1) assistant1name,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.assistant2) assistant2name,
                                        ");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.anaesthetist) anaesthetistname
					from inhospitalmainrecord_operation a 
                    where inpatientid = ? and 
                    inpatientdate = ? and 
                    opendate=? and status =1  order by seqid");

                string m_strCommand = stbSQL.ToString();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    DataRow drCurrent = null;
                    p_objResultArr = new clsInHospitalMainRecord_Operation[intRowsCount];
                    for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                    {
                        drCurrent = dtbResult.Rows[rowIndex];
                        p_objResultArr[rowIndex] = new clsInHospitalMainRecord_Operation();
                        p_objResultArr[rowIndex].m_strInPatientID = drCurrent["INPATIENTID"].ToString();
                        p_objResultArr[rowIndex].m_strInPatientDate = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strOpenDate = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyDate = Convert.ToDateTime(drCurrent["LASTMODIFYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyUserID = drCurrent["LASTMODIFYUSERID"].ToString();
                        p_objResultArr[rowIndex].m_strSeqID = drCurrent["SEQID"].ToString();
                        p_objResultArr[rowIndex].m_strOperationID = drCurrent["OPERATIONID"].ToString();
                        DateTime dtmTemp = DateTime.MinValue;
                        if (DateTime.TryParse(drCurrent["OPERATIONDATE"].ToString(), out dtmTemp))
                        {
                            p_objResultArr[rowIndex].m_strOperationDate = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            p_objResultArr[rowIndex].m_strOperationDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        p_objResultArr[rowIndex].m_strOperationName = drCurrent["OPERATIONNAME"].ToString();
                        p_objResultArr[rowIndex].m_strOperator = drCurrent["OPERATOR"].ToString();
                        p_objResultArr[rowIndex].m_strAssistant1 = drCurrent["ASSISTANT1"].ToString();
                        p_objResultArr[rowIndex].m_strAssistant2 = drCurrent["ASSISTANT2"].ToString();
                        p_objResultArr[rowIndex].m_strAanaesthesiaModeID = drCurrent["AANAESTHESIAMODEID"].ToString();
                        p_objResultArr[rowIndex].m_strCutLevel = drCurrent["CUTLEVEL"].ToString();
                        p_objResultArr[rowIndex].m_strAnaesthetist = drCurrent["ANAESTHETIST"].ToString();
                        p_objResultArr[rowIndex].m_strAanaesthesiaModeName = drCurrent["OPERATIONAANAESTHESIAMODENAME"].ToString();
                        p_objResultArr[rowIndex].m_strAnaesthetistName = drCurrent["anaesthetistname"].ToString();
                        p_objResultArr[rowIndex].m_strOperatorName = drCurrent["OperatorName"].ToString();
                        p_objResultArr[rowIndex].m_strAssistant1Name = drCurrent["Assistant1Name"].ToString();
                        p_objResultArr[rowIndex].m_strAssistant2Name = drCurrent["Assistant2Name"].ToString();
                        p_objResultArr[rowIndex].m_strOpreationLevel = drCurrent["OPERATIONLEVEL"].ToString();
                        p_objResultArr[rowIndex].m_strOpreationElective = drCurrent["OPERATIONELECTIVE"].ToString();//
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }
        [AutoComplete]
        public long m_lngGetDeletedOperationInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Operation[] p_objResultArr)
        {
            p_objResultArr = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetDeletedOperationInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                //                string m_strCommand = @"SELECT a.*,
                //										(select m.AnaesthesiaModeName  from AnaesthesiaMode m where a.AanaesthesiaModeID=m.AnaesthesiaModeID)  AanaesthesiaModeName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid") + @"(a.Operator) OperatorName,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid") + @"(a.Assistant1) Assistant1Name,
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid") + @"(a.Assistant2) Assistant2Name
                //										" + clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid") + @"(a.Anaesthetist)  AnaesthetistName 
                //					FROM InHospitalMainRecord_Operation a 
                //                    Where InPatientID = ? AND 
                //                    InPatientDate = ? AND 
                //                    OpenDate=? AND Status =0 
                //                    AND LastModifyDate IN (SELECT MAX(LastModifyDate) AS MAX_LastModifyDate 
                //                    FROM InHospitalMainRecord_Operation WHERE 
                //                    InPatientID = ? AND 
                //                    InPatientDate = ? AND 
                //                    OpenDate=? AND Status =0) ORDER BY SeqID";

                System.Text.StringBuilder stbSQL = new System.Text.StringBuilder(200);
                stbSQL.Append(@"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.lastmodifydate,
       a.lastmodifyuserid,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.seqid,
       a.operationid,
       a.operationdate,
       a.operationname,
       a.operator,
       a.assistant1,
       a.assistant2,
       a.aanaesthesiamodeid,
       a.cutlevel,
       a.anaesthetist,
       a.operationaanaesthesiamodename,
       a.operationanaesthetistname,a.operationlevel,a.operationelective,
                                        ");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.operator) operatorname,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.assistant1) assistant1name,
										");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.assistant2) assistant2name,
                                        ");
                stbSQL.Append(clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyid"));
                stbSQL.Append(@"(a.anaesthetist) anaesthetistname 
					from inhospitalmainrecord_operation a 
                    where inpatientid = ? and 
                    inpatientdate = ? and 
                    opendate=? and status =0 
                    and lastmodifydate in (select max(lastmodifydate) as max_lastmodifydate 
                    from inhospitalmainrecord_operation where 
                    inpatientid = ? and 
                    inpatientdate = ? and 
                    opendate=? and status =0) order by seqid");

                string m_strCommand = stbSQL.ToString();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    DataRow drCurrent = null;
                    p_objResultArr = new clsInHospitalMainRecord_Operation[intRowsCount];
                    for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                    {
                        drCurrent = dtbResult.Rows[rowIndex];
                        p_objResultArr[rowIndex] = new clsInHospitalMainRecord_Operation();
                        p_objResultArr[rowIndex].m_strInPatientID = drCurrent["INPATIENTID"].ToString();
                        p_objResultArr[rowIndex].m_strInPatientDate = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strOpenDate = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyDate = Convert.ToDateTime(drCurrent["LASTMODIFYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyUserID = drCurrent["LASTMODIFYUSERID"].ToString();
                        p_objResultArr[rowIndex].m_strDeActivedDate = Convert.ToDateTime(drCurrent["DEACTIVEDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strDeActivedOperatorID = drCurrent["DEACTIVEDOPERATORID"].ToString();
                        p_objResultArr[rowIndex].m_strSeqID = drCurrent["SEQID"].ToString();
                        p_objResultArr[rowIndex].m_strOperationID = drCurrent["OPERATIONID"].ToString();
                        DateTime dtmTemp = DateTime.MinValue;
                        if (DateTime.TryParse(drCurrent["OPERATIONDATE"].ToString(), out dtmTemp))
                        {
                            p_objResultArr[rowIndex].m_strOperationDate = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            p_objResultArr[rowIndex].m_strOperationDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        p_objResultArr[rowIndex].m_strOperationName = drCurrent["OPERATIONNAME"].ToString();
                        p_objResultArr[rowIndex].m_strOperator = drCurrent["OPERATOR"].ToString();
                        p_objResultArr[rowIndex].m_strAssistant1 = drCurrent["ASSISTANT1"].ToString();
                        p_objResultArr[rowIndex].m_strAssistant2 = drCurrent["ASSISTANT2"].ToString();
                        p_objResultArr[rowIndex].m_strAanaesthesiaModeID = drCurrent["AANAESTHESIAMODEID"].ToString();
                        p_objResultArr[rowIndex].m_strCutLevel = drCurrent["CUTLEVEL"].ToString();
                        p_objResultArr[rowIndex].m_strAnaesthetist = drCurrent["ANAESTHETIST"].ToString();
                        p_objResultArr[rowIndex].m_strAanaesthesiaModeName = drCurrent["OPERATIONAANAESTHESIAMODENAME"].ToString();
                        p_objResultArr[rowIndex].m_strAnaesthetistName = drCurrent["anaesthetistname"].ToString();
                        p_objResultArr[rowIndex].m_strOperatorName = drCurrent["OperatorName"].ToString();
                        p_objResultArr[rowIndex].m_strAssistant1Name = drCurrent["Assistant1Name"].ToString();
                        p_objResultArr[rowIndex].m_strAssistant2Name = drCurrent["Assistant2Name"].ToString();
                        p_objResultArr[rowIndex].m_strOpreationLevel = drCurrent["OPERATIONLEVEL"].ToString();
                        p_objResultArr[rowIndex].m_strOpreationElective = drCurrent["OPERATIONELECTIVE"].ToString();//
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }		//返回
            return lngRes;

        }

        #endregion

        #region 获得婴儿的子表
        /// <summary>
        /// 获得婴儿的子表
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBabyInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Baby[] p_objResultArr)
        {
            p_objResultArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetBabyInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                string strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       seqid,
       male,
       female,
       liveborn,
       dieborn,
       dienotborn,
       weight,
       die,
       changedepartment,
       outhospital,
       naturalcondiction,
       suffocate1,
       suffocate2,
       infectiontimes,
       infectionname,
       icd10,
       salvetimes,
       salvesuccesstimes
  from inhospitalmainrecord_baby
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 1
 order by seqid";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);


                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    DataRow drCurrent = null;
                    p_objResultArr = new clsInHospitalMainRecord_Baby[intRowsCount];
                    for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                    {
                        p_objResultArr[rowIndex] = new clsInHospitalMainRecord_Baby();
                        drCurrent = dtbResult.Rows[rowIndex];
                        p_objResultArr[rowIndex].m_strInPatientID = drCurrent["INPATIENTID"].ToString();
                        p_objResultArr[rowIndex].m_strInPatientDate = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strOpenDate = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyDate = Convert.ToDateTime(drCurrent["LASTMODIFYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyUserID = drCurrent["LASTMODIFYUSERID"].ToString();
                        p_objResultArr[rowIndex].m_strSeqID = drCurrent["SEQID"].ToString();
                        p_objResultArr[rowIndex].m_strMale = drCurrent["MALE"].ToString();
                        p_objResultArr[rowIndex].m_strFemale = drCurrent["FEMALE"].ToString();
                        p_objResultArr[rowIndex].m_strLiveBorn = drCurrent["LIVEBORN"].ToString();
                        p_objResultArr[rowIndex].m_strDieBorn = drCurrent["DIEBORN"].ToString();
                        p_objResultArr[rowIndex].m_strDieNotBorn = drCurrent["DIENOTBORN"].ToString();
                        p_objResultArr[rowIndex].m_strWeight = drCurrent["WEIGHT"].ToString();
                        p_objResultArr[rowIndex].m_strDie = drCurrent["DIE"].ToString();
                        p_objResultArr[rowIndex].m_strChangeDepartment = drCurrent["CHANGEDEPARTMENT"].ToString();
                        p_objResultArr[rowIndex].m_strOutHospital = drCurrent["OUTHOSPITAL"].ToString();
                        p_objResultArr[rowIndex].m_strNaturalCondiction = drCurrent["NATURALCONDICTION"].ToString();
                        p_objResultArr[rowIndex].m_strSuffocate1 = drCurrent["SUFFOCATE1"].ToString();
                        p_objResultArr[rowIndex].m_strSuffocate2 = drCurrent["SUFFOCATE2"].ToString();
                        p_objResultArr[rowIndex].m_strInfectionTimes = drCurrent["INFECTIONTIMES"].ToString();
                        p_objResultArr[rowIndex].m_strInfectionName = drCurrent["INFECTIONNAME"].ToString();
                        p_objResultArr[rowIndex].m_strICD10 = drCurrent["ICD10"].ToString();
                        p_objResultArr[rowIndex].m_strSalveTimes = drCurrent["SALVETIMES"].ToString();
                        p_objResultArr[rowIndex].m_strSalveSuccessTimes = drCurrent["SALVESUCCESSTIMES"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeletedBabyInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Baby[] p_objResultArr)
        {
            p_objResultArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetDeletedBabyInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                string m_strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       seqid,
       male,
       female,
       liveborn,
       dieborn,
       dienotborn,
       weight,
       die,
       changedepartment,
       outhospital,
       naturalcondiction,
       suffocate1,
       suffocate2,
       infectiontimes,
       infectionname,
       icd10,
       salvetimes,
       salvesuccesstimes
  from inhospitalmainrecord_baby
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 0
   and lastmodifydate in (select max(lastmodifydate) as max_lastmodifydate
                            from inhospitalmainrecord_baby
                           where inpatientid = ?
                             and inpatientdate = ?
                             and opendate = ?
                             and status = 0)
 order by seqid";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    DataRow drCurrent = null;
                    p_objResultArr = new clsInHospitalMainRecord_Baby[intRowsCount];
                    for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                    {
                        p_objResultArr[rowIndex] = new clsInHospitalMainRecord_Baby();
                        drCurrent = dtbResult.Rows[rowIndex];
                        p_objResultArr[rowIndex].m_strInPatientID = drCurrent["INPATIENTID"].ToString();
                        p_objResultArr[rowIndex].m_strInPatientDate = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strOpenDate = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyDate = Convert.ToDateTime(drCurrent["LASTMODIFYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyUserID = drCurrent["LASTMODIFYUSERID"].ToString();
                        p_objResultArr[rowIndex].m_strDeActivedDate = Convert.ToDateTime(drCurrent["DEACTIVEDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strDeActivedOperatorID = drCurrent["DEACTIVEDOPERATORID"].ToString();
                        p_objResultArr[rowIndex].m_strSeqID = drCurrent["SEQID"].ToString();
                        p_objResultArr[rowIndex].m_strMale = drCurrent["MALE"].ToString();
                        p_objResultArr[rowIndex].m_strFemale = drCurrent["FEMALE"].ToString();
                        p_objResultArr[rowIndex].m_strLiveBorn = drCurrent["LIVEBORN"].ToString();
                        p_objResultArr[rowIndex].m_strDieBorn = drCurrent["DIEBORN"].ToString();
                        p_objResultArr[rowIndex].m_strDieNotBorn = drCurrent["DIENOTBORN"].ToString();
                        p_objResultArr[rowIndex].m_strWeight = drCurrent["WEIGHT"].ToString();
                        p_objResultArr[rowIndex].m_strDie = drCurrent["DIE"].ToString();
                        p_objResultArr[rowIndex].m_strChangeDepartment = drCurrent["CHANGEDEPARTMENT"].ToString();
                        p_objResultArr[rowIndex].m_strOutHospital = drCurrent["OUTHOSPITAL"].ToString();
                        p_objResultArr[rowIndex].m_strNaturalCondiction = drCurrent["NATURALCONDICTION"].ToString();
                        p_objResultArr[rowIndex].m_strSuffocate1 = drCurrent["SUFFOCATE1"].ToString();
                        p_objResultArr[rowIndex].m_strSuffocate2 = drCurrent["SUFFOCATE2"].ToString();
                        p_objResultArr[rowIndex].m_strInfectionTimes = drCurrent["INFECTIONTIMES"].ToString();
                        p_objResultArr[rowIndex].m_strInfectionName = drCurrent["INFECTIONNAME"].ToString();
                        p_objResultArr[rowIndex].m_strICD10 = drCurrent["ICD10"].ToString();
                        p_objResultArr[rowIndex].m_strSalveTimes = drCurrent["SALVETIMES"].ToString();
                        p_objResultArr[rowIndex].m_strSalveSuccessTimes = drCurrent["SALVESUCCESSTIMES"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }			//返回
            return lngRes;

        }

        #endregion

        #region 获得化疗子表
        /// <summary>
        /// 获得化疗子表
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChemotherapyInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Chemotherapy[] p_objResultArr)
        {
            p_objResultArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetChemotherapyInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                string m_strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       seqid,
       chemotherapydate,
       medicinename,
       period,
       field_cr,
       field_pr,
       field_mr,
       field_s,
       field_p,
       field_na
  from ihmainrecord_chemotherapy
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 1
 order by seqid";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    p_objResultArr = new clsInHospitalMainRecord_Chemotherapy[intRowsCount];
                    DataRow drCurrent = null;
                    for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                    {
                        p_objResultArr[rowIndex] = new clsInHospitalMainRecord_Chemotherapy();
                        drCurrent = dtbResult.Rows[rowIndex];
                        p_objResultArr[rowIndex].m_strInPatientID = drCurrent["INPATIENTID"].ToString();
                        p_objResultArr[rowIndex].m_strInPatientDate = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strOpenDate = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyDate = Convert.ToDateTime(drCurrent["LASTMODIFYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyUserID = drCurrent["LASTMODIFYUSERID"].ToString();
                        p_objResultArr[rowIndex].m_strSeqID = drCurrent["SEQID"].ToString();
                        p_objResultArr[rowIndex].m_strChemotherapyDate = Convert.ToDateTime(drCurrent["CHEMOTHERAPYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strMedicineName = drCurrent["MEDICINENAME"].ToString();
                        p_objResultArr[rowIndex].m_strPeriod = drCurrent["PERIOD"].ToString();
                        p_objResultArr[rowIndex].m_strField_CR = drCurrent["FIELD_CR"].ToString();
                        p_objResultArr[rowIndex].m_strField_PR = drCurrent["FIELD_PR"].ToString();
                        p_objResultArr[rowIndex].m_strField_MR = drCurrent["FIELD_MR"].ToString();
                        p_objResultArr[rowIndex].m_strField_S = drCurrent["FIELD_S"].ToString();
                        p_objResultArr[rowIndex].m_strField_P = drCurrent["FIELD_P"].ToString();
                        p_objResultArr[rowIndex].m_strField_NA = drCurrent["FIELD_NA"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }

        [AutoComplete]
        public long m_lngGetDeletedChemotherapyInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Chemotherapy[] p_objResultArr)
        {
            p_objResultArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetDeletedChemotherapyInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                string m_strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       seqid,
       chemotherapydate,
       medicinename,
       period,
       field_cr,
       field_pr,
       field_mr,
       field_s,
       field_p,
       field_na
  from ihmainrecord_chemotherapy
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 0
   and lastmodifydate in (select max(lastmodifydate) as max_lastmodifydate
                            from ihmainrecord_chemotherapy
                           where inpatientid = ?
                             and inpatientdate = ?
                             and opendate = ?
                             and status = 0)
 order by seqid";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    p_objResultArr = new clsInHospitalMainRecord_Chemotherapy[intRowsCount];
                    DataRow drCurrent = null;
                    for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                    {
                        p_objResultArr[rowIndex] = new clsInHospitalMainRecord_Chemotherapy();
                        drCurrent = dtbResult.Rows[rowIndex];
                        p_objResultArr[rowIndex].m_strInPatientID = drCurrent["INPATIENTID"].ToString();
                        p_objResultArr[rowIndex].m_strInPatientDate = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strOpenDate = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyDate = Convert.ToDateTime(drCurrent["LASTMODIFYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLastModifyUserID = drCurrent["LASTMODIFYUSERID"].ToString();
                        p_objResultArr[rowIndex].m_strDeActivedDate = Convert.ToDateTime(drCurrent["DEACTIVEDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strDeActivedOperatorID = drCurrent["DEACTIVEDOPERATORID"].ToString();
                        p_objResultArr[rowIndex].m_strSeqID = drCurrent["SEQID"].ToString();
                        p_objResultArr[rowIndex].m_strChemotherapyDate = Convert.ToDateTime(drCurrent["CHEMOTHERAPYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strMedicineName = drCurrent["MEDICINENAME"].ToString();
                        p_objResultArr[rowIndex].m_strPeriod = drCurrent["PERIOD"].ToString();
                        p_objResultArr[rowIndex].m_strField_CR = drCurrent["FIELD_CR"].ToString();
                        p_objResultArr[rowIndex].m_strField_PR = drCurrent["FIELD_PR"].ToString();
                        p_objResultArr[rowIndex].m_strField_MR = drCurrent["FIELD_MR"].ToString();
                        p_objResultArr[rowIndex].m_strField_S = drCurrent["FIELD_S"].ToString();
                        p_objResultArr[rowIndex].m_strField_P = drCurrent["FIELD_P"].ToString();
                        p_objResultArr[rowIndex].m_strField_NA = drCurrent["FIELD_NA"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }

        #endregion

        #region 获取诊断内容
        /// <summary>
        /// 获取诊断内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objResultArr">诊断内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDiagnosisInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Diagnosis[] p_objResultArr)
        {
            p_objResultArr = null;

            if (string.IsNullOrEmpty(p_strInPatientID))
            {
                return -1;
            }

            DateTime dtmTemp = DateTime.MinValue;
            if (!DateTime.TryParse(p_strInPatientDate, out dtmTemp) || !DateTime.TryParse(p_strOpenDate, out dtmTemp))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string m_strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       seqid,
       diagnosistype,
       icd10,
       result,
       diagnosis
  from inhospitalmainrecord_diagnosis
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 1
 order by seqid";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    p_objResultArr = new clsInHospitalMainRecord_Diagnosis[intRowsCount];
                    DataRow drCurrent = null;

                    for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                    {
                        drCurrent = dtbResult.Rows[rowIndex];
                        if (drCurrent["SEQID"].ToString().Trim().Length == 1)
                            dtbResult.Rows[rowIndex]["SEQID"] = "00" + drCurrent["SEQID"].ToString();
                        else if (drCurrent["SEQID"].ToString().Trim().Length == 2)
                            dtbResult.Rows[rowIndex]["SEQID"] = "0" + drCurrent["SEQID"].ToString();
                        else
                            dtbResult.Rows[rowIndex]["SEQID"] = drCurrent["SEQID"].ToString();
                    }
                    DataView dvView = dtbResult.DefaultView;
                    dvView.Sort = "SEQID ASC";
                    dtbResult = dvView.ToTable();
                    for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                    {
                        p_objResultArr[rowIndex] = new clsInHospitalMainRecord_Diagnosis();
                        drCurrent = dtbResult.Rows[rowIndex];
                        p_objResultArr[rowIndex].m_strINPATIENTID = drCurrent["INPATIENTID"].ToString();
                        p_objResultArr[rowIndex].m_strINPATIENTDATE = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strOPENDATE = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLASTMODIFYDATE = Convert.ToDateTime(drCurrent["LASTMODIFYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLASTMODIFYUSERID = drCurrent["LASTMODIFYUSERID"].ToString();
                        p_objResultArr[rowIndex].m_strSEQID = drCurrent["SEQID"].ToString();
                        p_objResultArr[rowIndex].m_strDIAGNOSISTYPE = drCurrent["DIAGNOSISTYPE"].ToString();
                        p_objResultArr[rowIndex].m_strICD10 = drCurrent["ICD10"].ToString();
                        p_objResultArr[rowIndex].m_strRESULT = drCurrent["RESULT"].ToString();
                        p_objResultArr[rowIndex].m_strDIAGNOSIS = drCurrent["DIAGNOSIS"].ToString();
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

        [AutoComplete]
        public long m_lngGetDeleteDiagnosisInfo(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Diagnosis[] p_objResultArr)
        {
            p_objResultArr = null;

            if (string.IsNullOrEmpty(p_strInPatientID))
            {
                return -1;
            }

            DateTime dtmTemp = DateTime.MinValue;
            if (!DateTime.TryParse(p_strInPatientDate, out dtmTemp) || !DateTime.TryParse(p_strOpenDate, out dtmTemp))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string m_strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       seqid,
       diagnosistype,
       icd10,
       result,
       diagnosis
  from inhospitalmainrecord_diagnosis
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 0
   and lastmodifydate in (select max(lastmodifydate) as max_lastmodifydate
                            from inhospitalmainrecord_diagnosis
                           where inpatientid = ?
                             and inpatientdate = ?
                             and opendate = ?
                             and status = 0)
 order by seqid";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    p_objResultArr = new clsInHospitalMainRecord_Diagnosis[intRowsCount];
                    DataRow drCurrent = null;
                    for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                    {
                        p_objResultArr[rowIndex] = new clsInHospitalMainRecord_Diagnosis();
                        drCurrent = dtbResult.Rows[rowIndex];
                        p_objResultArr[rowIndex].m_strINPATIENTID = drCurrent["INPATIENTID"].ToString();
                        p_objResultArr[rowIndex].m_strINPATIENTDATE = Convert.ToDateTime(drCurrent["INPATIENTDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strOPENDATE = Convert.ToDateTime(drCurrent["OPENDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLASTMODIFYDATE = Convert.ToDateTime(drCurrent["LASTMODIFYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strLASTMODIFYUSERID = drCurrent["LASTMODIFYUSERID"].ToString();
                        p_objResultArr[rowIndex].m_strDEACTIVEDDATE = Convert.ToDateTime(drCurrent["DEACTIVEDDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_objResultArr[rowIndex].m_strDEACTIVEDOPERATORID = drCurrent["DEACTIVEDOPERATORID"].ToString();
                        p_objResultArr[rowIndex].m_strSEQID = drCurrent["SEQID"].ToString();
                        p_objResultArr[rowIndex].m_strDIAGNOSISTYPE = drCurrent["DIAGNOSISTYPE"].ToString();
                        p_objResultArr[rowIndex].m_strICD10 = drCurrent["ICD10"].ToString();
                        p_objResultArr[rowIndex].m_strRESULT = drCurrent["RESULT"].ToString();
                        p_objResultArr[rowIndex].m_strDIAGNOSIS = drCurrent["DIAGNOSIS"].ToString();
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

        #region 模糊查询麻醉方式
        /// <summary>
        /// 模糊查询麻醉方式
        /// </summary>
        /// <param name="p_strInput"></param>
        /// <param name="p_objAnaesthesiaModeInOperation"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAnaesthesiaModeLikeID(
            string p_strInput, out clsAnaesthesiaModeInOperation[] p_objAnaesthesiaModeInOperation)
        {
            p_objAnaesthesiaModeInOperation = null;
            long lngRes = 0;

            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetAnaesthesiaModeLikeID");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                string strCommand = " select anaesthesiamodeid,anaesthesiamodename from anaesthesiamode where status=0 and anaesthesiamodeid like ? ";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInput + "%";

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    p_objAnaesthesiaModeInOperation = new clsAnaesthesiaModeInOperation[intRowsCount];
                    DataRow drCurrent = null;
                    for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                    {
                        p_objAnaesthesiaModeInOperation[rowIndex] = new clsAnaesthesiaModeInOperation();
                        drCurrent = dtbResult.Rows[rowIndex];
                        p_objAnaesthesiaModeInOperation[rowIndex].strAnaesthesiaModeID = drCurrent[0].ToString();
                        p_objAnaesthesiaModeInOperation[rowIndex].strAnaesthesiaModeName = drCurrent[1].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;


        }

        #endregion

        /// <summary>
        /// 查找该表在该条件下是否有重复的记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCreateDateCount(
            string p_strInPatientID, string p_strInPatientDate, out int p_intRows)
        {
            p_intRows = 0;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetCreateDateCount");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;

                DataTable m_dtbResult = new DataTable();
                string m_strCommand = @"select count(inpatientid) recordcount from inhospitalmainrecord where 
                inpatientid = ? and 
                inpatientdate = ? and 
                status =1";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref m_dtbResult, objDPArr);
                if (lngRes > 0)
                {
                    p_intRows = int.Parse(m_dtbResult.Rows[0][0].ToString());
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }



        /// <summary>
        /// 获得最后修改时间,以及修改人
        /// 如果返回空，表示该记录已被删除
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strLastModifyDate"></param>
        /// <param name="p_strLastModifyUserID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLastModifyDateAndUser(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strLastModifyDate, out string p_strLastModifyUserID)
        {
            p_strLastModifyDate = null;
            p_strLastModifyUserID = null;

            long lngRes = 0;
            PublicMiddleTier.clsPublicMiddleTier objHRPServ = new PublicMiddleTier.clsPublicMiddleTier();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetLastModifyDateAndUser");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;
                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;

                DataTable m_dtbResult = new DataTable();
                lngRes = objHRPServ.m_lngGetLastModifyDateAndUser2( "InHospitalMainRecord", "InHospitalMainRecord_Content", p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_strLastModifyDate, out p_strLastModifyUserID);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strOperatorID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRecord(
            string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, string p_strOperatorID)
        {
            long lngRes = 0;
            PublicMiddleTier.clsPublicMiddleTier objHRPServ = new PublicMiddleTier.clsPublicMiddleTier();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngDeleteRecord");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate))
                    return -1;
                lngRes = objHRPServ.m_lngDeleteRecord2(  "InHospitalMainRecord", p_strInPatientID.Trim(), p_strInPatientDate, p_strOpenDate, p_strOperatorID.Trim());
                //if (lngRes > 0)
                //{

                //    lngRes = m_lngDeleteSubRecord("IHMainRecord_OtherDiagnosis", p_strInPatientID, dtmInpatientDate, dtmOpenDate, p_strOperatorID);

                //    lngRes = m_lngDeleteSubRecord("InHospitalMainRecord_Operation", p_strInPatientID, dtmInpatientDate, dtmOpenDate, p_strOperatorID);

                //    lngRes = m_lngDeleteSubRecord("InHospitalMainRecord_Baby", p_strInPatientID, dtmInpatientDate, dtmOpenDate, p_strOperatorID);

                //    lngRes = m_lngDeleteSubRecord("IHMainRecord_Chemotherapy", p_strInPatientID, dtmInpatientDate, dtmOpenDate, p_strOperatorID);

                //}

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }

        /// <summary>
        /// 获得最后删除时间,以及删除人
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strDeactivedDate"></param>
        /// <param name="p_strDeactivedUserID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeactivedDateAndUser(
            string p_strInPatientID, string p_strInPatientDate, out string p_strDeactivedDate, out string p_strDeactivedUserID)
        {
            p_strDeactivedDate = null;
            p_strDeactivedUserID = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetDeactivedDateAndUser");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;

                DataTable m_dtbResult = new DataTable();
                string strCommand = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strCommand = @"select top 1 deactiveddate,deactivedoperatorid 
                    from inhospitalmainrecord where inpatientid = ? 
                    and inpatientdate = ? 
                    and status =0  
                    order by deactiveddate desc";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strCommand = @"select deactiveddate, deactivedoperatorid
  from (select deactiveddate, deactivedoperatorid
          from inhospitalmainrecord
         where inpatientid = ?
           and inpatientdate = ?
           and status = 0
         order by deactiveddate desc)
 where rownum = 1";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strCommand = @" select deactiveddate,deactivedoperatorid 
                    from inhospitalmainrecord where inpatientid = ? 
                    and inpatientdate =? 
                    and status =0  
                    order by deactiveddate desc fetch first 1 row only ";
                }

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref m_dtbResult, objDPArr);
                if (lngRes > 0)
                {
                    if (m_dtbResult.Rows.Count <= 0)
                    {
                        p_strDeactivedDate = null;
                        p_strDeactivedUserID = null;
                    }
                    else
                    {
                        if (m_dtbResult.Rows[0][0] == null || m_dtbResult.Rows[0][0].ToString() == "")
                        {
                            p_strDeactivedDate = null;
                            p_strDeactivedUserID = null;
                        }
                        else
                        {
                            p_strDeactivedDate = DateTime.Parse(m_dtbResult.Rows[0][0].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            p_strDeactivedUserID = m_dtbResult.Rows[0][1].ToString();
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
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }

        /// <summary>
        /// 获取病历摘要内容
        /// </summary>
        /// <param name="strTypeID"></param>
        /// <param name="strInPatientID"></param>
        /// <param name="strInPatientDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllergicValue(string strTypeID, string strInPatientID, string strInPatientDate, bool p_blnIsInactive, out clsCaseHistorySummary p_objRecordContent)
        {
            long res = -1;
            string strSQLComm = "";
            p_objRecordContent = new clsCaseHistorySummary();
            DataTable dtbValue = new DataTable();
            strSQLComm = @"select a.itemcontent
  from inpatmedrec_item a, inpatmedrec b
 where a.typeid = ?
   and a.inpatientid = ?
   and a.inpatientdate = ?
   and a.typeid = b.typeid
   and a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
   and b.status = ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = strTypeID;
                objDPArr[1].Value = strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(strInPatientDate);
                objDPArr[3].Value = (p_blnIsInactive ? 1 : 0);

                res = objHRPServ.lngGetDataTableWithParameters(strSQLComm, ref dtbValue, objDPArr);

                if (res > 0 && dtbValue.Rows.Count > 0)
                {
                    //设置结果
                    p_objRecordContent.m_strCaseHistorySummary = dtbValue.Rows[0]["itemcontent"].ToString();
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return res;
        }
        /// <summary>
        /// 获取联系人关系和职业
        /// </summary>
        /// <param name="p_strParentID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAidDict(string p_strParentID, ref DataTable dtResult)
        {
            if (p_strParentID == null)
                p_strParentID = "";
            string strSQL = @"select t.dictname_vchr, t.dictid_chr, t.dictkind_chr, t.dictdefinecode_vchr
  from t_aid_dict t
 where t.dictkind_chr = ?
   and t.dictid_chr <> 0";
            long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strParentID;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        #region 出生地名处理
        /// <summary>
        /// 根据父类ID获取出生地地名
        /// </summary>
        /// <param name="p_strParentID">父类ID</param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDistrict(string p_strParentID, ref DataTable dtResult)
        {
            if (p_strParentID == null)
                p_strParentID = "";
            string strSQL = @"select name_chr,
       type_int,
       parentid_int,
       pycode_chr,
       wbcode_chr,
       shortno_chr,
       id_int,
       sort_int
  from t_aid_district
 where parentid_int = ?
 order by sort_int";
            long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strParentID;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        /// <summary>
        /// 添加出生地地名
        /// </summary>
        /// <param name="p_strDistrict">地名</param>
        /// <param name="p_strParentID">父类ID</param>
        /// <param name="p_strType">类型1.省,2.市,3.县</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddDistrict(string p_strDistrict, string p_strParentID, string p_strType)
        {
            if (p_strDistrict == null || p_strDistrict.Trim() == "" || p_strType == null || p_strType.Trim() == "")
                return -1;
            if (p_strParentID == null)
                p_strParentID = "";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            string strSQLCheck = @"select name_chr,
       type_int,
       parentid_int,
       pycode_chr,
       wbcode_chr,
       shortno_chr,
       id_int,
       sort_int from t_aid_district where name_chr=? and type_int=?";
            DataTable dtCheck = new DataTable();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strDistrict;
            objDPArr[1].Value = p_strType;

            long res = objHRPServ.lngGetDataTableWithParameters(strSQLCheck, ref dtCheck, objDPArr);
            if (dtCheck != null && dtCheck.Rows.Count > 0)
                return -31;

            long lngRes = -1;


            try
            {
                int intMaxID = 0;
                int intMaxSort = 0;
                string strSQLGetMaxID = @"select max(id_int) as id_int from t_aid_district";
                string strSQLGetMaxSort = @"select max(sort_int) as sort_int from t_aid_district where parentid_int=?";

                DataTable dtValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQLGetMaxID, ref dtValue);
                if (dtValue != null && dtValue.Rows.Count == 1)
                    intMaxID = int.Parse(dtValue.Rows[0]["ID_INT"] is DBNull ? "0" : dtValue.Rows[0]["ID_INT"].ToString());

                DataTable dtValue2 = new DataTable();
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strParentID;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQLGetMaxSort, ref dtValue2, objDPArr);
                if (dtValue2 != null && dtValue2.Rows.Count == 1)
                    intMaxSort = int.Parse(dtValue2.Rows[0]["SORT_INT"] is DBNull ? "0" : dtValue2.Rows[0]["SORT_INT"].ToString());

                string strSQLAddDis = @"insert into t_aid_district (name_chr,type_int,parentid_int,id_int,sort_int)
									values(?,?,?,?,?)";
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_strDistrict;
                objDPArr[1].Value = p_strType;
                objDPArr[2].Value = p_strParentID;
                objDPArr[3].Value = intMaxID + 1;
                objDPArr[4].Value = intMaxSort + 1;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQLAddDis, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        /// <summary>
        /// 修改出生地地名
        /// </summary>
        /// <param name="p_strDistrict">地名</param>
        /// <param name="p_strID">流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyDistrict(string p_strDistrict, string p_strID)
        {
            if (p_strDistrict == null || p_strDistrict.Trim() == "" || p_strID == null || p_strID.Trim() == "")
                return -1;
            long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQLModifyDis = @"update t_aid_district set name_chr=? where id_int=?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDistrict;
                objDPArr[1].Value = p_strID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQLModifyDis, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }


            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        /// <summary>
        /// 根据地名获取ID
        /// </summary>
        /// <param name="p_strDistrict">地名</param>
        /// <param name="p_strType">地名类型</param>
        /// <param name="p_strParentID">父类ID</param>
        /// <param name="p_strDisID">地名ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDisIDByName(string p_strDistrict, string p_strType, string p_strParentID, out string p_strDisID)
        {
            p_strDisID = "";
            if (p_strDistrict == null || p_strDistrict.Trim() == "" || p_strType == null || p_strType.Trim() == "")
                return -1;
            long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQLGetID = @"select id_int from t_aid_district where name_chr=? and type_int=? and parentid_int=?";
                DataTable dtValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strDistrict;
                objDPArr[1].Value = p_strType;
                objDPArr[2].Value = p_strParentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQLGetID, ref dtValue, objDPArr);
                if (lngRes > 0 && dtValue.Rows.Count == 1)
                    p_strDisID = dtValue.Rows[0]["ID_INT"].ToString();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }
        #endregion
        /// <summary>
        /// 获取转科情况及入院、出院科室
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_objDeptInstance"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInHospitalMainTransDeptInstance(string p_strRegisterID, out clsInHospitalMainTransDeptInstance p_objDeptInstance)
        {
            long lngRes = 0;
            p_objDeptInstance = new clsInHospitalMainTransDeptInstance();
            if (p_strRegisterID == null || p_strRegisterID.Trim() == "")
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID.Trim();

                //                string strSQL = @"select distinct tr.SOURCEDEPTID_CHR," 
                //                    + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETDEPTNAMEBYID") + @"(tr.sourcedeptid_chr) SourceDeptName,tr.TARGETDEPTID_CHR," 
                //                    + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETDEPTNAMEBYID") + @"(tr.targetdeptid_chr) TargetDeptName,tr.sourceareaid_chr," 
                //                    + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETDEPTNAMEBYID") + @"(tr.sourceareaid_chr)  sourceareaName,tr.targetareaid_chr," 
                //                    + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETDEPTNAMEBYID") + @"(tr.targetareaid_chr)  targetareaName,tr.sourcebedid_chr,"
                //                    + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETBEDNAMEBYID") + @"(tr.sourcebedid_chr) SourceBedName,tr.targetbedid_chr,"
                //                    + clsDatabaseSQLConvert.s_strGetFuntionSQL("F_GETBEDNAMEBYID") + @"(tr.targetbedid_chr) TargetBedName,tr.TYPE_INT,
                //       tr.MODIFY_DAT  TransDeptDate,
                //       le.MODIFY_DAT  OutDate
                //  from T_OPR_BIH_REGISTER re
                // inner join T_OPR_BIH_TRANSFER tr on re.registerid_chr = tr.registerid_chr
                //                              and tr.TYPE_INT <> 1
                //  left join T_OPR_BIH_LEAVE le on re.registerid_chr = le.registerid_chr
                //                              and le.status_int = 1
                // where re.registerid_chr = ? and re.STATUS_INT = 1
                // order by tr.MODIFY_DAT";
                string strSQL = @"select distinct tr.sourcedeptid_chr,
                                    f_getdeptnamebyid(tr.sourcedeptid_chr) sourcedeptname,
                                    tr.targetdeptid_chr,
                                    f_getdeptnamebyid(tr.targetdeptid_chr) targetdeptname,
                                    tr.sourceareaid_chr,
                                    f_getdeptnamebyid(tr.sourceareaid_chr) sourceareaname,
                                    tr.targetareaid_chr,
                                    f_getdeptnamebyid(tr.targetareaid_chr) targetareaname,
                                    tr.sourcebedid_chr,
                                    (select code_chr
                                       from t_bse_bed
                                      where bedid_chr = tr.sourcebedid_chr) sourcebedname,
                                    tr.targetbedid_chr,
                                    (select code_chr
                                       from t_bse_bed
                                      where bedid_chr = tr.targetbedid_chr) targetbedname,
                                    tr.type_int,
                                    tr.modify_dat transdeptdate,
                                    le.outhospital_dat outdate
                          from t_opr_bih_register re
                         inner join t_opr_bih_transfer tr on re.registerid_chr = tr.registerid_chr
                                                         and tr.type_int <> 1
                          left join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr
                                                      and le.status_int = 1
                         where re.registerid_chr = ?
                           and re.status_int = 1
                         order by tr.modify_dat";

                DataTable m_dtbResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref m_dtbResult, objDPArr);

                if (lngRes > 0 && m_dtbResult.Rows.Count > 0)
                {
                    int intType = 0;
                    DateTime dtmTransDeptDate = DateTime.MinValue;
                    //科室
                    string strSourceDept = "";
                    string strTargetDept = "";
                    ArrayList arrSourceDeptID = new ArrayList();
                    ArrayList arrTargetDeptID = new ArrayList();
                    ArrayList arrSourceDeptName = new ArrayList();
                    ArrayList arrTargetDeptName = new ArrayList();
                    ArrayList arrTransDeptDate = new ArrayList();
                    //病区
                    string strSourceArea = "";
                    string strTargetArea = "";
                    ArrayList arrSourceAreaID = new ArrayList();
                    ArrayList arrTargetAreaID = new ArrayList();
                    ArrayList arrSourceAreaName = new ArrayList();
                    ArrayList arrTargetAreaName = new ArrayList();
                    ArrayList arrTransAreaDate = new ArrayList();
                    //病床
                    string strSourceBed = "";
                    string strTargetBed = "";
                    ArrayList arrSourceBedID = new ArrayList();
                    ArrayList arrTargetBedID = new ArrayList();
                    ArrayList arrSourceBedName = new ArrayList();
                    ArrayList arrTargetBedName = new ArrayList();
                    ArrayList arrTransBedDate = new ArrayList();
                    DataRow objRow = null;
                    for (int i = 0; i < m_dtbResult.Rows.Count; i++)
                    {
                        objRow = m_dtbResult.Rows[i];
                        if (objRow["TYPE_INT"] == DBNull.Value)
                            continue;
                        int.TryParse(objRow["TYPE_INT"].ToString(), out intType);
                        dtmTransDeptDate = new DateTime(1900, 1, 1);
                        if (intType == 3)//转区
                        {
                            DateTime.TryParse(objRow["TransDeptDate"].ToString(), out dtmTransDeptDate);
                            //科室
                            strSourceDept = objRow["SOURCEDEPTID_CHR"].ToString();
                            strTargetDept = objRow["TARGETDEPTID_CHR"].ToString();
                            if (!string.IsNullOrEmpty(strSourceDept) && !string.IsNullOrEmpty(strTargetDept))
                            {
                                if (strSourceDept.Trim() != strTargetDept.Trim())
                                {
                                    arrSourceDeptID.Add(strSourceDept);
                                    arrTargetDeptID.Add(strTargetDept);
                                    arrSourceDeptName.Add(objRow["SourceDeptName"].ToString());
                                    arrTargetDeptName.Add(objRow["TargetDeptName"].ToString());
                                    arrTransDeptDate.Add(dtmTransDeptDate.ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                            }
                            //病区
                            strSourceArea = objRow["sourceareaid_chr"].ToString();
                            strTargetArea = objRow["targetareaid_chr"].ToString();
                            if (!string.IsNullOrEmpty(strSourceArea) && !string.IsNullOrEmpty(strTargetArea))
                            {
                                if (strSourceArea.Trim() != strTargetArea.Trim())
                                {
                                    arrSourceAreaID.Add(strSourceArea);
                                    arrTargetAreaID.Add(strTargetArea);
                                    arrSourceAreaName.Add(objRow["sourceareaName"].ToString());
                                    arrTargetAreaName.Add(objRow["targetareaName"].ToString());
                                    arrTransAreaDate.Add(dtmTransDeptDate.ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                            }
                            //病床
                            strSourceBed = objRow["sourcebedid_chr"].ToString();
                            strTargetBed = objRow["targetbedid_chr"].ToString();
                            if (!string.IsNullOrEmpty(strSourceBed) && !string.IsNullOrEmpty(strTargetBed))
                            {
                                arrSourceBedID.Add(strSourceBed);
                                arrTargetBedID.Add(strTargetBed);
                                arrSourceBedName.Add(objRow["sourcebedname"].ToString());
                                arrTargetBedName.Add(objRow["targetbedname"].ToString());
                                arrTransBedDate.Add(dtmTransDeptDate.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        else if (intType == 5)//入院
                        {
                            p_objDeptInstance.m_strInPatientDeptID = objRow["TARGETDEPTID_CHR"].ToString();
                            p_objDeptInstance.m_strInPatientDeptName = objRow["TargetDeptName"].ToString();
                            p_objDeptInstance.m_strInPatientAreaID = objRow["targetareaid_chr"].ToString();
                            p_objDeptInstance.m_strInPatientAreaName = objRow["targetareaName"].ToString();
                            p_objDeptInstance.m_strInPatientBedID = objRow["targetBedid_chr"].ToString();
                            p_objDeptInstance.m_strInPatientBedName = objRow["targetBedName"].ToString();
                        }
                        else if (intType == 6 || intType == 7)//出院、预出院
                        {
                            p_objDeptInstance.m_strOutPatientDeptID = objRow["SOURCEDEPTID_CHR"].ToString();
                            p_objDeptInstance.m_strOutPatientDeptName = objRow["SourceDeptName"].ToString();

                            p_objDeptInstance.m_strOutPatientAreaID = objRow["sourceareaid_chr"].ToString();
                            p_objDeptInstance.m_strOutPatientAreaName = objRow["sourceareaName"].ToString();

                            p_objDeptInstance.m_strOutPatientBedID = objRow["sourcebedid_chr"].ToString();
                            p_objDeptInstance.m_strOutPatientBedName = objRow["sourcebedName"].ToString();

                            DateTime dtmOut = new DateTime(1900, 1, 1);
                            DateTime.TryParse(objRow["OutDate"].ToString(), out dtmOut);
                            p_objDeptInstance.m_demOutPatientDate = dtmOut;
                        }
                    }
                    if (arrSourceDeptID.Count != 0 && arrTargetDeptID.Count != 0)
                    {
                        p_objDeptInstance.m_strTransSourceDeptIDArr = (string[])arrSourceDeptID.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransTargetDeptIDArr = (string[])arrTargetDeptID.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransSourceDeptNameArr = (string[])arrSourceDeptName.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransTargetDeptNameArr = (string[])arrTargetDeptName.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransDeptDateArr = (string[])arrTransDeptDate.ToArray(typeof(string));
                    }
                    if (arrSourceAreaID.Count != 0 && arrTargetAreaID.Count != 0)
                    {
                        p_objDeptInstance.m_strTransSourceAreaIDArr = (string[])arrSourceAreaID.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransTargetAreaIDArr = (string[])arrTargetAreaID.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransSourceAreaNameArr = (string[])arrSourceAreaName.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransTargetAreaNameArr = (string[])arrTargetAreaName.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransAreaDateArr = (string[])arrTransAreaDate.ToArray(typeof(string));
                    }
                    if (arrSourceBedID.Count != 0 && arrTargetBedID.Count != 0)
                    {
                        p_objDeptInstance.m_strTransSourceBedIDArr = (string[])arrSourceBedID.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransTargetBedIDArr = (string[])arrTargetBedID.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransSourceBedNameArr = (string[])arrSourceBedName.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransTargetBedNameArr = (string[])arrTargetBedName.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransBedDateArr = (string[])arrTransBedDate.ToArray(typeof(string));
                    }
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }            //返回
            return lngRes;
        }

        #region 修改病人信息
        /// <summary>
        /// 修改病人基本表信息
        /// </summary>
        /// <param name="p_strPatientID">病人ID</param>
        /// <param name="p_objPeopleInfo">病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateToBsePatient(string p_strPatientID, clsPeopleInfo p_objPeopleInfo)
        {
            if (string.IsNullOrEmpty(p_strPatientID) || p_objPeopleInfo == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update t_bse_patient t
   set t.idcard_chr = ?,
       t.married_chr = ?,
       t.birth_dat = ?,
       t.nationality_vchr = ?,
       t.nativeplace_vchr = ?,
       t.occupation_vchr = ?,
       t.RACE_VCHR = ?,
       t.homephone_vchr = ?,
       t.EMPLOYER_VCHR = ?,
       t.officeaddress_vchr = ?,
       t.officepc_vchr = ?,
       t.homeaddress_vchr = ?,
       t.homepc_chr = ?,
       t.contactpersonfirstname_vchr = ?,
       t.contactpersonlastname_vchr = ?,
       t.patientrelation_vchr = ?,
       t.contactpersonaddress_vchr = ?,
       t.contactpersonphone_vchr = ?,
       t.contactpersonpc_chr = ?,
       t.birthplace_vchr = ?,
       t.officephone_vchr = ?
 where t.patientid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(22, out objDPArr);
                objDPArr[0].Value = p_objPeopleInfo.m_StrIDCard;
                objDPArr[1].Value = p_objPeopleInfo.m_StrMarried;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objPeopleInfo.m_DtmBirth;
                objDPArr[3].Value = p_objPeopleInfo.m_StrNationality;
                objDPArr[4].Value = p_objPeopleInfo.m_StrNativePlace;
                objDPArr[5].Value = p_objPeopleInfo.m_StrOccupation;
                objDPArr[6].Value = p_objPeopleInfo.m_StrNation;
                objDPArr[7].Value = p_objPeopleInfo.m_StrHomePhone;
                objDPArr[8].Value = p_objPeopleInfo.m_StrOffice_name;
                objDPArr[9].Value = p_objPeopleInfo.m_StrOfficeAddress;
                objDPArr[10].Value = p_objPeopleInfo.m_StrOfficePC;
                objDPArr[11].Value = p_objPeopleInfo.m_StrHomeAddress;
                objDPArr[12].Value = p_objPeopleInfo.m_StrHomePC;
                objDPArr[13].Value = p_objPeopleInfo.m_StrLinkManLastName;
                objDPArr[14].Value = p_objPeopleInfo.m_StrLinkManLastName;
                objDPArr[15].Value = p_objPeopleInfo.m_StrPatientRelation;
                objDPArr[16].Value = p_objPeopleInfo.m_StrLinkManAddress;
                objDPArr[17].Value = p_objPeopleInfo.m_StrLinkManPhone;
                objDPArr[18].Value = p_objPeopleInfo.m_StrLinkManPC;
                objDPArr[19].Value = p_objPeopleInfo.m_StrBirthPlace;
                objDPArr[20].Value = p_objPeopleInfo.m_StrOfficePhone;//su.liang
                objDPArr[21].Value = p_strPatientID;

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
        /// 修改病人住院明细信息
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_objPeopleInfo">病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateToRegisterDetail(string p_strRegisterID, clsPeopleInfo p_objPeopleInfo)
        {
            if (string.IsNullOrEmpty(p_strRegisterID) || p_objPeopleInfo == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update t_opr_bih_registerdetail t
   set t.idcard_chr = ?,
       t.married_chr = ?,
       t.birth_dat = ?,
       t.nationality_vchr = ?,
       t.nativeplace_vchr = ?,
       t.occupation_vchr = ?,
       t.race_vchr = ?,
       t.homephone_vchr = ?,
       t.employer_vchr = ?,
       t.officeaddress_vchr = ?,
       t.officepc_vchr = ?,
       t.homeaddress_vchr = ?,
       t.homepc_chr = ?,
       t.contactpersonfirstname_vchr = ?,
       t.contactpersonlastname_vchr = ?,
       t.patientrelation_vchr = ?,
       t.contactpersonaddress_vchr = ?,
       t.contactpersonphone_vchr = ?,
       t.contactpersonpc_chr = ?,
       t.birthplace_vchr = ?,
       t.residenceplace_vchr = ?,
       t.officephone_vchr = ?
 where t.registerid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(23, out objDPArr);
                objDPArr[0].Value = p_objPeopleInfo.m_StrIDCard;
                objDPArr[1].Value = p_objPeopleInfo.m_StrMarried;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objPeopleInfo.m_DtmBirth;
                objDPArr[3].Value = p_objPeopleInfo.m_StrNationality;
                objDPArr[4].Value = p_objPeopleInfo.m_StrNativePlace;
                objDPArr[5].Value = p_objPeopleInfo.m_StrOccupation;
                objDPArr[6].Value = p_objPeopleInfo.m_StrNation;
                objDPArr[7].Value = p_objPeopleInfo.m_StrHomePhone;
                objDPArr[8].Value = p_objPeopleInfo.m_StrOffice_name;
                objDPArr[9].Value = p_objPeopleInfo.m_StrOfficeAddress;
                objDPArr[10].Value = p_objPeopleInfo.m_StrOfficePC;
                objDPArr[11].Value = p_objPeopleInfo.m_StrHomeAddress;
                objDPArr[12].Value = p_objPeopleInfo.m_StrHomePC;
                objDPArr[13].Value = p_objPeopleInfo.m_StrLinkManLastName;
                objDPArr[14].Value = p_objPeopleInfo.m_StrLinkManLastName;
                objDPArr[15].Value = p_objPeopleInfo.m_StrPatientRelation;
                objDPArr[16].Value = p_objPeopleInfo.m_StrLinkManAddress;
                objDPArr[17].Value = p_objPeopleInfo.m_StrLinkManPhone;
                objDPArr[18].Value = p_objPeopleInfo.m_StrLinkManPC;
                objDPArr[19].Value = p_objPeopleInfo.m_StrBirthPlace;
                objDPArr[20].Value = p_objPeopleInfo.m_StrHomeplace;
                objDPArr[21].Value = p_objPeopleInfo.m_StrOfficePhone;
                objDPArr[22].Value = p_strRegisterID;


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

        #region 病案提交及同步
        /// <summary>
        /// 设置病案状态为已提交
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_dtmInPatientDate">入院日期</param>
        /// <param name="p_dtmOpenDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitRecord(string p_strInPatientID, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            if (p_strInPatientID == null)
                return -1;
            long lngRes = -1;

            try
            {
                string strSQL = @"update inhospitalmainrecord set ishandin = 1 
where inpatientid=? and inpatientdate=? and opendate=?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].Value = p_dtmInPatientDate;
                objDPArr[2].Value = p_dtmOpenDate;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #region 获取住院次数
        /// <summary>
        /// 获取住院次数
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_dtmInDate">入院日期</param>
        /// <param name="p_intInTimes">住院次数</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInTimes(string p_strInPatientID, DateTime p_dtmInDate, out int p_intInTimes)
        {
            long lngRes = -1;
            p_intInTimes = 1;
            if (p_strInPatientID == null || p_strInPatientID == "")
            {
                return -1;
            }
            try
            {
                string strSQL = @"select inpatientcount_int from t_opr_bih_register where inpatientid_chr = ? and inareadate_dat = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].Value = p_dtmInDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    if (dtbValue.Rows[0]["INPATIENTCOUNT_INT"] != DBNull.Value)
                    {
                        p_intInTimes = Convert.ToInt32(dtbValue.Rows[0]["INPATIENTCOUNT_INT"]);
                    }
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

        #region 提交记录到ba1
        /// <summary>
        /// 提交记录到ba1
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="intInTimes">入院次数</param>
        /// <param name="p_objPeoInfo">病人基本信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToBA1(string p_strInPatientID, int intInTimes, clsPeopleInfo p_objPeoInfo)
        {
            if (string.IsNullOrEmpty(p_strInPatientID) || p_objPeoInfo == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strCheckSQL = @"select * from ba1 where prn = ? and name = ?";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = p_objPeoInfo.m_StrLastName;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCheckSQL, ref dtbResult, objDPArr);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    return 0;
                }

                string strCommitSQL = @"insert into ba1
  (prn, name, sex, birthday, birthpl, idcard, native, nation)
values
  (?, ?, ?, ?, ?, ?, ?, ?)";

                objHRPServ.CreateDatabaseParameter(8, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = p_objPeoInfo.m_StrLastName;
                objDPArr[2].Value = p_objPeoInfo.m_StrSex;
                objDPArr[3].Value = p_objPeoInfo.m_DtmBirth.ToString("yyyy/MM/dd");
                objDPArr[4].Value = p_objPeoInfo.m_StrNativePlace;
                objDPArr[5].Value = p_objPeoInfo.m_StrIDCard;
                objDPArr[6].Value = p_objPeoInfo.m_StrNationality;
                objDPArr[7].Value = p_objPeoInfo.m_StrNation;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
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

        #region 提交记录到ba2
        /// <summary>
        /// 提交记录到ba2
        /// </summary>
        /// <param name="p_objCollection">住院病案首页内容</param>
        /// <param name="p_intInTimes">住院次数</param>
        /// <param name="p_objTransferDept">转科信息</param>
        /// <param name="p_objPeoInfo">病人基本信息</param>
        /// <param name="p_strHISInDate">HIS住院号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToBA2(clsInHospitalMainRecord_Collection p_objCollection, int p_intInTimes, clsInHospitalMainTransDeptInstance p_objTransferDept, clsPeopleInfo p_objPeoInfo, string p_strHISInDate)
        {
            long lngRes = -1;
            try
            {
                if (p_objCollection == null || p_objCollection.m_objMain == null || p_objCollection.m_objContent == null || p_objPeoInfo == null || p_objTransferDept == null)
                {
                    return -1;
                }


                string strMarrid = m_strMarridCode(p_objPeoInfo.m_StrMarried);

                string strCheckSQL = @"select * from ba2 where prn = ? and times = ?";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                objDPArr[1].Value = p_intInTimes;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCheckSQL, ref dtbValue, objDPArr);

                if (lngRes < 0)
                    return -1;

                string strCommitSQL = "";
                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    return 0;
                }
                else
                {
                    strCommitSQL = @"insert into ba2 (mzzd,ryzd,qzdate,ifss,yngr,gmyw,blood,qjtimes,suctimes,sz,szqx,
body,sample,quality,zrdoct,zzdoct,zydoct,sxdoct,bmy,mzacco, ryacco,opacco,qjbr,qjsuc,sxfy,fyk,prn,times,name,fb,age,job,
statu,dwname,dwaddr,dwtele,dwpost,hkaddr,hkpost,lxname,relate,lxaddr,lxtele,rydate,rytime,ryinfo,source,rynum,rydept,
zknum,zkdept,cynum,cydept,cydate,cytime,days,sum1,cwf,xyf,zyf,zcyf,zchf,jcf,zlf,fsf,ssf,hyf,sxf,syf,jsf,qtf,workdate,
mzdoct) 
values(?,?,?,?,?,?,?,?,?,?,
	   ?,?,?,?,?,?,?,?,?,?,
	   ?,?,?,?,?,?,?,?,?,?,
	   ?,?,?,?,?,?,?,?,?,?,
	   ?,?,?,?,?,?,?,?,?,?,
	   ?,?,?,?,?,?,?,?,?,?,
	   ?,?,?,?,?,?,?,?,?,?,
	   ?,?,?)";
                }

                string strRYZD = "";
                if (p_objCollection.m_objDiagnosisArr != null && p_objCollection.m_objDiagnosisArr.Length > 0)
                {
                    for (int i = 0; i < p_objCollection.m_objDiagnosisArr.Length; i++)
                    {
                        if (p_objCollection.m_objDiagnosisArr[i].m_strDIAGNOSISTYPE == "1")
                        {
                            strRYZD = p_objCollection.m_objDiagnosisArr[i].m_strICD10;
                            break;
                        }
                    }
                }
                DateTime dtmTemp = DateTime.MinValue;
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(73, out objDPArr);
                objDPArr[0].Value = p_objCollection.m_objMain.m_strMZICD10;
                objDPArr[1].Value = strRYZD;
                objDPArr[2].Value = Convert.ToDateTime(p_objCollection.m_objContent.m_strConfirmDiagnosisDate).ToString("yyyy/MM/dd");
                if (p_objCollection.m_objOperationArr != null && p_objCollection.m_objOperationArr.Length > 0)
                {
                    objDPArr[3].Value = "y";
                }
                else
                {
                    objDPArr[3].Value = "n";
                }
                if (p_objCollection.m_objDiagnosisArr != null && p_objCollection.m_objDiagnosisArr.Length > 0)
                {
                    int intInfect = 0;
                    for (int i = 0; i < p_objCollection.m_objDiagnosisArr.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(p_objCollection.m_objDiagnosisArr[i].m_strICD10) && p_objCollection.m_objDiagnosisArr[i].m_strDIAGNOSISTYPE == "2")
                        {
                            intInfect++;
                        }
                    }
                    objDPArr[4].Value = intInfect;
                }
                else
                {
                    objDPArr[4].Value = 0;
                }
                objDPArr[5].Value = p_objCollection.m_objContent.m_strSensitive;
                objDPArr[6].Value = p_objCollection.m_objContent.m_strBloodType;
                if (p_objCollection.m_objContent.m_strSalveTimes == "" || p_objCollection.m_objContent.m_strSalveTimes == null)
                    objDPArr[7].Value = null;
                else
                    objDPArr[7].Value = p_objCollection.m_objContent.m_strSalveTimes;
                if (p_objCollection.m_objContent.m_strSalveSuccess == "" || p_objCollection.m_objContent.m_strSalveSuccess == null)
                    objDPArr[8].Value = null;
                else
                    objDPArr[8].Value = p_objCollection.m_objContent.m_strSalveSuccess;
                if (p_objCollection.m_objContent.m_strFollow == "1" || p_objCollection.m_objContent.m_strFollow == "True")
                    objDPArr[9].Value = "1";
                else
                    objDPArr[9].Value = "2";
                string strFollowTimes = "";
                if (p_objCollection.m_objContent.m_strFollow_Week != null && p_objCollection.m_objContent.m_strFollow_Week != "")
                    strFollowTimes += p_objCollection.m_objContent.m_strFollow_Week + "周";
                if (p_objCollection.m_objContent.m_strFollow_Month != null && p_objCollection.m_objContent.m_strFollow_Month != "")
                    strFollowTimes += p_objCollection.m_objContent.m_strFollow_Month + "月";
                if (p_objCollection.m_objContent.m_strFollow_Year != null && p_objCollection.m_objContent.m_strFollow_Year != "")
                    strFollowTimes += p_objCollection.m_objContent.m_strFollow_Year + "年";
                objDPArr[10].Value = strFollowTimes;
                if (p_objCollection.m_objContent.m_strCorpseCheck == "1" || p_objCollection.m_objContent.m_strCorpseCheck == "True")
                {
                    objDPArr[11].Value = "1";
                }
                else
                {
                    objDPArr[11].Value = "2";
                }

                if (p_objCollection.m_objContent.m_strModelCase == "1")
                {
                    objDPArr[12].Value = "1";
                }
                else
                {
                    objDPArr[12].Value = "2";
                }
                objDPArr[13].Value = p_objCollection.m_objContent.m_strQuality;
                objDPArr[14].Value = p_objCollection.m_objContent.m_strSubDirectorDtName;
                objDPArr[15].Value = p_objCollection.m_objContent.m_strDtName;
                objDPArr[16].Value = p_objCollection.m_objContent.m_strInHospitalDtName;
                objDPArr[17].Value = p_objCollection.m_objContent.m_strInternName;
                objDPArr[18].Value = p_objCollection.m_objContent.m_strCoderName;
                objDPArr[19].Value = p_objCollection.m_objContent.m_strAccordWithOutHospital;
                objDPArr[20].Value = p_objCollection.m_objContent.m_strAccordInWithOut;
                objDPArr[21].Value = p_objCollection.m_objContent.m_strAccordBeforeOperationWithAfter;
                if (p_objCollection.m_objContent.m_strSalveTimes != null && p_objCollection.m_objContent.m_strSalveTimes != "")
                {
                    objDPArr[22].Value = "y";
                }
                else
                {
                    objDPArr[22].Value = "n";
                }
                if (objDPArr[22].Value.ToString() == "y" && p_objCollection.m_objContent.m_strSalveSuccess != null
                    && p_objCollection.m_objContent.m_strSalveSuccess != "")
                {
                    objDPArr[23].Value = "y";
                }
                if (p_objCollection.m_objContent.m_strBloodTransActoin == "1" || p_objCollection.m_objContent.m_strBloodTransActoin == "True")
                {
                    objDPArr[24].Value = "1";
                }
                else
                {
                    objDPArr[24].Value = "2";
                }
                if (p_objCollection.m_objBabyArr != null && p_objCollection.m_objBabyArr.Length > 0)
                {
                    objDPArr[25].Value = "y";
                }
                else
                {
                    objDPArr[25].Value = "n";
                }
                objDPArr[26].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                objDPArr[27].Value = p_intInTimes;
                objDPArr[28].Value = p_objPeoInfo.m_StrLastName;
                objDPArr[29].Value = DBNull.Value;
                if (!string.IsNullOrEmpty(p_objPeoInfo.m_StrYearOfAge))
                {
                    objDPArr[30].Value = "Y" + p_objPeoInfo.m_StrYearOfAge;
                }
                else if (!string.IsNullOrEmpty(p_objPeoInfo.m_StrMonthOfAge))
                {
                    objDPArr[30].Value = "M" + p_objPeoInfo.m_StrMonthOfAge;
                }
                else if (!string.IsNullOrEmpty(p_objPeoInfo.m_StrDayOfAge))
                {
                    objDPArr[30].Value = "D" + p_objPeoInfo.m_StrDayOfAge;
                }
                else
                {
                    objDPArr[30].Value = DBNull.Value;
                }
                objDPArr[31].Value = DBNull.Value;
                objDPArr[32].Value = strMarrid.Trim();
                objDPArr[33].Value = p_objPeoInfo.m_StrOffice_name;
                objDPArr[34].Value = p_objPeoInfo.m_StrOfficeAddress;
                objDPArr[35].Value = p_objPeoInfo.m_StrOfficePhone;
                objDPArr[36].Value = p_objPeoInfo.m_StrOfficePC;
                objDPArr[37].Value = p_objPeoInfo.m_StrHomeplace;
                objDPArr[38].Value = p_objPeoInfo.m_StrHomePC;
                objDPArr[39].Value = p_objPeoInfo.m_StrLinkManLastName;
                objDPArr[40].Value = p_objPeoInfo.m_StrPatientRelation;
                objDPArr[41].Value = p_objPeoInfo.m_StrLinkManAddress;
                objDPArr[42].Value = p_objPeoInfo.m_StrLinkManPhone;
                if (DateTime.TryParse(p_strHISInDate, out dtmTemp))
                {
                    objDPArr[43].Value = dtmTemp.ToString("yyyy/MM/dd");
                    objDPArr[44].Value = dtmTemp.ToString("HH");
                }
                else
                {
                    objDPArr[43].Value = DBNull.Value;
                    objDPArr[44].Value = DBNull.Value;
                }
                objDPArr[45].Value = p_objCollection.m_objContent.m_strCondictionWhenIn;
                objDPArr[46].Value = DBNull.Value;
                objDPArr[47].Value = p_objTransferDept.m_strInPatientAreaID;
                objDPArr[48].Value = p_objTransferDept.m_strInPatientAreaName;
                if (p_objTransferDept.m_strTransTargetAreaIDArr != null && p_objTransferDept.m_strTransTargetAreaIDArr.Length > 0)
                {
                    objDPArr[49].Value = p_objTransferDept.m_strTransTargetAreaIDArr[0];
                }
                else
                {
                    objDPArr[49].Value = DBNull.Value;
                }
                if (p_objTransferDept.m_strTransTargetAreaNameArr != null && p_objTransferDept.m_strTransTargetAreaNameArr.Length > 0)
                {
                    objDPArr[50].Value = p_objTransferDept.m_strTransTargetAreaNameArr[0];
                }
                else
                {
                    objDPArr[50].Value = DBNull.Value;
                }
                objDPArr[51].Value = p_objTransferDept.m_strOutPatientAreaID;
                objDPArr[52].Value = p_objTransferDept.m_strOutPatientAreaName;
                if (p_objTransferDept.m_demOutPatientDate != DateTime.MinValue && p_objTransferDept.m_demOutPatientDate != new DateTime(1900, 1, 1))
                {
                    objDPArr[53].Value = p_objTransferDept.m_demOutPatientDate.ToString("yyyy/MM/dd");
                    objDPArr[54].Value = p_objTransferDept.m_demOutPatientDate.ToString("HH");
                }
                else
                {
                    objDPArr[53].Value = DBNull.Value;
                    objDPArr[54].Value = DBNull.Value;
                }
                if (DateTime.TryParse(p_strHISInDate, out dtmTemp))
                {
                    System.TimeSpan diff = new TimeSpan(0);
                    if (string.IsNullOrEmpty(p_objCollection.m_objContent.m_strOutPatientDate))
                    {
                        DateTime dtmNow = DateTime.Now;

                        diff = dtmNow.Subtract(dtmTemp);
                    }
                    else
                    {
                        diff = Convert.ToDateTime(p_objCollection.m_objContent.m_strOutPatientDate).Subtract(dtmTemp);
                    }
                    objDPArr[55].Value = ((int)diff.TotalDays + 1).ToString();
                }
                else
                {
                    objDPArr[55].Value = DBNull.Value;
                }
                double dblTemp = 0D;
                if (double.TryParse(p_objCollection.m_objContent.m_strTotalAmt, out dblTemp))
                {
                    objDPArr[56].Value = dblTemp;
                }
                else
                {
                    objDPArr[56].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strBedAmt, out dblTemp))
                {
                    objDPArr[57].Value = dblTemp;
                }
                else
                {
                    objDPArr[57].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strWMAmt, out dblTemp))
                {
                    objDPArr[58].Value = dblTemp;
                }
                else
                {
                    objDPArr[58].Value = DBNull.Value;
                }
                double dblZY = 0D;
                if (double.TryParse(p_objCollection.m_objContent.m_strCMSemiFinishedAmt, out dblTemp))
                {
                    dblZY += dblTemp;
                    objDPArr[60].Value = dblTemp;
                }
                else
                {
                    objDPArr[60].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strCMFinishedAmt, out dblTemp))
                {
                    dblZY += dblTemp;
                    objDPArr[61].Value = dblTemp;
                }
                else
                {
                    objDPArr[61].Value = DBNull.Value;
                }
                objDPArr[59].Value = dblZY;
                if (double.TryParse(p_objCollection.m_objContent.m_strCheckAmt, out dblTemp))
                {
                    objDPArr[62].Value = dblTemp;
                }
                else
                {
                    objDPArr[62].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strTreatmentAmt, out dblTemp))
                {
                    objDPArr[63].Value = dblTemp;
                }
                else
                {
                    objDPArr[63].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strRadiationAmt, out dblTemp))
                {
                    objDPArr[64].Value = dblTemp;
                }
                else
                {
                    objDPArr[64].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strOperationAmt, out dblTemp))
                {
                    objDPArr[65].Value = dblTemp;
                }
                else
                {
                    objDPArr[65].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strAssayAmt, out dblTemp))
                {
                    objDPArr[66].Value = dblTemp;
                }
                else
                {
                    objDPArr[66].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strNurseAmt, out dblTemp))
                {
                    objDPArr[67].Value = dblTemp;
                }
                else
                {
                    objDPArr[67].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strO2Amt, out dblTemp))
                {
                    objDPArr[68].Value = dblTemp;
                }
                else
                {
                    objDPArr[68].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strDeliveryChildAmt, out dblTemp))
                {
                    objDPArr[69].Value = dblTemp;
                }
                else
                {
                    objDPArr[69].Value = DBNull.Value;
                }
                double dblOther = 0D;
                if (double.TryParse(p_objCollection.m_objContent.m_strOtherAmt1, out dblTemp))
                {
                    dblOther += dblTemp;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strOtherAmt2, out dblTemp))
                {
                    dblOther += dblTemp;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strOtherAmt3, out dblTemp))
                {
                    dblOther += dblTemp;
                }
                objDPArr[70].Value = dblOther;
                objDPArr[71].Value = DateTime.Now.ToString("yyyy/MM/dd");
                objDPArr[72].Value = p_objCollection.m_objContent.m_strDoctorName;


                //执行SQL	
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
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
        /// 获取婚姻状况编码
        /// </summary>
        /// <param name="p_strName">婚姻状况名称</param>
        /// <returns></returns>
        [AutoComplete]
        private string m_strMarridCode(string p_strName)
        {
            if (string.IsNullOrEmpty(p_strName))
            {
                return string.Empty;
            }

            string strReturn = string.Empty;
            try
            {
                string strSQL = @"select t.dictid_chr
  from t_aid_dict t
 where t.dictkind_chr = '5' and t.dictname_vchr like ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strName + "%";

                DataTable dtbResult = new DataTable();

                long lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    strReturn = dtbResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return strReturn;
        }
        #endregion

        #region 提交记录到ba3
        /// <summary>
        /// 提交记录到ba3
        /// </summary>
        /// <param name="p_objCollection">住院病案首页内容</param>
        /// <param name="p_intInTimes">住院次数</param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToBA3(clsInHospitalMainRecord_Collection p_objCollection, int p_intInTimes, string p_strPatientName)
        {
            long lngRes = -1;
            try
            {
                if (p_objCollection == null || p_objCollection.m_objMain == null || p_objCollection.m_objContent == null || p_strPatientName == null)
                {
                    return -1;
                }
                string strDelSQL = @"select * from ba3 where prn = ? and times = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                objDPArr[1].Value = p_intInTimes;

                long lngEff = 0;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strDelSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                    return 0;

                string strCommitSQL = @"insert into ba3 (prn,times,zdxh,zljg,icdm10, name)
values(?,?,?,?,?,?)";

                if (p_objCollection.m_objContent.m_strICD_10OfMain != null && p_objCollection.m_objContent.m_strICD_10OfMain != "")
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                    objDPArr[1].Value = p_intInTimes;
                    objDPArr[2].Value = "1";
                    objDPArr[3].Value = p_objCollection.m_objContent.m_strMainConditionSeq;
                    objDPArr[4].Value = p_objCollection.m_objContent.m_strICD_10OfMain;
                    objDPArr[5].Value = p_strPatientName;

                    lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
                }

                if (p_objCollection.m_objDiagnosisArr != null && p_objCollection.m_objDiagnosisArr.Length > 0)
                {
                    for (int i = 0; i < p_objCollection.m_objDiagnosisArr.Length; i++)
                    {
                        if (string.IsNullOrEmpty(p_objCollection.m_objDiagnosisArr[i].m_strICD10))
                        {
                            continue;
                        }

                        if (p_objCollection.m_objDiagnosisArr[i].m_strDIAGNOSISTYPE == "3")
                        {
                            objDPArr = null;
                            objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                            objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                            objDPArr[1].Value = p_intInTimes;
                            objDPArr[2].Value = "2";
                            objDPArr[3].Value = p_objCollection.m_objDiagnosisArr[i].m_strRESULT;
                            objDPArr[4].Value = p_objCollection.m_objDiagnosisArr[i].m_strICD10;
                            objDPArr[5].Value = p_strPatientName;

                            lngEff = 0;
                            lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
                        }
                        else if (p_objCollection.m_objDiagnosisArr[i].m_strDIAGNOSISTYPE == "2")
                        {
                            objDPArr = null;
                            objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                            objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                            objDPArr[1].Value = p_intInTimes;
                            objDPArr[2].Value = "y";
                            objDPArr[3].Value = p_objCollection.m_objDiagnosisArr[i].m_strRESULT;
                            objDPArr[4].Value = p_objCollection.m_objDiagnosisArr[i].m_strICD10;
                            objDPArr[5].Value = p_strPatientName;

                            lngEff = 0;
                            lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
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
            //返回
            return lngRes;
        }
        #endregion

        #region 提交记录到ba4
        /// <summary>
        /// 提交记录到ba4
        /// </summary>
        /// <param name="p_objCollection">住院病案首页内容</param>
        /// <param name="p_intInTimes">住院次数</param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToBA4(clsInHospitalMainRecord_Collection p_objCollection, int p_intInTimes, string p_strPatientName)
        {
            long lngRes = -1;
            try
            {
                if (p_objCollection == null || p_objCollection.m_objMain == null || p_objCollection.m_objContent == null || p_strPatientName == null)
                {
                    return -1;
                }
                string strDelSQL = @"select * from ba4 where prn = ? and times = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                objDPArr[1].Value = p_intInTimes;

                long lngEff = 0;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strDelSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                    return 0;

                string strCommitSQL = @"insert into ba4 (PRN,TIMES,OPTIMES,OPDATE,OPCODE,QIEKOU,YUHE,DOCNAME,MAZUI,OPDOCTI,OPDOCTII,MZDOCT,name)
VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";

                if (p_objCollection.m_objOperationArr != null && p_objCollection.m_objOperationArr.Length > 0)
                {
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < p_objCollection.m_objOperationArr.Length; i++)
                    {
                        objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(13, out objDPArr);
                        objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                        objDPArr[1].Value = p_intInTimes;
                        objDPArr[2].Value = (i + 1).ToString();
                        if (DateTime.TryParse(p_objCollection.m_objOperationArr[i].m_strOpenDate, out dtmTemp))
                        {
                            objDPArr[3].Value = dtmTemp.ToString("yyyy/MM/dd");
                        }
                        else
                        {
                            objDPArr[3].Value = DBNull.Value;
                        }

                        objDPArr[4].Value = p_objCollection.m_objOperationArr[i].m_strOperationID;
                        if (p_objCollection.m_objOperationArr[i].m_strCutLevel != null
                            && p_objCollection.m_objOperationArr[i].m_strCutLevel.Trim() != "/")
                        {
                            string[] strArr = p_objCollection.m_objOperationArr[i].m_strCutLevel.Split('/');
                            if (strArr != null && strArr.Length > 0)
                            {
                                objDPArr[5].Value = strArr[0];
                                if (strArr.Length > 1)
                                {
                                    if (strArr[1] == "甲" || strArr[1] == "甲级")
                                        objDPArr[6].Value = "1";
                                    else if (strArr[1] == "乙" || strArr[1] == "乙级")
                                    {
                                        objDPArr[6].Value = "2";
                                    }
                                    else if (strArr[1] == "丙" || strArr[1] == "丙级")
                                    {
                                        objDPArr[6].Value = "3";
                                    }
                                }
                                else
                                {
                                    objDPArr[6].Value = DBNull.Value;
                                }
                            }
                        }
                        objDPArr[7].Value = p_objCollection.m_objOperationArr[i].m_strOperatorName;
                        objDPArr[8].Value = p_objCollection.m_objOperationArr[i].m_strAanaesthesiaModeName;
                        objDPArr[9].Value = p_objCollection.m_objOperationArr[i].m_strAssistant1Name;
                        objDPArr[10].Value = p_objCollection.m_objOperationArr[i].m_strAssistant2Name;
                        objDPArr[11].Value = p_objCollection.m_objOperationArr[i].m_strAnaesthetistName;
                        objDPArr[12].Value = p_strPatientName;

                        lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
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

        #region 提交记录到ba5
        /// <summary>
        /// 提交记录到ba5
        /// </summary>
        /// <param name="p_objCollection">住院病案首页内容</param>
        /// <param name="p_intInTimes">住院次数</param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToBA5(clsInHospitalMainRecord_Collection p_objCollection, int p_intInTimes, string p_strPatientName)
        {
            long lngRes = -1;
            try
            {
                if (p_objCollection == null || p_objCollection.m_objMain == null || p_objCollection.m_objContent == null || p_strPatientName == null)
                {
                    return -1;
                }
                string strDelSQL = @"select * from ba5 where prn = ? and times = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                objDPArr[1].Value = p_intInTimes;

                long lngEff = 0;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strDelSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                    return 0;

                string strCommitSQL = @"insert into ba5 (PRN,TIMES,BABYNUM,BABYSEX,TZ,HC,ZG,YNGR,GRNAME,BABYQJ,BABYSUC,HX,name)
VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";

                if (p_objCollection.m_objBabyArr != null && p_objCollection.m_objBabyArr.Length > 0)
                {
                    int intTemp = 0;
                    for (int i = 0; i < p_objCollection.m_objBabyArr.Length; i++)
                    {
                        objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(13, out objDPArr);
                        objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                        objDPArr[1].Value = p_intInTimes;
                        objDPArr[2].Value = (Convert.ToInt32(p_objCollection.m_objBabyArr[i].m_strSeqID) + 1).ToString();
                        if (p_objCollection.m_objBabyArr[i].m_strFemale == "1")
                            objDPArr[3].Value = "2";
                        else if (p_objCollection.m_objBabyArr[i].m_strMale == "1")
                            objDPArr[3].Value = "1";
                        else
                        {
                            objDPArr[3].Value = DBNull.Value;
                        }
                        if (!string.IsNullOrEmpty(p_objCollection.m_objBabyArr[i].m_strWeight))
                        {
                            if (p_objCollection.m_objBabyArr[i].m_strWeight.Trim().Length > 4)
                            {
                                objDPArr[4].Value = DBNull.Value;
                            }
                            else
                            {
                                objDPArr[4].Value = p_objCollection.m_objBabyArr[i].m_strWeight.Trim();
                            }
                        }
                        else
                        {
                            objDPArr[4].Value = DBNull.Value;
                        }
                        if (p_objCollection.m_objBabyArr[i].m_strLiveBorn == "1")
                            objDPArr[5].Value = "1";
                        else if (p_objCollection.m_objBabyArr[i].m_strDieBorn == "1")
                            objDPArr[5].Value = "2";
                        else if (p_objCollection.m_objBabyArr[i].m_strDieNotBorn == "1")
                            objDPArr[5].Value = "3";
                        else
                        {
                            objDPArr[5].Value = DBNull.Value;
                        }
                        if (p_objCollection.m_objBabyArr[i].m_strDie == "1")
                            objDPArr[6].Value = "1";
                        else if (p_objCollection.m_objBabyArr[i].m_strChangeDepartment == "1")
                            objDPArr[6].Value = "2";
                        else if (p_objCollection.m_objBabyArr[i].m_strOutHospital == "1")
                            objDPArr[6].Value = "3";
                        else
                        {
                            objDPArr[6].Value = DBNull.Value;
                        }
                        if (int.TryParse(p_objCollection.m_objBabyArr[i].m_strInfectionTimes, out intTemp))
                        {
                            objDPArr[7].Value = intTemp;
                        }
                        else
                        {
                            objDPArr[7].Value = DBNull.Value;
                        }
                        objDPArr[8].Value = p_objCollection.m_objBabyArr[i].m_strInfectionName;
                        if (int.TryParse(p_objCollection.m_objBabyArr[i].m_strSalveTimes, out intTemp))
                        {
                            objDPArr[9].Value = intTemp;
                        }
                        else
                        {
                            objDPArr[9].Value = DBNull.Value;
                        }
                        if (int.TryParse(p_objCollection.m_objBabyArr[i].m_strSalveSuccessTimes, out intTemp))
                        {
                            objDPArr[10].Value = intTemp;
                        }
                        else
                        {
                            objDPArr[10].Value = DBNull.Value;
                        }
                        if (p_objCollection.m_objBabyArr[i].m_strNaturalCondiction == "1")
                            objDPArr[11].Value = "1";
                        else if (p_objCollection.m_objBabyArr[i].m_strSuffocate1 == "1")
                            objDPArr[11].Value = "2";
                        else if (p_objCollection.m_objBabyArr[i].m_strSuffocate2 == "1")
                            objDPArr[11].Value = "3";
                        else
                        {
                            objDPArr[11].Value = DBNull.Value;
                        }
                        objDPArr[12].Value = p_strPatientName;

                        lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
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

        #region 提交记录到ba6
        /// <summary>
        /// 提交记录到ba6
        /// </summary>
        /// <param name="p_objCollection">住院病案首页内容</param>
        /// <param name="p_intInTimes">住院次数</param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToBA6(clsInHospitalMainRecord_Collection p_objCollection, int p_intInTimes, string p_strPatientName)
        {
            long lngRes = -1;
            try
            {
                if (p_objCollection == null || p_objCollection.m_objMain == null || p_objCollection.m_objContent == null || p_strPatientName == null)
                {
                    return -1;
                }

                string strCheckSQL = @"select * from ba6 where prn = ? and times = ?";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                objDPArr[1].Value = p_intInTimes;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCheckSQL, ref dtbValue, objDPArr);

                if (lngRes < 0)
                    return -1;

                string strCommitSQL = "";
                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    return 0;
                }
                else
                {
                    strCommitSQL = @"insert into ba6 (mzzd10,ryzd10,hbsag,hcv_ab,hiv_ab,kzr,jxdoct,ysxdoct,zkdoct,zknurse,
zkrq,redcell,plaque,serous,allblood,otherblood,hz_yj,hz_yc,hl_tj,hl_i,hl_ii,hl_iii,hl_zz,hl_ts,prn,times,name,hlf,mzf,yef,pcf) 
values(?,?,?,?,?,?,?,?,?,?,
	   ?,?,?,?,?,?,?,?,?,?,
	   ?,?,?,?,?,?,?,?,?,?,
       ?)";
                }

                string strRYZD = "";
                if (p_objCollection.m_objDiagnosisArr != null && p_objCollection.m_objDiagnosisArr.Length > 0)
                {
                    for (int i = 0; i < p_objCollection.m_objDiagnosisArr.Length; i++)
                    {
                        if (p_objCollection.m_objDiagnosisArr[i].m_strDIAGNOSISTYPE == "1")
                        {
                            strRYZD = p_objCollection.m_objDiagnosisArr[i].m_strICD10;
                            break;
                        }
                    }
                }
                double dblTemp = 0D;
                int intTemp = 0;
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(31, out objDPArr);
                objDPArr[0].Value = p_objCollection.m_objMain.m_strMZICD10;
                objDPArr[1].Value = strRYZD;
                objDPArr[2].Value = p_objCollection.m_objContent.m_strHbsAg;
                objDPArr[3].Value = p_objCollection.m_objContent.m_strHCV_Ab;
                objDPArr[4].Value = p_objCollection.m_objContent.m_strHIV_Ab;
                objDPArr[5].Value = p_objCollection.m_objContent.m_strDirectorDtName;
                objDPArr[6].Value = p_objCollection.m_objContent.m_strAttendInForAdvancesStudyDtName;
                objDPArr[7].Value = p_objCollection.m_objContent.m_strGraduateStudentInternName;
                objDPArr[8].Value = p_objCollection.m_objContent.m_strQCDtName;
                objDPArr[9].Value = p_objCollection.m_objContent.m_strQCNurseName;
                objDPArr[10].Value = Convert.ToDateTime(p_objCollection.m_objContent.m_strQCTime).ToString("yyyy/MM/dd");
                if (double.TryParse(p_objCollection.m_objContent.m_strRBC, out dblTemp))
                {
                    objDPArr[11].Value = dblTemp;
                }
                else
                {
                    objDPArr[11].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strPLT, out dblTemp))
                {
                    objDPArr[12].Value = dblTemp;
                }
                else
                {
                    objDPArr[12].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strPlasm, out dblTemp))
                {
                    objDPArr[13].Value = dblTemp;
                }
                else
                {
                    objDPArr[13].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strWholeBlood, out dblTemp))
                {
                    objDPArr[14].Value = dblTemp;
                }
                else
                {
                    objDPArr[14].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strOtherBlood, out dblTemp))
                {
                    objDPArr[15].Value = dblTemp;
                }
                else
                {
                    objDPArr[15].Value = DBNull.Value;
                }
                if (int.TryParse(p_objCollection.m_objContent.m_strConsultation, out intTemp))
                {
                    objDPArr[16].Value = intTemp;
                }
                else
                {
                    objDPArr[16].Value = DBNull.Value;
                }
                if (int.TryParse(p_objCollection.m_objContent.m_strLongDistanctConsultation, out intTemp))
                {
                    objDPArr[17].Value = intTemp;
                }
                else
                {
                    objDPArr[17].Value = DBNull.Value;
                }
                if (int.TryParse(p_objCollection.m_objContent.m_strTOPLevel, out intTemp))
                {
                    objDPArr[18].Value = intTemp;
                }
                else
                {
                    objDPArr[18].Value = DBNull.Value;
                }
                if (int.TryParse(p_objCollection.m_objContent.m_strNurseLevelI, out intTemp))
                {
                    objDPArr[19].Value = intTemp;
                }
                else
                {
                    objDPArr[19].Value = DBNull.Value;
                }
                if (int.TryParse(p_objCollection.m_objContent.m_strNurseLevelII, out intTemp))
                {
                    objDPArr[20].Value = intTemp;
                }
                else
                {
                    objDPArr[20].Value = DBNull.Value;
                }
                if (int.TryParse(p_objCollection.m_objContent.m_strNurseLevelIII, out intTemp))
                {
                    objDPArr[21].Value = intTemp;
                }
                else
                {
                    objDPArr[21].Value = DBNull.Value;
                }
                if (int.TryParse(p_objCollection.m_objContent.m_strICU, out intTemp))
                {
                    objDPArr[22].Value = intTemp;
                }
                else
                {
                    objDPArr[22].Value = DBNull.Value;
                }
                if (int.TryParse(p_objCollection.m_objContent.m_strSpecialNurse, out intTemp))
                {
                    objDPArr[23].Value = intTemp;
                }
                else
                {
                    objDPArr[23].Value = DBNull.Value;
                }
                objDPArr[24].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                objDPArr[25].Value = p_intInTimes;
                objDPArr[26].Value = p_strPatientName;
                if (double.TryParse(p_objCollection.m_objContent.m_strBloodAmt, out dblTemp))
                {
                    objDPArr[27].Value = dblTemp;
                }
                else
                {
                    objDPArr[27].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strAnaethesiaAmt, out dblTemp))
                {
                    objDPArr[28].Value = dblTemp;
                }
                else
                {
                    objDPArr[28].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strBabyAmt, out dblTemp))
                {
                    objDPArr[29].Value = dblTemp;
                }
                else
                {
                    objDPArr[29].Value = DBNull.Value;
                }
                if (double.TryParse(p_objCollection.m_objContent.m_strAccompanyAmt, out dblTemp))
                {
                    objDPArr[30].Value = dblTemp;
                }
                else
                {
                    objDPArr[30].Value = DBNull.Value;
                }

                //执行SQL	
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
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
        #endregion

        #region 提交记录到ba9
        /// <summary>
        /// 提交记录到ba9
        /// </summary>
        /// <param name="p_objCollection">住院病案首页内容</param>
        /// <param name="p_intInTimes">住院次数</param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToBA9(clsInHospitalMainRecord_Collection p_objCollection, int p_intInTimes, string p_strPatientName)
        {
            long lngRes = -1;
            try
            {
                if (p_objCollection == null || p_objCollection.m_objMain == null || p_objCollection.m_objContent == null || p_strPatientName == null)
                {
                    return -1;
                }
                string strDelSQL = @"select * from ba9 where prn = ? and times = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                objDPArr[1].Value = p_intInTimes;

                long lngEff = 0;
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strDelSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                    return 0;

                string strCommitSQL = @"insert into ba9 (prn,times,flfs,flcs,flzz,ygy,ycs,yts,yrq1,yrq2,qgy,qcs,qts,qrq1,qrq2,
zgy,zcs,zts,zrq1,zrq2,hlfs,hlff,name)
values(?,?,?,?,?,?,?,?,?,?,
	   ?,?,?,?,?,?,?,?,?,?,
	   ?,?,?)";
                string[] strRTModeArr = new string[] { "根治性", "姑息性", "辅助性" };
                string[] strRTEquipmentArr = new string[] { "钴", "直加", "X线", "后加" };
                string[] strRTRuleArr = new string[] { "连续", "间断", "分段" };
                string[] strChemotherapyModeArr = new string[] { "根治性", "姑息性", "新辅助性", "辅助性", "新药" };
                string[] strChemotherapyRuleArr = new string[] { "全化", "半化", "A插管", "胸腔注", "腹腔注", "髓注", "其他试用", "其他" };

                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(23, out objDPArr);
                objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                objDPArr[1].Value = p_intInTimes;
                if (p_objCollection.m_objContent.m_strRTModeSeq != "-1")
                    objDPArr[2].Value = strRTModeArr[Convert.ToInt32(p_objCollection.m_objContent.m_strRTModeSeq)];
                if (p_objCollection.m_objContent.m_strRTRuleSeq != "-1")
                    objDPArr[3].Value = strRTRuleArr[Convert.ToInt32(p_objCollection.m_objContent.m_strRTRuleSeq)];
                if (p_objCollection.m_objContent.m_strRTCo == "1")
                    objDPArr[4].Value = strRTEquipmentArr[0];
                else if (p_objCollection.m_objContent.m_strRTAccelerator == "1")
                    objDPArr[4].Value = strRTEquipmentArr[1];
                else if (p_objCollection.m_objContent.m_strRTX_Ray == "1")
                    objDPArr[4].Value = strRTEquipmentArr[2];
                else if (p_objCollection.m_objContent.m_strRTLacuna == "1")
                    objDPArr[4].Value = strRTEquipmentArr[3];
                objDPArr[5].Value = p_objCollection.m_objContent.m_strOriginalDiseaseGy;
                objDPArr[6].Value = p_objCollection.m_objContent.m_strOriginalDiseaseTimes;
                objDPArr[7].Value = p_objCollection.m_objContent.m_strOriginalDiseaseDays;
                objDPArr[8].Value = Convert.ToDateTime(p_objCollection.m_objContent.m_strOriginalDiseaseBeginDate).ToString("yyyy/MM/dd");
                objDPArr[9].Value = Convert.ToDateTime(p_objCollection.m_objContent.m_strOriginalDiseaseEndDate).ToString("yyyy/MM/dd");
                objDPArr[10].Value = p_objCollection.m_objContent.m_strLymphGy;
                objDPArr[11].Value = p_objCollection.m_objContent.m_strLymphTimes;
                objDPArr[12].Value = p_objCollection.m_objContent.m_strLymphDays;
                objDPArr[13].Value = Convert.ToDateTime(p_objCollection.m_objContent.m_strLymphBeginDate).ToString("yyyy/MM/dd");
                objDPArr[14].Value = Convert.ToDateTime(p_objCollection.m_objContent.m_strLymphEndDate).ToString("yyyy/MM/dd");
                objDPArr[15].Value = p_objCollection.m_objContent.m_strMetastasisGy;
                objDPArr[16].Value = p_objCollection.m_objContent.m_strMetastasisTimes;
                objDPArr[17].Value = p_objCollection.m_objContent.m_strMetastasisDays;
                objDPArr[18].Value = Convert.ToDateTime(p_objCollection.m_objContent.m_strMetastasisBeginDate).ToString("yyyy/MM/dd");
                objDPArr[19].Value = Convert.ToDateTime(p_objCollection.m_objContent.m_strMetastasisEndDate).ToString("yyyy/MM/dd");
                if (p_objCollection.m_objContent.m_strChemotherapyModeSeq != "-1")
                    objDPArr[20].Value = strChemotherapyModeArr[Convert.ToInt32(p_objCollection.m_objContent.m_strChemotherapyModeSeq)];
                if (p_objCollection.m_objContent.m_strChemotherapyWholeBody != "1")
                    objDPArr[21].Value = strChemotherapyRuleArr[0];
                else if (p_objCollection.m_objContent.m_strChemotherapyLocal != "1")
                    objDPArr[21].Value = strChemotherapyRuleArr[1];
                else if (p_objCollection.m_objContent.m_strChemotherapyIntubate != "1")
                    objDPArr[21].Value = strChemotherapyRuleArr[2];
                else if (p_objCollection.m_objContent.m_strChemotherapyThorax != "1")
                    objDPArr[21].Value = strChemotherapyRuleArr[3];
                else if (p_objCollection.m_objContent.m_strChemotherapyAbdomen != "1")
                    objDPArr[21].Value = strChemotherapyRuleArr[4];
                else if (p_objCollection.m_objContent.m_strChemotherapySpinal != "1")
                    objDPArr[21].Value = strChemotherapyRuleArr[5];
                else if (p_objCollection.m_objContent.m_strChemotherapyOtherTry != "1")
                    objDPArr[21].Value = strChemotherapyRuleArr[6];
                else if (p_objCollection.m_objContent.m_strChemotherapyOther != "1")
                    objDPArr[21].Value = strChemotherapyRuleArr[7];
                objDPArr[22].Value = p_strPatientName;

                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
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

        #region 提交记录到ba10
        /// <summary>
        /// 提交记录到ba10
        /// </summary>
        /// <param name="p_objCollection">住院病案首页内容</param>
        /// <param name="p_intInTimes">住院次数</param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToBA10(clsInHospitalMainRecord_Collection p_objCollection, int p_intInTimes, string p_strPatientName)
        {
            long lngRes = -1;
            try
            {
                if (p_objCollection == null || p_objCollection.m_objMain == null || p_objCollection.m_objContent == null || p_strPatientName == null)
                {
                    return -1;
                }
                string strDelSQL = @"select * from ba10 where prn = ? and times = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                objDPArr[1].Value = p_intInTimes;

                long lngEff = 0;
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strDelSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                    return 0;

                string strCommitSQL = @"insert into ba10 (prn,times,hldate1,hldrug,hlproc,hljg,name)
values(?,?,?,?,?,?,?)";

                if (p_objCollection.m_objChemotherapyArr != null && p_objCollection.m_objChemotherapyArr.Length > 0)
                {
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < p_objCollection.m_objChemotherapyArr.Length; i++)
                    {
                        objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(7, out objDPArr);
                        objDPArr[0].Value = p_objCollection.m_objMain.m_strInPatientID.Trim();
                        objDPArr[1].Value = p_intInTimes;
                        if (DateTime.TryParse(p_objCollection.m_objChemotherapyArr[i].m_strChemotherapyDate, out dtmTemp))
                        {
                            objDPArr[2].Value = dtmTemp.ToString("yyyy/MM/dd");
                        }
                        else
                        {
                            objDPArr[2].Value = DBNull.Value;
                        }
                        objDPArr[3].Value = p_objCollection.m_objChemotherapyArr[i].m_strMedicineName;
                        objDPArr[4].Value = p_objCollection.m_objChemotherapyArr[i].m_strPeriod;
                        if (p_objCollection.m_objChemotherapyArr[i].m_strField_CR == "1")
                            objDPArr[5].Value = "CR";
                        else if (p_objCollection.m_objChemotherapyArr[i].m_strField_MR == "1")
                            objDPArr[5].Value = "MR";
                        else if (p_objCollection.m_objChemotherapyArr[i].m_strField_NA == "1")
                            objDPArr[5].Value = "NA";
                        else if (p_objCollection.m_objChemotherapyArr[i].m_strField_P == "1")
                            objDPArr[5].Value = "P";
                        else if (p_objCollection.m_objChemotherapyArr[i].m_strField_PR == "1")
                            objDPArr[5].Value = "PR";
                        else if (p_objCollection.m_objChemotherapyArr[i].m_strField_S == "1")
                            objDPArr[5].Value = "S";
                        objDPArr[6].Value = p_strPatientName;

                        lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
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

        #region 提交记录到ba7
        /// <summary>
        /// 提交记录到ba7
        /// </summary>
        /// <param name="p_objTransferDeptArr">转科</param>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_intInTimes">住院次数</param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToBA7(clsInHospitalMainTransDeptInstance p_objTransferDeptArr, string p_strInPatientID, int p_intInTimes, string p_strPatientName)
        {
            if (p_objTransferDeptArr == null || string.IsNullOrEmpty(p_strInPatientID) || p_strPatientName == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strDelSQL = @"select * from ba7 where prn = ? and times = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(0, 2);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = p_intInTimes;

                long lngEff = 0;
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strDelSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                    return 0;

                string strCommitSQL = @"insert into ba7(prn,name,times,zknum,zkdept,zkdate,zktime) values (?,?,?,?,?,?,?)";

                if (p_objTransferDeptArr.m_strTransTargetAreaIDArr != null && p_objTransferDeptArr.m_strTransTargetAreaIDArr.Length > 0)
                {
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int index = 0; index < p_objTransferDeptArr.m_strTransTargetAreaIDArr.Length; index++)
                    {
                        objHRPServ.CreateDatabaseParameter(7, out objDPArr);
                        objDPArr[0].Value = p_strInPatientID;
                        objDPArr[1].Value = p_strPatientName;
                        objDPArr[2].Value = p_intInTimes;
                        objDPArr[3].Value = p_objTransferDeptArr.m_strTransTargetAreaIDArr[index];
                        objDPArr[4].Value = p_objTransferDeptArr.m_strTransTargetAreaNameArr[index];
                        if (DateTime.TryParse(p_objTransferDeptArr.m_strTransAreaDateArr[index], out dtmTemp))
                        {
                            objDPArr[5].Value = dtmTemp.ToString("yyyy/MM/dd");
                            objDPArr[6].Value = dtmTemp.ToString("HH");
                        }
                        else
                        {
                            objDPArr[5].Value = DBNull.Value;
                            objDPArr[6].Value = DBNull.Value;
                        }
                    }
                    lngRes = objHRPServ.lngExecuteParameterSQL(strCommitSQL, ref lngEff, objDPArr);
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
        #endregion

        /// <summary>
        /// 获取住院结算表里面病人自付金额部分
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSelfPay(
            string p_strInpatientID,out clsInHospitalMainRecord_Content p_objRecordcontent)//, DateTime p_dtmInhospitalDate,
        {
            p_objRecordcontent = null;
            if (string.IsNullOrEmpty(p_strInpatientID))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                string strSQL = @"select sum(t.sbsum_mny) sbsum_mny
  from t_opr_bih_charge t, t_Opr_Bih_Register t2
 where t.registerid_chr = t2.registerid_chr  and t2.registerid_chr = ?";
  // and t2.inpatientid_chr = ?
  // and t2.inpatient_dat = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInpatientID;
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
        public long m_lngGetCHRCATE( string p_strRegisterID, out clsInHospitalMainCharge[] p_objRecordArr)
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
          from (select (round(nvl((b.unitprice_dec * b.amount_dec), 0), 2) +
                       round(nvl(b.totaldiffcostmoney_dec, 0), 2)) as tolfee_mny,
                       --  b.totalmoney_dec   tolfee_mny,
                       c.itembihctype_chr,
                       d.typename_vchr    groupname_chr
                  from t_opr_bih_patientcharge b,
                       t_bse_chargeitem        c,
                       t_bse_chargeitemextype  d
                 where b.chargeitemid_chr = c.itemid_chr
                   and b.status_int = 1
                   and b.pstatus_int > 0 
                      --and b.pstatus_int in (1, 2)
                   and c.itembihctype_chr = d.typeid_chr
                   and d.flag_int = 5
                   and b.registerid_chr = ?) k
         group by k.groupname_chr
";

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
        /// <summary>
        /// 获取同步表费用信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">病人入院登记流水号</param>
        /// <param name="p_objRecordArr">记录列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCHRCATE2009( string p_strRegisterID, out clsInHospitalMainCharge[] p_objRecordArr)
        {
            p_objRecordArr = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @" select sum(k.tolfee_mny) tolfee_mny, k.groupname_chr
  from (select (round(nvl((b.unitprice_dec * b.amount_dec), 0), 2) +
                round(nvl(b.totaldiffcostmoney_dec, 0), 2)) as tolfee_mny,
                --  b.totalmoney_dec   tolfee_mny,
               c.itembihctype_chr,
               d.typename_vchr    groupname_chr
          from t_opr_bih_patientcharge b,
               t_bse_chargeitem        c,
               t_bse_chargeitemextype_bak  d
         where b.chargeitemid_chr = c.itemid_chr
           and b.status_int = 1
           and b.pstatus_int > 0 
           --and b.pstatus_int in (1, 2)
           and c.itembihctypebak_chr = d.typeid_chr
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
        /// <summary>
        /// 产科获取同步表费用信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">病人入院登记流水号</param>
        /// <param name="p_objRecordArr">记录列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeChanKe( string p_strRegisterID, DataTable p_strbbReisterID, out clsInHospitalMainCharge[] p_objRecordArr)
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
  from (select b.totalmoney_dec   tolfee_mny,
               c.itembihctype_chr,
               d.typename_vchr    groupname_chr
          from t_opr_bih_patientcharge b,
               t_bse_chargeitem        c,
               t_bse_chargeitemextype  d
         where b.chargeitemid_chr = c.itemid_chr
           and b.status_int = 1
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
        /// 更新病案首页的时间
        /// </summary>
        /// <param name="m_dtmUpdateDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public DateTime m_lngGetUpdateDatetime()
        {
            long lngRes = 0;
            DataTable dtResult = new DataTable();
            DateTime m_dtmUpdateDate = DateTime.MinValue;
            string strSQL = @"select updatedate from t_bse_hospitalinfo";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                DateTime.TryParse(dtResult.Rows[0][0].ToString(), out m_dtmUpdateDate);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return m_dtmUpdateDate;

        }
    }
    /// <summary>
    /// 从病案系统中获取字典数据，不支持事务
    /// </summary>
    [Transaction(TransactionOption.NotSupported)]
    [ObjectPooling(true)]
    public class clsDictFromBAServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 从广东病案系统获取ICD诊断
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        public long m_lngGetICDDiagnosisCode( out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector((byte)clsHRPTableService.enumDatabase_Selector.bytSQL_Server, (byte)clsHRPTableService.enumDatabase.bytGDCASE);

                string strSQL = @"select t.ficdm code,
       t.fjbname name,
       t.ftjm
from   ticd t
where  t.ficdversion = 10
order by t.ficdm,t.fjbname";

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPServ.Dispose();
                objHRPServ = null;
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
        /// 获取麻醉方式
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetAnaesthesiaMode( out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector((byte)clsHRPTableService.enumDatabase_Selector.bytSQL_Server, (byte)clsHRPTableService.enumDatabase.bytGDCASE);
                string strSQL = @"select t.fbh code,
       t.fmc name,
       t.fzjc py,
       right('0000'+t.fbh,4) seq
from   tstandardmx t
where  t.fcode = 'GBMZFS'
order by seq, name";
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPServ.Dispose();
                objHRPServ = null;
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
        /// 获取广东省病案统计系统手术码数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtResult">查询结果</param>
        /// <returns></returns>
        public long m_lngGetOprationCode(
            out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = null;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector((byte)clsHRPTableService.enumDatabase_Selector.bytSQL_Server, (byte)clsHRPTableService.enumDatabase.bytGDCASE);

                string strSQL = @"select t.fopcode code,t.fopname name from toperate t";
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        
    }
}
