using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService ;
using System.Data;
using weCare.Core.Entity;
using System.Collections;
using System.Text;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.CaseGradeServ
{
	/// <summary>
	/// 住院病历评分中间层
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsCaseGradeServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsCaseGradeServ()
		{}

		/// <summary>
        /// 获取病人曾经在院时间
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strEMRInPatientDateArr"></param>
		/// <param name="p_strHISInPatientDateArr"></param>
		/// <param name="p_strHISInPatientIDArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllInPatientTime(string p_strInPatientID, 
            out string[] p_strEMRInPatientDateArr,
            out string[] p_strHISInPatientDateArr,
            out string[] p_strHISInPatientIDArr)
		{
            p_strEMRInPatientDateArr = null;
            p_strHISInPatientDateArr = null;
            p_strHISInPatientIDArr = null;
			if(p_strInPatientID == null || p_strInPatientID =="")
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //string strSql = @"select INPATIENTDATE from INPATIENTDATEINFO where INPATIENTID = '" + p_strInPatientID + "'";
                string strSql = @"select t.emrinpatientdate,t.hisinpatientdate,t.hisinpatientid_chr from t_bse_hisemr_relation t where t.emrinpatientid= ?";

                DataTable dtbValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue,objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strEMRInPatientDateArr = new string[dtbValue.Rows.Count];
                    p_strHISInPatientDateArr = new string[dtbValue.Rows.Count];
                    p_strHISInPatientIDArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_strEMRInPatientDateArr[i] = Convert.ToDateTime(dtbValue.Rows[i]["emrinpatientdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strHISInPatientDateArr[i] = Convert.ToDateTime(dtbValue.Rows[i]["hisinpatientdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strHISInPatientIDArr[i] = dtbValue.Rows[i]["hisinpatientid_chr"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			return lngRes;
		}
		/// <summary>
		/// 获取评分结果
		/// </summary>
		/// <param name="p_objContent">不能为空值，必须要有病人ID和入院日期</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetGradeInfo(ref clsCaseGradeValue p_objContent)
		{
			if(p_objContent == null || p_objContent.m_strInPatientID == null || p_objContent.m_strInPatientID == "" || p_objContent.m_strInPatientDate == null || p_objContent.m_strInPatientDate == "")
				return (long)enmOperationResult.Parameter_Error;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
			try
			{
                string strSql = @"select inpatientid,
       inpatientdate,
       itemid,
       itemcontent,
       opendate,
       description from inpatientcase_grade where inpatientid = ? and inpatientdate = ?";
			
				DataTable dtbValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_objContent.m_strInPatientDate);

				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbValue,objDPArr);
				if(lngRes > 0 && dtbValue.Rows.Count > 0)
				{
					p_objContent.m_strOpenDate = dtbValue.Rows[0]["OPENDATE"].ToString().Trim();
					p_objContent.m_objItemValueArr = new clsCaseGrade_ItemValue[dtbValue.Rows.Count];
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						p_objContent.m_objItemValueArr[i] = new clsCaseGrade_ItemValue();
						p_objContent.m_objItemValueArr[i].m_strItemID = dtbValue.Rows[i]["ITEMID"].ToString().Trim();
						p_objContent.m_objItemValueArr[i].m_strItemContent = dtbValue.Rows[i]["ITEMCONTENT"].ToString().Trim();
						p_objContent.m_objItemValueArr[i].m_strDescription = dtbValue.Rows[i]["DESCRIPTION"].ToString().Trim();
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
	        finally
	        {
	          //objHRPServ.Dispose();

	        }
			return lngRes;
		}
		/// <summary>
		/// 获取科室下所有评过分的住院病历
		/// </summary>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_dtpSelectDate">日期条件</param>
		/// <param name="p_objContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetGradeInfoByDept(string p_strDeptID,DateTime p_dtpFirstDate,DateTime p_dtpLastDate,out clsCaseGradeValue[] p_objContentArr)
		{
			p_objContentArr = null;
			if(p_strDeptID == null || p_strDeptID == "")
				return (long)enmOperationResult.Parameter_Error;
            //string strSql = @"select indeptinfo.inpatientid,indeptinfo.inpatientdate  from indeptinfo INNER JOIN (select DISTINCT INPATIENTID,INPATIENTDATE from INPATIENTCASE_GRADE where INPATIENTDATE >"
            //    +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtpFirstDate)+@" and INPATIENTDATE <"+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtpLastDate)
            //    +@") ga  ON indeptinfo.inpatientid = ga.inpatientid where indeptinfo.indeptid = '"+ p_strDeptID+"'";

            string strSql = @"select distinct re.inpatientid_chr, re.inpatient_dat
								from t_opr_bih_register re
								inner join (select distinct inpatientid, inpatientdate
											from inpatientcase_grade
											where inpatientdate > ?
												and inpatientdate < ?) ga on re.inpatientid_chr =
																										ga.inpatientid
																									and re.inpatient_dat =
																										ga.inpatientdate
								where re.deptid_chr = ? and re.status_int = 1";
			clsHRPTableService objHRPServ =new clsHRPTableService();
			
			long lngRes = 0;
			try
			{
				DataTable dtbValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtpFirstDate;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtpLastDate;
                objDPArr[2].Value = p_strDeptID.Trim();

				lngRes = objHRPServ.lngGetDataTableWithParameters(strSql,ref dtbValue,objDPArr);
				if(lngRes > 0 && dtbValue.Rows.Count > 0)
				{
					p_objContentArr = new clsCaseGradeValue[dtbValue.Rows.Count];
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						p_objContentArr[i] = new clsCaseGradeValue();
                        p_objContentArr[i].m_strInPatientID = dtbValue.Rows[i]["inpatientid_chr"].ToString().Trim();
                        p_objContentArr[i].m_strInPatientDate = dtbValue.Rows[i]["inpatient_dat"].ToString().Trim();
						m_lngGetGradeInfo(ref p_objContentArr[i]);
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
	        finally
	        {
	          //objHRPServ.Dispose();

	        }
			return lngRes;
		}
		/// <summary>
		/// 获取病区下所有评过分的住院病历
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_dtpSelectDate">日期条件</param>
		/// <param name="p_objContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetGradeInfoByArea(string p_strAreaID,DateTime p_dtpFirstDate,DateTime p_dtpLastDate,out clsCaseGradeValue[] p_objContentArr)
		{
			p_objContentArr = null;
			if(p_strAreaID == null || p_strAreaID == "")
				return (long)enmOperationResult.Parameter_Error;
            //string strSql = @"select DISTINCT indeptinfo.inpatientid,indeptinfo.inpatientdate  from indeptinfo INNER JOIN (select DISTINCT INPATIENTID,INPATIENTDATE from INPATIENTCASE_GRADE where INPATIENTDATE >"
            //    +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtpFirstDate)+@" and INPATIENTDATE <"+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtpLastDate)
            //    +@") ga  ON indeptinfo.inpatientid = ga.inpatientid and indeptinfo.inpatientdate=ga.inpatientdate where indeptinfo.AREA_ID = '"+ p_strAreaID+"'";

            string strSql = @"select distinct re.inpatientid_chr, re.inpatient_dat
								from t_opr_bih_register re
								inner join (select distinct inpatientid, inpatientdate
											from inpatientcase_grade
											where inpatientdate >?
												and inpatientdate <?) ga on re.inpatientid_chr =
																										ga.inpatientid
																									and re.inpatient_dat =
																										ga.inpatientdate
								where re.areaid_chr = ? and re.status_int = 1";

			clsHRPTableService objHRPServ =new clsHRPTableService();
			
			long lngRes = 0;
			try
			{
				DataTable dtbValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtpFirstDate;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtpLastDate;
                objDPArr[2].Value = p_strAreaID.Trim();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
				if(lngRes > 0 && dtbValue.Rows.Count > 0)
				{
					p_objContentArr = new clsCaseGradeValue[dtbValue.Rows.Count];
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						p_objContentArr[i] = new clsCaseGradeValue();
                        p_objContentArr[i].m_strInPatientID = dtbValue.Rows[i]["inpatientid_chr"].ToString().Trim();
                        p_objContentArr[i].m_strInPatientDate = dtbValue.Rows[i]["inpatient_dat"].ToString().Trim();
						m_lngGetGradeInfo(ref p_objContentArr[i]);
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
	        finally
	        {
	          //objHRPServ.Dispose();

	        }
			return lngRes;
		}
		/// <summary>
		/// 保存评分值
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSaveGradeInfo(clsCaseGradeValue p_objContent)
		{
			if(p_objContent == null || p_objContent.m_strInPatientID == null || p_objContent.m_strInPatientID == "" 
				|| p_objContent.m_strInPatientDate == null || p_objContent.m_strInPatientDate == "" || p_objContent.m_objItemValueArr == null)
				return (long)enmOperationResult.Parameter_Error;
			string strSql = @"insert into inpatientcase_grade(inpatientid,inpatientdate,opendate,itemid,itemcontent,description) 
                            values(?,?,?,?,?,?)";
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                long lngEff = -1;
                for (int i = 0; i < p_objContent.m_objItemValueArr.Length; i++)
                {
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = p_objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_objContent.m_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_objContent.m_strOpenDate);
                    objDPArr[3].Value = p_objContent.m_objItemValueArr[i].m_strItemID;
                    objDPArr[4].Value = p_objContent.m_objItemValueArr[i].m_strItemContent;
                    objDPArr[5].Value = p_objContent.m_objItemValueArr[i].m_strDescription;

                    //string strSaveSql = strSql + p_objContent.m_objItemValueArr[i].m_strItemID + "','" + p_objContent.m_objItemValueArr[i].m_strItemContent + "','" + p_objContent.m_objItemValueArr[i].m_strDescription + "')";
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			return lngRes;
		}
		/// <summary>
		/// 修改评分值
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyGradeInfo(clsCaseGradeValue p_objContent)
		{
			if(p_objContent == null || p_objContent.m_strInPatientID == null || p_objContent.m_strInPatientID == "" 
				|| p_objContent.m_strInPatientDate == null || p_objContent.m_strInPatientDate == "" || p_objContent.m_objItemValueArr == null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                long lngEff = -1;
                string strModifySql = @"update inpatientcase_grade set opendate = ?,itemcontent = ?,description = ?
                    where inpatientid = ? and inpatientdate = ? and itemid = ?";
                for (int i = 0; i < p_objContent.m_objItemValueArr.Length; i++)
                {
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = DateTime.Parse(p_objContent.m_strOpenDate);
                    objDPArr[1].Value = p_objContent.m_objItemValueArr[i].m_strItemContent;
                    objDPArr[2].Value = p_objContent.m_objItemValueArr[i].m_strDescription;
                    objDPArr[3].Value = p_objContent.m_strInPatientID;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = DateTime.Parse(p_objContent.m_strInPatientDate);
                    objDPArr[5].Value = p_objContent.m_objItemValueArr[i].m_strItemID;

                    lngRes = objHRPServ.lngExecuteParameterSQL(strModifySql,ref lngEff,objDPArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			return lngRes;
		}
		/// <summary>
		/// 删除评分值
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteGradeInfo(string p_strInPatientID,string p_strInPatientDate)
		{
			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
				return (long)enmOperationResult.Parameter_Error;

			string strDeleteSql = @"delete from inpatientcase_grade where inpatientid = ? and inpatientdate = ?";

			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                long lngEff = -1;
				lngRes=objHRPServ.lngExecuteParameterSQL(strDeleteSql,ref lngEff,objDPArr); 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}
		/// <summary>
		/// 查找病历缺陷
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_hasContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDetailInfo(string p_strInPatientID,string p_strInPatientDate,out System.Collections.Generic.Dictionary<string, string> p_hasContent)
		{
			p_hasContent = null;
			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
				return (long)enmOperationResult.Parameter_Error;
			p_hasContent = new System.Collections.Generic.Dictionary<string, string>();

			#region 
			
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				string strSql = clsDatabaseSQLConvert.s_StrTop1+@" diagnosis,inhospitaldiagnosis,maindiagnosis,pathologydiagnosis,sensitive from inhospitalmainrecord_content where inpatientid = ? and inpatientdate = ?"
					+" order by lastmodifydate desc"+clsDatabaseSQLConvert.s_StrRownum;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

				DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
				if(lngRes > 0 && dtbValue.Rows.Count == 1)
				{
					p_hasContent.Add("门诊诊断",dtbValue.Rows[0]["DIAGNOSIS"].ToString().Trim());
					p_hasContent.Add("入院诊断",dtbValue.Rows[0]["INHOSPITALDIAGNOSIS"].ToString().Trim());
					p_hasContent.Add("出院诊断",dtbValue.Rows[0]["MAINDIAGNOSIS"].ToString().Trim());
					p_hasContent.Add("病理诊断",dtbValue.Rows[0]["PATHOLOGYDIAGNOSIS"].ToString().Trim());
					p_hasContent.Add("过敏药物",dtbValue.Rows[0]["SENSITIVE"].ToString().Trim());
				}
				strSql = @"select
(select count(inpatientid) from generaldiseaserecord where inpatientid='$' and inpatientdate=#) as 病程记录,
(select count(inpatientid) from handoverrecord where inpatientid='$' and inpatientdate=#)
+(select count(inpatientid) from takeoverrecord where inpatientid='$' and inpatientdate=#) as 交接班记录,
(select count(inpatientid) from turninrecord where inpatientid='$' and inpatientdate=#)
+(select count(inpatientid) from conveyrecord where inpatientid='$' and inpatientdate=#) as 转入转出记录,
(select count(inpatientid) from diseasesummaryrecord where inpatientid='$' and inpatientdate=#) as 阶段小结,
(select count(checkroomrecord) from checkroomrecord where inpatientid='$' and inpatientdate=#) as 查房记录,
(select count(inpatientid) from deadcasediscussrecord where inpatientid='$' and inpatientdate=#) as 死亡病例讨论,
(select count(inpatientid) from beforeoperationdiscussrecord where inpatientid='$' and inpatientdate=#) as 术前讨论,
(select count(inpatientid) from afteroperationrecord where inpatientid='$' and inpatientdate=#) as 手术后病程记录,
(select count(inpatientid) from casediscussrecord where inpatientid='$' and inpatientdate=#) as 病例讨论,
(select count(inpatientid) from deadrecord where inpatientid='$' and inpatientdate=#) as 死亡记录,
(select count(inpatientid) from saverecord where inpatientid='$' and inpatientdate=#) as 抢救记录,
(select count(inpatientid) from consultationrecord where inpatientid='$' and inpatientdate=#) as 会诊记录,
(select count(inpatientid) from operationrecordagreed where inpatientid='$' and inpatientdate=#) as 手术知情同意书,
(select count(inpatientid) from beforeoperationsummary where inpatientid='$' and inpatientdate=#) as 术前小结,
(select count(inpatientid) from operationrecorddoctor where inpatientid='$' and inpatientdate=#) as 手术记录单,
(select count(inpatientid) from picushiftinrecord where inpatientid='$' and inpatientdate=#) as icu转入记录,
(select count(inpatientid) from picushiftoutrecord where inpatientid='$' and inpatientdate=#) as icu转出记录,
(select count(inpatientid) from outhospitalrecord where inpatientid='$' and inpatientdate=#) as 出院记录 ";
				if(clsHRPTableService.bytDatabase_Selector == 2)
					strSql += " from dual";
                if (clsHRPTableService.bytDatabase_Selector == 4)
                    strSql += " from sysibm.sysdummy1";
				StringBuilder sbSql = new StringBuilder();
				sbSql.Append(strSql);

				string strLastSql = sbSql.ToString();
				strLastSql = strLastSql.Replace("$",p_strInPatientID);
				strLastSql =strLastSql.Replace("#",clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat( p_strInPatientDate));
				lngRes = objHRPServ.DoGetDataTable(strLastSql,ref dtbValue);
				if(lngRes > 0)
				{
					p_hasContent.Add("病程记录",dtbValue.Rows[0]["病程记录"].ToString().Trim());
					p_hasContent.Add("交接班记录",dtbValue.Rows[0]["交接班记录"].ToString().Trim());
					p_hasContent.Add("转入转出记录",dtbValue.Rows[0]["转入转出记录"].ToString().Trim());
					p_hasContent.Add("阶段小结",dtbValue.Rows[0]["阶段小结"].ToString().Trim());
					p_hasContent.Add("查房记录",dtbValue.Rows[0]["查房记录"].ToString().Trim());
					p_hasContent.Add("死亡病例讨论",dtbValue.Rows[0]["死亡病例讨论"].ToString().Trim());
					p_hasContent.Add("术前讨论",dtbValue.Rows[0]["术前讨论"].ToString().Trim());
					p_hasContent.Add("手术后病程记录",dtbValue.Rows[0]["手术后病程记录"].ToString().Trim());
					p_hasContent.Add("病例讨论",dtbValue.Rows[0]["病例讨论"].ToString().Trim());
					p_hasContent.Add("死亡记录",dtbValue.Rows[0]["死亡记录"].ToString().Trim());
					p_hasContent.Add("抢救记录",dtbValue.Rows[0]["抢救记录"].ToString().Trim());
					p_hasContent.Add("会诊记录",dtbValue.Rows[0]["会诊记录"].ToString().Trim());
					p_hasContent.Add("手术知情同意书",dtbValue.Rows[0]["手术知情同意书"].ToString().Trim());
					p_hasContent.Add("术前小结",dtbValue.Rows[0]["术前小结"].ToString().Trim());
					p_hasContent.Add("手术记录单",dtbValue.Rows[0]["手术记录单"].ToString().Trim());
					p_hasContent.Add("ICU转入记录",dtbValue.Rows[0]["ICU转入记录"].ToString().Trim());
					p_hasContent.Add("ICU转出记录",dtbValue.Rows[0]["ICU转出记录"].ToString().Trim());
					p_hasContent.Add("出院记录",dtbValue.Rows[0]["出院记录"].ToString().Trim());
				}
				#endregion

			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
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
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptId"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_strInpatientIdLike"></param>
        /// <param name="p_strInpatientNameLike"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatient( string p_strDeptId, string p_strAreaId, string p_strInpatientIdLike, string p_strInpatientNameLike, out DataTable p_dtbValues)
        {
            p_dtbValues = new DataTable();

            long lngRes = 0;
            try
            {
                string strSql = @"select pa.lastname_vchr,
       re.inpatientid_chr,
       re.inpatient_dat,
       le.modify_dat,
       gr.opendate,
       gr.itemcontent,
       re.registerid_chr,
       re.deptid_chr,
       re.areaid_chr,
       re.patientid_chr,
       re.pstatus_int
  from t_opr_bih_register re
 inner join t_bse_patient pa on re.patientid_chr = pa.patientid_chr
  left join t_opr_bih_leave le on re.registerid_chr = le.registerid_chr and le.status_int = 1
  left join (select inpatientid, inpatientdate, itemid, itemcontent, opendate, description
               from inpatientcase_grade
              where itemid = 'm_txtAllResult') gr on re.inpatientid_chr =
                                                     gr.inpatientid
                                                 and re.inpatient_dat =
                                                     gr.inpatientdate
 where re.deptid_chr = '" + p_strDeptId + "' and re.pstatus_int in (1,3) and re.status_int = 1";
                if (p_strAreaId != "")
                    strSql += " and re.areaid_chr = '" + p_strAreaId + "' ";
                if (p_strInpatientIdLike != "")
                    strSql += " and re.inpatientid_chr like '%" + p_strInpatientIdLike + "%' ";
                else if (p_strInpatientNameLike != "")
                    strSql += " and pa.lastname_vchr like '%" + p_strInpatientNameLike + "%' ";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                DataTable dtbResualt = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSql, ref dtbResualt);
                if (lngRes <= 0 || dtbResualt.Rows.Count == 0) return 0;
                p_dtbValues = dtbResualt;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            return lngRes;
        }
	}
}
