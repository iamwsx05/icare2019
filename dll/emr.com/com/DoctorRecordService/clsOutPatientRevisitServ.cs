using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.OutPatientRevisitServ
{
	/// <summary>
	/// 复诊功能中间件
	/// </summary>
[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsOutPatientRevisitServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsOutPatientRevisitServ()
		{
		}

		#region 复诊提醒
		/// <summary>
		/// 添加复诊提示
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
	[AutoComplete]	
	public long m_lngAddRemindRecord(clsOutPatientRevisitRemind_VO p_objContent)
		{
			if(p_objContent == null)
				return -1;
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {
                string strSql = @"insert into outpatientrevisitremind(inpatientid,inpatientdate,inpatientenddate,revisittype,revisittime,beforereminddate,times,status,remaindtext,revisittypecontent)
									values(?,?,?,?,?,?,?,?,?,?)";


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(10, out objDPArr);

                objDPArr[0].Value = p_objContent.m_StrInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objContent.m_DtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objContent.m_DtmInPatientEndDate;
                objDPArr[3].Value = p_objContent.m_IntRevisitType;
                objDPArr[4].Value = p_objContent.m_StrRevisitTime;
                objDPArr[5].Value = p_objContent.m_IntBeforeRemindDate;
                objDPArr[6].Value = p_objContent.m_IntTimes;
                objDPArr[7].Value = p_objContent.m_IntStatus;
                objDPArr[8].Value = p_objContent.m_StrRemaindText;
                objDPArr[9].Value = p_objContent.m_IntRevisitTypeContent;
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

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
			//返回
			return lngRes;

		}
		/// <summary>
		/// 修改复诊提示
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
	[AutoComplete]	
	public long m_lngModifyRemindRecord(clsOutPatientRevisitRemind_VO p_objContent)
		{
			if(p_objContent == null)
				return -1;
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {
                string strSql = @"update outpatientrevisitremind set status = ?,revisittype = ?,revisittime = ?,beforereminddate = ?,times = ?,remaindtext = ?,revisittypecontent = ?
	where inpatientid = ? and inpatientdate = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(9, out objDPArr);


                objDPArr[0].Value = p_objContent.m_IntStatus;
                objDPArr[1].Value = p_objContent.m_IntRevisitType;
                objDPArr[2].Value = p_objContent.m_StrRevisitTime;
                objDPArr[3].Value = p_objContent.m_IntBeforeRemindDate;
                objDPArr[4].Value = p_objContent.m_IntTimes;
                objDPArr[5].Value = p_objContent.m_StrRemaindText;
                objDPArr[6].Value = p_objContent.m_IntRevisitTypeContent;
                objDPArr[7].Value = p_objContent.m_StrInPatientID;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = p_objContent.m_DtmInPatientDate;
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                return lngRes;

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
			//返回
			return lngRes;

		}
		/// <summary>
		/// 获取单个复诊提示内容
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_dtmInPatientDate"></param>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
	[AutoComplete]	
	public long m_lngGetRemindRecord(string p_strInPatientID,DateTime p_dtmInPatientDate,out clsOutPatientRevisitRemind_VO[] p_objContent)
		{
			p_objContent = null;
			if(p_strInPatientID == null || p_strInPatientID == string.Empty || p_dtmInPatientDate == DateTime.MinValue)
				return -1;
			long lngRes = 0;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSql = @"select inpatientid,
       inpatientdate,
       inpatientenddate,
       status,
       revisittype,
       revisittime,
       beforereminddate,
       times,
       remaindtext,
       revisittypecontent
  from outpatientrevisitremind
 where inpatientid = ?
   and inpatientdate = ?";
                DataTable dtbResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInPatientDate;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                p_objContent = new clsOutPatientRevisitRemind_VO[dtbResult.Rows.Count];

                if (lngRes > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objContent[i] = new clsOutPatientRevisitRemind_VO();
                        p_objContent[i].m_StrInPatientID = p_strInPatientID;
                        p_objContent[i].m_DtmInPatientDate = p_dtmInPatientDate;
                        try
                        {
                            p_objContent[i].m_DtmInPatientEndDate = DateTime.Parse(dtbResult.Rows[i]["INPATIENTENDDATE"].ToString());
                        }
                        catch { p_objContent[i].m_DtmInPatientEndDate = DateTime.MinValue; }
                        try
                        {
                            p_objContent[i].m_IntRevisitType = Convert.ToInt32(dtbResult.Rows[i]["REVISITTYPE"]);
                        }
                        catch { p_objContent[i].m_IntRevisitType = 1; }
                        try
                        {
                            p_objContent[i].m_IntRevisitTypeContent = Convert.ToInt32(dtbResult.Rows[i]["REVISITTYPECONTENT"]);
                        }
                        catch { p_objContent[i].m_IntRevisitTypeContent = 1; }
                        try
                        {
                            p_objContent[i].m_IntStatus = Convert.ToInt32(dtbResult.Rows[i]["STATUS"]);
                        }
                        catch { p_objContent[i].m_IntStatus = 0; }
                        try
                        {
                            p_objContent[i].m_IntTimes = Convert.ToInt32(dtbResult.Rows[i]["TIMES"]);
                        }
                        catch { p_objContent[i].m_IntTimes = 0; }
                        try
                        {
                            p_objContent[i].m_IntBeforeRemindDate = Convert.ToInt32(dtbResult.Rows[i]["BEFOREREMINDDATE"]);
                        }
                        catch { p_objContent[i].m_IntBeforeRemindDate = 1; }

                        p_objContent[i].m_StrRevisitTime = dtbResult.Rows[i]["REVISITTIME"].ToString().Trim();
                        p_objContent[i].m_StrRemaindText = dtbResult.Rows[i]["REMAINDTEXT"].ToString().Trim();
                    }
                }
                return lngRes;
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
			//返回
			return lngRes;


		}

	/// <summary>
	/// 选择提醒时间后显示提醒内容
	/// </summary>
	/// <param name="p_strInPatientID">住院号</param>
	/// <param name="p_dtmInPatientDate">入院时间</param>
	/// <param name="p_dtmRevisitTime">提醒时间(Oracle数据库中该字段为VARCHAR类型?!)</param>
	/// <param name="p_strRemaindText">提醒内容</param>
	/// <returns></returns>
	[AutoComplete]
	public long m_lngGetRemindRecord(string p_strInPatientID, DateTime p_dtmInPatientDate, DateTime p_dtmRevisitTime, out string p_strRemaindText)
	{
		p_strRemaindText = "";
		long lngRes = -1;
		if(p_strInPatientID == null || p_strInPatientID == string.Empty || p_dtmInPatientDate == DateTime.MinValue || p_dtmRevisitTime == DateTime.MinValue)
			return -1;
		string strSql = @"select remaindtext from outpatientrevisitremind where inpatientid = ?
			 and inpatientdate = ?
			 and revisittime = ?";

		try
		{
            clsHRPTableService m_objServ = new clsHRPTableService();
			DataTable dtbResult=new DataTable ();

            IDataParameter[] objDPArr = null;
            m_objServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID.Trim();
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = p_dtmInPatientDate;
            objDPArr[2].Value = p_dtmRevisitTime.ToString("yyyy-MM-dd HH:mm:ss");

            lngRes = m_objServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);

			if(lngRes > 0 && dtbResult.Rows.Count > 0)
			{
				p_strRemaindText = dtbResult.Rows[0]["RemaindText"].ToString().Trim();
			}
		}
		catch(Exception objEx)
		{
			string strTmp=objEx.Message;
			com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
			bool blnRes = objLogger.LogError(objEx);
		}
		return lngRes;
	}

		/// <summary>
		/// 修改状态
		/// </summary>
		/// <param name="p_intStatus"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <returns></returns>
	[AutoComplete]	
	public long m_lngSetRemindRecordStatus(int p_intStatus,string p_strInPatientID,string p_strInPatientDate)
		{
			long lngRes = 0;
			try
			{
				if(p_intStatus < 0 || p_intStatus > 2 || p_strInPatientID == null || p_strInPatientID == string.Empty)
					return -1;
				string strSql = @"update outpatientrevisitremind set status = ? where inpatientid = ? and inpatientdate = ?";
                clsHRPTableService m_objServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                m_objServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_intStatus;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                long lngEff = -1;
                lngRes= m_objServ.lngExecuteParameterSQL(strSql,ref lngEff,objDPArr);
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;

		}

	/// <summary>
	/// 修改状态(有多条记录)
	/// </summary>
	/// <param name="p_intStatus"></param>
	/// <param name="p_strInPatientID"></param>
	/// <param name="p_strInPatientDate"></param>
	/// <param name="p_strRevisitTime"></param>
	/// <returns></returns>
	[AutoComplete]
	public long m_lngSetRemindRecordStatusMRecord(int p_intStatus,string p_strInPatientID,string p_strInPatientDate,string p_strRevisitTime)
	{
		long lngRes = -1;
		try
		{
			if(p_intStatus < 0 || p_intStatus > 2 || p_strInPatientID == null || p_strInPatientID == string.Empty || p_strRevisitTime == null || p_strRevisitTime == string.Empty)
				return -1;
			string strSql = @"update outpatientrevisitremind set status = ? where inpatientid = ? and inpatientdate = ? and revisittime = ?";
            clsHRPTableService m_objServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            m_objServ.CreateDatabaseParameter(4, out objDPArr);
            objDPArr[0].Value = p_intStatus;
            objDPArr[1].Value = p_strInPatientID;
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[3].Value = p_strRevisitTime;

            long lngEff = -1;
            lngRes = m_objServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
		}
		catch(Exception objEx)
		{
			string strTmp=objEx.Message;
			com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
			bool blnRes = objLogger.LogError(objEx);
		}
		//返回
		return lngRes;
	}

		/// <summary>
		/// 获取全部到期的复诊提示内容
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_objContentArr"></param>
		/// <returns></returns>
	[AutoComplete]	
	public long m_lngGetAllNeedRemindRecord(string p_strAreaID,out clsOutPatientRevisitRemind_VO[] p_objContentArr)
		{
			p_objContentArr = null;
			if(p_strAreaID == null || p_strAreaID == string.Empty)
				return -1;
			long lngRes = 0;
			try
			{
				string strtemp = "";
				if(clsHRPTableService.bytDatabase_Selector == 2)
					strtemp = "(floor(to_date(op.RevisitTime,'yyyy-mm-dd hh24:mi:ss')- sysdate) +'1')";
				else
					strtemp = "DATEDIFF ( day , getdate() , op.RevisitTime)";

				#region 旧表
//				string strSql = @"select op.* from OutPatientRevisitRemind op inner join indeptinfo de
//	on op.inpatientid = de.inpatientid and op.inpatientdate = de.inpatientdate
//	where op.Status =1 and ("+strtemp+" = op.BeforeRemindDate) and de.area_id = '"+p_strAreaID.Trim()+"'";
				#endregion

                string strSql = @"select op.inpatientid,
       op.inpatientdate,
       op.inpatientenddate,
       op.status,
       op.revisittype,
       op.revisittime,
       op.beforereminddate,
       op.times,
       op.remaindtext,
       op.revisittypecontent
  from outpatientrevisitremind op inner join t_opr_bih_register de
	on op.inpatientid = de.inpatientid_chr and op.inpatientdate = de.inpatient_dat and de.status_int = 1
	where op.status =1 and (" + strtemp+" = op.beforereminddate) and de.areaid_chr = '"+p_strAreaID.Trim()+"'";
				DataTable dtbResult = new DataTable();
                clsHRPTableService m_objServ = new clsHRPTableService();
				 lngRes = m_objServ.DoGetDataTable(strSql,ref dtbResult);
				if(lngRes <= 0 || dtbResult.Rows.Count <= 0)
					return 0;
				p_objContentArr = new clsOutPatientRevisitRemind_VO[dtbResult.Rows.Count];
				for(int i=0;i<dtbResult.Rows.Count;i++)
				{
					p_objContentArr[i] = new clsOutPatientRevisitRemind_VO();
					p_objContentArr[i].m_StrInPatientID = dtbResult.Rows[i]["INPATIENTID"].ToString().Trim();
					try
					{
						p_objContentArr[i].m_DtmInPatientDate = DateTime.Parse(dtbResult.Rows[i]["INPATIENTDATE"].ToString());
					}
					catch{}
					try
					{
						p_objContentArr[i].m_DtmInPatientEndDate = DateTime.Parse(dtbResult.Rows[i]["INPATIENTENDDATE"].ToString());}
					catch{p_objContentArr[i].m_DtmInPatientEndDate = DateTime.MinValue;}
					try
					{
						p_objContentArr[i].m_IntRevisitType = Convert.ToInt32(dtbResult.Rows[i]["REVISITTYPE"]);}
					catch{p_objContentArr[i].m_IntRevisitType = 1;}
					try
					{
						p_objContentArr[i].m_IntRevisitTypeContent = Convert.ToInt32(dtbResult.Rows[0]["REVISITTYPECONTENT"]);}
					catch{p_objContentArr[i].m_IntRevisitTypeContent = 1;}
					try
					{
						p_objContentArr[i].m_IntStatus = Convert.ToInt32(dtbResult.Rows[i]["STATUS"]);}
					catch{p_objContentArr[i].m_IntStatus = 0;}
					try
					{
						p_objContentArr[i].m_IntTimes = Convert.ToInt32(dtbResult.Rows[i]["TIMES"]);}
					catch{p_objContentArr[i].m_IntTimes = 0;}
					try
					{
						p_objContentArr[i].m_IntBeforeRemindDate = Convert.ToInt32(dtbResult.Rows[i]["BEFOREREMINDDATE"]);}
					catch{p_objContentArr[i].m_IntBeforeRemindDate = 1;}

					p_objContentArr[i].m_StrRevisitTime = dtbResult.Rows[i]["REVISITTIME"].ToString().Trim();
					p_objContentArr[i].m_StrRemaindText = dtbResult.Rows[i]["REMAINDTEXT"].ToString().Trim();
				}
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;

		}

	#region 开机自动运行右下角浮动窗口
	/// <summary>
	/// 获取全部到期的复诊提示内容(根据科室)
	/// </summary>
	/// <param name="p_strAreaID"></param>
	/// <param name="p_objContentArr"></param>
	/// <returns></returns>
	[AutoComplete]
	public long m_lngGetAllNeedRemindRecordByDept(string p_strDeptID,string p_strBeforeRemindDate,out clsOutPatientRevisitRemind_VO[] p_objContentArr)
	{
		p_objContentArr = null;
		if(p_strDeptID == null || p_strDeptID == string.Empty)
			return -1;
		long lngRes = -1;
		try
		{
			string strtemp1 = "",strtemp2 = "";
			if(clsHRPTableService.bytDatabase_Selector == 2)
			{
				strtemp1 = "(floor(to_date(op.RevisitTime,'yyyy-mm-dd hh24:mi:ss')- sysdate) +'1')";
				strtemp2 = "sysdate < to_date(op.RevisitTime,'yyyy-mm-dd hh24:mi:ss')";
			}
			else
			{
				strtemp1 = "DATEDIFF ( day , getdate() , op.RevisitTime)";
				strtemp2 = "getdate() < op.RevisitTime";
			}

			#region 旧表
//			string strSql = @"select op.* from OutPatientRevisitRemind op inner join indeptinfo de
//on op.inpatientid = de.inpatientid and op.inpatientdate = de.inpatientdate
//where op.Status =1 and ("+strtemp1+" <= "+p_strBeforeRemindDate+") and "+strtemp2 +" and de.InDeptID = '"+p_strDeptID.Trim()+"'";
			#endregion

            string strSql = @"select op.inpatientid,
       op.inpatientdate,
       op.inpatientenddate,
       op.status,
       op.revisittype,
       op.revisittime,
       op.beforereminddate,
       op.times,
       op.remaindtext,
       op.revisittypecontent
  from outpatientrevisitremind op inner join t_opr_bih_register de
on op.inpatientid = de.inpatientid_chr and op.inpatientdate = de.inpatient_dat and de.status_int = 1
where op.status =1 and (" + strtemp1+" <= "+p_strBeforeRemindDate+") and "+strtemp2 +" and de.deptid_chr = '"+p_strDeptID.Trim()+"'";
			DataTable dtbResult = new DataTable();
            clsHRPTableService m_objServ = new clsHRPTableService();
			lngRes = m_objServ.DoGetDataTable(strSql,ref dtbResult);
			if(lngRes <= 0 || dtbResult.Rows.Count <= 0)
				return 0;
			p_objContentArr = new clsOutPatientRevisitRemind_VO[dtbResult.Rows.Count];
			for(int i=0;i<dtbResult.Rows.Count;i++)
			{
				p_objContentArr[i] = new clsOutPatientRevisitRemind_VO();
				p_objContentArr[i].m_StrInPatientID = dtbResult.Rows[i]["InPatientID"].ToString().Trim();
				try
				{
					p_objContentArr[i].m_DtmInPatientDate = DateTime.Parse(dtbResult.Rows[i]["InPatientDate"].ToString());
				}
				catch{}
				try
				{
					p_objContentArr[i].m_DtmInPatientEndDate = DateTime.Parse(dtbResult.Rows[i]["InPatientEndDate"].ToString());}
				catch{p_objContentArr[i].m_DtmInPatientEndDate = DateTime.MinValue;}
				try
				{
					p_objContentArr[i].m_IntRevisitType = Convert.ToInt32(dtbResult.Rows[i]["RevisitType"]);}
				catch{p_objContentArr[i].m_IntRevisitType = 1;}
				try
				{
					p_objContentArr[i].m_IntRevisitTypeContent = Convert.ToInt32(dtbResult.Rows[0]["RevisitTypeContent"]);}
				catch{p_objContentArr[i].m_IntRevisitTypeContent = 1;}
				try
				{
					p_objContentArr[i].m_IntStatus = Convert.ToInt32(dtbResult.Rows[i]["Status"]);}
				catch{p_objContentArr[i].m_IntStatus = 0;}
				try
				{
					p_objContentArr[i].m_IntTimes = Convert.ToInt32(dtbResult.Rows[i]["Times"]);}
				catch{p_objContentArr[i].m_IntTimes = 0;}
				try
				{
					p_objContentArr[i].m_IntBeforeRemindDate = Convert.ToInt32(dtbResult.Rows[i]["BeforeRemindDate"]);}
				catch{p_objContentArr[i].m_IntBeforeRemindDate = 1;}

				
				p_objContentArr[i].m_StrRevisitTime = dtbResult.Rows[i]["REVISITTIME"].ToString().Trim();
				p_objContentArr[i].m_StrRemaindText = dtbResult.Rows[i]["RemaindText"].ToString().Trim();
			}
		}
		catch(Exception objEx)
		{
			string strTmp=objEx.Message;
			com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
			bool blnRes = objLogger.LogError(objEx);
		}
		//返回
		return lngRes;
	}

		
	#endregion

		/// <summary>
		/// 设置已过期或不需提示的状态为‘2’
		/// </summary>
		/// <param name="p_blnSetToday"></param>
		/// <param name="p_strAreaID"></param>
		/// <returns></returns>
	[AutoComplete]	
	public long m_lngSetOutDateRemindStatus(bool p_blnSetToday,string p_strAreaID)
		{
			if(p_strAreaID == null || p_strAreaID == string.Empty)
				return -1;
			long lngRes = 0;
            clsHRPTableService m_objServ = new clsHRPTableService();
			try
			{
				string strtemp = "";
				if(clsHRPTableService.bytDatabase_Selector == 2)
					strtemp = " (floor(to_date(RevisitTime,'yyyy-mm-dd hh24:mi:ss')- sysdate) +'1') ";
				else
					strtemp = " DATEDIFF ( day , getdate() , RevisitTime) ";
				string strFH = p_blnSetToday?@">=":@">";
					string strSql = @"update outpatientrevisitremind set status = '2' where ("+strtemp+" "+strFH+@" beforereminddate) and status <> '2'
		and (inpatientid in (select inpatientid from t_opr_bih_register de
		where de.areaid_chr = '" + p_strAreaID.Trim() + "' and inpatientid = de.inpatientid_chr and inpatientdate = de.inpatient_dat and de.status_int = 1))";
				lngRes=	 m_objServ.DoExcute(strSql);
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
			
		}
		#endregion 复诊提醒

		#region 复诊记录
		/// <summary>
		/// 添加记录
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
	[AutoComplete]	
	public long m_lngAddRecordContent(clsOutPatientRevisitRecord_VO p_objContent)
		{
			if(p_objContent == null)
				return -1;
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {
                string strSql = @"insert into outpatientrevisitrecord(inpatientid,inpatientdate,opendate,createdate,createuserid,status,revisitrecord,inpatientenddate)
	values(?,?,?,?,?,?,?,?)";


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(8, out objDPArr);

                objDPArr[0].Value = p_objContent.m_StrInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objContent.m_DtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objContent.m_DtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objContent.m_DtmCreatedDate;
                objDPArr[4].Value = p_objContent.m_StrCreatedUserID;
                objDPArr[5].Value = 0;
                objDPArr[6].Value = p_objContent.m_StrRevisitRecord;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = p_objContent.m_DtmInPatientEndDate;
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

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
			//返回
			return lngRes;

		}
		/// <summary>
		/// 修改记录
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
	[AutoComplete]	
	public long m_lngModifyRecordContent(clsOutPatientRevisitRecord_VO p_objContent)
		{
			if(p_objContent == null)
				return -1;
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {
                string strSql = @"update outpatientrevisitrecord set opendate = ?,revisitrecord = ? where inpatientid = ? and inpatientdate = ? and createdate = ? and inpatientenddate = ?";


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objContent.m_DtmOpenDate;
                objDPArr[1].Value = p_objContent.m_StrRevisitRecord;
                objDPArr[2].Value = p_objContent.m_StrInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objContent.m_DtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objContent.m_DtmCreatedDate;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objContent.m_DtmInPatientEndDate;
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //m_objServ.Dispose();
            }
			//返回
			return lngRes;

		}
		/// <summary>
		/// 删除记录
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngDeleteRecordContent(clsOutPatientRevisitRecord_VO p_objContent)
		{
			if(p_objContent == null)
				return -1;
			string strSql = @"update outpatientrevisitrecord set status = ?,deactiveddate = ?,deactivedoperatorid = ? where inpatientid = ? and inpatientdate = ? and createdate = ? and inpatientenddate = ?";
//			IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[7];
//			for(int j=0;j<objDPArr.Length;j++)
//				objDPArr[j]=new Oracle.DataAccess.Client.OracleParameter();

			clsHRPTableService objHRPServ =new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			objHRPServ.CreateDatabaseParameter(7,out objDPArr);
            objDPArr[0].Value = p_objContent.m_IntStatus;
            objDPArr[1].DbType = DbType.DateTime;
			objDPArr[1].Value= p_objContent.m_DtmDeActivedDate;
			objDPArr[2].Value= p_objContent.m_StrDeActivedOperatorID;
            objDPArr[3].Value = p_objContent.m_StrInPatientID;
            objDPArr[4].DbType = DbType.DateTime;
            objDPArr[4].Value = p_objContent.m_DtmInPatientDate;
            objDPArr[5].DbType = DbType.DateTime;
            objDPArr[5].Value = p_objContent.m_DtmCreatedDate;
            objDPArr[6].DbType = DbType.DateTime;
			objDPArr[6].Value= p_objContent.m_DtmInPatientEndDate;
			long lngEff = 0;
            long lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            //objHRPServ.Dispose();
			return lngRes;
		}
		/// <summary>
		/// 获取未删除记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_dtmInPatientDate"></param>
		/// <param name="p_objContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
	public long m_lngGetRecordContentByInPatient(string p_strInPatientID,DateTime p_dtmInPatientDate,out clsOutPatientRevisitRecord_VO[] p_objContentArr)
		{
			p_objContentArr = null;
			long lngRes = 0;
			try
            {
				if(p_strInPatientID == null || p_strInPatientID == string.Empty || p_dtmInPatientDate == DateTime.MinValue)
                    return -1;
                clsHRPTableService m_objServ = new clsHRPTableService();
                string strSql = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       status,
       firstprintdate,
       deactiveddate,
       deactivedoperatorid,
       inpatientenddate,
       revisitrecord
  from outpatientrevisitrecord
 where inpatientid = ?
   and inpatientdate = ?
   and status = 0
 order by createdate desc";

                IDataParameter[] objDPArr = null;
                m_objServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInPatientDate;

                lngRes= m_lngGetRecordContentBySQL(strSql,objDPArr,out p_objContentArr);
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;

		}
		/// <summary>
		/// 获取全部已删除记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_dtmInPatientDate"></param>
		/// <param name="p_objContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
	public long m_lngGetAllDeActivedRecordContent(string p_strInPatientID,DateTime p_dtmInPatientDate,out clsOutPatientRevisitRecord_VO[] p_objContentArr)
		{
			p_objContentArr = null;
			if(p_strInPatientID == null || p_strInPatientID == string.Empty || p_dtmInPatientDate == DateTime.MinValue)
				return -1;
			long lngRes = 0;
			try
            {
                clsHRPTableService m_objServ = new clsHRPTableService();
                string strSql = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       status,
       firstprintdate,
       deactiveddate,
       deactivedoperatorid,
       inpatientenddate,
       revisitrecord
  from outpatientrevisitrecord
 where inpatientid = ?
   and inpatientdate = ?
   and status = 1";

                IDataParameter[] objDPArr = null;
                m_objServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInPatientDate;

				lngRes= m_lngGetRecordContentBySQL(strSql,objDPArr,out p_objContentArr);
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;

		}
		/// <summary>
		/// 获取已删除记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_dtmInPatientDate"></param>
		/// <param name="p_objContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
	public long m_lngGetDeActivedRecordContent(string p_strInPatientID,DateTime p_dtmInPatientDate,DateTime p_dtmCreatedDate,out clsOutPatientRevisitRecord_VO p_objContent)
		{
			p_objContent = null;
			if(p_strInPatientID == null || p_strInPatientID == string.Empty || p_dtmInPatientDate == DateTime.MinValue)
				return -1;
			long lngRes = 0;
			try
            {
                clsHRPTableService m_objServ = new clsHRPTableService();
                string strSql = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       status,
       firstprintdate,
       deactiveddate,
       deactivedoperatorid,
       inpatientenddate,
       revisitrecord
  from outpatientrevisitrecord
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
   and status = 1";
				clsOutPatientRevisitRecord_VO[] p_objContentArr = null;
                IDataParameter[] objDPArr = null;
                m_objServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmCreatedDate;

				 lngRes = m_lngGetRecordContentBySQL(strSql,objDPArr,out p_objContentArr);
				if(lngRes > 0 && p_objContentArr != null && p_objContentArr.Length == 1)
				{
					p_objContent = p_objContentArr[0];
				}
			
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;

		}
		/// <summary>
		/// 获取记录
		/// </summary>
		/// <param name="p_strSql"></param>
		/// <param name="p_objContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
	private long m_lngGetRecordContentBySQL(string p_strSql,IDataParameter[] p_objParameter,out clsOutPatientRevisitRecord_VO[] p_objContentArr)
		{
			p_objContentArr = null;
			if(p_strSql == null || p_strSql == string.Empty)
				return -1;
			long lngRes = 0;
			try
			{
                clsHRPTableService m_objServ = new clsHRPTableService();
				DataTable dtbResult = new DataTable();
                lngRes = m_objServ.lngGetDataTableWithParameters(p_strSql, ref dtbResult, p_objParameter);
				if(lngRes <= 0 || dtbResult.Rows.Count <= 0)
					return 0;
				p_objContentArr = new clsOutPatientRevisitRecord_VO[dtbResult.Rows.Count];
				for(int i=0;i<dtbResult.Rows.Count;i++)
				{
					p_objContentArr[i] = new clsOutPatientRevisitRecord_VO();
					p_objContentArr[i].m_StrInPatientID = dtbResult.Rows[i]["INPATIENTID"].ToString().Trim();
					try
					{
						p_objContentArr[i].m_DtmInPatientDate = Convert.ToDateTime(dtbResult.Rows[i]["INPATIENTDATE"]);
					}
					catch{p_objContentArr[i].m_DtmInPatientDate = DateTime.MinValue;}
					try
					{
						p_objContentArr[i].m_DtmCreatedDate = Convert.ToDateTime(dtbResult.Rows[i]["CREATEDATE"]);
					}
					catch{p_objContentArr[i].m_DtmCreatedDate = DateTime.MinValue;} 
					try
					{
						p_objContentArr[i].m_DtmInPatientEndDate = Convert.ToDateTime(dtbResult.Rows[i]["INPATIENTENDDATE"]);
					}
					catch{p_objContentArr[i].m_DtmInPatientEndDate = DateTime.MinValue;}
					p_objContentArr[i].m_StrCreatedUserID = dtbResult.Rows[i]["CREATEUSERID"].ToString().Trim();
					p_objContentArr[i].m_StrRevisitRecord = dtbResult.Rows[i]["REVISITRECORD"].ToString().Trim();
					p_objContentArr[i].m_StrDeActivedOperatorID = dtbResult.Rows[i]["DEACTIVEDOPERATORID"].ToString().Trim();
					try
					{
						p_objContentArr[i].m_DtmOpenDate = Convert.ToDateTime(dtbResult.Rows[i]["OPENDATE"]);
					}
					catch{p_objContentArr[i].m_DtmOpenDate = DateTime.Now;}
					try
					{
						p_objContentArr[i].m_DtmDeActivedDate = Convert.ToDateTime(dtbResult.Rows[i]["DEACTIVEDDATE"]);
					}
					catch{p_objContentArr[i].m_DtmDeActivedDate = DateTime.MinValue;}
					try
					{
						p_objContentArr[i].m_IntStatus = Convert.ToInt32(dtbResult.Rows[i]["STATUS"]);
					}
					catch{p_objContentArr[i].m_IntStatus = 0;}
				}
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;

		}
		#endregion 复诊记录
	}
}
