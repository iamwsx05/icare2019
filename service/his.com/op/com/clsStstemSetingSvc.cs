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
	/// clsRegChargeTypeSvc 的摘要说明。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]

	public class clsStstemSetingSvc:com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsStstemSetingSvc()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 获得模块信息 xigui.peng 2005-12-3
		/// <summary>
		/// 获得模块信息 xigui.peng 2005-12-3
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_dtInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLeftInfo( out System.Data.DataTable p_dtInfo)
		{
		  string strSQL;
			p_dtInfo= new DataTable();
			long lngRegs=0; 
            strSQL = "select moduleid_chr, modulename_chr, engname_chr, pycode_chr, wbcode_chr, order_int from t_sys_modulelist order by order_int,moduleid_chr";
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRegs = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtInfo);
				objHRPSvc.Dispose();	
			  
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRegs;

		}
		#endregion
		#region 系统功能设置列表 张国良   2005-2-23
		/// <summary>
		/// 系统功能设置列表 张国良   2005-2-23
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_tabReport"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngStstemSeting( out System.Data.DataTable p_tabReport)
		{
			string strSQL;
			p_tabReport=new DataTable();
			long lngRes = 0; 
            strSQL = "select setid_chr, setname_vchr, setdesc_vchr, setstatus_int, moduleid_chr from t_sys_setting order by setid_chr";
			try
			{
				
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_tabReport);
				objHRPSvc.Dispose();	
						
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;

			
		}
		#endregion

		#region 修改系统功能设置 张国良   2005-2-23
		/// <summary>
		/// 修改系统功能设置 张国良   2005-2-23
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_tabReport"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyStstemSeting( string p_strID,string p_strStatus,string p_strModuleID)
		{
			string strSQL;
			long lngRes = 0; 

			strSQL="UPDATE t_sys_setting "+
					"SET setstatus_int = "+p_strStatus+" "+
					"WHERE setid_chr = '"+p_strID+"'"+" and MODULEID_CHR='"+p_strModuleID+"'";
			try
			{
				
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.DoExcute(strSQL);
				objHRPSvc.Dispose();	
						
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;

			
		}
		#endregion

	}

}
