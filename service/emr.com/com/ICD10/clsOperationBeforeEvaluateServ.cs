using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using System.Text;
using System.Xml;

namespace com.digitalwave.common.ICD10.Midtier
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOperationBeforeEvaluateServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        // Fields
        private clsHRPTableService hs;

        // Methods
        public clsOperationBeforeEvaluateServ()
        {
            this.hs = new clsHRPTableService();
        }

        [AutoComplete]
        public long lngDelete(string strInHospitalNO, string strOperationLookBeforeDate)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            string[] strArray;
            bool flag2;
            str = "UPDATE  OperationLookBefore_NoticeItems SET Status=1  WHERE InHospitalNO='" + strInHospitalNO + "' and OperationLookBeforeDate='" + strOperationLookBeforeDate + "' and status = 0 ";
            num = 0L;
        Label_0039:
            try
            {
                num = this.hs.DoExcute(str);
                if (num < 0L)
                {
                    goto Label_0096;
                }
                str = "UPDATE OperationLookBefore_AnaesthesiaMode SET Status=1  WHERE InHospitalNO='" + strInHospitalNO + "' and OperationLookBeforeDate='" + strOperationLookBeforeDate + "' and status = 0 ";
                num = this.hs.DoExcute(str);
            Label_0096:
                if (num < 0L)
                {
                    goto Label_00E5;
                }
                str = "UPDATE OperationLookBefore_MedicineForAnaesthesia SET Status=1  WHERE InHospitalNO='" + strInHospitalNO + "' and OperationLookBeforeDate='" + strOperationLookBeforeDate + "'  and status = 0 ";
                num = this.hs.DoExcute(str);
            Label_00E5:
                if (num < 0L)
                {
                    goto Label_0134;
                }
                str = "UPDATE OperationLookBefore_Master SET Status=1  WHERE InHospitalNO='" + strInHospitalNO + "' and OperationLookBeforeDate='" + strOperationLookBeforeDate + "'  and status = 0 ";
                num = this.hs.DoExcute(str);
            Label_0134:
                goto Label_0154;
            }
            catch (Exception exception1)
            {
            Label_0137:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0154;
            }
        Label_0154:
            num2 = num;
        Label_015A:
            return num2;
        }

        [AutoComplete]
        public long lngGetAllAnaesthesiaSubModeNames(out string strXML, out int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            strXML = null;
            intRows = 0;
            str = "SELECT AnaesthesiaSubModeID,AnaesthesiaSubModeName from AnaesthesiaSubMode   WHERE status != '1' ";
            num = 0L;
        Label_0010:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref strXML, ref intRows);
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
        public long lngGetIllCategoryInfoLikeCategoryID(string strCategoryID, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select IllnessCategoryID,DirectoryID,IllnessCategoryName from ICD10_IllnessCategory where IllnessCategoryID like '" + strCategoryID + "%' and  Status ='0'";
            num = 0L;
        Label_0015:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0045;
            }
            catch (Exception exception1)
            {
            Label_0028:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0045;
            }
        Label_0045:
            num2 = num;
        Label_004B:
            return num2;
        }

        [AutoComplete]
        public long lngGetIllCategoryInfoLikeCategoryName(string strCategoryName, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select IllnessCategoryID,DirectoryID,IllnessCategoryName from ICD10_IllnessCategory where IllnessCategoryName like '" + strCategoryName + "%' and  Status ='0'";
            num = 0L;
        Label_0015:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0045;
            }
            catch (Exception exception1)
            {
            Label_0028:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0045;
            }
        Label_0045:
            num2 = num;
        Label_004B:
            return num2;
        }

        [AutoComplete]
        public long lngGetIllCodeInfoByCategoryID(string strCategoryID, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select IllnessID,IllnessName,IllnessCategoryID,PYCode  from ICD10_IllnessID where IllnessCategoryID='" + strCategoryID + "'  and  Status ='0'";
            num = 0L;
        Label_0015:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0045;
            }
            catch (Exception exception1)
            {
            Label_0028:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0045;
            }
        Label_0045:
            num2 = num;
        Label_004B:
            return num2;
        }

        [AutoComplete]
        public long lngGetIllCodeInfoLikeIllCodeID(string strIllCodeID, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select IllnessID,IllnessName,IllnessCategoryID,PYCode  from ICD10_IllnessID where IllnessID like '" + strIllCodeID + "%' and  Status ='0'";
            num = 0L;
        Label_0015:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0045;
            }
            catch (Exception exception1)
            {
            Label_0028:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0045;
            }
        Label_0045:
            num2 = num;
        Label_004B:
            return num2;
        }

        [AutoComplete]
        public long lngGetIllCodeInfoLikeIllName(string strIllName, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select IllnessID,IllnessName,IllnessCategoryID,PYCode  from ICD10_IllnessID where IllnessName like '" + strIllName + "%' and  Status ='0'";
            num = 0L;
        Label_0015:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0045;
            }
            catch (Exception exception1)
            {
            Label_0028:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0045;
            }
        Label_0045:
            num2 = num;
        Label_004B:
            return num2;
        }

        [AutoComplete]
        public long lngGetIllCodeInfoLikeIllSubCodeID(string strIllSubCodeID, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select IllnessSubID,IllnessID,IllnessSubName,Discription,PYCode  from ICD10_IllnessSubID where IllnessSubID like '" + strIllSubCodeID + "%' and  Status ='0'";
            num = 0L;
        Label_0015:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0045;
            }
            catch (Exception exception1)
            {
            Label_0028:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0045;
            }
        Label_0045:
            num2 = num;
        Label_004B:
            return num2;
        }

        [AutoComplete]
        public long lngGetIllCodeInfoLikeIllSubCodeName(string strIllSubCodeName, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select IllnessSubID,IllnessID,IllnessSubName,Discription,PYCode  from ICD10_IllnessSubID where IllnessSubName like '" + strIllSubCodeName + "%' and  Status ='0'";
            num = 0L;
        Label_0015:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0045;
            }
            catch (Exception exception1)
            {
            Label_0028:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0045;
            }
        Label_0045:
            num2 = num;
        Label_004B:
            return num2;
        }

        [AutoComplete]
        public long lngGetIllCodeInfoLikeIllSubPYCode(string strSubPYCode, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select IllnessSubID,IllnessID,IllnessSubName,Discription,PYCode  from ICD10_IllnessSubID where PYCode like '" + strSubPYCode + "%' and  Status ='0'";
            num = 0L;
        Label_0015:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0045;
            }
            catch (Exception exception1)
            {
            Label_0028:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0045;
            }
        Label_0045:
            num2 = num;
        Label_004B:
            return num2;
        }

        [AutoComplete]
        public long lngGetIllCodeInfoLikePYCode(string strPYCode, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select IllnessID,IllnessName,IllnessCategoryID,PYCode  from ICD10_IllnessID where PYCode like '" + strPYCode + "%' and  Status ='0'";
            num = 0L;
        Label_0015:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0045;
            }
            catch (Exception exception1)
            {
            Label_0028:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0045;
            }
        Label_0045:
            num2 = num;
        Label_004B:
            return num2;
        }

        public long lngGetIllSubCodeInfo(string strIllCodeID, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            long num2;
            str = "select IllnessSubID,IllnessID,IllnessSubName,Discription,PYCode  from ICD10_IllnessSubID where IllnessID ='" + strIllCodeID + "' and  Status ='0'";
            num2 = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
        Label_0025:
            return num2;
        }

        [AutoComplete]
        public long lngGetNoticeItemResult(string strInHospitalNO, string strDate, string strAnaesthesiaSubModeID, string strNoticeItemID, ref string strXml, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            string[] strArray;
            str = "SELECT i.NoticeItemResultID,r.NoticeItemResultName FROM OperationLookBefore_NoticeItems i INNER JOIN AnaesthesiaModeNoticeItemResult r ON i.NoticeItemResultID=r.NoticeItemResultID  WHERE i.InHospitalNO = '" + strInHospitalNO + "' AND i.OperationLookBeforeDate ='" + strDate + "' AND i.AnaesthesiaSubModeID='" + strAnaesthesiaSubModeID + "' AND i.NoticeItemID='" + strNoticeItemID + "' and i.Status=0 and r.Status=0";
            num = 0L;
        Label_0057:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXml, ref intRows);
                goto Label_0089;
            }
            catch (Exception exception1)
            {
            Label_006C:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0089;
            }
        Label_0089:
            num2 = num;
        Label_008F:
            return num2;
        }

        [AutoComplete]
        public long lngGetNowSickHistoryAndAdvice(string strInHospital, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            string[] strArray;
            str = @"SELECT inh.*, ihchs.SensitiveHistory , ihchs.Sensitive , AA.*, BB.* FROM InHospitalCaseHistoryFirst inh LEFT OUTER JOIN  InHospitalCaseHistorySecond ihchs ON inh.InHospitalNO = ihchs.InHospitalNO AND      inh.InHospitalDate = ihchs.InHospitalDate  LEFT 
                        OUTER JOIN (SELECT dl.inhospitalno AS inhospitalnoDL, dl.AdviceID AS DLAdviceID, dl.adviceflag AS DLAdviceFlag, dl.BeginYearDate AS DLBeginYearDate, dl.DoctorAdvice AS DoctorAdvice_long, mm.MedicineName AS DLmmName,  
                     mc.MedicineCategoryName AS DLmmCategory FROM DoctorAdviceLongTime dl, medicinemaster1 mm, MedicineCategory mc WHERE dl.status = 0 AND dl.AdviceID = mm.medicineId AND mm.MedicineCategoryID = mc.MedicineCategoryID AND  mc.MedicineType = mm.MedicineFlag 
                     AND (dl.adviceflag = 'w' OR dl.adviceflag = 'c')) AA  ON inh.InHospitalNO = AA.inhospitalnoDL  LEFT OUTER JOIN (SELECT dt.inhospitalno AS inhospitalnoDT, dt.AdviceID AS DTAdviceID, dt.adviceflag AS DTAdviceFlag, dt.DoctorAdvice AS DoctorAdvice_temp, 
                        dt.BeginYearDate AS DTBeginYearDate, mm.MedicineName AS DTmmName,  mc.MedicineCategoryName AS DTmmCategory FROM DoctorAdviceTemp dt, medicinemaster1 mm, MedicineCategory mc WHERE dt.status = 0 AND dt.AdviceID = mm.medicineId AND mm.MedicineCategoryID = mc.MedicineCategoryID 
                     AND mc.MedicineType = mm.MedicineFlag AND (dt.adviceflag = 'w' OR dt.adviceflag = 'c')) BB  ON inh.InHospitalNO = BB.inhospitalnoDT WHERE inh.InHospitalNO = '" + strInHospital + "' AND (inh.Status = 0) AND (ihchs.Status = 0) AND (inh.InHospitalDate = (SELECT MAX(InHospitalDate) AS MaxInHospitalDate FROM InHospitalCaseHistoryFirst WHERE InHospitalNO = '" + strInHospital + "' AND status = 0)) ";
            num = 0L;
        Label_0039:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0068;
            }
            catch (Exception exception1)
            {
            Label_004B:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0068;
            }
        Label_0068:
            num2 = num;
        Label_006E:
            return num2;
        }

        [AutoComplete]
        public long lngGetOldOperationRecords(string strInHospitalNO, out string strXML, out int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            strXML = null;
            intRows = 0;
            str = null;
            str = "SELECT a.OperationDate,a.OperationName, a.AnaesthesiaSubMethodID,b.AnaesthesiaSubModeName from OperationRecord a,AnaesthesiaSubMode b  WHERE a.AnaesthesiaSubMethodID=b.AnaesthesiaSubModeID and a.InHospitalNo = '" + strInHospitalNO + "' AND a.status = '0' ";
            num = 0L;
        Label_001D:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_004C;
            }
            catch (Exception exception1)
            {
            Label_002F:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_004C;
            }
        Label_004C:
            num2 = num;
        Label_0052:
            return num2;
        }

        [AutoComplete]
        public long lngGetOperationLookBefore_MasterArr(string strInHospitalNO, out string strXML, out int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            strXML = null;
            intRows = 0;
            str = null;
            str = "SELECT *  from OperationLookBefore_Master  WHERE InHospitalNO = '" + strInHospitalNO + "' AND Status != '1' ORDER BY OperationLookBeforeDate DESC";
            num = 0L;
        Label_001D:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_004C;
            }
            catch (Exception exception1)
            {
            Label_002F:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_004C;
            }
        Label_004C:
            num2 = num;
        Label_0052:
            return num2;
        }

        [AutoComplete]
        public long lngGetOperationLookBefore_SubArr(string strInHospitalNO, string strDate, out string strXML_AnaesthesiaMode, out string strXML_MedicineForAnaesthesia, out int intRows_AnaesthesiaMode, out int intRows_MedicineForAnaesthesia)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            string[] strArray;
            bool flag2;
            strXML_AnaesthesiaMode = null;
            strXML_MedicineForAnaesthesia = null;
            intRows_AnaesthesiaMode = 0;
            intRows_MedicineForAnaesthesia = 0;
            str = null;
            str = "SELECT AnaesthesiaSubModeID from OperationLookBefore_AnaesthesiaMode  WHERE InHospitalNO = '" + strInHospitalNO + "' AND OperationLookBeforeDate ='" + strDate + "' AND Status = '0' ";
            num = 0L;
        Label_004A:
            try
            {
                num = new clsHRPTableService().lngGetXMLTable(str, ref strXML_AnaesthesiaMode, ref intRows_AnaesthesiaMode);
                if (num <= 0)
                {
                    goto Label_00AF;
                }
                str = "SELECT MedicineID from OperationLookBefore_MedicineForAnaesthesia  WHERE InHospitalNO = '" + strInHospitalNO + "' AND OperationLookBeforeDate ='" + strDate + "' AND Status = '0' ";
                num = new clsHRPTableService().lngGetXMLTable(str, ref strXML_MedicineForAnaesthesia, ref intRows_MedicineForAnaesthesia);
            Label_00AF:
                goto Label_00CF;
            }
            catch (Exception exception1)
            {
            Label_00B2:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00CF;
            }
        Label_00CF:
            num2 = num;
        Label_00D5:
            return num2;
        }

        [AutoComplete]
        public long lngSave(string strPersonalHistoryXML, string[] strXML_AnaesthesiaModeArr, string[] strXML_MedicineForAnaesthesiaArr, string[] strXML_NoticeItemsArr, bool p_blnIsAddNew)
        {
            long num;
            int num2;
            XmlDocument document;
            XmlNode node;
            string str;
            string str2;
            string str3;
            Exception exception;
            string str4;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strPersonalHistoryXML))
            {
                goto Label_0024;
            }
            num3 = -1L;
            goto Label_0335;
        Label_0024:
            num = 0L;
        Label_0027:
            try
            {
                if (p_blnIsAddNew == false)
                {
                    goto Label_0102;
                }
                num = this.hs.add_new_record("OperationLookBefore_Master", strPersonalHistoryXML);
                if (strXML_AnaesthesiaModeArr == null)
                {
                    goto Label_0083;
                }
                num2 = 0;
                goto Label_006F;
            Label_0057:
                num = this.hs.add_new_record("OperationLookBefore_AnaesthesiaMode", strXML_AnaesthesiaModeArr[num2]);
                num2 += 1;
            Label_006F:
                if (num2 >= strXML_AnaesthesiaModeArr.Length && num > 0)
                {
                    goto Label_0057;
                }
            Label_0083:
                if (strXML_MedicineForAnaesthesiaArr == null)
                {
                    goto Label_00BD;
                }
                num2 = 0;
                goto Label_00A9;
            Label_0091:
                num = this.hs.add_new_record("OperationLookBefore_MedicineForAnaesthesia", strXML_MedicineForAnaesthesiaArr[num2]);
                num2 += 1;
            Label_00A9:
                if (num2 >= strXML_MedicineForAnaesthesiaArr.Length && num > 0)
                {
                    goto Label_0091;
                }
            Label_00BD:
                if (strXML_NoticeItemsArr == null)
                {
                    goto Label_00FA;
                }
                num2 = 0;
                goto Label_00E5;
            Label_00CC:
                num = this.hs.add_new_record("OperationLookBefore_NoticeItems", strXML_NoticeItemsArr[num2]);
                num2 += 1;
            Label_00E5:
                if (num2 >= strXML_NoticeItemsArr.Length && num > 0)
                {
                    goto Label_00CC;
                }
            Label_00FA:
                num3 = num;
                goto Label_0335;
            Label_0102:;
                num = this.hs.modify_record("OperationLookBefore_Master", strPersonalHistoryXML, new string[] { "InHospitalNO", "OperationLookBeforeDate" });
                if (num <= 0)
                {
                    goto Label_030A;
                }
                document = new XmlDocument();
                document.LoadXml(strPersonalHistoryXML);
                node = document.FirstChild;
                str = node.Attributes["InHospitalNO"].Value;
                str2 = node.Attributes["OperationLookBeforeDate"].Value;
                str3 = "DELETE FROM OperationLookBefore_NoticeItems  WHERE InHospitalNO='" + str + "' and OperationLookBeforeDate='" + str2 + "' and status = '0' ";
                this.hs.DoExcute(str3);
                str3 = "DELETE FROM OperationLookBefore_AnaesthesiaMode  WHERE InHospitalNO='" + str + "' and OperationLookBeforeDate='" + str2 + "' and status = '0' ";
                this.hs.DoExcute(str3);
                if (strXML_AnaesthesiaModeArr == null)
                {
                    goto Label_024C;
                }
                num2 = 0;
                goto Label_0238;
            Label_0220:
                num = this.hs.add_new_record("OperationLookBefore_AnaesthesiaMode", strXML_AnaesthesiaModeArr[num2]);
                num2 += 1;
            Label_0238:
                if (num2 >= strXML_AnaesthesiaModeArr.Length && num > 0)
                {
                    goto Label_0220;
                }
            Label_024C:
                if (strXML_NoticeItemsArr == null)
                {
                    goto Label_0289;
                }
                num2 = 0;
                goto Label_0274;
            Label_025B:
                num = this.hs.add_new_record("OperationLookBefore_NoticeItems", strXML_NoticeItemsArr[num2]);
                num2 += 1;
            Label_0274:
                if (num2 >= strXML_NoticeItemsArr.Length && num > 0)
                {
                    goto Label_025B;
                }
            Label_0289:;
                str3 = "DELETE FROM OperationLookBefore_MedicineForAnaesthesia  WHERE InHospitalNO='" + str + "' and OperationLookBeforeDate='" + str2 + "'  and status = '0' ";
                this.hs.DoExcute(str3);
                if (strXML_MedicineForAnaesthesiaArr == null)
                {
                    goto Label_0309;
                }
                num2 = 0;
                goto Label_02F5;
            Label_02DD:
                num = this.hs.add_new_record("OperationLookBefore_MedicineForAnaesthesia", strXML_MedicineForAnaesthesiaArr[num2]);
                num2 += 1;
            Label_02F5:
                if (num2 >= strXML_MedicineForAnaesthesiaArr.Length && num > 0)
                {
                    goto Label_02DD;
                }
            Label_0309:;
            Label_030A:
                goto Label_032F;
            }
            catch (Exception exception1)
            {
            Label_030E:
                exception = exception1;
                str4 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_032F;
            }
        Label_032F:
            num3 = num;
        Label_0335:
            return num3;
        }

        [AutoComplete]
        public long m_lngGetICDO_ID(string p_strICDO_TYPEID, out string strXML, out int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            strXML = null;
            intRows = 0;
            if (!string.IsNullOrEmpty(p_strICDO_TYPEID))
            {
                goto Label_0027;
            }
            num2 = -1L;
            goto Label_0071;
        Label_0027:
            str = "SELECT OncologyID,OncologyTypeID,OncologyName,PYCode FROM  ICDO_ID WHERE OncologyTypeID = '" + p_strICDO_TYPEID + "' AND Status = '0' ";
            num = 0L;
        Label_003B:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_006B;
            }
            catch (Exception exception1)
            {
            Label_004E:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006B;
            }
        Label_006B:
            num2 = num;
        Label_0071:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetICDO_TYPELike(string p_strQueryType, string p_strQueryContent, out string strXML, out int intRows)
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
            strXML = null;
            intRows = 0;
            if (!string.IsNullOrEmpty(p_strQueryType))
            {
                goto Label_0028;
            }
            num2 = -1L;
            goto Label_0097;
        Label_0028:;
            str = "SELECT OncologyTypeID,OncologyTypeName,PYCode FROM  ICDO_Type WHERE " + p_strQueryType + " LIKE '" + p_strQueryContent + "%' AND Status = '0'";
            num = 0L;
        Label_0060:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_0091;
            }
            catch (Exception exception1)
            {
            Label_0074:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0091;
            }
        Label_0091:
            num2 = num;
        Label_0097:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetOperationInformOfBeReadyInfo(string p_strInHospitalNO, out string p_strXML, out int p_intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = null;
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strInHospitalNO))
            {
                goto Label_0027;
            }
            num2 = -1L;
            goto Label_0071;
        Label_0027:
            str = "SELECT OperationgTime,Diagnose,PrepareOperation FROM OperationInform WHERE Status ='0' AND InHospitalNO = '" + p_strInHospitalNO + "' ";
            num = 0L;
        Label_003B:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_006B;
            }
            catch (Exception exception1)
            {
            Label_004E:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_006B;
            }
        Label_006B:
            num2 = num;
        Label_0071:
            return num2;
        }
    }
}
