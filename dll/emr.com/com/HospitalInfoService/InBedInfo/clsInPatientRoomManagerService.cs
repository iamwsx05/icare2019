using System;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.InBedInfoManagerService
{
	/// <summary>
	/// 的中间件�?
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsInPatientRoomManagerService : com.digitalwave.iCare.middletier.clsMiddleTierBase
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
		public long m_lngGetRoomInfo( string p_strRoomID,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 

                string strSQL = @"select ipr.begin_date_room,iprd.room_name
									from inpatient_room ipr,inpatient_room_desc iprd
									where ipr.room_id = iprd.room_id
									and ipr.room_id = '" + p_strRoomID + @"'
									and ipr.end_date_room = " + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @"
									and iprd.end_date_room_naming = " + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat();

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

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