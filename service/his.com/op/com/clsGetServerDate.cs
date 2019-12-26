using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;
using System.Collections;
namespace com.digitalwave.iCare.middletier.HIS
{
	/// <summary>
	/// ��ȡ���ݿ������ʱ��
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsGetServerDate:com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// ��ȡ���ݿ������ʱ��
		/// </summary>
		/// <returns></returns>
		[AutoComplete]
		public static System.DateTime s_GetServerDate()
		{
			long lngRes = 0;
			System.DateTime datResult = System.DateTime.Now;
			string strSQL = "";
			if(clsHRPTableService.bytDatabase_Selector == 2)
			{
				strSQL=	@"SELECT sysdate
							  FROM dual";
			}
			else
			{
				strSQL=	@"SELECT getdate() as sysdate";
			}
			System.Data.DataTable dtbResult = new System.Data.DataTable();
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbResult);
				objHRPSvc.Dispose();

				if(lngRes >0 && dtbResult !=null)
				{
					datResult = System.DateTime.Parse(dtbResult.Rows[0]["sysdate"].ToString());
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx); 
			}
			return datResult;
		}


		/// <summary>
		/// ��ȡ���ݿ������ʱ��
		/// </summary>
		/// <returns></returns>
		[AutoComplete]
		public System.DateTime m_GetServerDate()
		{
			long lngRes = 0;
			System.DateTime datResult = System.DateTime.Now;
			
			string strSQL = "";
			if(clsHRPTableService.bytDatabase_Selector == 2)
			{
				strSQL = @"SELECT sysdate
							  FROM dual";
			}
			else
			{
				strSQL=	@"SELECT getdate() as sysdate";
			}
			System.Data.DataTable dtbResult = new System.Data.DataTable();
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbResult);
				objHRPSvc.Dispose();

				if(lngRes >0 && dtbResult !=null)
				{
					datResult = System.DateTime.Parse(dtbResult.Rows[0]["sysdate"].ToString());
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx); 
			}
			return datResult;
		}
	}
}
