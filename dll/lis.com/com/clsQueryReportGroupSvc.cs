using System;
using System.Data;
using System.EnterpriseServices;
using Microsoft.VisualBasic;
using com.digitalwave.Utility; //Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.LIS
{
    [Transaction(TransactionOption.Supported)]
	[ObjectPooling(Enabled=true)]
    public class clsQueryReportGroupSvc: com.digitalwave.iCare.middletier.clsMiddleTierBase//MiddleTierBase.dll
    {
        #region ���캯��
        public clsQueryReportGroupSvc()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion

        #region ���ݱ�����ID��ȡ������VO
		[AutoComplete]
		public long m_lngGetReportGroupVOByReportGroupID(string p_strReportGroupID,
			out clsReportGroup_VO p_objResultVO)
		{
			long lngRes = 0;
			p_objResultVO = null; 

			string strSQL = @"SELECT * FROM T_AID_LIS_REPORT_GROUP WHERE report_group_id_chr = '"+p_strReportGroupID+"'";
			DataTable dtbResult = new DataTable();
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
				objHRPSvc.Dispose();
				if(lngRes > 0 && dtbResult.Rows.Count > 0)
				{
					p_objResultVO = new clsReportGroup_VO();
					ConstructReportGroupVO(dtbResult.Rows[0],ref p_objResultVO);
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
			}
			return lngRes;
		}
		#endregion

        #region ���ݼ���걾��ID�õ������ڱ������VO 
		/// <summary>
		/// ���ݼ���걾��ID�õ������ڱ������VO ���� 2004.06.1
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strCheckItemID"></param>
		/// <param name="p_objSampleGroupVO"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetReportGoupVOBySampleGroupID(string p_strSampleGroupID,out clsReportGroup_VO p_objReportGroupVO)
		{
			long lngRes = 0;
			p_objReportGroupVO = null; 

			string strSQL = @"SELECT t1.* 
								FROM t_aid_lis_report_group t1,t_aid_lis_report_group_detail t2
								WHERE t1.report_group_id_chr = t2.report_group_id_chr
								AND t2.sample_group_id_chr = '" + p_strSampleGroupID+ "'";

			DataTable dtbRet = null;
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbRet);
				objHRPSvc.Dispose();
				if(lngRes > 0 && dtbRet != null)
				{
					if(dtbRet.Rows.Count > 0)
					{
						p_objReportGroupVO = new clsReportGroup_VO();
						
						ConstructReportGroupVO(dtbRet.Rows[0],ref p_objReportGroupVO);
						
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
			}
			return lngRes;
		}
		#endregion

        #region ��ȡ���еı�������ϸ 
		[AutoComplete]
		public long m_lngGetAllReportGroupDetail(out clsReportGroupDetail_VO[] objReportGroupVOList)
		{
			long lngRes = 0;
			objReportGroupVOList = null; 

			string strSQL = @"SELECT * FROM t_aid_lis_report_group_detail";
			DataTable dtbReportDetail = null;
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbReportDetail);
				objHRPSvc.Dispose();
				if(dtbReportDetail != null && dtbReportDetail.Rows.Count > 0)
				{
					objReportGroupVOList = new clsReportGroupDetail_VO[dtbReportDetail.Rows.Count];
					for(int i=0;i<dtbReportDetail.Rows.Count;i++)
					{
						objReportGroupVOList[i] = new clsReportGroupDetail_VO();
						ConstructReportGroupDetail(dtbReportDetail.Rows[i],ref objReportGroupVOList[i]);
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}
		#endregion

        #region ����ReportGroupDetailVO 
        [AutoComplete]
		private void ConstructReportGroupDetail(System.Data.DataRow objRow,ref clsReportGroupDetail_VO objReportGroupDetailVO)
		{
			objReportGroupDetailVO.strReportGroupID = objRow["REPORT_GROUP_ID_CHR"].ToString().Trim();
			objReportGroupDetailVO.strSampleGroupID = objRow["SAMPLE_GROUP_ID_CHR"].ToString().Trim();
			objReportGroupDetailVO.strPrintSeq = objRow["PRINT_SEQ_INT"].ToString().Trim();
		}
		#endregion

        #region ��ȡ���еı�����
		[AutoComplete]
		public long m_lngGetAllReportGroup(ref clsReportGroup_VO[] objReportGroupList)
		{
			long lngRes = 0; 
			string strSQL = @"SELECT * FROM T_AID_LIS_REPORT_GROUP ORDER BY REPORT_GROUP_ID_CHR";
			DataTable dtbReportGroup = null;
			objReportGroupList = null;
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbReportGroup);
				objHRPSvc.Dispose();
				if(lngRes > 0 && dtbReportGroup != null)
				{
					if(dtbReportGroup.Rows.Count > 0)
					{
						objReportGroupList = new clsReportGroup_VO[dtbReportGroup.Rows.Count];
						for(int i=0;i<dtbReportGroup.Rows.Count;i++)
						{
							objReportGroupList[i] = new clsReportGroup_VO();
							ConstructReportGroupVO(dtbReportGroup.Rows[i],ref objReportGroupList[i]);
							m_lngGetReportGroupDetail( objReportGroupList[i].strReportGroupID,ref objReportGroupList[i].objSampleGroupVO);
						}
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}
		#endregion

        #region ����ReportGroupVO 
        [AutoComplete]
		private void ConstructReportGroupVO(System.Data.DataRow objRow,ref clsReportGroup_VO objReportGroupVO)
		{
			objReportGroupVO.strReportGroupID = objRow["REPORT_GROUP_ID_CHR"].ToString().Trim();
			objReportGroupVO.strReportGroupName = objRow["REPORT_GROUP_NAME_VCHR"].ToString().Trim();
			objReportGroupVO.strPYCode = objRow["PY_CODE_CHR"].ToString().Trim();
			objReportGroupVO.strPrintTitle = objRow["PRINT_TITLE_VCHR"].ToString().Trim();
			objReportGroupVO.strWBCode = objRow["WB_CODE_CHR"].ToString().Trim();
			objReportGroupVO.strAssistCode01 = objRow["ASSIST_CODE01_CHR"].ToString().Trim();
			objReportGroupVO.strAssistCode02 = objRow["ASSIST_CODE02_CHR"].ToString().Trim();
			objReportGroupVO.strPrintCategoryID = objRow["PRINT_CATEGORY_ID_CHR"].ToString().Trim();
		}
		#endregion

        #region ��ȡĳһ�������µı걾����ϸ 
		[AutoComplete]
		public long m_lngGetReportGroupDetail(string strReportGroupID,ref clsSampleGroup_VO[] objSampleGroupList)
		{
			long lngRes = 0; 

			string strSQL = @"SELECT t2.*
							    FROM t_aid_lis_report_group_detail t1,
									 t_aid_lis_sample_group t2 
							   WHERE t1.SAMPLE_GROUP_ID_CHR = t2.SAMPLE_GROUP_ID_CHR
								 AND t1.REPORT_GROUP_ID_CHR = '"+strReportGroupID+@"'
							   ORDER BY t1.PRINT_SEQ_INT";
			DataTable dtbReportGroupDetail = null;
			objSampleGroupList = null;
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbReportGroupDetail);
				objHRPSvc.Dispose();
				if(lngRes > 0 && dtbReportGroupDetail != null)
				{
					if(dtbReportGroupDetail.Rows.Count > 0)
					{
						objSampleGroupList = new clsSampleGroup_VO[dtbReportGroupDetail.Rows.Count];
						for(int i=0;i<dtbReportGroupDetail.Rows.Count;i++)
						{
							objSampleGroupList[i] = new clsSampleGroup_VO();
							ConstructSampleGroupVO(dtbReportGroupDetail.Rows[i],ref objSampleGroupList[i]);
						}
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}
		#endregion

        #region ����clsReportGroupDetail_VO 
        [AutoComplete]
		private void ConstructReportGroupDetailVO(System.Data.DataRow objRow,ref clsReportGroupDetail_VO objReportGroupDetail)
		{
			objReportGroupDetail.strReportGroupID = objRow["REPORT_GROUP_ID_CHR"].ToString().Trim();
			objReportGroupDetail.strSampleGroupID = objRow["SAMPLE_GROUP_ID_CHR"].ToString().Trim();
			objReportGroupDetail.strPrintSeq = objRow["PRINT_SEQ_INT"].ToString().Trim();
		}
		#endregion

		#region ����clsSampleGroupVO 
        [AutoComplete]
		private void ConstructSampleGroupVO(System.Data.DataRow objRow, ref clsSampleGroup_VO p_objSampleGroupVO)
		{
			p_objSampleGroupVO.strSampleGroupID = objRow["SAMPLE_GROUP_ID_CHR"].ToString().Trim();
			p_objSampleGroupVO.strPYCode = objRow["PY_CODE_CHR"].ToString().Trim();
			p_objSampleGroupVO.strAssistCode01 = objRow["ASSIST_CODE01_CHR"].ToString().Trim();
			p_objSampleGroupVO.strAssistCode02 = objRow["ASSIST_CODE02_CHR"].ToString().Trim();
			p_objSampleGroupVO.strRemark = objRow["REMARK_VCHR"].ToString().Trim();
			p_objSampleGroupVO.strIsHandWork = objRow["IS_HAND_WORK_INT"].ToString().Trim();
			p_objSampleGroupVO.strDeviceModleID = objRow["DEVICE_MODEL_ID_CHR"].ToString().Trim();
			p_objSampleGroupVO.strCheckCategoryID = objRow["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
			p_objSampleGroupVO.strSampleTypeID = objRow["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
			p_objSampleGroupVO.strSampleGroupName = objRow["SAMPLE_GROUP_NAME_CHR"].ToString().Trim();
			if(objRow["PRINT_SEQ_INT"].ToString().Trim() != "")
			{
				p_objSampleGroupVO.intPRINT_SEQ_INT = int.Parse(objRow["PRINT_SEQ_INT"].ToString().Trim());
			}
			else
			{
				p_objSampleGroupVO.intPRINT_SEQ_INT = -1;
			}
		}
		#endregion

    }
}
