using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.iCare.Public.MenuExtend.Service
{
	/// <summary>
	/// Summary description for clsThreeMeasureShareService.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsThreeMeasureShareService2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetFirstValue(string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = @"select *
								from
								(select top 1 DiastolicValue
								from(select '1' as dd
								)as Dumm full outer join
								(---------------------------------
								select top 1 DiastolicValue
								from ThreeMeasureRecord a,ThreeMeasureRecordContent b,
								(select InPatientID,InPatientDate,OpenDate,Max(ModifyDate) as ModifyDate
								from ThreeMeasureRecordContent
								group by InPatientID,InPatientDate,OpenDate
								having InPatientID = '"+p_strInPaitentID+@"'
								and InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
								)as Base
								where b.InPatientID = a.InPatientID
								and b.InPatientDate = a.InPatientDate
								and b.OpenDate = a.OpenDate
								and b.InPatientID = Base.InPatientID
								and b.InPatientDate = Base.InPatientDate
								and b.OpenDate = Base.OpenDate
								and b.ModifyDate = Base.ModifyDate
								and a.Status = 0
								and DiastolicValue is not null
								---------------------------------
								)as DV
								on Dumm.dd = DV.DiastolicValue
								order by DiastolicValue desc
								)as DV ,
								(select top 1 SystolicValue
								from(select '1' as dd
								)as Dumm full outer join
								(-----------------------------
								select top 1 SystolicValue
								from ThreeMeasureRecord a,ThreeMeasureRecordContent b,
								(select InPatientID,InPatientDate,OpenDate,Max(ModifyDate) as ModifyDate
								from ThreeMeasureRecordContent
								group by InPatientID,InPatientDate,OpenDate
								having InPatientID = '"+p_strInPaitentID+@"'
								and InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
								)as Base
								where b.InPatientID = a.InPatientID
								and b.InPatientDate = a.InPatientDate
								and b.OpenDate = a.OpenDate
								and b.InPatientID = Base.InPatientID
								and b.InPatientDate = Base.InPatientDate
								and b.OpenDate = Base.OpenDate
								and b.ModifyDate = Base.ModifyDate
								and a.Status = 0
								and SystolicValue is not null
								----------------------------
								)as SV
								on Dumm.dd = SV.SystolicValue
								order by SystolicValue desc
								)as SV,
								(select top 1 DiastolicValue2
								from(select '1' as dd
								)as Dumm full outer join
								(---------------------
								select top 1 DiastolicValue2
								from ThreeMeasureRecord a,ThreeMeasureRecordContent b,
								(select InPatientID,InPatientDate,OpenDate,Max(ModifyDate) as ModifyDate
								from ThreeMeasureRecordContent
								group by InPatientID,InPatientDate,OpenDate
								having InPatientID = '"+p_strInPaitentID+@"'
								and InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
								)as Base
								where b.InPatientID = a.InPatientID
								and b.InPatientDate = a.InPatientDate
								and b.OpenDate = a.OpenDate
								and b.InPatientID = Base.InPatientID
								and b.InPatientDate = Base.InPatientDate
								and b.OpenDate = Base.OpenDate
								and b.ModifyDate = Base.ModifyDate
								and a.Status = 0
								and DiastolicValue2 is not null
								------------------------
								)as D2V
								on Dumm.dd = D2V.DiastolicValue2
								order by DiastolicValue2 desc
								)as D2V,
								(select top 1 SystolicValue2
								from(select '1' as dd
								)as Dumm full outer join
								(-----------------
								select top 1 SystolicValue2
								from ThreeMeasureRecord a,ThreeMeasureRecordContent b,
								(select InPatientID,InPatientDate,OpenDate,Max(ModifyDate) as ModifyDate
								from ThreeMeasureRecordContent
								group by InPatientID,InPatientDate,OpenDate
								having InPatientID = '"+p_strInPaitentID+@"'
								and InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
								)Base
								where b.InPatientID = a.InPatientID
								and b.InPatientDate = a.InPatientDate
								and b.OpenDate = a.OpenDate
								and b.InPatientID = Base.InPatientID
								and b.InPatientDate = Base.InPatientDate
								and b.OpenDate = Base.OpenDate
								and b.ModifyDate = Base.ModifyDate
								and a.Status = 0
								and SystolicValue2 is not null
								-------------------
								)S2V
								on Dumm.dd = S2V.SystolicValue2
								order by SystolicValue2 desc
								)S2V,
								(select top 1 PulseValue
								from(select '1' as dd
								)as Dumm full outer join
								(---------------
								select top 1 PulseValue
								from ThreeMeasureRecord a,ThreeMeasureRecContAccess b,
								(select InPatientID,InPatientDate,OpenDate,Max(ModifyDate) as ModifyDate
								from ThreeMeasureRecContAccess
								group by InPatientID,InPatientDate,OpenDate
								having InPatientID = '"+p_strInPaitentID+@"'
								and InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
								)Base
								where b.InPatientID = a.InPatientID
								and b.InPatientDate = a.InPatientDate
								and b.OpenDate = a.OpenDate
								and b.InPatientID = Base.InPatientID
								and b.InPatientDate = Base.InPatientDate
								and b.OpenDate = Base.OpenDate
								and b.ModifyDate = Base.ModifyDate
								and a.Status = 0
								and PulseValue is not null
								--------------
								)PV
								on Dumm.dd = PV.PulseValue
								order by PulseValue desc
								)PV,
								(select top 1 TemperatureValue
								from(select '1' as dd
								)Dumm full outer join
								(----------------
								select top 1 TemperatureValue
								from ThreeMeasureRecord a,ThreeMeasureRecContAccess b,
								(select InPatientID,InPatientDate,OpenDate,Max(ModifyDate) as ModifyDate
								from ThreeMeasureRecContAccess
								group by InPatientID,InPatientDate,OpenDate
								having InPatientID = '"+p_strInPaitentID+@"'
								and InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
								)Base
								where b.InPatientID = a.InPatientID
								and b.InPatientDate = a.InPatientDate
								and b.OpenDate = a.OpenDate
								and b.InPatientID = Base.InPatientID
								and b.InPatientDate = Base.InPatientDate
								and b.OpenDate = Base.OpenDate
								and b.ModifyDate = Base.ModifyDate
								and a.Status = 0
								and TemperatureValue is not null
								------------------
								)TV
								on Dumm.dd = TV.TemperatureValue
								order by TemperatureValue desc
								)as TV,
								(select top 1 BreathValue
								from(select '1' as dd
								)as Dumm full outer join
								(-----------------
								select top 1 BreathValue
								from ThreeMeasureRecord a,ThreeMeasureRecContAccess b,
								(select InPatientID,InPatientDate,OpenDate,Max(ModifyDate) as ModifyDate
								from ThreeMeasureRecContAccess
								group by InPatientID,InPatientDate,OpenDate
								having InPatientID = '"+p_strInPaitentID+@"'
								and InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
								)Base
								where b.InPatientID = a.InPatientID
								and b.InPatientDate = a.InPatientDate
								and b.OpenDate = a.OpenDate
								and b.InPatientID = Base.InPatientID
								and b.InPatientDate = Base.InPatientDate
								and b.OpenDate = Base.OpenDate
								and b.ModifyDate = Base.ModifyDate
								and a.Status = 0
								and BreathValue is not null
								-----------------------
								)BV
								on Dumm.dd = BV.BreathValue
								order by BreathValue desc
								)BV";
			#endregion SQL

			p_dtbResult = new DataTable();

			return new clsHRPTableService().DoGetDataTable (strSQL, ref p_dtbResult);
		
		}

		/// <summary>
		/// 获取最邻近时间的数据
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtmCompare"></param>
		/// <param name="p_strResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetNearestValue(string p_strInPaitentID,string p_strInPatientDate,DateTime p_dtmCompare, out string[] p_strResult)
		{
			p_strResult = new string[4];
            
			for(int i = 0; i < p_strResult.Length; i++)
			{
				string strSql = "";
				switch(i)
				{
					case 0:
						#region 体温
						strSql = @"select b.CreateTime,b.TemperatureValue
from ThreeMeasureRecord as a 
inner join ThreeMeasureRecContAccess as b
on a.InPatientID = b.InPatientID
and a.InPatientDate = b.InPatientDate
and a.OpenDate = b.OpenDate
where a.InPatientID = '"+p_strInPaitentID+@"'
and a.InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
and a.Status = '0'
and b.TemperatureValue is not null
and b.ModifyDate in 
(select max(ModifyDate) as ModifyDate 
from ThreeMeasureRecordContent
group by InPatientID,InPatientDate,OpenDate
having InPatientID = '"+p_strInPaitentID+@"'
and InPatientDate = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+")";
			#endregion
						break;
					case 1:
						#region 脉搏
						strSql = @"select b.CreateTime,b.PulseValue
from ThreeMeasureRecord as a 
inner join ThreeMeasureRecContAccess as b
on a.InPatientID = b.InPatientID
and a.InPatientDate = b.InPatientDate
and a.OpenDate = b.OpenDate
where a.InPatientID = '"+p_strInPaitentID+@"'
and a.InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
and a.Status = '0'
and b.PulseValue is not null
and b.ModifyDate in 
(select max(ModifyDate) as ModifyDate 
from ThreeMeasureRecordContent
group by InPatientID,InPatientDate,OpenDate
having InPatientID = '"+p_strInPaitentID+@"'
and InPatientDate = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+")";
			#endregion
						break;
					case 2:
						#region 呼吸
						strSql = @"select b.CreateTime,b.BreathValue
from ThreeMeasureRecord as a 
inner join ThreeMeasureRecContAccess as b
on a.InPatientID = b.InPatientID
and a.InPatientDate = b.InPatientDate
and a.OpenDate = b.OpenDate
where a.InPatientID = '"+p_strInPaitentID+@"'
and a.InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
and a.Status = '0'
and b.BreathValue is not null
and b.ModifyDate in 
(select max(ModifyDate) as ModifyDate 
from ThreeMeasureRecordContent
group by InPatientID,InPatientDate,OpenDate
having InPatientID = '"+p_strInPaitentID+@"'
and InPatientDate = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+")";
			#endregion
						break;
					case 3:
						#region 血压
						strSql = @"select a.CreateDate as CreateTime,(rtrim(b.SystolicValue) + '/' + rtrim(b.DiastolicValue)) as PressureValue
from ThreeMeasureRecord as a 
inner join ThreeMeasureRecordContent as b
on a.InPatientID = b.InPatientID
and a.InPatientDate = b.InPatientDate
and a.OpenDate = b.OpenDate
where a.InPatientID = '"+p_strInPaitentID+@"'
and a.InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
and a.Status = '0'
and b.DiastolicValue is not null
and b.SystolicValue is not null
and b.ModifyDate in 
(select max(ModifyDate) as ModifyDate 
from ThreeMeasureRecordContent
group by InPatientID,InPatientDate,OpenDate
having InPatientID = '"+p_strInPaitentID+@"'
and InPatientDate = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+")";
						#endregion
						break;
				}

				p_strResult[i] = m_strGetNearestValue(p_dtmCompare,strSql);
			}
	
			return 1;
		}
		
		[AutoComplete]
		private string m_strGetNearestValue(DateTime p_dtmCompare,string p_strSql)
		{
			DataTable dtResult = new DataTable();
			long lngRes = new clsHRPTableService().DoGetDataTable (p_strSql, ref dtResult);

			if(lngRes <=0 || dtResult.Rows.Count == 0)
				return "";

			TimeSpan tsPre =  p_dtmCompare - DateTime.Parse(dtResult.Rows[0]["CREATETIME"].ToString()).AddHours(2);//4pm -> 14:00:00
			int intNearestIndex = 0;

			for(int i = 1; i < dtResult.Rows.Count; i++)
			{
				TimeSpan tsNow = p_dtmCompare - DateTime.Parse(dtResult.Rows[i][0].ToString()).AddHours(2);

				if(tsNow.Duration() < tsPre.Duration())
				{
					intNearestIndex = i;
					tsPre = tsNow;
				}
			}

			return dtResult.Rows[intNearestIndex][1].ToString();
		}


	}
}
