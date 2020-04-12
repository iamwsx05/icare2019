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
    public class clsICD10DialogServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        // Fields
        private clsHRPTableService hs;

        // Methods
        public clsICD10DialogServ()
        {
            this.hs = new clsHRPTableService();
        }

        [AutoComplete]
        public long lngAddDetailCode(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_005B;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.add_new_record("ICD10_IllnessDetailID", strXML);
                goto Label_0055;
            }
            catch (Exception exception1)
            {
            Label_003A:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0055;
            }
        Label_0055:
            num2 = num;
        Label_005B:
            return num2;
        }

        [AutoComplete]
        public long lngAddKnubID(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_005B;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.add_new_record("ICDO_ID", strXML);
                goto Label_0055;
            }
            catch (Exception exception1)
            {
            Label_003A:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0055;
            }
        Label_0055:
            num2 = num;
        Label_005B:
            return num2;
        }

        [AutoComplete]
        public long lngAddKnubType(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_005B;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.add_new_record("ICDO_Type", strXML);
                goto Label_0055;
            }
            catch (Exception exception1)
            {
            Label_003A:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0055;
            }
        Label_0055:
            num2 = num;
        Label_005B:
            return num2;
        }

        [AutoComplete]
        public long lngAddNewCategory(string strCategoryName, string strDirectoryID, out string strCategoryID)
        {
            string str;
            string str2;
            long num;
            long num2;
            Exception exception;
            string str3;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            string[] strArray;
            str = "";
            strCategoryID = null;
            if (!string.IsNullOrEmpty(strCategoryName) && !string.IsNullOrEmpty(strDirectoryID))
            {
                goto Label_003D;
            }
            num3 = -1L;
            goto Label_00F7;
        Label_003D:
            num = 0L;
        Label_0040:
            try
            {
                num = new clsHRPTableService().lngGenerateID(7, "IllnessCategoryID", "ICD10_IllnessCategory", out str2);
                if (num < 0L)
                {
                    goto Label_00C2;
                }
                str = "<Patient IllnessCategoryID='" + str2 + "' IllnessCategoryName='" + strCategoryName + "' DirectoryID='" + strDirectoryID + "' Status='0' />";
                num2 = this.hs.add_new_record("ICD10_IllnessCategory", str);
                strCategoryID = str2;
                num3 = num2;
                goto Label_00F7;
            Label_00C2:
                strCategoryID = "";
                num3 = 0L;
                goto Label_00F7;
            }
            catch (Exception exception1)
            {
            Label_00D0:
                exception = exception1;
                str3 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00F1;
            }
        Label_00F1:
            num3 = num;
        Label_00F7:
            return num3;
        }

        [AutoComplete]
        public long lngAddNewDirectory(string strDirectoryName, out string strDirectoryID)
        {
            string str;
            string str2;
            long num;
            long num2;
            Exception exception;
            string str3;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            string[] strArray;
            strDirectoryID = null;
            str = "";
            if (!string.IsNullOrEmpty(strDirectoryName))
            {
                goto Label_002D;
            }
            num3 = -1L;
            goto Label_00DA;
        Label_002D:
            num = 0L;
        Label_0030:
            try
            {
                num = this.hs.lngGenerateID(7, "DirectoryID", "ICD10_Directory", out str2);
                if (num < 0L)
                {
                    goto Label_00A5;
                }
                str = "<Patient DirectoryID='" + str2 + "' DirectoryName='" + strDirectoryName + "' Status='0' />";
                num2 = this.hs.add_new_record("ICD10_Directory", str);
                strDirectoryID = str2;
                num3 = num2;
                goto Label_00DA;
            Label_00A5:
                strDirectoryID = "";
                num3 = 0L;
                goto Label_00DA;
            }
            catch (Exception exception1)
            {
            Label_00B3:
                exception = exception1;
                str3 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00D4;
            }
        Label_00D4:
            num3 = num;
        Label_00DA:
            return num3;
        }

        [AutoComplete]
        public long lngAddNewIllnessCode(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_005B;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.add_new_record("ICD10_IllnessID", strXML);
                goto Label_0055;
            }
            catch (Exception exception1)
            {
            Label_003A:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0055;
            }
        Label_0055:
            num2 = num;
        Label_005B:
            return num2;
        }

        [AutoComplete]
        public long lngAddNewIllnessSubCode(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_005B;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.add_new_record("ICD10_IllnessSubID", strXML);
                goto Label_0055;
            }
            catch (Exception exception1)
            {
            Label_003A:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0055;
            }
        Label_0055:
            num2 = num;
        Label_005B:
            return num2;
        }

        [AutoComplete]
        public long lngDelCategory(string strID, string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strID) && !string.IsNullOrEmpty(strXML))
            {
                goto Label_0031;
            }
            num2 = -1L;
            goto Label_007E;
        Label_0031:
            num = 0L;
        Label_0034:
            try
            {
                num = this.hs.modify_record("ICD10_IllnessCategory", strXML, new string[] { "IllnessCategoryID" });
                goto Label_0078;
            }
            catch (Exception exception1)
            {
            Label_005D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0078;
            }
        Label_0078:
            num2 = num;
        Label_007E:
            return num2;
        }

        [AutoComplete]
        public long lngDelDirectory(string strID, string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strID) && !string.IsNullOrEmpty(strXML))
            {
                goto Label_0031;
            }
            num2 = -1L;
            goto Label_007E;
        Label_0031:
            num = 0L;
        Label_0034:
            try
            {
                num = this.hs.modify_record("ICD10_Directory", strXML, new string[] { "DirectoryID" });
                goto Label_0078;
            }
            catch (Exception exception1)
            {
            Label_005D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0078;
            }
        Label_0078:
            num2 = num;
        Label_007E:
            return num2;
        }

        [AutoComplete]
        public long lngDelIllDetailCodeInfo(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_006E;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.modify_record("ICD10_IllnessDetailID", strXML, new string[] { "IllnessDetailID" });
                goto Label_0068;
            }
            catch (Exception exception1)
            {
            Label_004D:
                exception = exception1;
                str = exception.Message;
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
        public long lngDelIllnessCodeID(string strIllCodeID, string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strIllCodeID) && !string.IsNullOrEmpty(strXML))
            {
                goto Label_0031;
            }
            num2 = -1L;
            goto Label_007E;
        Label_0031:
            num = 0L;
        Label_0034:
            try
            {
                num = this.hs.modify_record("ICD10_IllnessID", strXML, new string[] { "IllnessID" });
                goto Label_0078;
            }
            catch (Exception exception1)
            {
            Label_005D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0078;
            }
        Label_0078:
            num2 = num;
        Label_007E:
            return num2;
        }

        [AutoComplete]
        public long lngDelIllnessSubCodeID(string strIllSubCodeID, string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strIllSubCodeID) && !string.IsNullOrEmpty(strXML))
            {
                goto Label_0031;
            }
            num2 = -1L;
            goto Label_007E;
        Label_0031:
            num = 0L;
        Label_0034:
            try
            {
                num = this.hs.modify_record("ICD10_IllnessSubID", strXML, new string[] { "IllnessSubID" });
                goto Label_0078;
            }
            catch (Exception exception1)
            {
            Label_005D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0078;
            }
        Label_0078:
            num2 = num;
        Label_007E:
            return num2;
        }

        [AutoComplete]
        public long lngDelKnubType(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_006E;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.modify_record("ICDO_Type", strXML, new string[] { "OncologyTypeID" });
                goto Label_0068;
            }
            catch (Exception exception1)
            {
            Label_004D:
                exception = exception1;
                str = exception.Message;
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
        public long lngDelKnupIDRecord(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_006E;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.modify_record("ICDO_ID", strXML, new string[] { "OncologyID" });
                goto Label_0068;
            }
            catch (Exception exception1)
            {
            Label_004D:
                exception = exception1;
                str = exception.Message;
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
        public long lngGetCategoryInfo(string strDirID, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(strDirID))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_006B;
        Label_0021:
            str = "select IllnessCategoryID,IllnessCategoryName from ICD10_IllnessCategory where DirectoryID ='" + strDirID + "'and  Status ='0'";
            num = 0L;
        Label_0035:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
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
        public long lngGetDetailCodeInfo(string strIllnessSubID, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(strIllnessSubID))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_006B;
        Label_0021:
            str = "select IllnessDetailID,IllnessSubID,IllnessDetailName,PYCode,StatCode,LesserCode from ICD10_IllnessDetailID where IllnessSubID ='" + strIllnessSubID + "'and  Status ='0'";
            num = 0L;
        Label_0035:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
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
        public long lngGetDirInfo(ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select DirectoryID,DirectoryName from ICD10_Directory where Status ='0'";
            num = 0L;
        Label_000A:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_003A;
            }
            catch (Exception exception1)
            {
            Label_001D:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_003A;
            }
        Label_003A:
            num2 = num;
        Label_0040:
            return num2;
        }

        [AutoComplete]
        public long lngGetIllCodeInfo(string strCategoryID, ref string strXML, ref int intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(strCategoryID))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_006B;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                str = "select IllnessID,IllnessName,IllnessCategoryID,PYCode,StatCode,LesserCode  from ICD10_IllnessID where IllnessCategoryID ='" + strCategoryID + "'and  Status ='0'";
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
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
        public long lngGetIllSubCodeInfo(string strIllCodeID, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(strIllCodeID))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_006B;
        Label_0021:
            str = "select IllnessSubID,IllnessID,IllnessSubName,Discription,PYCode,StatCode,LesserCode  from ICD10_IllnessSubID where IllnessID ='" + strIllCodeID + "'and  Status ='0'";
            num = 0L;
        Label_0035:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
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
        public long lngGetKnubIDInfoArr(string strOncologyTypeID, ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select OncologyID,OncologyTypeID,OncologyName,PYCode from ICDO_ID where OncologyTypeID ='" + strOncologyTypeID + "' and Status ='0'";
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
        public long lngGetKnubTypeInfoArr(ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select OncologyTypeID,OncologyTypeName,PYCode from ICDO_Type where Status ='0'";
            num = 0L;
        Label_000A:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_003A;
            }
            catch (Exception exception1)
            {
            Label_001D:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_003A;
            }
        Label_003A:
            num2 = num;
        Label_0040:
            return num2;
        }

        [AutoComplete]
        public long lngGetTablesInfo(ref string strXML, ref int intRows)
        {
            string str;
            long num;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            str = "select *  from ( select DirCategory.DirectoryID,DirCategory.DirectoryName,DirCategory.IllnessCategoryID as CategoryID1,DirCategory.IllnessCategoryName , IllID.IllnessCategoryID as CategoryID2,IllID.IllnessID as IllnessID1,IllID.IllnessName as IllnessName1,IllID.PYCode as PYCode1 from ( select dir.DirectoryID,dir.DirectoryName ,Category.IllnessCategoryID , Category.IllnessCategoryName   from dbo.ICD10_Directory dir left outer join dbo.ICD10_IllnessCategory Category  on dir.DirectoryID =Category.DirectoryID and Category.Status='0'  where dir.Status='0' )  DirCategory  left outer join  dbo.ICD10_IllnessID IllID   on DirCategory.IllnessCategoryID =IllID.IllnessCategoryID and IllID.Status='0' )  DirIllID  left outer join   dbo.ICD10_IllnessSubID IllSubID   on DirIllID.IllnessID1 = IllSubID.IllnessID and IllSubID.Status='0' order by DirIllID.DirectoryID ";
            num = 0L;
        Label_000A:
            try
            {
                num = this.hs.lngGetXMLTable(str, ref strXML, ref intRows);
                goto Label_003A;
            }
            catch (Exception exception1)
            {
            Label_001D:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_003A;
            }
        Label_003A:
            num2 = num;
        Label_0040:
            return num2;
        }

        [AutoComplete]
        public long lngModifyCategory(string strID, string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strID) && !string.IsNullOrEmpty(strXML))
            {
                goto Label_0031;
            }
            num2 = -1L;
            goto Label_007E;
        Label_0031:
            num = 0L;
        Label_0034:
            try
            {
                num = this.hs.modify_record("ICD10_IllnessCategory", strXML, new string[] { "IllnessCategoryID" });
                goto Label_0078;
            }
            catch (Exception exception1)
            {
            Label_005D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0078;
            }
        Label_0078:
            num2 = num;
        Label_007E:
            return num2;
        }

        [AutoComplete]
        public long lngModifyDirectory(string strID, string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strID) && !string.IsNullOrEmpty(strXML))
            {
                goto Label_0031;
            }
            num2 = -1L;
            goto Label_007E;
        Label_0031:
            num = 0L;
        Label_0034:
            try
            {
                num = this.hs.modify_record("ICD10_Directory", strXML, new string[] { "DirectoryID" });
                goto Label_0078;
            }
            catch (Exception exception1)
            {
            Label_005D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0078;
            }
        Label_0078:
            num2 = num;
        Label_007E:
            return num2;
        }

        [AutoComplete]
        public long lngModifyIllDetailInfo(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_006E;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.modify_record("ICD10_IllnessDetailID", strXML, new string[] { "IllnessDetailID" });
                goto Label_0068;
            }
            catch (Exception exception1)
            {
            Label_004D:
                exception = exception1;
                str = exception.Message;
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
        public long lngModifyIllnessCode(string strXML, string strIllnessID)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strIllnessID) && !string.IsNullOrEmpty(strXML))
            {
                goto Label_0031;
            }
            num2 = -1L;
            goto Label_007E;
        Label_0031:
            num = 0L;
        Label_0034:
            try
            {
                num = this.hs.modify_record("ICD10_IllnessID", strXML, new string[] { "IllnessID" });
                goto Label_0078;
            }
            catch (Exception exception1)
            {
            Label_005D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0078;
            }
        Label_0078:
            num2 = num;
        Label_007E:
            return num2;
        }

        [AutoComplete]
        public long lngModifyIllnessSubCode(string strXML, string strIllnessSubID)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strIllnessSubID) && !string.IsNullOrEmpty(strXML))
            {
                goto Label_0031;
            }
            num2 = -1L;
            goto Label_007E;
        Label_0031:
            num = 0L;
        Label_0034:
            try
            {
                num = this.hs.modify_record("ICD10_IllnessSubID", strXML, new string[] { "IllnessSubID" });
                goto Label_0078;
            }
            catch (Exception exception1)
            {
            Label_005D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0078;
            }
        Label_0078:
            num2 = num;
        Label_007E:
            return num2;
        }

        [AutoComplete]
        public long lngModifyKnubID(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_006E;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.modify_record("ICDO_ID", strXML, new string[] { "OncologyID" });
                goto Label_0068;
            }
            catch (Exception exception1)
            {
            Label_004D:
                exception = exception1;
                str = exception.Message;
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
        public long lngModifyKnubType(string strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0021;
            }
            num2 = -1L;
            goto Label_006E;
        Label_0021:
            num = 0L;
        Label_0024:
            try
            {
                num = this.hs.modify_record("ICDO_Type", strXML, new string[] { "OncologyTypeID" });
                goto Label_0068;
            }
            catch (Exception exception1)
            {
            Label_004D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0068;
            }
        Label_0068:
            num2 = num;
        Label_006E:
            return num2;
        }
    }
}
