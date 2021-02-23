using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using System.Reflection;

using com.digitalwave.iCare.middletier;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using iCare.RIS.Utility;

namespace com.digitalwave.iCare.middletier.RIS
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsES_ReportModifyService : clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngAddNew(clsEndoscopyReport p_objReport, out string p_strReportID)
        {
            string strSql = @"insert into t_ris_es_report values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 0;
            p_strReportID = string.Empty;
            if (string.IsNullOrEmpty(p_objReport.m_strReportID))
            {
                lngRes = m_lngGetNewReportID(ref p_strReportID);
                p_objReport.m_strReportID = p_strReportID;
                if (lngRes <= 0)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("生成Report失败");
                    return lngRes;
                }
            }
            try
            {
                IDataParameter[] objDPArr = null;
                long lngAff = 0;
                objHRPSvc.CreateDatabaseParameter(39, out objDPArr);
                objDPArr[0].Value = p_objReport.m_strReportID.Trim();
                objDPArr[1].Value = p_objReport.m_strOtherID.Trim();
                objDPArr[2].Value = p_objReport.m_dtmCheckDate;
                objDPArr[3].Value = p_objReport.m_strCheckRoom;
                objDPArr[4].Value = p_objReport.m_strApplyID.Trim();
                objDPArr[5].Value = p_objReport.m_strDepName;
                objDPArr[6].Value = p_objReport.m_strDeptID.Trim();
                objDPArr[7].Value = p_objReport.m_strApplyDoctor;
                objDPArr[8].Value = p_objReport.m_strApplyDoctorID;
                objDPArr[9].Value = p_objReport.m_dtmApplyDate;
                objDPArr[10].Value = p_objReport.m_strApplyPurpose;
                objDPArr[11].Value = p_objReport.m_strSourceType;
                objDPArr[12].Value = p_objReport.m_strPatientCardID.Trim();
                objDPArr[13].Value = p_objReport.m_strPatientID.Trim();
                objDPArr[14].Value = p_objReport.m_strInPatientID.Trim();
                objDPArr[15].Value = p_objReport.m_strInhospitalNumber;
                objDPArr[16].Value = p_objReport.m_strBedNo;
                objDPArr[17].Value = p_objReport.m_strPatientName;
                objDPArr[18].Value = p_objReport.m_strSex;
                objDPArr[19].Value = p_objReport.m_strAge;
                objDPArr[20].Value = p_objReport.m_strAddress;
                objDPArr[21].Value = p_objReport.m_strTelephone;
                objDPArr[22].Value = p_objReport.m_strCheckPart;
                objDPArr[23].Value = p_objReport.m_strPreviousHistory;
                objDPArr[24].Value = p_objReport.m_strClinicalDiano;
                objDPArr[25].Value = p_objReport.m_strOperationFindings;
                objDPArr[26].Value = p_objReport.m_strRemark;
                objDPArr[27].Value = p_objReport.m_strChargeDetail;
                objDPArr[28].Value = p_objReport.m_strEndoscopyFindings;
                objDPArr[29].Value = p_objReport.m_strEndoscopyDiagnose;                              
                objDPArr[30].Value = p_objReport.m_strReportDoctorID.Trim();
                objDPArr[31].Value = p_objReport.m_strReportDoctor;
                objDPArr[32].Value = p_objReport.m_dtmReportDate;
                objDPArr[33].Value = 0;
                objDPArr[34].Value = p_objReport.m_strArea;
                objDPArr[35].Value = p_objReport.m_strAdvise;
                objDPArr[36].Value = p_objReport.m_strHP;
                objDPArr[37].Value = p_objReport.m_strPathologyID;
                objDPArr[38].Value = p_objReport.m_strPathologyDiagnosis;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);

                strSql = "update ar_common_apply set status_int = 1 where applyid = ?";
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objReport.m_strApplyID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);

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
        public long m_lngGetNewReportID(ref string p_strReportID)
        {
            string strSql = @"select seq_es_report.nextval from dual";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 0;
            p_strReportID = string.Empty;
            try
            {
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSql, ref dtValue);
                int id = 1;
                if (lngRes > 0 && dtValue.Rows.Count > 0)
                {
                    if (dtValue.Rows[0][0] != DBNull.Value)
                    {
                        id = Convert.ToInt32(dtValue.Rows[0][0]) + 1;
                    }
                }
                string strId = id.ToString();
                p_strReportID += strId;

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
        public long m_lngModify(clsEndoscopyReport p_objReport)
        {
            string strSql = @"update t_ris_es_report t
   set t.otherid_chr            = ?,
       t.checkdate_dat          = ?,
       t.checkroom_chr          = ?,
       t.applyid_chr            = ?,
       t.applydepartment_chr    = ?,
       t.applydeptid_chr        = ?,
       t.applydoctor_chr        = ?,
       t.applydoctorid_chr      = ?,
       t.applydate_dat          = ?,
       t.applypurpose_chr       = ?,
       t.sourcetype_chr         = ?,
       t.patientcardid_chr      = ?,
       t.patientid_chr          = ?,
       t.inpatientid_chr        = ?,
       t.inhospitalnumber_chr   = ?,
       t.bedno_chr              = ?,
       t.patientname_chr        = ?,
       t.gender_chr             = ?,
       t.age_chr                = ?,
       t.address_chr            = ?,
       t.telephone_chr          = ?,
       t.checkpart_chr          = ?,
       t.previoushistory_chr    = ?,
       t.clinicaldiagnosis_chr  = ?,
       t.operationfindings_chr  = ?,
       t.remark_chr             = ?,
       t.chargedetail_chr       = ?,
       t.endoscopyfindings_chr  = ?,
       t.endoscopydiagnose_chr  = ?,
       t.reportdoctorid_chr     = ?,
       t.reportdoctor_chr       = ?,
       t.reportdate_dat         = ?,
       t.status_int             = ?,
       t.area_chr               = ?,
       t.advise_chr             = ?,
       t.hp_chr                 = ?,
       t.pathologyid_chr        = ?,
       t.pathologydiagnosis_chr = ?
 where t.reportid_chr = ?

";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                long lngAff = 0;
                objHRPSvc.CreateDatabaseParameter(39, out objDPArr);
                objDPArr[0].Value = p_objReport.m_strOtherID;
                objDPArr[1].Value = p_objReport.m_dtmCheckDate;
                objDPArr[2].Value = p_objReport.m_strCheckRoom;
                objDPArr[3].Value = p_objReport.m_strApplyID;
                objDPArr[4].Value = p_objReport.m_strDepName;
                objDPArr[5].Value = p_objReport.m_strDeptID;
                objDPArr[6].Value = p_objReport.m_strApplyDoctor;
                objDPArr[7].Value = p_objReport.m_strApplyDoctorID;
                objDPArr[8].Value = p_objReport.m_dtmApplyDate;
                objDPArr[9].Value = p_objReport.m_strApplyPurpose;
                objDPArr[10].Value = p_objReport.m_strSourceType;
                objDPArr[11].Value = p_objReport.m_strPatientCardID;
                objDPArr[12].Value = p_objReport.m_strPatientID;
                objDPArr[13].Value = p_objReport.m_strInPatientID;
                objDPArr[14].Value = p_objReport.m_strInhospitalNumber;
                objDPArr[15].Value = p_objReport.m_strBedNo;
                objDPArr[16].Value = p_objReport.m_strPatientName;
                objDPArr[17].Value = p_objReport.m_strSex;
                objDPArr[18].Value = p_objReport.m_strAge;
                objDPArr[19].Value = p_objReport.m_strAddress;
                objDPArr[20].Value = p_objReport.m_strTelephone;
                objDPArr[21].Value = p_objReport.m_strCheckPart;
                objDPArr[22].Value = p_objReport.m_strPreviousHistory;
                objDPArr[23].Value = p_objReport.m_strClinicalDiano;
                objDPArr[24].Value = p_objReport.m_strOperationFindings;
                objDPArr[25].Value = p_objReport.m_strRemark;
                objDPArr[26].Value = p_objReport.m_strChargeDetail;
                objDPArr[27].Value = p_objReport.m_strEndoscopyFindings;
                objDPArr[28].Value = p_objReport.m_strEndoscopyDiagnose;
                objDPArr[29].Value = p_objReport.m_strReportDoctorID;
                objDPArr[30].Value = p_objReport.m_strReportDoctor;
                objDPArr[31].Value = p_objReport.m_dtmReportDate;
                objDPArr[32].Value = p_objReport.m_intStatus;
                objDPArr[33].Value = p_objReport.m_strArea;
                objDPArr[34].Value = p_objReport.m_strAdvise;
                objDPArr[35].Value = p_objReport.m_strHP;
                objDPArr[36].Value = p_objReport.m_strPathologyID;
                objDPArr[37].Value = p_objReport.m_strPathologyDiagnosis;
                objDPArr[38].Value = p_objReport.m_strReportID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                if (lngRes > 0)
                {
                    lngRes = lngAff;
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

        /// <summary>
        /// 删除报告
        /// </summary>
        /// <param name="p_strReportID">报告ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelete(string p_strReportID)
        {
            string strSql = @"update  t_ris_es_report t set t.status_int = -1
 where t.reportid_chr = ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                long lngAff = 0;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strReportID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                if (lngRes > 0)
                {
                    lngRes = lngAff;
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
        public long m_lngCommit(string p_strReportID)
        {
            string strSql = @"update  t_ris_es_report t set t.status_int = 1
 where t.reportid_chr = ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                long lngAff = 0;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strReportID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                if (lngRes > 0)
                {
                    lngRes = lngAff;
                    strSql = @"update ar_common_apply
   set status_int = 2
 where applyid = (select t.applyid_chr
                    from t_ris_es_report t
                   where t.reportid_chr = ?)";
                    objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = p_strReportID;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
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
    }
}
