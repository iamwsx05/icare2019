using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using System.Xml;

namespace com.digitalwave.iCare.middletier.ICU
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsEvaluation_QuerySvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngGetTimeInfoOfAPatient(string p_strTableName, string p_strInPatientID, string p_strInPatientDate, string p_strFromDate, string p_strToDate, ref string p_strXml, ref int intRows)
        {
            long lngRes = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {               
                string strCommand = "";
                strCommand = "select distinct activitytime from " + p_strTableName + " where inpatientno =?"
                    + " and status =0 and inpatientdate=?";

                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPSvc.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);
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


        [AutoComplete]
        public long m_lngGetEvaluationData(string p_strTableName, string p_strPatientID, string p_strInPatientDate, string p_strCreateDate, ref string p_strXml, ref int intRows)
        {
            long lngRes = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {                
                string strCommand = "";
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = "select top 1 *  from " + p_strTableName + " a WHERE a.InPatientNO=? AND a.Status =0 and a.ActivityTime=? and a.InPatientDate=? order by ModifyDate desc";
                else
                    strCommand = "select * from(select *  from " + p_strTableName + " a where a.inpatientno=? and a.status =0 and a.activitytime=? and a.inpatientdate=? order by modifydate desc)where rownum = 1";

                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreateDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPSvc.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);
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
