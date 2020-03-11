using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity; 
using com.digitalwave.Utility.SQLConvert;
using System.Data;

namespace com.digitalwave.DomainUserLoginInfoServ
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsDomainUserLoginInfoServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        //添加用户登陆信息
        [AutoComplete]
        public long m_lngAddDomainUserLoginInfo( string p_strXml, out string p_strDateTimeNow)
        {
            long res = -1;
            p_strDateTimeNow = "";
            clsHRPTableService clsTabService = new clsHRPTableService();
            try
            {
                p_strDateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (p_strXml == null || p_strXml == "")
                    return (long)enmOperationResult.Parameter_Error; 

                p_strXml = p_strXml.Replace("LoginDateTime=''", "LoginDateTime=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strDateTimeNow) + ")");
                res = clsTabService.add_new_record("EmployeeLoginInfo", p_strXml);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //clsTabService.Dispose();
            }
            return res;
        }

        //修改用户登陆信息
        [AutoComplete]
        public long m_lngModifyDomainUserLoginInfo( string p_strEmployeeID, string p_strLoginDateTime, string p_strIPAddress)
        {
            long res = -1;
            string strCommand = "";
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (p_strEmployeeID == null || p_strEmployeeID == "" || p_strLoginDateTime == null || p_strLoginDateTime == "" || p_strIPAddress == null || p_strIPAddress == "")
                    return (long)enmOperationResult.Parameter_Error;
                string strDateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = @"update employeelogininfo set leavedatetime=?  where employeeid=? 
                         and logindatetime=? and ipaddress=? ";
                else
                    strCommand = @"update employeelogininfo set leavedatetime=?  where employeeid=? 
                         and logindatetime=? and ipaddress=? ";

                IDataParameter[] objDPArr = null;
                clsTablService.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(strDateTimeNow);
                objDPArr[1].Value = p_strEmployeeID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strLoginDateTime);
                objDPArr[3].Value = p_strIPAddress;

                long lngEff = -1;
                res = clsTablService.lngExecuteParameterSQL(strCommand, ref lngEff,objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //clsTablService.Dispose();
            }
            return res;
        }
    }
}
