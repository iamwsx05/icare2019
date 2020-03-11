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
    public class clsHRPMedicineMasterServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngGetMedicineBaseInfo(string p_strMecicineID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMecicineID))
            {
                goto Label_002B;
            }
            num2 = -1L;
            goto Label_0074;
        Label_002B:
            str = "SELECT T1.*,T2.MedicineCategoryName FROM MedicineMaster1 AS T1, MedicineCategory AS T2 WHERE T1.Status = '0' AND T2.Status = '0' AND T1.MedicineFlag = T2.MedicineType AND T1.MedicineCategoryID = T2.MedicineCategoryID AND T1.MedicineID = '" + p_strMecicineID + "' ";
            num = 0L;
        Label_003F:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_006E;
            }
            catch (Exception exception1)
            {
            Label_0051:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006E;
            }
        Label_006E:
            num2 = num;
        Label_0074:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineCureSicknessInfo(string p_strMecicineID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMecicineID))
            {
                goto Label_002B;
            }
            num2 = -1L;
            goto Label_0074;
        Label_002B:
            str = "SELECT * FROM V_MedicineCureSickness_ForSearchICD10Illness WHERE MedicineID = '" + p_strMecicineID + "' ";
            num = 0L;
        Label_003F:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_006E;
            }
            catch (Exception exception1)
            {
            Label_0051:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006E;
            }
        Label_006E:
            num2 = num;
        Label_0074:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineDirectionInfo(string p_strMedicineID, string p_strMedicineTypeID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMedicineID) && !string.IsNullOrEmpty(p_strMedicineTypeID))
            {
                goto Label_003C;
            }
            num2 = -1L;
            goto Label_00AA;
        Label_003C:;
            str = "SELECT DirectionID,DirectionName FROM MedicineDirection WHERE MedicineID = '" + p_strMedicineID + "' AND MedicineOfTypeID = '" + p_strMedicineTypeID + "' AND Status = '0'";
            num = 0L;
        Label_0074:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_00A4;
            }
            catch (Exception exception1)
            {
            Label_0087:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00A4;
            }
        Label_00A4:
            num2 = num;
        Label_00AA:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineDosageInfo(string p_strMecicineID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMecicineID))
            {
                goto Label_002B;
            }
            num2 = -1L;
            goto Label_0074;
        Label_002B:
            str = "SELECT * FROM V_MedicineOfDosage_ForSearchICD10Illness_WithDesc WHERE MedicineID = '" + p_strMecicineID + "'";
            num = 0L;
        Label_003F:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_006E;
            }
            catch (Exception exception1)
            {
            Label_0051:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006E;
            }
        Label_006E:
            num2 = num;
        Label_0074:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineDosageInfo(string p_strMedicineID, string p_strMedicineTypeID, string p_strMedicineDirectionID, string p_strAdultOrChildID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMedicineID) && !string.IsNullOrEmpty(p_strMedicineTypeID))
            {
                goto Label_0040;
            }
            num2 = -1L;
            goto Label_0102;
        Label_0040:
            if (!string.IsNullOrEmpty(p_strMedicineDirectionID) && !string.IsNullOrEmpty(p_strAdultOrChildID))
            {
                goto Label_0075;
            }
            num2 = -1L;
            goto Label_0102;
        Label_0075:;
            str = "SELECT * FROM V_MedicineOfDosage_ForSearchICD10Illness_WithDesc WHERE MedicineID = '" + p_strMedicineID + "' AND MedicineOfTypeID = '" + p_strMedicineTypeID + "' AND DirectionID = '" + p_strMedicineDirectionID + "' AND AdultOrChild = '" + p_strAdultOrChildID + "' ";
            num = 0L;
        Label_00CB:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_00FC;
            }
            catch (Exception exception1)
            {
            Label_00DF:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00FC;
            }
        Label_00FC:
            num2 = num;
        Label_0102:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineDosageInfoBySicknessID(string p_strMecicineID, string p_strMedicineTypeID, string p_strMedicineDirectionID, string p_strAdultOfChildID, string p_strSicknessID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMecicineID) && !string.IsNullOrEmpty(p_strMedicineTypeID))
            {
                goto Label_0040;
            }
            num2 = -1L;
            goto Label_0138;
        Label_0040:
            if (!string.IsNullOrEmpty(p_strMedicineDirectionID) && !string.IsNullOrEmpty(p_strAdultOfChildID))
            {
                goto Label_0075;
            }
            num2 = -1L;
            goto Label_0138;
        Label_0075:
            if (!string.IsNullOrEmpty(p_strSicknessID))
            {
                goto Label_009A;
            }
            num2 = -1L;
            goto Label_0138;
        Label_009A:;
            str = "SELECT * FROM V_MedicineOfDosage_ForSearchICD10Illness_WithDesc WHERE MedicineID = '" + p_strMecicineID + "' AND MedicineOfTypeID = '" + p_strMedicineTypeID + "' AND DirectionID = '" + p_strMedicineDirectionID + "' AND AdultOrChild = '" + p_strAdultOfChildID + "' AND SicknessID ='" + p_strSicknessID + "' ";
            num = 0L;
        Label_0101:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0132;
            }
            catch (Exception exception1)
            {
            Label_0115:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0132;
            }
        Label_0132:
            num2 = num;
        Label_0138:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineInsuranceInfo(string p_strMecicineID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMecicineID))
            {
                goto Label_002B;
            }
            num2 = -1L;
            goto Label_0074;
        Label_002B:
            str = "SELECT * FROM V_MedicineInsuranceInfo WHERE MedicineID = '" + p_strMecicineID + "' ";
            num = 0L;
        Label_003F:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_006E;
            }
            catch (Exception exception1)
            {
            Label_0051:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006E;
            }
        Label_006E:
            num2 = num;
        Label_0074:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineShortnessInfo(string p_strQueryType, string p_strQueryContent, string p_strMedicineFlag, string p_strInsuranceCompanyID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strQueryType))
            {
                goto Label_0030;
            }
            num2 = -1L;
            goto Label_00E0;
        Label_0030:
            if (!string.IsNullOrEmpty(p_strMedicineFlag))
            {
                goto Label_0053;
            }
            num2 = -1L;
            goto Label_00E0;
        Label_0053:;
            str = "SELECT T1.MedicineID,T1.MedicineName,T1.PYCode, T1.LatinName,T1.EnglishName,T1.MedicineFlag,V1.InsuranceCompanyID,V1.InsuranceCompanyName, V1.InsuranceTypeID,ISNULL(V1.InsuranceTypeName,'无') AS InsuranceTypeName ,V1.ChargePercent,V1.InsuranceQty,V1.InsuranceQtyUnit FROM MedicineMaster1 AS T1 LEFT OUTER JOIN V_MedicineInsuranceInfo AS V1 ON T1.MedicineID = V1.MedicineID AND V1.InsuranceCompanyID = '" + p_strInsuranceCompanyID + "' WHERE T1.MedicineFlag = '" + p_strMedicineFlag + "' AND T1." + p_strQueryType + " LIKE '" + p_strQueryContent + "%' AND T1.Status = '0'";
            num = 0L;
        Label_00A9:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_00DA;
            }
            catch (Exception exception1)
            {
            Label_00BD:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00DA;
            }
        Label_00DA:
            num2 = num;
        Label_00E0:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineStandardInfo(string p_strMecicineID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMecicineID))
            {
                goto Label_002B;
            }
            num2 = -1L;
            goto Label_0074;
        Label_002B:
            str = "SELECT MS.MedicineOfTypeID,MT.MedicineOfTypeName,MS.StandardID, MS.StandardName,MS.UnitOfStandard,MS.RetailPrice FROM MedicineOfStandard AS MS,MedicineOfType AS MT WHERE MS.MedicineOfTypeID = MT.MedicineOfTypeID AND MS.Status = '0' AND MT.Status = '0' AND MS.MedicineID = '" + p_strMecicineID + "'";
            num = 0L;
        Label_003F:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_006E;
            }
            catch (Exception exception1)
            {
            Label_0051:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006E;
            }
        Label_006E:
            num2 = num;
        Label_0074:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineStandardInfoByTypeID(string p_strMedicineID, string p_strMedicineTypeID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMedicineID) && !string.IsNullOrEmpty(p_strMedicineTypeID))
            {
                goto Label_003C;
            }
            num2 = -1L;
            goto Label_00AA;
        Label_003C:;
            str = "SELECT T1.MedicineID,T1.MedicineOfTypeID,T2.MedicineOfTypeName, T1.StandardID,T1.StandardName,T1.Begin_Standard_Naming,T1.UnitOfStandard, T1.AlarmMinQty,T1.AlarmMaxQty,T1.AutoPruchase, T1.AutoPruchaseQty,T1.AutoReturn,T1.AutoReturnQty, T1.RetailPrice FROM MedicineOfStandard AS T1,  MedicineOfType AS T2  WHERE T1.MedicineID = '" + p_strMedicineID + "'   AND T1.MedicineOfTypeID = '" + p_strMedicineTypeID + "'  AND T1.Status = '0'  AND T2.MedicineOfTypeID = T1.MedicineOfTypeID AND T2.Status ='0' ";
            num = 0L;
        Label_0074:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_00A4;
            }
            catch (Exception exception1)
            {
            Label_0087:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00A4;
            }
        Label_00A4:
            num2 = num;
        Label_00AA:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineStorageInfo(string p_strMecicineID, string p_strMedicineTypeID, string p_strMedicineStandardID, string p_strMedRoomFlag, string p_strInOutFlag, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMecicineID) && !string.IsNullOrEmpty(p_strMedRoomFlag))
            {
                goto Label_0042;
            }
            num2 = -1L;
            goto Label_0137;
        Label_0042:
            if (!string.IsNullOrEmpty(p_strMedicineStandardID))
            {
                goto Label_0064;
            }
            num2 = -1L;
            goto Label_0137;
        Label_0064:
            if (!string.IsNullOrEmpty(p_strMedicineTypeID) && !string.IsNullOrEmpty(p_strInOutFlag))
            {
                goto Label_0099;
            }
            num2 = -1L;
            goto Label_0137;
        Label_0099:;
            str = "SELECT V1.*,VM.VendorFirstName FROM  (SELECT * FROM MedicineStorage WHERE New = '1' AND  MedRoomFlag = '" + p_strMedRoomFlag + "' AND MedicineID ='" + p_strMecicineID + "'  AND MedicineOfTypeID = '" + p_strMedicineTypeID + "'  AND StandardID = '" + p_strMedicineStandardID + "' AND InOutFlag = '" + p_strInOutFlag + "') AS V1   LEFT OUTER JOIN VendorMaster AS VM ON VM.VendorID = V1.VendorID AND VM.Status = '0'";
            num = 0L;
        Label_0100:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0131;
            }
            catch (Exception exception1)
            {
            Label_0114:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0131;
            }
        Label_0131:
            num2 = num;
        Label_0137:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineTabuInfo(string p_strMecicineID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMecicineID))
            {
                goto Label_002B;
            }
            num2 = -1L;
            goto Label_0074;
        Label_002B:
            str = "SELECT MT.TabuMedicineID AS MedicineTabuID,MM.MedicineName AS MedicineTabuName FROM MedicineTabu AS MT,MedicineMaster1 AS MM WHERE MM.MedicineID = MT.TabuMedicineID AND MT.MedicineID = '" + p_strMecicineID + "' and MT.status = '0' ";
            num = 0L;
        Label_003F:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_006E;
            }
            catch (Exception exception1)
            {
            Label_0051:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006E;
            }
        Label_006E:
            num2 = num;
        Label_0074:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineTogetherWorkingInfo(string p_strMecicineID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMecicineID))
            {
                goto Label_002B;
            }
            num2 = -1L;
            goto Label_0074;
        Label_002B:
            str = "SELECT MT.TogetherMedicineID AS MedicineTogetherWorkingID,MM.MedicineName AS MedicineTogetherWorkingName FROM MedicineTogetherWorking AS MT,MedicineMaster1 AS MM WHERE MM.MedicineID = MT.TogetherMedicineID AND MT.MedicineID = '" + p_strMecicineID + "' and MT.status = '0' ";
            num = 0L;
        Label_003F:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_006E;
            }
            catch (Exception exception1)
            {
            Label_0051:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006E;
            }
        Label_006E:
            num2 = num;
        Label_0074:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMedicineTypeInfo(string p_strMedicineID, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strMedicineID))
            {
                goto Label_002B;
            }
            num2 = -1L;
            goto Label_0074;
        Label_002B:
            str = "SELECT T1.MedicineOfTypeID,T2.MedicineOfTypeName,T2.ShortName FROM MedicineAndMedicineType AS T1,MedicineOfType AS T2 WHERE T1.Status = '0' AND T2.Status = '0' AND T1.MedicineOfTypeID = T2.MedicineOfTypeID AND  T1.MedicineID = '" + p_strMedicineID + "' ";
            num = 0L;
        Label_003F:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_006E;
            }
            catch (Exception exception1)
            {
            Label_0051:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006E;
            }
        Label_006E:
            num2 = num;
        Label_0074:
            return num2;
        }
    }
}
