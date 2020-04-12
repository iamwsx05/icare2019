using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using System.Text;

namespace com.digitalwave.common.ICD10.Midtier
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsAssistantDiagnosisItemTypeInitServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngAddNewRecord(string strXML, ref string strItemTypeID)
        {
            long num;
            StringBuilder builder;
            string str;
            int num2;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            DateTime time;
            strItemTypeID = "";
            num = 0L;
        Label_000B:
            try
            {
                num = new clsHRPTableService().lngGenerateID(4, "AssistantDiagnoseItemTypeID", "ICD10_AssistantDiagnoseItemType", out strItemTypeID);
                if (num <= 0)
                {
                    goto Label_008C;
                }
                builder = new StringBuilder(strXML);
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                num2 = strXML.IndexOf(" ");
                builder.Insert(num2, " AssistantDiagnoseItemTypeID =  '" + strItemTypeID + "' ");
                strXML = builder.ToString();
                num = new clsHRPTableService().add_new_record("ICD10_AssistantDiagnoseItemType", strXML);
            Label_008C:
                goto Label_00B0;
            }
            catch (Exception exception1)
            {
            Label_008F:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00B0;
            }
        Label_00B0:
            num3 = num;
        Label_00B6:
            return num3;
        }

        [AutoComplete]
        public long m_lngDeleteRecord(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            string[] strArray;
            num = 0L;
        Label_0004:
            try
            {
                num = new clsHRPTableService().modify_record("ICD10_AssistantDiagnoseItemType", strXML, new string[] { "AssistantDiagnoseItemTypeID" });
                goto Label_0047;
            }
            catch (Exception exception1)
            {
            Label_002C:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0047;
            }
        Label_0047:
            num2 = num;
        Label_004D:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetAssistantDiagnosisItemType(ref string strXml, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "";
            str = "select AssistantDiagnoseItemTypeID,AssistantDiagnoseItemTypeName from ICD10_AssistantDiagnoseItemType where Status='0' ";
            num = 0L;
        Label_0010:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref strXml, ref intRows);
                goto Label_003F;
            }
            catch (Exception exception1)
            {
            Label_0022:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_003F;
            }
        Label_003F:
            num2 = num;
        Label_0045:
            return num2;
        }

        [AutoComplete]
        public long m_lngModifyRecord(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            string[] strArray;
            num = 0L;
        Label_0004:
            try
            {
                num = new clsHRPTableService().modify_record("ICD10_AssistantDiagnoseItemType", strXML, new string[] { "AssistantDiagnoseItemTypeID" });
                goto Label_0047;
            }
            catch (Exception exception1)
            {
            Label_002C:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0047;
            }
        Label_0047:
            num2 = num;
        Label_004D:
            return num2;
        }

    }
}
