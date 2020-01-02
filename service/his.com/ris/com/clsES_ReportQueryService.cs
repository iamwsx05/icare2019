using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using System.Reflection;

using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using iCare.RIS.Utility;

namespace com.digitalwave.iCare.middletier.RIS
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsES_ReportQueryService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngGetReportList(string p_strSqlCase, List<string> p_lstParam, out List<clsEndoscopyReport> p_lstReport)
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
       t.endoscopyfindings_chr,
       t.endoscopydiagnose_chr,
       t.reportdoctorid_chr,
       t.reportdoctor_chr,
       t.reportdate_dat,
       t.status_int,
       t.area_chr,
       t.advise_chr,
       t.hp_chr,
       t.pathologyid_chr,
       t.pathologydiagnosis_chr
  from t_ris_es_report t where ";
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
                    p_lstReport = new List<clsEndoscopyReport>(dtValue.Rows.Count);
                    clsEndoscopyReport objRecord = null;
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
        public long m_lngGetReportTitleList(string p_strSqlCase, List<string> p_lstParam, out List<clsEndoscopyReport> p_lstReport)
        {
            string strSql = @"select t.reportid_chr,
       t.otherid_chr,       
       t.applyid_chr,       
       t.applydate_dat
  from t_ris_es_report t where ";
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
                    p_lstReport = new List<clsEndoscopyReport>(dtValue.Rows.Count);
                    clsEndoscopyReport objRecord = null;
                    foreach (DataRow row in dtValue.Rows)
                    {
                        objRecord = new clsEndoscopyReport();
                        objRecord.m_strReportID = clsMyConverter.ToString(row["reportid_chr"]);
                        objRecord.m_strOtherID = clsMyConverter.ToString(row["otherid_chr"]);
                        objRecord.m_strApplyID = clsMyConverter.ToString(row["applyid_chr"]);
                        objRecord.m_dtmApplyDate = clsMyConverter.ToDateTime(row["applydate_dat"]);
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
        public long m_lngGetReport(string p_strReportID, out clsEndoscopyReport p_objReport)
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
       t.endoscopyfindings_chr,
       t.endoscopydiagnose_chr,
       t.reportdoctorid_chr,
       t.reportdoctor_chr,
       t.reportdate_dat,
       t.status_int,
       t.area_chr,
       t.advise_chr,
       t.hp_chr,
       t.pathologyid_chr,
       t.pathologydiagnosis_chr
  from t_ris_es_report t where status_int >= 0 and reportid_chr = ?";
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
                if (lngRes > 0)
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
        private clsEndoscopyReport m_objDataRow2Record(ref clsEndoscopyReport objRecord, DataRow row)
        {
            objRecord = new clsEndoscopyReport();
            objRecord.m_dtmApplyDate = clsMyConverter.ToDateTime(row["applydate_dat"]);
            objRecord.m_dtmReportDate = clsMyConverter.ToDateTime(row["reportdate_dat"]);
            objRecord.m_dtmCheckDate = clsMyConverter.ToDateTime(row["checkdate_dat"]);
            objRecord.m_strAddress = clsMyConverter.ToString(row["address_chr"]);
            objRecord.m_strAge = clsMyConverter.ToString(row["age_chr"]);
            objRecord.m_strApplyID = clsMyConverter.ToString(row["applyid_chr"]);            
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
            objRecord.m_strEndoscopyDiagnose = clsMyConverter.ToString(row["endoscopydiagnose_chr"]);
            objRecord.m_strEndoscopyFindings = clsMyConverter.ToString(row["endoscopyfindings_chr"]);
            objRecord.m_strAdvise = clsMyConverter.ToString(row["advise_chr"]);
            objRecord.m_strOperationFindings = clsMyConverter.ToString(row["operationfindings_chr"]);
            objRecord.m_strArea = clsMyConverter.ToString(row["area_chr"]);
            objRecord.m_strHP = clsMyConverter.ToString(row["hp_chr"]);
            objRecord.m_strPathologyID = clsMyConverter.ToString(row["pathologyid_chr"]);
            objRecord.m_strPathologyDiagnosis = clsMyConverter.ToString(row["pathologydiagnosis_chr"]);
            return objRecord;
        }


        [AutoComplete]
        public long m_mthGetCommonTemplateInfo(string m_strEmpID, string m_strDeptID, out DataTable m_objTableA, out DataTable m_objTableB, out DataTable p_objTableC, out DataTable p_objTableD)
        {
            m_objTableA = new DataTable();
            m_objTableB = new DataTable();
            p_objTableC = null;
            p_objTableD = null;
            string strSQL = @"select   a.set_id, a.start_date, a.end_date, a.set_name, a.visiblity_level,
         a.employee_id, a.keyword, a.keyword_type, a.keyword_py, a.order_no,
         a.form_id
    from (select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmESWorkStation'
                                                        and control_id =
                                                               'm_txtEndoscopyFindings') b on ts.set_id =
                                                                                          b.templateset_id
           where ts.form_id = 'frmESWorkStation'
             and (   (ts.visiblity_level = 0 and ts.employee_id = ?)
                  or (ts.visiblity_level = 1)
                 )
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%'
          union all
          select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmESWorkStation'
                                                        and control_id =
                                                               'm_txtEndoscopyFindings') b on ts.set_id =
                                                                                          b.templateset_id
                 join templateset_dept tsd on ts.set_id = tsd.set_id
           where ts.form_id = 'frmESWorkStation'
             and ts.visiblity_level = 2
             and tsd.department_id = ?
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%') a join template_path b on a.keyword || '>>' like b.fullpath || '>>%'
                                     order by b.order_no, a.order_no
";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = m_strEmpID;
                m_objDataParaArr[1].Value = m_strDeptID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTableA, m_objDataParaArr);
                strSQL = @"select   a.set_id, a.start_date, a.end_date, a.set_name, a.visiblity_level,
         a.employee_id, a.keyword, a.keyword_type, a.keyword_py, a.order_no,
         a.form_id
    from (select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmESWorkStation'
                                                        and control_id =
                                                               'm_txtEndoscopyDiagnosis') b on ts.set_id =
                                                                                          b.templateset_id
           where ts.form_id = 'frmESWorkStation'
             and (   (ts.visiblity_level = 0 and ts.employee_id = ?)
                  or (ts.visiblity_level = 1)
                 )
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%'
          union all
          select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmESWorkStation'
                                                        and control_id =
                                                               'm_txtEndoscopyDiagnosis') b on ts.set_id =
                                                                                          b.templateset_id
                 join templateset_dept tsd on ts.set_id = tsd.set_id
           where ts.form_id = 'frmESWorkStation'
             and ts.visiblity_level = 2
             and tsd.department_id = ?
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%') a
join template_path b on a.keyword || '>>' like b.fullpath || '>>%'
                                     order by  b.order_no, a.order_no";
                m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = m_strEmpID;
                m_objDataParaArr[1].Value = m_strDeptID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTableB, m_objDataParaArr);



                m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = m_strEmpID;
                m_objDataParaArr[1].Value = m_strDeptID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTableA, m_objDataParaArr);
                strSQL = @"select   a.set_id, a.start_date, a.end_date, a.set_name, a.visiblity_level,
         a.employee_id, a.keyword, a.keyword_type, a.keyword_py, a.order_no,
         a.form_id
    from (select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmESWorkStation'
                                                        and control_id =
                                                               'm_txtPathologyDiagnosis') b on ts.set_id =
                                                                                          b.templateset_id
           where ts.form_id = 'frmESWorkStation'
             and (   (ts.visiblity_level = 0 and ts.employee_id = ?)
                  or (ts.visiblity_level = 1)
                 )
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%'
          union all
          select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmESWorkStation'
                                                        and control_id =
                                                               'm_txtPathologyDiagnosis') b on ts.set_id =
                                                                                          b.templateset_id
                 join templateset_dept tsd on ts.set_id = tsd.set_id
           where ts.form_id = 'frmESWorkStation'
             and ts.visiblity_level = 2
             and tsd.department_id = ?
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%') a
join template_path b on a.keyword || '>>' like b.fullpath || '>>%'
                                     order by  b.order_no, a.order_no";
                m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = m_strEmpID;
                m_objDataParaArr[1].Value = m_strDeptID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_objTableC, m_objDataParaArr);




                m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = m_strEmpID;
                m_objDataParaArr[1].Value = m_strDeptID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTableA, m_objDataParaArr);
                strSQL = @"select   a.set_id, a.start_date, a.end_date, a.set_name, a.visiblity_level,
         a.employee_id, a.keyword, a.keyword_type, a.keyword_py, a.order_no,
         a.form_id
    from (select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmESWorkStation'
                                                        and control_id =
                                                               'm_txtAdvise') b on ts.set_id =
                                                                                          b.templateset_id
           where ts.form_id = 'frmESWorkStation'
             and (   (ts.visiblity_level = 0 and ts.employee_id = ?)
                  or (ts.visiblity_level = 1)
                 )
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%'
          union all
          select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmESWorkStation'
                                                        and control_id =
                                                               'm_txtAdvise') b on ts.set_id =
                                                                                          b.templateset_id
                 join templateset_dept tsd on ts.set_id = tsd.set_id
           where ts.form_id = 'frmESWorkStation'
             and ts.visiblity_level = 2
             and tsd.department_id = ?
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%') a
join template_path b on a.keyword || '>>' like b.fullpath || '>>%'
                                     order by  b.order_no, a.order_no";
                m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = m_strEmpID;
                m_objDataParaArr[1].Value = m_strDeptID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_objTableD, m_objDataParaArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
    }
}
