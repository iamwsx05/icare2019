using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility;

namespace com.digitalwave.AtlasForChecksLab
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsAtlasForChecksLabServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        // Fields
        private clsHRPTableService objHrp;

        // Methods
        public clsAtlasForChecksLabServ()
        {
            this.objHrp = new clsHRPTableService();
        }

        [AutoComplete]
        public long m_lngAdd_new_record(string p_strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(p_strXML))
            {
                goto Label_0014;
            }
            num2 = -1L;
            goto Label_004E;
        Label_0014:
            num = 0L;
        Label_0017:
            try
            {
                num = this.objHrp.add_new_record("LIBRARY_KNOTYPEFCL", p_strXML);
                goto Label_0048;
            }
            catch (Exception exception1)
            {
            Label_002D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0048;
            }
        Label_0048:
            num2 = num;
        Label_004E:
            return num2;
        }

        [AutoComplete]
        public long m_lngAddNewItemrecord(string p_strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(p_strXML))
            {
                goto Label_0014;
            }
            num2 = -1L;
            goto Label_004E;
        Label_0014:
            num = 0L;
        Label_0017:
            try
            {
                num = this.objHrp.add_new_record("LIBRARY_KNOITEMFCL", p_strXML);
                goto Label_0048;
            }
            catch (Exception exception1)
            {
            Label_002D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0048;
            }
        Label_0048:
            num2 = num;
        Label_004E:
            return num2;
        }

        [AutoComplete]
        public long m_lngAddPictureServ(string p_strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            if (!string.IsNullOrEmpty(p_strXML))
            {
                goto Label_0014;
            }
            num2 = -1L;
            goto Label_004E;
        Label_0014:
            num = 0L;
        Label_0017:
            try
            {
                num = this.objHrp.add_new_record("LIBRARY_KNOPICFCL", p_strXML);
                goto Label_0048;
            }
            catch (Exception exception1)
            {
            Label_002D:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0048;
            }
        Label_0048:
            num2 = num;
        Label_004E:
            return num2;
        }

        [AutoComplete]
        public long m_lngDeleteAtlasItem(string p_strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strXML))
            {
                goto Label_0014;
            }
            num2 = -1L;
            goto Label_0061;
        Label_0014:
            num = 0L;
        Label_0017:
            try
            {
                num = this.objHrp.modify_record("LIBRARY_KNOITEMFCL", p_strXML, new string[] { "AtlasItemID" });
                goto Label_005B;
            }
            catch (Exception exception1)
            {
            Label_0040:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_005B;
            }
        Label_005B:
            num2 = num;
        Label_0061:
            return num2;
        }

        [AutoComplete]
        public long m_lngDeleteAtlasType(string p_strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strXML))
            {
                goto Label_0014;
            }
            num2 = -1L;
            goto Label_0061;
        Label_0014:
            num = 0L;
        Label_0017:
            try
            {
                num = this.objHrp.modify_record("LIBRARY_KNOTYPEFCL", p_strXML, new string[] { "AtlasTypeID" });
                goto Label_005B;
            }
            catch (Exception exception1)
            {
            Label_0040:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_005B;
            }
        Label_005B:
            num2 = num;
        Label_0061:
            return num2;
        }

        [AutoComplete]
        public long m_lngDeletePicture(string p_strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strXML))
            {
                goto Label_0014;
            }
            num2 = -1L;
            goto Label_0061;
        Label_0014:
            num = 0L;
        Label_0017:
            try
            {
                num = this.objHrp.modify_record("LIBRARY_KNOPICFCL", p_strXML, new string[] { "PictruesID" });
                goto Label_005B;
            }
            catch (Exception exception1)
            {
            Label_0040:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_005B;
            }
        Label_005B:
            num2 = num;
        Label_0061:
            return num2;
        }

        [AutoComplete]
        public long m_lngDeletePictureByItemID(string p_strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strXML))
            {
                goto Label_0014;
            }
            num2 = -1L;
            goto Label_0061;
        Label_0014:
            num = 0L;
        Label_0017:
            try
            {
                num = this.objHrp.modify_record("LIBRARY_KNOPICFCL", p_strXML, new string[] { "AtlasItemID" });
                goto Label_005B;
            }
            catch (Exception exception1)
            {
            Label_0040:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_005B;
            }
        Label_005B:
            num2 = num;
        Label_0061:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetAtlasItemAll(ref string p_strXML, ref int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            p_strXML = null;
            p_intRows = 0;
            num = 0L;
        Label_000A:
            try
            {
                str = "SELECT * FROM LIBRARY_KNOITEMFCL WHERE Status = '0'  ";
                num = this.objHrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0040;
            }
            catch (Exception exception1)
            {
            Label_0023:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0040;
            }
        Label_0040:
            num2 = num;
        Label_0046:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetAtlasPictureAll(string strItemID, ref string p_strXML, ref int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            p_strXML = null;
            p_intRows = 0;
            num = 0L;
        Label_000A:
            try
            {
                str = "SELECT * FROM LIBRARY_KNOPICFCL WHERE Status = '0' and AtlasItemID= '" + strItemID + "'";
                num = this.objHrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_004B;
            }
            catch (Exception exception1)
            {
            Label_002E:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_004B;
            }
        Label_004B:
            num2 = num;
        Label_0051:
            return num2;
        }

        [AutoComplete]
        public long m_lngGetAtlasTypeAll(ref string p_strXML, ref int p_intRows)
        {
            long num;
            string str;
            Exception exception;
            string str2;
            clsLogText text;
            bool flag;
            long num2;
            p_strXML = null;
            p_intRows = 0;
            num = 0L;
        Label_000A:
            try
            {
                str = "SELECT * FROM LIBRARY_KNOTYPEFCL WHERE Status = '0'  ";
                num = this.objHrp.lngGetXMLTable(str, ref p_strXML, ref p_intRows);
                goto Label_0040;
            }
            catch (Exception exception1)
            {
            Label_0023:
                exception = exception1;
                str2 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0040;
            }
        Label_0040:
            num2 = num;
        Label_0046:
            return num2;
        }

        [AutoComplete]
        public long m_lngModifyItem(string p_strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strXML))
            {
                goto Label_0014;
            }
            num2 = -1L;
            goto Label_0061;
        Label_0014:
            num = 0L;
        Label_0017:
            try
            {
                num = this.objHrp.modify_record("LIBRARY_KNOITEMFCL", p_strXML, new string[] { "AtlasItemID" });
                goto Label_005B;
            }
            catch (Exception exception1)
            {
            Label_0040:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_005B;
            }
        Label_005B:
            num2 = num;
        Label_0061:
            return num2;
        }

        [AutoComplete]
        public long m_lngModifyPictureServ(string p_strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strXML))
            {
                goto Label_0014;
            }
            num2 = -1L;
            goto Label_0061;
        Label_0014:
            num = 0L;
        Label_0017:
            try
            {
                num = this.objHrp.modify_record("LIBRARY_KNOPICFCL", p_strXML, new string[] { "PictruesID" });
                goto Label_005B;
            }
            catch (Exception exception1)
            {
            Label_0040:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_005B;
            }
        Label_005B:
            num2 = num;
        Label_0061:
            return num2;
        }

        [AutoComplete]
        public long m_lngModifyType(string p_strXML)
        {
            long num;
            Exception exception;
            string str;
            clsLogText text;
            bool flag;
            long num2;
            bool flag2;
            string[] strArray;
            if (!string.IsNullOrEmpty(p_strXML))
            {
                goto Label_0014;
            }
            num2 = -1L;
            goto Label_0061;
        Label_0014:
            num = 0L;
        Label_0017:
            try
            {
                num = this.objHrp.modify_record("LIBRARY_KNOTYPEFCL", p_strXML, new string[] { "AtlasTypeID" });
                goto Label_005B;
            }
            catch (Exception exception1)
            {
            Label_0040:
                exception = exception1;
                str = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_005B;
            }
        Label_005B:
            num2 = num;
        Label_0061:
            return num2;
        }
    }
}
