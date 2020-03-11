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
    public class clsICD10InfServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngGetIllnessInf(string p_FindStr, int p_CZType, ref DataTable dtResult)
        {
            string str;
            string str2;
            long num;
            Exception exception;
            string str3;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            int num3;
            str = "";
            str2 = "";
            num = 0L;
        Label_0010:
            try
            {
                if (!string.IsNullOrEmpty(p_FindStr))
                {
                    goto Label_003A;
                }
                str2 = "%";
                goto Label_004D;
            Label_003A:
                str2 = p_FindStr.Replace("'", "''");
            Label_004D:
                num3 = p_CZType;
                switch (num3)
                {
                    case 0:
                        goto Label_0065;

                    case 1:
                        goto Label_0078;

                    case 2:
                        goto Label_008B;
                }
                goto Label_009E;
            Label_0065:
                str = "select id,icd_code,icd_name,icd_path,icd_desc from ticd10 where icd_code like '" + str2 + "' and icd_desc='疾病' order by icd_code";
                goto Label_00A4;
            Label_0078:
                str = "select id,icd_code,icd_name,icd_path,icd_desc from ticd10 where icd_name like '" + str2 + "' and icd_desc='疾病' order by icd_name";
                goto Label_00A4;
            Label_008B:
                str = "select id,icd_code,icd_name,icd_path,icd_desc from ticd10 where icd_py like '" + str2 + "' and icd_desc='疾病' order by icd_py";
                goto Label_00A4;
            Label_009E:
                num2 = -1L;
                goto Label_00D8;
            Label_00A4:
                num = new clsHRPTableService().DoGetDataTable(str, ref dtResult);
                goto Label_00D2;
            }
            catch (Exception exception1)
            {
            Label_00B4:
                exception = exception1;
                str3 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00D2;
            }
        Label_00D2:
            num2 = num;
        Label_00D8:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetIllnessList(string p_FindStr, ref DataTable dtResult)
        {
            string str;
            string str2;
            long num;
            Exception exception;
            string str3;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            str = "";
            str2 = "";
            num = 0L;
        Label_0010:
            try
            {
                if (!string.IsNullOrEmpty(p_FindStr))
                {
                    goto Label_0039;
                }
                num2 = -1L;
                goto Label_01B8;
            Label_0039:
                str2 = p_FindStr + ">>";
                if (p_FindStr.ToUpper() == "KEY")
                {
                    str = "select id,icd_code,icd_name,icd_path,icd_desc from ticd10 where to_char(id)=icd_path";
                }
                num = new clsHRPTableService().DoGetDataTable(str, ref dtResult);
                goto Label_01B2;
            }
            catch (Exception exception1)
            {
            Label_0194:
                exception = exception1;
                str3 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_01B2;
            }
        Label_01B2:
            num2 = num;
        Label_01B8:
            return num2;
        }

        [AutoComplete]
        public long m_lngSaveInPatient_ICD(clsInPatient_ICDInf p_objInPatient_ICDInf, int p_intCz)
        {
            string str;
            long num;
            long num2;
            clsHRPTableService service;
            IDataParameter[] parameterArray;
            IDataParameter[] parameterArray2;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            int num4;
            str = "";
            num = 0L;
            if (p_objInPatient_ICDInf != null)
            {
                goto Label_0020;
            }
            num3 = -1L;
            goto Label_012A;
        Label_0020:
            num2 = 0L;
            service = new clsHRPTableService();
        Label_0029:
            try
            {
                num4 = p_intCz;
                switch (num4)
                {
                    case 0:
                        goto Label_0041;

                    case 1:
                        goto Label_00A6;
                }
                goto Label_00FB;
            Label_0041:
                parameterArray = null;
                service.CreateDatabaseParameter(3, out parameterArray);
                parameterArray[0].Value = p_objInPatient_ICDInf.m_strInPatientID;
                parameterArray[1].Value = (DateTime)p_objInPatient_ICDInf.m_dtmInPatientDate;
                parameterArray[2].Value = p_objInPatient_ICDInf.m_strICD_ID;
                str = "insert into tInPatient_ICD10 (InPatientID,InPatientDate,ICD_ID) values(?,?,?)";
                num2 = new clsHRPTableService().lngExecuteParameterSQL(str, ref num, parameterArray);
                goto Label_0100;
            Label_00A6:
                parameterArray2 = null;
                service.CreateDatabaseParameter(2, out parameterArray2);
                parameterArray2[0].Value = p_objInPatient_ICDInf.m_strInPatientID;
                parameterArray2[1].Value = (DateTime)p_objInPatient_ICDInf.m_dtmInPatientDate;
                str = "delete from tInPatient_ICD10 where InPatientID=? and InPatientDate=? ";
                num2 = new clsHRPTableService().lngExecuteParameterSQL(str, ref num, parameterArray2);
                goto Label_0100;
            Label_00FB:
                num2 = -1L;
            Label_0100:
                goto Label_0124;
            }
            catch (Exception exception1)
            {
            Label_0103:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0124;
            }
        Label_0124:
            num3 = num2;
        Label_012A:
            return num3;
        }

        [AutoComplete]
        public string m_strGetID(int intIDLength, string strIDField, string strIDTable)
        {
            string str;
            clsHRPTableService service;
            long num;
            Exception exception;
            clsLogText text;
            bool flag;
            string str2;
            bool flag2;
            str = string.Empty;
        Label_0007:
            try
            {
                service = new clsHRPTableService();
                if (service.lngGenerateID(intIDLength, strIDField, strIDTable, out str) == 1L)
                {
                    goto Label_002B;
                }
                str = "";
            Label_002B:
                goto Label_0044;
            }
            catch (Exception exception1)
            {
            Label_002E:
                exception = exception1;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0044;
            }
        Label_0044:
            str2 = str;
        Label_004A:
            return str2;
        }
    }
}
