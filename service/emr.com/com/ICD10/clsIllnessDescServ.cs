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
    public class clsIllnessDescServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngSaveIllnessSymptomRelation(string p_strSaveSql)
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
                num = new clsHRPTableService().DoExcute(p_strSaveSql);
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
