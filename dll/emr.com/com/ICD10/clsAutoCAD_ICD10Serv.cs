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
    public class clsAutoCAD_ICD10Serv : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        // Fields
        private clsHRPTableService hrp;

        // Methods
        public clsAutoCAD_ICD10Serv()
        {
            this.hrp = new clsHRPTableService();
        }

        [AutoComplete]
        public bool m_blnMainReacordExist(string p_strICD10ID)
        {
            string str;
            int num;
            bool flag;
            string str2;
            Exception exception;
            string str3;
            clsLogText text;
            bool flag2;
            bool flag3;
            bool flag4;
            str = "";
            num = 0;
            flag = false;
        Label_000B:
            try
            {
                str2 = "Select ICD10ID  from ICD10_AssistantDiagnose where ICD10ID='" + p_strICD10ID + "' ";
                this.hrp.lngGetXMLTable(str2, ref str, ref num);
                if (num <= 0)
                {
                    goto Label_003F;
                }
                flag = true;
                goto Label_0041;
            Label_003F:
                flag = false;
            Label_0041:
                goto Label_0065;
            }
            catch (Exception exception1)
            {
            Label_0044:
                exception = exception1;
                str3 = exception.Message;
                text = new clsLogText();
                flag2 = text.LogError(exception);
                goto Label_0065;
            }
        Label_0065:
            flag3 = flag;
        Label_006B:
            return flag3;
        }

        [AutoComplete]
        public long m_lngAddNewICD10_AssistantDiagnose(string p_strXml)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(p_strXml))
            {
                goto Label_001B;
            }
            num2 = -1L;
            goto Label_0055;
        Label_001B:
            num = 0L;
        Label_001E:
            try
            {
                num = this.hrp.add_new_record("ICD10_AssistantDiagnose", p_strXml);
                goto Label_004F;
            }
            catch (Exception exception1)
            {
            Label_0034:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_004F;
            }
        Label_004F:
            num2 = num;
        Label_0055:
            return num2;
        }

        [AutoComplete]
        public long m_lngAddNewItem(string p_strICD10_AssistantDiagnoseItemXML, string p_strICD10_AssistantDiagnoseStandardXML, out string p_strID, out string p_strBeginItemDate)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            DateTime time;
            string[] strArray;
            p_strID = "";
            p_strBeginItemDate = "";
            if (!string.IsNullOrEmpty(p_strICD10_AssistantDiagnoseItemXML) && !string.IsNullOrEmpty(p_strICD10_AssistantDiagnoseStandardXML))
            {
                goto Label_003D;
            }
            num2 = -1L;
            goto Label_0157;
        Label_003D:
            num = 0L;
        Label_0040:
            try
            {
                p_strBeginItemDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                num = this.hrp.lngGenerateID(7, "AssistantDiagnoseItemID", "ICD10_AssistantDiagnoseItem", out p_strID);
                p_strICD10_AssistantDiagnoseItemXML = p_strICD10_AssistantDiagnoseItemXML.Insert(p_strICD10_AssistantDiagnoseItemXML.IndexOf(' ', 0), " AssistantDiagnoseItemID='" + p_strID + "' BeginItemDate='" + p_strBeginItemDate + "' ");
                num = this.hrp.add_new_record("ICD10_AssistantDiagnoseItem", p_strICD10_AssistantDiagnoseItemXML);
                if (num > 0L)
                {
                    goto Label_00D9;
                }
                num2 = num;
                goto Label_0157;
            Label_00D9:;
                p_strICD10_AssistantDiagnoseStandardXML = p_strICD10_AssistantDiagnoseStandardXML.Insert(p_strICD10_AssistantDiagnoseStandardXML.IndexOf(' ', 0), " AssistantDiagnoseItemID='" + p_strID + "' BeginStandardDate='" + p_strBeginItemDate + "' ");
                num = this.hrp.add_new_record("ICD10_AssistantDiagnoseStandard", p_strICD10_AssistantDiagnoseStandardXML);
                goto Label_0151;
            }
            catch (Exception exception1)
            {
            Label_0136:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0151;
            }
        Label_0151:
            num2 = num;
        Label_0157:
            return num2;
        }

        [AutoComplete]
        public long m_lngAddNewPutUpItem(string p_strXml, out string p_strID, out string p_strDate)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            DateTime time;
            string[] strArray;
            p_strID = "";
            p_strDate = "";
            if (!string.IsNullOrEmpty(p_strXml))
            {
                goto Label_002C;
            }
            num2 = -1L;
            goto Label_00DB;
        Label_002C:
            num = 0L;
        Label_002F:
            try
            {
                p_strDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                num = this.hrp.lngGenerateID(10, "PutUpItemID", "ICD10_AssistantDiagnoseClinicPutUp", out p_strID);
                p_strXml = p_strXml.Insert(p_strXml.IndexOf(' ', 0), " PutUpItemID='" + p_strID + "' BeginDate='" + p_strDate + "' ");
                num = this.hrp.add_new_record("ICD10_AssistantDiagnoseClinicPutUp", p_strXml);
                goto Label_00D5;
            }
            catch (Exception exception1)
            {
            Label_00BA:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00D5;
            }
        Label_00D5:
            num2 = num;
        Label_00DB:
            return num2;
        }

        [AutoComplete]
        public long m_lngDeleteICD10_AssistantDiagnose(string p_strICD10ID)
        {
            long num;
            string str;
            string str2;
            int num2;
            string str3;
            Exception exception;
            string str4;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            DateTime time;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strICD10ID))
            {
                goto Label_0027;
            }
            num3 = -1L;
            goto Label_0145;
        Label_0027:
            num = 0L;
        Label_002A:
            try
            {
                num = this.m_lngDeleteICD10_AssistantDiagnoseAlwaysSickness(p_strICD10ID);
                if (num <= 0)
                {
                    goto Label_0049;
                }
                num = this.m_lngDeleteICD10_AssistantDiagnoseCheckCode(p_strICD10ID);
            Label_0049:
                if (num <= 0)
                {
                    goto Label_005F;
                }
                num = this.m_lngDeletePutUpItem(p_strICD10ID);
            Label_005F:
                if (num <= 0)
                {
                    goto Label_0075;
                }
                num = this.m_lngDeleteItem(p_strICD10ID);
            Label_0075:
                if (num <= 0)
                {
                    goto Label_011B;
                }
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                str2 = "";
                num2 = 0;
                str3 = "Select * from ICD10_AssistantDiagnose  WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.lngGetXMLTable(str3, ref str2, ref num2);
                if (num2 <= 0)
                {
                    goto Label_011A;
                }
                str3 = "UPDATE ICD10_AssistantDiagnose SET Status = '1',DeActivedDate='" + str + "' WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.DoExcute(str3);
            Label_011A:;
            Label_011B:
                goto Label_013F;
            }
            catch (Exception exception1)
            {
            Label_011E:
                exception = exception1;
                str4 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_013F;
            }
        Label_013F:
            num3 = num;
        Label_0145:
            return num3;
        }

        [AutoComplete]
        private long m_lngDeleteICD10_AssistantDiagnoseAlwaysSickness(string p_strICD10ID)
        {
            long num;
            string str;
            string str2;
            int num2;
            string str3;
            Exception exception;
            string str4;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            DateTime time;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strICD10ID))
            {
                goto Label_0027;
            }
            num3 = -1L;
            goto Label_00E8;
        Label_0027:
            num = 0L;
        Label_002A:
            try
            {
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                str2 = "";
                num2 = 0;
                str3 = "Select * from ICD10_AssistantDiagnoseAlwaysSickness  WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.lngGetXMLTable(str3, ref str2, ref num2);
                if (num2 <= 0)
                {
                    goto Label_00BE;
                }
                str3 = "UPDATE ICD10_AssistantDiagnoseAlwaysSickness SET Status = '1',DeActivedDate='" + str + "' WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.DoExcute(str3);
            Label_00BE:
                goto Label_00E2;
            }
            catch (Exception exception1)
            {
            Label_00C1:
                exception = exception1;
                str4 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00E2;
            }
        Label_00E2:
            num3 = num;
        Label_00E8:
            return num3;
        }

        [AutoComplete]
        private long m_lngDeleteICD10_AssistantDiagnoseCheckCode(string p_strICD10ID)
        {
            long num;
            string str;
            string str2;
            int num2;
            string str3;
            Exception exception;
            string str4;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            DateTime time;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strICD10ID))
            {
                goto Label_0027;
            }
            num3 = -1L;
            goto Label_00E8;
        Label_0027:
            num = 0L;
        Label_002A:
            try
            {
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                str2 = "";
                num2 = 0;
                str3 = "Select * from ICD10_AssistantDiagnoseCheckCode  WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.lngGetXMLTable(str3, ref str2, ref num2);
                if (num2 <= 0)
                {
                    goto Label_00BE;
                }
                str3 = "UPDATE ICD10_AssistantDiagnoseCheckCode SET Status = '1',DeActivedDate='" + str + "' WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.DoExcute(str3);
            Label_00BE:
                goto Label_00E2;
            }
            catch (Exception exception1)
            {
            Label_00C1:
                exception = exception1;
                str4 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00E2;
            }
        Label_00E2:
            num3 = num;
        Label_00E8:
            return num3;
        }

        [AutoComplete]
        private long m_lngDeleteItem(string p_strICD10ID)
        {
            long num;
            string str;
            string str2;
            int num2;
            string str3;
            Exception exception;
            string str4;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            DateTime time;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strICD10ID))
            {
                goto Label_0027;
            }
            num3 = -1L;
            goto Label_016F;
        Label_0027:
            num = 0L;
        Label_002A:
            try
            {
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                str2 = "";
                num2 = 0;
                str3 = "Select * from ICD10_AssistantDiagnoseStandard  WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.lngGetXMLTable(str3, ref str2, ref num2);
                if (num2 <= 0)
                {
                    goto Label_00BE;
                }
                str3 = "UPDATE ICD10_AssistantDiagnoseStandard SET Status = '1',DeActivedDate='" + str + "' WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.DoExcute(str3);
            Label_00BE:
                if (num <= 0)
                {
                    goto Label_0145;
                }
                str3 = "Select * from ICD10_AssistantDiagnoseItem  WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.lngGetXMLTable(str3, ref str2, ref num2);
                if (num2 <= 0)
                {
                    goto Label_0144;
                }
                str3 = "UPDATE ICD10_AssistantDiagnoseItem SET Status = '1',DeActivedDate='" + str + "' WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.DoExcute(str3);
            Label_0144:;
            Label_0145:
                goto Label_0169;
            }
            catch (Exception exception1)
            {
            Label_0148:
                exception = exception1;
                str4 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0169;
            }
        Label_0169:
            num3 = num;
        Label_016F:
            return num3;
        }

        [AutoComplete]
        public long m_lngDeleteItem(string p_strAssistantDiagnoseItemID, string p_strBeginItemDate, string p_strICD10ID)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;

            if (!string.IsNullOrEmpty(p_strAssistantDiagnoseItemID) && !string.IsNullOrEmpty(p_strBeginItemDate) && !string.IsNullOrEmpty(p_strICD10ID))
            {
                goto Label_0047;
            }
            num2 = -1L;
            goto Label_0111;
        Label_0047:
            num = 0L;
        Label_004A:
            try
            {
                str = "UPDATE ICD10_AssistantDiagnoseStandard SET Status = '1' WHERE AssistantDiagnoseItemID = '" + p_strAssistantDiagnoseItemID + "' AND BeginStandardDate = '" + p_strBeginItemDate + "' AND ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                this.hrp.DoExcute(str);
                str = "UPDATE ICD10_AssistantDiagnoseItem SET Status = '1' WHERE AssistantDiagnoseItemID = '" + p_strAssistantDiagnoseItemID + "' AND BeginItemDate = '" + p_strBeginItemDate + "' AND ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.DoExcute(str);
                goto Label_010B;
            }
            catch (Exception exception1)
            {
            Label_00EE:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_010B;
            }
        Label_010B:
            num2 = num;
        Label_0111:
            return num2;
        }

        [AutoComplete]
        private long m_lngDeletePutUpItem(string p_strICD10ID)
        {
            long num;
            string str;
            string str2;
            int num2;
            string str3;
            Exception exception;
            string str4;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            DateTime time;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strICD10ID))
            {
                goto Label_0027;
            }
            num3 = -1L;
            goto Label_00E8;
        Label_0027:
            num = 0L;
        Label_002A:
            try
            {
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                str2 = "";
                num2 = 0;
                str3 = "Select * from ICD10_AssistantDiagnoseClinicPutUp  WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.lngGetXMLTable(str3, ref str2, ref num2);
                if (num2 <= 0)
                {
                    goto Label_00BE;
                }
                str3 = "UPDATE ICD10_AssistantDiagnoseClinicPutUp SET Status = '1',DeActivedDate='" + str + "' WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                num = this.hrp.DoExcute(str3);
            Label_00BE:
                goto Label_00E2;
            }
            catch (Exception exception1)
            {
            Label_00C1:
                exception = exception1;
                str4 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00E2;
            }
        Label_00E2:
            num3 = num;
        Label_00E8:
            return num3;
        }

        [AutoComplete]
        public long m_lngDeletePutUpItem(string p_strPutUpItemID, string p_strBeginDate)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strPutUpItemID) && !string.IsNullOrEmpty(p_strBeginDate))
            {
                goto Label_0031;
            }
            num2 = -1L;
            goto Label_009D;
        Label_0031:
            num = 0L;
        Label_0034:
            try
            {
                str = "UPDATE ICD10_AssistantDiagnoseClinicPutUp SET Status = '1' WHERE PutUpItemID = '" + p_strPutUpItemID + "' AND BeginDate = '" + p_strBeginDate + "' AND Status ='0' ";
                num = this.hrp.DoExcute(str);
                goto Label_0097;
            }
            catch (Exception exception1)
            {
            Label_007A:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0097;
            }
        Label_0097:
            num2 = num;
        Label_009D:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetAlwaysSickness(string p_strICD10ID, out string p_strXML, out int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strICD10ID))
            {
                goto Label_0030;
            }
            num2 = -1L;
            goto Label_007A;
        Label_0030:
            num = 0L;
        Label_0033:
            try
            {
                str = "Select a.AlwaysICD10ID  ,ISNULL(ISNULL(ISNULL(b1.IllnessName,b2.IllnessSubName),b3.IllnessDetailName),b4.OncologyName) as AlwaysICD10Name  from ICD10_AssistantDiagnoseAlwaysSickness a  LEFT OUTER join ICD10_IllnessID b1 ON a.AlwaysICD10ID=b1.IllnessID and (len(a.AlwaysICD10ID)=3)  LEFT OUTER join ICD10_IllnessSubID b2 ON a.AlwaysICD10ID=b2.IllnessSubID and (len(a.AlwaysICD10ID)=5)  LEFT OUTER join ICD10_IllnessDetailID b3 ON a.AlwaysICD10ID=b3.IllnessDetailID and (len(a.AlwaysICD10ID)=6)  LEFT OUTER join ICDO_ID b4 ON a.AlwaysICD10ID=b4.OncologyID and (len(a.AlwaysICD10ID)=7) where a.ICD10ID='" + p_strICD10ID + "' and a.Status=0";
                num = this.hrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0074;
            }
            catch (Exception exception1)
            {
            Label_0057:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0074;
            }
        Label_0074:
            num2 = num;
        Label_007A:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetCheckCodeID(string p_strICD10ID, out string p_strXML, out int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strICD10ID))
            {
                goto Label_0030;
            }
            num2 = -1L;
            goto Label_007A;
        Label_0030:
            num = 0L;
        Label_0033:
            try
            {
                str = "Select *  from ICD10_AssistantDiagnoseCheckCode where ICD10ID='" + p_strICD10ID + "' and Status=0";
                num = this.hrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0074;
            }
            catch (Exception exception1)
            {
            Label_0057:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0074;
            }
        Label_0074:
            num2 = num;
        Label_007A:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetClinicPutUpByICD10ID(string p_strICD10ID, out string p_strXML, out int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strICD10ID))
            {
                goto Label_0030;
            }
            num2 = -1L;
            goto Label_007A;
        Label_0030:
            num = 0L;
        Label_0033:
            try
            {
                str = "Select *  from ICD10_AssistantDiagnoseClinicPutUp where ICD10ID='" + p_strICD10ID + "' and Status=0";
                num = this.hrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0074;
            }
            catch (Exception exception1)
            {
            Label_0057:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0074;
            }
        Label_0074:
            num2 = num;
        Label_007A:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetGetAllCheckCharValue(out string p_strXML, out int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            p_strXML = "";
            p_intRows = 0;
            num = 0L;
        Label_000E:
            try
            {
                str = "Select *  from CheckCharValue where Status=0";
                num = this.hrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0044;
            }
            catch (Exception exception1)
            {
            Label_0027:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0044;
            }
        Label_0044:
            num2 = num;
        Label_004A:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetGetAllICD10_AssistantDiagnoseItemType(out string p_strXML, out int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            p_strXML = "";
            p_intRows = 0;
            num = 0L;
        Label_000E:
            try
            {
                str = "Select *  from ICD10_AssistantDiagnoseItemType where Status=0";
                num = this.hrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0044;
            }
            catch (Exception exception1)
            {
            Label_0027:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0044;
            }
        Label_0044:
            num2 = num;
        Label_004A:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetGetAllUnit(out string p_strXML, out int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            p_strXML = "";
            p_intRows = 0;
            num = 0L;
        Label_000E:
            try
            {
                str = "Select *  from Unit where Status=0";
                num = this.hrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0044;
            }
            catch (Exception exception1)
            {
            Label_0027:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0044;
            }
        Label_0044:
            num2 = num;
        Label_004A:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetItemByICD10ID(string p_strICD10ID, out string p_strXML, out int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strICD10ID) && p_strICD10ID.Trim() != "")
            {
                goto Label_0030;
            }
            num2 = -1L;
            goto Label_007A;
        Label_0030:
            num = 0L;
        Label_0033:
            try
            {
                str = "Select a.*,b.ValueRange,b.ValueType,b.ItemValueFrom,b.ItemValueTo,b.ResultCharID  from ICD10_AssistantDiagnoseItem a,ICD10_AssistantDiagnoseStandard b where a.ICD10ID='" + p_strICD10ID + "' and a.Status=0 and a.ICD10ID=b.ICD10ID and a.AssistantDiagnoseItemID=b.AssistantDiagnoseItemID  and a.BeginItemDate=b.BeginStandardDate and b.Status=0 ";
                num = this.hrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0074;
            }
            catch (Exception exception1)
            {
            Label_0057:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0074;
            }
        Label_0074:
            num2 = num;
        Label_007A:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetMainReacord(string p_strICD10ID, out string p_strXML, out int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            p_strXML = "";
            p_intRows = 0;
            if (!string.IsNullOrEmpty(p_strICD10ID) && p_strICD10ID.Trim() != "")
            {
                goto Label_0030;
            }
            num2 = -1L;
            goto Label_007A;
        Label_0030:
            num = 0L;
        Label_0033:
            try
            {
                str = "Select *  from ICD10_AssistantDiagnose where ICD10ID='" + p_strICD10ID + "' and Status=0";
                num = this.hrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0074;
            }
            catch (Exception exception1)
            {
            Label_0057:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0074;
            }
        Label_0074:
            num2 = num;
        Label_007A:
            return num2;
        }

        [AutoComplete]
        public long m_lngModifyICD10_AssistantDiagnose(string p_strXml)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strXml) && p_strXml.Trim() != "")
            {
                goto Label_001B;
            }
            num2 = -1L;
            goto Label_0068;
        Label_001B:
            num = 0L;
        Label_001E:
            try
            {
                num = this.hrp.modify_record("ICD10_AssistantDiagnose", p_strXml, new string[] { "ICD10ID" });
                goto Label_0062;
            }
            catch (Exception exception1)
            {
            Label_0047:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0062;
            }
        Label_0062:
            num2 = num;
        Label_0068:
            return num2;
        }

        [AutoComplete]
        public long m_lngModifyItem(string p_strICD10_AssistantDiagnoseItemXML, string p_strICD10_AssistantDiagnoseStandardXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strICD10_AssistantDiagnoseItemXML) && !string.IsNullOrEmpty(p_strICD10_AssistantDiagnoseStandardXML))
            {
                goto Label_002E;
            }
            num2 = -1L;
            goto Label_00D4;
        Label_002E:
            num = 0L;
        Label_0031:
            try
            {
                num = this.hrp.modify_record("ICD10_AssistantDiagnoseItem", p_strICD10_AssistantDiagnoseItemXML, new string[] { "ICD10ID", "AssistantDiagnoseItemID", "BeginItemDate" });
                if (num <= 0)
                {
                    goto Label_0079;
                }
                num2 = num;
                goto Label_00D4;
            Label_0079:;
                num = this.hrp.modify_record("ICD10_AssistantDiagnoseStandard", p_strICD10_AssistantDiagnoseStandardXML, new string[] { "ICD10ID", "AssistantDiagnoseItemID", "BeginStandardDate" });
                goto Label_00CE;
            }
            catch (Exception exception1)
            {
            Label_00B3:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_00CE;
            }
        Label_00CE:
            num2 = num;
        Label_00D4:
            return num2;
        }

        [AutoComplete]
        public long m_lngModifyPutUpItem(string p_strXml)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strXml) && p_strXml.Trim() != "")
            {
                goto Label_001B;
            }
            num2 = -1L;
            goto Label_0071;
        Label_001B:
            num = 0L;
        Label_001E:
            try
            {
                num = this.hrp.modify_record("ICD10_AssistantDiagnoseClinicPutUp", p_strXml, new string[] { "PutUpItemID", "BeginDate" });
                goto Label_006B;
            }
            catch (Exception exception1)
            {
            Label_0050:
                exception = exception1;
                str = exception.Message;
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
        public long m_lngSaveICD10_AssistantDiagnoseAlwaysSickness(string p_strICD10ID, string[] p_strAlwaysICD10IDArr)
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
            DateTime time;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strICD10ID) && p_strAlwaysICD10IDArr != null && p_strAlwaysICD10IDArr.Length > 0)
            {
                goto Label_002F;
            }
            num3 = -1L;
            goto Label_0126;
        Label_002F:
            num = 0L;
        Label_0032:
            try
            {
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                str2 = "UPDATE ICD10_AssistantDiagnoseAlwaysSickness SET Status = '1',DeActivedDate='" + str + "' WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                this.hrp.DoExcute(str2);
                num = 1L;
                num2 = 0;
                goto Label_00E8;
            Label_0090:;
                str2 = "INSERT INTO ICD10_AssistantDiagnoseAlwaysSickness(ICD10ID,AlwaysICD10ID,BeginDate,Status) VALUES( '" + p_strICD10ID + "','" + p_strAlwaysICD10IDArr[num2] + "','" + str + "' ,'0' )";
                num = this.hrp.DoExcute(str2);
                num2 += 1;
            Label_00E8:
                if (num2 >= p_strAlwaysICD10IDArr.Length && num > 0)
                {
                    goto Label_0090;
                }
                goto Label_0120;
            }
            catch (Exception exception1)
            {
            Label_00FF:
                exception = exception1;
                str3 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0120;
            }
        Label_0120:
            num3 = num;
        Label_0126:
            return num3;
        }

        [AutoComplete]
        public long m_lngSaveICD10_AssistantDiagnoseCheckCode(string p_strICD10ID, string[] p_strCheckCodeIDArr)
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
            DateTime time;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strICD10ID) && p_strCheckCodeIDArr != null && p_strCheckCodeIDArr.Length > 0)
            {
                goto Label_002F;
            }
            num3 = -1L;
            goto Label_0126;
        Label_002F:
            num = 0L;
        Label_0032:
            try
            {
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                str2 = "UPDATE ICD10_AssistantDiagnoseCheckCode SET Status = '1',DeActivedDate='" + str + "' WHERE ICD10ID = '" + p_strICD10ID + "' AND Status ='0' ";
                this.hrp.DoExcute(str2);
                num = 1L;
                num2 = 0;
                goto Label_00E8;
            Label_0090:;
                str2 = "INSERT INTO ICD10_AssistantDiagnoseCheckCode(ICD10ID,CheckCodeID,BeginDate,Status) VALUES( '" + p_strICD10ID + "','" + p_strCheckCodeIDArr[num2] + "','" + str + "' ,'0' )";
                num = this.hrp.DoExcute(str2);
                num2 += 1;
            Label_00E8:
                if (num2 >= p_strCheckCodeIDArr.Length && num > 0)
                {
                    goto Label_0090;
                }
                goto Label_0120;
            }
            catch (Exception exception1)
            {
            Label_00FF:
                exception = exception1;
                str3 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0120;
            }
        Label_0120:
            num3 = num;
        Label_0126:
            return num3;
        }
    }
}
