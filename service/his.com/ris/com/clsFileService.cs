using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;

using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.RIS
{
    /// <summary>
    /// 医技图像归档服务
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsFileService : clsMiddleTierBase
    {
        /// <summary>
        /// 根据报告ID获取报告所包含的图像
        /// </summary>
        /// <param name="p_strReportID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReportImage(string p_strReportID, string p_strModality, out List<clsRISImageInfo> p_lstImageInfo)
        {
            string strSql = @"select imageuid_vchr,
       reportid_vchr,
       modality_vchr,
       filename_vchr,
       status_int
  from t_ris_image t
 where t.reportid_vchr = ?
   and t.modality_vchr = ?
   and t.status_int = 1";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            p_lstImageInfo = null;
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strReportID;
                objDPArr[1].Value = p_strModality;
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0)
                {
                    p_lstImageInfo = new List<clsRISImageInfo>(dtValue.Rows.Count);
                    clsRISImageInfo objInfo = null;
                    foreach (DataRow row in dtValue.Rows)
                    {
                        objInfo = new clsRISImageInfo();
                        objInfo.m_strImageUID = row["imageuid_vchr"].ToString();
                        objInfo.m_strReportID = row["reportid_vchr"].ToString();
                        objInfo.m_strModality = row["modality_vchr"].ToString();
                        objInfo.m_strFileName = row["filename_vchr"].ToString();
                        objInfo.m_intStatus = Convert.ToInt32(row["status_int"]);
                        p_lstImageInfo.Add(objInfo);
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
        public long m_lngSaveReportImage(clsRISImageInfo p_objImageInfo)
        {
            string strSql = @"select count(imageuid_vchr) from t_ris_image t where t.imageuid_vchr = ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                long lngAff = 0;
                DataTable dtValue = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objImageInfo.m_strImageUID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0 && dtValue.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtValue.Rows[0][0]) > 0)
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
                strSql = @"insert into t_ris_image values(?,?,?,?,1)";
                objHRPSvc.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objImageInfo.m_strImageUID;
                objDPArr[1].Value = p_objImageInfo.m_strReportID;
                objDPArr[2].Value = p_objImageInfo.m_strModality;
                objDPArr[3].Value = p_objImageInfo.m_strFileName;
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
        public long m_lngDeleteImage(string p_strImageUID)
        {
            string strSql = @"update t_ris_image t set status_int = 0 where t.imageuid_vchr = ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                long lngAff = 0;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strImageUID;
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
        public long m_lngGetImageCount(DateTime p_dtmStart, DateTime p_dtmEnd, out int p_intCount)
        {
            string strSql = @"select count(t1.recordid)
  from ar_image t1, ar_apply_report t2
 where t1.recordid = t2.recordid
   and t2.ceateddate between ? and ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            p_intCount = 0;
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_dtmStart;
                objDPArr[1].Value = p_dtmEnd;
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0)
                {
                    p_intCount = Convert.ToInt32(dtValue.Rows[0][0]);                    
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
        public long m_lngGetPACSImageList(DateTime p_dtmStart, DateTime p_dtmEnd, out List<string> p_lstData)
        {
            string strSql = @"select t1.instanceuid
  from images t1, studies t2, series t3, patients t4
 where t1.seriesuid = t3.seriesuid
   and t2.studyuid = t3.studyuid
   and t2.patientid = t4.patientid
   and t2.studydate between ? and ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytPACS;
            long lngRes = 0;
            p_lstData = null;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_dtmStart;
                objDPArr[1].Value = p_dtmEnd;
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0)
                {
                    p_lstData = new List<string>(dtValue.Rows.Count);                    
                    foreach(DataRow row in dtValue.Rows)
                    {
                        p_lstData.Add(row[0].ToString());
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
        public long m_lngGetExportImage(DateTime p_dtmStart, DateTime p_dtmEnd, out List<clsExportData> p_lstData)
        {
            p_lstData = null;
            string strSql = @"select t1.imagecontent, t2.ceateddate, t2.patientid, t2.recordid
  from ar_image t1, ar_apply_report t2
 where t1.recordid = t2.recordid
   and t2.ceateddate between ? and ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            p_lstData = null;
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_dtmStart;
                objDPArr[1].Value = p_dtmEnd;
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0)
                {
                    p_lstData = new List<clsExportData>(dtValue.Rows.Count);
                    clsExportData objData = null;
                    foreach (DataRow row in dtValue.Rows)
                    {
                        objData = new clsExportData();
                        objData.m_objImage = (byte[])row["imagecontent"];
                        objData.m_dtmCreate = Convert.ToDateTime(row["ceateddate"]);
                        objData.m_strPatientID = row["patientid"].ToString();
                        objData.m_strReportID = row["recordid"].ToString();
                        p_lstData.Add(objData);
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
        public long m_lngGetPACSImage(string p_strImageUid, out clsDicomData p_objData)
        {
            string strSql = @"select t1.instanceuid,   
       t2.studyuid,
       t2.studydate,
       t3.seriesuid,
       t3.modality,
       t4.patientid
  from images t1, studies t2, series t3, patients t4
 where t1.seriesuid = t3.seriesuid
   and t2.studyuid = t3.studyuid
   and t2.patientid = t4.patientid
   and t1.instanceuid = ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytPACS;
            p_objData = null;
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strImageUid;
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0 && dtValue.Rows.Count > 0)
                {
                    DataRow row = dtValue.Rows[0];
                    p_objData = new clsDicomData();
                    p_objData.m_strImageUid = row["instanceuid"].ToString();
                    //p_objData.m_objImage = row["image"];                    
                    p_objData.m_strStudyUid = row["studyuid"].ToString();
                    p_objData.m_dtmCreate = Convert.ToDateTime(row["studydate"]);
                    p_objData.m_strModality = row["modality"].ToString();
                    p_objData.m_strSeriesUid = row["seriesuid"].ToString();
                    p_objData.m_strPatientID = row["patientid"].ToString();
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
