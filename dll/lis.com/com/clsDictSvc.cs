using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility; //Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
namespace com.digitalwave.iCare.middletier.LIS
{
	/// <summary>
	/// clsDictSvc ��ժҪ˵����
	/// 
	/// </summary>
	[Transaction(TransactionOption.Supported)]
	[ObjectPooling(Enabled=true)]
	public class clsDictSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase//MiddleTierBase.dll
	{
		#region ���캯��
		public clsDictSvc()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion

		#region �����ֵ�����õ������б�(��ȥ��һ��������˵��) 
		/// <summary>
		/// �����ֵ�����õ������б�(��ȥ��һ��������˵��) 
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDictKind"></param>
		/// <param name="p_dtbDictList">
		/// table name : t_aid_dict
		/// column:
		/// dictid_chr
		/// dictkind_chr
		/// dictname_vchr
		/// pycode_chr
		/// wbcode_chr
		/// jxcode_chr
		/// </param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDictListFor(string p_strDictKind, out DataTable p_dtbDictList)
		{
			long lngRes = 0;
			p_dtbDictList = null; 

			com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

			string strSQL = @"SELECT jxcode_chr,wbcode_chr, pycode_chr, dictname_vchr, dictid_chr,dictkind_chr 
							 FROM t_aid_dict 
							 WHERE 
							 trim(dictid_chr) <> '0' 
							 AND dictkind_chr = '" + p_strDictKind + "'";

			try
			{
		//		IDataParameter[] objParamArr = clsIDataParameterCreator.m_objConstructIDataParameterArr(p_strDictKind);
		//		lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref p_dtbDictList,objParamArr);		
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbDictList);
			objHRPSvc.Dispose();
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
				lngRes = 0;
			}
			return lngRes;
		}
		#endregion

		#region �����ֵ�����õ������б���ȥ��һ������˵���� 
		[AutoComplete]
		public long m_lngGetDictListFor(string p_strDictKind,out clsAIDDICT_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = null; 

			string strSQL = @"SELECT * 
								FROM t_aid_dict 
							   WHERE trim(dictid_chr) <> '0' 
								 AND dictkind_chr = '" + p_strDictKind + "'";
			try
			{
				DataTable dtbResult = new DataTable();
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
				objHRPSvc.Dispose();
				if(lngRes > 0 && dtbResult.Rows.Count > 0)
				{
					p_objResultArr = new clsAIDDICT_VO[dtbResult.Rows.Count];
					for(int i1=0;i1<p_objResultArr.Length;i1++)
					{
						p_objResultArr[i1] = new clsAIDDICT_VO();
						p_objResultArr[i1].m_strJXCODE_CHR = dtbResult.Rows[i1]["JXCODE_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strWBCODE_CHR = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strDICTNAME_VCHR = dtbResult.Rows[i1]["DICTNAME_VCHR"].ToString().Trim();
						p_objResultArr[i1].m_strDICTID_CHR = dtbResult.Rows[i1]["DICTID_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strDICTKIND_CHR = dtbResult.Rows[i1]["DICTKIND_CHR"].ToString().Trim();
						p_objResultArr[i1].m_strDICTSEQID_CHR = dtbResult.Rows[i1]["DICTSEQID_CHR"].ToString().Trim();
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}
		#endregion
	}
}
