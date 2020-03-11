using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using System.Xml;

namespace com.digitalwave.iCare.middletier.ICU
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEvaluation_UpdataSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        [AutoComplete]
        public long m_lngAddNew(string p_strTableName, string p_strMainXml)
        {
            long lngRes = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {                
                if (p_strMainXml == null || p_strMainXml == "")
                    return -1;

                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int intIndex = p_strMainXml.IndexOf(' ');

                p_strMainXml = p_strMainXml.Insert(intIndex, " ModifyDate=\"" + strCurrentTime + "\"");

                lngRes = objHRPSvc.add_new_record(p_strTableName, p_strMainXml);
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
        public long m_lngDeActiveRecord(string p_strTableName, string strXml)
        {
            long lngRes = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int intIndex = strXml.IndexOf(' ');

                strXml = strXml.Insert(intIndex, " DeactivatedDate=\"" + strCurrentTime + "\"");


                lngRes = objHRPSvc.modify_record(p_strTableName, strXml, "inpatientno", "inpatientdate", "activitytime");
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
        public long lngAddNewRecordOfAutoEval(string p_strTableName, string p_strFiledXml)
        {
            long lngRes = -1;
            clsHRPTableService hs = new clsHRPTableService();
            try
            {                
                StringBuilder tem = new StringBuilder(p_strFiledXml);
                int index = p_strFiledXml.IndexOf(" ");

                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tem.Insert(index, " Status=0");
                p_strFiledXml = tem.ToString();

                lngRes = new clsHRPTableService().add_new_record(p_strTableName, p_strFiledXml);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                hs.Dispose();
            }
            return lngRes;
        }
    }
}
