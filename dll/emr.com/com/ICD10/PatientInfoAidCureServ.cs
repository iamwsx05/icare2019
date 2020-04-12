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
    public class PatientInfoAidCureServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngGetICD10_AssistantDiagnose(string p_strIncoming, out string p_strXML, out int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = null;
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strIncoming))
            {
                goto Label_0027;
            }
            num2 = -1L;
            goto Label_006B;
        Label_0027:
            num = 0L;
        Label_002A:
            try
            {
                str = "SELECT * FROM V_ICD10_All_Info AS V1, ICD10_AssistantDiagnose AS T1 WHERE T1.Status = '0' AND V1.UnionICD10ID = T1.ICD10ID " + p_strIncoming;
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0065;
            }
            catch (Exception exception1)
            {
            Label_0048:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0065;
            }
        Label_0065:
            num2 = num;
        Label_006B:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetICD10_AssistantDiagnoseAlwaysSicknessResult(string[] p_strAlwaysICD10IDArr, out string p_strXML, out int p_intRows)
        {
            long num;
            StringBuilder builder;
            string str;
            int num2;
            string str2;
            Exception exception;
            string str3;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            p_strXML = null;
            p_intRows = 0;
            if (p_strAlwaysICD10IDArr != null && p_strAlwaysICD10IDArr.Length > 0)
            {
                goto Label_0022;
            }
            num3 = -1L;
            goto Label_00CD;
        Label_0022:
            num = 0L;
        Label_0025:
            try
            {
                builder = new StringBuilder(" AND T1.AlwaysICD10ID IN (");
                str = "";
                num2 = 0;
                goto Label_005A;
            Label_003B:
                builder.Append("'" + p_strAlwaysICD10IDArr[num2] + "',");
                num2 += 1;
            Label_005A:
                if (num2 < ((int)p_strAlwaysICD10IDArr.Length))
                {
                    goto Label_003B;
                }
                str = builder.ToString();
                str = str.Substring(0, str.Length - 1) + ")";
                str2 = "SELECT DISTINCT UnionICD10ID, UnionICD10Name FROM V_ICD10_All_Info AS V1, ICD10_AssistantDiagnoseAlwaysSickness AS T1 WHERE T1.Status = '0' AND V1.UnionICD10ID = T1.ICD10ID" + str;
                num = new clsHRPTableService().lngGetXMLTable(str2, ref p_strXML, ref p_intRows);
                goto Label_00C7;
            }
            catch (Exception exception1)
            {
            Label_00A6:
                exception = exception1;
                str3 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00C7;
            }
        Label_00C7:
            num3 = num;
        Label_00CD:
            return num3;
        }

        [AutoComplete]
        public long m_lngGetICD10_AssistantDiagnoseCheckCodeResult(string[] p_strCheckCodeIDArr, out string p_strXML, out int p_intRows)
        {
            long num;
            StringBuilder builder;
            string str;
            int num2;
            string str2;
            Exception exception;
            string str3;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            p_strXML = null;
            p_intRows = 0;
            if (p_strCheckCodeIDArr != null && p_strCheckCodeIDArr.Length > 0)
            {
                goto Label_0022;
            }
            num3 = -1L;
            goto Label_00CD;
        Label_0022:
            num = 0L;
        Label_0025:
            try
            {
                builder = new StringBuilder(" AND T1.CheckCodeID IN (");
                str = "";
                num2 = 0;
                goto Label_005A;
            Label_003B:
                builder.Append("'" + p_strCheckCodeIDArr[num2] + "',");
                num2 += 1;
            Label_005A:
                if (num2 < ((int)p_strCheckCodeIDArr.Length))
                {
                    goto Label_003B;
                }
                str = builder.ToString();
                str = str.Substring(0, str.Length - 1) + ")";
                str2 = "SELECT DISTINCT UnionICD10ID, UnionICD10Name FROM ICD10_AssistantDiagnoseCheckCode AS T1, V_ICD10_All_Info AS V1 WHERE  T1.Status = '0' AND T1.ICD10ID = V1.UnionICD10ID " + str;
                num = new clsHRPTableService().lngGetXMLTable(str2, ref p_strXML, ref p_intRows);
                goto Label_00C7;
            }
            catch (Exception exception1)
            {
            Label_00A6:
                exception = exception1;
                str3 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00C7;
            }
        Label_00C7:
            num3 = num;
        Label_00CD:
            return num3;
        }

        [AutoComplete]
        public long m_lngGetICD10_AssistantDiagnoseClinicPutUpResult(string p_strPutUpItemName, out string p_strXML, out int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = null;
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strPutUpItemName))
            {
                goto Label_0027;
            }
            num2 = -1L;
            goto Label_0070;
        Label_0027:
            num = 0L;
        Label_002A:
            try
            {
                str = "SELECT DISTINCT UnionICD10ID, UnionICD10Name FROM V_ICD10_All_Info AS V1, ICD10_AssistantDiagnoseClinicPutUp AS T1 WHERE T1.Status = '0' AND V1.UnionICD10ID = T1.ICD10ID AND T1.PutUpItemName Like '%" + p_strPutUpItemName + "%'";
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_006A;
            }
            catch (Exception exception1)
            {
            Label_004D:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006A;
            }
        Label_006A:
            num2 = num;
        Label_0070:
            return num2;
        }
    }
}
