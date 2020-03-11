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
    public class clsInPatientBedManagerService : com.digitalwave.iCare.middletier.clsMiddleTierBase
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
		public long m_lngGetBedInfo( string p_strBedID,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 

                string strSQL = @"select code_chr as bed_name
									from t_bse_bed
									where bedid_chr = ?";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBedID;

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