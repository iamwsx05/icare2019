using System;
using System.Data;
using System.EnterpriseServices;
using Microsoft.VisualBasic;
using com.digitalwave.Utility; //Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.LIS
{
	/// <summary>
	/// Summary description for clsBaseCheckGroupSvc.
	/// 
	/// ��װ�йػ��������������ͨѸ�߼�
	/// </summary>
	[Transaction(TransactionOption.Supported)]
	[ObjectPooling(Enabled=true)]
	public class clsBaseCheckGroupSvc:com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsBaseCheckGroupSvc()
		{
			//
			// TODO: Add constructor logic here
			//
		}



		#region ��������������ID��ѯ���п������Ļ��������� 
		/// <summary>
		/// ��������������ID��ѯ���п������Ļ��������� 
		/// </summary>
		/// <param name="p_objPrinipal"></param>
		/// <param name="p_strSampleTypeID"></param>
		/// <param name="p_dtbGroup"></param>
		/// <returns>
		/// GROUPID_CHR
		/// GROUPNAME_VCHR
		/// </returns>
		[AutoComplete]
		public long m_lngGetBaseCheckGroupBySampleTypeID( string p_strSampleTypeID,out System.Data.DataTable p_dtbGroup)
		{
			long lngRes=0;
			p_dtbGroup=null;
			string strSQL=@"SELECT t1.groupid_chr, t1.groupname_vchr 
							FROM t_aid_lis_check_group t1								
							WHERE t1.has_subgroup_chr = '0' 
							AND t1.sample_type_id_chr = ? 
							";

			try
			{
				System.Data.IDataParameter[] objParamArr = clsIDataParameterCreator.m_objConstructIDataParameterArr(p_strSampleTypeID);
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref p_dtbGroup,objParamArr);  
				objHRPSvc.Dispose();
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
			}
			return lngRes;
		}
		#endregion

		#region ���ݻ���������ID�������������������е���������ID 
		/// <summary>
		/// ���ݻ���������ID�������������������е���������ID ���� 2004.05.07
		/// </summary>
		/// <param name="p_objPrinipal"></param>
		/// <param name="p_strBaseCheckGroupID"></param>
		/// <param name="p_dtbDeviceModel"></param>
		/// <returns>
		/// DEVICE_MODEL_ID_CHR
		/// DEVICE_MODEL_DESC_VCHR
		/// </returns>
		[AutoComplete]
		public long m_lngGetDeviceModelByBaseCheckGroupID( string p_strBaseCheckGroupID,out System.Data.DataTable p_dtbDeviceModel)
		{
			long lngRes = 0;
			p_dtbDeviceModel = null;
			string strSQL=@"SELECT DISTINCT t1.device_model_id_chr, t2.device_model_desc_vchr 
							FROM t_bse_lis_check_item_dev_item t1 ,
								 t_bse_lis_device_model t2							
							WHERE t1.device_model_id_chr = t2.device_model_id_chr 
							AND t1.groupid_chr = ? 
							";

			try
			{
				System.Data.IDataParameter[] objParamArr = clsIDataParameterCreator.m_objConstructIDataParameterArr(p_strBaseCheckGroupID);
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref p_dtbDeviceModel,objParamArr);  
				objHRPSvc.Dispose();
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
			}
			return lngRes;
		}
		#endregion
	}
}
