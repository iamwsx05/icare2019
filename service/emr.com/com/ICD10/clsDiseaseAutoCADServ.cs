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
    public class clsDiseaseAutoCADServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long lngXMLSearchMedicineFromSicknessID(string[] strSicknessIDArr, ref string strXMLTable, ref int intRows)
        {
            string str;
            int num;
            long num2;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            if (strSicknessIDArr != null && strSicknessIDArr.Length > 0)
            {
                goto Label_001F;
            }
            num3 = -1L;
            goto Label_009F;
        Label_001F:
            str = "select distinct m1.MedicineID,m2.MedicineName from MedicineCureSickness m1,MedicineMaster1 m2 where ( m1.SicknessID='" + strSicknessIDArr[0] + "' ";
            num = 1;
            goto Label_004E;
        Label_0036:
            str = str + " or m1.SicknessID='" + strSicknessIDArr[num] + "' ";
            num += 1;
        Label_004E:
            if (num < ((int)strSicknessIDArr.Length))
            {
                goto Label_0036;
            }
            str = str + ") and m1.Status=0 and m1.MedicineID=m2.MedicineID and m2.Status=0 order by m1.MedicineID";
            num2 = 0L;
        Label_0069:
            try
            {
                num2 = new clsHRPTableService().lngGetXMLTable(str, ref strXMLTable, ref intRows);
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
            num3 = num2;
        Label_009F:
            return num3;
        }

        [AutoComplete]
        public long lngXMLSearchSicknessFromSymptom(string[] strSymptomArr, ref string strXMLTable, ref int intRows)
        {
            string str;
            int num;
            long num2;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            if (strSymptomArr != null && strSymptomArr.Length > 0)
            {
                goto Label_001F;
            }
            num3 = -1L;
            goto Label_009F;
        Label_001F:
            str = "select distinct SicknessID,SicknessName from SicknessTable where (Symptom='" + strSymptomArr[0] + "' ";
            num = 1;
            goto Label_004E;
        Label_0036:
            str = str + " or Symptom='" + strSymptomArr[num] + "' ";
            num += 1;
        Label_004E:
            if (num < ((int)strSymptomArr.Length))
            {
                goto Label_0036;
            }
            str = str + ") and Status=0 order by SicknessID";
            num2 = 0L;
        Label_0069:
            try
            {
                num2 = new clsHRPTableService().lngGetXMLTable(str, ref strXMLTable, ref intRows);
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
            num3 = num2;
        Label_009F:
            return num3;
        }
    }
}
