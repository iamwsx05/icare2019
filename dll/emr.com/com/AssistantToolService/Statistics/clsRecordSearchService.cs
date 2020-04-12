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
	/// ����ѯ����
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsRecordSearchService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// ��ȡ����Ϣ
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
		/// ��ȡ�����ֶ���Ϣ
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
		/// ��ȡ�����б�
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
		/// ��ѯ���˼�¼��Ϣ
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
			//�̶�����Sql
			string strNew = @"select 
(select count(*) from inpatmedrec where typeid = '" + p_strTypeID + "' and inpatientid='$' and inpatientdate=# and status=0) as "+ p_strTypeName+",";
			string strOld = @"select 
(select count(*) from inpatientcasehistory_history where inpatientid='$' and inpatientdate=# and status=0) as סԺ����,
(select count(*) from inpatientcasehistory_history where inpatientid='$' and inpatientdate=# and status=0) as סԺ����ģʽ2,";
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
+(select count(*) from saverecord where inpatientid='$' and inpatientdate=# and status=0) as ���̼�¼,
(select count(*) from consultationrecord where inpatientid='$' and inpatientdate=# and status=0) as �����¼,
(select count(*) from operationrecordagreed where inpatientid='$' and inpatientdate=# and status=0) as ����֪��ͬ����,
(select count(*) from beforeoperationsummary where inpatientid='$' and inpatientdate=# and status=0) as ��ǰС��,
(select count(*) from operationrecorddoctor where inpatientid='$' and inpatientdate=# and status=0) as ������¼��,
(select count(*) from picushiftinrecord where inpatientid='$' and inpatientdate=# and status=0) as icuת���¼,
(select count(*) from picushiftoutrecord where inpatientid='$' and inpatientdate=# and status=0) as icuת����¼,
(select count(*) from outhospitalrecord where inpatientid='$' and inpatientdate=# and status=0) as ��Ժ��¼,
(select count(*) from inhospitalmainrecord where inpatientid='$' and inpatientdate=# and status=0) as סԺ������ҳ,
(select count(*) from qcrecord where inpatientid='$' and inpatientdate=# and status=0) as �����������ֱ�,
(select count(*) from spectcheckorder where inpatientid='$' and inpatientdate=# and status=0) as spect������뵥,
(select count(*) from hightoxygencheckorder where inpatientid='$' and inpatientdate=# and status=0) as ��ѹ���������뵥,
(select count(*) from bultrasoniccheckorder where inpatientid='$' and inpatientdate=# and status=0) as b�ͳ������������뵥,
(select count(*) from ctcheckorder where inpatientid='$' and inpatientdate=# and status=0) as ct������뵥,
(select count(*) from xraycheckorder where inpatientid='$' and inpatientdate=# and status=0) as x�����뵥,
(select count(*) from pathologyorgcheckorder where inpatientid='$' and inpatientdate=# and status=0) as ���������֯�ͼ쵥,
(select count(*) from mriapply where inpatientid='$' and inpatientdate=# and status=0) as mri���뵥, 
(select count(*) from ekgorder where inpatientid='$' and inpatientdate=# and status=0) as �ĵ�ͼ���뵥, 
(select count(*) from psgorder where inpatientid='$' and inpatientdate=# and status=0) as ��ҽѧ������뵥, 
(select count(*) from sirsevaluation where inpatientno='$' and inpatientdate=# and status=0) as sirs�������,
(select count(*) from improveglasgowcomaevluation where inpatientno='$' and inpatientdate=# and status=0) as ����glasgow��������,
(select count(*) from lunginjuryevalucation where inpatientno='$' and inpatientdate=# and status=0) as ���Է���������,
(select count(*) from newbabyinjurycaseevaluation where inpatientno='$' and inpatientdate=# and status=0) as ������Σ�ز�������,
(select count(*) from babyinjurycaseevaluation where inpatientno='$' and inpatientdate=# and status=0) as С��Σ�ز�������,
(select count(*) from apacheiivaluation where inpatientno='$' and inpatientdate=# and status=0) as apacheii����,
(select count(*) from apacheiiivaluation where inpatientno='$' and inpatientdate=# and status=0) as apacheiii����,
(select count(*) from tissvaluation where inpatientno='$' and inpatientdate=# and status=0) as tiss28����,
(select count(*) from inpatientevaluate where inpatientid='$' and inpatientdate=# and status=0) as ��Ժ��������,
(select count(*) from threemeasurerecord where inpatientid='$' and inpatientdate=# and status=0) as �����,
(select count(*) from generalnurserecord where inpatientid='$' and inpatientdate=# and status=0) as һ�㻤���¼,
(select count(*) from watchitemrecord where inpatientid='$' and inpatientdate=# and status=0) as �۲���Ŀ��¼��,
(select count(*) from intensivetendrecord1 where inpatientid='$' and inpatientdate=# and status=0) as Σ�ػ��߻����¼,
(select count(*) from icuintensivetend where inpatientid='$' and inpatientdate=# and status=0) as Σ��֢�໤�����ػ���¼��,
(select count(*) from operationrecord where inpatientid='$' and inpatientdate=# and status=0) as ���������¼,
(select count(*) from operationequipmentqty where inpatientid='$' and inpatientdate=# and status=0) as ������е_���ϵ�����,
(select count(*) from icubreath where inpatientid='$' and inpatientdate=# and status=0) as ����icu���������Ƽ໤��¼��,
1 as ���Ʒ���,
1 as ʵ���Ҽ��鱨�浥,
1 as Ӱ�񱨸浥
";
			if(p_strTypeID != null)
				strFixedSql = strNew + strFixedSql;
			else
				strFixedSql = strOld + strFixedSql;
			sbSql.Append(strFixedSql);
			#region ����Զ������Sql
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
		/// ��ȡ������ר�Ʋ���������ר�Ʋ����ã�
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
		/// ��ȡ��ѯ�ֶ���Ϣ
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
