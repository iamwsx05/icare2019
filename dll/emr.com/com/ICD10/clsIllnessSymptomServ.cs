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
    public class clsIllnessSymptomServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngGetICDInf(string p_strSql, ref DataTable dtResult)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            num = 0L;
        Label_0004:
            try
            {
                num = new clsHRPTableService().DoGetDataTable(p_strSql, ref dtResult);
                goto Label_0030;
            }
            catch (Exception exception1)
            {
            Label_0015:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0030;
            }
        Label_0030:
            num2 = num;
        Label_0036:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetIllnessSymptomInf(string p_strSql, ref DataTable dtResult)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            str = "";
            str = p_strSql;
            num = 0L;
        Label_000C:
            try
            {
                if (str.Trim().Length > 0)
                {
                    goto Label_002A;
                }
                str = "select Symptom_ID,IllnessSymptom.Name from IllnessSymptom order by IllnessSymptom.Name";
            Label_002A:
                num = new clsHRPTableService().DoGetDataTable(str, ref dtResult);
                goto Label_0057;
            }
            catch (Exception exception1)
            {
            Label_003A:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0057;
            }
        Label_0057:
            num2 = num;
        Label_005D:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMaxID(string p_strSql, ref DataTable dtResult)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            num = 0L;
        Label_0004:
            try
            {
                num = new clsHRPTableService().DoGetDataTable(p_strSql, ref dtResult);
                goto Label_0030;
            }
            catch (Exception exception1)
            {
            Label_0015:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0030;
            }
        Label_0030:
            num2 = num;
        Label_0036:
            return num2;
        }

        [AutoComplete]
        public long m_lngSaveIllnessSymptom(string p_strSql)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            num = 0L;
        Label_0004:
            try
            {
                num = new clsHRPTableService().DoExcute(p_strSql);
                goto Label_002F;
            }
            catch (Exception exception1)
            {
            Label_0014:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_002F;
            }
        Label_002F:
            num2 = num;
        Label_0035:
            return num2;
        }
    }
}
