using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;

using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using iCare.RIS.Utility;

namespace com.digitalwave.iCare.middletier.RIS
{
    /// <summary>
    /// 超声报告查询中间件 by haozhong.liu
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsRIS_USReportQuery_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngGetReportList(string p_strSqlCase, List<string> p_lstParam, out List<clsUltraSoundReport> p_lstReport)
        {
            string strSql = @"select distinct t.reportid_chr,
       t.otherid_chr,
       t.checkdate_dat,
       t.checkroom_chr,
       t.applyid_chr,
       t.applydepartment_chr,
       t.applydeptid_chr,
       t.applydoctor_chr,
       t.applydoctorid_chr,
       t.applydate_dat,
       t.applypurpose_chr,
       t.sourcetype_chr,
       t.patientcardid_chr,
       t.patientid_chr,
       t.inpatientid_chr,
       t.inhospitalnumber_chr,
       t.bedno_chr,
       t.patientname_chr,
       t.gender_chr,
       t.age_chr,
       t.address_chr,
       t.telephone_chr,
       t.checkpart_chr,
       t.previoushistory_chr,
       t.clinicaldiagnosis_chr,
       t.operationfindings_chr,
       t.remark_chr,
       t.chargedetail_chr,
       t.ultrasoundfindings_chr,
       t.ultrasoundprompts_chr,
       t.heartsetting_int,
       t.heartnumber_int,
       t.positivenumber_int,
       t.reportdoctorid_chr,
       t.reportdoctor_chr,
       t.reportdate_dat,
       t.status_int,
       t.area_chr,
       t.m_modelsetting_chr,
       t.b_modelsetting_chr,
       t.functionsetting_chr,
       t.dopplersetting_chr,
       a.isgreen_int
  from t_ris_us_report t, t_opr_attachrelation a where t.applyid_chr = a.attachid_vchr(+) and ";
            strSql += p_strSqlCase;
            strSql += " order by t.applydate_dat";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            p_lstReport = null;
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(p_lstParam.Count, out objDPArr);
                int i = 0;
                foreach (object objParam in p_lstParam)
                {
                    objDPArr[i].Value = p_lstParam[i];
                    i++;
                }
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0)
                {
                    p_lstReport = new List<clsUltraSoundReport>(dtValue.Rows.Count);
                    clsUltraSoundReport objRecord = null;
                    foreach (DataRow row in dtValue.Rows)
                    {
                        objRecord = m_objDataRow2Record(ref objRecord, row);
                        p_lstReport.Add(objRecord);
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetReportTitleList(string p_strSqlCase, List<string> p_lstParam, out List<clsUltraSoundReport> p_lstReport)
        {
            string strSql = @"select t.reportid_chr,
       t.otherid_chr,       
       t.applyid_chr,       
       t.applydate_dat,
       t.patientcardid_chr
  from t_ris_us_report t where ";
            strSql += p_strSqlCase;
            strSql += " order by t.applydate_dat";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            p_lstReport = null;
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(p_lstParam.Count, out objDPArr);
                int i = 0;
                foreach (object objParam in p_lstParam)
                {
                    objDPArr[i].Value = p_lstParam[i];
                    i++;
                }
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0)
                {
                    p_lstReport = new List<clsUltraSoundReport>(dtValue.Rows.Count);
                    clsUltraSoundReport objRecord = null;
                    foreach (DataRow row in dtValue.Rows)
                    {
                        objRecord = new clsUltraSoundReport();
                        objRecord.m_strReportID = clsMyConverter.ToString(row["reportid_chr"]);
                        objRecord.m_strOtherID = clsMyConverter.ToString(row["otherid_chr"]);
                        objRecord.m_strApplyID = clsMyConverter.ToString(row["applyid_chr"]);
                        objRecord.m_dtmApplyDate = clsMyConverter.ToDateTime(row["applydate_dat"]);
                        // 2011.8.18\
                        objRecord.m_strPatientCardID = clsMyConverter.ToString(row["patientcardid_chr"]);
                        p_lstReport.Add(objRecord);
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetReport(string p_strReportID, out clsUltraSoundReport p_objReport)
        {
            string strSql = @"select t.reportid_chr,
       t.otherid_chr,
       t.checkdate_dat,
       t.checkroom_chr,
       t.applyid_chr,
       t.applydepartment_chr,
       t.applydeptid_chr,
       t.applydoctor_chr,
       t.applydoctorid_chr,
       t.applydate_dat,
       t.applypurpose_chr,
       t.sourcetype_chr,
       t.patientcardid_chr,
       t.patientid_chr,
       t.inpatientid_chr,
       t.inhospitalnumber_chr,
       t.bedno_chr,
       t.patientname_chr,
       t.gender_chr,
       t.age_chr,
       t.address_chr,
       t.telephone_chr,
       t.checkpart_chr,
       t.previoushistory_chr,
       t.clinicaldiagnosis_chr,
       t.operationfindings_chr,
       t.remark_chr,
       t.chargedetail_chr,
       t.ultrasoundfindings_chr,
       t.ultrasoundprompts_chr,
       t.heartsetting_int,
       t.heartnumber_int,
       t.positivenumber_int,
       t.reportdoctorid_chr,
       t.reportdoctor_chr,
       t.reportdate_dat,
       t.status_int,
       t.area_chr,
       t.m_modelsetting_chr,
       t.b_modelsetting_chr,
       t.functionsetting_chr,
       t.dopplersetting_chr,
       a.isgreen_int
  from t_ris_us_report t, t_opr_attachrelation a  
     where t.applyid_chr = a.attachid_vchr(+) 
       and t.status_int >= 0 
       and t.reportid_chr = ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            p_objReport = null;
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strReportID;
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0 && dtValue.Rows.Count > 0)
                {
                    m_objDataRow2Record(ref p_objReport, dtValue.Rows[0]);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }


        //DataRow数据转换成申请单vo
        private clsUltraSoundReport m_objDataRow2Record(ref clsUltraSoundReport objRecord, DataRow row)
        {
            objRecord = new clsUltraSoundReport();
            objRecord.m_dtmApplyDate = clsMyConverter.ToDateTime(row["applydate_dat"]);
            objRecord.m_dtmReportDate = clsMyConverter.ToDateTime(row["reportdate_dat"]);
            objRecord.m_intHeartNumber = clsMyConverter.ToInt32(row["heartnumber_int"]);
            objRecord.m_intHeartSetting = clsMyConverter.ToInt32(row["heartsetting_int"]);
            objRecord.m_dtmCheckDate = clsMyConverter.ToDateTime(row["checkdate_dat"]);
            objRecord.m_strAddress = clsMyConverter.ToString(row["address_chr"]);
            objRecord.m_strAge = clsMyConverter.ToString(row["age_chr"]);
            objRecord.m_strApplyID = clsMyConverter.ToString(row["applyid_chr"]);
            objRecord.m_intPositiveNumber = clsMyConverter.ToInt32(row["positivenumber_int"]);
            objRecord.m_intStatus = clsMyConverter.ToInt32(row["status_int"]);
            objRecord.m_strApplyDoctor = clsMyConverter.ToString(row["applydoctor_chr"]);
            objRecord.m_strApplyDoctorID = clsMyConverter.ToString(row["applydoctorid_chr"]);
            objRecord.m_strApplyPurpose = clsMyConverter.ToString(row["applypurpose_chr"]);
            objRecord.m_strBedNo = clsMyConverter.ToString(row["bedno_chr"]);
            objRecord.m_strCheckRoom = clsMyConverter.ToString(row["checkroom_chr"]);
            objRecord.m_strChargeDetail = clsMyConverter.ToString(row["chargedetail_chr"]);
            objRecord.m_strCheckPart = clsMyConverter.ToString(row["checkpart_chr"]);
            objRecord.m_strClinicalDiano = clsMyConverter.ToString(row["clinicaldiagnosis_chr"]);
            objRecord.m_strDepName = clsMyConverter.ToString(row["applydepartment_chr"]);
            objRecord.m_strInhospitalNumber = clsMyConverter.ToString(row["inhospitalnumber_chr"]);
            objRecord.m_strDeptID = clsMyConverter.ToString(row["applydeptid_chr"]);
            objRecord.m_strOtherID = clsMyConverter.ToString(row["otherid_chr"]);
            objRecord.m_strPatientCardID = clsMyConverter.ToString(row["patientcardid_chr"]);
            objRecord.m_strInPatientID = clsMyConverter.ToString(row["inpatientid_chr"]);
            objRecord.m_strPatientID = clsMyConverter.ToString(row["patientid_chr"]);
            objRecord.m_strPatientName = clsMyConverter.ToString(row["patientname_chr"]);
            objRecord.m_strPreviousHistory = clsMyConverter.ToString(row["previoushistory_chr"]);
            objRecord.m_strRemark = clsMyConverter.ToString(row["remark_chr"]);
            objRecord.m_strReportDoctor = clsMyConverter.ToString(row["reportdoctor_chr"]);
            objRecord.m_strReportDoctorID = clsMyConverter.ToString(row["reportdoctorid_chr"]);
            objRecord.m_strReportID = clsMyConverter.ToString(row["reportid_chr"]);
            objRecord.m_strSex = clsMyConverter.ToString(row["gender_chr"]);
            objRecord.m_strSourceType = clsMyConverter.ToString(row["sourcetype_chr"]);
            objRecord.m_strTelephone = clsMyConverter.ToString(row["telephone_chr"]);
            objRecord.m_strUltrasoundFindings = clsMyConverter.ToString(row["ultrasoundfindings_chr"]);
            objRecord.m_strUltrasoundPrompts = clsMyConverter.ToString(row["ultrasoundprompts_chr"]);
            objRecord.m_strOperationFindings = clsMyConverter.ToString(row["operationfindings_chr"]);
            objRecord.m_strArea = clsMyConverter.ToString(row["area_chr"]);
            //是否是先诊疗后结算的标示
            try
            {
                int.TryParse(row["isgreen_int"].ToString().Trim(), out objRecord.m_intIsGreen);
            }
            catch (Exception objEx) { }


            objRecord.m_objB_ModelSetting.m_mthParse(row["b_modelsetting_chr"].ToString());
            objRecord.m_objM_ModelSetting.m_mthParse(row["m_modelsetting_chr"].ToString());
            objRecord.m_objDopplerSetting.m_mthParse(row["dopplersetting_chr"].ToString());
            objRecord.m_objFunctionSetting.m_mthParse(row["functionsetting_chr"].ToString());
            return objRecord;
        }
    }
}
