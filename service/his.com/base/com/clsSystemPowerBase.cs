//using System;
//using System.Data;
//using com.digitalwave.Utility;//Utility.dll
//using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll 
//using weCare.Core.Entity;
//using System.EnterpriseServices;
//using System.Collections;

//namespace com.digitalwave.iCare.middletier.HIS
//{
//    [Transaction(TransactionOption.Required)]
//    [ObjectPooling(Enabled = true)]
//    public class clsSystemPowerBase : com.digitalwave.iCare.middletier.clsMiddleTierBase
//    {
//        // Fields
//        private string strOperatorID;

//        // Methods
//        public clsSystemPowerBase(string p_strOperatorID)
//        {
//            this.strOperatorID = p_strOperatorID;
//        }

//        public bool isHasRight(string p_ModuleName)
//        {
//            return true;
//        }

//        [AutoComplete]
//        private bool m_lngGetMap(string p_strRoleID, string p_strModuleID)
//        {
//            string Sql = string.Empty;
//            DataTable dt = null;
//            clsHRPTableService svc = new clsHRPTableService();

//            Sql = "SELECT * FROM T_SYS_ROLEMODULEMAP WHERE ROLEID_CHR = '" + p_strRoleID + "' and MODULEID_CHR = '" + p_strModuleID + "'";
//            svc.lngGetDataTableWithoutParameters(Sql, ref dt);
//            if (dt != null && dt.Rows.Count > 0)
//                return true;
//            else
//                return false;
//        }

//        [AutoComplete]
//        private bool m_lngGetModule(string p_strMODULEID)
//        {
//            string Sql = string.Empty;
//            DataTable dt = null;
//            clsHRPTableService svc = new clsHRPTableService();

//            Sql = "SELECT * FROM T_SYS_MODULE WHERE MODULEID_CHR = '" + p_strMODULEID + "'";
//            svc.lngGetDataTableWithoutParameters(Sql, ref dt);
//            if (dt != null && dt.Rows.Count > 0)
//                return true;
//            else
//                return false;
//        }

//        [AutoComplete]
//        private bool m_lngGetRoleModuleMap(string p_strOperatorID, string p_strModuleID)
//        {
//            string Sql = string.Empty;
//            DataTable dt = null;
//            clsHRPTableService svc = new clsHRPTableService();

//            Sql = "SELECT * FROM T_SYS_EMPROLEMAP WHERE EMPID_CHR = '" + p_strOperatorID + "'";
//            svc.lngGetDataTableWithoutParameters(Sql, ref dt);
//            if (dt != null && dt.Rows.Count > 0)
//            {
//                Sql = "SELECT * FROM T_BSE_POPEDOMRIGHT WHERE ROLEID_CHR = '" + dt.Rows[0]["ROLEID_CHR"].ToString() + "' AND POPEDOMID_CHR = '" + p_strModuleID + "'";
//                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
//                if (dt != null && dt.Rows.Count > 0)
//                    return true;
//                else
//                    return false;
//            }
//            else
//                return false;
//        }

//        private string m_strGetFullName(object p_objName, string p_strFunctionName)
//        {
//            if (p_objName == null)
//                return "";
//            else
//                return p_objName.ToString() + "." + p_strFunctionName;
//        }

//        private string m_strGetModuleID(string p_ModuleName)
//        {
//            string Sql = string.Empty;
//            DataTable dt = null;
//            clsHRPTableService svc = new clsHRPTableService();

//            Sql = "SELECT * FROM T_BSE_POPEDOM WHERE FERIXNAME_VCHR = '" + p_ModuleName + "'";
//            svc.lngGetDataTableWithoutParameters(Sql, ref dt);
//            if (dt != null && dt.Rows.Count > 0)
//                return dt.Rows[0]["POPEDOMID_CHR"].ToString();
//            else
//                return "";
//        }
//    }
//}
