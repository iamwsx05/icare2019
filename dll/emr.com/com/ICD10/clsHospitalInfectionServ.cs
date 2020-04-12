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
    public class clsHospitalInfectionServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public bool m_blnIsExitExplan(string p_strId)
        {
            string str;
            clsHRPTableService service;
            long num;
            IDataParameter[] parameterArray;
            DataTable table;
            Exception exception;
            bool flag;
            bool flag2;
            if (!string.IsNullOrEmpty(p_strId))
            {
                goto Label_0020;
            }
            flag = false;
            goto Label_009A;
        Label_0020:
            str = "select infection_id from hospitalinfectionexplain where infection_id = ?";
            service = new clsHRPTableService();
            num = 0L;
        Label_002F:
            try
            {
                parameterArray = null;
                service.CreateDatabaseParameter(1, out parameterArray);
                parameterArray[0].Value = p_strId;
                table = new DataTable();
                if (service.lngGetDataTableWithParameters(str, ref table, parameterArray) > 0 && table.Rows.Count > 0)
                {
                    flag = true;
                    goto Label_009A;
                }
                else
                {
                    goto Label_007E;
                }
            Label_007E:
                goto Label_0094;
            }
            catch (Exception exception1)
            {
            Label_0081:
                exception = exception1;
                new clsLogText().LogError(exception);
                goto Label_0094;
            }
        Label_0094:
            flag = false;
        Label_009A:
            return flag;
        }

        [AutoComplete]
        public bool m_blnIsManager(string p_strEmpId)
        {
            string str;
            clsHRPTableService service;
            long num;
            IDataParameter[] parameterArray;
            DataTable table;
            Exception exception;
            bool flag;
            bool flag2;
            if (!string.IsNullOrEmpty(p_strEmpId))
            {
                goto Label_0018;
            }
            else
            {
                flag = false;
                goto Label_00A0;
            }
        Label_0018:
            str = "select a.mapid_chr from t_sys_role t,t_sys_emprolemap a where t.roleid_chr = a.roleid_chr and a.empid_chr = ? and t.name_vchr=?";
            service = new clsHRPTableService();
            num = 0L;
        Label_0027:
            try
            {
                parameterArray = null;
                service.CreateDatabaseParameter(2, out parameterArray);
                parameterArray[0].Value = p_strEmpId;
                parameterArray[1].Value = "知识库维护";
                table = new DataTable();
                if (service.lngGetDataTableWithParameters(str, ref table, parameterArray) > 0 && table.Rows.Count > 0)
                {
                    flag = true;
                    goto Label_00A0;
                }
                else
                {
                    goto Label_0084;
                }
            Label_0084:
                goto Label_009A;
            }
            catch (Exception exception1)
            {
            Label_0087:
                exception = exception1;
                new clsLogText().LogError(exception);
                goto Label_009A;
            }
        Label_009A:
            flag = false;
        Label_00A0:
            return flag;
        }

        [AutoComplete]
        public long m_lngAddHospitalInfection(string[] p_strStandardValues, string[] p_strExplanValues)
        {
            long num;
            int num2;
            Exception exception;
            long num3;
            bool flag;
            num = 0L;
            num2 = -1;
        Label_0006:
            try
            {
                num = this.m_lngAddHospitalInfectionStandard(p_strStandardValues, out num2);
                if (num <= 0)
                {
                    goto Label_0028;
                }
                num = this.m_lngAddHospitalInfectionExplan(p_strExplanValues, num2);
            Label_0028:
                goto Label_003C;
            }
            catch (Exception exception1)
            {
            Label_002B:
                exception = exception1;
                new clsLogText().LogError(exception);
                goto Label_003C;
            }
        Label_003C:
            num3 = num;
        Label_0041:
            return num3;
        }

        [AutoComplete]
        public long m_lngAddHospitalInfectionExplan(string[] p_strValues, int p_intId)
        {
            string str;
            clsHRPTableService service;
            long num;
            IDataParameter[] parameterArray;
            long num2;
            Exception exception;
            long num3;
            bool flag;
            if (p_strValues != null && p_strValues.Length >= 3)
            {
                goto Label_0020;
            }
            num3 = -1L;
            goto Label_009A;
        Label_0020:
            str = "insert into hospitalinfectionexplain (infection_id, clinic_diagnoses, pathogeny_diagnoses, explain_vchr) values (?, ?, ?, ?)";
            service = new clsHRPTableService();
            num = 0L;
        Label_002F:
            try
            {
                parameterArray = null;
                service.CreateDatabaseParameter(4, out parameterArray);
                parameterArray[0].Value = (int)p_intId;
                parameterArray[1].Value = p_strValues[0];
                parameterArray[2].Value = p_strValues[1];
                parameterArray[3].Value = p_strValues[2];
                num2 = 0L;
                num = service.lngExecuteParameterSQL(str, ref num2, parameterArray);
                goto Label_0094;
            }
            catch (Exception exception1)
            {
            Label_0081:
                exception = exception1;
                new clsLogText().LogError(exception);
                goto Label_0094;
            }
        Label_0094:
            num3 = num;
        Label_009A:
            return num3;
        }

        [AutoComplete]
        private long m_lngAddHospitalInfectionStandard(string[] p_strStandardValues, out int p_intId)
        {
            string str;
            clsHRPTableService service;
            long num;
            int num2;
            IDataParameter[] parameterArray;
            long num3;
            Exception exception;
            long num4;
            bool flag;
            p_intId = -1;
            if (p_strStandardValues != null && p_strStandardValues.Length >= 4)
            {
                goto Label_001F;
            }
            num4 = -1L;
            goto Label_00DB;
        Label_001F:
            str = "  insert into hospitalinfectionstandard (infection_id, infection_name, infection_path, infection_desc, parentid_int) values (?, ?, ?, ?, ?)";
            service = new clsHRPTableService();
            num = 0L;
        Label_002E:
            try
            {
                num2 = -1;
                num = service.m_lngGenerateNewID("hospitalinfectionstandard", "infection_id", out num2);
                if (num > 0 && num2 != -1)
                {
                    goto Label_005B;
                }
                num2 = 0;
            Label_005B:
                p_intId = num2;
                parameterArray = null;
                service.CreateDatabaseParameter(5, out parameterArray);
                parameterArray[0].Value = (int)num2;
                parameterArray[1].Value = p_strStandardValues[0];
                parameterArray[2].Value = p_strStandardValues[1];
                parameterArray[3].Value = p_strStandardValues[2];
                parameterArray[4].Value = p_strStandardValues[3];
                num3 = 0L;
                num = service.lngExecuteParameterSQL(str, ref num3, parameterArray);
                goto Label_00D5;
            }
            catch (Exception exception1)
            {
            Label_00C2:
                exception = exception1;
                new clsLogText().LogError(exception);
                goto Label_00D5;
            }
        Label_00D5:
            num4 = num;
        Label_00DB:
            return num4;
        }

        [AutoComplete]
        public long m_lngDeleteHospitalInfection(string p_strId)
        {
            string str;
            clsHRPTableService service;
            long num;
            IDataParameter[] parameterArray;
            long num2;
            IDataParameter[] parameterArray2;
            Exception exception;
            long num3;
            bool flag;
            if (!string.IsNullOrEmpty(p_strId))
            {
                goto Label_0024;
            }
            num3 = -1L;
            goto Label_00B3;
        Label_0024:
            str = "delete from hospitalinfectionstandard where infection_id = ?";
            service = new clsHRPTableService();
            num = 0L;
        Label_0033:
            try
            {
                parameterArray = null;
                service.CreateDatabaseParameter(1, out parameterArray);
                parameterArray[0].Value = p_strId;
                num2 = 0L;
                num = service.lngExecuteParameterSQL(str, ref num2, parameterArray);
                if (num <= 0)
                {
                    goto Label_0097;
                }
                str = "delete from hospitalinfectionexplain where infection_id = ?";
                parameterArray2 = null;
                service.CreateDatabaseParameter(1, out parameterArray2);
                parameterArray2[0].Value = p_strId;
                num2 = 0L;
                num = service.lngExecuteParameterSQL(str, ref num2, parameterArray2);
            Label_0097:
                goto Label_00AD;
            }
            catch (Exception exception1)
            {
            Label_009A:
                exception = exception1;
                new clsLogText().LogError(exception);
                goto Label_00AD;
            }
        Label_00AD:
            num3 = num;
        Label_00B3:
            return num3;
        }

        [AutoComplete]
        public long m_lngModifyHospitalInfection(string[] p_strStandardValues, string[] p_strExplanValues)
        {
            long num;
            Exception exception;
            long num2;
            num = 0L;
        Label_0004:
            try
            {
                num = this.m_lngModifyHospitalInfectionStandard(p_strStandardValues);
                num = this.m_lngModifyHospitalInfectionExplan(p_strExplanValues);
                goto Label_0029;
            }
            catch (Exception exception1)
            {
            Label_0018:
                exception = exception1;
                new clsLogText().LogError(exception);
                goto Label_0029;
            }
        Label_0029:
            num2 = num;
        Label_002E:
            return num2;
        }

        [AutoComplete]
        private long m_lngModifyHospitalInfectionExplan(string[] p_strValues)
        {
            string str;
            clsHRPTableService service;
            long num;
            IDataParameter[] parameterArray;
            long num2;
            Exception exception;
            long num3;
            bool flag;
            if (p_strValues != null && p_strValues.Length >= 4)
            {
                goto Label_0019;
            }
            num3 = -1L;
            goto Label_0090;
        Label_0019:
            str = " update hospitalinfectionexplain set clinic_diagnoses = ?, PATHOGENY_DIAGNOSES = ?, EXPLAIN_VCHR = ? where infection_id = ?";
            service = new clsHRPTableService();
            num = 0L;
        Label_0028:
            try
            {
                parameterArray = null;
                service.CreateDatabaseParameter(4, out parameterArray);
                parameterArray[0].Value = p_strValues[1];
                parameterArray[1].Value = p_strValues[2];
                parameterArray[2].Value = p_strValues[3];
                parameterArray[3].Value = p_strValues[0];
                num2 = 0L;
                num = service.lngExecuteParameterSQL(str, ref num2, parameterArray);
                goto Label_008A;
            }
            catch (Exception exception1)
            {
            Label_0077:
                exception = exception1;
                new clsLogText().LogError(exception);
                goto Label_008A;
            }
        Label_008A:
            num3 = num;
        Label_0090:
            return num3;
        }

        [AutoComplete]
        private long m_lngModifyHospitalInfectionStandard(string[] p_strStandardValues)
        {
            string str;
            clsHRPTableService service;
            long num;
            IDataParameter[] parameterArray;
            long num2;
            Exception exception;
            long num3;
            bool flag;
            if (p_strStandardValues != null && p_strStandardValues.Length >= 2)
            {
                goto Label_0019;
            }
            num3 = -1L;
            goto Label_0078;
        Label_0019:
            str = "update hospitalinfectionstandard set infection_name = ? where infection_id = ?";
            service = new clsHRPTableService();
            num = 0L;
        Label_0028:
            try
            {
                parameterArray = null;
                service.CreateDatabaseParameter(2, out parameterArray);
                parameterArray[0].Value = p_strStandardValues[1];
                parameterArray[1].Value = p_strStandardValues[0];
                num2 = 0L;
                num = service.lngExecuteParameterSQL(str, ref num2, parameterArray);
                goto Label_0072;
            }
            catch (Exception exception1)
            {
            Label_005F:
                exception = exception1;
                new clsLogText().LogError(exception);
                goto Label_0072;
            }
        Label_0072:
            num3 = num;
        Label_0078:
            return num3;
        }
    }
}
