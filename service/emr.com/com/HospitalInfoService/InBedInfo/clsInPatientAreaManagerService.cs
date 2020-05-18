using System;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;

namespace com.digitalwave.InBedInfoManagerService
{
	/// <summary>
	/// 的中间件�?
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsInPatientAreaManagerService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strResultXml">返回的结�?/param>
		/// <param name="p_intResultRows">记录的数�?/param>
		/// <returns>
		/// 操作结果�?
		/// 0：失败�?
		/// 1：成功�?
		/// </returns>
		[AutoComplete]
		public long m_lngGetAreaInfo( string p_strAreaID,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 

                //				string strSQL = @"select IPA.Begin_Date_Area,IPAD.Area_Name
                //									from InPatient_Area IPA,InPatient_Area_Desc IPAD
                //									where IPA.Area_ID = IPAD.Area_ID
                //									and IPA.Area_ID = '"+p_strAreaID+@"'
                //									and IPA.End_Date_Area = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //									and IPAD.End_Date_Area_Naming = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat();
                //使用新表 modified by tfzhang at 2005�?0�?7�?15:11:52
                string strSQL = @"select t.createdate_dat as begin_date_area, t.deptname_vchr as area_name
									from t_bse_deptdesc t
									where t.deptid_chr  = ?";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strAreaID;

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			//返回
			return lngRes;
		}
	}
}