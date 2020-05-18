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
    public class clsCheckPageServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngLikeSeachCheckItemArr(string strSearchingString, string strSearchConditionType, string strCheckFlag, ref string strSetXML, ref int introws)
        {
            string str;
            long num;
            string str2;
            string str3;
            Exception exception;
            string str4;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string str5;
            int num3;
            string[] strArray;

            if (!string.IsNullOrEmpty(strSearchingString) || !string.IsNullOrEmpty(strSearchConditionType) || !string.IsNullOrEmpty(strCheckFlag))
            {
                goto Label_003A;
            }
            num2 = -1L;
            goto Label_0149;
        Label_003A:
            str = "";
            num = 0L;
        Label_0043:
            try
            {
                str5 = strSearchConditionType;
                if (str5 == null)
                {
                    goto Label_00A5;
                }
                if (str5 == "按编号查找")
                {
                    goto Label_0085;
                }
                if (str5 == "按名称查找")
                {
                    goto Label_008D;
                }
                if (str5 == "按拼音码查找")
                {
                    goto Label_0095;
                }
                if (str5 == "按英文名查找")
                {
                    goto Label_009D;
                }
                goto Label_00A5;
            Label_0085:
                str = "m.CheckCodeID";
                goto Label_00AD;
            Label_008D:
                str = "m.CheckName";
                goto Label_00AD;
            Label_0095:
                str = "m.PYCode";
                goto Label_00AD;
            Label_009D:
                str = "m.CheckNameForEnglish";
                goto Label_00AD;
            Label_00A5:
                str = "m.CheckCode";
            Label_00AD:
                str2 = "";
            Label_00B3:
                try
                {
                    str2 = int.Parse(strCheckFlag).ToString("0000000");
                    goto Label_00D4;
                }
                catch
                {
                Label_00CC:
                    num2 = -1L;
                    goto Label_0149;
                }
            Label_00D4:;
                str3 = "Select m.CheckCodeID,m.CheckName,m.PYCode,m.CheckNameForEnglish,p.RetailPrice,p.Unit FROM CheckCodeForLab m LEFT OUTER JOIN  PriceMasterTable p ON m.PaymentNO = p.ItemID AND p.ItemFlag = 'DM' AND  p.Status = 0 where  m.Status=0 AND " + str + " like '" + strSearchingString.Trim() + "%'";
                num = new clsHRPTableService().lngGetXMLLikeQuery(str3, ref strSetXML, ref introws);
                goto Label_0143;
            }
            catch (Exception exception1)
            {
            Label_0122:
                exception = exception1;
                str4 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0143;
            }
        Label_0143:
            num2 = num;
        Label_0149:
            return num2;
        }

        [AutoComplete]
        public long m_lngXMLQueryCheckItemsFromIDArr(string[] p_strCheckCodeIDArr, string p_strCheckFlag, ref string strSetXML, ref int introws)
        {
            long num;
            string str;
            string str2;
            int num2;
            Exception exception;
            string str3;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            int num4;
            if (p_strCheckCodeIDArr != null && p_strCheckCodeIDArr.Length > 0)
            {
                goto Label_001F;
            }
            num3 = -1L;
            goto Label_00CE;
        Label_001F:
            num = 0L;
        Label_0022:
            try
            {
                str = "";
            Label_0029:
                try
                {
                    str = int.Parse(p_strCheckFlag).ToString("0000000");
                    goto Label_004D;
                }
                catch
                {
                Label_0042:
                    num3 = -1L;
                    goto Label_00CE;
                }
            Label_004D:
                str2 = "Select m.CheckCodeID,m.CheckName,m.PYCode,m.CheckNameForEnglish,p.RetailPrice,p.Unit FROM CheckCodeForLab m LEFT OUTER JOIN  PriceMasterTable p ON m.PaymentNO = p.ItemID AND p.ItemFlag = 'DM' AND  p.Status = 0 where m.Status=0 AND m.CheckCodeID = '" + p_strCheckCodeIDArr[0] + "'  ";
                num2 = 1;
                goto Label_007D;
            Label_0065:
                str2 = str2 + "  OR m.CheckCodeID = '" + p_strCheckCodeIDArr[num2] + "'  ";
                num2 += 1;
            Label_007D:
                if (num2 < ((int)p_strCheckCodeIDArr.Length))
                {
                    goto Label_0065;
                }
                str2 = str2 + " ORDER BY m.CheckCodeID";
                num = new clsHRPTableService().lngGetXMLTable(str2, ref strSetXML, ref introws);
                goto Label_00C8;
            }
            catch (Exception exception1)
            {
            Label_00A7:
                exception = exception1;
                str3 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00C8;
            }
        Label_00C8:
            num3 = num;
        Label_00CE:
            return num3;
        }

        [AutoComplete]
        public long m_lngXMLQueryMedicalFromIDArr(string[] p_strCheckCodeIDArr, ref string strSetXML, ref int introws)
        {
            long num;
            string str;
            int num2;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            if (p_strCheckCodeIDArr != null && p_strCheckCodeIDArr.Length > 0)
            {
                goto Label_001F;
            }
            num3 = -1L;
            goto Label_009F;
        Label_001F:
            num = 0L;
        Label_0022:
            try
            {
                str = "Select * from MedicalNOForLab where Status=0 and  CheckCode = '" + p_strCheckCodeIDArr[0] + "'  ";
                num2 = 1;
                goto Label_0052;
            Label_003A:
                str = str + "  OR CheckCode = '" + p_strCheckCodeIDArr[num2] + "'  ";
                num2 += 1;
            Label_0052:
                if (num2 < ((int)p_strCheckCodeIDArr.Length))
                {
                    goto Label_003A;
                }
                str = str + " ORDER BY CheckCode";
                num = new clsHRPTableService().lngGetXMLTable(str, ref strSetXML, ref introws);
                goto Label_0099;
            }
            catch (Exception exception1)
            {
            Label_007B:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0099;
            }
        Label_0099:
            num3 = num;
        Label_009F:
            return num3;
        }
    }
}
