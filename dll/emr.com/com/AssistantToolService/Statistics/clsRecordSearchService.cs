using System.EnterpriseServices;
using System;
using System.Text ;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.StatisticsService
{
	/// <summary>
	/// 表单查询功能
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsRecordSearchService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 获取表单信息
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetFormInfo( out DataTable p_dtbResult)
		{
			p_dtbResult = null;

            string strSql = @"select formname, searchinfotype, mainsearchinfo from recordsearch_forminfo order by formname";			
			

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				lngRes=objHRPServ.DoGetDataTable(strSql,ref p_dtbResult);
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
        }	

		/// <summary>
		/// 获取表单的字段信息
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strFormName"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetFieldInfo(string p_strFormName, out DataTable p_dtbResult)
		{
			p_dtbResult = null;

			string strSql = @"
                            select formname, fieldname, conditionfieldtype, conditionfieldname, sortindex
                            from recordsearch_fieldinfo
                            where formname = ? order by sortindex";
			

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strFormName;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
        }

		/// <summary>
		/// 获取病人列表
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objSearchInfo"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientList( clsRecordSearch_SearchInfo p_objSearchInfo,out DataTable p_dtbResult)
		{
			p_dtbResult = null;

			if(p_objSearchInfo == null)
				return 0;


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				lngRes=objHRPServ.DoGetDataTable(p_objSearchInfo.m_strSQL,ref p_dtbResult);		

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
        }

		/// <summary>
		/// 查询病人记录信息
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strTypeID"></param>
		/// <param name="p_strTypeName"></param>
		/// <param name="p_strInPatientNO"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objCustomForms"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientRecordCount(string p_strTypeID, string p_strTypeName, 
			string p_strInPatientNO,string p_strInPatientDate,clsCustom_SubmitValue[] p_objCustomForms, out DataTable p_dtbResult)
		{
			p_dtbResult = null;

			#region SQL
			StringBuilder sbSql = new StringBuilder();
			//固定表单的Sql
			string strNew = @"select 
(select count(*) from inpatmedrec where typeid = '" + p_strTypeID + "' and inpatientid='$' and inpatientdate=# and status=0) as "+ p_strTypeName+",";
			string strOld = @"select 
(select count(*) from inpatientcasehistory_history where inpatientid='$' and inpatientdate=# and status=0) as 住院病历,
(select count(*) from inpatientcasehistory_history where inpatientid='$' and inpatientdate=# and status=0) as 住院病历模式2,";
			string strFixedSql = @"
(select count(*) from generaldiseaserecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from handoverrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from turninrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from conveyrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from takeoverrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from diseasesummaryrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from checkroomrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from deadcasediscussrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from beforeoperationdiscussrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from afteroperationrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from casediscussrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from deadrecord where inpatientid='$' and inpatientdate=# and status=0)
+(select count(*) from saverecord where inpatientid='$' and inpatientdate=# and status=0) as 病程记录,
(select count(*) from consultationrecord where inpatientid='$' and inpatientdate=# and status=0) as 会诊记录,
(select count(*) from operationrecordagreed where inpatientid='$' and inpatientdate=# and status=0) as 手术知情同意书,
(select count(*) from beforeoperationsummary where inpatientid='$' and inpatientdate=# and status=0) as 术前小结,
(select count(*) from operationrecorddoctor where inpatientid='$' and inpatientdate=# and status=0) as 手术记录单,
(select count(*) from picushiftinrecord where inpatientid='$' and inpatientdate=# and status=0) as icu转入记录,
(select count(*) from picushiftoutrecord where inpatientid='$' and inpatientdate=# and status=0) as icu转出记录,
(select count(*) from outhospitalrecord where inpatientid='$' and inpatientdate=# and status=0) as 出院记录,
(select count(*) from inhospitalmainrecord where inpatientid='$' and inpatientdate=# and status=0) as 住院病案首页,
(select count(*) from qcrecord where inpatientid='$' and inpatientdate=# and status=0) as 病案质量评分表,
(select count(*) from spectcheckorder where inpatientid='$' and inpatientdate=# and status=0) as spect检查申请单,
(select count(*) from hightoxygencheckorder where inpatientid='$' and inpatientdate=# and status=0) as 高压氧治疗申请单,
(select count(*) from bultrasoniccheckorder where inpatientid='$' and inpatientdate=# and status=0) as b型超声显像检查申请单,
(select count(*) from ctcheckorder where inpatientid='$' and inpatientdate=# and status=0) as ct检查申请单,
(select count(*) from xraycheckorder where inpatientid='$' and inpatientdate=# and status=0) as x线申请单,
(select count(*) from pathologyorgcheckorder where inpatientid='$' and inpatientdate=# and status=0) as 病理活体组织送检单,
(select count(*) from mriapply where inpatientid='$' and inpatientdate=# and status=0) as mri申请单, 
(select count(*) from ekgorder where inpatientid='$' and inpatientdate=# and status=0) as 心电图申请单, 
(select count(*) from psgorder where inpatientid='$' and inpatientdate=# and status=0) as 核医学检查申请单, 
(select count(*) from sirsevaluation where inpatientno='$' and inpatientdate=# and status=0) as sirs诊断评分,
(select count(*) from improveglasgowcomaevluation where inpatientno='$' and inpatientdate=# and status=0) as 改良glasgow昏迷评分,
(select count(*) from lunginjuryevalucation where inpatientno='$' and inpatientdate=# and status=0) as 急性肺损伤评分,
(select count(*) from newbabyinjurycaseevaluation where inpatientno='$' and inpatientdate=# and status=0) as 新生儿危重病例评分,
(select count(*) from babyinjurycaseevaluation where inpatientno='$' and inpatientdate=# and status=0) as 小儿危重病例评分,
(select count(*) from apacheiivaluation where inpatientno='$' and inpatientdate=# and status=0) as apacheii评分,
(select count(*) from apacheiiivaluation where inpatientno='$' and inpatientdate=# and status=0) as apacheiii评分,
(select count(*) from tissvaluation where inpatientno='$' and inpatientdate=# and status=0) as tiss28评分,
(select count(*) from inpatientevaluate where inpatientid='$' and inpatientdate=# and status=0) as 入院病人评估,
(select count(*) from threemeasurerecord where inpatientid='$' and inpatientdate=# and status=0) as 三测表,
(select count(*) from generalnurserecord where inpatientid='$' and inpatientdate=# and status=0) as 一般护理记录,
(select count(*) from watchitemrecord where inpatientid='$' and inpatientdate=# and status=0) as 观察项目记录表,
(select count(*) from intensivetendrecord1 where inpatientid='$' and inpatientdate=# and status=0) as 危重患者护理记录,
(select count(*) from icuintensivetend where inpatientid='$' and inpatientdate=# and status=0) as 危重症监护中心特护记录单,
(select count(*) from operationrecord where inpatientid='$' and inpatientdate=# and status=0) as 手术护理记录,
(select count(*) from operationequipmentqty where inpatientid='$' and inpatientdate=# and status=0) as 手术器械_敷料点数表,
(select count(*) from icubreath where inpatientid='$' and inpatientdate=# and status=0) as 中心icu呼吸机治疗监护记录单,
1 as 趋势分析,
1 as 实验室检验报告单,
1 as 影像报告单
";
			if(p_strTypeID != null)
				strFixedSql = strNew + strFixedSql;
			else
				strFixedSql = strOld + strFixedSql;
			sbSql.Append(strFixedSql);
			#region 添加自定义表单的Sql
//			if(p_objCustomForms != null && p_objCustomForms.Length > 0)
//			{
//				for(int i = 0; i < p_objCustomForms.Length; i++)
//				{
//					sbSql.Append(",");
//					sbSql.Append("\r\n");
////					sbSql.Append("'");
////					sbSql.Append("' = ");
////					sbSql.Append("\r\n");
//					sbSql.Append("(select count(*) from ");
//					sbSql.Append(p_objCustomForms[i].m_strFormTable);
//					sbSql.Append(" where InPatientID='$' and InPatientDate=# and Status=0) as ");
//					sbSql.Append(p_objCustomForms[i].m_strFormName);
//				}
//			}
			#endregion

			if(clsHRPTableService.bytDatabase_Selector == 2)
				sbSql.Append("\r\nfrom dual");

			#endregion SQL

			string strSql = sbSql.ToString();
			strSql = strSql.Replace("$",p_strInPatientNO);
			strSql =strSql.Replace("#",clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat( p_strInPatientDate));
			

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				lngRes=objHRPServ.DoGetDataTable(strSql,ref p_dtbResult);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
        }

		/// <summary>
		/// 获取科室下专科病历表单（新专科病历用）
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngIMR_GetFormInfo( out DataTable p_dtbResult)
		{
			p_dtbResult = null;

            string strSql = @"select typeid, typename from inpatmedrec_type";
			

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				lngRes=objHRPServ.DoGetDataTable(strSql,ref p_dtbResult);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
        }

		/// <summary>
		/// 获取查询字段信息
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strFormName"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngIMR_GetFieldInfo(string p_strFormID, out DataTable p_dtbResult)
		{
			p_dtbResult = null;

			string strSql =@"select distinct b.itemid,b.itemname,b.itemtype 
                            from inpatmedrec_type a 
                            inner join inpatmedrec_type_item b 
                            on a.typeid = b.typeid
                            where a.typeid = ?";
			

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strFormID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strSQL"></param>
		/// <param name="p_dtbValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSearchesBySQL(string p_strSQL,ref DataTable p_dtbValues)
		{

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				lngRes=objHRPServ.DoGetDataTable(p_strSQL,ref p_dtbValues);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}
	}
}
